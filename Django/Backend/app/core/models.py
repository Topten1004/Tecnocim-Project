import datetime
import math
import os

from dateutil.relativedelta import relativedelta
from django.contrib.auth.models import AbstractBaseUser, BaseUserManager, PermissionsMixin
from django.core.exceptions import ValidationError
from django.db.models import Sum
from django.db import models

from django.core.validators import MinLengthValidator, MaxValueValidator

from app.settings import BASE_DIR, MEDIA_ROOT
from core.dictionaries import entidades

productos_largo = ['aval','prestamo' ,'leasing', 'hipoteca']
productos_poliza = ['poliza', 'click']
productos_compras = ['confirming estandar', 'confirming pronto pago', 'financiacion importaciones', 'credito documentario importaciones']
productos_ventas = ['descuento pagares', 'descuento pagares no orden', 'anticipo recibos', 'anticipo facturas',
                    'anticipo pagos domiciliados', 'credito documentario exportaciones', 'factoring con recurso', 'factoring sin recurso']


def numeroMeses(final, inicio, inverse=False):
    meses = (final.year - inicio.year) * 12 + (final.month - inicio.month)
    #print(f'Inicio {inicio.day}, Final: {final.day}, Comparación: {inicio.day < final.day}')
    if inverse:
        if inicio.day < final.day: meses += 1 #final es el día de referencia
    else:
        if final.day < inicio.day: meses += 1 #inicio es el día de referencia
    return meses


class UserManager(BaseUserManager):

    def create_user(self, email, password=None, **extra_fields):
        if not email:
            raise ValueError('Usuario ha de tener una dirección email')
        user= self.model(email=self.normalize_email(email), **extra_fields)
        user.set_password(password)
        user.save(using=self._db)

        return user

    def create_superuser(self, email, password):
        user = self.create_user(email, password)
        user.is_staff = True
        user.is_superuser = True
        user.save(using=self._db)

        return user


class User(AbstractBaseUser, PermissionsMixin):

    email = models.EmailField(max_length=255, unique=True)
    name = models.CharField(verbose_name='Nombre', max_length=255)
    is_active = models.BooleanField(default=True)
    is_staff = models.BooleanField(default=False)

    objects = UserManager()

    USERNAME_FIELD = 'email'


def primera_letra(CIF):
    if not CIF[0].isalpha():
        raise ValidationError(message='El CIF debería empezar con una letra')

class Empresa(models.Model):

    CIF = models.CharField(verbose_name='CIF', validators=[MinLengthValidator(9), primera_letra], max_length=12, unique=True, blank=False)
    nombre = models.CharField(verbose_name='Nombre comercial', max_length=50)
    contacto = models.CharField(verbose_name='Persona de contacto', max_length=50, null=True)
    telefono = models.BigIntegerField(verbose_name='Teléfono de contacto', null=True)
    email = models.EmailField(verbose_name='Email', null=True)
    configFile = models.FilePathField(verbose_name='Lectura BSS', path=BASE_DIR, null=True, blank=True)
    #status = models.IntegerField(verbose_name='Status', default=0)

    class Meta:
        ordering = ['nombre', 'CIF']

    @property
    def documents(self):
        status = 0
        documento = Documento.objects.filter(empresa__CIF=self.CIF, origen='BSS', status=True).latest('fecha')
        if documento:
            contratos = Pool.objects.filter(documento__empresa__CIF=self.CIF, documento__fecha=documento.fecha, contrato__isnull=True).count()
            if contratos > 0:
                status = 1
            else:
                status = 2
        return Documento.objects.filter(empresa__CIF=self.CIF, status=True).count(), status

    def __str__(self):
        return f'{str(self.id)} {self.CIF} {self.nombre}'


