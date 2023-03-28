import copy
import datetime
import math
import os
from sys import stdout

import numpy as np
# import logging
import pandas as pd
from dateutil.relativedelta import relativedelta
from drf_spectacular.utils import extend_schema, OpenApiParameter
from numpy import NaN
from rest_framework.authentication import SessionAuthentication, TokenAuthentication
from rest_framework.decorators import api_view, permission_classes, parser_classes, authentication_classes
from rest_framework.parsers import MultiPartParser
from rest_framework.permissions import IsAuthenticated
from rest_framework.utils import json

from app import settings
from app.settings import MEDIA_ROOT
from core import dictionaries
from core.dictionaries import finanzas, conceptosAbreviado
from core.models import Empresa, Documento, Contabilidad, Pool, Extracciones, Extracciones_Errores, Analitica, Ratio, \
    Crudos
from rest_framework.response import Response

from core.serializers import PoolSerializer, ContabilidadSerializer
from core.views import get_set_contracts, servicio
from sources.BSS import BSS
from sources.XLSX import detect_fields
from sources.cirbe import cirbe
from sources.modelo200 import modelo200
from sources.extractFromPDF import extractJsonFromPDF
from rest_framework import status, viewsets
from django.db.models import Q

def checkActivoPasivo(doc, extraccion):
    activo = Contabilidad.objects.get(documento=doc, concepto='total activo').magnitud
    pasivo = Contabilidad.objects.get(documento=doc, concepto='total patrimonio neto y pasivo').magnitud
    if activo - pasivo != 0:
        extraccion_error = Extracciones_Errores(**{'extraccion': extraccion, 'mensaje': 'Activo y pasivo no cuadran',
                                                   'traza': f'Activo: {activo}. Pasivo: {pasivo}. Diferencia: {activo - pasivo}',
                                                   'bloqueo': 3})
        extraccion_error.save()
    return activo - pasivo


def upload_generico(file, fecha, tipo, CIF, extraccion_id):

    fechahora = datetime.datetime.now()
    #print(file)
    params = {'tipo': tipo, 'fechahora': fechahora, 'ruta': file.name, 'resultado': 'ko'}
    if extraccion_id:
        extraccion = Extracciones.objects.update_or_create(id=extraccion_id, defaults=params)[0]
        extraccion.save()
        Extracciones_Errores.objects.filter(extraccion=extraccion).delete()
    else:
        extraccion = Extracciones(**params)
        extraccion.save()

    if not os.path.exists(os.path.join(MEDIA_ROOT, CIF)):
        os.makedirs(os.path.join(MEDIA_ROOT, CIF))
        os.makedirs(os.path.join(MEDIA_ROOT, CIF, 'BSS'))
        os.makedirs(os.path.join(MEDIA_ROOT, CIF, 'Cirbe'))
        os.makedirs(os.path.join(MEDIA_ROOT, CIF, 'Modelo200'))
        os.makedirs(os.path.join(MEDIA_ROOT, CIF, 'Resultados'))

    try:
        empresa = Empresa.objects.get(CIF=CIF)
    except:
        #print('Empresa no existe. Crear empresa antes de cargar el documento')
        return 'empresa', extraccion

    try:
        date = fecha.split('-')
        date = datetime.date(int(date[0]), int(date[1]), int(date[2]))
        if date > datetime.date.today() + relativedelta(days=1):
            #print('Fecha fuera de rango')
            return 'fecha', extraccion
    except:
        #print('Fecha en formato incorrecto')
        return 'fecha', extraccion

    original = os.path.basename(file.name)
    extension = os.path.splitext(file.name)[1]
    compare = os.path.join(CIF, tipo, original.rsplit('.', 1)[0] + '_' + str(fecha) + extension).replace(' ','_')
    present = os.path.join(MEDIA_ROOT, compare)
    #print(f'original: {file.name}, extension: {extension}, basepath: {os.path.basename(file.name)}')
    #print(f'Antes de la query: {compare}\n\t{present}')
    if os.path.isfile(present):
        #print('Fichero existe y se borra')
        os.remove(present)

    """
    try:
        check = Documento.objects.get(origen=tipo, fecha=date, empresa=empresa)
        if check:
            check.delete()
    except:
        pass
    """
    params = {'documento': file, 'origen': tipo, 'fecha': fecha, 'empresa': empresa, 'extraccion': extraccion}
    #print(f'Params: {params}')
    """
    try:
        Documento.objects.get(documento=compare).delete()
    except Exception as e:
        print('Except: ', e)
    finally:
    """
    if tipo == Documento.BSS:
        # location = os.path.join(MEDIA_ROOT, CIF, 'BSS', os.path.splitext(file.name)[0].split('/')[-1] + '_' + str(fecha) + '.xlsx')
        file.name = 'BSS_' + original
        #print('BSS: ', file.name)
    elif tipo == Documento.CIRBE:
        #location = os.path.join(MEDIA_ROOT, CIF, 'Cirbe', os.path.splitext(file.name)[0].split('/')[-1] + '_' + str(fecha) + '.pdf')
        """
        fecha_pool = Pool.objects.filter(documento__empresa__CIF=CIF).latest('documento__fecha').documento.fecha
        if fecha > fecha_pool:
            return Response({'error': 'Cargar el BSS antes del Cirbe'}, status.HTTP_417_EXPECTATION_FAILED)
        """
        file.name = 'Cirbe_' + original
        #print('Cirbe: ', file.name)
    elif tipo == Documento.MODELO200:
        # location = os.path.join(MEDIA_ROOT, CIF, 'Modelo200', os.path.splitext(file.name)[0].split('/')[-1] + '_' + str(fecha) + '.pdf')
        file.name = '200_' + original
        #print('Modelo 200: ', file.name)
    else:
        return 'tipoDoc', extraccion

    #print('Parametros: ', params)

    extension = file.name.split('.')[-1]
    if (tipo == 'BSS' and extension == 'pdf') or ((tipo == 'Modelo200' or tipo == 'Cirbe') and (extension == 'xls' or extension == 'xlsx')):
        return 'extension', extraccion

    doc, created = Documento.objects.update_or_create(origen=tipo, fecha=date, empresa=empresa,
                                                      defaults={'documento': file, 'status': False, 'extraccion': extraccion})
    # si es un archivo erróneo y se detecta más adelante, se habrá de borrar

    return doc, extraccion

def handle_uploaded_file(location, f):
    location = os.path.join(os.path.dirname(location), f.name)
    #print(location)
    with open(location, 'wb+') as destination:
        for chunk in f.chunks():
            destination.write(chunk)
    destination.close()
    return location


"""
@extend_schema(parameters=[OpenApiParameter(name='file', description='Fichero a cargar', required=True),
                           OpenApiParameter(name='fecha', description='Fecha de la info del fichero', required=True, type=str),
                           OpenApiParameter(name='tipoDoc', description='Tipo de documento', required=True, type=str),
                           OpenApiParameter(name='CIF', description='CIF empresa', required=True, type=str)])
"""


def return_function(arguments, status):

    print('Return function: ', arguments)
    extraccion_error = Extracciones_Errores(**arguments)
    extraccion_error.save()
    print('todo bien')
    return Response({'error': arguments['mensaje']}, status=status)


