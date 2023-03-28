import json
import math
import sys
from datetime import datetime, date
from math import nan

import pandas
import unidecode
import pandas as pd
import numpy as np
import os

from core import dictionaries
from core.dictionaries import renombrar, conceptosTotal, conceptosAbreviado200, conceptosTotalOriginal, \
    conceptosAbreviado
from core.models import Contabilidad, Ratio, Empresa, Extracciones_Errores, Analitica
from rest_framework import status
from rest_framework.response import Response

from sources.ratios import ratios_verticales

pd.set_option('display.max_columns', None)
pd.set_option('display.max_rows', None)

concepts_of_interest = ['00101', '00102', '00111', '00115', '00118', '00126', '00134', '00135', '00136', '00137', '00138',
                        '00149', '00150', '00158', '00159', '00160', '00168', '00176', '00177', '00180', '00185', '00186',
                        '00187', '00190', '00191', '00194', '00195', '00198', '00199', '00200', '00201', '00202', '00207',
                        '00208', '00209', '00210', '00211', '00216', '00218', '00219', '00222', '00223', '00224', '00225',
                        '00226', '00227', '00228', '00229', '00230', '00231', '00233', '00234', '00237', '00238', '00239',
                        '00250', '00251', '00252', '00255', '00258', '00259', '00260', '00265', '00270', '00279', '00284',
                        '00285', '00286', '00287', '00294', '00295', '00296', '00297', '00305', '00309', '00312', '00313',
                        '00329', '00326', '00327', '00500']

concepts_of_interest_activo = concepts_of_interest[:concepts_of_interest.index('00185')]
concepts_of_interest_pasivo = concepts_of_interest[concepts_of_interest.index('00185'):concepts_of_interest.index('00255')]
concepts_of_interest_pyg = concepts_of_interest[concepts_of_interest.index('00255'):]

# concepts_of_interest = [101, 102, 111, 115, 118, 134, 136, 137, 138, 149, 160, 168, 176, 177, 180, 185, 186, 187, 190, 191,
#                        194, 195, 198, 199, 200, 201, 202, 208, 209, 210, 211, 216, 223, 224, 225, 226, 227, 228, 229, 230,
#                        231, 238, 239, 250, 251, 252, 255, 258, 259, 260, 265, 270, 279, 284, 285, 286, 287, 294, 295, 296]

columns_names = ['concepto', 'codigo', 'magnitud']


def reArrangeText(text):
    tails = ["(P)", "P)", "(A,", "A,", "(N,", "(N)", "A)", "A", " ", ""]
    chunks = text.split(" ")
    i = len(chunks)
    # print(chunks, " ", i)
    while chunks[i - 1] in tails:
        chunks = chunks[0: i - 1]
        # print(chunks)
        i = len(chunks)
    finalText = " ".join(chunks)
    #for i in range(len(chunks) - 1):
    #    finalText = finalText + chunks[i] + " "

    #finalText += chunks[len(chunks) - 1]
    finalText = unidecode.unidecode(finalText)
    finalText = finalText.lower()
    if finalText[0] == '(': finalText = finalText[1:]
    if finalText[-1] == ')': finalText = finalText[:-1]
    # print(finalText)
    return finalText

def reverseReArrangeText(text):
    heads = ["(p)", "p)", "(a,", "a,", "a", "(n,", "(n)", "a)", " ", ""]
    chunks = text.split(" ")
    i = len(chunks)
    #print(chunks, " ", i)
    while chunks[0] in heads:
        chunks = chunks[1:]
        # print(chunks)

    finalText = " ".join(chunks)
    finalText = unidecode.unidecode(finalText).lower()
    if finalText[0] == '(' or finalText[0] == ')': finalText = finalText[1:]
    if finalText[-1] == ')' or finalText[-1] == '(': finalText = finalText[:-1]
    # print(finalText)
    return finalText


