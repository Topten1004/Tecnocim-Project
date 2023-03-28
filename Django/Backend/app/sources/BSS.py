# import logging
import datetime
import json
import os
from calendar import monthrange

import pandas as pd
from dateutil.relativedelta import relativedelta
from django.db.models import Sum

from core.serializers import PoolSerializer, ContabilidadSerializer
from sources import XLSX
from sources.BalanceCuentas import BalanceCuentas
from core.models import Pool, Crudos, Contabilidad, Ratio, Extracciones_Errores, Analitica, Cirbe, Empresa, Contrato

from sources.CuentasContables import contabilidad_por_cuentas
from sources.ratios import ratios_verticales


def detectPool(df):

    #print(df.columns)
    pool = pd.DataFrame(columns=['cuenta', 'concepto', 'dispuesto'])
    pool.flags.allows_duplicate_labels = False

    ### FALTAN LAS 572 QUE SE HAN DE RELACIONAR CON LOS DE CP PARA CALCULAR CORRECTAMENTE EL DISPUESTO
    for index, row in df.iterrows():
        if row.cuenta[:2] == '52' or row.cuenta[:3] == '170' or row.cuenta[:3] == '171' or row.cuenta[:3] == '174' or row.cuenta[:3] == '574':
            #print(row)
            pool.loc[len(pool)] = row.tolist()

    return pool


def savePool(row, documento):

    #print('Inicio:\n', row)
    #row2 = row.drop(['documento', 'cuenta'])
    #print(row2.to_dict())
    data = Pool.objects.update_or_create(documento=documento, cuenta=row.cuenta,
                                         defaults={'concepto': row.concepto, 'dispuesto': row.dispuesto,
                                                   'extraccion': row.extraccion})[0]

    try:
        register = Pool.objects.filter(documento__empresa=documento.empresa, cuenta=row.cuenta).exclude(contrato__isnull=True)
        #print('Primer filtro:\n',register)
        ### SOLO SIRVE SI SE CARGAN LOS DOCUMENTOS EN ORDEN CRONOLÓGICO
        register = register.latest('documento__fecha')
        #print('Segundo filtro:\n', register)
        #print(f'Comparación de fechas: inicio {register.contrato.inicio}, fecha {documento.fecha}, vencimiento {register.contrato.vencimiento}')
        #print(register.contrato.inicio <= documento.fecha <= register.contrato.vencimiento)
        if register.contrato.inicio <= documento.fecha <= register.contrato.vencimiento:
            data.contrato = register.contrato
            data.save()
    except:
        pass

    return data

def sugerencias(documento, conceptos, extraccion, sign):
    try:
        year = documento.fecha.year
        month = documento.fecha.month
        if month == 1:
            previous_month = 12
            previous_year = year - 2
        else:
            previous_month = month - 1
            previous_year = year - 1
        if month == 12:
            following_year = year
            following_month = 1
        else:
            following_year = year - 1
            following_month = month + 1

        #pool = PoolSerializer(Pool.objects.filter(documento__empresa=documento.empresa, contrato__isnull=True), many=True).data
        conceptos['variacion'] = Contabilidad.objects.get(documento=documento,
                                                          concepto="variacion de existencias de productos terminados y en curso de fabricacion").magnitud

        if conceptos['variacion'] == 0:
            try:
                variaciones = Contabilidad.objects.filter(documento__empresa=documento.empresa,
                                                          documento__fecha=datetime.date(year-1, month, monthrange(year-1, month)[1]),
                                                          concepto="variacion de existencias de productos terminados y en curso de fabricacion")
            except:
                variaciones = Contabilidad.objects.filter(documento__empresa=documento.empresa,
                                                          documento__fecha__range=[datetime.date(previous_year, previous_month, 1),
                                                                                   datetime.date(following_year, following_month,
                                                                                                 monthrange(following_year, following_month)[1])],
                                                          concepto="variacion de existencias de productos terminados y en curso de fabricacion")
            if variaciones:  variacion = variaciones.aggregate(Sum('magnitud'))['magnitud__sum'] / len(variaciones)
            else: variacion = None
            conceptos['sugerencia_variacion'] = variacion

        conceptos['amortizacion'] = Contabilidad.objects.get(documento=documento, concepto="amortizacion del inmovilizado").magnitud

        if conceptos['amortizacion'] == 0:
            try:
                amortizaciones = Contabilidad.objects.filter(documento__empresa=documento.empresa,
                                                             documento__fecha=datetime.date(year-1, month, monthrange(year-1, month)[1]),
                                                             concepto="amortizacion del inmovilizado")
            except:
                amortizaciones = Contabilidad.objects.filter(documento__empresa=documento.empresa,
                                                             documento__fecha__range=[datetime.date(previous_year, previous_month, 1),
                                                                                      datetime.date(following_year, following_month,
                                                                                                    monthrange(following_year, following_month)[1])],
                                                             concepto="amortizacion del inmovilizado")
            if amortizaciones:
                amortizacion = amortizaciones.aggregate(Sum('magnitud'))['magnitud__sum'] / len(amortizaciones)
            else:
                amortizacion = None
            conceptos['sugerencia_amortizacion'] = amortizacion

        conceptos['impuestos'] = Contabilidad.objects.get(documento=documento, concepto="impuestos sobre beneficios").magnitud

        if conceptos['impuestos'] == 0:
            resultados = Contabilidad.objects.get(documento=documento,
                                                  concepto="resultado del ejercicio procedente de operaciones continuadas").magnitud
            conceptos['sugerencia_impuestos'] = resultados * 0.25 * sign

        if conceptos['variacion'] == 0 or conceptos['amortizacion'] == 0 or conceptos['impuestos'] == 0:
            extraccion_error = Extracciones_Errores(**{'extraccion': extraccion, 'mensaje': 'carencias',
                                                       'traza': f'{conceptos}', 'bloqueo': 3})
            extraccion_error.save()

        #variacion = ContabilidadSerializer(variacion).data
        #extraccion.resultado = 'ok'
        #extraccion.save()
        conceptos['documento'] = documento.id
        return conceptos, True #, documento__fecha_lte=fecha

    except Exception as e:
        return {'extraccion': extraccion, 'mensaje': f'Error calculando las sugerencias de variación de existencias, '
                                                     f'amortizaciones e impuestos (función BSS()): {e}',
                'traza': f'{e}', 'bloqueo': 1}, False