@api_view(['POST'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@parser_classes((MultiPartParser,))
def upload(request):

    # logging.basicConfig(filename='smartdebt.log', filemode='w', level=logging.DEBUG)
    # logging.info('Start')
    try:
        extraccion_id = request.POST['extraccion_id']
        extraccion = Extracciones.objects.get(id=extraccion_id)
    except:
        #print('No se ha identificado un id correcto de extracción, se generará un registro nuevo para el procesamiento del documento')
        extraccion = Extracciones(**{'fechahora': datetime.datetime.now(), 'ruta': 'empty', 'resultado': 'ko'})
        extraccion.save()    #print(f'tipo {tipo}\tCIF {CIF}\tfecha {fecha}')

    try:
        fecha = request.POST['fecha']
        test = fecha.split('-')
        test = datetime.date(int(test[0]), int(test[1]), int(test[2]))
        if test > datetime.date.today() + relativedelta(days=1):
            raise Exception()
    except Exception as e:
        return return_function(arguments={'extraccion': extraccion, 'mensaje': 'Fecha faltante o fecha a futuro o en formato incorrecto (usar yyyy-mm-dd)',
                                          'traza': f'{e}', 'bloqueo': 1}, status=status.HTTP_400_BAD_REQUEST)

    tipoDocs = dict(Documento.choices_tipo)
    try:
        tipo = request.POST['tipoDoc']
        CIF = request.POST['CIF']
        if tipo not in tipoDocs.keys():
            return return_function(arguments={'extraccion': extraccion,
                                              'mensaje': f'{tipo} no está entre las opciones válidas: {tipoDocs}',
                                              'bloqueo': 1}, status=status.HTTP_400_BAD_REQUEST)
        try:
            Empresa.objects.get(CIF=CIF)
        except Exception as e:
            return return_function(arguments={'extraccion': extraccion, 'mensaje': f'Empresa {CIF} no existe', 'bloqueo': 1,
                                              'traza': f'{e}'}, status=status.HTTP_400_BAD_REQUEST)
    except Exception as e:
        return return_function(arguments={'extraccion': extraccion, 'mensaje': 'Falta CIF de la empresa y/o tipo de documento',
                                          'traza': f'{e}', 'bloqueo': 1}, status=status.HTTP_400_BAD_REQUEST)
    extraccion.tipo = tipo
    extraccion.save()

    try:
        file = request.FILES['file']
    except Exception as e:
        return return_function(arguments={'extraccion': extraccion, 'mensaje': f'No se ha adjuntado fichero a la petición de procesamiento',
                                          'bloqueo': 1, 'traza': f'{e}'}, status=status.HTTP_400_BAD_REQUEST)
    #print(f'file {file}\ttipo {tipo}\tCIF {CIF}\tfecha {fecha}')

    try:
        doc, extraccion = upload_generico(file, fecha, tipo, CIF, extraccion.pk)
    except Exception as e:
        return return_function(arguments={'extraccion': extraccion, 'mensaje': f'Fallo del upload_generico',
                                          'bloqueo': 1, 'traza': f'{e}'}, status=status.HTTP_500_INTERNAL_SERVER_ERROR)

    if doc == 'empresa' or doc == 'extension' or doc == 'tipoDoc':
        if doc == 'empresa':
            mensaje = 'empresa no existe; crear empresa antes de cargar documentos'
        elif doc == 'extension':
            mensaje = 'la extensión del fichero no se corresponde con el tipo de documento a procesar'
        elif doc == 'tipoDoc':
            mensaje = 'tipo de documento erróneo'
        else:
            mensaje = 'error no controlado'
        
        extraccion.mensaje = mensaje
        extraccion.save()
        return Response({'error': mensaje}, status=status.HTTP_417_EXPECTATION_FAILED)
    
    if tipo == 'BSS':
        conceptos = {}
        try:
            conceptos['variacion'] = float(request.POST['variacion'])
        except Exception as e:
            print(f'No hay o no se ha procesado la variación de existencias: {e}')
            conceptos['variacion'] = None
        try:
            conceptos['amortizacion'] = float(request.POST['amortizacion'])
        except Exception as e:
            print(f'No hay o no se ha procesado la variación de existencias: {e}')
            conceptos['amortizacion'] = None
        try:
            conceptos['impuestos'] = float(request.POST['impuestos'])
        except Exception as e:
            print(f'No hay o no se ha procesado la variación de existencias: {e}')
            conceptos['impuestos'] = None

        if doc.empresa.configFile:
            print('Tiene configFile')
            try:
                result, success = BSS(doc, conceptos, extraccion)
            except Exception as e:
                return return_function(arguments={'extraccion':extraccion, 'mensaje': 'Fallo en alguna de las rutinas no controladas',
                                                  'traza': f'{e}', 'bloqueo': 1}, status=status.HTTP_500_INTERNAL_SERVER_ERROR)
            if success:
                extraccion.resultado = 'ok'
                extraccion.tipo = Documento.BSS
                extraccion.save()
                checkActivoPasivo(doc, extraccion)
                # logging.info('Finish')
                # result.refresh_from_db()
                return Response({'conceptos_pendientes': result})
            else:
                doc.delete()
                return return_function(arguments=result, status=status.HTTP_500_INTERNAL_SERVER_ERROR)

        else:
            print('No tiene configFile')
            configFile, doc2 = detect_fields(doc, extraccion)
            print('Retorno de detect_fieds en views: ', configFile, doc2)
            if doc2:
                result, success = BSS(doc2, conceptos, extraccion)
                print('Result, success de no tener configFile: ', result, success)
                if success:
                    extraccion.resultado = 'ok'
                    extraccion.tipo = Documento.BSS
                    extraccion.save()
                    checkActivoPasivo(doc2, extraccion)
                    #result.refresh_from_db()
                    return Response({'conceptos_pendientes': result})
                else:
                    print(f'Doc2: {result["mensaje"]}')
                    doc2.delete()
                    return return_function(arguments=result, status=status.HTTP_418_IM_A_TEAPOT)
            else:
                print('No Doc2: No se ha detectado con éxito la estructura del excel. Proceder a edición manual.')
                doc2.delete()
                return return_function(arguments={'extraccion':extraccion, 'mensaje': 'No se ha detectado con éxito la estructura '
                                                                                      'del excel. Proceder a edición manual.',
                                                  'bloqueo': 2}, status=status.HTTP_418_IM_A_TEAPOT)

    else:
        fichero = extractJsonFromPDF(doc.documento)
        #print(fichero)
        if not fichero:
            doc.delete()
            return return_function(arguments={'extraccion': extraccion, 'mensaje': 'Error de la API de Acrobat',
                                              'bloqueo': 1}, status=status.HTTP_415_UNSUPPORTED_MEDIA_TYPE)
        #fichero = os.path.join(MEDIA_ROOT, CIF, 'Modelo200', 'MODELO_200_2020_BSV_2020-12-31.json')
        #fichero = os.path.join(MEDIA_ROOT, CIF, 'Modelo200', os.path.splitext(original)[0].split('/')[-1].replace(' ', '_') + '_' + str(fecha) + '.json')
        #print(fichero)
        if tipo == 'Cirbe':
            try:
                output, success = cirbe(fichero, doc, extraccion)
                print('Cirbe: ', output, success)
            except Exception as e:
                doc.delete()
                return return_function({'extraccion': extraccion, 'mensaje': f'Fallo no-controlado del Cirbe',
                                        'traza': f'{e}', 'bloqueo': 1}, status=status.HTTP_418_IM_A_TEAPOT)
            if success:
                extraccion.resultado = 'ok'
                extraccion.tipo = tipo
                extraccion.save()
                # logging.info('Finish')
                return Response(output)
            else:
                doc.delete()
                return return_function(arguments=output, status=status.HTTP_400_BAD_REQUEST)

        elif tipo == 'Modelo200':
            try:
                #print(fichero)
                output, success = modelo200(fichero, doc, extraccion)
                print(f'Output: {output}, Success: {success}')
            except Exception as e:
                #print('Hay algo en modelo200 que no estaba controlado por try-except y que no ha funcionado')
                #print(f'{e}')
                doc.delete()
                #print(f'Fallo no-controlado de modelo200: {e}')
                return return_function({'extraccion': extraccion, 'mensaje': 'Error no controlado de modelo200',
                                        'traza': f'{e}', 'bloqueo': 1}, status=status.HTTP_418_IM_A_TEAPOT)
            if not success:
                doc.delete()
                print(output)
                return return_function(arguments=output, status=status.HTTP_500_INTERNAL_SERVER_ERROR)
            else:
                extraccion.resultado = 'ok'
                extraccion.tipo = tipo
                extraccion.save()
                if output:
                    extraccion_registro = Extracciones_Errores(**{'extraccion': extraccion,
                                                                  'mensaje': 'Posibles conceptos faltantes en procesamiento modelo200',
                                                                  'traza': f'{output}', 'bloqueo': 3})
                    extraccion_registro.save()
                #print(f'Se ha cambiado resultado a {extraccion.resultado}')
            return Response(output)

        else:
            doc.delete()
            return return_function({'extraccion': extraccion, 'mensaje': 'Tipo de archivo erróneo; tipos válidos = BSS, Modelo200 o Cirbe',
                                    'bloqueo': 2}, status.HTTP_406_NOT_ACCEPTABLE)

"""
@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='CIF', description='NIF de empresa de interés', required=True, type=str),
                           OpenApiParameter(name='fecha', description='fecha de interés', required=False, type=str)])
def resumenPyG(request):

    CIF = request.GET['CIF']
    try:
        empresa = Empresa.objects.get(CIF=CIF)
    except Exception as e:
        return Response({'error': f'Empresa {CIF} no existe. Mensaje: {e}'})

    try:
        fecha = request.GET['fecha'].split('-')
        fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
    except:
        # print('Falta el parámetro "fecha" o no se ha enviado con el formato y cifras correctas. Se toma la fecha actual por defecto')
        fecha = datetime.date.today()

    try:
        actual = Documento.objects.filter(fecha__lte=fecha, origen='BSS', empresa=empresa).latest('fecha')
    except Exception as e:
        return Response({'error': f'No se dispone de un Balance de Sumas y Saldos sobre el que calcular la contabilidad analítica, '
                                  f'por favor cargar un BSS: {e}'}, status=status.HTTP_404_NOT_FOUND)

    fecha = actual.fecha
    doc = actual

    df = pd.DataFrame(columns=['concepto', 'magnitud', 'porcentaje_gastos', 'porcentaje_ventas', 'porcentaje_ingresos'])
    bbdd = Analitica.objects.filter(documento=doc)
    #print('Query de Analítica: ', bbdd)
    gastos = bbdd.get(cuenta='total gastos').magnitud
    ventas = bbdd.get(cuenta='total ventas').magnitud
    ingresos = bbdd.get(cuenta='total ingresos').magnitud

    for element in analitica.keys():
        df.loc[len(df)] = [element, bbdd.get(cuenta=element).magnitud, 0, 0, 0]

    df.loc[len(df)] = ['total ventas', ventas, 0, 0, 0]
    df.loc[len(df)] = ['total ingresos', ingresos, 0, 0, 0]
    df.loc[len(df)] = ['total gastos', gastos, 0, 0, 0]
    df.loc[len(df)] = ['beneficios', ingresos - gastos, 0, 0, 0]

    df.porcentaje_gastos = round(df.magnitud / gastos * 100, 2)
    df.porcentaje_ventas = round(df.magnitud / ventas * 100, 2)
    df.porcentaje_ingresos = round(df.magnitud / ingresos * 100, 2)
    df.magnitud = round(df.magnitud, 2)

    return Response({fecha.strftime('%Y-%m-%d'): df.to_dict('records')})
"""

@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='CIF', description='NIF de empresa de interés', required=True, type=str),
                           OpenApiParameter(name='fecha', description='fecha de interés', required=False, type=str),
                           OpenApiParameter(name='tipo', description='EOAF, EVA o - por defecto - Analítica', required=False, type=str)])
