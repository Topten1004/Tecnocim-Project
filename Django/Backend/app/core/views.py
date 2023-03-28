import datetime
import math
from operator import itemgetter

import django
import numpy as np
import openpyxl
import pandas as pd
from django.http import JsonResponse
from drf_spectacular.utils import extend_schema, extend_schema_view, OpenApiParameter, OpenApiTypes
from rest_framework import viewsets, request, status
from rest_framework.authentication import TokenAuthentication, SessionAuthentication
from rest_framework.decorators import api_view, permission_classes, authentication_classes
from rest_framework.permissions import IsAuthenticated
from rest_framework.serializers import CharField
from rest_framework.utils import json

import core.views
from core.models import *
from core import serializers, dictionaries
from rest_framework.response import Response
from rest_framework.reverse import reverse
from core.serializers import ContratoSerializer, PoolSerializer, CirbeSerializer, EmpresaSerializer
from equivalencias.models import Personal, Real, Entidad, Producto, Moneda
from core.models import numeroMeses

from core.models import Crudos


class AnaliticaViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.AnaliticaSerializer

    queryset = Analitica.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]

    filterset_fields = {'documento__empresa__CIF': ['exact'],
                        'documento__fecha': ['lte', 'exact', 'gte'],
                        'documento__origen': ['exact'],
                        'cuenta': ['exact']}


class CirbeViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.CirbeSerializer

    queryset = Cirbe.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]

    filterset_fields = {'documento__empresa__CIF': ['exact'],
                        'documento__fecha': ['lte', 'exact', 'gte'],
                        'entidad': ['exact'],
                        'operacion': ['exact'],
                        'contrato': ['isnull']}


class CirbeContratoViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.CirbeSerializer

    queryset = Cirbe.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]

    # Response devuelve LP o CP de contrato único, y también los productos sin clasificación, para poder comprobar que
    # el 100% está clasificado ... o no
    @extend_schema(parameters=[OpenApiParameter(name='CIF', description='filtrar por compañía', required=True, type=str),
                               OpenApiParameter(name='inicio', description='fecha mínima', required=True, type=str),
                               OpenApiParameter(name='vencimiento', description='fecha máxima', required=True, type=str),
                               OpenApiParameter(name='entidad', description='Id de entidad financiera', required=False, type=str)])
    def list(self, request, *args, **kwargs):

        CIF = self.request.query_params.get('CIF')
        try:
            empresa = Empresa.objects.get(CIF=CIF)
        except Exception as e:
            return Response({'error': f'Empresa con CIF {CIF} no existe o fallo bbdd: {e}'}, status=status.HTTP_400_BAD_REQUEST)

        try:
            entidad = Entidad.objects.get(id=self.request.query_params['entidad'])
        except Exception as e:
            entidad = None

        inicio = self.request.query_params.get('inicio')
        inicio = inicio.split('-')
        try:
            inicio = datetime.date(int(inicio[0]), int(inicio[1]), int(inicio[2]))
        except:
            return Response({'error': f'Formato incorrecto de fecha: {inicio}. Formato correcto yyyy-mm-dd'})

        vencimiento = self.request.query_params.get('vencimiento')
        vencimiento = vencimiento.split('-')
        try:
            vencimiento = datetime.date(int(vencimiento[0]), int(vencimiento[1]), int(vencimiento[2]))
        except:
            return Response({'error': f'Formato incorrecto de fecha: {vencimiento}. Formato correcto yyyy-mm-dd'})

        if vencimiento < inicio:
            return Response({'error': f'Fechas incorrectas "Vencimiento" {vencimiento} tiene que estar en el futuro respecto de "Inicio" {inicio}'}, status=status.HTTP_400_BAD_REQUEST)

        if entidad:
            cirbes = Cirbe.objects.filter(documento__empresa=empresa, contrato__isnull=True,
                                          documento__fecha__range=[inicio, vencimiento], entidad=entidad).\
                exclude(producto__in=['multilinea', 'ventas', 'compras', 'excepcion', 'sin']).values_list('operacion')
        else:
            cirbes = Cirbe.objects.filter(documento__empresa=empresa, contrato__isnull=True,
                                          documento__fecha__range=[inicio, vencimiento]).\
                exclude(producto__in=['multilinea', 'ventas', 'compras', 'excepcion', 'sin']).values_list('operacion')

        cirbes = [operacion[0] for operacion in cirbes]
        cirbes = list(set(cirbes))
        #for element in cirbes:
            #print(element)

        final = []
        for cirbe in cirbes:
            final.append(Cirbe.objects.filter(operacion=cirbe).latest('documento__fecha'))
        #for element in final:
            #print(element)

        return Response(CirbeSerializer(final, many=True).data)


class ContabilidadViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.ContabilidadSerializer

    queryset = Contabilidad.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]

    filterset_fields = {'documento__empresa__CIF': ['exact'],
                        'documento__fecha': ['lte', 'exact', 'gte'],
                        'documento__origen': ['exact'],
                        'concepto': ['exact']}


class ContratoViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.ContratoSerializer

    queryset = Contrato.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]

    filterset_fields = {'inicio': ['lte'],
                        'vencimiento': ['gte']}

    """
    @extend_schema(parameters=[OpenApiParameter(name='id_pool', description='Asociar contrato con cuenta del Pool', required=True, type=int),
                               OpenApiParameter(name='id_cirbe', description='Asociar contrato con un cirbe', required=False, type=int),
                               OpenApiParameter(name='id_cuenta', description='Asociar con otra cuenta del pool', required=False, type=str)])
    """
    @staticmethod
    def prepare_parameters(request):

        try:
            id_pool = request.POST['id_pool']
        except:
            return {'error': 'id de elemento del Pool obligatorio'}, None, None, None

        try:
            pool = Pool.objects.get(id=id_pool)
            try:
                contrato = Contrato.objects.get(id=pool.contrato.id)
                id_contrato = contrato.id
            except:
                id_contrato = False
        except Pool.DoesNotExist:
            return {'error': 'non-valid id for Pool; Pool register does not exist'}, None, None, None

        try:
            id_cirbe = request.POST['id_cirbe']
            if id_cirbe:
                try:
                    cirbe = Cirbe.objects.get(id=id_cirbe)
                except:
                    return {'error': 'non-valid id for Cirbe; Cirbe register does not exist'}, None, None, None
            else: cirbe = False
        except:
            cirbe = False

        try:
            id_cuenta = request.POST['id_cuenta']
            #print('id_cuenta: ', id_cuenta)
            try:
                cuenta = Pool.objects.get(id=id_cuenta)
                #print('Cuenta: ', cuenta)
                if (cuenta.cuenta[0] == '1' and pool.cuenta[0] == '1') or (pool.cuenta[0] == '5' and cuenta.cuenta[0] == '5'):
                    return {'error': 'no se pueden asociar cuentas de largo a cuentas de largo o cuentas de corto a cuentas de corto'}, None, None, None
            except Pool.DoesNotExist:
                return {'error': 'non-valid account for Pool; Pool register does not exist'}, None, None, None
        except:
            cuenta = False
            print('No se ha incluido cuenta complementaria')

        try:
            if pool.cuenta[1] == 1 and request.POST['producto'] not in productos_largo:
                return {'error': f'El producto {request.POST["producto"]} no se corresponde con cuentas de largo plazo {productos_largo}'}, None, None, None
        except Exception as e:
            return {'error': f'Probablemente falta el "producto": {e}'}, None, None, None

        try:
            if pool.cuenta[1] == 5 and request.POST['producto'] in [element for element in productos_largo if element != 'prestamo']:
                return {'error': f'El producto {request.POST["producto"]} no se corresponde con cuentas de corto plazo'}, None, None, None
        except Exception as e:
            return {'error': f'Probablemente falta el "producto": {e}'}, None, None, None

        request = request.POST.dict()

        try:
            inicio = request['inicio'].split('-')
            try:
                inicio = datetime.date(int(inicio[0]), int(inicio[1]), int(inicio[2]))
                request['inicio'] = inicio
            except:
                return {'error': 'Formato de fecha no se corresponde con yyyy-mm-dd'}, None, None, None
        except:
            return {'error': 'Se requiere una fecha de inicio'}, None, None, None

        try:
            vencimiento = request['vencimiento'].split('-')
            try:
                vencimiento = datetime.date(int(vencimiento[0]), int(vencimiento[1]), int(vencimiento[2]))
                request['vencimiento'] = vencimiento
            except:
                return {'error': 'Formato de fecha no se corresponde con yyyy-mm-dd'}, None, None, None
        except:
                return {'error': 'Se requiere una fecha de vencimiento'}, None, None, None

        check = (inicio <= pool.documento.fecha <= vencimiento)
        if not check:
            return {'error': f'fechas inconsistentes de inicio, fecha y vencimiento: {inicio}, {pool.documento.fecha}, {vencimiento}'}, None, None, None
        if numeroMeses(vencimiento, inicio) < 1:
            return {'error': f'el plazo mínimo para un préstamo es de 1 mes: {vencimiento} {inicio} no cumplen la condición'}, None, None, None

        if id_contrato:
            request['id'] = id_contrato
        request.pop('id_pool')
        try:
            request.pop('id_cuenta')
        except:
            pass
        try:
            request.pop('id_cirbe')
        except:
            pass

        #print(f'Request: {request} \tParams: {self.request.query_params.dict()}')


        #generar entidades 'entidad', 'producto' y 'moneda' a partir de los parámetros del request
        try:
            request['entidad'] = Entidad.objects.get(pk=request['entidad'])
        except:
            return {'error': 'Identificador no válido, consultar base de datos'}, None, None, None

        try:
            request['producto'] = Producto.objects.get(tipo=request['producto'])
        except:
            return {'error': f'Tipo no válido de producto {request.POST["producto"]}. Notificar a desarrollo si el tipo és válido'}, None, None, None

        try:
            moneda = request['moneda']
            try:
                request['moneda'] = Moneda.objects.get(tipo=moneda)
            except:
                return {'error': 'Código de divisa no válido. Notificar a desarrollo si el código és válido'}, None, None, None
        except:
            pass

        return request, pool, cuenta, cirbe

    def create(self, request, *args, **kwargs):

        request, pool, cuenta, cirbe = self.prepare_parameters(request)
        if not pool:
            return Response({'error': request}, status=status.HTTP_400_BAD_REQUEST)
        contrato = Contrato(**request)
        contrato.save()
        inicio = contrato.inicio
        fin = datetime.date(contrato.vencimiento.year, 12, 31)

        try:
            id_contrato = request['id']
        except:
            id_contrato = None
        id_empresa = pool.documento.empresa
        if cuenta:
            contrato_original = cuenta.contrato

        if id_contrato:
            #desvincular los pools del contrato (ya sean pools de la cuenta principal o la complementaria)
            desvincular = Pool.objects.filter(contrato=id_contrato)
            #print('Desvincular Pool: ', desvincular)
            for element in desvincular:
                element.contrato = None
                element.save()
        vincular = Pool.objects.filter(cuenta=pool.cuenta, documento__empresa=id_empresa,
                                       documento__fecha__range=[inicio, fin], contrato__isnull=True)
        len_vincular = len(vincular)
        docs_activos = Documento.objects.filter(empresa=id_empresa, origen='BSS', fecha__range=[inicio, fin])
        len_docs = len(docs_activos)
        missing = []
        faltantes = {}
        if len_vincular != len_docs:
            for element in docs_activos:
                try:
                    Pool.objects.get(documento=element, cuenta=pool.cuenta)
                except:
                    missing.append(element.fecha)
            faltantes = {pool.cuenta: missing}

        #vincular pools de cuenta principal al nuevo contrato
        print('Vincular Pool: ', vincular)
        for element in vincular:
            print('Vincular cuenta: ', element)
            element.contrato = contrato
            element.save()

        #si hay cuenta complementaria, repetir el proceso con los pools de la complementaria
        print('Cuenta: ', cuenta)
        if cuenta:
            desvincular = Pool.objects.filter(contrato=cuenta.contrato, cuenta=cuenta.cuenta)
            print('Desvincular Cuenta: ', desvincular)
            for element in desvincular:
                element.contrato = None
                element.save()
            vincular = Pool.objects.filter(cuenta=cuenta.cuenta, documento__empresa=id_empresa,
                                           documento__fecha__range=[inicio, fin], contrato__isnull=True)
            len_vincular = len(vincular)
            if len_vincular != len_docs:
                missing = []
                for element in docs_activos:
                    try:
                        Pool.objects.get(documento=element, cuenta=cuenta.cuenta)
                    except:
                        missing.append(element.fecha)
                faltantes[cuenta.cuenta] = missing

            print('Vincular Cuenta: ', vincular)
            for element in vincular:
                element.contrato = contrato
                element.save()
            #si el contrato ha quedado húerfano de cuentas, borrarlo
            print(f'contrato original {contrato_original}\ncontrato referencia {cuenta.contrato}\ncontrato nuevo {contrato}')
            if Pool.objects.filter(contrato=contrato_original).count() == 0: contrato_original.delete()

        print('Faltantes: ', faltantes)

        if id_contrato:
            desvincular = Cirbe.objects.filter(contrato=id_contrato)
            #print('Desvincular Cirbe: ', desvincular)
            for element in desvincular:
                element.contrato = None
                element.save()
        if cirbe:
            print('Cirbe: ', cirbe.id)
            #cirbe.update(contrato=contrato)
            vincular = Cirbe.objects.filter(entidad=cirbe.entidad, operacion=cirbe.operacion,
                                            documento__fecha__range=[inicio, contrato.vencimiento], contrato__isnull=True)
            print('Vincular Cirbe: ', vincular)
            for element in vincular:
                #pool = Pool.objects.filter(documento__empresa=id_empresa, contrato=element.contrato, documento__fecha=element.documento.fecha)
                #if pool:
                #    dispuesto = pool.aggregate(Sum('dispuesto'))
                #    if dispuesto != element.dispuesto:
                #        # un print pero habría de ser un return Response, pero por ahora solo visualizar en lugar de interrumpir
                #        print(f'El dispuesto en el Cirbe en {element.documento.fecha} es diferente al del BSS: {element.dispuesto} {dispuesto}')
                element.contrato = contrato
                element.save()

        return Response({'contratos': ContratoSerializer(contrato).data, 'cuentas faltantes': faltantes})

    # se asume que el update es el estandar y que se concatena automáticamente en un create_or_update
    """
    def update(self, request, *args, **kwargs):

        print(f'Request: {json.dumps(request.POST.dict())} \tParams: {self.request.query_params.dict()}')
        request = request.POST.dict()
        print(request)
        # generar entidades 'entidad', 'producto' y 'moneda' a partir de los parámetros del request
        request['entidad'] = Entidad.objects.get(codigo=request['entidad'])
        request['producto'] = Producto.objects.get(tipo=request['producto'])
        request['moneda'] = Moneda.objects.get(tipo=request['moneda'])
        try:
            contrato = Contrato.objects.get(id=request['id'])
        except:
            return Response({'error': 'non-valid id; contract does not exist'}, status.HTTP_404_NOT_FOUND)

        contrato = Contrato(**request)
        contrato.save()

        return Response(ContratoSerializer(contrato).data)
    """

    # no queremos que el list devuelva todos los contratos, solo los de la empresa de interés; fecha formato yyyy-mm-dd
    @extend_schema(parameters=[OpenApiParameter(name='CIF', description='Filtrar por Empresa', required=True, type=str),
                               OpenApiParameter(name='fecha', description='Filtrar por fecha', required=False, type=str)])
    def list(self, request, *args, **kwargs):

        # CIF = request.POST['CIF']
        # fecha = request.POST['fecha']

        try:
            CIF = request.GET['CIF']
            Empresa.objects.get(CIF=CIF)
        except:
            return Response({'error': 'Falta el CIF o no existe una empresa con ese CIF'}, status.HTTP_400_BAD_REQUEST)

        try:
            fecha = request.GET['fecha'].split('-')
            fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
        except:
            print('No se ha detectado fecha o formato incorrecto, se procede a tomar la fecha actual')
            fecha = datetime.date.today()
            actual = Documento.objects.filter(fecha__lte=fecha, origen='BSS', empresa__CIF=CIF).latest('fecha')
            fecha = actual.fecha
            #return Response({'error': 'Falta la fecha o formato incorrecto'}, status.HTTP_400_BAD_REQUEST)
        #recuperamos los contratos de las cuentas del pool de la empresa
        pool = Pool.objects.filter(documento__empresa__CIF=CIF).exclude(contrato__isnull=True).values_list('contrato')
        #pasamos la query a una lista con los ids de los contratos
        pool = [x[0] for x in pool if x[0]]
        #eliminamos valores repetidos
        pool = set(pool)
        #recuperamos los contratos activos con esos ids
        contratos = Contrato.objects.filter(pk__in=pool, inicio__lte=fecha, vencimiento__gte=fecha).exclude(producto='excepcion').order_by('producto', 'entidad')
        #print('Contratos: ', contratos)

        return Response(ContratoSerializer(contratos, many=True).data)


class ContratoChoicesViewSet(viewsets.ViewSet):

    def list(self):
        serializer = serializers.ContratoChoicesSerializer
        return Response(serializer.data)


class CrudosViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.CrudosSerializer

    queryset = Crudos.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]

    filterset_fields = {'documento__empresa__CIF': ['exact'],
                        'cuenta': ['exact']}


class DocumentoViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.DocumentoSerializer

    queryset = Documento.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]

    filterset_fields = {'empresa__CIF': ['exact'],
                        'fecha': ['lte', 'gte'],
                        'origen': ['exact']}


class EmpresaViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.EmpresaSerializer
    queryset = Empresa.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]

    filterset_fields = {'CIF': ['exact']}

class ExtraccionesViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.ExtraccionesSerializer
    queryset = Extracciones.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]


class Extracciones_ErroresViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.Extracciones_ErroresSerializer
    queryset = Extracciones_Errores.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]

"""
class OperacionViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.OperacionSerializer

    queryset = Operacion.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]
"""


# reducir un grupo de elementos del pool a sus contratos únicos
def get_set_contracts(pool):
    final_pool = []
    for element in pool:
        if pool.filter(contrato=element.contrato).count() == 2:
            # if element.cuenta[:3] == '170' or element.cuenta[:3] == '171':
            if element.plazo:
                element.dispuesto = pool.filter(contrato=element.contrato).aggregate(dispuesto_sum=Sum('dispuesto'))[
                    'dispuesto_sum']
                final_pool.append(element)
        else:
            final_pool.append(element)

    return final_pool


class PoolViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.PoolSerializer

    queryset = Pool.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]
    #filterset_fields = {'documento__empresa__CIF': ['exact'],
    #                    'contrato': ['isnull']}

    # @extend_schema(parameters=[OpenApiParameter(name='present', description='Ver el más actual', required=True, type=bool),
    #                           OpenApiParameter(name='CIF', description='Filtrar por empresa', required=True, type=str),
    #                           OpenApiParameter(name='contrato', description='True=ver elementos pool que tienen contrato. '
    #                                                                         'No cuenta si present=True)', required=False, type=bool)])
    @extend_schema(
        parameters=[OpenApiParameter(name='fecha', description='pool en esa fecha', required=False, type=str),
                    OpenApiParameter(name='CIF', description='empresa', required=True, type=str),
                    OpenApiParameter(name='fecha_en_cuenta', description='si se quiere info de una fecha en concreto',
                                     required=False, type=str),
                    OpenApiParameter(name='fk_en_cuenta', description='si se quiere info de todo el pool o los registros con o sin contrato',
                                     required=False, type=str),
                    OpenApiParameter(name='fk', description='True=ver elementos pool relacionados con foreign key, '
                                                            'False=ver elementos que no tienen foreign key,'
                                                            'all=ver todos elementos)', required=False, type=bool)])
    def list(self, request, *args, **kwargs):

        # print(self.request.query_params.get('CIF'), '\tpresent: ', self.request.query_params.get('present'), '\tContrato: ', self.request.query_params.get('contrato'))

        # CIF = self.request.query_params.get('CIF')
        # print(CIF, '\t', contrato)
        # present = (self.request.query_params.get('present') == 'True')
        try:
            CIF = request.GET['CIF']
        except:
            return Response({'error': 'Falta el parametro CIF'}, status.HTTP_400_BAD_REQUEST)

        try:
            fecha_en_cuenta = (request.GET['fecha_en_cuenta'] == 'True')
        except:
            #print('Falta el post parameter "fecha_en_cuenta" o el valor proporcionado no es un booleano. Se ignorará la fecha')
            fecha_en_cuenta = False
            # return Response({'error': 'Falta el post parameter "fecha_en_cuenta" o el valor proporcionado no es un booleano'},
            #                status.HTTP_400_BAD_REQUEST)
        try:
            fk_en_cuenta = (request.GET['fk_en_cuenta'] == 'True')
        except:
            #print('Falta el post parameter "fk_en_cuenta", se devolverá todo el Pool')
            fk_en_cuenta = False
            # return Response({'warning': 'Falta el post parameter "fk_en_cuenta", se devolverá el Pool actual'},
            #                status.HTTP_400_BAD_REQUEST)
        if fecha_en_cuenta:
            try:
                fecha = request.GET['fecha']
                fecha = fecha.split('-')
                fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
                if fecha > datetime.date.today():
                    #print('No son válidas fechas en el futuro, se tomará la fecha actual como referencia.')
                    fecha = datetime.date.today()
            except:
                #print('No se ha recibido fecha o fecha inválida. Se toma el día de hoy como fecha')
                fecha = datetime.date.today()
        else:
            fecha = datetime.date.today()
        try:
            # fecha__gte=datetime.date(fecha.year-1, fecha.month, fecha.day), para filtrar que hay docs de menos de un año
            # de antiguedad; no parece funcionar, hay que analizar
            actual = Documento.objects.filter(fecha__lte=fecha, origen='BSS', empresa__CIF=CIF).latest('fecha')
        except:
            return Response({'error': 'No se han importado balances de sumas y saldos de hace menos de un año. Actualizar documentación'})
        #print('Fecha más actual: ', actual.fecha)
        fecha = actual.fecha

        if fk_en_cuenta:
            try:
                fk = request.GET['fk']
            except:
                #print('No se ha recibido "fk". Se devolverán todas las cuentas')
                fk = 'all'
        else:
            fk = 'all'

        try:
            pool = Pool.objects.filter(documento=actual)
        except:
            #print('No existe una empresa con ese CIF')
            return Response({'empty query': f'no existe una empresa con el CIF {CIF}'}, status.HTTP_400_BAD_REQUEST)

        if not fecha_en_cuenta:
            if fk_en_cuenta:
                if fk == 'True':
                    pool = pool.exclude(contrato__isnull=True)
                elif fk == 'False':
                    pool = pool.filter(contrato__isnull=True, contrato__producto='excepcion')
                else:
                    pass
            else:
                pass

        else:
            if fk_en_cuenta:
                pool = pool.filter(contrato__isnull=True, documento=actual)
            else:
                pool = pool.exclude(contrato__isnull=True).exclude(contrato__producto='excepcion').\
                            filter(contrato__inicio__lte=fecha, contrato__vencimiento__gte=fecha)
                pool = get_set_contracts(pool)

        #for element in pool:
        #    print(f'Pool de fecha: {element.cuenta} {element.dispuesto}')
        response = []
        for element in pool:
            try:
                cirbe = CirbeSerializer(Cirbe.objects.filter(contrato=element.contrato).latest('documento__fecha')).data
            except:
                cirbe = {}
            element = PoolSerializer(element).data
            element['cirbe'] = cirbe
            response.append(element)
        try:
            response = sorted(response, key=lambda i: (i['contrato']['tipo'], i['contrato']['entidad']), reverse=True)
        except:
            pass
        #return Response({fecha.strftime('%Y-%m-%d'): PoolSerializer(pool, many=True).data})
        return Response({fecha.strftime('%Y-%m-%d'): response})


def instance_dict(instance, key_format=None):
    "Returns a dictionary containing field names and values for the given instance"
    from django.db.models.fields.related import ForeignKey
    if key_format:
        assert '%s' in key_format, 'key_format must contain a %s'
    key = lambda key: key_format and key_format % key or key

    d = {}
    for field in instance._meta.fields:
        attr = field.name
        value = getattr(instance, attr)
        if value is not None and isinstance(field, ForeignKey):
            value = value._get_pk_val()
        d[key(attr)] = value
    for field in instance._meta.many_to_many:
        d[key(field.name)] = [obj._get_pk_val() for obj in getattr(instance, field.attname).all()]
    return d
"""
def calculo_caracteristicas(element, fecha):
    caracteristicas = {}
    precio = element.precio / 100
    caracteristicas['precio'] = (pow(precio + 1.0, element.periodificacion / 12.0) - 1.0)
    caracteristicas['plazos_pasados'] = int(math.floor(numeroMeses(fecha, element.inicio) / element.periodificacion))
    caracteristicas['plazos_amortizacion'] = int((numeroMeses(element.vencimiento, element.inicio) - element.carencia) /
                                                 element.periodificacion)

    if precio == 0:
        caracteristicas['cuota'] = element.limite / caracteristicas['plazos_amortizacion']
    else:
        caracteristicas['cuota'] = round(element.limite * caracteristicas['precio'] /
                                     (1.0 - pow(1.0 + caracteristicas['precio'], -1.0 *
                                                caracteristicas['plazos_amortizacion'])), 2)
    caracteristicas['pendiente_anual'] = int(numeroMeses(datetime.date(fecha.year, 12, 31), fecha) /
                                             element.periodificacion) * caracteristicas['cuota']

    return caracteristicas


def get_lp(pool, fecha):

    final_pool = []
    for element in pool:
        if pool.filter(contrato=element.contrato).count() == 2:
            if element.plazo:
                element.dispuesto = pool.filter(contrato=element.contrato).aggregate(dispuesto_sum=Sum('dispuesto'))['dispuesto_sum']
                caracteristicas = calculo_caracteristicas(element.contrato, fecha)
                element = instance_dict(element)
                contrato = instance_dict(Contrato.objects.get(id=element['contrato']))
                contrato['cuota'] = caracteristicas['cuota']
                contrato['pendientes'] = caracteristicas['plazos_amortizacion']
                contrato['pendiente_anual'] = caracteristicas['pendiente_anual']
                element['contrato'] = contrato
                print(element)
                final_pool.append(element)
        else:
            pass

    return final_pool

@api_view(['POST'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
def pool_for_alia(request):

    CIF = request.POST['CIF']
    fecha = datetime.date.today()
    actual = Documento.objects.filter(empresa__CIF=CIF, fecha__lte=fecha, origen='BSS').latest('fecha')
    print('Fecha más actual: ', actual.fecha)
    fecha = actual.fecha
    pool = Pool.objects.filter(documento=actual).exclude(contrato__isnull=True).exclude(contrato__producto='excepcion')
    pool_lp = get_lp(pool, fecha)
    cuentas = [cuenta['id'] for cuenta in pool_lp]
    print(cuentas)
    pool_cp = Pool.objects.exclude(id__in=cuentas)
    print(pool_cp)

    return Response({fecha.strftime('%Y-%m-%d'): {'prestamos': json.dumps(pool_lp, default=str),
                                                  'corto plazo': PoolSerializer(pool_cp, many=True).data}})
"""

class RatioViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.RatioSerializer

    queryset = Ratio.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]

    filterset_fields = {'documento__empresa__CIF': ['exact'],
                        'documento__fecha': ['lte', 'exact', 'gte'],
                        'documento__origen': ['exact'],
                        'concepto': ['exact']}

"""
@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='CIF', description='CIF de la empresa de interés', required=True, type=str),
                           OpenApiParameter(name='fecha', description='fecha de interés', required=True, type=str)])
def RatiosVerticales(request):
    try:
        CIF = request.GET['CIF']
    except:
        return Response({'error': 'Falta el CIF en la petición de información'}, status.HTTP_400_BAD_REQUEST)

    try:
        empresa = Empresa.objects.get(CIF=CIF)
    except Empresa.DoesNotExist:
        return Response({'error': 'No existe una empresa con el CIF proporcionado'}, status.HTTP_400_BAD_REQUEST)

    try:
        fecha = request.queryparams.get('fecha').split('-')
        try:
            fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
        except:
            return Response({'error': 'Formato o números incorrectos para fecha que ha de ser yyyy-mm-dd'},
                            status=status.HTTP_400_BAD_REQUEST)
    except:
        # print('No se ha enviado fecha a la api, se toma la fecha presente como fecha por defecto')
        fecha = datetime.date.today()

    try:
        actual_Modelo200 = Documento.objects.filter(fecha__lte=fecha, origen='Modelo200', empresa=empresa).latest('fecha')
    except:
        actual_Modelo200 = None
    try:
        actual_BSS = Documento.objects.filter(fecha__lte=fecha, origen='BSS', empresa=empresa).latest('fecha')
    except:
        actual_BSS = None

    if not actual_BSS and not actual_Modelo200:
        return Response({'error': f'No existe documentación anterior a {fecha}, cargar documentación antes de realizar la consulta de ratios'},
                        status=status.HTTP_404_NOT_FOUND)

    if actual_Modelo200:
        actual = actual_Modelo200
        if actual_BSS and actual_BSS.fecha > actual_Modelo200.fecha:
            actual = actual_BSS
    else:
        actual = actual_BSS

    fecha = actual.fecha
    fecha1 = datetime.date(fecha.year - 1, 12, 31)
    fecha2 = datetime.date(fecha.year - 2, 12, 31)
    year1 = str(fecha.year)
    year2 = str(fecha.year - 1)
    year3 = str(fecha.year - 2)

    #ratiosDF = pd.DataFrame(columns=['grupo', 'subgrupo', 'ratio', year1, 'evolucion1', year2, 'evolucion2', year3])
    ratiosDF = pd.DataFrame(columns=['ratio', year1, 'evolucion1', year2, 'evolucion2', year3])

    ratiosActual = Ratio.objects.filter(documento=actual)
    ratiosDF['ratio'] = ratiosActual.values_list('concepto', flat=True)

    ratiosIntermedios = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha1, documento__origen='Modelo200')
    if not ratiosIntermedios:
        ratiosIntermedios = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha1, documento__origen='BSS')

    ratiosInicio = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha2, documento__origen='Modelo200')
    if not ratiosInicio:
        ratiosInicio = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha2, documento__origen='BSS')

    print(ratiosActual.first().documento, ratiosIntermedios.first().documento, ratiosInicio.first().documento)

    print(len(ratiosActual.values_list('concepto', flat=True)),
          len(ratiosIntermedios.values_list('concepto', flat=True)),
          len(ratiosInicio.values_list('concepto', flat=True)))
    i = 0
    while i < len(ratiosActual.values_list('concepto', flat=True)):
        print(ratiosActual.values_list('concepto', flat=True)[i])
        print(ratiosIntermedios.values_list('concepto', flat=True)[i])
        print(ratiosInicio.values_list('concepto', flat=True)[i], '\n')
        i+=1

    ratiosDF[year1] = ratiosActual.values_list('magnitud', flat=True)
    if ratiosIntermedios:
        ratiosDF[year2] = ratiosIntermedios.values_list('magnitud', flat=True)
        ratiosDF.evolucion1 = round((ratiosDF[year1]/ratiosDF[year2] - 1) * 100, 2)
    else:
        ratiosDF.evolucion1 = None
        ratiosDF[year2] = None

    if ratiosInicio:
        ratiosDF[year3] = ratiosInicio.values_list('magnitud', flat=True)
        if ratiosIntermedios:
            ratiosDF.evolucion2 = round((ratiosDF[year2]/ratiosDF[year3] - 1) * 100, 2)
        else:
            ratiosDF.evolucion2 = None
    else:
        ratiosDF.evolucion2 = None
        ratiosDF[year3] = None
    ratiosDF[year1] = [round(x, 2) for x in ratiosDF[year1]]
    if ratiosIntermedios:
        ratiosDF[year2] = [round(x, 2) for x in ratiosDF[year2]]
    if ratiosInicio:
        ratiosDF[year3] = [round(x, 2) for x in ratiosDF[year3]]
    print(ratiosDF)
    return Response({fecha.strftime('%Y-%m-%d'): ratiosDF.to_dict('record')})
"""
def round_or_none(x, dec):
    if x != None and not math.isnan(x):
        if x == math.inf or x == -math.inf:
            return x
        else:
            return round(x, dec)
    else:
        return None