class Extracciones(models.Model):

    choices_tipo = (('Cirbe', 'CIRBE'), ('Modelo200', 'MODELO200'), ('BSS', 'BSS'))
    choices_resultado = (('ok', 'OK'), ('ko', 'KO'))

    tipo = models.CharField(verbose_name='Tipo', choices=choices_tipo, max_length=10, null=True)
    # mirar si se puede poner pathfield
    ruta = models.CharField(verbose_name='Ruta', max_length=255)
    ruta_unmerged = models.CharField(verbose_name='Unmerged_Excel', max_length=255, default=None, null=True, blank=True)
    fechahora = models.DateTimeField(verbose_name='Fecha')
    resultado = models.CharField(verbose_name='Resultado', choices=choices_resultado, max_length=2, null=True)
    #mensaje = models.TextField(verbose_name='Motivo', null=True)

    def __str__(self):
        return f'{str(self.id)} {str(self.ruta)} {self.fechahora.strftime("%Y-%m-%d %H:%M")} {str(self.resultado)}'

class Extracciones_Errores(models.Model):

    choices_bloqueo = ((1, 'Crítico'), (2, 'Interrupción'), (3, 'Potencial'))
    choices_tabla = (('Documento', 'Documento'), ('Crudos', 'Crudos'), ('Contabilidad', 'Contabilidad'),
                     ('Ratio', 'Ratio'), ('Analitica', 'Analítica'), ('Pool', 'Pool'), ('Cirbe', 'Cirbe'))

    extraccion = models.ForeignKey(Extracciones, verbose_name='Extracción', on_delete=models.CASCADE)
    mensaje = models.CharField(verbose_name='Mensaje', max_length=255, default='', null=True, blank=True)
    traza = models.TextField(verbose_name='Motivo', null=True, blank=True)
    bloqueo = models.IntegerField(verbose_name='Bloqueo', choices=choices_bloqueo, null=True)
    tabla = models.CharField(verbose_name='Tabla', choices=choices_tabla, null=True, blank=True, max_length=50)
    campo = models.CharField(verbose_name='Columna', null=True, blank=True, max_length=50)


def user_file_path(instance, file):
    filename = file.split('_', 1)[1]
    inicio = file.split('_')[0]
    extension = file.split('.')[-1]
    #print(f'Filename: {filename}\tInicio: {inicio}\tExtension: {extension}')
    if inicio == 'BSS': location = os.path.join(instance.empresa.CIF, 'BSS', filename.rsplit('.', 1)[0] + '_' + str(instance.fecha) + '.' + extension)
    elif inicio == 'Cirbe': location = os.path.join(instance.empresa.CIF, 'Cirbe', filename.rsplit('.', 1)[0] + '_' + str(instance.fecha) + '.pdf')
    else: location = os.path.join(instance.empresa.CIF, 'Modelo200', filename.rsplit('.', 1)[0] + '_' + str(instance.fecha) + '.pdf')
    #print(f'Filename: {filename}\tLocation: {location}')
    return location


class Documento(models.Model):
    CIRBE = 'Cirbe'
    MODELO200 = 'Modelo200'
    BSS = 'BSS'
    
    choices_tipo = ((CIRBE, 'CIRBE'), (MODELO200, 'MODELO200'), (BSS, 'BSS'))

    documento = models.FileField(verbose_name='Documento', upload_to=user_file_path)
    origen = models.CharField(verbose_name='Tipo de Archivo', choices=choices_tipo, max_length=9)
    fecha = models.DateField(verbose_name='Fecha Info', validators=[MaxValueValidator(limit_value=datetime.date.today)])
    empresa = models.ForeignKey(Empresa, verbose_name='Empresa', on_delete=models.CASCADE)
    status = models.BooleanField(verbose_name='Status', default=False)
    extraccion = models.ForeignKey(Extracciones, verbose_name='Extracción', on_delete=models.CASCADE)

    class Meta:
        unique_together = ('origen', 'fecha', 'empresa')
        ordering = ['empresa', 'origen', 'fecha']

    def delete(self, using=None, keep_parents=False):
        original, extension = os.path.splitext(self.documento.name)
        # print(original, extension)

        if extension == '.pdf':
            pdf = '{0}{1}{2}'.format(MEDIA_ROOT, original, '.pdf')
            json_doc = '{0}{1}{2}'.format(MEDIA_ROOT, original, '.json')
            # print(pdf, json_doc)
            print(os.path.isfile(pdf), os.path.isfile(json_doc))
            if os.path.isfile(pdf):
                os.remove(pdf)
            if os.path.isfile(json_doc):
                os.remove(json_doc)
        elif extension == '.xlsx':
            bss = '{0}{1}{2}'.format(MEDIA_ROOT, original, '.xlsx')
            unmerged = '{0}{1}'.format(MEDIA_ROOT, '{0}_unmerged.xlsx'.format(original))
            results = '{0}{1}'.format(MEDIA_ROOT, '{0}resultados.xlsx'.format(original))
            if os.path.isfile(bss):
                os.remove(bss)
            if os.path.isfile(results):
                os.remove(results)
            if os.path.isfile(unmerged) and self.extraccion.resultado == 'ok':
                os.remove(unmerged)
        super(Documento, self).delete()

    def __str__(self):
        return str(self.id) + ' ' + self.documento.name

