import datetime
import math

import numpy as np
import pandas as pd

def check(args):
    return [element==True for element in args]

def ratios_verticales(contabilidad, fecha):
    desired_width = 2000
    pd.set_option('display.width', desired_width)

    #extrapolacion = (datetime.date(fecha.year, 12, 31) - datetime.date(fecha.year - 1, 12, 31)).days/((fecha - datetime.date(fecha.year-1, 12, 31)).days)
    #print('Extrapolación: ', extrapolacion)
    extrapolacion = 1
    kpis = pd.DataFrame(columns=['concepto', 'magnitud', 'unidades'])
    moneda = '€'

    #print('Sin loc pero con values[0]: ', contabilidad[contabilidad.concepto == 'activo corriente'].magnitud.values[0])

    ratio = contabilidad[contabilidad.concepto == 'activo corriente'].magnitud.values[0] - \
            contabilidad[contabilidad.concepto == 'pasivo corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['fondo maniobra', ratio, moneda]
    #print('fondo maniobra', ratio)
    denominador = contabilidad[contabilidad.concepto == 'pasivo corriente'].magnitud.values[0]

    ratio = abs(contabilidad[contabilidad.concepto == 'activo corriente'].magnitud.values[0] /
            denominador)

    kpis.loc[len(kpis)] = ['liquidez', ratio, moneda]
    #print('liquidez', ratio)
    ratio = abs((contabilidad[contabilidad.concepto == 'activo corriente'].magnitud.values[0] -
             contabilidad[contabilidad.concepto == 'existencias'].magnitud.values[0]) / \
             denominador)
    kpis.loc[len(kpis)] = ['prueba acida', ratio, None]
    #print('prueba acida', ratio)
    ratio = abs(contabilidad[contabilidad.concepto == 'efectivo y otros activos liquidos equivalentes'].magnitud.values[0] / \
            denominador)
    kpis.loc[len(kpis)] = ['disponibilidad', ratio, None]
    #print('disponibilidad', ratio)

    ratio = kpis[kpis.concepto == 'fondo maniobra'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['fondo maniobra/activo total', ratio, None]
    #print('maniobra/activo', ratio)

    ratio = (contabilidad[contabilidad.concepto == 'pasivo no corriente'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'pasivo corriente'].magnitud.values[0]) / \
            contabilidad[contabilidad.concepto == 'total patrimonio neto y pasivo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['endeudamiento', ratio, None]
    #print('endeudamiento', ratio)

    ratio = contabilidad[contabilidad.concepto == 'pasivo no corriente'].magnitud.values[0] /(
            contabilidad[contabilidad.concepto == 'pasivo corriente'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'pasivo no corriente'].magnitud.values[0])
    kpis.loc[len(kpis)] = ['calidad deuda', ratio, None]
    #print('calidad deuda', ratio)
    denominador = contabilidad[contabilidad.concepto == 'deudas con entidades de credito a largo plazo'].magnitud.values[0] + \
            contabilidad[contabilidad.concepto == 'deudas con entidades de credito a corto plazo'].magnitud.values[0]

    ratio = abs(contabilidad[contabilidad.concepto == 'gastos financieros'].magnitud.values[0] * extrapolacion / denominador)
    kpis.loc[len(kpis)] = ['coste de deuda', ratio, None]
    #print('coste deuda', ratio)

    """
    # 'coste medio del pasivo' no se puede calcular todavía porque faltan dividendos; ¿no son los dividendos a cuenta?
    ratio = (contabilidad[contabilidad.concepto == 'gastos financieros'].magnitud.values[0] + \
             contabilidad[contabilidad.concepto == 'dividendos'].magnitud.values[0]) /\
            contabilidad[contabilidad.concepto == 'total patrimonio neto y pasivo'].magnitud.values[0]
    contabilidad.loc[len(kpis)] = ['coste medio del pasivo', ratio, None]
    """
    ratio = extrapolacion * contabilidad[contabilidad.concepto == 'resultado de la cuenta de perdidas y ganancias'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BN', ratio, moneda]
    #print('BN', ratio)
    ratio = extrapolacion * contabilidad[contabilidad.concepto == 'resultado antes de impuestos'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAI', ratio, moneda]
    #print('BAI', ratio)
    ratio = extrapolacion * contabilidad[contabilidad.concepto == 'resultado de explotacion'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAII', ratio, moneda]
    #print('BAII', ratio)
    EBITDA = extrapolacion * (contabilidad[contabilidad.concepto == 'resultado de explotacion'].magnitud.values[0] -
             contabilidad[contabilidad.concepto == 'amortizacion del inmovilizado'].magnitud.values[0] -
             contabilidad[contabilidad.concepto == 'deterioro y resultado por enajenaciones del inmovilizado'].magnitud.values[0])
    #print('EBITDA', EBITDA)
    CASHFLOW = extrapolacion * (contabilidad[contabilidad.concepto == 'resultado de la cuenta de perdidas y ganancias'].magnitud.values[0] -
               contabilidad[contabilidad.concepto == 'amortizacion del inmovilizado'].magnitud.values[0])
    #print('cashflow', CASHFLOW)
    kpis.loc[len(kpis)] = ['cashflow', CASHFLOW, moneda]

    kpis.loc[len(kpis)] = ['EBITDA', EBITDA, moneda]
    ratio = contabilidad[contabilidad.concepto == 'deudas a largo plazo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['DFB L/P', ratio, moneda]
    #print('DFBLP', ratio)
    ratio = contabilidad[contabilidad.concepto == 'deudas a corto plazo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['DFB C/P', ratio, moneda]
    #print('DFBCP', ratio)
    ratio = abs(kpis[kpis.concepto == 'DFB L/P'].magnitud.values[0] +
            kpis[kpis.concepto == 'DFB C/P'].magnitud.values[0])
    kpis.loc[len(kpis)] = ['DFB', ratio, moneda]
    #print('DFB', ratio)
    ratio = abs(kpis[kpis.concepto == 'DFB'].magnitud.values[0] -
            contabilidad[contabilidad.concepto == 'efectivo y otros activos liquidos equivalentes'].magnitud.values[0] -
            contabilidad[contabilidad.concepto == 'inversiones financieras a corto plazo'].magnitud.values[0])
    kpis.loc[len(kpis)] = ['DFN', ratio, moneda]
    #print('DFN', ratio)

    ratio = kpis[kpis.concepto == 'DFB'].magnitud.values[0] / EBITDA
    kpis.loc[len(kpis)] = ['DFB/EBITDA', ratio, None]
    #print('DFB/EBITDA', ratio)
    ratio = kpis[kpis.concepto == 'DFN'].magnitud.values[0] / EBITDA
    kpis.loc[len(kpis)] = ['DFN/EBITDA', ratio, None]
    #print('DFN/EBITDA', ratio)

    ratio = kpis[kpis.concepto == 'DFB'].magnitud.values[0] / CASHFLOW
    kpis.loc[len(kpis)] = ['DFB/cashflow', ratio, None]
    #print('DFB/cashflow', ratio)
    ratio = kpis[kpis.concepto == 'DFN'].magnitud.values[0] / CASHFLOW
    kpis.loc[len(kpis)] = ['DFN/cashflow', ratio, None]
    #print('DFN/cashflow', ratio)

    ##################################################################################################
    ###                                                                                            ###
    ### NO ESTÁN LOS DE SERVICIO DE DEUDA. COMO DEPENDEN DEL POOL, SE CALCULAN CON QUERIES EN BBDD ###
    ###                                                                                            ###
    ##################################################################################################

    ratio = extrapolacion * contabilidad[contabilidad.concepto == 'resultado de explotacion'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAII/activo', ratio, None]
    #print('BAII/activo', ratio)

    ratio = extrapolacion * contabilidad[contabilidad.concepto == 'resultado antes de impuestos'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAI/activo', ratio, None]
    #print('BAI/activo', ratio)
    ratio = extrapolacion * contabilidad[contabilidad.concepto == 'resultado del ejercicio'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BN/activo', ratio, None]
    #print('BN/activo', ratio)
    ratio = extrapolacion * contabilidad[contabilidad.concepto == 'resultado de la cuenta de perdidas y ganancias'].magnitud.values[0] / \
            (contabilidad[contabilidad.concepto == 'patrimonio neto'].magnitud.values[0] -
             extrapolacion * contabilidad[contabilidad.concepto == 'resultado del ejercicio'].magnitud.values[0])
    kpis.loc[len(kpis)] = ['BN/(FFPP-beneficio año)', ratio, None]
    #print('algo', ratio)

    ratio = contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0] * \
            contabilidad[contabilidad.concepto == 'resultado antes de impuestos'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'patrimonio neto'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'resultado de explotacion'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['apalancamiento financiero', ratio, None]
    #print('apalancamiento', ratio)

    ratio = abs((contabilidad[contabilidad.concepto == 'gastos de personal'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'otros gastos de explotacion'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'imputacion de subvenciones de inmovilizado no financiero y otras'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'excesos de provisiones'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'deterioro y resultado por enajenaciones del inmovilizado'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'diferencia negativa de combinaciones de negocio'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'gastos financieros'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'variacion de valor razonable en instrumentos financieros'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'diferencias de cambio'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'deterioro y resultado por enajenaciones de instrumentos financieros'].magnitud.values[0]) /
            ((contabilidad[contabilidad.concepto == 'importe neto de la cifra de negocios'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'variacion de existencias de productos terminados y en curso de fabricacion'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'trabajos realizados por la empresa para su activo'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'aprovisionamientos'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'otros ingresos de explotacion'].magnitud.values[0]) /
            contabilidad[contabilidad.concepto == 'importe neto de la cifra de negocios'].magnitud.values[0]))

    kpis.loc[len(kpis)] = ['punto muerto', ratio, moneda]
    #print('punto muerto', ratio)
    ratio = extrapolacion * abs((contabilidad[contabilidad.concepto == 'gastos de personal'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'otros gastos de explotacion'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'excesos de provisiones'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'deterioro y resultado por enajenaciones del inmovilizado'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'diferencia negativa de combinaciones de negocio'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'gastos financieros'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'variacion de valor razonable en instrumentos financieros'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'diferencias de cambio'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'deterioro y resultado por enajenaciones de instrumentos financieros'].magnitud.values[0])/12.0/\
            ((contabilidad[contabilidad.concepto == 'importe neto de la cifra de negocios'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'variacion de existencias de productos terminados y en curso de fabricacion'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'trabajos realizados por la empresa para su activo'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'aprovisionamientos'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'otros ingresos de explotacion'].magnitud.values[0]) /
            contabilidad[contabilidad.concepto == 'importe neto de la cifra de negocios'].magnitud.values[0]))

    kpis.loc[len(kpis)] = ['punto muerto mensual', ratio, moneda]
    #print('punto muerto mensual', ratio)
    ratio = extrapolacion * (contabilidad[contabilidad.concepto == 'importe neto de la cifra de negocios'].magnitud.values[0] +
            contabilidad[contabilidad.concepto == 'aprovisionamientos'].magnitud.values[0]) + \
            contabilidad[contabilidad.concepto == 'variacion de existencias de productos terminados y en curso de fabricacion'].magnitud.values[0]

    kpis.loc[len(kpis)] = ['margen de contribucion', ratio, moneda]
    #print('margen', ratio)
    ingresos = extrapolacion * (contabilidad[contabilidad.concepto == 'importe neto de la cifra de negocios'].magnitud.values[0])
    kpis.loc[len(kpis)] = ['ventas', ingresos, moneda]
    #print('ventas', ratio)
    ratio = 365*contabilidad[contabilidad.concepto == 'deudores comerciales y otras cuentas a cobrar'].magnitud.values[0] / \
            ingresos / extrapolacion

    kpis.loc[len(kpis)] = ['PM cobro', ratio, 'días']
    #print('PM cobro', ratio)
    ratio = kpis[kpis.concepto == 'BN'].magnitud.values[0] / ingresos
    kpis.loc[len(kpis)] = ['BN/ventas', ratio, None]
    #print('BN/ventas', ratio)
    ratio = contabilidad[contabilidad.concepto == 'capital'].magnitud.values[0] / ingresos
    kpis.loc[len(kpis)] = ['capital/ventas', ratio, None]
    #print('capital/ventas', ratio)
    ratio = contabilidad[contabilidad.concepto == 'patrimonio neto'].magnitud.values[0] / ingresos
    kpis.loc[len(kpis)] = ['FFPP/ventas', ratio, None]
    #print('ffpp/ventas', ratio)
    # no se pueden calcular los ratios dependientes de deuda bancaria
    ratio = ingresos / kpis[kpis.concepto == 'DFB'].magnitud.values[0]

    kpis.loc[len(kpis)] = ['ventas/DFB', ratio, None]
    #print('ventas/dfb', ratio)
    ratio = ingresos / kpis[kpis.concepto == 'DFB C/P'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['ventas/DFB C/P', ratio, None]
    #print('ventas/dfbcp', ratio)
    ratio = ingresos / kpis[kpis.concepto == 'DFB L/P'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['ventas/DFB L/P', ratio, None]
    #print('ventas/dfblp', ratio)
    ratio = ingresos / contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['ventas/activo', ratio, None]
    #print('ventas/activo', ratio)
    ratio = ingresos / contabilidad[contabilidad.concepto == 'activo corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['ventas/activo corriente', ratio, None]
    #print('ventas/activocorriente', ratio)
    ratio = ingresos / contabilidad[contabilidad.concepto == 'activo no corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['ventas/activo no corriente', ratio, None]
    #print('ventas/activonocorriente', ratio)
    ratio = kpis[kpis.concepto == 'BN'].magnitud.values[0] / ingresos
    kpis.loc[len(kpis)] = ['BN/ventas', ratio, None]
    #print('bn/ventas', ratio)
    ratio = kpis[kpis.concepto == 'BN'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'capital'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BN/capital', ratio, None]
    #print('bn/capital', ratio)
    ratio = kpis[kpis.concepto == 'BN'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'patrimonio neto'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BN/FFPP', ratio, None]
    #print('bn/ffpp', ratio)
    ratio = kpis[kpis.concepto == 'BN'].magnitud.values[0] / \
            (contabilidad[contabilidad.concepto == 'patrimonio neto'].magnitud.values[0] -
             extrapolacion * contabilidad[contabilidad.concepto == 'resultado de la cuenta de perdidas y ganancias'].magnitud.values[0])
    kpis.loc[len(kpis)] = ['BN/(FFPP-RDO)', ratio, None]
    #print('algo', ratio)
    ratio = kpis[kpis.concepto == 'BN'].magnitud.values[0] / \
            kpis[kpis.concepto == 'DFB'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BN/DFB', ratio, None]
    #print('bn/dfb', ratio)
    ratio = kpis[kpis.concepto == 'BN'].magnitud.values[0] / \
            kpis[kpis.concepto == 'DFB C/P'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BN/DFB C/P', ratio, None]
    #print('bn/dfbcp', ratio)
    ratio = kpis[kpis.concepto == 'BN'].magnitud.values[0] / \
            kpis[kpis.concepto == 'DFB L/P'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BN/DFB L/P', ratio, None]
    #print('bn/dfblp', ratio)
    ratio = kpis[kpis.concepto == 'BN'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BN/activo', ratio, None]
    #print('bn/activo', ratio)
    ratio = kpis[kpis.concepto == 'BN'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'activo corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BN/activo corriente', ratio, None]
    #print('bn/activocorriente', ratio)
    ratio = kpis[kpis.concepto == 'BN'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'activo no corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BN/activo no corriente', ratio, None]
    #print('bn/activonocorriente', ratio)
    ratio = kpis[kpis.concepto == 'BAII'].magnitud.values[0] / ingresos
    kpis.loc[len(kpis)] = ['BAII/ventas', ratio, None]
    #print('baii/ventas', ratio)
    ratio = kpis[kpis.concepto == 'BAII'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'capital'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAII/capital', ratio, None]
    #print('baii/capital', ratio)
    ratio = kpis[kpis.concepto == 'BAII'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'patrimonio neto'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAII/FFPP', ratio, None]
    #print('baii/ffpp', ratio)
    ratio = kpis[kpis.concepto == 'BAII'].magnitud.values[0] / \
            kpis[kpis.concepto == 'DFB'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAII/DFB', ratio, None]
    #print('baii/dfb', ratio)
    ratio = kpis[kpis.concepto == 'BAII'].magnitud.values[0] / \
            kpis[kpis.concepto == 'DFB C/P'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAII/DFB C/P', ratio, None]
    #print('baii/dfbcp', ratio)
    ratio = kpis[kpis.concepto == 'BAII'].magnitud.values[0] / \
            kpis[kpis.concepto == 'DFB L/P'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAII/DFB L/P', ratio, None]
    #print('bai/dfblp', ratio)
    ratio = kpis[kpis.concepto == 'BAII'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAII/activo', ratio, None]
    #print('baii/activo', ratio)
    ratio = kpis[kpis.concepto == 'BAII'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'activo corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAII/activo corriente', ratio, None]

    ratio = kpis[kpis.concepto == 'BAII'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'activo no corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAII/activo no corriente', ratio, None]

    ratio = kpis[kpis.concepto == 'BAI'].magnitud.values[0] / ingresos
    kpis.loc[len(kpis)] = ['BAI/ventas', ratio, None]

    ratio = kpis[kpis.concepto == 'BAI'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'capital'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAI/capital', ratio, None]

    ratio = kpis[kpis.concepto == 'BAI'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'patrimonio neto'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAI/FFPP', ratio, None]

    ratio = kpis[kpis.concepto == 'BAI'].magnitud.values[0] / \
            kpis[kpis.concepto == 'DFB'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAI/DFB', ratio, None]

    ratio = kpis[kpis.concepto == 'BAI'].magnitud.values[0] / \
            kpis[kpis.concepto == 'DFB C/P'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAI/DFB C/P', ratio, None]

    ratio = kpis[kpis.concepto == 'BAI'].magnitud.values[0] / \
            kpis[kpis.concepto == 'DFB L/P'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAI/DFB L/P', ratio, None]

    ratio = kpis[kpis.concepto == 'BAI'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAI/activo', ratio, None]

    ratio = kpis[kpis.concepto == 'BAI'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'activo corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAI/activo corriente', ratio, None]

    ratio = kpis[kpis.concepto == 'BAI'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'activo no corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['BAI/activo no corriente', ratio, None]

    ratio = EBITDA / ingresos
    kpis.loc[len(kpis)] = ['EBITDA/ventas', ratio, None]

    ratio = EBITDA / contabilidad[contabilidad.concepto == 'capital'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['EBITDA/capital', ratio, None]

    ratio = EBITDA / contabilidad[contabilidad.concepto == 'patrimonio neto'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['EBITDA/FFPP', ratio, None]

    ratio = EBITDA / kpis[kpis.concepto == 'DFB'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['EBITDA/DFB', ratio, None]

    ratio = EBITDA / kpis[kpis.concepto == 'DFB C/P'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['EBITDA/DFB C/P', ratio, None]

    ratio = EBITDA / kpis[kpis.concepto == 'DFB L/P'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['EBITDA/DFB L/P', ratio, None]

    ratio = EBITDA / contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['EBITDA/activo', ratio, None]

    ratio = EBITDA / contabilidad[contabilidad.concepto == 'activo corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['EBITDA/activo corriente', ratio, None]

    ratio = EBITDA / contabilidad[contabilidad.concepto == 'activo no corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['EBITDA/activo no corriente', ratio, None]

    ratio = CASHFLOW / ingresos
    kpis.loc[len(kpis)] = ['cashflow/ventas', ratio, None]

    ratio = CASHFLOW / contabilidad[contabilidad.concepto == 'capital'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['cashflow/capital', ratio, None]

    ratio = CASHFLOW / contabilidad[contabilidad.concepto == 'patrimonio neto'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['cashflow/FFPP', ratio, None]

    ratio = CASHFLOW / kpis[kpis.concepto == 'DFB'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['cashflow/DFB', ratio, None]

    ratio = CASHFLOW / kpis[kpis.concepto == 'DFB C/P'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['cashflow/DFB C/P', ratio, None]

    ratio = CASHFLOW / kpis[kpis.concepto == 'DFB L/P'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['cashflow/DFB L/P', ratio, None]

    ratio = CASHFLOW / contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['cashflow/activo', ratio, None]

    ratio = CASHFLOW / contabilidad[contabilidad.concepto == 'activo corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['cashflow/activo corriente', ratio, None]

    ratio = CASHFLOW / contabilidad[contabilidad.concepto == 'activo no corriente'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['cashflow/activo no corriente', ratio, None]

    ratio = contabilidad[contabilidad.concepto == 'resultado del ejercicio procedente de operaciones continuadas'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'patrimonio neto'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['ROE', extrapolacion * ratio, None]

    ratio = kpis[kpis.concepto == 'BAII'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'total activo'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['ROA/ROI', ratio, None]

    ratio = contabilidad[contabilidad.concepto == 'aprovisionamientos'].magnitud.values[0] / 365
    kpis.loc[len(kpis)] = ['compras medias diarias sin iva', extrapolacion * ratio, moneda]

    ratio = contabilidad[contabilidad.concepto == 'aprovisionamientos'].magnitud.values[0] * 1.21 / 365
    kpis.loc[len(kpis)] = ['compras medias diarias con iva', extrapolacion * ratio, moneda]

    ratio = 365.0 * contabilidad[contabilidad.concepto == 'acreedores comerciales y otras cuentas a pagar'].magnitud.values[0] / \
            contabilidad[contabilidad.concepto == 'aprovisionamientos'].magnitud.values[0] / extrapolacion
    kpis.loc[len(kpis)] = ['PM proveedores', abs(ratio), 'días']

    ratio = contabilidad[contabilidad.concepto == "variacion de existencias de productos terminados y en curso de fabricacion"].magnitud.values[0] + \
            contabilidad[contabilidad.concepto == 'aprovisionamientos'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['coste de ventas', ratio, moneda]

    ratio = contabilidad[contabilidad.concepto == 'deudores comerciales y otras cuentas a cobrar'].magnitud.values[0] + \
            contabilidad[contabilidad.concepto == 'existencias'].magnitud.values[0] + \
            contabilidad[contabilidad.concepto == 'efectivo y otros activos liquidos equivalentes'].magnitud.values[0] - \
            contabilidad[contabilidad.concepto == 'acreedores comerciales y otras cuentas a pagar'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['NOF', ratio, moneda]

    ratio = kpis[kpis.concepto == 'NOF'].magnitud.values[0] - \
            kpis[kpis.concepto == 'fondo maniobra'].magnitud.values[0]
    kpis.loc[len(kpis)] = ['NRN', ratio, moneda]
    #print('NOF', ratio)
    ratio = ingresos / 365
    kpis.loc[len(kpis)] = ['ventas medias diarias sin iva', ratio, moneda]
    #print('sin iva', ratio)
    ratio = ingresos * 1.21 / 365
    kpis.loc[len(kpis)] = ['ventas medias diarias con iva', ratio, moneda]
    #print('con iva', ratio)
    kpis.loc[len(kpis)] = ['ventas/capital', 1.0 / kpis[kpis.concepto == 'capital/ventas'].magnitud.values[0], None]
    i=1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['ventas/FFPP', 1.0 / kpis[kpis.concepto == 'FFPP/ventas'].magnitud.values[0], None]
    i+=1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['DFB/ventas', 1.0 / kpis[kpis.concepto == 'ventas/DFB'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['DFB C/P/ventas', 1.0 / kpis[kpis.concepto == 'ventas/DFB C/P'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['DFB L/P/ventas', 1.0 / kpis[kpis.concepto == 'ventas/DFB L/P'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['activo/ventas', 1.0/kpis[kpis.concepto == 'ventas/activo'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['activo corriente/ventas',
                           1.0/kpis[kpis.concepto == 'ventas/activo corriente'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['activo no corriente/ventas',
                           1.0/kpis[kpis.concepto == 'ventas/activo no corriente'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['ventas/BN', 1.0 / kpis[kpis.concepto == 'BN/ventas'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)

    kpis.loc[len(kpis)] = ['capital/BN', 1.0 / kpis[kpis.concepto == 'BN/capital'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)

    kpis.loc[len(kpis)] = ['FFPP/BN', 1.0 / kpis[kpis.concepto == 'BN/FFPP'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['(FFPP-RDO)/BN', 1.0 / kpis[kpis.concepto == 'BN/(FFPP-RDO)'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['DFB/BN', 1.0 / kpis[kpis.concepto == 'BN/DFB'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['DFB C/P/BN', 1.0 / kpis[kpis.concepto == 'BN/DFB C/P'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['DFB L/P/BN', 1.0 / kpis[kpis.concepto == 'BN/DFB L/P'].magnitud.values[0], None]
    i += 1
    #print(i, ratio)
    kpis.loc[len(kpis)] = ['activo/BN', 1.0 / kpis[kpis.concepto == 'BN/activo'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo corriente/BN',
                           1.0 / kpis[kpis.concepto == 'BN/activo corriente'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo no corriente/BN',
                           1.0 / kpis[kpis.concepto == 'BN/activo no corriente'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['ventas/BAII', 1.0 / kpis[kpis.concepto == 'BAII/ventas'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['capital/BAII', 1.0 / kpis[kpis.concepto == 'BAII/capital'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['FFPP/BAII', 1.0 / kpis[kpis.concepto == 'BAII/FFPP'].magnitud.values[0], None]


    kpis.loc[len(kpis)] = ['DFB/BAII', 1.0 / kpis[kpis.concepto == 'BAII/DFB'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['DFB C/P/BAII',
                           1.0 / kpis[kpis.concepto == 'BAII/DFB'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['DFB L/P/BAII',
                           1.0 / kpis[kpis.concepto == 'BAII/DFB L/P'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo/BAII',
                           1.0 / kpis[kpis.concepto == 'BAII/activo'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo corriente/BAII',
                           1.0 / kpis[kpis.concepto == 'BAII/activo corriente'].magnitud.values[0], None]
    kpis.loc[len(kpis)] = ['activo no corriente/BAII',
                           1.0 / kpis[kpis.concepto == 'BAII/activo no corriente'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['ventas/BAI',
                           1.0 / kpis[kpis.concepto == 'BAI/ventas'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['capital/BAI',
                           1.0 / kpis[kpis.concepto == 'BAI/capital'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['FFPP/BAI',
                           1.0 / kpis[kpis.concepto == 'BAI/FFPP'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['DFB/BAI',
                           1.0 / kpis[kpis.concepto == 'BAI/DFB'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['DFB C/P/BAI',
                           1.0 / kpis[kpis.concepto == 'BAI/DFB C/P'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['DFB L/P/BAI',
                           1.0 / kpis[kpis.concepto == 'BAI/DFB L/P'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo/BAI',
                           1.0 / kpis[kpis.concepto == 'BAI/activo'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo corriente/BAI',
                           1.0 / kpis[kpis.concepto == 'BAI/activo corriente'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo no corriente/BAI',
                           1.0 / kpis[kpis.concepto == 'BAI/activo no corriente'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['ventas/EBITDA',
                           1.0 / kpis[kpis.concepto == 'EBITDA/ventas'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['capital/EBITDA',
                           1.0 / kpis[kpis.concepto == 'EBITDA/capital'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['FFPP/EBITDA',
                           1.0 / kpis[kpis.concepto == 'EBITDA/FFPP'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['DFB C/P/EBITDA', 1.0 / 
                                            kpis[kpis.concepto == 'EBITDA/DFB C/P'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['DFB L/P/EBITDA',
                           1.0 / kpis[kpis.concepto == 'EBITDA/DFB L/P'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo/EBITDA',
                           1.0 / kpis[kpis.concepto == 'EBITDA/activo'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo corriente/EBITDA',
                           1.0 / kpis[kpis.concepto == 'EBITDA/activo corriente'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo no corriente/EBITDA',
                           1.0 / kpis[kpis.concepto == 'EBITDA/activo no corriente'].magnitud.values[0], None]
    kpis.loc[len(kpis)] = ['ventas/cashflow',
                           1.0 / kpis[kpis.concepto == 'cashflow/ventas'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['capital/cashflow',
                           1.0 / kpis[kpis.concepto == 'cashflow/capital'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['FFPP/cashflow',
                           1.0 / kpis[kpis.concepto == 'cashflow/FFPP'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['cashflow/DFN',
                           1.0 / kpis[kpis.concepto == 'DFN/cashflow'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['DFB C/P/cashflow',
                           1.0 / kpis[kpis.concepto == 'cashflow/DFB C/P'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['DFB L/P/cashflow',
                           1.0 / kpis[kpis.concepto == 'cashflow/DFB L/P'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo/cashflow',
                           1.0 / kpis[kpis.concepto == 'cashflow/activo'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo corriente/cashflow',
                           1.0 / kpis[kpis.concepto == 'cashflow/activo corriente'].magnitud.values[0], None]

    kpis.loc[len(kpis)] = ['activo no corriente/cashflow',
                           1.0 / kpis[kpis.concepto == 'cashflow/activo no corriente'].magnitud.values[0], None]
    #print(kpis)
    return kpis