def checkText(outputString, lack):
    outputString = reArrangeText(outputString)
    if len(outputString.split('(n')) > 1:
        split = outputString.split('(n')
        initial = reArrangeText(split[0])
        lack = outputString.replace(initial, '')
        outputString = initial
        lack = reverseReArrangeText(lack)
    elif len(outputString.split('(a')) > 1:
        split = outputString.split('(a')
        lack = reArrangeText(split[0])
        initial = outputString.replace(lack, '')
        outputString = initial
        lack = reverseReArrangeText(lack)
    elif len(outputString.split('(p')) > 1:
        split = outputString.split('(p')
        initial = reArrangeText(split[0])
        lack = reverseReArrangeText(outputString.replace(initial, ''))
        outputString = initial
    return outputString, lack

base_path = 'C:/Alia-Tecnocim/Modelo200/'

def selector2019(size, iterator, row):

    data = []
    new_row = {}
    previous = row.Text.strip()
    initial, lack = checkText(previous, '')
    new_row['concepto'] = initial
    # columna_previa = 'inicio'
    do = True

    if size > 0:
        for index, row in iterator:
            outputString = row.Text
            #if str(outputString).strip() == '00295':
            #    a = input('pausa')
            #    print(a)
            #print(outputString, pd.isna(outputString))
            if not pd.isna(outputString):
                outputString = outputString.strip()
                #print('Diferente de (N): ', outputString[:3] != '(N)')
                if outputString[:3] != '(N)':
                    outputFloat = outputString.replace('.', '')
                    outputFloat = outputFloat.replace(',', '.')
                    # probar a ver si se puede transformar el string en float
                    try:
                        float(outputFloat)
                        #print('Ha pasado el try')
                        conversion = True
                    except:
                        conversion = False

                    if len(outputString.split(' ')[-1]) < 4:
                        ## FALLA SI HAY CANTIDADES MENORES DE 10€. NO ES RELEVANTE PERO ES PUEDE VER SI HAY ALTERNATIVA
                        # si la row anterior no tenía Cantidad, entonces habrá que cerrar la row y salvarla
                        # para ello comprobamos si el elemento anterior a esta iteración era un código (int)
                        try:
                            a = int(outputString)
                            if a < 20:
                                do = False
                        except:
                            do = True
                        if do:
                            try:
                                int(previous)
                                #print('Ha pasado el int')
                                # si estamos tratando un concepto y el anterior era un código, hay que salvar la fila
                                # e iniciar una nueva con el nuevo concepto
                                new_row['magnitud'] = '0'
                                data.append(new_row)
                                #print(f'\nFila sin cantidad: {new_row}')
                                new_row = {}
                            except:
                                pass
                            outputString, lack = checkText(outputString, lack)
                            #outputString = outputString.replace('(', '').replace(')', '')
                            # una vez salvada e inicializada la nueva fila, le asignamos el concepto detectado
                            new_row['concepto'] = outputString
                            previous = outputString
                        do = True
                        # si el texto no cumple las condiciones anteriores y su primer caracter es '0', se trata de un código
                    elif outputString[0] == '0':
                        if previous[0] == '0':
                            data.append({'concepto': lack, 'codigo': outputString, 'magnitud': 0})
                        else:
                            new_row['codigo'] = outputString
                        previous = outputString
                    # si no cumple las condiciones anteriores pero se había convertido con éxito a float, se trata de una cantidad
                    elif conversion:
                        new_row['magnitud'] = outputFloat
                        # tras una cantidad, siempre hay que salvar fila y reiniciar el proceso
                        data.append(new_row)
                        #print(f'\nFila con cantidad: {new_row}')
                        new_row = {}
                        previous = outputString
                    # guardamos el texto de esta iteración, porque lo podemos necesitar en la siguiente para ciertas comprobaciones
                    #previous = outputString
    else:
        #print('\nNo se ha detectado información\n')
        return False

    #data.append(new_row)

    return data