"""
class Operacion(models.Model):

    usuario = models.ForeignKey(settings.AUTH_USER_MODEL, verbose_name='Usuario', on_delete=models.CASCADE)
    empresa = models.ForeignKey(Empresa, verbose_name='Empresa', on_delete=models.CASCADE)
    documento = models.ForeignKey(Documento, verbose_name='Documento', null=True, on_delete=models.CASCADE)
    momento = models.DateTimeField(verbose_name='Hora')
    operacion = models.JSONField(verbose_name='Operacion', null=True)

    def __str__(self):
        return self.usuario.email + ' ' + str(self.momento)
"""

class Crudos(models.Model):

    cuenta = models.CharField(verbose_name='Cuenta', max_length=100)
    concepto = models.CharField(verbose_name='Concepto', max_length=100, default='indefinido')
    magnitud = models.FloatField(verbose_name='Magnitud')
    documento = models.ForeignKey(Documento, verbose_name='Documento', on_delete=models.CASCADE)
    extraccion = models.ForeignKey(Extracciones, verbose_name='Extracción', null=True, on_delete=models.CASCADE)

    class Meta:
        unique_together = ('cuenta', 'documento',)
        ordering = ['documento', 'cuenta']

    def __str__(self):
        return f'{self.documento} {self.cuenta} {str(self.magnitud)}'


class Contabilidad(models.Model):

    concepto = models.CharField(verbose_name='Concepto', max_length=100, blank=True)
    magnitud = models.FloatField(verbose_name='Magnitud')
    codigo = models.CharField(verbose_name='Codigo', max_length=10, null=True)
    #origen = models.CharField(verbose_name='Origen', max_length=10)
    documento = models.ForeignKey(Documento, verbose_name='Documento', on_delete=models.CASCADE)
    extraccion = models.ForeignKey(Extracciones, verbose_name='Extracción', on_delete=models.CASCADE)

    class Meta:
        unique_together = ('codigo', 'concepto', 'documento',)
        ordering = ['concepto', 'documento']

    def __str__(self):
        return f'{self.documento} {self.concepto} {str(self.magnitud)}'


class Ratio(models.Model):

    concepto = models.CharField(verbose_name='Concepto', max_length=100)
    magnitud = models.FloatField(verbose_name='Magnitud')
    unidades = models.CharField(verbose_name='Unidades', null=True, max_length=50)
    documento = models.ForeignKey(Documento, verbose_name='Documento', on_delete=models.CASCADE)
    extraccion = models.ForeignKey(Extracciones, verbose_name='Extracción', on_delete=models.CASCADE)

    class Meta:
        unique_together = ('concepto', 'documento',)
        ordering = ['documento', 'concepto']

    def __str__(self):
        return f'{self.documento} {self.concepto} {str(self.magnitud)}'


