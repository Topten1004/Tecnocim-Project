import datetime
import email
from django.test import TestCase
from django.contrib.auth import get_user_model

from core import models


class ModelTests(TestCase):

    def test_create_user_with_email_successful(self):

        print('test de create_user_with_email')
        email = 'test@example.com'
        password = 'testpass123'
        user = get_user_model().objects.create_user(email=email, password=password)

        self.assertEqual(user.email, email)
        self.assertTrue(user.check_password(password))

    def test_new_user_email_normalized(self):

        print('Test de emails normalizados')
        sample_emails = [
            ['test1@EXAMPLE.com', 'test1@example.com'],
            ['Test2@Example.com', 'Test2@example.com'],
            ['TEST3@Example.com', 'TEST3@example.com'],
            ['test4@example.COM', 'test4@example.com']]
        for email, expected in sample_emails:
            user = get_user_model().objects.create_user(email, 'sample123')
            self.assertEqual(user.email, expected)

    def test_new_user_without_email_raises_error(self):
        with self.assertRaises(ValueError):
            get_user_model().objects.create_user('', 'test123')

    def test_create_superuser(self):

        user = get_user_model().objects.create_superuser('test@example.com', 'test123')

        self.assertTrue(user.is_superuser)
        self.assertTrue(user.is_staff)

    def crear_empresa(self):

        parametros = {'CIF': 'ES0438965', 'nombre': 'Miralbueno', 'contacto': 'Alba', 'telefono': 987634234,
                      'email': 'test@example.com', 'configFile': 'C:\\Alia-Tecnocim\\Modelo200\\test.json'}

        return parametros, models.Empresa.objects.create(**parametros)

    def test_crear_empresa(self):
        parametros, empresa = self.crear_empresa()
        email_parametros = parametros['email']
        email_empresa = empresa.email
        self.assertEqual(email_empresa, email_parametros)

    def crear_documento(self):
        parametros, empresa = self.crear_empresa()
        fecha = datetime.date.today()
        parametros_doc = {'documento': parametros['configFile'], 'fecha': fecha, 'empresa': empresa}
        return parametros_doc, models.Documento.objects.create(**parametros_doc)

    def test_crear_documento(self):
        parametros, documento = self.crear_documento()
        self.assertEqual(parametros['empresa'], documento.empresa)

    def test_crear_concepto_contable(self):
        parametros, documento = self.crear_documento()
        parametros = {'concepto': 'inmovilizado', 'magnitud': 10000, 'codigo': '00200', 'origen': 'modelo200', 'documento': documento}
        contable = models.Contabilidad.objects.create(**parametros)
        self.assertEqual(parametros['documento'], contable.documento)
