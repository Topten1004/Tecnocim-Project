from core import dictionaries

list = [1000+i for i in range(7000)]
for grupo in dictionaries.conceptosAbreviado.keys():
    for concepto in dictionaries.conceptosAbreviado[grupo]:
        for cuenta in dictionaries.conceptosAbreviado[grupo][concepto]:
            sublist = [x for x in list if str(x)[:len(str(cuenta))] == str(cuenta)]
            for x in sublist: list.remove(x)

list3 = set([str(cuenta)[:3] for cuenta in list])
extra3 = []
for element in list3:
    sublist = [cuenta for cuenta in list if str(cuenta)[:3] == element]
    print(len(sublist))
    if len(sublist) == 10:
        for x in sublist: list.remove(x)
        extra3.append(int(element[:3]))

list2 = set([str(cuenta)[:2] for cuenta in extra3])
extra2 = []
for element in list2:
    sublist = [cuenta for cuenta in extra3 if str(cuenta)[:2] == element]
    print(len(sublist), sublist)
    if len(sublist) == 10:
        for x in sublist: extra3.remove(x)
        extra2.append(int(element[:2]))
print(sorted(extra2))
print(sorted(extra3))
print(list)
print(len(list+extra2+extra3))