class Analitica(models.Model):

    cuenta = models.CharField(verbose_name='Cuenta', max_length=100)
    #concepto = models.CharField(verbose_name='Concepto', max_length=100, null=True)
    magnitud = models.FloatField(verbose_name='Magnitud')
    documento = models.ForeignKey(Documento, verbose_name='Documento', on_delete=models.CASCADE)
    extraccion = models.ForeignKey(Extracciones, verbose_name='Extracción', on_delete=models.CASCADE)

    class Meta:
        unique_together = ('cuenta', 'documento',)
        ordering = ['documento', 'cuenta']

    def __str__(self):
        return f'{self.documento} {self.cuenta} {str(self.magnitud)}'


class Contrato(models.Model):

    valoracion = ((0, 'Sin Valoración'), (1, 'Inviable'), (2, 'Alto Riesgo'), (3, 'Promedio'), (4, 'Bajo Riesgo'), (5, 'Seguro'),)
    periodificacion = ((1, 'Mensual'), (3, 'Trimestral'), (4, 'Cuatrimestral'), (6, 'Semestral'), (12, 'Anual'),)

    dictionary_periodificacion = {k: v for v, k in periodificacion}

    entidad = models.ForeignKey('equivalencias.Entidad', verbose_name='Entidad', on_delete=models.PROTECT)
    #codigo = models.CharField(verbose_name='Codigo', max_length=50)
    producto = models.ForeignKey('equivalencias.Producto', verbose_name='Producto', on_delete=models.PROTECT)
    inicio = models.DateField(verbose_name='Inicio')
    vencimiento = models.DateField(verbose_name='Vencimiento')
    carencia = models.IntegerField(verbose_name='Carencia', default=0)
    precio = models.FloatField(verbose_name='Precio')
    limite = models.FloatField(verbose_name='Límite')
    moneda = models.ForeignKey('equivalencias.Moneda', verbose_name='Moneda', on_delete=models.PROTECT, default='EUR')
    periodificacion = models.IntegerField(verbose_name='Periodificación', default=1, choices=periodificacion)
    valoracion = models.IntegerField(verbose_name='Valoración', choices=valoracion, default=0, null=True, blank=True)
    notas = models.CharField(verbose_name='Notas', max_length=1200, default='', null=True, blank=True)
    digitalizada = models.BooleanField(verbose_name='Doc Digitalizado', default=False, null=True, blank=True)
    minimis = models.BooleanField(verbose_name='Minimis', default=False, null=True, blank=True)

    @property
    def entidad_nombre(self):
        return self.entidad.nombre

    @property
    def tipo(self):
        if self.producto.tipo in productos_largo:
            return 'prestamos'
        elif self.producto.tipo in productos_compras:
            return 'compras'
        elif self.producto.tipo in productos_ventas:
            return 'ventas'
        else:
            return 'otros'

    @property
    def activo(self):
        return self.vencimiento >= datetime.date.today()

    @property
    def plazo(self):
        ### ES EL NÚMERO DE MESES, PERO COMPROBAR QUE PARA PERIODOS MENORES DE 1 MES SIGUE CONTANDO 1 MES O 1 LIQUIDACIÓN PENDIENTE
        return self.producto.tipo in productos_largo
               #(self.vencimiento.year - self.inicio.year) * 12 + self.vencimiento.month - self.inicio.month
    """
    @property
    def dispuesto_real(self):
        fecha = datetime.date.today()
        actual = Pool.objects.filter(contrato=self.id, documento__fecha__lte=fecha).latest('documento__fecha')
        actual = Pool.objects.filter(contrato=self.id, documento=actual.documento)
        if actual.count() == 2:
            if self.producto.tipo in productos_largo:
                return actual.aggregate(dispuesto_sum=Sum('dispuesto'))['dispuesto_sum']
            else: return actual[0].dispuesto
        else:
            return actual[0].dispuesto
    """

    @property
    def plazos_amortizacion(self):
        if self.producto.tipo in productos_largo:
            return math.ceil((numeroMeses(self.vencimiento, self.inicio) - int(self.carencia)) / int(self.periodificacion))
        else:
            return None

    @property
    def cuota(self):
        if self.producto.tipo in productos_largo:

            plazos_amortizacion = self.plazos_amortizacion
            precio = float(self.precio)
            limite = float(self.limite)
            #parcial = (float(self.precio) / 100.0 + 1.0) ** (int(self.periodificacion) / 12.0) - 1.0
            parcial = (precio/100.0/12.0 * int(self.periodificacion))
            if plazos_amortizacion == 0: plazos_amortizacion = 1
            if math.isclose(precio, 0):
                return round(limite / plazos_amortizacion, 2)
            else:
                return round(limite * parcial / (1.0 - (1.0 + parcial) ** (-1.0 * plazos_amortizacion)), 2)
        else:
            return None

    @property
    def pendiente_anual(self):
        if self.producto.tipo in productos_largo:
            periodificacion = int(self.periodificacion)
            carencia = int(self.carencia)
            inicio = self.inicio
            limite = float(self.limite)
            precio = float(self.precio)
            fecha = datetime.date.today()
            fecha_carencia = self.inicio + relativedelta(months=carencia)
            end_year = datetime.date(fecha.year, 12, 31)
            pagos_end_year = int(numeroMeses(end_year, inicio) / periodificacion)
            pagos_pasado = int(numeroMeses(fecha, inicio) / periodificacion)
            pagos_pendientes = pagos_end_year - pagos_pasado
            if fecha < fecha_carencia:
                if end_year < fecha_carencia:
                    pendiente = limite * precio / 100.0 / 12.0 * pagos_pendientes * periodificacion
                else:
                    pagos_carencia_pendientes = math.ceil(int(carencia - numeroMeses(fecha_carencia, fecha)) / periodificacion)
                    pendiente = limite * precio / 100.0 / 12.0 * pagos_carencia_pendientes * periodificacion
                    pendiente += math.ceil(numeroMeses(datetime.date(fecha.year, 12, 31), fecha_carencia) / periodificacion) * self.cuota
            else:
                pendiente = math.ceil(numeroMeses(datetime.date(fecha.year, 12, 31), fecha) / periodificacion) * self.cuota
            return pendiente
        else:
            return None

    @property
    def cuentas(self):
        pool = sorted(set(Pool.objects.filter(contrato=self).exclude(contrato=None).values_list('cuenta', flat=True)))
        cuentas = len(pool)
        """
        if cuentas > 1:
            final_pool = pool.copy()
            for cuenta in pool:
                if cuenta[0] == '1':
                    final_pool.remove(cuenta)
            pool = ''.join(final_pool)
        else:
            pool = None
        """
        return pool, cuentas

    @property
    def operacion(self):
        cirbe = Cirbe.objects.filter(contrato=self).exclude(contrato__isnull=True)
        print([element.contrato for element in cirbe])
        if cirbe:
            return {
                'id': cirbe[0].id,
                'operacion': cirbe[0].operacion,
                'entidad_nombre': cirbe[0].entidad.nombre
            }
        else:
            return None

    def descripcionValoracion(self):
        return self.get_valoracion_display()

    def descripcionPeriodificacion(self):
        return self.get_periodificacion_display()

    class Meta:
        ordering = ['entidad', 'inicio']

    def __str__(self):
        if self.pk:
            pool = Pool.objects.filter(contrato=self)
            conceptos = set(pool.values_list('concepto', flat=True))
            ids = pool.values_list('id', flat=True)
        else:
            pool = []
            conceptos = None
        return f'{self.pk} {self.entidad} {conceptos} {[str(element) for element in pool]}' # {self.entidad} {self.producto}'


