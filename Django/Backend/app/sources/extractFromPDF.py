# import logging
import os.path
import shutil
import sys

from adobe.pdfservices.operation.auth.credentials import Credentials
from adobe.pdfservices.operation.exception.exceptions import ServiceApiException, ServiceUsageException, SdkException
from adobe.pdfservices.operation.pdfops.options.extractpdf.extract_pdf_options import ExtractPDFOptions
from adobe.pdfservices.operation.pdfops.options.extractpdf.extract_element_type import ExtractElementType
from adobe.pdfservices.operation.execution_context import ExecutionContext
from adobe.pdfservices.operation.io.file_ref import FileRef
from adobe.pdfservices.operation.pdfops.extract_pdf_operation import ExtractPDFOperation
import zipfile

from app.settings import BASE_DIR, MEDIA_ROOT
import warnings
warnings.filterwarnings("ignore")

def resource_path(relative_path):
    """ Get absolute path to resource, works for dev and for PyInstaller """
    try:
        # PyInstaller creates a temp folder and stores path in _MEIPASS
        base_path = sys._MEIPASS
    except Exception:
        base_path = os.path.join(BASE_DIR, 'sources')

    return os.path.join(base_path, relative_path)

def extractJsonFromPDF(fichero):
    #print('Inicio transformación ', fichero)
    #logging.basicConfig(level=os.environ.get("LOGLEVEL", "INFO"))

    try:
        # get base path.
        #base_path = os.path.dirname(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
        resources = resource_path('resources')

        # Initial setup, create credentials instance.
        credentials = Credentials.service_account_credentials_builder() \
            .from_file(resources + "/pdfservices-api-credentials.json") \
            .build()

        # Create an ExecutionContext using credentials and create a new operation instance.
        execution_context = ExecutionContext.create(credentials)
        extract_pdf_operation = ExtractPDFOperation.create_new()

        # Set operation input from a source file.
        source = FileRef.create_from_local_file(os.path.join(MEDIA_ROOT, fichero.name))
        extract_pdf_operation.set_input(source)

        # Build ExtractPDF options and set them into the operation
        extract_pdf_options: ExtractPDFOptions = ExtractPDFOptions.builder() \
            .with_element_to_extract(ExtractElementType.TEXT) \
            .build()
        extract_pdf_operation.set_options(extract_pdf_options)

        # Execute the operation.
        result: FileRef = extract_pdf_operation.execute(execution_context)

        # Save the result to the specified location.
        nuevoFichero = os.path.splitext(fichero.name)[0]
        fileZip = os.path.join(MEDIA_ROOT, nuevoFichero+'.zip')
        #print(f'\nResult: {result}\tFileZip: {fileZip}\n')

        if os.path.isfile(fileZip):
            os.remove(fileZip)
            #print('Fichero borrado')

        with open(fileZip, 'wb+') as destination:
            result.write_to_stream(destination)
        #shutil.move(result, fileZip)

        #result.save_as(fileZip)
        #print('Resultado comprimido salvado en: ', fileZip)

        nuevoDirectorio = os.path.join(MEDIA_ROOT, os.path.dirname(fichero.name), 'tmp')
        if os.path.isdir(nuevoDirectorio): pass
        else: os.mkdir(nuevoDirectorio)
        fileJson = nuevoDirectorio+'/structuredData.json'
        if os.path.isfile(fileJson):
            os.remove(fileJson)
        with zipfile.ZipFile(fileZip, 'r') as zip_ref:
            zip_ref.extractall(nuevoDirectorio)
        nuevoFichero = os.path.join(MEDIA_ROOT, os.path.splitext(fichero.name)[0] + '.json')
        if os.path.isfile(nuevoFichero):
            os.remove(nuevoFichero)
        #os.replace('structuredDataJson.json', os.path.basename(nuevoFichero), src_dir_fd=nuevoDirectorio, dst_dir_fd=os.path.dirname(nuevoFichero))
        os.replace(fileJson, nuevoFichero)
        os.remove(fileZip)

    except Exception as e: #(ServiceApiException, ServiceUsageException, SdkException):
        # logging.exception("Exception encountered while executing operation")
        #print('Exception: ', e)
        nuevoFichero = False

    return nuevoFichero

if __name__ == '__main__':
    extractJsonFromPDF('C:\\Users\\Pablo\\OneDrive\\Tecnocim\\Ficheros\\HistorialTecnocim\\SOFT 200-2020.pdf')