def analitica(request):
    CIF = request.GET['CIF']
    try:
        empresa = Empresa.objects.get(CIF=CIF)
    except Exception as e:
        return Response({'error': f'Empresa {CIF} no existe. Mensaje: {e}'})

    try:
        tipo = request.GET['tipo']
    except:
        tipo = 'analitica'

    try:
        fecha = request.GET['fecha'].split('-')
        fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
    except:
        # print('Falta el parámetro "fecha" o no se ha enviado con el formato y cifras correctas. Se toma la fecha actual por defecto')
        fecha = datetime.date.today()

    actual = Documento.objects.filter(Q(fecha__lte=fecha), Q(empresa=empresa), Q(origen='BSS') | Q(origen='Modelo200')).latest('fecha')
    print('Latest: ', actual)
    actual = Documento.objects.filter(Q(fecha=actual.fecha), Q(empresa=empresa), Q(origen='BSS') | Q(origen='Modelo200'))
    print('Filter: ', actual)
    if len(actual) == 2:
        actual = actual.get(origen='BSS')
    else:
        actual = actual.first()
    print('Final: ', actual)
    #else:
    #    return Response({'error': f'No se dispone de un documento sobre el que calcular la contabilidad analítica, '
    #                              f'por favor cargar un BSS o un modelo 200'}, status=status.HTTP_404_NOT_FOUND)

    fechaReal = actual.fecha
    doc = actual

    #inv_abreviado = {k: k for k in dictionaries.analitica.keys()}
    #inv_abreviado = {}
    #for key1 in conceptosAbreviado.keys():
    #    for key2 in conceptosAbreviado[key1].keys():
    #        for element in conceptosAbreviado[key1][key2]:
    #            inv_abreviado[str(element)] = key2

    if tipo == 'EOAF':
        df = pd.DataFrame(columns=['campo', 'prioridad', 'concepto', 'denominacion', doc.fecha.year, doc.fecha.year - 1,
                                   'variacion', 'variacion circulante', 'variacion fijo'])

        fijo = ['activo no corriente', "inmovilizado intangible", 'inmovilizado material', "inversiones inmobiliarias",
                "inversiones en empresas del grupo y asociadas a largo plazo", "inversiones financieras a largo plazo",
                "activos por impuesto diferido", "deudores comerciales no corrientes", 'patrimonio neto', "fondos propios",
                "capital", "prima de emision", "reservas", "acciones y participaciones en patrimonio propias",
                "resultados de ejercicios anteriores", "otras aportaciones de socios", "resultado del ejercicio",
                "dividendo a cuenta", "otros instrumentos de patrimonio neto", "ajustes por cambios de valor",
                "subvenciones, donaciones y legados recibidos", 'pasivo no corriente', 'provisiones a largo plazo',
                'deudas a largo plazo', 'deudas con entidades de credito a largo plazo',
                'acreedores por arrendamiento financiero a largo plazo', 'otras deudas a largo plazo',
                "deudas con empresas del grupo y asociadas a largo plazo", "pasivos por impuesto diferido",
                "periodificaciones a largo plazo", "deuda con caracteristicas especiales a largo plazo",
                "acreedores comerciales no corrientes"]

        circulante = ['activo corriente', "activos no corrientes mantenidos para la venta", "existencias",
                      'deudores comerciales y otras cuentas a cobrar', "clientes por ventas y prestaciones de servicios",
                      "accionistas (socios) por desembolsos exigidos", "otros deudores",
                      "inversiones en empresas del grupo y asociadas a corto plazo", "inversiones financieras a corto plazo",
                      "periodificaciones a corto plazo_activo", "efectivo y otros activos liquidos equivalentes",
                      'pasivo corriente', "pasivos vinculados con activos no corrientes mantenidos para la venta",
                      "provisiones a corto plazo", 'deudas a corto plazo',
                      'deudas con entidades de credito a corto plazo', 'acreedores por arrendamiento financiero a corto plazo',
                      'otras deudas a corto plazo', "deudas con empresas del grupo y asociadas a corto plazo",
                      "acreedores comerciales y otras cuentas a pagar", "periodificaciones a corto plazo_pasivo",
                      "deuda con caracteristicas especiales a corto plazo"]

        try:
            for campo, concepto, prioridad, denominacion in finanzas:

                if campo != 'resultados':
                    data = get_row(doc, concepto)
                    #data = [element if element else 0 for element in data]
                    try:
                        data = [data[0], data[2], round(data[0] - data[2], 2)]
                    except Exception as e:
                        return Response({'error': f'Puede que falte información del ejercicio fiscal {fechaReal.year} o {fechaReal.year-1}: {e}'})
                    if concepto in fijo: data += [None, data[2]]
                    elif concepto in circulante: data += [data[2], None]
                    else: data += [None, None]

                    data.insert(0, denominacion)
                    data.insert(0, concepto)
                    data.insert(0, prioridad)
                    data.insert(0, campo)
                    if data[4] != 0 or data[5] != 0:
                        df.loc[len(df)] = data

        except Exception as e:
            return Response({'error': f'{e}'}, status=status.HTTP_500_INTERNAL_SERVER_ERROR)

        df.fillna(0, inplace=True)
        print('Fin de diferencias: ', df)
        df_resumen_LP = pd.DataFrame(columns=['campo', 'prioridad', 'concepto', 'denominacion', 'origen', 'aplicacion'])
        df_resumen_CP = pd.DataFrame(columns=['campo', 'prioridad', 'concepto', 'denominacion', 'origen', 'aplicacion'])

        #iterator = df.iterrows()
        #while(iterator.concepto != 'activo corriente'):
        #    df_resumen_LP.loc[len(df_resumen_LP)] = [iterator.campo, iterator.prioridad, iterator.concepto]
        iterator = df.iterrows()
        for index, row in iterator:
            if row.concepto == 'activo corriente': break
            if row.prioridad == 3:
                print('LP Activo: ', row.concepto, row[doc.fecha.year], row[doc.fecha.year-1], row.variacion)
                if row.variacion < 0:
                    df_resumen_LP.loc[len(df_resumen_LP)] = [row.campo, row.prioridad, row.concepto, row.denominacion, abs(row.variacion), 0]
                else:
                    df_resumen_LP.loc[len(df_resumen_LP)] = [row.campo, row.prioridad, row.concepto, row.denominacion, 0, abs(row.variacion)]
        for index, row in iterator:
            if row.concepto == 'patrimonio neto': break
            if row.prioridad == 3:
                print('CP Activo: ', row.concepto, row[doc.fecha.year], row[doc.fecha.year-1], row.variacion)
                if row.variacion < 0:
                    df_resumen_CP.loc[len(df_resumen_CP)] = [row.campo, row.prioridad, row.concepto, row.denominacion, abs(row.variacion), 0]
                else:
                    df_resumen_CP.loc[len(df_resumen_CP)] = [row.campo, row.prioridad, row.concepto, row.denominacion, 0, abs(row.variacion)]
        for index, row in iterator:
            if row.concepto == 'pasivo corriente': break
            if row.prioridad == 3:
                print('LP Pasivo: ', row.concepto, row[doc.fecha.year], row[doc.fecha.year-1], row.variacion)
                if row.variacion > 0:
                    df_resumen_LP.loc[len(df_resumen_LP)] = [row.campo, row.prioridad, row.concepto, row.denominacion, abs(row.variacion), 0]
                else:
                    df_resumen_LP.loc[len(df_resumen_LP)] = [row.campo, row.prioridad, row.concepto, row.denominacion, 0, abs(row.variacion)]
        for index, row in iterator:
            if row.prioridad == 3:
                print('CP Pasivo: ', row.concepto, row[doc.fecha.year], row[doc.fecha.year-1], row.variacion)
                if row.variacion > 0:
                    df_resumen_CP.loc[len(df_resumen_CP)] = [row.campo, row.prioridad, row.concepto, row.denominacion, abs(row.variacion), 0]
                else:
                    df_resumen_CP.loc[len(df_resumen_CP)] = [row.campo, row.prioridad, row.concepto, row.denominacion, 0, abs(row.variacion)]

        df_resumen_LP.replace(NaN, None, inplace=True)
        df_resumen_CP.replace(NaN, None, inplace=True)
        df_resumen_LP['plazo'] = 'LP'
        df_resumen_CP['plazo'] = 'CP'
        df_resumen = pd.concat([df_resumen_LP, df_resumen_CP], ignore_index=True)
        df_resumen = df_resumen[['plazo', 'campo', 'concepto', 'denominacion', 'origen', 'aplicacion']]
        df_resumen.sort_values(['plazo', 'campo', 'origen', 'aplicacion'], ascending=[True, True, False, False], inplace=True)
        #df_resumen_LP.sort_values(['campo', 'origen', 'aplicacion'], ascending=[True, False, False], inplace=True)
        #df_resumen_CP.sort_values(['campo', 'origen', 'aplicacion'], ascending=[True, False, False], inplace=True)
        #print(df_resumen_LP)
        #print(df_resumen_CP)
        print(df_resumen)
        print('\n', sum(df_resumen.origen), sum(df_resumen.aplicacion))
        return Response({'comparacion': (fechaReal.strftime('%Y-%m-%d'), datetime.date(fechaReal.year-1, 12, 31).strftime('%Y-%m-%d')),
                         'EOAF': df_resumen.to_dict('records')})

    else:
        crudos = Crudos.objects.filter(documento=actual)
        df = pd.DataFrame(columns=['raiz', 'concepto', 'cuenta', 'magnitud', 'porcentaje_gastos', 'porcentaje_ventas', 'porcentaje_ingresos'])
        if tipo == 'EVA':
            df_eva = pd.DataFrame(columns=['raiz', 'concepto', 'cuenta', 'Magnitud', 'Beneficios', 'Beneficios/Ventas'])

        bbdd = Analitica.objects.filter(documento=doc)
        #print('Query de Analítica: ', bbdd)
        gastos = - bbdd.get(cuenta='total gastos').magnitud
        ventas = - bbdd.get(cuenta='total ventas').magnitud
        ingresos = - bbdd.get(cuenta='total ingresos').magnitud
        impuestos = - bbdd.get(cuenta='impuesto de sociedades').magnitud

        mensaje = ''
        remanente = 0
        remanente_parcial = 0
        for key in dictionaries.analitica.keys():
            #print(key)
            try:
                cuenta = bbdd.get(cuenta=key)
                remanente -= cuenta.magnitud
                if tipo == 'EVA':
                    df_eva.loc[len(df_eva)] = [key, cuenta.cuenta, cuenta.cuenta, - cuenta.magnitud, round(remanente, 2), 0]
                else:
                    df.loc[len(df)] = [key, cuenta.cuenta, cuenta.cuenta, - cuenta.magnitud, 0, 0, 0]
                if actual.origen == 'BSS':
                    for x in dictionaries.analitica[key]:
                        if len(str(x)) == 2:
                            elements = [str(x*10+i) for i in range(10)]
                        else:
                            elements = [str(x)]
                        #print(elements)
                        for element in elements:
                            try:
                                cuenta = bbdd.get(cuenta=str(element))
                                remanente_parcial_inicial = remanente_parcial
                                remanente_parcial -= cuenta.magnitud
                                try:
                                    nombre = dictionaries.pgc_analitica[int(cuenta.cuenta)]
                                except:
                                    nombre = 'Desconocido'
                                if tipo == 'EVA':
                                    try:
                                        #print(dictionaries.pgc_analitica[int(cuenta.cuenta)])
                                        if cuenta.magnitud != 0:
                                            df_eva.loc[len(df_eva)] = [key, f'{cuenta.cuenta}: {nombre}', f'{cuenta.cuenta}: {nombre}',
                                                                       - cuenta.magnitud, round(remanente_parcial, 2), 0]
                                            cuentas = [elemento for elemento in crudos if elemento.cuenta[:len(cuenta.cuenta)] == cuenta.cuenta]
                                            #print(cuentas)
                                            for element in cuentas:
                                                #print(f'cuenta {element.cuenta} de {element.magnitud}')
                                                if element.magnitud != 0:
                                                    remanente_parcial_inicial -= element.magnitud
                                                    #print(remanente_parcial_inicial)
                                                    df_eva.loc[len(df_eva)] = [key, f'{cuenta.cuenta}: {nombre}',
                                                                               f'{element.cuenta}: {element.concepto}', - element.magnitud,
                                                                               round(remanente_parcial_inicial, 2), 0]
                                    except:
                                        pass
                                else:
                                    try:
                                        if cuenta.magnitud != 0:
                                            df.loc[len(df)] = [key, f'{cuenta.cuenta}: {nombre}',
                                                               f'{cuenta.cuenta}: {nombre}', - cuenta.magnitud, 0, 0, 0]
                                            cuentas = [elemento for elemento in crudos if elemento.cuenta[:len(cuenta.cuenta)] == cuenta.cuenta]
                                            #print(cuentas)
                                            for element in cuentas:
                                                if element.magnitud != 0:
                                                    df.loc[len(df)] = [key, f'{cuenta.cuenta}: {nombre}',
                                                                       f'{element.cuenta}: {element.concepto}',
                                                                       - element.magnitud, 0, 0, 0]
                                    except:
                                        submensaje = f'Quizás falte {nombre} '
                                        print(submensaje)
                                        mensaje += submensaje
                            except:
                                submensaje = f'Falta cuenta {element} '
                                print(submensaje)
                                mensaje += submensaje
            except:
                submensaje = f'Falta concepto {key}'
                print(submensaje)
                mensaje += submensaje

        if tipo == 'EVA':
            df_eva.loc[len(df_eva)] = ['Beneficios', 'Beneficios', 'Beneficios', cuenta.magnitud, round(remanente_parcial, 2), 0]
            df_eva['Beneficios/Ventas'] = round(df_eva['Beneficios'] / abs(ventas) * 100, 2)
            df_eva['Magnitud'] = round(df_eva.Magnitud, 2)
            return Response({fecha.strftime('%Y-%m-%d'): df_eva.to_dict('records')})
        else:
            df.loc[len(df)] = ['Beneficios', 'Beneficios', 'Beneficios', ingresos + gastos + impuestos, 0, 0, 0]
            df.loc[len(df)] = ['Ventas', 'Ventas', 'Ventas', ventas, 0, 0, 0]
            df.loc[len(df)] = ['Ingresos', 'Ingresos', 'Ingresos',ingresos, 0, 0, 0]
            df.loc[len(df)] = ['Gastos', 'Gastos', 'Gastos', gastos, 0, 0, 0]
            df.loc[len(df)] = ['EBITDA', 'EBITDA', 'EBITDA', Ratio.objects.get(concepto='EBITDA', documento=doc).magnitud, 0, 0, 0]
            df.loc[len(df)] = ['BAII', 'BAII', 'BAII', Ratio.objects.get(concepto='BAII', documento=doc).magnitud, 0, 0, 0]
            df.loc[len(df)] = ['BAI', 'BAI', 'BAI', Ratio.objects.get(concepto='BAI', documento=doc).magnitud, 0, 0, 0]
            df.loc[len(df)] = ['Beneficios', 'Beneficios', 'Beneficios', ingresos + gastos + impuestos, 0, 0, 0]

            df.porcentaje_gastos = round(df.magnitud / gastos * 100, 2)
            df.porcentaje_ventas = round(df.magnitud / ventas * 100, 2)
            df.porcentaje_ingresos = round(df.magnitud / ingresos * 100, 2)
            df.magnitud = round(df.magnitud, 2)
            #print(df)
            return Response({actual.fecha.strftime('%Y-%m-%d'): df.to_dict('records'), 'warning': mensaje})


