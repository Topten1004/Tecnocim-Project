import core.dictionaries as dictionaries
import pandas as pd

from core.models import Analitica, Ratio


def contabilidad_por_cuentas(df):
    pd.set_option('display.max_columns', None)
    pd.set_option('display.max_rows', None)
    desired_width = 2000
    pd.set_option('display.width', desired_width)

    # ordenar las cuentas, las de pocos dígitos quedarían en las primeras filas del df pero ya se han filtrado, es
    # un paso probablemente innecesario pero por ahora no se elimina
    df.sort_values(by=['cuenta'], inplace=True)
    #print('\nInicio de cálculos de contabilidad analítica\n'
    #      'Tener en cuenta que no se comprueba si los resultados del ejercicio se han pasado a la cuenta 129, puede generar desequilibrios\n')

    # crear df vacío para el almacenamiento de resultados
    contabilidad = pd.DataFrame(columns=['cuenta', 'magnitud'])
    #contabilidad.flags.allows_duplicate_labels = False

    # coger todas las llaves de la contabilidad analítica e iterar
    for key in dictionaries.analitica.keys():
        # el total de la cuenta se pone a 0 inicialmente
        cantidad = 0
        # el intervalo de cuentas que hay que agregar para tener la cantidad total asociada a un cuenta
        # se modela como el número de cuenta inicial y cuántos más números tendremos; ej. [600,9] quiere decir de la
        # cuenta 600 a la 609
        cuentas = dictionaries.analitica[key]
        # iteramos para cada cuenta
        for x in cuentas:
            if len(str(x)) == 2:
                elements = [x*10+i for i in range(10)]
            else:
                elements = [str(x)]
            for element in elements:
                # se realiza agregación de cuentas para generar cuentas de 3 dígitos
                length = len(str(element))
                #print(element)
                #print(element, " ", type(element), " ", length)
                # añadimos columna adicional en la que el número de cuenta son solo los tres primeros dígitos
                df['CuentaTemp'] = [str(cuenta)[0:length] for cuenta in df.cuenta]
                # se suman todos los saldos de las cuentas que tengan los tres primero dígitos iguales
                cuenta = df[df['CuentaTemp'] == str(element)]['magnitud'].sum()
                # se suma esa cuenta a las previas
                cantidad += cuenta
                # se añade una nueva fila al dataframe con el saldo de la cuenta de tres dígitos
                contabilidad.loc[len(contabilidad)] = [str(element), cuenta]
            # se añade el cuenta contable (grupo de cuentas de tres dígitos) al dataframe
            # se añade el concepto que representa el intervalo de cuentas de tres dígitos que se agregan en un único concepto
        contabilidad.loc[len(contabilidad)] = [key, cantidad]
    #print('Construida Contabilidad analítica')
    # se elimina la columna de tres dígitos una vez cumplida su función
    df.drop(labels='CuentaTemp', axis=1, inplace=True)
    # nueva iteración con los nuevos datos para generar cuentas agregados
    for key in dictionaries.analiticaAgregados.keys():
        cantidad = 0
        for element in dictionaries.analiticaAgregados[key]:
            cantidad += float(contabilidad[contabilidad.cuenta == element]['magnitud'])
        #new_row = {'Concepto': key, 'Cantidad': cantidad}
        contabilidad.loc[len(contabilidad)] = [key, cantidad]
    """
    # cálculo de totales
    totalGastos = float(contabilidad[contabilidad.cuenta == 'total gastos']['magnitud'])
    totalVentas = float(contabilidad[contabilidad.cuenta == 'total ventas']['magnitud'])
    totalIngresos = float(contabilidad[contabilidad.cuenta == 'total ingresos']['magnitud'])

    # cálculo de todas las cantidades versus los totales
    contabilidad['Relativo a Gastos (%)'] = contabilidad.magnitud / totalGastos * 100
    contabilidad['Relativo a Ventas (%)'] = contabilidad.magnitud / totalVentas * 100
    contabilidad['Relativo a Ingresos (%)'] = contabilidad.magnitud / totalIngresos * 100
    """

    return contabilidad