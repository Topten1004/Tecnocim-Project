# Django command to wait for the database to be available

import time

from psycopg2 import OperationalError as Psycopg2Error

from django.db.utils import OperationalError
from django.core.management.base import BaseCommand

class Command(BaseCommand):

    def handle(self, *args, **options):
        self.stdout.write('Esperando a la base de datos ...')
        db_up = False
        while db_up is False:
            try:
                self.check(databases=['default'])
                db_up = True
            except (Psycopg2Error, OperationalError):
                self.stdout.write('Base de datos no disponible, esperando 1 segundo más ...')
                time.sleep(1)

        self.stdout.write(self.style.SUCCESS('Base de datos disponible'))