def mydivision(n, d):
    if d and not math.isnan(d):
        if n != None and not math.isnan(n):
            return n/d
        else:
            return None
    else:
        if n and not math.isnan(n):
            return float('inf')
        else:
            return None

def calcularHorizontal(doc):

    actual = doc
    fecha = actual.fecha
    empresa = doc.empresa

    existencias_finales = Contabilidad.objects.get(concepto='existencias', documento=doc).magnitud

    existencias_iniciales = Contabilidad.objects.filter(concepto='existencias',
                                                        documento__fecha=datetime.date(fecha.year - 1, 12, 31),
                                                        documento__empresa=empresa)
    try:
        existencias_iniciales = existencias_iniciales.get(documento__origen='Modelo200').magnitud
    except:
        existencias_iniciales = existencias_iniciales.get(documento__origen='BSS').magnitud

    aprovisionamientos1 = Contabilidad.objects.get(concepto='aprovisionamientos', documento=actual).magnitud

    if existencias_iniciales == None or existencias_iniciales == None or aprovisionamientos1 == None or \
            math.isnan(existencias_iniciales) or math.isnan(existencias_finales) or math.isnan(aprovisionamientos1):
        raise Exception('faltan datos de existencias y aprovisionamientos')

    ratio = existencias_finales - aprovisionamientos1 - existencias_iniciales
    ratios = {}
    ratios['coste ventas'] = [round(ratio, 4), '€']

    rotacion = round_or_none(mydivision(2 * ratio, (existencias_finales + existencias_iniciales)), 4)
    ratios['rotacion stocks'] = [rotacion, 'días']

    temp = mydivision((existencias_finales + existencias_iniciales)/2, ratio)
    try:
        if temp: almacenamiento = round(365 * temp, 4)
        else: almacenamiento = None
    except: almacenamiento = float('inf')

    ratios['PM almacenamiento'] = [almacenamiento, 'días']

    cobro1 = Ratio.objects.get(concepto='PM cobro', documento=actual).magnitud
    try: ratios['PM cobro + almacenamiento'] = [round(cobro1 + almacenamiento, 4), 'días']
    except: ratios['PM cobro + almacenamiento'] = [None, 'días']

    proveedores1 = Ratio.objects.get(concepto='PM proveedores', documento=actual).magnitud
    try: ratios['PM maduracion'] = [round(ratios['PM cobro + almacenamiento'][0] - proveedores1, 4), 'días']
    except: ratios['PM maduracion'] = [None, 'días']

    ventas1 = Ratio.objects.get(concepto='ventas', documento=actual).magnitud
    deuda1 = Contabilidad.objects.get(concepto='deuda con caracteristicas especiales a corto plazo', documento=actual).magnitud
    # ratio = extrapolacion * (ventas1 * 0.012 + ventas1 / 365 * cobro1 - aprovisionamientos1 / 365 * proveedores1) + \
    #        (existencias1 + existencias2) / 2  - deuda1

    try:
        if ventas1 != 0:
            if cobro1 == 0:
                ratio1 = math.inf
            else:
                ratio1 = ventas1 * 0.012 + ventas1 / 365 * cobro1
        else:
            if cobro1 == 0:
                ratio1 = None
            else:
                ratio1 = 0

        if aprovisionamientos1 != 0:
            if proveedores1 == 0:
                ratio2 = math.inf
            else:
                ratio2 = aprovisionamientos1 / 365 * proveedores1
        else:
            if proveedores1 == 0:
                ratio2 = None
            else:
                ratio2 = 0

        if ratio1 != None and ratio2 != None:
            if ratio1 == math.inf:
                if ratio2 == math.inf:
                    ratio = None
                else:
                    ratio = math.inf
            else:
                if ratio2 == math.inf:
                    ratio = -math.inf
                else:
                    ratio = (ratio1 - ratio2) + (existencias_finales + existencias_iniciales) / 2 - deuda1
        else:
            ratio = None

        if ratio:
            if ratio != math.inf and ratio != float('-inf'):
                ratio = round_or_none(ratio, 2)
            ratios['NOF medias'] = [ratio, '€']
        else:
            ratios['NOF medias'] = [None, '€']

        try:
            fondo1 = Ratio.objects.get(concepto='fondo maniobra', documento=actual).magnitud
            ratios['NRN medias'] = [round(ratio - fondo1, 2), '€']
        except:
            ratios['NRN medias'] = [None, '€']
    except:
        ratios['NRN medias'] = [None, '€']
        ratios['NOF medias'] = [None, '€']

    return ratios


def calcularEvolucion(ratiosDF, empresa, fecha, year1, year2, origen):
    doc = Documento.objects.get(empresa=empresa, fecha=fecha, origen=origen)
    result = calcularHorizontal(doc)
    #print(result)
    for element in result.keys():
        #print(f'{element}: {result[element][0]}')
        ratiosDF.loc[ratiosDF['ratio'] == element, year2] = result[element][0]
        try:
            #print(f"Numerador {year1}: {ratiosDF.loc[ratiosDF['ratio'] == element, year1].item()} - "
            #      f"Denominador {year2}: {result[element][0]}")
            x = mydivision(ratiosDF.loc[ratiosDF['ratio'] == element, year1].item(), result[element][0])
            if x != None and x != math.inf and x != -math.inf:
                ratiosDF.loc[ratiosDF['ratio'] == element, 'evolucion1'] = round((x - 1) * 100, 2)
            else: ratiosDF.loc[ratiosDF['ratio'] == element, 'evolucion1'] = x
        except Exception as e:
            #print('Exception', e)
            ratiosDF.loc[ratiosDF['ratio'] == element, 'evolucion1'] = None
        #print(f"Evolución: {ratiosDF.loc[ratiosDF['ratio'] == element, 'evolucion1'].item()}")
    return ratiosDF


def check_request(request):
    try:
        CIF = request.GET['CIF']
    except:
        return {'error': 'Falta el CIF en la petición de información'}, status.HTTP_400_BAD_REQUEST, None

    try:
        empresa = Empresa.objects.get(CIF=CIF)
    except:
        return {'error': f'no existe compañía con CIF {CIF}'}, status.HTTP_404_NOT_FOUND, None
    # ajustar la fecha a aquella fecha más próxima (en el pasado) a la solicitada para la que se disponga de información
    try:
        fecha = request.GET['fecha'].split('-')
        try:
            fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
        except:
            return {'error': 'Formato o números incorrectos para fecha que ha de ser yyyy-mm-dd'}, \
                   status.HTTP_400_BAD_REQUEST, None
    except:
        # print('Fecha incorrecta, se toma la fecha de hoy como fecha de referencia')
        fecha = datetime.date.today()

    try:
        # fecha__gte=datetime.date(fecha.year-1, fecha.month, fecha.day), para filtrar que hay docs de menos de un año
        # de antiguedad; no parece funcionar, hay que analizar
        actual = Documento.objects.filter(empresa=empresa, fecha__lte=fecha, origen='BSS').latest('fecha')
    except:
        return {'error': 'Servicios de deuda: No se ha identificado documentación válida. Revisar documentación'}, \
               status.HTTP_404_NOT_FOUND, None

    return empresa, fecha, actual
"""
@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='CIF', description='CIF de la empresa de interés', required=True, type=str),
                           OpenApiParameter(name='fecha', description='fecha de interés', required=True, type=str)])
def CoberturaDeuda(request):
    empresa, fecha, actual = check_request(request)
    if not actual:
        return Response(empresa, fecha)
"""

def servicio(contratos, year):
    #print(year)
    servicio_final = 0
    meses = 12
    instancias = []
    #print(contratos)
    #for contrato in [id for id in contratos if id]:
    #    if contrato:
    #        contrato = Contrato.objects.get(id=contrato)
    #        instancias.append(contrato)
    contratos = [contrato for contrato in contratos if contrato]
    #print(contratos)
    for contrato in contratos:
        #print(contrato.inicio > datetime.date(year, 1, 1), contrato.vencimiento < datetime.date(year, 12, 31), contrato.cuota)
        if contrato.inicio > datetime.date(year, 1, 1):
            meses = numeroMeses(datetime.date(year, 12, 31), contrato.inicio)
        if contrato.vencimiento < datetime.date(year, 12, 31):
            meses = meses - numeroMeses(datetime.date(year, 12, 31), contrato.vencimiento)
        if contrato.cuota:
            servicio_final += contrato.cuota * math.ceil(meses / contrato.periodificacion)
    #print(servicio_final)
    return servicio_final


@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='CIF', description='CIF de la empresa de interés', required=True, type=str),
                           OpenApiParameter(name='fecha', description='fecha de interés', required=True, type=str),
                           OpenApiParameter(name='tabla', description='tabla de interés', required=False, type=str)])