def selector2020(size, iterator, row, codigos):

    data = []
    new_row = {}
    errores = []
    previous = row.Text.strip()
    initial, lack = checkText(previous, '')
    new_row['concepto'] = initial

    if size > 0:
        for index, row in iterator:
            outputString = row.Text
            #print('Principio: ', outputString)
            # if str(outputString).strip() == '00295':
            #    a = input('pausa')
            #    print(a)
            # print(outputString, pd.isna(outputString))
            if not pd.isna(outputString) and len(outputString) > 2 and 'modelo' not in outputString.lower():
                if outputString[:3] != '(N)':
                    if outputString == 'Reserva de revalorización (Ley 16/2012, de 27 de diciembre) (N) ':
                        outputString = 'Reserva de revalorización'
                        #print(outputString)
                    outputString = outputString.strip().split(' ')
                    #print('Principio: ', outputString)
                    new_output = outputString
                    for element in outputString:
                        if element in codigos:
                            #if element == '00500':
                            #    a = input('pausa')
                            #    print(a)
                            if 'codigo' in new_row.keys():
                                if 'concepto' in new_row.keys():
                                    data.append(new_row)
                                    new_row = {}
                                else:
                                    new_row['concepto'] = lack
                            new_row['codigo'] = element
                            # previous = element
                            new_output = [piece for piece in outputString if piece != element]
                            #print(element)
                        else:
                            new_element_float = element.replace('.', '')
                            new_element_float = new_element_float.replace(',', '.')

                            try:
                                float(new_element_float)
                                # if len(new_element_float)>2:
                                if 'magnitud' in new_row.keys():
                                    data.append(new_row)
                                    new_row = {}
                                new_row['magnitud'] = new_element_float
                                new_output = [piece for piece in new_output if piece != element]
                                #print(element)
                            except:
                                pass

                    outputString = ' '.join(new_output)
                    if len(outputString) > 0:
                        outputString, lack = checkText(outputString, lack)
                        #print(f"'{outputString}', ")
                        #print(f'Final : {outputString in conceptosTotal} {"reserva de valorizacion" in outputString}\n')
                        if 'reserva de revalorizacion' in outputString:
                            outputString = 'reserva de revalorizacion'
                        if outputString == 'personal (remuneraciones pendientes de pago':
                            outputString = 'personal (remuneraciones pendientes de pago)'
                    if outputString in conceptosTotalOriginal:
                        if 'concepto' in new_row.keys():
                            data.append(new_row)
                            if new_row['concepto'] == 'resultado de la cuenta de perdidas y ganancias': break
                            new_row = {}
                        new_row['concepto'] = outputString
                        if lack in conceptosTotalOriginal:
                            errores.append(lack)

    else:
        #print('\nNo se ha detectado información\n')
        return False

    data.append(new_row)

    return data, errores