@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='fecha', description='cobertura a esta fecha', required=False, type=str),
                           OpenApiParameter(name='CIF', description='NIF de empresa de interés', required=True, type=str)])
def cobertura(request):
    CIF = request.GET['CIF']
    try:
        empresa = Empresa.objects.get(CIF=CIF)
    except Exception as e:
        return Response({'error': f'Empresa {CIF} no existe. Mensaje: {e}'})
    try:
        fecha = request.GET['fecha'].split('-')
        fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
    except:
        # print('Falta el parámetro "fecha" o no se ha enviado con el formato y cifras correctas. Se toma la fecha actual por defecto')
        fecha = datetime.date.today()

    try:
        actual = Documento.objects.filter(fecha__lte=fecha, origen='BSS', empresa__CIF=CIF).latest('fecha')
    except Exception as e:
        return Response({'error': f'Falta un balance de sumas y saldos para el cálculo de la cobertura: {e}'}, status=status.HTTP_404_NOT_FOUND)

    if not actual:
        return Response({'error': 'No se dispone de información todavía, por favor cargar un balance de sumas y saldos'},
                        status=status.HTTP_404_NOT_FOUND)

    fecha1 = actual.fecha
    fecha2 = datetime.date(actual.fecha.year - 1, 12, 31)
    fecha3 = datetime.date(actual.fecha.year - 2, 12, 31)
    doc = actual
    # elementos del pool para cada año con contrato
    pool1 = Pool.objects.filter(documento__empresa=empresa, documento__fecha__range=[fecha2, fecha1]).exclude(contrato__isnull=True)
    pool2 = Pool.objects.filter(documento__empresa=empresa, documento__fecha__range=[fecha3, fecha2]).exclude(contrato__isnull=True)
    pool3 = Pool.objects.filter(documento__empresa=empresa, documento__fecha__range=[datetime.date(fecha3.year, 1, 1), fecha3]).exclude(contrato__isnull=True)
    # contratos únicos de largo plazo en el set de registros del pool de cada año
    contratos1 = list(set([pool.contrato for pool in pool1 if pool.contrato.plazo]))
    servicio1 = servicio(contratos1, fecha1.year)

    contratos2 = list(set([pool.contrato for pool in pool2 if pool.contrato.plazo]))
    servicio2 = servicio(contratos2, fecha2.year)
    if servicio1 and servicio2:
        evolucion1 = round((servicio1/servicio2 - 1) * 100, 2)
    elif servicio1 and not servicio2:
        evolucion1 = 1000000000000
    elif not servicio1 and servicio2:
        evolucion1 = 0
    else: evolucion1 = None

    contratos3 = list(set([pool.contrato for pool in pool3 if pool.contrato.plazo]))
    servicio3 = servicio(contratos3, fecha3.year)

    if servicio2 and servicio3:
        evolucion2 = round((servicio2 / servicio3 - 1) * 100, 2)
    elif servicio2 and not servicio3:
        evolucion2 = 1000000000000
    elif not servicio1 and servicio3:
        evolucion2 = 0
    else:
        evolucion2 = None

    EBITDA1 = Ratio.objects.get(documento=actual, concepto='EBITDA').magnitud
    if servicio1:
        cobertura1 = round(EBITDA1/servicio1 * 100, 2)
    else:
        cobertura1 = 1000000000000

    EBITDA2 = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha2, concepto='EBITDA')

    if EBITDA2:
        if len(EBITDA2) > 1:
            EBITDA2 = EBITDA2.get(documento__origen='Modelo200').magnitud
        else: EBITDA2 = EBITDA2.first().magnitud
        tendencia1 = round((EBITDA1/EBITDA2 - 1) * 100, 2)
    else:
        EBITDA2 = None
        tendencia1 = None
    if servicio2 and EBITDA2:
        cobertura2 = round(EBITDA2/servicio2 * 100, 2)
    elif servicio2 and not EBITDA2:
        cobertura2 = 0
    elif not servicio2 and EBITDA2:
        cobertura2 = 1000000000000
    else:
        cobertura2 = None

    EBITDA3 = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha3, concepto='EBITDA')

    if EBITDA3:
        if len(EBITDA3) > 1:
            EBITDA3 = EBITDA3.get(documento__origen='Modelo200').magnitud
        else: EBITDA3 = EBITDA3.first().magnitud
        if EBITDA2:
            tendencia2 = round((EBITDA2/EBITDA3 - 1) * 100, 2)
        else: tendencia2 = None
    else:
        EBITDA3 = None
        tendencia2 = None

    if servicio3 and EBITDA3:
        cobertura3 = round(EBITDA3/servicio3, 2)
    elif servicio3 and not EBITDA3:
        cobertura3 = 0
    elif not servicio3 and EBITDA3:
        cobertura3 = 1000000000000
    else:
        cobertura3 = None

    if cobertura1 and cobertura2:
        porcentaje1 = round((cobertura1/cobertura2 - 1) * 100, 2)
    elif cobertura1 and not cobertura2:
        porcentaje1 = 1000000000000
    elif not cobertura1 and cobertura2:
        porcentaje1 = 0
    else:
        porcentaje1 = None

    if cobertura2 and cobertura3:
        porcentaje2 = round((cobertura2/cobertura3 - 1) * 100, 2)
    elif cobertura2 and not cobertura3:
        porcentaje2 = 1000000000000
    elif not cobertura2 and cobertura3:
        porcentaje2 = 0
    else:
        porcentaje2 = None

    df = pd.DataFrame(columns=['concepto', fecha1.year, f'tendencia {fecha2.year}-{fecha1.year}', fecha2.year,
                               f'tendencia {fecha3.year}-{fecha2.year}', fecha3.year])
    df.loc[0] = ['EBITDA', EBITDA1, tendencia1, EBITDA2, tendencia2, EBITDA3]
    df.loc[1] = ['Servicio', servicio1, evolucion1, servicio2, evolucion2, servicio3]
    df.loc[2] = ['cobertura', cobertura1, porcentaje1, cobertura2, porcentaje2, cobertura3]

    return Response({f'servicio {fecha1}': df.to_dict('records')})

# utilizar tras la review_pool para identificar los productos que requieren la edición de un contrato
@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='CIF', description='NIF de empresa de interés', required=True, type=str)])
def get_cuentas_sin_contrato(request):

    CIF = request.GET['CIF']
    #por algún motivo desconocido el .data de la serialización no devuelve los ids, de manera que se genera una
    #lista independiente con los ids
    cuentas = Pool.objects.filter(documento__empresa__CIF=CIF, contrato__isnull=True) #, documento__fecha_lte=fecha

    return Response(PoolSerializer(cuentas, many=True).data)

# sería para utilizar siempre que haya una lectura de un BSS y de manera previa a la edición de contratos, no sería
# necesario si los contratos se editan justo después de una carga de un nuevo BSS, pero habrá que estructurarlo con el
# frontend, porque si se pueden cargar varios BSS sin editar contratos, entonces hay que implementar este proceso
@api_view(['POST'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='id_pool', description='Asociar contrato con cuenta del Pool', required=True, type=int)])
def review_pool(request):

    try:
        pool = Pool.objects.get(id=request.query_params.get('id_pool'))
    except:
        return Response({'error': 'wrong primary key, pool object does not exist'}, status=status.HTTP_400_BAD_REQUEST)

    try:
        productos = Pool.objects.filter(documento__empresa__CIF=pool.documento.empresa.CIF,
                                        contrato__isnull=False).filter(contrato__inicio__lte=pool.documento.fecha,
                                                                     contrato__vencimiento__gte=pool.documento.fecha)
        check = True

    except:
        check = False

    if check:
        for producto in productos:
            if producto.cuenta == pool.cuenta and producto.concepto == pool.concepto:
                pool.update(contrato=producto.contrato)
                break
                #actualizaciones.append(producto.pk)

    return Response({'pool': PoolSerializer(pool).data})

"""
@api_view(['GET', 'POST'])
@permission_classes((IsAuthenticated,))
def enter_contract(request):
"""

