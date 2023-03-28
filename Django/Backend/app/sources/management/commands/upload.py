import os
from django.core.management.base import BaseCommand

from core.models import Extracciones_Errores, Extracciones
from sources.BSS import BSS
from sources.XLSX import detect_fields
from sources.cirbe import cirbe
from sources.extractFromPDF import extractJsonFromPDF
from sources.modelo200 import modelo200
from sources.views import upload_generico, checkActivoPasivo
from django.core.files.base import ContentFile, File

import warnings
warnings.filterwarnings("ignore")

class Command(BaseCommand):

    def add_arguments(self, parser):
        parser.add_argument('--filename', type=str, required=True)
        parser.add_argument('--fecha', type=str, required=True)
        parser.add_argument('--tipoDoc', type=str, required=True)
        parser.add_argument('--CIF', type=str, required=True)
        parser.add_argument('--extraccion', type=int, required=False)
        parser.add_argument('--variacion', type=float, required=False)
        parser.add_argument('--amortizacion', type=float, required=False)
        parser.add_argument('--impuestos', type=float, required=False)

        return super().add_arguments(parser)

    def handle(self, *args, **options):

        # 'r' solo no funciona con el open, funciona 'rb'
        # https://stackoverflow.com/questions/49394039/encoding-error-when-saving-excel-file-to-django-fielfield
        # no funciona si se usa dockers, ya que el fichero está fuera
        fecha =                 options['fecha']
        tipo =                  options['tipoDoc']
        CIF =                   options['CIF']
        extraccion_id =         options['extraccion']
        conceptos = {'variacion': options['variacion'],
                     'amortizacion': options['amortizacion'],
                     'impuestos': options['impuestos']}

        try:
            f = open(options['filename'], 'rb')
            #file = ContentFile(bytes(f.read(), encoding='UTF-8'), name=os.path.basename(f.name))
            file = File(f)
        except Exception as e:
            extraccion = Extracciones(**{'tipo': tipo, 'fechahora': fecha, 'ruta': options['filename'], 'resultado': 'ko'})
            extraccion.save()
            extraccion_error = Extracciones_Errores(**{'extraccion': extraccion, 'mensaje': 'Fichero no existe',
                                                       'traza': f'{e}', 'bloqueo': 1})
            extraccion_error.save()
            return extraccion.resultado + '|' +str(extraccion.id)

        doc, extraccion = upload_generico(file, fecha, tipo, CIF, extraccion_id)

        file.close()

        if doc == 'empresa' or doc == 'extension' or doc == 'fecha' or doc == 'tipoDoc':
            if doc == 'empresa':
                mensaje = 'empresa no existe; crear empresa antes de cargar documentos'
            elif doc == 'extension':
                mensaje = 'extensión del fichero no se corresponde con el tipo de documento'
            elif doc == 'fecha':
                mensaje = 'contenidos de fecha incorrectos'
            elif doc == 'tipoDoc':
                mensaje = 'tipo de documento incorrecto'
            else:
                mensaje = 'error no controlado'
            extraccion_error = Extracciones_Errores(**{'extraccion': extraccion, 'mensaje': mensaje, 'bloqueo': 2})
            extraccion_error.save()

            return extraccion.resultado + '|' + str(extraccion.id)

        #self.stdout.write(self.style.SUCCESS("Se ha creado el documento %s - %s".format(doc.documento.name, doc.empresa.CIF)))

        if tipo == 'BSS':

            if doc.empresa.configFile:
                try:
                    result, success = BSS(doc, conceptos, extraccion)
                except Exception as e:
                    #print(f'Hay algo en BSS que no estaba controlado por try-except y que no ha funcionado: {e}')
                    extraccion_error = Extracciones_Errores(**{'extraccion': extraccion,
                                                               'mensaje': 'Fallo en alguna de las rutinas no controladas',
                                                               'traza': f'{e}', 'bloqueo': 1})
                    extraccion_error.save()
                    doc.delete()
                    return extraccion.resultado + '|' + str(extraccion.id)
                #print('Resultado: ', result, fail)
                if success:
                    extraccion.resultado = 'ok'
                    extraccion.save()
                    checkActivoPasivo(doc, extraccion)
                else:
                    extraccion_errores = Extracciones_Errores(**result)
                    extraccion_errores.save()
                    doc.delete()

            else:
                configFile, doc2 = detect_fields(doc, extraccion)
                if doc2:
                    result, success = BSS(doc2, conceptos, extraccion)
                    if success:
                        extraccion.resultado = 'ok'
                        extraccion.save()
                        checkActivoPasivo(doc2, extraccion)
                    else:
                        extraccion_errores = Extracciones_Errores(**result)
                        extraccion_errores.save()
                        doc2.delete()
                else:
                    extraccion_errores = Extracciones_Errores(**{'extraccion': extraccion, 'mensaje': 'Fallo detección coordenadas' , 'bloqueo': 1})
                    extraccion_errores.save()
                    doc2.delete()

        else:
            fichero = extractJsonFromPDF(doc.documento)
            if not fichero:
                extraccion_error = Extracciones_Errores(**{'extraccion': extraccion,
                                                           'mensaje': 'Error de la API de Acrobat',
                                                           'bloqueo': 1})
                extraccion_error.save()
                doc.delete()

            if tipo == 'Cirbe':
                try:
                    output, success = cirbe(fichero, doc, extraccion)
                except Exception as e:
                    extraccion_error = Extracciones_Errores(**{'extraccion': extraccion,
                                                               'mensaje': f'Fallo no-controlado del Cirbe',
                                                               'traza': f'{e}', 'bloqueo': 1})
                    extraccion_error.save()
                    doc.delete()
                    return extraccion.resultado + '|' + str(extraccion.id)

                if success:
                    extraccion.resultado = 'ok'
                    extraccion.save()
                else:
                    extraccion_error = Extracciones_Errores(**output)
                    extraccion_error.save()
                    doc.delete()
                    #print(f'Error vinculado a: {output}')

            elif tipo == 'Modelo200':
                try:
                    #print(fichero)
                    output, success = modelo200(fichero, doc, extraccion)
                    #print(output, success)
                except Exception as e:
                    #print('Hay algo en modelo200 que no estaba controlado por try-except y que no ha funcionado')
                    #print(f'{e}')
                    extraccion_error = Extracciones_Errores(**{'extraccion': extraccion,
                                                               'mensaje': 'Error no controlado de modelo200',
                                                               'traza': f'{e}', 'bloqueo': 1})
                    extraccion_error.save()
                    doc.delete()
                    return extraccion.resultado + '|' + str(extraccion.id)
                if success:
                    extraccion.resultado = 'ok'
                    extraccion.save()
                    # print(f'Se ha cambiado resultado a {extraccion.resultado}')
                else:
                    extraccion_error = Extracciones_Errores(**output)
                    extraccion_error.save()
                    doc.delete()
            else:
                extraccion_error = Extracciones_Errores(**{'extraccion': extraccion,
                                                           'mensaje': 'Tipo de archivo erróneo; tipos válidos = BSS, Modelo200 o Cirbe',
                                                           'bloqueo': 2})
                extraccion_error.save()
                doc.delete()
                #print(f'Tipo de archivo erróneo; tipos válidos = BSS, Modelo200, Cirbe')
        return extraccion.resultado + '|' + str(extraccion.id)