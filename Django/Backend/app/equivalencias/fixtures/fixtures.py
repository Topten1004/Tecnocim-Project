from core import dictionaries

fixture = []
i = 0
for element in dictionaries.entidades:
    new_element = {}
    new_element['model'] = 'equivalencias.Entidad'
    new_element['pk'] = i
    new_element['fields'] = {'codigo': element[0], 'nombre': element[1]}
    print(' {"model": "equivalencias.Entidad", "pk": '+str(i)+', "fields": {"codigo": "'+str(element[0])+'", "nombre": "'+element[1]+'"}},')
    i += 1