def get_row_crudo(doc, cuenta):

    try:
        first = Crudos.objects.get(documento=doc, cuenta=cuenta)
    except Exception as e:
        raise Exception(f'No se han encontrado la cuenta {cuenta}')

    fecha2 = datetime.date(doc.fecha.year - 1, 12, 31)
    fecha3 = datetime.date(fecha2.year - 1, 12, 31)

    actual = float(first.magnitud)

    # data = [actual]
    # if campo == 'resultados' and concepto != "variacion de existencias de productos terminados y en curso de fabricacion":
    #    actual = actual * (datetime.date(doc.fecha.year, 12, 31) - datetime.date(doc.fecha.year - 1, 12, 31)).days / \
    #             (doc.fecha - datetime.date(doc.fecha.year-1, 12, 31)).days
    # print(f'Actual: {actual}')
    try:
        intermedio = Crudos.objects.get(documento__empresa=doc.empresa, documento__fecha=fecha2, cuenta=cuenta).magnitud
            # print(f'BSS - Intermedio de {concepto}: {intermedio}')
    except:
        intermedio = None

    if intermedio:
        #print(intermedio)
        intermedio = float(intermedio)

    try:
        inicial = Crudos.objects.get(documento__empresa=doc.empresa, documento__fecha=fecha3, cuenta=cuenta).magnitud
        # print(f'Modelo200 - Inicial de {concepto}: {inicial}')
    except:
        inicial = None

    if inicial:
        #print(inicial)
        inicial = float(inicial)

    # print(actual, intermedio, inicial)

    if not intermedio or intermedio == 0:
        crecimiento1 = None
    else:
        crecimiento1 = round((actual / intermedio - 1) * 100, 2)

    if not inicial or inicial == 0:
        crecimiento2 = None
    else:
        try:
            crecimiento2 = round((intermedio / inicial - 1) * 100, 2)
        except:
            raise Exception(f'Es necesario cargar la contabilidad de {fecha2}')

    # print([concepto, actual, crecimiento1, intermedio, crecimiento2, inicial])

    data = [round(actual, 2)]
    if intermedio:
        data.extend([crecimiento1, round(intermedio, 2)])
    elif intermedio == 0:
        data.extend([None, 0])
    else:
        data.extend([None, None])
    if inicial:
        data.extend([crecimiento2, round(inicial, 2)])
    elif inicial == 0:
        data.extend([None, 0])
    else:
        data.extend([None, None])

    return data


def get_row(doc, concepto):

    try:
        first = Contabilidad.objects.get(documento=doc, concepto=concepto)
    except:
        raise Exception(f'No se han encontrado el concepto {concepto}')

    fecha2 = datetime.date(doc.fecha.year-1, 12, 31)
    fecha3 = datetime.date(fecha2.year-1, 12, 31)

    actual = float(first.magnitud)

    #data = [actual]
    #if campo == 'resultados' and concepto != "variacion de existencias de productos terminados y en curso de fabricacion":
    #    actual = actual * (datetime.date(doc.fecha.year, 12, 31) - datetime.date(doc.fecha.year - 1, 12, 31)).days / \
    #             (doc.fecha - datetime.date(doc.fecha.year-1, 12, 31)).days
    #print(f'Actual: {actual}')
    try:
        intermedio = Contabilidad.objects.get(documento__empresa=doc.empresa, documento__fecha=fecha2,
                                              concepto=concepto, documento__origen='Modelo200').magnitud
        #print(f'Modelo200 - Intermedio de {concepto}: {intermedio}')
    except:
        try:
            intermedio = Contabilidad.objects.get(documento__empresa=doc.empresa, documento__fecha=fecha2,
                                                  concepto=concepto, documento__origen='BSS').magnitud
            #print(f'BSS - Intermedio de {concepto}: {intermedio}')
        except:
            intermedio = None

    if intermedio: intermedio = float(intermedio)

    try:
        inicial = Contabilidad.objects.get(documento__empresa=doc.empresa, documento__fecha=fecha3,
                                           concepto=concepto, documento__origen='Modelo200').magnitud
        #print(f'Modelo200 - Inicial de {concepto}: {inicial}')
    except:
        try:
            inicial = Contabilidad.objects.get(documento__empresa=doc.empresa, documento__fecha=fecha3,
                                               concepto=concepto, documento__origen='BSS').magnitud
            #print(f'BSS - Inicial de {concepto}: {inicial}')
        except:
            inicial = None

    if inicial: inicial = float(inicial)

    #print(actual, intermedio, inicial)

    if not intermedio or intermedio == 0: crecimiento1 = None
    else: crecimiento1 = round((actual / intermedio - 1)*100, 2)

    if not inicial or inicial == 0: crecimiento2 = None
    else:
        try:
            crecimiento2 = round((intermedio / inicial - 1)*100, 2)
        except:
            raise Exception(f'Es necesario cargar la contabilidad de {fecha2}')

    #print([concepto, actual, crecimiento1, intermedio, crecimiento2, inicial])

    data = [round(actual, 2)]
    if intermedio: data.extend([crecimiento1, round(intermedio, 2)])
    elif intermedio == 0: data.extend([None, 0])
    else: data.extend([None, None])
    if inicial: data.extend([crecimiento2, round(inicial, 2)])
    elif inicial == 0: data.extend([None, 0])
    else: data.extend([None, None])

    return data

def activo_pasivo_ventas(**kwargs):

    campo = kwargs['campo']
    if campo == 'activo':
        denominadores = [kwargs['activo1'], kwargs['activo2'], kwargs['activo3']]
    elif campo == 'pasivo':
        denominadores = [kwargs['pasivo1'], kwargs['pasivo2'], kwargs['pasivo3']]
    else:
        denominadores = [kwargs['ventas1'], kwargs['ventas2'], kwargs['ventas3']]

    return denominadores

def masa(numerador, denominador):

   if denominador != 0:
        masa = round(numerador / denominador * 100, 2)
   else:
        masa = 'inf'

   return masa

def get_row_contabilidad(**kwargs):

    presente    =   kwargs['presente']
    intermedio  =   kwargs['intermedio']
    inicial     =   kwargs['inicial']
    concepto    =   kwargs['concepto']
    denominadores = kwargs['denominadores']

    presente = presente.get(concepto=concepto).magnitud

    data = [presente, masa(presente, denominadores[0])]

    if intermedio:
        intermedio = intermedio.get(concepto=concepto).magnitud
        if intermedio != 0:
            data.extend([round((presente/intermedio - 1) * 100, 2)])
        else:
            data.extend(['inf'])
        data.extend([intermedio, masa(intermedio, denominadores[1])])
    else:
        data.extend([None, None, None])

    if inicial:
        if intermedio or intermedio == 0:
            inicial = inicial.get(concepto=concepto).magnitud
            if inicial != 0:
                data.extend([round((intermedio / inicial - 1) * 100, 2)])
            else:
                data.extend(['inf'])
            data.extend([inicial, masa(inicial, denominadores[2])])
        else:
            raise Exception('Es necesario cargar datos para el año intermedio')
    else:
        data.extend([None, None, None])

    return data

# comparación de Balance de situación y PyG de los tres último periodos
@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
### POR AHORA SOLO FUNCIONA SI TENEMOS UN HISTORIAL MÍNIMO DE 3 PERIODOS Y COGE LA FECHA
### MÁS CERCANA EN EL PASADO
@extend_schema(parameters=[OpenApiParameter(name='fecha', description='balance y PyG en esa fecha', required=False, type=str),
                           OpenApiParameter(name='CIF', description='NIF de empresa de interés', required=True, type=str)])
