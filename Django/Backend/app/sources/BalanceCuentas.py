import datetime

from core import dictionaries
import pandas as pd
#import Ratios
from core.models import Contabilidad, Crudos


def BalanceCuentas(df, doc, cuentas_129):
    desired_width = 1200
    pd.set_option('display.width', desired_width)
    pd.set_option('display.float_format', lambda x: '%0.4f' % x)

    # df en el que salvar los resultados de la interpretación
    contabilidad = pd.DataFrame(columns=['concepto', 'magnitud'])
    contabilidad.flags.allows_duplicate_labels = False

    #print('\nInicio de cálculo de Balance de situación y Resultados\n')
    cuentas = df.cuenta.values.tolist()
    df['CuentaTemp'] = [str(cuenta)[0:3] for cuenta in df.cuenta]
    # se suman todas las cuentas que empiecen por la misma cifra que 'element'
    cantidad = df[df['CuentaTemp'] == '100']['magnitud'].sum()

    if cantidad < 0:
        change = True
    else:
        change = False

    # se itera por llaves, donde cada llave es un concepto contable diferente, con las cuentas asociadas como valores
    for concepto in dictionaries.conceptosAbreviado.keys():

        if change and concepto != "ACTIVO":
            signo = -1.0
        else:
            signo = 1.0

        for key in dictionaries.conceptosAbreviado[concepto].keys():
            cantidad = 0.0
            # se iteran las cuentas asociadas a esa llave
            for element in dictionaries.conceptosAbreviado[concepto][key]:
                # de cada cuenta, se coge solo el número de digitos equivalente a la cuenta 'element'
                length = len(str(element))
                # print(element, " ", type(element), " ", length)
                # se crea columna donde la cuenta tiene solo 'length' dígitos; así, por ejemplo, si la cuenta 'element' es la 20,
                # la columna tendrá los dos primero dígitos de cada cuenta, y todas las cuentas que empiecen por 20 se podrán agregar
                df['CuentaTemp'] = [str(cuenta)[0:length] for cuenta in df.cuenta]
                # se suman todas las cuentas que empiecen por la misma cifra que 'element'
                cantidad += df[df['CuentaTemp'] == str(element)]['magnitud'].sum() * signo
                sublist = [x for x in cuentas if str(x)[:length] == str(element)]
                for x in sublist:
                    cuentas.remove(x)
            # print(cantidad)
            # se añade al df la fila del concepto contable y su saldo asociado
            contabilidad.loc[len(contabilidad)] = [key, cantidad]
            # innecesario, se usaba cuando se hacía uso de append para añadir la fila, pero no se borra por ahora
            # new_row = {'Concepto': key, 'Magnitud': cantidad}
            # print(new_row)
            # contabilidad = pd.concat([contabilidad, pd.DataFrame.from_dict(new_row)], ignore_index=True)

    for cuenta in dictionaries.polivalentes.keys():
        length = len(str(cuenta))
        df['CuentaTemp'] = [str(cuenta)[0:length] for cuenta in df.cuenta]
        cantidad = df[df['CuentaTemp'] == str(cuenta)]['magnitud'].sum()
        #print(f'Polivalente {cuenta}: {cantidad}')
        if cantidad > 0:
            #print(contabilidad.at[contabilidad[contabilidad.concepto == dictionaries.polivalentes[cuenta][0]].index[0], 'magnitud'])
            contabilidad.loc[contabilidad.concepto == dictionaries.polivalentes[cuenta][0], 'magnitud'] += cantidad
        else:
            #print(contabilidad.at[contabilidad.concepto == dictionaries.polivalentes[cuenta][1]])
            contabilidad.loc[contabilidad.concepto == dictionaries.polivalentes[cuenta][1], 'magnitud'] -= cantidad
        sublist = [x for x in cuentas if str(x)[:length] == str(cuenta)]
        for x in sublist:
            cuentas.remove(x)
    #print('Cuentas remanentes: ', cuentas)

    # se determina si hay que calcular los impuestos o no, a base de mirar si las cuentas de impuestos están vacías o no
    df['CuentaTemp'] = [str(cuenta)[0:3] for cuenta in df.cuenta]
    # se suman todas las cuentas que empiecen por 63
    #print('Contenido en la cuenta 63: ', df[df['CuentaTemp'] == '63']['magnitud'].sum())
    check = df[df['CuentaTemp'].isin(['630', '633', '638'])]['magnitud'].sum()
    #print('Check con iterable: ', check)
    check = 0
    for cuenta in ['630', '633', '638']:
        check += df[df['CuentaTemp'] == cuenta]['magnitud'].sum()
    if check == 0:
        aplicar = True
    else:
        aplicar = False

    df['CuentaTemp'] = [str(cuenta)[0:2] for cuenta in df.cuenta]
    check = df[df['CuentaTemp'] == '68']['magnitud'].sum()
    if not check: check_amortizacion = True
    else: check_amortizacion = False

    #print('¿La aplicación va a aplicar impuestos?: ', aplicar)
    # se borra la columna cuentaTemp porque ya ha cumplido su cometido
    df.drop(labels='CuentaTemp', axis=1, inplace=True)
    # print(contabilidad[contabilidad.concepto == 'otras aportaciones socios']['magnitud'])
    # print(contabilidad)

    #conceptosTotales = []
    #for key in dictionaries.conceptosAbreviado.keys():
        #conceptosTotales.extend(dictionaries.conceptosAbreviado[key].keys())

    # se continúan los cálculos, ahora calculando los valores agregados
    for key in dictionaries.agregados.keys():
        cantidad = 0.0
        # la iteración por los valores asociados a la key nos permite sumar todos los conceptos asociados al concepto agregado
        for element in dictionaries.agregados[key]:
            #print(element)
            #print(contabilidad[contabilidad.concepto == element]['magnitud'])
            # if element in conceptosTotales:
            cantidad += float(contabilidad[contabilidad.concepto == element]['magnitud'].values)
        contabilidad.loc[len(contabilidad)] = [key, cantidad]
        #print(key, cantidad)
    # mismo esquema: calculamos valores agregados de los valores agregados de los conceptos contables
    for key in dictionaries.agregadosFinales.keys():
        cantidad = 0
        # print(key)
        for element in dictionaries.agregadosFinales[key]:
            #print(element, ' ', contabilidad[contabilidad.concepto == element]['magnitud'])
            cantidad += float(contabilidad[contabilidad.concepto == element]['magnitud'])
        #print(f'{key}: {cantidad}')
        contabilidad.loc[len(contabilidad)] = [key, cantidad]

    ### CÓDIGO DE AJUSTES POR ESTIMACIÓN DE IMPUESTOS Y AMORTIZACIONES, PERO YA VEREMOS SI AL FINAL SE HACE POR
    ### CREACIÓN DE CUENTAS FICTICIAS

    # lo más probable es que el BSS no esté cerrado, de manera que se realiza el siguiente ajuste a partir de los resultados:
    # impuestos = float(contabilidad[contabilidad.concepto =="impuestos sobre beneficios"]['magnitud'])
    resultados = float(contabilidad[contabilidad.concepto == 'resultado del ejercicio procedente de operaciones continuadas']['magnitud'])
    #print('\nRESULTADOS:\n', resultados)
    #print(contabilidad)
    # si la empresa tiene beneficios, se le aplica una tributación del 25% y el 75% restante va al pasivo
    # si no los tiene no tributa y las pérdidas van al pasivo
    """
    if resultados > 0: and aplicar:
        impuestos = 0.25
        # (1) se incrementan los impuestos un 25% de los beneficios
        print('Impuestos antes: ', contabilidad.loc[contabilidad.concepto == "impuestos sobre beneficios"]['magnitud'])
        contabilidad.loc[contabilidad.concepto == "impuestos sobre beneficios", 'magnitud'] -= resultados * impuestos
        print('Impuestos después: ', contabilidad.loc[contabilidad.concepto == "impuestos sobre beneficios"]['magnitud'])
        # (2) se decrementan los resultados parciales un 25% debido a los impuestos adicionales
        print('Resultado antes: ', contabilidad.loc[contabilidad.concepto == 'resultado del ejercicio procedente de operaciones continuadas']['magnitud'])
        contabilidad.loc[contabilidad.concepto == 'resultado del ejercicio procedente de operaciones continuadas', 'magnitud'] = resultados * (1.0 - impuestos)
        print('Resultado después: ', contabilidad.loc[contabilidad.concepto == 'resultado del ejercicio procedente de operaciones continuadas']['magnitud'])
        # (3) se actualizan los resultados finales un 25% debido a los impuestos adicionales
        print('Resultado final antes: ', contabilidad.loc[contabilidad.concepto == 'resultado de la cuenta de perdidas y ganancias', 'magnitud'])
        contabilidad.loc[contabilidad.concepto == 'resultado de la cuenta de perdidas y ganancias', 'magnitud'] -= resultados * impuestos
        print('Resultado final después: ', contabilidad.loc[contabilidad.concepto == 'resultado de la cuenta de perdidas y ganancias']['magnitud'])

    else:
        impuestos = 0
    """
    #print(contabilidad.loc[contabilidad.concepto == "resultado del ejercicio"].magnitud.values[0])
    resultados_pasivo = contabilidad.loc[contabilidad.concepto == "resultado del ejercicio"].magnitud.values[0]
    #print('Resultado Ejercicio en pasivo: ', resultados_pasivo)
    #print('resultado del ejercicio en resultados: ', resultados)
    if resultados_pasivo == 0 and resultados != 0:
        cuenta_129 = cuentas_129[0]
        #print(cuenta_129)
        Crudos.objects.update_or_create(cuenta=cuenta_129, documento=doc, extraccion=doc.extraccion,
                                        defaults={'concepto': 'correccion resultados en pasivo', 'magnitud': resultados})
        #print(Crudos.objects.filter(cuenta=cuenta_129, documento=doc, extraccion=doc.extraccion)[0].magnitud)
        # (4) se decrementa el resultado final un 25% de los resultados de operaciones continuadas por los impuestos adicionales
        #print('Cuenta 129 antes: ', contabilidad.loc[contabilidad.concepto == "resultado del ejercicio", 'magnitud'].values[0])
        contabilidad.loc[contabilidad.concepto == "resultado del ejercicio", 'magnitud'] = resultados
        #print('Cuenta 129 después: ', contabilidad.loc[contabilidad.concepto == "resultado del ejercicio", 'magnitud'].values[0])
        # (5) se incrementa el patrimonio neto con el beneficio neto del ejercicio
        #print('Patrimonio antes: ', contabilidad.loc[contabilidad.concepto == "patrimonio neto", 'magnitud'].values[0])
        contabilidad.loc[contabilidad.concepto == 'fondos propios', 'magnitud'] += resultados
        contabilidad.loc[contabilidad.concepto == "patrimonio neto", 'magnitud'] += resultados
        #print('Patrimonio después: ', contabilidad.loc[contabilidad.concepto == "patrimonio neto", 'magnitud'].values[0])
        # (6) se incrementa el pasivo total con el resultado del ejercicio
        #print('Pasivo antes: ', contabilidad.loc[contabilidad.concepto == "total patrimonio neto y pasivo", 'magnitud'].values[0])
        #print('Acreedores comerciales antes: ', contabilidad.loc[contabilidad.concepto == "acreedores comerciales y otras cuentas a pagar"])
        contabilidad.loc[contabilidad.concepto == "total patrimonio neto y pasivo", 'magnitud'] += resultados
        #print('Pasivo después: ', contabilidad.loc[contabilidad.concepto == "total patrimonio neto y pasivo", 'magnitud'].values[0])

    contabilidad = contabilidad.reset_index(drop=True)

    #contabilidad = Ratios.ratios(contabilidad)

    contabilidad['codigo'] = [dictionaries.conceptosAbreviado200[key] if key in dictionaries.conceptosAbreviado200.keys() else None
                              for key in contabilidad.concepto]
    #contabilidad['origen'] = 'BSS'
    #contabilidad = contabilidad[['concepto', 'codigo', 'magnitud', 'Fecha', 'origen']]
    contabilidad['magnitud'] = round(contabilidad.magnitud, 2)
    contabilidad = contabilidad[['concepto', 'codigo', 'magnitud']] #, 'origen']]

    #print('Contabilidad ajustada: ', contabilidad)

    return contabilidad, cuentas