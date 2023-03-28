import datetime
import inspect
import os
from operator import itemgetter

import pandas as pd
import math
import numpy as np
from setuptools import glob

from sources.defaultDir import defaultDir
from equivalencias.models import Tipo, NatInterv, SolCol, Moneda, Real, Personal, SituOper, Plazo, Entidad, Producto
from core.models import Cirbe, Contrato, Pool, Extracciones_Errores


def handle_uploaded_file(f):
    with open(defaultDir+f.name, 'wb+') as destination:
        for chunk in f.chunks():
            destination.write(chunk)
    return destination

def checkDir(dirname):
    if not os.path.isdir(dirname):
        os.mkdir(dirname)
        return False
    else: return True

def getFile(dirname, extensions):
    if not checkDir(dirname):
        return False
    list_of_files = []
    for element in extensions:
        list_of_files += glob.glob(dirname + '*.' + element)
    if len(list_of_files) == 0:
        return False
    else: return list_of_files


# filtrar las cuentas del BSS y conservar solo aquellas asociadas a productos financieros
def pool(df):

    #print(df.columns)
    cirbe = pd.DataFrame(columns=['cuenta', 'concepto', 'dispuesto', 'documento'])
    cirbe.flags.allows_duplicate_labels = False

    for index, row in df.iterrows():
        if row.cuenta[:3] == '52' or row.cuenta[:3] == '170' or row.cuenta[:3] == '171':
            cirbe.loc[len(cirbe)] = row.tolist()

    #print(cirbe)

    return cirbe

#def numeroMeses(final, inicio):
#    return (final.year - inicio.year) * 12 + (final.month - inicio.month)

def Diff(li1, li2):
    return list(set(li1) - set(li2)) + list(set(li2) - set(li1))

# función de añadir elemento a una celda, generando lista a partir de str o expandiendo lista a partir de lista
def add_to_columna(columna, text):
    if type(columna) == str: return [columna, text]
    else: return columna.append(text)

# función para determinar si un registro alfanumérico contiene algún número (al final no se hace uso de esta función)
def detect_numbers(characters):
    numbers = ['0','1','2','3','4','5','6','7','8','9']
    if any([x in numbers for x in characters]): return True
    return False

def no_blanks(text):
    text = list(text)
    characters = [' ', '\t', '\n', '\v', '\r', '\f']
    while text[0] in characters: text = text[1:]
    while text[-1] == characters: text = text[:-1]
    return ''.join(text)

def num_participantes(text):
    if type(text) != str:   return 0
    text = text.split(',')
    if len(text) == 1:      return 1
    else:                   return int(text[-1])

def sol_col(text):
    if type(text) != str: return None
    return text.split(',')[0]


def retrieveEntidad(x):
    try:
        register = Entidad.objects.get(codigo=x)
    except:
        register = Entidad(codigo=x)
        register.save()
    return register

def retrieveTipo(x):
    try:
        registro = Tipo.objects.get(tipo=x)
    except:
        registro = Tipo(tipo=x)
        registro.save()
    return registro

def retrieveNatInterv(x):
    try:
        registro = NatInterv.objects.get(tipo=x)
    except:
        registro = NatInterv(tipo=x)
        registro.save()
    return registro

def solidaria(text):
    if type(text) == str:
        if len(text.split(',')) > 1:
            tipo = text.split(',')[0]
            try:
                registro = SolCol.objects.get(tipo=tipo)
            except:
                registro = SolCol(tipo=tipo)
            return registro
        else:
            try:
                registro = SolCol(tipo=text)
                return registro
            except:
                return ''
    #return null

def retrieveMoneda(x):
    try:
        registro = Moneda.objects.get(tipo=x)
    except:
        registro = Moneda(tipo=x)
        registro.save()
    return registro

def retrieveProducto(x):
    try:
        registro = Producto.objects.get(tipo=x)
    except:
        registro = Producto(tipo=x)
        registro.save()
    return registro

def saveGarantiaReal(data, garantiaReal):
    for element in garantiaReal:
        #print('Element: ', element)
        if element in Real.diccionario.keys():
            try:
                register = Real.objects.get(deuda=data, tipo=element)
                register = Real(register.id, deuda=data, tipo=element)
            except:
                register = Real(deuda=data, tipo=element)
            register.save()
            return register
        else:
            #print('Elemento erróneo en garantías reales: ', element)
            return False

def saveGarantiaPersonal(data, garantiaPersonal):
    for element in garantiaPersonal:
        #print('Element: ', element)
        if element in Personal.diccionario.keys():
            try:
                register = Personal.objects.get(deuda=data, tipo=element)
                register = Personal(register.id, deuda=data, tipo=element)
            except:
                register = Personal(deuda=data, tipo=element)
            register.save()
            return register
        else:
            #print('Elemento erróneo en garantías reales: ', element)
            return False

# funciones de depuración de contenidos
def personal(text):
    if type(text) == str and text != '': return text
    return None

def real(text):
    if type(text) == str and text != '': return text
    return None

def situOper(text):
    if type(text) == str:
        try:
            register = SituOper.objects.get(tipo=text)
        except:
            register = SituOper(tipo=text)
            register.save()
        return register
    else:
        return None

def plazo(text):
    if type(text) == str:
        try:
            register = Plazo.objects.get(tipo=text)
        except:
            register = Plazo(tipo=text)
            register.save()
        return register
    else:
        return ''