def analitica_modelo200(resultados, doc):

    extraccion = doc.extraccion
    data = resultados.loc[resultados['codigo'] == '00287', 'magnitud'].item()
    #print('Data: ', data)
    if data > 0:
        Analitica.objects.update_or_create(cuenta='beneficios extraordinarios', documento=doc,
                                           defaults={'magnitud': data, 'extraccion': extraccion})
    else:
        Analitica.objects.update_or_create(cuenta='extraordinarios', documento=doc,
                                           defaults={'magnitud': data, 'extraccion': extraccion})

    data = resultados.loc[resultados['codigo'] == '00294', 'magnitud'].item()
    if data > 0:
        try:
            beneficios = Analitica.objects.get(cuenta='beneficios extraordinarios', documento=doc).magnitud
            beneficios += data
        except:
            beneficios = data
        Analitica.objects.update_or_create(cuenta='beneficios extraordinarios', documento=doc,
                                           defaults={'magnitud': beneficios, 'extraccion': extraccion})
    else:
        try:
            extraordinarios = Analitica.objects.get(cuenta='extraordinarios', documento=doc).magnitud
            extraordinarios += data
        except:
            extraordinarios = data
        Analitica.objects.update_or_create(cuenta='extraordinarios', documento=doc,
                                           defaults={'magnitud': extraordinarios, 'extraccion': extraccion})

    data = resultados.loc[resultados['codigo'] == '00295', 'magnitud'].item()
    if data > 0:
        try:
            beneficios = Analitica.objects.get(cuenta='beneficios extraordinarios', documento=doc).magnitud
            beneficios += data
        except:
            beneficios = data
        Analitica.objects.update_or_create(cuenta='beneficios extraordinarios', documento=doc,
                                           defaults={'magnitud': beneficios, 'extraccion': extraccion})
    else:
        try:
            extraordinarios = Analitica.objects.get(cuenta='extraordinarios', documento=doc).magnitud
            extraordinarios += data
        except:
            extraordinarios = data
        Analitica.objects.update_or_create(cuenta='extraordinarios', documento=doc,
                                           defaults={'magnitud': extraordinarios, 'extraccion': extraccion})

    for element in dictionaries.analitica.keys():

        if element == 'ventas':
            data = resultados.loc[resultados['codigo'] == '00255', 'magnitud'].item()
        elif element == 'consumo':
            data = resultados.loc[resultados['codigo'] == '00260', 'magnitud'].item() + \
                   resultados.loc[resultados['codigo'] == '00258', 'magnitud'].item()
        elif element == 'trabajos':
            data = resultados.loc[resultados['codigo'] == '00263', 'magnitud'].item()
        elif element == 'deterioros':
            data = resultados.loc[resultados['codigo'] == '00264', 'magnitud'].item()
        elif element == 'otros ingresos de gestion':
            data = resultados.loc[resultados['codigo'] == '00265', 'magnitud'].item()
        elif element == 'personal':
            data = resultados.loc[resultados['codigo'] == '00270', 'magnitud'].item()
        elif element == 'exteriores':
            data = resultados.loc[resultados['codigo'] == '00280', 'magnitud'].item() + \
                   resultados.loc[resultados['codigo'] == '00709', 'magnitud'].item()
        elif element == 'tributos':
            data = resultados.loc[resultados['codigo'] == '00281', 'magnitud'].item()
        elif element == 'extraordinarios':
            data = resultados.loc[resultados['codigo'] == '00282', 'magnitud'].item()
        elif element == 'otros gastos de gestion':
            data = resultados.loc[resultados['codigo'] == '00283', 'magnitud'].item()
        elif element == 'amortizaciones':
            data = resultados.loc[resultados['codigo'] == '00284', 'magnitud'].item()
        elif element == 'subvenciones':
            data = resultados.loc[resultados['codigo'] == '00285', 'magnitud'].item()
        elif element == 'exceso de deterioros':
            data = resultados.loc[resultados['codigo'] == '00286', 'magnitud'].item()
        elif element == 'ingresos financieros':
            data = resultados.loc[resultados['codigo'] == '00297', 'magnitud'].item()
        elif element == 'financieros':
            data = resultados.loc[resultados['codigo'] == '00305', 'magnitud'].item()
        elif element == 'baii':
            data = resultados.loc[resultados['codigo'] == '00296', 'magnitud'].item()
        elif element == 'bai':
            data = resultados.loc[resultados['codigo'] == '00325', 'magnitud'].item()
        elif element == 'impuesto de sociedades':
            data = resultados.loc[resultados['codigo'] == '00326', 'magnitud'].item()
        elif element == 'beneficio neto':
            data = resultados.loc[resultados['codigo'] == '00500', 'magnitud'].item()
        else:
            data = False

        if data or data == 0:
            Analitica.objects.update_or_create(cuenta=element, documento=doc, defaults={'magnitud': data, 'extraccion': extraccion})

    for key in dictionaries.analiticaAgregados.keys():
        cantidad = 0
        for element in dictionaries.analiticaAgregados[key]:
            #print(f'Agregados - key {key} concepto {element}')
            try:
                cantidad += Analitica.objects.get(cuenta=element, documento=doc).magnitud
            except:
                return element
        Analitica.objects.update_or_create(cuenta=key, documento=doc, defaults={'magnitud': cantidad, 'extraccion': extraccion})

    return False