def BSS(documento, conceptos, extraccion):

    inicio = datetime.datetime.now()
    #print(('Status inicio: ', documento.status)
    #print('Inicio: ', time.time())
    #global status
    result, success = XLSX.read(documento, conceptos, extraccion)

    if success:
        df = result
        cuentas_129 = success
        ##print(('Devuelto por XLSX: ', df)
    else:
        return result, False
    #df['documento'] = documento
    #final = datetime.datetime.now()
    #print(('Lectura xlsx: ', final - inicio)
    #inicio = final
    #identificar las cuentas en la bbdd si ya se había hecho una carga anterior
    crudos = list(Crudos.objects.filter(documento=documento).values_list('cuenta', flat=True))
    #print('Crudos iniciales: ', crudos)
    for index, row in df.iterrows():
        try:
            register, success = Crudos.objects.update_or_create(documento=documento, cuenta=row.cuenta,
                                                                defaults={'concepto': row.concepto,
                                                                          'magnitud': row.magnitud, 'extraccion': extraccion})
            try:
                #ir borrando de la lista las cuentas que se están usando en la actualización
                crudos.remove(row.cuenta)
            except: pass

        except Exception as e:
            try:
                field = ' '.join(row.tolist)
            except:
                field = 'no-identificado'
            return {'extraccion': extraccion, 'mensaje': 'Error salvando los datos crudos (función BSS())',
                    'traza': f'{e}', 'bloqueo': 1, 'tabla': 'Crudos', 'campo': field}, False
    #    return {'extraccion': extraccion, 'mensaje': 'Error salvando los datos crudos (función BSS())',
    #            'traza': f'{e}', 'bloqueo': 1, 'tabla': 'Crudos'}, False
    #final = datetime.datetime.now()
    #print(('Salvar crudos: ', final - inicio)
    #inicio = final
    #print('Crudos: ', crudos)
    if crudos:
        #si hay alguna cuenta que no se ha actualizado (por estar en una versión precedente), se borra
        #print('Crudos a eliminar: ', crudos)
        Crudos.objects.filter(documento=documento, cuenta__in=crudos).delete()

    try:
        contabilidad, warning = BalanceCuentas(df, documento, cuentas_129)
        # logging.info(contabilidad)
        if warning:
            warning = Extracciones_Errores(**{'extraccion': extraccion, 'mensaje': 'Posibles cuentas no-detectadas',
                                              'traza': f'{warning}', 'bloqueo': 3})
            warning.save()
    except Exception as e:
        return {'extraccion': extraccion, 'mensaje': 'Error calculando la contabilidad (función BalanceCuentas())',
                'traza': f'{e}', 'bloqueo': 1}, False

    activo = contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0]
    pasivo = contabilidad[contabilidad.concepto == 'total patrimonio neto y pasivo'].magnitud.values[0]
    #print(f'Activo: {activo}, Pasivo: {pasivo}, Config: {documento.empresa.configFile}')
    #f = open(documento.empresa.configFile, "r")
    #print(f.read())
    if activo != pasivo:
        #Empresa.objects.update_or_create(id=documento.empresa.id, defaults={'configFile': None})
        empresa = documento.empresa
        if os.path.exists(empresa.configFile):
            try:
                f = open(empresa.configFile)
                traza = json.load(f)
                traza = json.dumps(traza)
            except:
                traza = 'no se ha podido leer el contenido de configFile'
        else: traza = 'no existe configFile'
        os.remove(empresa.configFile)
        empresa.configFile = None
        empresa.save()
        #print(f'{traza} Activo: {activo}, Pasivo:{pasivo}')
        #print(empresa.configFile)
        ### SI NO SE QUIERE QUE EL DESCUADRE BLOQUEE EL PROCESAMIENTO
        extraccion_error = Extracciones_Errores(**{'extraccion': extraccion,
                                                   'mensaje': f'Balance no cuadra. Activo: {activo}, Pasivo: {pasivo}. '
                                                   f'Posible falta de saldos netos (función BalanceCuentas())',
                                                   'traza': f'{traza} Activo: {activo}, Pasivo:{pasivo}', 'bloqueo': 3})
        extraccion_error.save()
        #return {'extraccion': extraccion, 'mensaje': f'Balance no cuadra. Activo: {activo}, Pasivo: {pasivo}. '
        #                                             f'Posible falta de saldos netos (función BalanceCuentas())',
        #        'traza': f'{traza} Activo: {activo}, Pasivo:{pasivo}', 'bloqueo': 2}, False

    try:
        for i, row in contabilidad.iterrows():
            #print('Concepto en salvar contabilidad: ', row.concepto)
            data, cuentas_129 = Contabilidad.objects.update_or_create(codigo=row.codigo, concepto=row.concepto, documento=documento,
                                                                      defaults={'magnitud': row.magnitud, 'extraccion': extraccion})
    except Exception as e:
        try:
            field = ' '.join(row.tolist)
        except:
            field = 'no-identificado'
        return {'extraccion': extraccion, 'mensaje': 'Error salvando la contabilidad (función BSS())', 'traza': f'{e}',
                'bloqueo': 1, 'tabla': 'Contabilidad', 'campo': field}, False

    try:
        pool_previo = list(Pool.objects.filter(documento=documento).values_list('cuenta', flat=True))
        #('Pool previo: ', pool_previo)
        pool = detectPool(df)
        pool.concepto = pool['concepto'].fillna('')
        pool.dispuesto = pool['dispuesto'].fillna(0)
    except Exception as e:
        return {'extraccion': extraccion, 'mensaje': 'Error detectando el pool (función BSS())', 'traza': f'{e}',
                'bloqueo': 1}, False

    # logging.info(pool)
    try:
        for i, row in pool.iterrows():
            row.dispuesto = abs(round(row.dispuesto, 2))
            row.extraccion = extraccion
            data = savePool(row, documento)
            try:
                pool_previo.remove(row.cuenta)
            except:
                pass
            #if data.contrato:
            #    dispuesto_pool = Pool.objects.filter(documento=data.documento, contrato=data.contrato)
            #    dispuesto = dispuesto_pool.aggregate(Sum('dispuesto'))
            #    try:
            #        cirbe = Cirbe.objects.get(documento__empresa=documento.empresa, documento__fecha=documento.fecha, contrato=data.contrato)
            #        if cirbe.dispuesto != dispuesto:
            #            Extracciones_Errores(**{'extraccion': extraccion, 'mensaje': f'Error: el dispuesto del BSS ({row.dispuesto}) '
            #                                                                         f'no se corresponde con el del Cirbe {cirbe.dispuesto}',
            #                                    'bloqueo': 3, 'tabla': 'Pool', 'campo': 'dispuesto'}).save()
            #    except:
            #        pass
    except Exception as e:
        try:
            field = ' '.join(row.tolist)
        except:
            field = 'no-identificado'
        return {'extraccion': extraccion, 'mensaje': 'Error salvando los datos del pool (función BSS())', 'traza': f'{e}',
                'bloqueo': 1, 'tabla': 'Pool', 'campo': field}, False

    #print('Pool previo: ', pool_previo)
    if pool_previo:
        error = Extracciones_Errores(**{'extraccion': extraccion, 'mensaje': 'Quedan cuentas de la actualización anterior '
                                                                             'que se habrían de borrar o sobran de la presente',
                                        'traza': f'{pool_previo}', 'bloqueo': 3})
        error.save()
        #Pool.objects.filter(documento=documento, cuenta__in=pool_previo).delete()

    poolddbb = set(list(Pool.objects.filter(documento__empresa=documento.empresa, contrato__inicio__lte=documento.fecha,
                                            contrato__vencimiento__gte=documento.fecha).values_list('cuenta')))
    #print('Cuentas con contrato: ', list(poolddbb))
    missing = []
    for i, row in pool.iterrows():
        if row.cuenta not in poolddbb:
            missing.append(row.cuenta)
    if missing:
        error = Extracciones_Errores(**{'extraccion': extraccion, 'mensaje': 'Faltan cuentas de productos financieros activos',
                                        'traza': f'{missing}', 'bloqueo': 3})
        error.save()

    Analitica.objects.filter(documento=documento).delete()
    try:
        df['CuentaTemp'] = [str(cuenta)[0:3] for cuenta in df.cuenta]
        # se suman todas las cuentas que empiecen por la misma cifra que 'element'
        cantidad = df[df['CuentaTemp'] == '100']['magnitud'].sum()
        if cantidad < 0:
            sign = -1
        else:
            sign = 1
        df.drop('CuentaTemp', axis=1, inplace=True)
        insert_impuestos = True
        cuentas = list(set([str(x)[0:3] for x in df.cuenta]))
        if [x for x in cuentas if x in ['630', '633', '638']]:
            insert_impuestos = False

        if insert_impuestos and conceptos['impuestos']:
            # print(sign, variacion)
            cuenta = '630' + '0' * (max - 3)
            df.loc[len(df)] = [cuenta, 'correccion impuestos', conceptos['impuestos'] * sign]
        analitica = contabilidad_por_cuentas(df)

    except Exception as e:
        return {'extraccion': extraccion, 'mensaje': 'Error calculando y salvando la contabilidad analítica (función contabilidad_por_cuentas())',
                'traza': f'{e}', 'bloqueo': 1}, False

    try:
        for index, row in analitica.iterrows():
            data, status = Analitica.objects.update_or_create(cuenta=row.cuenta, documento=documento,
                                                              defaults={'magnitud': row.magnitud, 'extraccion': extraccion})
    except Exception as e:
        try:
            field = ' '.join(row.tolist())
        except:
            field = 'no-identificado'
        return {'extraccion': extraccion, 'mensaje': 'Error salvando contabilidad analítica (función BSS())', 'traza': f'{e}',
                'bloqueo': 1, 'tabla': 'Analitica', 'campo': field}, False

    try:
        kpis = ratios_verticales(contabilidad, documento.fecha)
    except Exception as e:
        return {'extraccion': extraccion, 'mensaje': 'Error calculando los ratios verticales (función ratios_verticales())',
                'traza': f'{e}', 'bloqueo': 1}, False

    Ratio.objects.filter(documento=documento).delete()
    for i, row in kpis.iterrows():
        try:
            data, status = Ratio.objects.update_or_create(documento=documento, concepto=row.concepto,
                                                          defaults={'magnitud': round(row.magnitud, 4),
                                                                    'unidades': row.unidades, 'extraccion': extraccion})
        except Exception as e:
            try:
                field = ' '.join(row.tolist)
            except Exception as e:
                field = 'no-identificado'
            return {'extraccion': extraccion, 'mensaje': 'Error salvando los ratios verticales (función BSS())', 'traza': f'{e}',
                    'bloqueo': 1, 'tabla': 'Ratios', 'campo': field}, False

    #print(('Status final: ', documento.status)
    documento.status = True
    documento.save()
    """
    excel = os.path.join(MEDIA_ROOT, os.path.dirname(documento.documento.name), os.path.basename(documento.documento.name).rsplit('.', 1)[0]+'resultados.xlsx')
    if os.path.exists(excel):
        os.remove(excel)

    writer = pd.ExcelWriter(excel, engine='xlsxwriter')

    # Write each dataframe to a different worksheet.
    df.to_excel(writer, sheet_name='Crudos', index=False)
    contabilidad.to_excel(writer, sheet_name='Contabilidad', index=False)
    #pool.to_excel(writer, sheet_name='Pool', index=False)
    analitica.to_excel(writer, sheet_name='Analítica', index=False)
    kpis.to_excel(writer, sheet_name='Ratios', index=False)
    #kpis.to_excel(writer, sheet_name='Indicadores', index=False)
    writer.save()
    #writer.close()
    """
    #print(('Final BSS: ', final - inicio)
    return sugerencias(documento, conceptos, extraccion, sign)