def balancePyG(request):

    try:
        CIF = request.GET['CIF']
        empresa = Empresa.objects.get(CIF=CIF)
    except:
        return Response({'error': f'Falta el CIF o la empresa no existe'}, status=status.HTTP_404_NOT_FOUND)
    try:
        fecha = request.GET['fecha'].split('-')
        fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
    except:
        #print('Falta el parámetro "fecha" o no se ha enviado con el formato y cifras correctas. Se toma la fecha actual por defecto')
        fecha = datetime.date.today()

    try:
        actual = Documento.objects.filter(fecha__lte=fecha, origen='BSS', empresa__CIF=CIF).latest('fecha')
    except:
        try:
            actual = Documento.objects.filter(fecha__lte=fecha, origen='Modelo200', empresa__CIF=CIF).latest('fecha')
        except Exception as e:
            return Response({'error': f'{e}'}, status=status.HTTP_404_NOT_FOUND)

        # order_by('-documento__fecha').first()
    # print('Fecha más actual: ', actual.documento.fecha)
    if not actual:
        return Response({'error': 'No se dispone de información todavía, por favor cargar un balance de sumas y saldos o un Modelo200'},
                        status=status.HTTP_404_NOT_FOUND)

    fechaReal = actual.fecha
    doc = actual
    presente = Contabilidad.objects.filter(documento=doc)
    activo1 = presente.get(concepto='total activo').magnitud
    pasivo1 = presente.get(documento=doc, concepto='total patrimonio neto y pasivo').magnitud
    ventas1 = Analitica.objects.get(documento=doc, cuenta='ventas').magnitud

    fecha2 = datetime.date(fechaReal.year-1, 12, 31)
    fecha3 = datetime.date(fecha2.year-1, 12, 31)

    intermedio = Contabilidad.objects.filter(documento__empresa=empresa, documento__fecha=fecha2, documento__origen='Modelo200')
    #print(f'Modelo200 - Intermedio de {concepto}: {intermedio}')
    if not intermedio:
        intermedio = Contabilidad.objects.filter(documento__empresa=empresa, documento__fecha=fecha2, documento__origen='BSS')
        #print(f'BSS - Intermedio de {concepto}: {intermedio}')
        if not intermedio:
            intermedio = None
    if intermedio:
        activo2 = intermedio.get(concepto='total activo')
        pasivo2 = intermedio.get(concepto='total patrimonio neto y pasivo').magnitud
        ventas2 = Analitica.objects.get(documento=activo2.documento, cuenta='ventas').magnitud
        activo2 = activo2.magnitud
    else:
        activo2 = pasivo2 = ventas2 = None

    inicial = Contabilidad.objects.filter(documento__empresa=empresa, documento__fecha=fecha3, documento__origen='Modelo200')
    #print(f'Modelo200 - Inicial de {concepto}: {inicial}')
    if not inicial:
        inicial = Contabilidad.objects.filter(documento__empresa=empresa, documento__fecha=fecha3, documento__origen='BSS')
        #print(f'BSS - Inicial de {concepto}: {inicial}')
        if not inicial:
            inicial = None
    if inicial:
        activo3 = inicial.get(concepto='total activo')
        pasivo3 = inicial.get(concepto='total patrimonio neto y pasivo').magnitud
        ventas3 = Analitica.objects.get(documento=activo3.documento, cuenta='ventas').magnitud
        activo3 = activo3.magnitud
    else:
        activo3 = pasivo3 = ventas3 = None

    crudos = Crudos.objects.filter(documento=actual)
    #capital = sum([cuenta.magnitud for cuenta in crudos if int(cuenta.cuenta[:3]) in dictionaries.conceptosAbreviado['PASIVO']['capital']])
    capital = Contabilidad.objects.get(documento=actual, concepto='capital')

    if capital.magnitud > 0: sign = 1
    else: sign = -1

    df = pd.DataFrame(columns=['campo', 'prioridad', 'concepto', 'denominacion', fechaReal.strftime("%m/%d/%Y"), 'masa1',
                               'evolucion1', str(fecha2.year), 'masa2', 'evolucion2', str(fecha3.year), 'masa3'])

    diccionario = copy.deepcopy(conceptosAbreviado)
    diccionario_denominacion = copy.deepcopy(dictionaries.pgc_analitica)
    for k, v in dictionaries.polivalentes.items():
        cuentas = [elemento for elemento in crudos if elemento.cuenta[:len(str(k))] == str(k)]
        agregado = sum([cantidad.magnitud for cantidad in cuentas])
        if agregado > 0:
            diccionario['ACTIVO'][v[0]] += [k]
            diccionario_denominacion[k] = v[0]
        else:
            diccionario['PASIVO'][v[1]] += [k]
            diccionario_denominacion[k] = v[1]

    try:
        for campo, concepto, prioridad, denominacion in finanzas:

            kwargs = {'campo': campo, 'activo1': activo1, 'activo2': activo2, 'activo3': activo3, 'pasivo1': pasivo1,
                      'pasivo2': pasivo2, 'pasivo3': pasivo3, 'ventas1': ventas1, 'ventas2': ventas2, 'ventas3': ventas3}

            denominadores = activo_pasivo_ventas(**kwargs)

            kwargs = {'concepto': concepto, 'presente': presente, 'intermedio': intermedio, 'inicial': inicial,
                      'denominadores': denominadores}
            #print(campo, concepto)
            if concepto == 'importe neto de la cifra de negocios':
                df.loc[len(df)] = [campo, 2, 'Ingresos y Gastos de Explotación', 'Ingresos y Gastos de Explotación',
                                   None, None, None, None, None, None, None, None]
            #print(campo, concepto)
            data = get_row_contabilidad(**kwargs)

            data.insert(0, denominacion)
            data.insert(0, concepto)
            data.insert(0, prioridad)
            data.insert(0, campo)
            #print(data)
            df.loc[len(df)] = data

            if doc.origen == 'BSS':
                try:
                    for cuenta in diccionario[campo.upper()][concepto]:
                        if len(str(cuenta)) == 2:
                            list_cuentas = range(10*cuenta, 10*cuenta+10)
                        else:
                            list_cuentas = [cuenta]
                        #print('Cuentas: ', list_cuentas)
                        for x in list_cuentas:
                            cuentas = [elemento for elemento in crudos if elemento.cuenta[:len(str(x))] == str(x)]
                            agregado = sum([cantidad.magnitud for cantidad in cuentas])
                            #print(f"{concepto} {x} {dictionaries.pgc_analitica[x]} {agregado}")
                            if agregado and agregado != 0:
                                if campo != 'activo':
                                    agregado = - agregado
                                
                                try: nombre = diccionario_denominacion[x]
                                except:
                                    try:
                                        nombre = diccionario_denominacion[int(str(x)[:(len(str(x))-1)])]
                                    except: nombre = 'Desconocido'
                                #print(f'{concepto} {x}')
                                df.loc[len(df)] = [campo, prioridad + 1, cuenta, f'{x}: {nombre}', round(agregado, 2),
                                                   round(agregado / denominadores[0] * 100, 2), None, None, None, None, None, None]
                            #print(cuentas)
                            for element in cuentas:
                                #print(f'{element.cuenta} {element.magnitud}')
                                ajustado = element.magnitud
                                if ajustado != 0:
                                    if campo != 'activo':
                                        ajustado = - ajustado
                                    df.loc[len(df)] = [campo, prioridad + 2, element.cuenta, f'{element.cuenta}: {element.concepto}',
                                                       round(ajustado, 2), round(ajustado/denominadores[0]*100, 2),
                                                       None, None, None, None, None, None]
                                        #print(get_row_crudo(doc, element.cuenta))
                                    #df.loc[len(df)] = [campo, prioridad+2, element.cuenta, f'{element.cuenta}:{element.concepto}'] +\
                                    #                   get_row_crudo(doc, element.cuenta)
                except:
                    pass

    except Exception as e:
        return Response({'error': f'Loop de BalancePyG: {e}'}, status=status.HTTP_500_INTERNAL_SERVER_ERROR)

    return Response({fechaReal.strftime('%Y-%m-%d'): df.to_dict('records')})

"""
def get_row_single(doc, campo, concepto):
    try:
        first = Contabilidad.objects.get(documento=doc, concepto=concepto)
    except:
        return Response({'error': f'no se ha encontrado {concepto}'})
    #print('First: ', first)
    actual = float(first.magnitud)

    ### super simplificación que hay que corregir, porque no aplica a todos los conceptos del campo 'resultados'
    #if campo == 'resultados':
    #    actual = actual * 12 / doc.fecha.month

    return round(actual, 2)

@api_view(['POST'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
### POR AHORA SOLO FUNCIONA SI TENEMOS UN HISTORIAL MÍNIMO DE 3 PERIODOS Y COGE LA FECHA
### MÁS CERCANA EN EL PASADO
@extend_schema(parameters=[OpenApiParameter(name='fecha', description='balance y PyG en esa fecha', required=False, type=str)])
def balancePyGSingle(request):

    CIF = request.POST['CIF']
    try:
        fecha = request.GET['fecha'].split('-')
        fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
    except:
        #print('Falta el parámetro "fecha" o no se ha enviado con el formato y cifras correctas. Se toma la fecha actual por defecto')
        fecha = datetime.date.today()

    try:
        actual = Documento.objects.filter(fecha__lte=fecha, origen='BSS', empresa__CIF=CIF).latest('fecha')
    except:
        try:
            actual = Documento.objects.filter(fecha__lte=fecha, origen='Modelo200', empresa__CIF=CIF).latest('fecha')
        except Exception as e:
            return Response({'error': f'{e}'})

        #order_by('-documento__fecha').first()
    #print('Fecha más actual: ', actual.documento.fecha)
    if not actual:
        return Response({'error': 'No se dispone de información todavía, por favor cargar un balance de sumas y saldos o un Modelo200'})

    fechaReal = actual.fecha
    doc = actual

    df = pd.DataFrame(columns=['campo', 'prioridad', 'concepto', 'denominacion', 'magnitud'])

    for campo, concepto, prioridad, denominacion in finanzas:

        if concepto == 'margen de contribucion':
            ventas = get_row_single(doc, campo, 'ventas sin aprovisionamientos')
            aprovisionamientos = get_row_single(doc, campo, 'aprovisionamientos')
            #print('Ventas: ', ventas)
            #print('Aprovisionamientos: ', aprovisionamientos)
            data = ventas + aprovisionamientos

        elif concepto == 'ebitda':
            #print(df)
            margen = float(df[df['concepto'] == 'margen de contribucion'].values[0].tolist()[4])
            fijos = get_row_single(doc, campo, 'costes fijos')
            #print('Margen: ', margen)
            #print('Fijos: ', fijos)
            data = margen + fijos
        else:
            data = get_row_single(doc, campo, concepto)

        data = [campo, prioridad, concepto, denominacion, data]

        df.loc[len(df)] = data

    #print(df)

    return Response(df.to_dict('records'))
"""

# cálculo de ratios para una empresa y fecha determinada a partir de los datos en la BBDD
@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='fecha', description='Ratios en esta fecha', required=False, type=str),
                           OpenApiParameter(name='CIF', description='NIF de empresa de interés', required=True, type=str)])