class Pool(models.Model):

    cuenta = models.CharField(verbose_name='Cuenta', max_length=50)
    concepto = models.CharField(verbose_name='Concepto', max_length=250)
    dispuesto = models.FloatField(verbose_name='Dispuesto')
    contrato = models.ForeignKey(Contrato, verbose_name='Contrato', null=True, on_delete=models.SET_NULL)
    documento = models.ForeignKey(Documento, verbose_name='Documento', on_delete=models.CASCADE)
    extraccion = models.ForeignKey(Extracciones, verbose_name='Extracción', on_delete=models.CASCADE)

    class Meta:
        unique_together = ('cuenta', 'documento',)
        ordering = ['documento', 'cuenta']

    @property
    def plazo(self):
        return self.cuenta[0] == '1'

    def __str__(self):
        visible = f'{str(self.id)} {str(self.cuenta)} {str(self.documento.empresa.CIF)} '
        if self.contrato:
            visible += str(self.contrato.id)

        return visible


class Cirbe(models.Model):
    ### PRODUCTO ESTÁ DUPLICADO CON 'CONTRATO', PARA COMPROBAR QUE SE HA INTRODUCIDO CORRECTAMENTE AL EDITAR EL
    ### CONTRATO Y/O COMPROBAR QUE EL SILOGISMO DE CLASIFICACIÓN FUNCIONA CORRECTAMENTE

    entidad = models.ForeignKey('equivalencias.Entidad', verbose_name='Entidad', on_delete=models.PROTECT)
    operacion = models.CharField(verbose_name='Operación', max_length=100, null=True)
    tipo = models.ForeignKey('equivalencias.Tipo', verbose_name='Concepto', on_delete=models.PROTECT)
    producto = models.ForeignKey('equivalencias.Producto', verbose_name='Producto', on_delete=models.PROTECT)
    dispuesto = models.FloatField(verbose_name='Dispuesto')
    moneda = models.ForeignKey('equivalencias.Moneda', verbose_name='Moneda', on_delete=models.PROTECT)
    solCol = models.ForeignKey('equivalencias.SolCol', verbose_name='Solidario/Colaborativo', null=True, on_delete=models.PROTECT)
    participantes = models.IntegerField(verbose_name='#Participantes', default=0)
    real = models.ManyToManyField('equivalencias.Real')
    personal = models.ManyToManyField('equivalencias.Personal')
    plazo = models.ForeignKey('equivalencias.Plazo', verbose_name='Plazo', on_delete=models.PROTECT)
    natInterv = models.ForeignKey('equivalencias.NatInterv', verbose_name='Naturaleza Intervención', on_delete=models.PROTECT)
    situOper = models.ForeignKey('equivalencias.SituOper', verbose_name='Situación Operacional', on_delete=models.PROTECT, null=True)
    importes = models.FloatField(verbose_name='Importes')
    demora = models.FloatField(verbose_name='Intereses Demora')
    disponible = models.FloatField(verbose_name='Disponible')
    contrato = models.ForeignKey(Contrato, verbose_name='Contrato', null=True, on_delete=models.SET_NULL)
    documento = models.ForeignKey(Documento, verbose_name='Documento', on_delete=models.CASCADE)
    extraccion = models.ForeignKey(Extracciones, verbose_name='Extracción', on_delete=models.CASCADE)

    @property
    def disponible_repartido(self):
        try:
            dispuesto = Cirbe.objects.filter(contrato=self.contrato, documento=self.documento).aggregate(Sum('dispuesto'))
            operaciones = Cirbe.objects.filter(contrato=self.contrato, documento=self.documento).count()
            disponible = (self.contrato.limite - dispuesto)/operaciones
            return disponible
        except:
            #print('Excepción en el cálculo del disponible')
            exit(1)
        return 0

    class Meta:
        unique_together = ('documento', 'entidad', 'operacion',)
        ordering = ['documento', 'entidad', 'operacion']

    def __str__(self):
        visible = f'{str(self.id)} {str(self.entidad)} {self.operacion} {self.documento}'
        if self.contrato:
            visible += ' ' + str(self.contrato.id)
        return visible

    #@property
    ### ¿CÓMO HABRÍA DE SER ESTA PROPIEDAD CON MÚLTIPLES GARANTÍAS?
    #def garantias(self):
    #    return self.real.descripcion + self.personal.descripcion
    """
    @property
    def CortoLargoPlazo(self):

        productosLP =['AVAL', 'LEASING', 'PRESTAMO', 'HIPOTECA']

        if self.producto in productosLP and self.contrato.vencimiento - datetime.date.today() > datetime.timedelta(days = 365):
            return 'LP'
        else: return 'CP'
    """