def RatiosIntegrados(request):

    try:
        CIF = request.GET['CIF']
    except:
        return Response({'error': 'Falta el CIF en la petición de información'}, status.HTTP_400_BAD_REQUEST)

    try:
        empresa = Empresa.objects.get(CIF=CIF)
    except Empresa.DoesNotExist:
        return Response({'error': f'No existe una empresa con el CIF proporcionado {CIF}'}, status.HTTP_404_NOT_FOUND)

    try:
        tabla = request.GET['tabla']
        #tablas = []
        #for element in dictionaries.grupos_ratios.keys():
        #    tablas.extend(list(dictionaries.grupos_ratios[element].keys()))
        if tabla not in dictionaries.grupos_ratios.keys():
            return Response({'error': f'No esiste la tabla solicitada {tabla}'})
    except:
        #return Response({'error': f'Indispensable indicar una de las tablas en {dictionaries.grupos_ratios.keys()}'})
        tabla = None

    try:
        fecha = request.queryparams.get('fecha').split('-')
        try:
            fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
        except:
            return Response({'error': 'Formato o números incorrectos para fecha que ha de ser yyyy-mm-dd'},
                            status=status.HTTP_400_BAD_REQUEST)
    except:
        # print('No se ha enviado fecha a la api, se toma la fecha presente como fecha por defecto')
        fecha = datetime.date.today()

    try:
        actual_Modelo200 = Documento.objects.filter(fecha__lte=fecha, origen='Modelo200', empresa=empresa).latest('fecha')
    except:
        actual_Modelo200 = None
    try:
        actual_BSS = Documento.objects.filter(fecha__lte=fecha, origen='BSS', empresa=empresa).latest('fecha')
    except:
        actual_BSS = None

    if not actual_BSS and not actual_Modelo200:
        return Response({'error': f'No existe documentación anterior a {fecha}, cargar documentación antes de realizar la consulta de ratios'},
                        status=status.HTTP_404_NOT_FOUND)

    #print(actual_BSS, actual_Modelo200)

    if actual_Modelo200:
        actual = actual_Modelo200
        if actual_BSS and actual_BSS.fecha > actual_Modelo200.fecha:
            actual = actual_BSS
    else:
        actual = actual_BSS

    fecha1 = actual.fecha.strftime('%d/%m/%Y')
    fecha2 = datetime.date(actual.fecha.year - 1, 12, 31)
    fecha3 = datetime.date(actual.fecha.year - 2, 12, 31)
    year2 = str(actual.fecha.year - 1)
    year3 = str(actual.fecha.year - 2)


    ratiosDF = pd.DataFrame(columns=['grupo', 'subgrupo', 'ratio', 'unidades', fecha1, 'evolucion1', year2, 'evolucion2', year3])
    #ratiosDF = pd.DataFrame(columns=['ratio', year1, 'evolucion1', year2, 'evolucion2', year3])

    ratiosActual = Ratio.objects.filter(documento=actual)

    ratiosIntermedios = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha2, documento__origen='Modelo200')
    if not ratiosIntermedios:
            ratiosIntermedios = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha2, documento__origen='BSS')

    ratiosInicio = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha3, documento__origen='Modelo200')
    if not ratiosInicio:
            ratiosInicio = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha3, documento__origen='BSS')

    indicador = {}
    if tabla:
        grupos = range(1)
    else:
        grupos = dictionaries.grupos_ratios.keys()
    for grupo in grupos:
        if tabla:
            subgrupos = dictionaries.grupos_ratios[tabla].keys()
        else:
            subgrupos = dictionaries.grupos_ratios[grupo].keys()
        #print(grupo, subgrupos)
        for subgrupo in subgrupos:
            if tabla:
                ratios = dictionaries.grupos_ratios[tabla][subgrupo]
            else:
                ratios = dictionaries.grupos_ratios[grupo][subgrupo]
            #print(ratios)
            for ratio in ratios:
                #print(ratio)
                if not tabla:
                    indicador['grupo'] = grupo
                else:
                    indicador['grupo'] = tabla
                indicador['subgrupo'] = subgrupo
                indicador['ratio'] = ratio
                try:
                    ratioActual = ratiosActual.get(concepto=ratio)
                    indicador['unidades'] = ratioActual.unidades
                    indicador[fecha1] = ratioActual.magnitud
                    if ratiosIntermedios:
                        try:
                            indicador[year2] = ratiosIntermedios.get(concepto=ratio).magnitud
                            div = mydivision(indicador[fecha1], indicador[year2])
                            if div != None and div != math.inf and div != -math.inf:
                                indicador['evolucion1'] = round((div - 1) * 100, 2)
                            else:
                                indicador['evolucion1'] = div
                        except:
                            indicador['evolucion1'] = None
                            indicador[year2] = None
                    else:
                        indicador['evolucion1'] = None
                        indicador[year2] = None
                    if ratiosInicio:
                        try:
                            indicador[year3] = ratiosInicio.get(concepto=ratio).magnitud
                            if ratiosIntermedios:
                                div = mydivision(indicador[year2], indicador[year3])
                                if div != None and div != math.inf and div != -math.inf:
                                    indicador['evolucion2'] = round((div - 1) * 100, 2)
                                else:
                                    indicador['evolucion2'] = div
                            else:
                                indicador['evolucion2'] = None
                        except:
                            indicador[year3] = None
                            indicador['evolucion2'] = None
                    else:
                        indicador['evolucion2'] = None
                        indicador[year3] = None
                    ratiosDF = ratiosDF.append(indicador, ignore_index=True)
                except: pass
                #print(indicador)
                #print(ratiosDF)

    """
    ratiosDF[fecha1] = ratiosActual.magnitud.map(lambda x: round(x, 2) if x else None)
    if ratiosIntermedios:
        ratiosDF[year2] = ratiosIntermedios.magnitud.map(lambda x: round(x, 2) if x else None)
    if ratiosInicio:
        ratiosDF[year3] = ratiosInicio.magnitud.map(lambda x: round(x, 2) if x else None)
    """
    mensaje = ''
    if not tabla or tabla == 'working capital':
        try:
            result = calcularHorizontal(actual)
            #print('Result: ', result)
        except Exception as e:
            #print(e)
            result = False
            mensaje = f'Error {e}'
        if not result:
            mensaje += f' o bien es necesaria la información contable del ejercicio {actual.fecha.year - 1}'
            #return Response({'error': f'Es necesaria la información contable del ejercicio {actual.fecha.year - 1}'}, status=status.HTTP_404_NOT_FOUND)
        else:
            for element in result.keys():
                subgrupo = 'periodos medios de maduracion'
                if element in ['NOF medias', 'NRN medias']:
                    subgrupo = 'NOF vs. NRN'
                #print(f'{element}: {result[element][0]}')
                wc = round_or_none(result[element][0], 2)
                #print(f'{element}: {wc}')
                #print('Nueva fila: ', ['working capital', subgrupo, element, result[element][1], wc, None, None, None, None])
                ratiosDF.loc[len(ratiosDF)] = ['working capital', subgrupo, element, result[element][1],
                                               wc, None, None, None, None]

        Intermedio = True
        try:
            ratiosDF = calcularEvolucion(ratiosDF, empresa, fecha2, fecha1, year2, 'Modelo200')
        except:
            try:
                ratiosDF = calcularEvolucion(ratiosDF, empresa, fecha2, fecha1, year2, 'BSS')
            except:
                Intermedio = False

        if Intermedio:
            #ratiosDF.loc[ratiosDF]
            try:
                ratiosDF = calcularEvolucion(ratiosDF, empresa, fecha3, year2, year3, 'Modelo200')
            except:
                try:
                    ratiosDF = calcularEvolucion(ratiosDF, empresa, fecha3, year2, year3, 'BSS')
                except:
                    pass

    if not tabla or tabla == 'endeudamiento':
        contratos = list(set([pool.contrato for pool in Pool.objects.filter(documento=actual)]))
        servicio1 = servicio(contratos, actual.fecha.year)

        contratos = list(set([pool.contrato for pool in Pool.objects.filter(documento__empresa=empresa,
                                                                            documento__fecha__range=[fecha2, actual.fecha],
                                                                            documento__origen='BSS')]))
        # ('servicio2: ', contratos)
        if contratos:
            servicio2 = servicio(contratos, actual.fecha.year - 1)
            div = mydivision(servicio1-servicio2, servicio2)
            if div != math.inf and div != -math.inf and div != None:
                evolucion1 = round(div*100, 2)
            else:
                evolucion1 = div
        else:
            servicio2 = None
            evolucion1 = None

        contratos = list(set([pool.contrato for pool in Pool.objects.filter(documento__empresa=empresa,
                                                                            documento__fecha__range=[fecha3, fecha2],
                                                                            documento__origen='BSS')]))
        # print('servicio3: ', contratos)
        if contratos:
            servicio3 = servicio(contratos, actual.fecha.year - 2)
            try:
                div = mydivision(servicio2 - servicio3, servicio3)
                if div != math.inf and div != -math.inf and div != None:
                    evolucion2 = round(div * 100, 2)
                else:
                    evolucion2 = None
            except:
                evolucion2 = None
        else:
            servicio3 = None
            evolucion2 = None

        try: servicio1 = round(servicio1, 2)
        except: pass
        try: servicio2 = round(servicio2, 2)
        except: pass
        try: servicio3 = round(servicio3, 2)
        except: pass
        ratiosDF.loc[len(ratiosDF)] = ['endeudamiento', 'servicio de deuda', 'servicio de deuda', '€', servicio1,
                                       evolucion1, servicio2, evolucion2, servicio3]

        for element in ['servicio/EBITDA', 'servicio/BN', 'servicio/beneficio bruto']:

            concepto = element.split('/')[1]
            if concepto == 'beneficio bruto': concepto = 'margen de contribucion'

            denominador1 = Ratio.objects.get(documento=actual, concepto=concepto).magnitud
            if denominador1 == None or servicio1 == None:
                ratio1 = None
            else:
                ratio1 = mydivision(servicio1, denominador1)

            denominador2 = Ratio.objects.filter(documento__empresa=empresa,
                                                documento__fecha=datetime.date(actual.fecha.year-1, 12, 31)).exclude(documento__origen= 'Cirbe')
            if len(set(denominador2.values_list('documento', flat=True))) > 1:
                denominador2 = denominador2.filter(documento__origen='Modelo200')
            #else: denominador2 = denominador2.get(documento__origen='BSS')
            try:
                denominador2 = denominador2.get(concepto=concepto).magnitud
            except:
                denominador2 = None
            if servicio2 and denominador2:
                ratio2 = mydivision(servicio2, denominador2)
                div = mydivision(ratio1, ratio2)
                if div != None and div != math.inf and div != -math.inf:
                    evolucion1 = round(div - 1) * 100
                else:
                    evolucion1 = div
            else:
                ratio2 = None
                evolucion1 = None

            denominador3 = Ratio.objects.filter(documento__empresa=empresa,
                                                documento__fecha=datetime.date(actual.fecha.year - 2, 12, 31)).exclude(documento__origen= 'Cirbe')
            if len(set(denominador3.values_list('documento', flat=True))) > 1:
                denominador3 = denominador2.filter(documento__origen='Modelo200')
            try:
                denominador3 = denominador3.get(concepto=concepto).magnitud
            except:
                denominador3 = None
            if servicio3 and denominador3:
                ratio3 = mydivision(servicio3, denominador3)
                div = mydivision(ratio2, ratio3)
                if div != None and div != math.inf and div != -math.inf:
                    evolucion2 = round((div - 1) * 100, 2)
                else:
                    evolucion2 = div
            else:
                ratio3 = None
                evolucion2 = None

            if evolucion1:
                if ratio2 != None and ratio2 != math.inf and ratio2 != -math.inf:
                    ratio2 = round(ratio2)
            if evolucion2:
                if ratio3 != None and ratio3 != math.inf and ratio3 != -math.inf:
                    ratio3 = round(ratio3, 2)

            ratiosDF.loc[len(ratiosDF)] = ['endeudamiento', 'servicio de deuda', element, None, round(ratio1, 2), evolucion1,
                                           ratio2, evolucion2, ratio3]

    #ratiosDF = ratiosDF.fillna(np.nan).replace([np.nan], [None])
    #ratiosDF = ratiosDF.replace(math.nan, None)
    #ratiosDF = ratiosDF.replace(float('nan'), None)
    #ratiosDF = ratiosDF.where(pd.notnull(ratiosDF), None)
    ratiosDF = ratiosDF.replace(math.nan, '')


    ratiosDF = ratiosDF.replace(float('inf'), 'inf')
    ratiosDF = ratiosDF.replace(float('-inf'), '-inf')

    """
    ratiosDF[fecha1] = ratiosDF[fecha1].replace(float('inf'), 'inf')
    ratiosDF['evolucion1'] = ratiosDF['evolucion1'].replace(float('inf'), 'inf')
    ratiosDF[year2] = ratiosDF[year2].replace(float('inf'), 'inf')
    ratiosDF['evolucion2'] = ratiosDF['evolucion2'].replace(float('inf'), 'inf')
    ratiosDF[year3] = ratiosDF[year3].replace(float('inf'), 'inf')
    """
    ratiosDF.sort_values(['grupo', 'subgrupo'], inplace=True)
    #if tabla == 'working capital': print(ratiosDF)
    return Response({fecha1: ratiosDF.to_dict('record'), 'mensaje': mensaje})

