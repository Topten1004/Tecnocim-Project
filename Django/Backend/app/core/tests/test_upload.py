from io import StringIO
from django.test import TestCase
from core.models import Empresa, Extracciones_Errores
import os
from django.core.files.base import File
from django.core.management import call_command

class UploadTestCase(TestCase):
    fixtures = ['equivalencias/fixtures/fixtures.json',] # Carga fixtures
    files_path = 'core/tests/' # Donde se encuentran los ficheros prueba
    data = {
        ## CIRBE ###
        'CIRBE1': { # OK
            'filename': files_path + '/files/Cirbe01.pdf',
            'fecha': '2019-12-31',
            'tipoDoc': 'Cirbe',
            'CIF': 'G31123060',
            'extraccion': None,
            'resultado': 'ok'
        },
        'CIRBE2': { # Fecha errónea (KO)
            'filename': files_path + '/files/Cirbe01.pdf',
            'fecha': 'fake',
            'tipoDoc': 'Cirbe',
            'CIF': 'J57923468',
            'extraccion': None,
            'resultado': 'ko'
        },
        'CIRBE3': { # TipoDoc erróneo (KO)
            'filename': files_path + '/files/Cirbe01.pdf',
            'fecha': '2019-12-31',
            'tipoDoc': 'fake',
            'CIF': 'P4613771G',
            'extraccion': None,
            'resultado': 'ko'
        },
        'CIRBE4': { # OK
            'filename': files_path + '/files/Cirbe02.pdf',
            'fecha': '2020-12-31',
            'tipoDoc': 'Cirbe',
            'CIF': 'V22422398',
            'extraccion': None,
            'resultado': 'ok'
        },
        'CIRBE5': { # Extensión errónea
            'filename': files_path + '/files/BSS_01.xlsx',
            'fecha': '2020-12-31',
            'tipoDoc': 'Cirbe',
            'CIF': 'C28927879',
            'extraccion': None,
            'resultado': 'ko'
        },
        ### MODELO 200 ###
        'MODELO200_1': { # OK
            'filename': files_path + '/files/Modelo200_01.pdf',
            'fecha': '2020-12-31',
            'tipoDoc': 'Modelo200',
            'CIF': 'Q9166735B',
            'extraccion': None,
            'resultado': 'ok'
        },
        'MODELO200_2': { # Fecha errónea (KO)
            'filename': files_path + '/files/Modelo200_01.pdf',
            'fecha': 'fake',
            'tipoDoc': 'Modelo200',
            'CIF': 'W6166788G',
            'extraccion': None,
            'resultado': 'ko'
        },
        'MODELO200_3': { # TipoDoc erróneo (KO)
            'filename': files_path + '/files/Modelo200_01.pdf',
            'fecha': '2020-12-31',
            'tipoDoc': 'fake',
            'CIF': 'F09538646',
            'extraccion': None,
            'resultado': 'ko'
        },
        'MODELO200_4': { # OK (COLOCAR OTRO DOCUMENTO)
            'filename': files_path + '/files/Modelo200_01.pdf',
            'fecha': '2020-12-31',
            'tipoDoc': 'Modelo200',
            'CIF': 'U81747586',
            'extraccion': None,
            'resultado': 'ok'
        },
        'MODELO200_5': { # Extensión errónea
            'filename': files_path + '/files/BSS_01.xlsx',
            'fecha': '2020-12-31',
            'tipoDoc': 'Modelo200',
            'CIF': 'Q2052232B',
            'extraccion': None,
            'resultado': 'ko'
        },
        # ### BSS ###
        'BSS_01': { # OK
            'filename': files_path + '/files/BSS_01.xlsx',
            'fecha': '2020-12-31',
            'tipoDoc': 'BSS',
            'CIF': 'A63998199',
            'extraccion': None,
            'resultado': 'ok'
        },
        'BSS_02': { # Fecha errónea (KO)
            'filename': files_path + '/files/BSS_01.xlsx',
            'fecha': 'fake',
            'tipoDoc': 'BSS',
            'CIF': 'E96163746',
            'extraccion': None,
            'resultado': 'ko'
        },
        'BSS_03': { # TipoDoc erróneo (KO)
            'filename': files_path + '/files/BSS_01.xlsx',
            'fecha': '2020-12-31',
            'tipoDoc': 'fake',
            'CIF': 'J55657514',
            'extraccion': None,
            'resultado': 'ko'
        },
        'BSS_04': { # OK
            'filename': files_path + '/files/BSS_02.xlsx',
            'fecha': '2021-12-31',
            'tipoDoc': 'BSS',
            'CIF': 'R0180901A',
            'extraccion': None,
            'resultado': 'ok'
        },
        'BSS_05': { # Extensión errónea
            'filename': files_path + '/files/Modelo200_01.pdf',
            'fecha': '2021-12-31',
            'tipoDoc': 'BSS',
            'CIF': 'U56299423',
            'extraccion': None,
            'resultado': 'ko'
        },
        'empresa_inexistente': {
            'filename': files_path + '/files/Modelo200_01.pdf',
            'fecha': '2021-12-31',
            'tipoDoc': 'BSS',
            'CIF': 'W3565820B',
            'extraccion': None,
            'resultado': 'ko'
        }
    }

    def call_command(self, *args, **kwargs):
        out = StringIO()
        call_command(
            "upload",
            *args,
            stdout=out,
            stderr=StringIO(),
            **kwargs,
        )
        return out.getvalue()
    
    def setUp(self):
        for key, item in self.data.items():
            if key == 'empresa_inexistente': # descartamos la creacion de esta empresa para pasar test
                continue
            Empresa.objects.get_or_create(
                nombre=item['CIF'],
                CIF=item['CIF']
            )

    def test_extractor(self):
        for key, item in self.data.items():
            print('Test extractor command: {0}'.format(key))
            filename = item['filename']
            self.assertEqual(os.path.exists(filename), True, 'No se encuentra el archivo \'{0}\''.format(filename))
            output = self.call_command(
                filename=filename,
                fecha=item['fecha'],
                tipoDoc=item['tipoDoc'],
                CIF=item['CIF'],
                extraccion=item['extraccion']
            )
            result = output.split('|')
            self.assertEqual(len(result), 2, 'La salida del comando no contiene el formato correcto id|resultado')
            if item['resultado'] == 'ko':
                self.assertEqual(result[0], item['resultado'], Extracciones_Errores.objects.last().mensaje)
            else:
                if result[0] == 'ko':
                    self.assertEqual(result[0], item['resultado'], Extracciones_Errores.objects.last().mensaje)
                else:
                    self.assertEqual(result[0], item['resultado'], 'Extracción debería ser ok')