# load json file and save table
def modelo200(file, doc, extraccion):
    # base_path = 'C:/Users/Pablo/IdeaProjects/pdfservices-java-sdk-samples/output/Modelo200/2020'
    # comprobar si existe ya un fichero json del Modelo200; borrarlo por si es uno antiguo; descomprimir el que nos interesa
    # file = base_path + '/structuredData.json'
    if os.path.isfile(file):
        # cargar datos del json
        with open(file, 'r', encoding='utf8') as f:
            data = json.load(f)
    else:
        return ({'error': f'\nFichero {file} no se localiza. Revisar directorio y nombre del fichero\n'}, status.HTTP_400_BAD_REQUEST), False
        #a = input('Presione cualquier tecla para finalizar')
        #quit()

    # conservar el campo 'elements'
    elements = data['elements']
    # determinar los campos a conservar dentro de 'elements'
    keep = ['Bounds', 'Page', 'Text']

    # deshacerse de los valores nulos    df = pd.DataFrame.from_records(elements)[keep].dropna()
    df = pd.DataFrame.from_records(elements)
    df = df[keep]
    # detección automática de ejercicio y CIF; no se usa por ahora

    #print(df['Bounds'].str[0], type(df['Bounds'].str[0]))
    #print(df['Bounds'].str[0].map(lambda x: math.floor(x) if not np.isnan(x) else None) == 529.0)
    try:
        ejercicio = df.loc[df[df['Bounds'].str[0].map(lambda x: math.floor(x) if not np.isnan(x) else None) == 529.0].index, 'Text'].values[0]
        #sys.stdout.write(f'Ejercicio: {ejercicio}')
    except:
        #sys.stdout.write('No se ha localizado el ejercicio')
        ejercicio = 2100
    #print(ejercicio)
    #NIF = df.loc[df[df['Bounds'].str[0] == 31.82000732421875].index, 'Text'].values[0] # No funciona, el Bound puede variar
    #print('NIF: ', NIF)
    # filtramos las páginas innecesarias; es un método menos robusto que con java (DeletePages) pero es que el SDK en python
    # no ofrece la utilidad de borrar páginas; si cambia el formato de los pdfs del modelo 200, tendrá impacto en este punto
    df = df[df.Page > 2]
    codigos = ['00500', '00760', '00761', '00762', '00763', '01001', '01002']
    codigos += ['00' + str(i) for i in range(101, 333)]
    codigos += ['00' + str(i) for i in range(700, 713)]
    if int(ejercicio) < 2020:
        eliminate = ['00181', '00182', '00183', '00184', '00253', '00254', '00272', '00712']
        codigos = [element for element in codigos if element not in eliminate]
        df = df[df.Page < 9]
    else:
        # codigos = set(codigos)
        eliminate = ['00181', '00182', '00183', '00184', '00272']
        codigos = [element for element in codigos if element not in eliminate]
        df = df[df.Page < 10]

    #sys.stdout.write(df.to_string())
    #a = input('Pausa tras display')
    iterator = df.iterrows()
    # iterar las primeras filas hasta encontrar la información relevante; probablemente se puede eliminar porque el
    # proceso iterativo de construcción del df ya lo tiene en cuenta
    while True:
        index, row = next(iterator)
        #print('Bucle inicial: ', index, row)
        try:
            if 'ACTIVO NO CORRIENTE (N, A, P)' in row.Text.strip(): break
        except:
            continue
    # while previous == np.nan: next(iterator)

    errores = []

    if int(ejercicio) <= 2019:
        try:
            data = selector2019(df.size, iterator, row)
            if not data:
                return {'extraccion': extraccion, 'mensaje': 'pdf defectuoso; enviar a desarrollo', 'bloqueo': 1}, False
        except Exception as e:
            return {'extraccion': extraccion, 'mensaje': 'Fallo en selector2019', 'traza': f'{e}', 'bloqueo': 1}, False
    else:
        try:
            data, errores = selector2020(df.size, iterator, row, codigos)
            if not data:
                return {'extraccion': extraccion, 'mensaje': 'pdf defectuoso; enviar a desarrollo', 'bloqueo': 1}, False
        except Exception as e:
            return {'extraccion': extraccion, 'mensaje': 'Fallo en selector2020', 'traza': f'{e}', 'bloqueo': 1}, False

    resultados = pd.DataFrame(data)
    resultados.magnitud = pd.to_numeric(resultados.magnitud)
    resultados.magnitud = resultados.magnitud.fillna(0)
    #print(resultados)
    for index, value in resultados.iterrows():
        try:
            concepto = conceptosAbreviado200[value.concepto]
            if value.codigo != concepto:
                return {'extraccion': extraccion, 'mensaje': f'Fallo en alineación código-concepto: {value.concepto}: {value.codigo}',
                        'tabla': 'Contabilidad', 'bloqueo': 1}, False
        except:
            pass
    #print('Errores: ', errores)

    for key, value in renombrar.items():
        posicion = resultados[resultados.codigo == key].index.tolist()
        if posicion: resultados.at[posicion[0], 'concepto'] = value

    for element in conceptosTotal:
        if element not in resultados.concepto.tolist():
            errores.append(element)

    for concepto in conceptosAbreviado:
        for element in conceptosAbreviado[concepto].keys():
            if element not in resultados.concepto.tolist():
                doc.delete()
                extraccion_error = Extracciones_Errores(**{'extraccion': extraccion,
                                                           'mensaje': f'Falta como mínimo el {element}; enviar a desarrollo',
                                                           'bloqueo': 1})
                extraccion_error.save()
                #print(f'falta como mínimo el {element}; enviar pdf a desarrollo')
                return ({'error': f'falta como mínimo el {element}; enviar pdf a desarrollo'},
                        status.HTTP_500_INTERNAL_SERVER_ERROR), False

    for element in codigos:
        if element not in resultados.codigo.tolist():
            errores.append(element)

    for index, row in resultados.iterrows():
        if type(row.magnitud) != float:
            errores.append(row)

    for key, values in dictionaries.modelo200Extra.items():
        data = 0
        #print('Bucle: ', key, values)
        for codigo in values:
            try:
                #print(f'Cuenta {codigo}: {resultados[resultados.codigo == codigo].magnitud.values[0]}')
                data += resultados[resultados.codigo == codigo].magnitud.values[0]
                #print('Cantidad acumulada: ', data)
            except Exception as e:
                extraccion_error = Extracciones_Errores(**{'extraccion': extraccion,
                                                           'mensaje': f'Probable error con el código: {codigo}',
                                                           'traza': f'{e}', 'bloqueo': 3})
                extraccion_error.save()
                #print(f'Probable error con el código: {codigo}')
                errores.append(codigo)
        resultados.loc[len(resultados)] = [key, '', round(data, 2)]
        #print(f'Resultado final para {key} = {data}')

    resultados.magnitud = resultados.magnitud.fillna(0)
    #print('Contabilidad: ', resultados)
    try:
        for i, row in resultados.iterrows():
            #pass
            #print(row)
            Contabilidad.objects.update_or_create(concepto=row.concepto, documento=doc, codigo=row.codigo,
                                                  defaults={'magnitud': row.magnitud, 'extraccion': extraccion})
    except Exception as e:
        #print(f'Error salvando ratio {row}')
        return {'extraccion': extraccion, 'mensaje': f'Error salvando concepto {row}', 'traza': f'{e}',
                'tabla': 'Contabilidad', 'campo': f'{row}', 'bloqueo': 1}, False

    #sys.stdout.write(resultados.to_string())
    #sys.stdout.write(', '.join(errores))

    desired_width = 1000
    pd.set_option('display.width', desired_width)
    np.set_printoptions(linewidth=desired_width)
    pd.set_option('display.max_columns', 15)

    try:
        result = analitica_modelo200(resultados, doc)
        #print(result)
        if result:
            # print(f'Error salvando ratio {row}')
            return {'extraccion': extraccion, 'mensaje': f'Error salvando analítica a partir de modelo 200 por query a {result}',
                     'tabla': 'Analitica', 'bloqueo': 1}, False
    except Exception as e:
        # print(f'Error salvando ratio {row}')
        return {'extraccion': extraccion, 'mensaje': f'Error salvando analítica a partir de modelo 200', 'traza': f'{e}',
                 'tabla': 'Analitica', 'bloqueo': 1}, False

    contabilidad = resultados.drop(['codigo'], axis=1)
    try:
        kpis = ratios_verticales(contabilidad, doc.fecha)
    except Exception as e:
        return {'extraccion': extraccion, 'mensaje': 'Error calculando los ratios verticales (función ratios_verticales())',
                'traza': f'{e}', 'bloqueo': 1}, False

    for i, row in kpis.iterrows():
        try:
            Ratio.objects.update_or_create(documento=doc, concepto=row.concepto,
                                           defaults={'magnitud': round(row.magnitud, 4), 'unidades': row.unidades, 'extraccion': extraccion})
        except Exception as e:
            try:
                field = ' '.join(row.tolist)
            except Exception as e:
                field = 'no-identificado'
            return {'extraccion': extraccion, 'mensaje': 'Error salvando los ratios verticales (función modelo200)',
                    'traza': f'{e}', 'bloqueo': 1, 'tabla': 'Ratios', 'campo': field}, False

    doc.status = True
    doc.save()

    """
    print('Antes: ', doc.empresa.status)
    setattr(doc.empresa, 'status', doc.empresa.status + 1)
    doc.empresa.save()
    #empresa = Empresa.objects.get(CIF=doc.empresa.CIF)
    #setattr(empresa, 'status', empresa.status + 1)
    #empresa.save()
    print('Después: ', doc.empresa.status)
    print('Después con query:', Empresa.objects.get(CIF=doc.empresa.CIF).status)
    """
    #sys.setrecursionlimit(1500)

    """
    new_list = []
    new_dict = {}
    print('Resultados: ', resultados)
    for index, element in resultados.iterrows():
        element['fecha'] = element.documento.fecha
        element.drop(['documento'], inplace=True)
        new_list.append(element.to_dict())
    print(new_list)

    resultados.documento = resultados.documento[0].fecha
    kpis.documento = kpis.documento[0].fecha
    kpis.codigo = None
    union = pd.concat([resultados, kpis])
    union = union.reset_index(drop=True)
    print(union)
    """
    #print(resultados.to_dict('records'))
    return errores, True