"""
@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='CIF', description='CIF de la empresa de interés', required=True, type=str),
                           OpenApiParameter(name='fecha', description='fecha de interés', required=True, type=str),
                           OpenApiParameter(name='tabla', description='tabla de interés', required=False, type=str)])
def RatiosVerticales(request):
    try:
        CIF = request.GET['CIF']
    except:
        return Response({'error': 'Falta el CIF en la petición de información'}, status.HTTP_400_BAD_REQUEST)

    try:
        empresa = Empresa.objects.get(CIF=CIF)
    except Empresa.DoesNotExist:
        return Response({'error': f'No existe una empresa con el CIF proporcionado {CIF}'}, status.HTTP_400_BAD_REQUEST)

    try:
        tabla = request.GET['tabla']
        if tabla not in dictionaries.grupos_ratios.keys():
            return Response({'error': f'No esiste la tabla solicitada {tabla}'})
    except:
        #return Response({'error': f'Indispensable indicar una de las tablas en {dictionaries.grupos_ratios.keys()}'})
        tabla = None

    try:
        fecha = request.queryparams.get('fecha').split('-')
        try:
            fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
        except:
            return Response({'error': 'Formato o números incorrectos para fecha que ha de ser yyyy-mm-dd'},
                            status=status.HTTP_400_BAD_REQUEST)
    except:
        # print('No se ha enviado fecha a la api, se toma la fecha presente como fecha por defecto')
        fecha = datetime.date.today()

    try:
        actual_Modelo200 = Documento.objects.filter(fecha__lte=fecha, origen='Modelo200', empresa=empresa).latest('fecha')
    except:
        actual_Modelo200 = None
    try:
        actual_BSS = Documento.objects.filter(fecha__lte=fecha, origen='BSS', empresa=empresa).latest('fecha')
    except:
        actual_BSS = None

    if not actual_BSS and not actual_Modelo200:
        return Response({'error': f'No existe documentación anterior a {fecha}, cargar documentación antes de realizar la consulta de ratios'},
                        status=status.HTTP_404_NOT_FOUND)

    if actual_Modelo200:
        actual = actual_Modelo200
        if actual_BSS and actual_BSS.fecha > actual_Modelo200.fecha:
            actual = actual_BSS
    else:
        actual = actual_BSS

    fecha = actual.fecha
    fecha1 = datetime.date(fecha.year - 1, 12, 31)
    fecha2 = datetime.date(fecha.year - 2, 12, 31)
    year1 = str(fecha.year)
    year2 = str(fecha.year - 1)
    year3 = str(fecha.year - 2)


    ratiosDF = pd.DataFrame(columns=['grupo', 'subgrupo', 'ratio', 'unidades', year1, 'evolucion1', year2, 'evolucion2', year3])
    #ratiosDF = pd.DataFrame(columns=['ratio', year1, 'evolucion1', year2, 'evolucion2', year3])

    ratiosActual = Ratio.objects.filter(documento=actual)

    ratiosIntermedios = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha1, documento__origen='Modelo200')
    if not ratiosIntermedios:
        ratiosIntermedios = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha1, documento__origen='BSS')

    ratiosInicio = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha2, documento__origen='Modelo200')
    if not ratiosInicio:
        ratiosInicio = Ratio.objects.filter(documento__empresa=empresa, documento__fecha=fecha2, documento__origen='BSS')

    indicador = {}
    if tabla:
        grupos = range(1)
    else:
        grupos = dictionaries.grupos_ratios.keys()
    for grupo in grupos:
        if tabla:
            subgrupos = dictionaries.grupos_ratios[tabla].keys()
        else:
            subgrupos = dictionaries.grupos_ratios[grupo].keys()
        #print(grupo, subgrupos)
        for subgrupo in subgrupos:
            if tabla:
                ratios = dictionaries.grupos_ratios[tabla][subgrupo]
            else:
                ratios = dictionaries.grupos_ratios[grupo][subgrupo]
            #print(ratios)
            for ratio in ratios:
                #print(ratio)
                if not tabla:
                    indicador['grupo'] = grupo
                else:
                    indicador['grupo'] = tabla
                indicador['subgrupo'] = subgrupo
                indicador['ratio'] = ratio
                indicador['unidades'] = ratiosActual.get(concepto=ratio).unidades
                indicador[year1] = ratiosActual.get(concepto=ratio).magnitud
                if ratiosIntermedios:
                    indicador[year2] = ratiosIntermedios.get(concepto=ratio).magnitud
                    indicador['evolucion1'] = round((indicador[year1]/indicador[year2] - 1) * 100, 2)
                else:
                    indicador['evolucion1'] = None
                    indicador[year2] = None

                if ratiosInicio:
                    indicador[year3] = ratiosInicio.get(concepto=ratio).magnitud
                    if ratiosIntermedios:
                        indicador['evolucion2'] = round((indicador[year2]/indicador[year3] - 1) * 100, 2)
                    else:
                        indicador['evolucion2'] = None
                else:
                    indicador['evolucion2'] = None
                    indicador[year3] = None
                ratiosDF = ratiosDF.append(indicador, ignore_index=True)
                #print(indicador)
                #print(ratiosDF)
    ratiosDF[year1] = [round(x, 2) for x in ratiosDF[year1]]
    if ratiosIntermedios:
        ratiosDF[year2] = [round(x, 2) for x in ratiosDF[year2]]
    if ratiosInicio:
        ratiosDF[year3] = [round(x, 2) for x in ratiosDF[year3]]
    print(ratiosDF)
    return Response({fecha.strftime('%Y-%m-%d'): ratiosDF.to_dict('record')})


@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='CIF', description='CIF de la empresa de interés', required=True, type=str),
                           OpenApiParameter(name='fecha', description='fecha de interés', required=True, type=str)])
def RatiosHorizontales(request):

    try:
        CIF = request.GET['CIF']
    except:
        return Response({'error': 'Falta el CIF en la petición de información'}, status.HTTP_400_BAD_REQUEST)

    try:
        empresa = Empresa.objects.get(CIF=CIF)
    except Empresa.DoesNotExist:
        return Response({'error': 'No existe una empresa con el CIF proporcionado'}, status.HTTP_400_BAD_REQUEST)

    try:
        fecha = request.GET['fecha'].split('-')
        try:
            fecha = datetime.date(int(fecha[0]), int(fecha[1]), int(fecha[2]))
        except:
            return Response({'error': 'Formato o números incorrectos para fecha que ha de ser yyyy-mm-dd'})
    except:
        #print('No se ha enviado fecha a la api, se toma la fecha presente como fecha por defecto')
        fecha = datetime.date.today()

    try:
        actual_Modelo200 = Documento.objects.filter(fecha__lte=fecha, origen='Modelo200', empresa=empresa).latest('fecha')
    except:
        actual_Modelo200 = None
    try:
        actual_BSS = Documento.objects.filter(fecha__lte=fecha, origen='BSS', empresa=empresa).latest('fecha')
    except:
        actual_BSS = None

    if not actual_BSS and not actual_Modelo200:
        return Response({'error': f'No existe documentación anterior a {fecha}, cargar documentación antes de realizar la consulta de ratios'},
                        status=status.HTTP_404_NOT_FOUND)

    if actual_Modelo200:
        actual = actual_Modelo200
        if actual_BSS and actual_BSS.fecha > actual_Modelo200.fecha:
            actual = actual_BSS
    else:
        actual = actual_BSS

    fecha = actual.fecha

    #extrapolacion = (datetime.date(fecha.year, 12, 31) - datetime.date(fecha.year - 1, 12, 31)).days/((fecha - datetime.date(fecha.year-1, 12, 31)).days)

    ratios = {}
    existencias_finales = Contabilidad.objects.get(concepto='existencias', documento=actual).magnitud

    existencias_iniciales = Contabilidad.objects.filter(concepto='existencias', documento__fecha=datetime.date(fecha.year-1, 12, 31),
                                               documento__empresa=empresa)
    if not existencias_iniciales:
        return Response({'error': f'No existen datos para el año fiscal {fecha.year-1}, cargar BSS o Modelo200'}, status=status.HTTP_404_NOT_FOUND)

    try:
        existencias_iniciales = existencias_iniciales.get(documento__origen='Modelo200').magnitud
    except:
        existencias_iniciales = existencias_iniciales.get(documento__origen='BSS').magnitud

    aprovisionamientos1 = Contabilidad.objects.get(concepto='aprovisionamientos', documento=actual).magnitud

    ratio = existencias_finales - aprovisionamientos1 - existencias_iniciales

    ratios['coste ventas'] = round(ratio, 4)
    ratios['rotacion stocks'] = round(2*ratio/(existencias_finales+existencias_iniciales), 4)
    ratios['PM almacenamiento'] = round(365*((existencias_finales+existencias_iniciales)/2)/ratio, 4)

    cobro1 = Ratio.objects.get(concepto='PM cobro', documento=actual).magnitud

    ratios['PM cobro + almacenamiento'] = round(cobro1 + ratios['PM almacenamiento'], 4)

    proveedores1 = Ratio.objects.get(concepto='PM proveedores', documento=actual).magnitud

    ratios['PM maduracion'] = round(ratios['PM cobro + almacenamiento'] - proveedores1, 4)

    ventas1 = Ratio.objects.get(concepto='ventas', documento=actual).magnitud

    deuda1 = Contabilidad.objects.get(concepto='deuda con caracteristicas especiales a corto plazo', documento__fecha=actual).magnitud

    #ratio = extrapolacion * (ventas1 * 0.012 + ventas1 / 365 * cobro1 - aprovisionamientos1 / 365 * proveedores1) + \
    #        (existencias1 + existencias2) / 2  - deuda1
    ratio = (ventas1 * 0.012 + ventas1 / 365 * cobro1 - aprovisionamientos1 / 365 * proveedores1) + (existencias_finales + existencias_iniciales) / 2 - deuda1
    ratios['NOF medias'] = round(ratio, 2)

    fondo1 = Ratio.objects.get(concepto='fondo maniobra', documento=actual).magnitud

    ratios['NRN medias'] = round(ratio - fondo1, 2)

    return Response({fecha.strftime('%Y-%m-%d'): ratios})
"""