def changeToFloat(index, element, extraccion, campo=None):
    try:
        return float(element.replace('.', '').replace(',', '.'))
    except Exception as e:
        param = {'extraccion': extraccion, 'mensaje': f'Fallo en changeToFloat en la conversión a número en '
                                                      f'fila {index+2} para {element}', 'traza': f'{e}',
                 'tabla': 'Cirbe', 'bloqueo': 1}
        if campo:
            param['campo'] = campo
        extraccion_error = Extracciones_Errores(**param)
        extraccion_error.save()
        #print(f'\nFallo en la conversión a número en fila {index+2}\n')
        return np.nan

# a veces las operaciones se pueden partir en dos celdas diferentes; esta función las reunifica
def unificar_operaciones(cirbe):
    operacion = []
    flag = False
    for index, element in cirbe.iterrows():
        # si el Tipo es un float en lugar de ser un código, quiere decir que el dato era parte de una operación
        if type(element.tipo) == float:
            # si ha sido identificado como float pero es un nan, quiere decir que 'operacion' está partida por un espacio
            if math.isnan(element.tipo):
                # conservamos la operación
                operacion.append(element.operacion)
                # eliminar la fila errónea del df
                cirbe.drop(index, inplace=True)
                # marcar que se ha detectado una fila errónea, debida al espacio entre las dos partes del código partido de la operación
                flag = True
        # si el Tipo no es float, en principio es correcto
        else:
            # si la fila anterior sí que era errónea
            if flag:
                # añadimos la operación actual a la anterior
                operacion.append(cirbe.operacion.loc[index])
                # juntamos ambas mitades de la operación con un separador de espacio vacío (mismo que se había detectado)
                cirbe.operacion.loc[index] = ' '.join(operacion)
                # se vacía la lista de operaciones partidas
                operacion = []
                # se devuelve el estado a que no hay errores
                flag = False
    return cirbe

def poolBancario(entidad, concepto, plazo, disponible, dispuesto): ## ASEGURARSE DE QUE DISPONIBLE Y ENTIDAD LLEGAN COMO NÚMEROS

    #print('Concepto: ', concepto)
    concepto = Tipo.objects.get(tipo=concepto).descripcion
    plazo = Plazo.objects.get(tipo=plazo).descripcion

    if dispuesto == 0 and disponible == 1: return None

    elif entidad == 3017 and concepto == 'CRÉDITO COMERCIAL' and disponible != 0: return 'sin' #'ventas'

    elif concepto == 'AVAL FINANCIERO' or concepto == 'AVALES Y CAUCIONES NO FINANCIEROS PRESTADOS': return 'aval'

    elif concepto == 'CRÉDITOS DOCUMENTARIOS IRREVOCABLES': return 'sin' #'documentario'

    elif entidad == 4799 and dispuesto != 0 and disponible == 0 and plazo == 'HASTA TRES MESES': return 'prestamo'

    elif entidad == 8838 and dispuesto != 0 and disponible == 0 and plazo == 'HASTA TRES MESES': return 'prestamo'

    elif entidad == 3191 and concepto == 'CRÉDITO FINANCIERO' and plazo == 'HASTA TRES MESES':
        if disponible+dispuesto <= 6000: return 'sin' #'tarjeta'
        elif disponible+dispuesto > 6000: return 'poliza'

    elif concepto == 'HIPOTECA INMOBILIARIA': return 'hipoteca'

    elif concepto == 'CRÉDITO FINANCIERO':
        if plazo == 'HASTA TRES MESES':
            if disponible != 0:
                if entidad == 182: return 'sin' #'tarjeta'
                else: return 'poliza'
            else:
                if entidad == 182: return 'poliza'
                else: return 'compras'

        elif plazo == 'VENCIMIENTO INDETERMINADO': return 'sin' #'tarjeta'

        else:
            if disponible != 0:
                if plazo == 'MÁS DE TRES MESES Y HASTA UN AÑO' and entidad == 3035: return 'sin' #'tarjeta'
                else: return 'poliza'
            else:
                if plazo == 'MÁS DE TRES MESES Y HASTA UN AÑO': return 'sin' #'compras'
                else: return 'prestamo'

    # FALTA COMPROBAR SI LOS COMERCIALES CON Y SIN RECURSO PERO CON DISPONIBLE TAMBIÉN SON FINANCIACIÓN DE VENTAS
    elif concepto == 'CRÉDITO COMERCIAL CON RECURSO' or concepto == 'CRÉDITO COMERCIAL SIN RECURSO': return 'sin' #'ventas'

    elif concepto == 'ARRENDAMIENTO FINANCIERO PARA EL ARRENDATARIO': return 'leasing'

    elif concepto == 'DISPONIBLES EN OTROS COMPROMISOS': return 'multilinea'

    else: return 'sin'


"""
import sys


def str_to_class(classname):
    return getattr(sys.modules[__name__], classname)

def argumentsForCirbe(row):

    fields = inspect.getmembers(Cirbe)
    fields = dict(fields)
    print(fields)
    fields = {key: fields[key] for key in row.keys()}
    print(fields)

    new_dict = {}
    for key in row:
        if 'Forward' in fields[key]:
            new_key = key[0]
            new_key = new_key.upper()+key[1:]
            print(new_key)
            new_dict[key] = str_to_class(key)(tipo=row[key])
        else:
            new_dict[key] = row[key]

    return new_dict
"""