### API OBSOLETA, AHORA SE OPERA CON ratiosVerticales()
def ratios(request):
    desired_width = 2000
    pd.set_option('display.width', desired_width)
    CIF = request.GET['CIF']
    #doc = request.FILE['file']

    try:
        Empresa.objects.get(CIF=CIF)
    except:
        return Response({'error': f'No existe la empresa {CIF}'})

    try:
        fecha = request.GET['fecha'].split('-')
        fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
    except:
        #print('Falta el parámetro "fecha" o no se ha enviado con el formato y cifras correctas. Se toma la fecha actual por defecto')
        fecha = datetime.date.today()

    try:
        actual_BSS = Contabilidad.objects.filter(documento__fecha__lte=fecha, concepto='total activo', documento__origen='BSS',
                                                 documento__empresa__CIF=CIF).latest('documento__fecha')
    except:
        actual_BSS = None

    try:
        actual_Modelo200 = Contabilidad.objects.filter(documento__fecha__lte=fecha, concepto='total activo', documento__origen='Modelo200',
                                                 documento__empresa__CIF=CIF).latest('documento__fecha')
    except:
        actual_Modelo200 = None

    if actual_Modelo200:
        actual = actual_Modelo200
        tipoDoc = 'Modelo200'
    elif actual_BSS:
        actual = actual_BSS
        tipoDoc = 'BSS'
    else:
        return Response({'error': f'No existe documentación para la empresa {CIF}'})

        #order_by('-documento__fecha').first()
    #print('Fecha más actual: ', actual.documento.fecha)

    fecha = actual.documento.fecha

    kpis = pd.DataFrame(columns=['concepto', 'magnitud'])

    ### FALTA INTRODUCIR ORIGEN='MODELO200' EN LA QUERY Y TRY EXCEPT PARA CONSULTAR ORIGEN='BSS' SI NO HAY MODELO200

    ratio = Contabilidad.objects.get(concepto='activo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud - \
            Contabilidad.objects.get(concepto='pasivo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['fondo maniobra', ratio]

    ratio = abs(
        Contabilidad.objects.get(concepto='activo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
        Contabilidad.objects.get(concepto='pasivo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud)

    kpis.loc[len(kpis)] = ['liquidez', ratio]

    ratio = abs(
        (Contabilidad.objects.get(concepto='activo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud -
         Contabilidad.objects.get(concepto='existencias', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud) / \
         Contabilidad.objects.get(concepto='pasivo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud)

    kpis.loc[len(kpis)] = ['prueba acida', ratio]

    ratio = abs(Contabilidad.objects.get(concepto='efectivo y otros activos liquidos equivalentes', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
                Contabilidad.objects.get(concepto='pasivo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud)

    kpis.loc[len(kpis)] = ['disponibilidad', ratio]

    ratio = kpis.loc[kpis.concepto == 'fondo maniobra', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='total activo', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['fondo maniobra/activo total', ratio]

    ratio = (Contabilidad.objects.get(concepto='pasivo no corriente', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='pasivo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud) / \
             Contabilidad.objects.get(concepto='total patrimonio neto y pasivo', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['endeudamiento', ratio]

    ratio = Contabilidad.objects.get(concepto='pasivo no corriente', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
            (Contabilidad.objects.get(concepto='pasivo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='pasivo no corriente', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud)

    kpis.loc[len(kpis)] = ['calidad deuda', ratio]

    ratio = abs(Contabilidad.objects.get(concepto='gastos financieros', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
               (Contabilidad.objects.get(concepto='deudas con entidades de credito a largo plazo', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
                Contabilidad.objects.get(concepto='deudas con entidades de credito a corto plazo', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud))

    kpis.loc[len(kpis)] = ['coste de deuda', ratio]

    #'coste medio del pasivo' no se puede calcular todavía porque faltan dividendos; ¿no son los dividendos a cuenta?
    #ratio = (Contabilidad.objects.get(concepto='gastos financieros', 'magnitud'] + \
    #         Contabilidad.objects.get(concepto='dividendos', 'magnitud']) /\
    #         Contabilidad.objects.get(concepto='total pasivo', 'magnitud']
    #Contabilidad.loc[len(kpis)] = ['coste medio del pasivo', ratio]
    
    ratio = Contabilidad.objects.get(concepto='resultado de la cuenta de perdidas y ganancias', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['BN', ratio]

    ratio = Contabilidad.objects.get(concepto='resultado antes de impuestos', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['BAI', ratio]

    ratio = Contabilidad.objects.get(concepto='resultado de explotacion', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['BAII', ratio]

    EBITDA = Contabilidad.objects.get(concepto='resultado de explotacion', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud - \
             Contabilidad.objects.get(concepto='amortizacion del inmovilizado', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud - \
             Contabilidad.objects.get(concepto='excesos de provisiones', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    CASHFLOW = Contabilidad.objects.get(concepto='resultado de la cuenta de perdidas y ganancias', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud - \
               Contabilidad.objects.get(concepto='amortizacion del inmovilizado', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['cashflow', CASHFLOW]

    kpis.loc[len(kpis)] = ['EBITDA', EBITDA]

    ratio = Contabilidad.objects.get(concepto='deudas a largo plazo', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['DFB L/P', ratio]

    ratio = Contabilidad.objects.get(concepto='deudas a corto plazo', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['DFB C/P', ratio]

    ratio = abs(kpis.loc[kpis.concepto == 'DFB L/P', 'magnitud'].values[0] +
                kpis.loc[kpis.concepto == 'DFB C/P', 'magnitud'].values[0])

    kpis.loc[len(kpis)] = ['DFB', ratio]

    ratio = abs(kpis.loc[kpis.concepto == 'DFB', 'magnitud'].values[0] - \
                Contabilidad.objects.get(concepto='efectivo y otros activos liquidos equivalentes', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud -
                Contabilidad.objects.get(concepto='inversiones financieras a corto plazo', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud)

    kpis.loc[len(kpis)] = ['DFN', ratio]

    ratio = kpis.loc[kpis.concepto == 'DFB', 'magnitud'].values[0] / EBITDA
    kpis.loc[len(kpis)] = ['DFB/EBITDA', ratio]

    ratio = kpis.loc[kpis.concepto == 'DFN', 'magnitud'].values[0] / EBITDA
    kpis.loc[len(kpis)] = ['DFN/EBITDA', ratio]

    ratio = kpis.loc[kpis.concepto == 'DFB', 'magnitud'].values[0] / CASHFLOW
    kpis.loc[len(kpis)] = ['DFB/cashflow', ratio]

    ratio = kpis.loc[kpis.concepto == 'DFN', 'magnitud'].values[0] / CASHFLOW
    kpis.loc[len(kpis)] = ['DFN/cashflow', ratio]

    ####################################################################################################
    ###                                                                                              ###
    ### NO ESTÁN LOS DE SERVICIO DE DEUDA. COMO DEPENDEN DEL CONTRATO, SE HA DE VER CÓMO SE CALCULAN ###
    ###                                                                                              ###
    ####################################################################################################

    ratio = Contabilidad.objects.get(concepto='resultado de explotacion', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
            Contabilidad.objects.get(concepto='total activo', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['BAII/activo', ratio]

    ratio = Contabilidad.objects.get(concepto='resultado antes de impuestos', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
            Contabilidad.objects.get(concepto='total activo', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['BAI/activo', ratio]

    ratio = Contabilidad.objects.get(concepto='resultado del ejercicio', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
            Contabilidad.objects.get(concepto='total activo', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['BN/activo', ratio]

    ratio = Contabilidad.objects.get(concepto='resultado de la cuenta de perdidas y ganancias', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
            (Contabilidad.objects.get(concepto='patrimonio neto', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud -
             Contabilidad.objects.get(concepto='resultado del ejercicio', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud)
    kpis.loc[len(kpis)] = ['BN/(FFPP-beneficio año)', ratio]

    ratio = Contabilidad.objects.get(concepto='total activo', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud * \
            Contabilidad.objects.get(concepto='resultado antes de impuestos', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
            Contabilidad.objects.get(concepto='patrimonio neto', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
            Contabilidad.objects.get(concepto='resultado de explotacion', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['apalancamiento financiero', ratio]

    ratio = abs((Contabilidad.objects.get(concepto='gastos de personal', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='otros gastos de explotacion', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='imputacion de subvenciones de inmovilizado no financiero y otras', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='excesos de provisiones', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='deterioro y resultado por enajenaciones del inmovilizado', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='diferencia negativa de combinaciones de negocio', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='gastos financieros', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='variacion de valor razonable en instrumentos financieros', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='diferencias de cambio', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='deterioro y resultado por enajenaciones de instrumentos financieros', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud) /
            ((Contabilidad.objects.get(concepto='importe neto de la cifra de negocios', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='variacion de existencias de productos terminados y en curso de fabricacion', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='trabajos realizados por la empresa para su activo', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='aprovisionamientos', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
            Contabilidad.objects.get(concepto='otros ingresos de explotacion', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud) /
            Contabilidad.objects.get(concepto='importe neto de la cifra de negocios', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud))

    kpis.loc[len(kpis)] = ['punto muerto', ratio]

    ratio = abs((Contabilidad.objects.get(concepto='gastos de personal', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                          documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='otros gastos de explotacion', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                      documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='excesos de provisiones', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                      documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='deterioro y resultado por enajenaciones del inmovilizado', documento__fecha=fecha,
                                      documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='diferencia negativa de combinaciones de negocio', documento__fecha=fecha,
                                      documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='gastos financieros', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                      documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='variacion de valor razonable en instrumentos financieros', documento__fecha=fecha,
                                      documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='diferencias de cambio', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                      documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='deterioro y resultado por enajenaciones de instrumentos financieros', documento__fecha=fecha,
                                      documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud)/12.0/\
             (Contabilidad.objects.get(concepto='importe neto de la cifra de negocios', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                       documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='variacion de existencias de productos terminados y en curso de fabricacion',
                                      documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='trabajos realizados por la empresa para su activo', documento__fecha=fecha,
                                      documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='aprovisionamientos', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                      documento__origen=tipoDoc).magnitud +
             Contabilidad.objects.get(concepto='otros ingresos de explotacion', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                      documento__origen=tipoDoc).magnitud) /
             Contabilidad.objects.get(concepto='importe neto de la cifra de negocios', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                      documento__origen=tipoDoc).magnitud)

    kpis.loc[len(kpis)] = ['punto muerto mensual', ratio]

    ratio = Contabilidad.objects.get(concepto='importe neto de la cifra de negocios', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud + \
            Contabilidad.objects.get(concepto='variacion de existencias de productos terminados y en curso de fabricacion',
                                     documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud + \
            Contabilidad.objects.get(concepto='trabajos realizados por la empresa para su activo', documento__fecha=fecha,
                                     documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud + \
            Contabilidad.objects.get(concepto='aprovisionamientos', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud + \
            Contabilidad.objects.get(concepto='otros ingresos de explotacion', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud

    kpis.loc[len(kpis)] = ['margen de contribucion', ratio]

    ratio = Contabilidad.objects.get(concepto='importe neto de la cifra de negocios', documento__fecha=fecha,
                                     documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['ventas', ratio]

    ratio = kpis.loc[kpis.concepto == 'BN', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BN/ventas', ratio]

    ratio = Contabilidad.objects.get(concepto='capital', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud / \
            kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['capital/ventas', ratio]

    ratio = Contabilidad.objects.get(concepto='patrimonio neto', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud / \
            kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['FFPP/ventas', ratio]

    # no se pueden calcular los ratios dependientes de deuda bancaria

    ratio = kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'DFB', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['ventas/DFB', ratio]

    ratio = kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'DFB C/P', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['ventas/DFB C/P', ratio]

    ratio = kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'DFB L/P', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['ventas/DFB L/P', ratio]

    ratio = kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='total activo', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['ventas/activo', ratio]

    ratio = kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='activo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['ventas/activo corriente', ratio]

    ratio = kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='activo no corriente', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['ventas/activo no corriente', ratio]

    ratio = kpis.loc[kpis.concepto == 'BN', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BN/ventas', ratio]

    ratio = kpis.loc[kpis.concepto == 'BN', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='capital', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BN/capital', ratio]

    ratio =kpis.loc[kpis.concepto == 'BN', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='patrimonio neto', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BN/FFPP', ratio]

    ratio = kpis.loc[kpis.concepto == 'BN', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'DFB', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BN/DFB', ratio]

    ratio = kpis.loc[kpis.concepto == 'BN', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'DFB C/P', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BN/DFB C/P', ratio]

    ratio = kpis.loc[kpis.concepto == 'BN', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'DFB L/P', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BN/DFB L/P', ratio]

    ratio = kpis.loc[kpis.concepto == 'BN', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='total activo', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BN/activo', ratio]

    ratio = kpis.loc[kpis.concepto == 'BN', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='activo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BN/activo corriente', ratio]

    ratio = kpis.loc[kpis.concepto == 'BN', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='activo no corriente', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BN/activo no corriente', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAII', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BAII/ventas', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAII', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='capital', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BAII/capital', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAII', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='patrimonio neto', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BAII/FFPP', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAII', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'DFB', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BAII/DFB', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAII', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'DFB C/P', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BAII/DFB C/P', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAII', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'DFB L/P', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BAII/DFB L/P', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAII', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='total activo', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BAII/activo', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAII', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='activo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BAII/activo corriente', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAII', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='activo no corriente', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BAII/activo no corriente', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAI', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BAI/ventas', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAI', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='capital', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BAI/capital', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAI', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='patrimonio neto', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BAI/FFPP', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAI', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'DFB', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BAI/DFB', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAI', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'DFB C/P', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BAI/DFB C/P', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAI', 'magnitud'].values[0] / \
            kpis.loc[kpis.concepto == 'DFB L/P', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['BAI/DFB L/P', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAI', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='total activo', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BAI/activo', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAI', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='activo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BAI/activo corriente', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAI', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='activo no corriente', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['BAI/activo no corriente', ratio]

    ratio = EBITDA / kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['EBITDA/ventas', ratio]

    ratio = EBITDA / \
            Contabilidad.objects.get(concepto='capital', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['EBITDA/capital', ratio]

    ratio = EBITDA / \
            Contabilidad.objects.get(concepto='patrimonio neto', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['EBITDA/FFPP', ratio]

    ratio = EBITDA / kpis.loc[kpis.concepto == 'DFB', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['EBITDA/DFB', ratio]

    ratio = EBITDA / kpis.loc[kpis.concepto == 'DFB C/P', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['EBITDA/DFB C/P', ratio]

    ratio = EBITDA / kpis.loc[kpis.concepto == 'DFB L/P', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['EBITDA/DFB L/P', ratio]

    ratio = EBITDA / \
            Contabilidad.objects.get(concepto='total activo', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['EBITDA/activo', ratio]

    ratio = EBITDA / \
            Contabilidad.objects.get(concepto='activo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['EBITDA/activo corriente', ratio]

    ratio = EBITDA / \
            Contabilidad.objects.get(concepto='activo no corriente', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['EBITDA/activo no corriente', ratio]

    ratio = CASHFLOW / kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['cashflow/ventas', ratio]

    ratio = CASHFLOW / \
            Contabilidad.objects.get(concepto='capital', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['cashflow/capital', ratio]

    ratio = CASHFLOW / \
            Contabilidad.objects.get(concepto='patrimonio neto', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['cashflow/FFPP', ratio]

    ratio = CASHFLOW / kpis.loc[kpis.concepto == 'DFB', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['cashflow/DFB', ratio]

    ratio = CASHFLOW / kpis.loc[kpis.concepto == 'DFB C/P', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['cashflow/DFB C/P', ratio]

    ratio = CASHFLOW / kpis.loc[kpis.concepto == 'DFB L/P', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['cashflow/DFB L/P', ratio]

    ratio = CASHFLOW / \
            Contabilidad.objects.get(concepto='total activo', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['cashflow/activo', ratio]

    ratio = CASHFLOW / \
            Contabilidad.objects.get(concepto='activo corriente', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['cashflow/activo corriente', ratio]

    ratio = CASHFLOW / \
            Contabilidad.objects.get(concepto='activo no corriente', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['cashflow/activo no corriente', ratio]

    ratio = Contabilidad.objects.get(concepto='resultado del ejercicio procedente de operaciones continuadas',
                                     documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
            Contabilidad.objects.get(concepto='patrimonio neto', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['ROE', ratio]

    ratio = kpis.loc[kpis.concepto == 'BAII', 'magnitud'].values[0] / \
            Contabilidad.objects.get(concepto='total activo', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['ROA/ROI', ratio]

    ratio = Contabilidad.objects.get(concepto='aprovisionamientos', documento__fecha=fecha, documento__empresa__CIF=CIF,
                                     documento__origen=tipoDoc).magnitud * 1.21 / 365
    kpis.loc[len(kpis)] = ['compras medias diarias', ratio]

    ratio = 365.0 * Contabilidad.objects.get(concepto='acreedores comerciales y otras cuentas a pagar', documento__fecha=fecha,
                                             documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
            Contabilidad.objects.get(concepto='aprovisionamientos', documento__fecha=fecha, documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud
    kpis.loc[len(kpis)] = ['PM Proveedores', ratio]

    ratio = kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0] * 1.21 / 365
    kpis.loc[len(kpis)] = ['ventas medias diarias', ratio]

    ratio = 365.0 * Contabilidad.objects.get(concepto='deudas a largo plazo', documento__fecha=fecha,
                                             documento__empresa__CIF=CIF, documento__origen=tipoDoc).magnitud / \
            kpis.loc[kpis.concepto == 'ventas', 'magnitud'].values[0]
    kpis.loc[len(kpis)] = ['PM Cobro', ratio]

    kpis.loc[len(kpis)] = ['ventas/capital',
                           1.0 / kpis.loc[kpis.concepto == 'capital/ventas', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['ventas/FFPP',
                           1.0 / kpis.loc[kpis.concepto == 'FFPP/ventas', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB/ventas',
                           1.0 / kpis.loc[kpis.concepto == 'ventas/DFB', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB C/P/ventas',
                           1.0 / kpis.loc[kpis.concepto == 'ventas/DFB C/P', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB L/P/ventas',
                           1.0 / kpis.loc[kpis.concepto == 'ventas/DFB L/P', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo/venta',
                           1.0 / kpis.loc[kpis.concepto == 'ventas/activo', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo corriente/ventas',
                           1.0 / kpis.loc[kpis.concepto == 'ventas/activo corriente', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo no corriente/ventas',
                           1.0 / kpis.loc[kpis.concepto == 'ventas/activo no corriente', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['ventas/BN', 1.0 / kpis.loc[kpis.concepto == 'BN/ventas', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['capital/BN',
                           1.0 / kpis.loc[kpis.concepto == 'BN/capital', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['FFPP/BN', 1.0 / kpis.loc[kpis.concepto == 'BN/FFPP', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB/BN', 1.0 / kpis.loc[kpis.concepto == 'BN/DFB', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB C/P/BN',
                           1.0 / kpis.loc[kpis.concepto == 'BN/DFB C/P', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB L/P/BN',
                           1.0 / kpis.loc[kpis.concepto == 'BN/DFB L/P', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo/BN', 1.0 / kpis.loc[kpis.concepto == 'BN/activo', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo corriente/BN',
                           1.0 / kpis.loc[kpis.concepto == 'BN/activo corriente', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo no corriente/BN',
                           1.0 / kpis.loc[kpis.concepto == 'BN/activo no corriente', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['ventas/BAII',
                           1.0 / kpis.loc[kpis.concepto == 'BAII/ventas', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['capital/BAII',
                           1.0 / kpis.loc[kpis.concepto == 'BAII/capital', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['FFPP/BAII', 1.0 / kpis.loc[kpis.concepto == 'BAII/FFPP', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB/BAII', 1.0 / kpis.loc[kpis.concepto == 'BAII/DFB', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB C/P/BAII',
                           1.0 / kpis.loc[kpis.concepto == 'BAII/DFB', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB L/P/BAII',
                           1.0 / kpis.loc[kpis.concepto == 'BAII/DFB L/P', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo/BAII',
                           1.0 / kpis.loc[kpis.concepto == 'BAII/activo', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo corriente/BAII',
                           1.0 / kpis.loc[kpis.concepto == 'BAII/activo corriente', 'magnitud'].values[0]]
    kpis.loc[len(kpis)] = ['activo no corriente/BAII',
                           1.0 / kpis.loc[kpis.concepto == 'BAII/activo no corriente', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['ventas/BAI',
                           1.0 / kpis.loc[kpis.concepto == 'BAI/ventas', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['capital/BAI',
                           1.0 / kpis.loc[kpis.concepto == 'BAI/capital', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['FFPP/BAI',
                           1.0 / kpis.loc[kpis.concepto == 'BAI/FFPP', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB/BAI',
                           1.0 / kpis.loc[kpis.concepto == 'BAI/DFB', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB C/P/BAI',
                           1.0 / kpis.loc[kpis.concepto == 'BAI/DFB C/P', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB L/P/BAI',
                           1.0 / kpis.loc[kpis.concepto == 'BAI/DFB L/P', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo/BAI',
                           1.0 / kpis.loc[kpis.concepto == 'BAI/activo', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo corriente/BAI',
                           1.0 / kpis.loc[kpis.concepto == 'BAI/activo corriente', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo no corriente/BAI',
                           1.0 / kpis.loc[kpis.concepto == 'BAI/activo no corriente', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['ventas/EBITDA',
                           1.0 / kpis.loc[kpis.concepto == 'EBITDA/ventas', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['capital/EBITDA',
                           1.0 / kpis.loc[kpis.concepto == 'EBITDA/capital', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['FFPP/EBITDA',
                           1.0 / kpis.loc[kpis.concepto == 'EBITDA/FFPP', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB C/P/EBITDA',
                           1.0 / kpis.loc[kpis.concepto == 'EBITDA/DFB C/P', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB L/P/EBITDA',
                           1.0 / kpis.loc[kpis.concepto == 'EBITDA/DFB L/P', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo/EBITDA',
                           1.0 / kpis.loc[kpis.concepto == 'EBITDA/activo', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo corriente/EBITDA',
                           1.0 / kpis.loc[kpis.concepto == 'EBITDA/activo corriente', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo no corriente/EBITDA',
                           1.0 / kpis.loc[kpis.concepto == 'EBITDA/activo no corriente', 'magnitud'].values[0]]
    kpis.loc[len(kpis)] = ['ventas/cashflow',
                           1.0 / kpis.loc[kpis.concepto == 'cashflow/ventas', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['capital/cashflow',
                           1.0 / kpis.loc[kpis.concepto == 'cashflow/capital', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['FFPP/cashflow',
                           1.0 / kpis.loc[kpis.concepto == 'cashflow/FFPP', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['cashflow/DFN',
                           1.0 / kpis.loc[kpis.concepto == 'DFN/cashflow', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB C/P/cashflow',
                           1.0 / kpis.loc[kpis.concepto == 'cashflow/DFB C/P', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['DFB L/P/cashflow',
                           1.0 / kpis.loc[kpis.concepto == 'cashflow/DFB L/P', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo/cashflow',
                           1.0 / kpis.loc[kpis.concepto == 'cashflow/activo', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo corriente/cashflow', 1.0 /
                           kpis.loc[kpis.concepto == 'cashflow/activo corriente', 'magnitud'].values[0]]

    kpis.loc[len(kpis)] = ['activo no corriente/cashflow', 1.0 /
                           kpis.loc[kpis.concepto == 'cashflow/activo no corriente', 'magnitud'].values[0]]

    kpis.magnitud = kpis.magnitud.map(lambda x: round(x, 4))

    return Response(kpis)