def check_pool(actual, fecha):
    try:
        pool = Pool.objects.filter(documento=actual).exclude(contrato__isnull=True).exclude(contrato__producto='excepcion')\
            .filter(contrato__inicio__lte=fecha, contrato__vencimiento__gte=fecha)
        if not pool.exists():
            return {'error': f'El Pool de {actual} está vacío (no hay cuentas asociadas a contratos activos)'}, status.HTTP_400_BAD_REQUEST
    except:
        return {'error': f'El Pool de {actual} está vacío (no hay cuentas asociadas a contratos activos)'}, status.HTTP_400_BAD_REQUEST

    return pool

@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='CIF', description='empresa de interés', required=True, type=str),
                           OpenApiParameter(name='fecha', description='pool en esa fecha', required=False, type=str)])
def ServicioDeudaView(request):

    servicio = pd.DataFrame()
    # recuperar el pool de una empresa
    empresa, fecha, actual = check_request(request)
    if not actual:
        return Response({'error': f'No hay documentación para la {empresa} en {fecha}'}, status=status.HTTP_404_NOT_FOUND)

    # filtrar por empresa
    pool = check_pool(actual, fecha)

    if type(pool[0]) == dict:
        return Response(pool[0], pool[1])

    total = []
    #fechas = []
    for cuenta in pool:
        if cuenta.plazo and str(cuenta.contrato.producto) in productos_largo:
            #print(cuenta.cuenta, cuenta.plazo)
            contrato = cuenta.contrato
            #nombre = Entidad.dictionary[contrato.entidad.codigo]
            #fecha = datetime.date.today()
            fecha_carencia = contrato.inicio + relativedelta(months=contrato.carencia)
            #print(fecha, fecha_carencia)
            pago_carencia = contrato.limite * contrato.precio / 100.0 * contrato.periodificacion / 12.0
            """
            pagos = []
            delta = relativedelta(months=1)
            while fecha <= contrato.vencimiento:
                if fecha.month not in fechas:
                    fechas.append(fecha.month)
                if fecha <= fecha_carencia:
                    if numeroMeses(fecha_carencia, fecha)%contrato.periodificacion == 0:
                        pagos.append(pago_carencia)
                    else: pagos.append(0)
                else:
                    if numeroMeses(contrato.vencimiento, fecha)%contrato.periodificacion == 0:
                        pagos.append(contrato.cuota)
                    else: pagos.append(0)
                fecha += delta
            total.append({'cuenta': cuenta.cuenta, 'entidad': nombre, 'pagos': pagos})
            """
            carencia = []
            principal = []
            if fecha_carencia >= fecha:
                #print('estoy en carencia')
                meses_carencia = numeroMeses(fecha_carencia, fecha, inverse=True)
                #añadir un '0' para el primer mes si ya se ha realizado el pago de intereses en el mes en curso
                if fecha.day >= fecha_carencia.day: carencia = [0]
                resto = meses_carencia%contrato.periodificacion
                #print(fecha_carencia.day, fecha.day, meses_carencia, resto)
                if resto > 0:
                    carencia += (resto - 1) * [0] + [pago_carencia]
                patron = [0] * (contrato.periodificacion - 1) + [pago_carencia]
                carencia += patron * int(meses_carencia / contrato.periodificacion)
                meses_principal = numeroMeses(contrato.vencimiento, fecha_carencia, inverse=True)
            else:
                #añadir un '0' para el primer mes si ya se ha realizado el pago en el mes en curso
                if fecha.day >= contrato.vencimiento.day: principal = [0]
                meses_principal = numeroMeses(contrato.vencimiento, fecha, inverse=True)
            resto = meses_principal%contrato.periodificacion
            if resto > 0:
                principal = (resto - 1) * [0] + [contrato.cuota]
            patron = [0] * (contrato.periodificacion - 1) + [contrato.cuota]
            principal += patron * int(meses_principal / contrato.periodificacion)
            #nombre = Entidad.dictionary[contrato.entidad.codigo]
            total.append({'cuenta': cuenta.cuenta, 'entidad': contrato.entidad.nombre,
                          'cuantia': contrato.limite, 'divisa': contrato.moneda.tipo, 'inicio': contrato.inicio,
                          'vencimiento': contrato.vencimiento, 'pendiente': cuenta.dispuesto, 'pagos': carencia + principal})
        elif cuenta.plazo and str(cuenta.contrato.producto) not in productos_largo:
            return Response({'error': f'Producto mal clasificado: True: {cuenta.plazo} True: {cuenta.cuenta[0] == 1} cuenta {cuenta.cuenta} '
                                      f'producto {str(cuenta.contrato.producto)}'}, status.HTTP_400_BAD_REQUEST)
        else:
            pass

    length = max([len(x['pagos']) for x in total])

    ### SERIA MEJOR CONSTRUIR DF DESDE EL PRINCIPIO, AÑADIR COLUMNA CON AGREGADO, AÑADIR FILA CON PAGOS ANUALES
    ### MUCHO TRABAJO PARA MINIAPP, VER SI SE PUEDE RETRASAR HASTA APP
    """
    df = pd.DataFrame(index=np.arange(length))
    i = 0
    for element in total:
        df[str(i)] = pd.Series(element['pagos'])
        i += 1
    df.replace(np.nan, 0, inplace=True)
    df["sum"] = 0
    for index, row in df.iterrows():
        add = 0
        for element in row:
            if element:
                add += element
        df.loc[index, 'sum'] = add
    print(df)
    """
    fechas = []
    if len(total) > 0:
        initial_month = fecha.month
        for n in range(length):
            month = (n+initial_month)%12
            if month == 0:
                month = 12
            fechas.append(f'{fecha.year + int(math.ceil((initial_month+n)/12)-1)}-{month}')

    total.append({'fechas': fechas})
    #servicio.insert(len(servicio.columns), nombre, pagos)
    #print('Servicio: ', servicio)
    return Response({actual.fecha.strftime('%Y-%m-%d'): total}) #, 'fechas': fechas})
    #return Response(servicio.to_json(orient='records'))