if __name__ == '__main__':
    # file = "C:\\Alia-Tecnocim\\Modelo200\\2020 MIRALBUENO PRODUCTS Mod 200 Periodo 0A.pdf"
    # year = 2020

    # status, file = extract_txt_from_pdf.extractJsonFromPDF(file)
    # file = "C:\\Alia-Tecnocim\\Modelo200\\2020 MIRALBUENO PRODUCTS Mod 200 Periodo 0A.json"
    # file = "C:\\Users\\Pablo\\OneDrive\\Tecnocim\\Ficheros\\HistorialTecnocim\\TEC 200 2019 DEF.json"
    # year = 2019

    #name = "C:\\Users\\Pablo\\SmartDebt\\app\\documents\\F50011048\\Modelo200\\200_2019_BSV_2019-12-31.json"
    name = "C:\\Users\\Pablo\\SmartDebt\\app\\documents\\F50011048\\Modelo200\\200_BSV_2021_2021-12-31.json"
    f = open(name, "r")
    #file = ContentFile(bytes(f.read(), encoding='UTF-8'), name=os.path.basename(f.name))
    #fecha = datetime(2019, 12, 31)
    from core.models import Documento
    doc = Documento.objects.all().first()
    resultados = modelo200(f, doc, 14)
    """
    resultados = resultados[['concepto', 'codigo', 'magnitud']]
    print(resultados)
    file = os.path.dirname(file) + '/output.xlsx'
    if os.path.isfile(file):
        os.remove(file)

    resultados.to_excel(file)
    """