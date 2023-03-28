import datetime
import json
import os.path
import random
from random import randint

from django.contrib.auth import get_user_model
from django.test import TestCase

from rest_framework import status
from rest_framework.reverse import reverse
from rest_framework.test import APIClient

from core.models import Empresa, Documento
from core.serializers import EmpresaSerializer, DocumentoSerializer

EMPRESA_URL = reverse('base:empresa-list')
DOCUMENTO_URL = reverse('base:documento-list')

print('URL: ', EMPRESA_URL)
print('URL: ', DOCUMENTO_URL)


def crear_empresa(**params):

    CIF = 'ESB'+str(randint(1000, 9999))

    path = os.path.join('C:/Alia-Tecnocim/Modelo200/', 'test.json')

    defaults = {'CIF': CIF, 'nombre': 'Miralbueno', 'contacto': 'Alba', 'telefono': 987634234,
                'email': 'test@example.com', 'configFile': path}
    defaults.update(params)
    #print('crear_empresa: ', defaults)
    empresa = Empresa.objects.create(**defaults)
    return defaults, empresa

def crear_documento(**params):
    #parametros, empresa = crear_empresa(**params)
    #print('Parametros crear doc: ', params)
    start_date = datetime.date(2000, 1, 1)
    end_date = datetime.date(2022, 7, 1)
    time_between_dates = end_date - start_date
    days_between_dates = time_between_dates.days
    random_number_of_days = random.randrange(days_between_dates)
    fecha = start_date + datetime.timedelta(days=random_number_of_days)
    parametros_doc = {'documento': params['documento'], 'fecha': fecha, 'empresa': params['empresa']}
    documento = Documento.objects.create(**parametros_doc)
    #print('Documento: ', documento)
    return parametros_doc, documento

"""
class PublicEmpresaAPITests(TestCase):

    def setUp(self):
        self.client = APIClient()

    def test_auth_required(self):
        res = self.client.get(EMPRESA_URL)
        self.assertEqual(res.status_code, status.HTTP_401_UNAUTHORIZED)


class PrivateEmpresaApiTests(TestCase):

    def setUp(self):
        self.client = APIClient()
        self.user = get_user_model().objects.create_user(
            'user@example.com',
            'testpass123',
        )
        self.client.force_authenticate(self.user)

    def test_retrieve_empresa(self):
        crear_empresa()
        crear_empresa()

        empresas = Empresa.objects.all()
        serializer = EmpresaSerializer(empresas, many=True)
        res = self.client.get(EMPRESA_URL)
        self.assertEqual(res.status_code, status.HTTP_200_OK)
        self.assertEqual(res.data, serializer.data)


class PublicDocumentoAPITests(TestCase):

    def setUp(self):
        self.client = APIClient()

    def test_auth_required(self):
        res = self.client.get(DOCUMENTO_URL)
        self.assertEqual(res.status_code, status.HTTP_401_UNAUTHORIZED)

"""
class PrivateDocumentoApiTests(TestCase):

    def setUp(self):
        self.client = APIClient()
        self.user = get_user_model().objects.create_user(
            'user@example.com',
            'testpass123',
        )
        self.client.force_authenticate(self.user)

    def test_retrieve_empresa(self):
        defaults, empresa = crear_empresa()
        #print(defaults,'\n', empresa)
        crear_documento(documento=defaults['configFile'], empresa=empresa)
        crear_documento(documento=defaults['configFile'], empresa=empresa)

        documentos = Documento.objects.all()
        serializer = DocumentoSerializer(documentos, many=True)
        #print('Serializer: ', serializer.data)
        #print('Documentos: ', documentos)
        #print('Client: ', self.client)
        res = self.client.get(DOCUMENTO_URL)
        #print('Res.data: ', res.data)
        #print('Serializer.data: ', serializer.data)
        self.assertEqual(res.status_code, status.HTTP_200_OK)
        self.assertEqual(res.data, serializer.data)
        
    def test_filter_by_date(self):

        mindate = datetime.date.today()
        maxdate = datetime.date(1900, 1, 1)
        docs = []
        for i in range(3):
            defaults, empresa = crear_empresa()
            for j in range(4):
                parametros = {'documento': defaults['configFile'], 'empresa': empresa}
                parametros, doc = crear_documento(**parametros)
                print(doc)
                docs.append(doc)
                if parametros['fecha'] < mindate: mindate = parametros['fecha']
                if parametros['fecha'] > maxdate: maxdate = parametros['fecha']
        time_between_dates = maxdate - mindate
        days_between_dates = time_between_dates.days
        mindate = mindate + datetime.timedelta(days=round(days_between_dates/4.0))
        maxdate = maxdate - datetime.timedelta(days=round(days_between_dates/4.0))
        params = {'mindate': mindate, 'maxdate': maxdate}

        filteredDocs = []
        for element in docs:
            if mindate <= element.fecha <= maxdate: filteredDocs.append(element)
        res = self.client.get(DOCUMENTO_URL, params)
        serializer = DocumentoSerializer(filteredDocs, many=True)
        print(filteredDocs)
        print(res.data)
        print(serializer.data)
        #print('Res.data Filter: ', len(res.data))
        #print('Serializer.data Filter: ', len(serializer.data))

        self.assertEqual(serializer.data, res.data)