@api_view(['GET'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
@extend_schema(parameters=[OpenApiParameter(name='CIF', description='empresa de interés', required=True, type=str),
                           OpenApiParameter(name='fecha', description='pool en esa fecha', required=False, type=str)])
def deuda_agregada(request):

    empresa, fecha, actual = check_request(request)

    if not actual:
        return Response(empresa, fecha)

    pool = check_pool(actual, fecha)
    if type(pool[0]) == dict:
        return Response(pool[0], pool[1])

    distribucion_deuda = pd.DataFrame(columns=['entidad', 'total', 'porcentaje'])
    distribucion_inversion = pd.DataFrame(columns=['entidad', 'pendiente', 'porcentaje'])
    distribucion_circulante = pd.DataFrame(columns=['entidad', 'limite', 'porcentaje_limite', 'dispuesto', 'porcentaje_dispuesto'])
    distribucion_productos = pd.DataFrame(columns=['producto', 'limite', 'porcentaje_limite', 'dispuesto', 'porcentaje_dispuesto'])

    pool = get_set_contracts(pool)

    limite = sum([x.contrato.limite for x in pool if x.contrato.producto.tipo not in productos_largo and x.contrato.producto.tipo != 'excepcion'])
    pendiente = sum([x.dispuesto for x in pool if x.contrato.producto.tipo in productos_largo])
    total = limite + pendiente
    dispuesto = sum([x.dispuesto for x in pool if x.contrato.producto.tipo not in productos_largo and x.contrato.producto.tipo != 'excepcion'])
    total_actual = dispuesto + pendiente

    entidades = set([x.contrato.entidad for x in pool])

    for entidad in list(entidades):

        nombre = entidad.nombre
        deuda_pendiente = [x.dispuesto for x in pool if x.contrato.entidad == entidad and x.contrato.producto.tipo in productos_largo]
        deuda_dispuesto = [x.dispuesto for x in pool if x.contrato.entidad == entidad and x.contrato.producto.tipo not in productos_largo
                           and x.contrato.producto.tipo != 'excepcion']
        deuda_limite = [x.contrato.limite for x in pool if x.contrato.entidad == entidad and x.contrato.producto.tipo not in productos_largo
                        and x.contrato.producto.tipo != 'excepcion']
        deuda_total = sum(deuda_pendiente) + sum(deuda_limite)
        if deuda_total:
            distribucion_deuda.loc[len(distribucion_deuda)] = [nombre, deuda_total, round(100 * deuda_total / total, 2)]

        if deuda_pendiente:
            deuda_pendiente = sum(deuda_pendiente)
            distribucion_inversion.loc[len(distribucion_inversion)] = [nombre, deuda_pendiente, round(100 * deuda_pendiente / pendiente, 2)]

        if deuda_dispuesto or deuda_limite:
            deuda_dispuesto = sum(deuda_dispuesto)
            deuda_limite = sum(deuda_limite)
            distribucion_circulante.loc[len(distribucion_circulante)] = [nombre, deuda_limite, round(100 * deuda_limite / limite, 2),
                                                                         deuda_dispuesto, round(100 * deuda_dispuesto / dispuesto, 2)]

    distribucion_deuda = distribucion_deuda.groupby('entidad', as_index=False).sum()
    distribucion_inversion = distribucion_inversion.groupby('entidad', as_index=False).sum()
    distribucion_circulante = distribucion_circulante.groupby('entidad', as_index=False).sum()

    for producto, choice in Producto.choices:
        deuda_pendiente = [x.dispuesto for x in pool if x.contrato.producto.tipo == producto and x.contrato.producto.tipo in
                           productos_largo]
        deuda_dispuesto = [x.dispuesto for x in pool if x.contrato.producto.tipo == producto and x.contrato.producto.tipo not in
                           productos_largo and x.contrato.producto.tipo != 'excepcion']
        deuda_limite = [x.contrato.limite for x in pool if x.contrato.producto.tipo == producto and x.contrato.producto.tipo
                        not in productos_largo + ['excepcion']]
        #print(deuda_pendiente, deuda_dispuesto, deuda_limite)
        deuda_pendiente = sum(deuda_pendiente) #remamente lp
        deuda_dispuesto = sum(deuda_dispuesto) + deuda_pendiente  # deuda real
        deuda_limite = sum(deuda_limite) + deuda_pendiente  # deuda potencial
        #print(producto, deuda_pendiente, deuda_dispuesto, deuda_limite)
        if deuda_limite:
            distribucion_productos.loc[len(distribucion_productos)] = [producto, deuda_limite, round(100 * deuda_limite / total, 2),
                                                                       deuda_dispuesto, round(100 * deuda_dispuesto / total_actual, 2)]

    return Response({'total': distribucion_deuda.sort_values(['porcentaje'], ascending=False).to_dict('record'),
                     'inversion': distribucion_inversion.sort_values(['porcentaje'], ascending=False).to_dict('record'),
                     'circulante': distribucion_circulante.sort_values(['porcentaje_limite'], ascending=False).to_dict('record'),
                     'producto': distribucion_productos.sort_values(['porcentaje_limite'], ascending=False).to_dict('record')})


def validacion_operacion(operacion):

    argumentos = {}
    if operacion.Entidad:
        try: argumentos['entidad'] = Entidad.objects.get(codigo=operacion.Entidad)
        except: return 'Código de entidad inexistente'
    else:
        try: argumentos['entidad'] = Entidad.objects.get(nombre=operacion.Nombre)
        except: return 'Nombre de entidad inexistente'
    try: argumentos['producto'] = Producto.objects.get(tipo=operacion.Producto)
    except: return 'Producto no válido'
    if not (operacion['Importe Inicial'] and operacion['Importe Inicial'] > 0): return 'Falta el importe de la operación'
    try: argumentos['moneda'] = Moneda.objects.get(tipo=operacion.Moneda)
    except: return 'Moneda no válida'
    #print(Documento.choices_tipo)
    #print([x[1] for x in list(Documento.choices_tipo)])
    #print('Periodificacion: ', Contrato.periodificacion)
    #print(list(Contrato.periodificacion))
    #print([x[1] for x in list(Contrato.periodificacion)])
    if operacion['Forma de Pago'] not in Contrato.dictionary_periodificacion.keys(): return 'Forma de pago no válida' #[x[1] for x in list(Contrato.periodificacion)]: pass
    check = operacion['Carencia (meses)']
    if not (check >= 0 and check == int(check)): return 'Valor inválido para carencia'
    try: date_inicio = operacion['Fecha Inicio'].to_pydatetime().date()
    except: return 'Falta fecha de inicio o bien formato incorrecto'
    #print('Check: ', date_inicio)
    if date_inicio and date_inicio > datetime.date.today(): return 'Fecha de inicio no puede estar en el futuro'
    argumentos['inicio'] = date_inicio
    try:
        date_vencimiento = operacion['Fecha Vencimiento'].to_pydatetime().date()
    except:
        return 'Falta la fecha de vencimiento o formato incorrecto'
    if date_vencimiento and date_vencimiento < datetime.date.today(): return 'Fecha de vencimiento no puede estar en el pasado'
    argumentos['vencimiento'] = date_vencimiento

    cuenta = Pool.objects.filter(documento__empresa=operacion.empresa, cuenta=operacion.Cuenta,
                                 documento__fecha__range=[date_inicio, date_vencimiento])
    if not cuenta: return f'Cuenta principal {operacion.Cuenta} inexistente en el rango de fechas de la operación'
    else: argumentos['cuenta_id'] = cuenta.latest('documento__fecha').id

    if operacion['Cuenta Asociada']:
        asociada = Pool.objects.filter(documento__empresa=operacion.empresa, cuenta=operacion['Cuenta Asociada'],
                                       documento__fecha__range=[date_inicio, date_vencimiento])
        if asociada:
            argumentos['asociada_id'] = asociada.latest('documento__fecha').id
        else:
            return f'Cuenta asociada {operacion["Cuenta Asociada"]} inexistente para la empresa en el rango de fechas de la operación'

    if operacion['Operacion Cirbe Asociada']:
        cirbe = Cirbe.objects.filter(documento__empresa=operacion.empresa, operacion=operacion['Operacion Cirbe Asociada'],
                                     documento__fecha__range=[date_inicio, date_vencimiento])
        if cirbe:
            argumentos['cirbe_id'] = cirbe.latest('documento__fecha').id
        else:
            return 'Registro Cirbe inexistente para la empresa en el rango de fechas de la operación'

    if operacion.Precio > 1: return 'Error en el tipo de interés (superior al 100%)'

    return argumentos


@api_view(['POST'])
@authentication_classes([TokenAuthentication,])
@permission_classes((IsAuthenticated,))
def upload_contratos(request):

    try:
        CIF = request.POST['CIF']
    except:
        return Response({'error': 'Falta el CIF en la petición de información'}, status.HTTP_400_BAD_REQUEST)

    try:
        empresa = Empresa.objects.get(CIF=CIF)
    except Empresa.DoesNotExist:
        return Response({'error': 'No existe una empresa con el CIF proporcionado'}, status.HTTP_404_NOT_FOUND)

    try:
        file = request.FILES['file']
    except Exception as e:
        return Response({'error': 'falta fichero adjunto'}, status=status.HTTP_400_BAD_REQUEST)

    try:
        book = openpyxl.load_workbook(file)
    except Exception as e:
        return Response({'error': f'Fallo abriendo el excel: {e}'}, status=status.HTTP_500_INTERNAL_SERVER_ERROR)

    try:
        sh = book.get_sheet_by_name('Operaciones')
    except Exception as e:
        #print('Hay que configurar los parámetros de lectura del fichero Excel')
        return Response({'error': f'El excel no tiene página "Operaciones", revisar que se trata de una plantilla de contratos'})
    df = pd.read_excel(file, sheet_name=sh.title, usecols=lambda x: 'Unnamed' not in x,
                       converters={'Entidad': str, 'Cuenta': str, 'Cuenta Asociada': str}, parse_dates=True)
    #df = pd.read_excel(file, sheet_name=sh.title)
    df.dropna(how='all', inplace=True)
    df.replace(np.nan, None, inplace=True)
   #print(df)

    contratos = []
    identificadores = {}
    for index, operacion in df.iterrows():

        operacion['empresa'] = empresa
        #print(operacion.to_dict())

        validacion = validacion_operacion(operacion)
        #print(validacion)
        #print(operacion)
        if not type(validacion) == dict:
            return Response({'error': f'{validacion}: {operacion.to_dict()}'}, status=status.HTTP_400_BAD_REQUEST)
        #print(validacion)

        """
        try:
            pool = Pool.objects.filter(documento__empresa=empresa, cuenta=operacion.Cuenta, 
                                       documento__fecha__range=[validacion['inicio'], validacion['vencimiento']])
            #print([True for element in pool if element.contrato])
            if [True for element in pool if element.contrato]:
                return Response({'error': f'Esta compañía ya dispone de contrato para {operacion.Cuenta}'},
                                status=status.HTTP_400_BAD_REQUEST)
        except:
            pass
        """

        try:
            contrato = Contrato(**{'entidad': validacion['entidad'], 'producto': validacion['producto'], 'inicio': validacion['inicio'],
                                   'vencimiento': validacion['vencimiento'], 'carencia': int(operacion['Carencia (meses)']),
                                   'precio': operacion['Precio'] * 100, 'limite': operacion['Importe Inicial'], 'moneda': validacion['moneda'],
                                   'periodificacion': Contrato.dictionary_periodificacion[operacion['Forma de Pago']]})
            contratos.append(contrato)
        except Exception as e:
            return Response({'error': f'Fallo creando el contrato con {operacion.to_dict()} y {validacion}: {e}'},
                            status=status.HTTP_500_INTERNAL_SERVER_ERROR)

        identificadores[operacion.Cuenta] = validacion['cuenta_id']
        if operacion['Cuenta Asociada']: identificadores[operacion['Cuenta Asociada']] = validacion['asociada_id']
        if operacion['Operacion Cirbe Asociada']: identificadores[operacion['Operacion Cirbe Asociada']] = validacion['cirbe_id']

    df = df[['Cuenta', 'Cuenta Asociada', 'Operacion Cirbe Asociada']]
    df['cuenta_id'] = df['asociada_id'] = df['cirbe_id'] = None

    for index, value in df.iterrows():
        df.loc[index, 'cuenta_id'] = identificadores[str(value.Cuenta)]
        if value['Cuenta Asociada']: df.loc[index, 'asociada_id'] = identificadores[str(value['Cuenta Asociada'])]
        if value['Operacion Cirbe Asociada']: df.loc[index, 'cirbe_id'] = identificadores[str(value['Operacion Cirbe Asociada'])]

    #print('Contratos: ', contratos)
    return Response({'empresa': EmpresaSerializer(empresa).data, 'parametros': df.to_dict('records'),
                     'contratos': ContratoSerializer(contratos, many=True).data})
