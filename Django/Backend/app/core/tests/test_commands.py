# Test custom Django management commands

from unittest.mock import patch

from psycopg2 import OperationalError as Psycopg2Error

from django.core.management import call_command
from django.db.utils import OperationalError
from django.test import SimpleTestCase


@patch('core.management.commands.wait_for_db.Command.check') #simularemos la función check de Command (función que hereda de la clase padre)
class CommandTests(SimpleTestCase):

    def test_wait_for_db_ready(self, patched_check):
        # el valor de retorno será true
        patched_check.return_value = True
        # llamamos el comando
        call_command('wait_for_db')
        # comprobamos si efectivamente se ha llamado a la db
        patched_check.assert_called_once_with(databases=['default'])
        print('Test test_wait_for_db_ready')

    @patch('time.sleep')
    def test_wait_for_db_delay(self, patched_sleep, patched_check):
        # se simula que se devuelven 5 excepciones en 5 intentos y que el sexto intento tiene éxito
        patched_check.side_effect = [Psycopg2Error] * 2 + [OperationalError] * 3 + [True]
        # llamamos el comando
        call_command('wait_for_db')
        # comprobamos que la simulación ha generado 6 respuestas
        self.assertEqual(patched_check.call_count, 6)
        # asegurar que se está usando 'default' en las llamadas
        patched_check.assert_called_with(databases=['default'])
        print('Test test_wait_for_db_delay')