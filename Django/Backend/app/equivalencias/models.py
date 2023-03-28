from django.db import models

from core import dictionaries


class Entidad(models.Model):

    dictionary = dict(dictionaries.entidades)

    codigo = models.CharField(verbose_name='Código', max_length=5, unique=True, null=True, default=None, blank=True)
    nombre = models.CharField(verbose_name='Nombre', max_length=100)

    class Meta:
        ordering = ['codigo']

    def save(self, *args, **kwargs):
        if not self.codigo:
            if self.pk:
                entidad = Entidad.objects.exclude(pk=self.pk).filter(nombre=self.nombre) # Si actualizamos nos excluimos por si es mismo nombre que ya tiene
            else:
                entidad = Entidad.objects.filter(nombre=self.nombre)
            if entidad:
                raise Exception('Esta entidad ya existe')

        super(Entidad, self).save(*args, **kwargs)


    @property
    def endings(self):
        endings = []
        for x in Entidad.dictionary.values():
            final = x.split(',')[-1]
            if final.strip() not in endings and len(x.split(',')) > 1:
                endings.append(final.strip())

        # Se añade a la lista de strings finales el caracter vacío, para propósitos de filtrado
        endings.append('')
        endings.append('SUCURSAL')
        endings.append('EN')
        endings.append('ESPAÑA')
        return endings

    @property
    def fullName(self):
        return str(self.codigo)+' '+Entidad.dictionary[self.codigo]

    @property
    def lowerName(self):
        return str(self.codigo)+' '+Entidad.dictionary[self.codigo].lower().split(',')[0]

    @property
    def bancaria(self):
        return not self.codigo

    def __str__(self):
        return f'{self.codigo} {self.nombre}'


class Moneda(models.Model):

    tipos = [('AED', 'AED'), ('AOA', 'AOA'), ('ARS', 'ARS'), ('ATS', 'ATS'), ('AUD', 'AUD'), ('BEF', 'BEF'), ('BGN', 'BGN'),
             ('BRL', 'BRL'), ('CAD', 'CAD'), ('CHF', 'CHF'), ('CLP', 'CLP'), ('CNY', 'CNY'), ('COP', 'COP'), ('CYP', 'CYP'),
             ('CZK', 'CZK'), ('DEM', 'DEM'), ('DKK', 'DKK'), ('DZD', 'DZD'), ('EEK', 'EEK'), ('EGP', 'EGP'), ('ESB', 'ESB'),
             ('ESP', 'ESP'), ('EUR', 'EUR'), ('FIM', 'FIM'), ('FRF', 'FRF'), ('GBP', 'GBP'), ('GRD', 'GRD'), ('HKD', 'HKD'),
             ('HRK', 'HRK'), ('HUF', 'HUF'), ('IDR', 'IDR'), ('IEP', 'IEP'), ('ILS', 'ILS'), ('INR', 'INR'), ('ISK', 'ISK'),
             ('ITL', 'ITL'), ('JPY', 'JPY'), ('KES', 'KES'), ('KRW', 'KRW'), ('KWD', 'KWD'), ('LBP', 'LBP'), ('LTL', 'LTL'),
             ('LUF', 'LUF'), ('LVL', 'LVL'), ('MAD', 'MAD'), ('MTL', 'MTL'), ('MXN', 'MXN'), ('MYR', 'MYR'), ('NLG', 'NLG'),
             ('NOK', 'NOK'), ('NZD', 'NZD'), ('PEN', 'PEN'), ('PHP', 'PHP'), ('PKR', 'PKR'), ('PLN', 'PLN'), ('PTE', 'PTE'),
             ('QAR', 'QAR'), ('RON', 'RON'), ('RUB', 'RUB'), ('RUR', 'RUR'), ('SAR', 'SAR'), ('SEK', 'SEK'), ('SGD', 'SGD'),
             ('SIT', 'SIT'), ('SKK', 'SKK'), ('THB', 'THB'), ('TND', 'TND'), ('TRY', 'TRY'), ('USD', 'USD'), ('US2', 'US2'),
             ('VEB', 'VEB'), ('VEF', 'VEF'), ('XAF', 'XAF'), ('XEU', 'XEU'), ('ZAR', 'ZAR'), ('L00', 'L00')]

    tipo = models.CharField(verbose_name='Moneda', max_length=3, choices=tipos, primary_key=True)


class NatInterv(models.Model):

    tipos = [('T11', 'TITULAR DE RIESGO DIRECTO'), ('T12', 'TITULAR DE RIESGO DIRECTO úNICO'),
             ('T13', 'TITULAR DE RIESGO DIRECTO SOLIDARIO'),
             ('T14', 'TIT. RIESGO DIR.MANCOMUNADO NO SOLID. CON OTROS TIT.MANCOMUNADOS'),
             ('T15', 'TIT. RIESGO DIR.MANCOMUNADO NO SOLID. CON OTROS TIT.MANCOMUNADOS'),
             ('T16', 'TITULAR DE RIESGO POR SUBVENCIONAR EL PRINCIPAL'),
             ('T17', 'TITULAR DE RIESGO POR SUBVENCIONAR EXCLUSIVAMENTE INTERESES'), ('T19', 'GARANTE'),
             ('T20', 'GARANTE SOLIDARIO'), ('T21', 'GARANTE NO SOLIDARIO'),
             ('T22', 'COMPROMISO DE FIRMA EN EFECTOS'),
             ('T23', 'COMPROMISO DE FIRMA EN EFECTOS'), ('T24', 'CONTRAPARTE EN UN DERIVADO DE CRéDITO COMPRADO'),
             ('T25', 'ASEGURADOR O AFIANZADOR'), ('T26', 'TERCERO COMPROMETIDO A PAGAR IMP. EN OPER. ARRENDA. FINANCIERO'),
             ('T27', 'ENTIDAD AGENTE DEL PRÉSTAMO SINDICADO'), ('T28', 'SOCIEDAD INSTRUMENTAL TENEDORA'),
             ('T29', 'ENTIDAD QUE GESTIONA LA OPERACIÓN'),
             ('T30', 'ENTIDAD DECLARANTE QUE CONCEDE DE FORMA SOLIDARIA LA OPERACIóN'),
             ('T31', 'ENTIDAD EMISORA DE LOS VALORES ADQUIRIDOS TEMPORALMENTE'),
             ('T32', 'ENTIDAD EMISORA DE LOS VALORES PRESTADOS'),
             ('T69', 'ENTIDAD EMISORA DE ACTIVOS FINANCIEROS RECIBIDOS EN GARANTíA'),
             ('T71', 'TITULAR DE RIESGO POR SUBVENCIONAR PRINCIPAL E INTERESES')]

    tipo = models.CharField(verbose_name='Naturaleza Intervención', max_length=3, choices=tipos, primary_key=True)


class Personal(models.Model):

    tipos = [('G00', 'SIN GARANTÍA PERSONAL'), ('G01', 'GARANTÍA DE ADMINISTRACIONES PÚBLICAS'), ('G02', 'GARANTÍA CESCE'),
             ('G03', 'GAR. EMPR. PÚBL. CUYA ACTIV. PRINC. SEA ASEGURAMIENTO/AVAL DE CRÉDITO'),
             ('G04', 'GARANTÍA DE ENTIDAD DE CRÉDITO RESIDENTE EN ESPAÑA'),
             ('G05', 'GARANTÍA DE ENTIDAD DE CRÉDITO NO RESIDENTE EN ESPAÑA'),
             ('G06', 'GARANTÍA DE SOCIEDAD DE GARANTÍA RECÍPROCA'), ('G07', 'GARANTÍA DE OTRA ENTIDAD DECLARANTE A LA CIR'),
             ('G08', 'GARANTÍA DEL RESTO DE LAS PERSONAS JURÍDICAS'), ('G09', 'GARANTÍA DE HOGARES'),
             ('G10', 'RESTO DE GARANTÍAS PERSONALES'), ('G11', 'HIPOTECA INMOBILIARIA (PRIMERA HIPOTECA)'),
             ('G12', 'HIPOTECA INMOBILIARIA (RESTO DE LAS HIPOTECAS)'),
             ('G13', 'GARANTÍA PIGNORATICIA (ACTIVOS FINANCIEROS)'), ('G14', 'HIPOTECA NAVAL'),
             ('G15', 'OPERACIÓN INSCRITA EN EL REGISTRO DE VENTAS A PLAZO DE BIENES MUEBLES'),
             ('G16', 'GARANTÍAS REALES DISTINTAS DE LAS ANTERIORES'), ('G17', 'ARRENDAMIENTO FINANCIERO DE BIENES INMUEBLES'),
             ('G18', 'ARRENDAMIENTO FINANCIERO DEL RESTO DE LOS BIENES'), ('G19', 'SIN GARANTÍA REAL'),
             ('G20', 'HIPOTECA INMOBILIARIA'), ('G21', 'RESTO DE LAS GARANTÍAS REALES'),
             ('G22', 'GA. CESCE/EMPR. PÚBL. CUYA ACT. PRINC. SEA ASEGURAMIENTO/AVAL CRÉDITO'),
             ('G33', 'GARANTÍA DE ENTIDAD DECLARANTE A LA CIR'), ('E14', 'TOTAL'), ('E15', 'PARCIAL')]

    #deuda = models.ForeignKey(Cirbe, verbose_name='Producto', on_delete=models.CASCADE)
    tipo = models.CharField(verbose_name='Garantías Personales', max_length=3, choices=tipos, unique=True)

    class Meta:
        ordering = ['tipo']

    diccionario = dict(tipos)

    @property
    def descripcion(self):
        return Personal.diccionario[self.tipo]


class Plazo(models.Model):

    tipos = [('L01', 'HASTA TRES MESES (EXCEPTO A LA VISTA)'), ('L02', 'MÁS DE TRES MESES Y HASTA SEIS MESES'),
             ('L03', 'MÁS DE SEIS MESES Y HASTA UN AÑO'), ('L04', 'MÁS DE UN AÑO Y HASTA DOS AÑOS'),
             ('L05', 'MÁS DE DOS AÑOS Y HASTA CINCO AÑOS'), ('L06', 'MÁS DE CINCO AÑOS'),
             ('L07', 'VENCIMIENTO INDETERMINADO'), ('L08', 'HASTA TRES MESES'),
             ('L09', 'MÁS DE TRES MESES Y HASTA UN AÑO'), ('L10', 'MÁS DE UN AÑO Y HASTA 5 AÑOS')]

    tipo = models.CharField(verbose_name='Plazo', max_length=3, choices=tipos, primary_key=True)

    diccionario = dict(tipos)

    @property
    def descripcion(self):
        return Plazo.diccionario[self.tipo]


class Producto(models.Model):

    choices = [("anticipo facturas", 'ANTICIPO-FACTURAS'), ("anticipo pagos domiciliados", 'ANTICIPO-DOMICILIADOS'),
               ("anticipo recibos", 'ANTICIPO-RECIBOS'), ('aval', 'AVAL'), ('click', 'CLICK - PAGO PROVEEDORES'),
               ("confirming estandar", 'CONFIRMING'), ("confirming pronto pago", 'CONFIRMING-PRONTO PAGO'),
               ("documentario exportaciones", 'DOCUMENTARIO-EXPORTACIONES'),
               ("documentario importaciones", 'DOCUMENTARIO-IMPORTACIONES'), ("descuento pagares", 'DESCUENTO-PAGARÉS'),
               ("descuento pagares no orden", 'DESCUENTO-PAGARÉS NO ORDEN'), ("factoring con recurso", 'FACTORING CON RECURSO'),
               ("factoring sin recurso", 'FACTORING SIN RECURSO'), ("financiacion importaciones", 'FINANCIACIÓN IMPORTACIONES'),
               ('hipoteca', 'HIPOTECA INMOBILIARIA'), ('leasing', 'LEASING'), ('multilinea', 'MULTILÍNEA'),
               ('poliza', 'PÓLIZA DE CRÉDITO'), ('prestamo', 'PRÉSTAMO'), ('tarjeta', 'TARJETAS'), ('excepcion', 'EXCEPCIÓN'),
               ('sin', 'SIN CLASIFICACIÓN')]

    diccionario = dict(choices)

    tipo = models.CharField(verbose_name='Producto', max_length=50, choices=choices, primary_key=True)

    class Meta:
        ordering = ['tipo']

    @property
    def descripcion(self):
        return Producto.diccionario[self.tipo]

    @property
    def plazo(self):
        return self.tipo in ['aval', 'documentario', 'prestamo', 'leasing', 'hipoteca']

    def __str__(self):
        return f'{self.tipo}'

class Real(models.Model):

    tipos = [('G00', 'SIN GARANTÍA PERSONAL'), ('G01', 'GARANTÍA DE ADMINISTRACIONES PÚBLICAS'), ('G02', 'GARANTÍA CESCE'),
            ('G03', 'GAR. EMPR. PÚBL. CUYA ACTIV. PRINC. SEA ASEGURAMIENTO/AVAL DE CRÉDITO'),
            ('G04', 'GARANTÍA DE ENTIDAD DE CRÉDITO RESIDENTE EN ESPAÑA'),
            ('G05', 'GARANTÍA DE ENTIDAD DE CRÉDITO NO RESIDENTE EN ESPAÑA'),
            ('G06', 'GARANTÍA DE SOCIEDAD DE GARANTÍA RECÍPROCA'),
            ('G07', 'GARANTÍA DE OTRA ENTIDAD DECLARANTE A LA CIR'), ('G08', 'GARANTÍA DEL RESTO DE LAS PERSONAS JURÍDICAS'),
            ('G09', 'GARANTÍA DE HOGARES'), ('G10', 'RESTO DE GARANTÍAS PERSONALES'),
            ('G11', 'HIPOTECA INMOBILIARIA (PRIMERA HIPOTECA)'), ('G12', 'HIPOTECA INMOBILIARIA (RESTO DE LAS HIPOTECAS)'),
            ('G13', 'GARANTÍA PIGNORATICIA (ACTIVOS FINANCIEROS)'), ('G14', 'HIPOTECA NAVAL'),
            ('G15', 'OPERACIÓN INSCRITA EN EL REGISTRO DE VENTAS A PLAZO DE BIENES MUEBLES'),
            ('G16', 'GARANTÍAS REALES DISTINTAS DE LAS ANTERIORES'), ('G17', 'ARRENDAMIENTO FINANCIERO DE BIENES INMUEBLES'),
            ('G18', 'ARRENDAMIENTO FINANCIERO DEL RESTO DE LOS BIENES'), ('G19', 'SIN GARANTÍA REAL'),
            ('G20', 'HIPOTECA INMOBILIARIA'), ('G21', 'RESTO DE LAS GARANTÍAS REALES'),
            ('G22', 'GA. CESCE/EMPR. PÚBL. CUYA ACT. PRINC. SEA ASEGURAMIENTO/AVAL CRÉDITO'),
            ('G33', 'GARANTÍA DE ENTIDAD DECLARANTE A LA CIR'), ('E14', 'TOTAL'), ('E15', 'PARCIAL')]

    #deuda = models.ForeignKey(Cirbe, verbose_name='Producto', on_delete=models.CASCADE)
    tipo = models.CharField(verbose_name='Garantías Reales', max_length=3, choices=tipos, unique=True)

    class Meta:
        ordering = ['tipo']

    diccionario = dict(tipos)

    @property
    def descripcion(self):
        return Real.diccionario[self.tipo]


class SituOper(models.Model):

    tipos = [('I15', 'OPERACIÓN DUDOSA (SIN INCUMPLIMIENTOS O CON INCUMPLIM. HASTA 90 DÍAS)'),
             ('I16', 'OPERACIÓN REESTRUCTURADA AL AMPARO DEL REAL DECRETO-LEY 6/2012'),
             ('I17', 'OPER. DE REFINANCIACIÓN, REFINANCIADA O REESTRUCT. POR OTROS MOTIVOS'),
             ('I18', 'OPERACIÓN INCLUIDA EN UN CONVENIO DE ACREEDORES'),
             ('I19', 'OTRAS SITUACIONES CON INCUMPL. ENTRE MÁS DE 90 DÍAS Y HASTA 4 AÑOS'),
             ('I20', 'OTRAS SITUACIONES CON INCUMPLIMIENTOS DE MÁS DE CUATRO AÑOS'),
             ('I21', 'OPERACIÓN EN SUSPENSO'), ('I22', 'RESTO DE LAS SITUACIONES')]

    tipo = models.CharField(verbose_name='Situación Operacional', max_length=3, choices=tipos, primary_key=True)

    diccionario = dict(tipos)

    @property
    def descripcion(self):
        return SituOper.diccionario[self.tipo]


class SolCol(models.Model):

    tipos = (('T33', 'SOLIDARIO',), ('T34', 'COLECTIVO',),)
    tipo = models.CharField(verbose_name='Solidario/Colaborativo', max_length=3, choices=tipos, primary_key=True)

    diccionario = dict(tipos)

    @property
    def descripcion(self):
        return SolCol.diccionario[self.tipo]


class Tipo(models.Model):

    tipos = [('VA0', 'CRÉDITO FINANCIERO. RESTO DE LAS CUENTAS DE CORRESPONSALÍA'),
             ('VBD', 'CRÉDITO FINANCIERO. DERECHOS DE COBRO SOBRE TARIFAS REGULADAS'),
             ('VB1', 'PRÉST. RECOMPRA INVERSA. GAR. EFECTIVO ENTREG. EN PERMUTAS DE VALORES'),
             ('VB2', 'PRÉST. RECOMPRA INVERSA. RESTO DE LOS PRÉSTAMOS DE RECOMPRA INVERSA'),
             ('VB3', 'AVALES POR CANTIDADES ANTICIP. EN LA CONSTRUCCIÓN Y VENTA VIVIENDAS'),
             ('VB4', 'CRÉDITO FINANCIERO. DERECHOS DE COBRO POR SUBVENCIONES'), ('V19', 'VALORES PRESTADOS'),
             ('V24', 'CRÉDITO COMERCIAL CON RECURSO'), ('V25', 'CRÉDITO COMERCIAL CON RECURSO. DESCUENTO DE PAPEL COMERCIAL'),
             ('V26', 'CRÉDITO COMERCIAL CON RECURSO. RESTO DE LAS OPERACIONES'), ('V27', 'CRÉDITO COMERCIAL SIN RECURSO'),
             ('V28', 'CRÉDITO COMERCIAL SIN RECURSO. PAGO A PROVEEDORES CON INVERSIÓN'),
             ('V29', 'CRÉDITO COMERCIAL SIN RECURSO. PAGO A PROVEEDORES SIN INVERSIÓN'),
             ('V30', 'CRÉDITO COMERCIAL SIN RECURSO. FACTORING CON INVERSIÓN'),
             ('V31', 'CRÉDITO COMERCIAL SIN RECURSO. FACTORING SIN INVERSIÓN'), ('V32', 'CRÉDITO FINANCIERO'),
             ('V33', 'CRÉDITO FINANCIERO. CUENTAS DE CRÉDITO CON DISPOSICIONES POR ETAPAS'),
             ('V34', 'CRÉDITO FINANCIERO. RESTO DE LAS CUENTAS DE CRÉDITO'), ('V35', 'CRÉDITO FINANCIERO. EFECTOS FINANCIEROS'),
             ('V36', 'CRÉDITO FINANCIERO. PRÉSTAMOS CON DISPOSICIONES POR ETAPAS'),
             ('V37', 'CRÉDITO FINANCIERO. HIPOTECAS INVERSAS'),
             ('V38', 'CRÉDITO FINANCIERO. OTROS PRÉST. CON ENTREGAS APLAZADAS DE PRINCIPAL'),
             ('V39', 'CRÉDITO FINANCIERO. PRÉSTAMOS HÍBRIDOS'), ('V40', 'CRÉDITO FINANCIERO. RESTO DE LOS PRÉSTAMOS A PLAZO'),
             ('V41', 'CRÉDITO FINANCIERO. TARJETAS DE CRÉDITO'), ('V42', 'CRÉDITO FINANCIERO. CUENTAS CORRIENTES O DE AHORRO'),
             ('V43', 'CRÉDITO FINANCIERO. CUENTAS MUTUAS'), ('V44', 'CRÉDITO FINANCIERO. DESCUBIERTOS'),
             ('V45', 'CRÉDITO FINANCIERO. EXCEDIDOS EN CUENTAS DE CRÉDITO'),
             ('V46', 'CRÉDITO FINANCIERO. ANTICIPOS DE PENSIÓN O NÓMINA'),
             ('V47', 'CRÉDITO FINANCIERO. ACTIVOS PROCEDENTES DE OPERAC. FUERA DE BALANCE'),
             ('V48', 'CRÉDITO FINANCIERO. DERIVADOS IMPAGADOS'),
             ('V49', 'CRÉDITO FINANCIERO. RESTO DE LOS PRÉSTAMOS A LA VISTA'),
             ('V51', 'ARRENDAMIENTO FINANCIERO PARA EL ARRENDATARIO'), ('V52', 'ARRENDAMIENTO OPERATIVO PARA EL ARRENDATARIO'),
             ('V53', 'PRÉSTAMOS DE RECOMPRA INVERSA'), ('V54', 'VALORES REPRESENTATIVOS DE DEUDA'), ('V55', 'AVAL FINANCIERO'),
             ('V56', 'AVAL FINANCIERO ANTE ENTIDAD DECLARANTE'),
             ('V57', 'AVAL FINANCIERO SOLIDARIO CON OTRAS ENTIDADES DECLARANTES'), ('V58', 'RESTO DE AVALES FINANCIEROS'),
             ('V59', 'DERIVADO DE CRÉDITO (PROTECCIÓN VENDIDA)'),
             ('V60', 'DERIVADO DE CRÉDITO (PROTECCIÓN VENDIDA) ANTE ENTIDAD DECLARANTE'),
             ('V61', 'RESTO DE DERIVADO DE CRÉDITO (PROTECCIÓN VENDIDA)'), ('V62', 'DEPÓSITOS A FUTURO'),
             ('V63', 'AVALES Y CAUCIONES NO FINANCIEROS PRESTADOS'),
             ('V64', 'RESTO DE GARANTÍAS NO FINANCIERAS CONCEDIDAS. ANTE ENTIDAD DECLARANT'),
             ('V65', 'RESTO GAR. NO FINAN. CONCEDIDAS. SOLIDARIOS CON OTRAS ENTIDADES DECL.'),
             ('V66', 'RESTO DE GARANTÍAS NO FINANCIERAS CONCEDIDAS. RESTO'), ('V67', 'CRÉDITOS DOCUMENTARIOS IRREVOCABLES'),
             ('V68', 'DISPONIBLES EN OTROS COMPROMISOS'),
             ('V69', 'DISPONIBLES EN OTROS COMPROMISOS. PÓLIZAS DE RIESGO GLOBAL MULTIUSO'),
             ('V70', 'DISPONIBLES EN OTROS COMPROMISOS. LÍNEA DE AVALES'),
             ('V71', 'DISPONIBLES EN OTROS COMPROMISOS. LÍNEA DE CRÉDITOS DOCUMENTARIOS'),
             ('V72', 'DISPONIBLES EN OTROS COMPROMISOS. CRÉDITO POR DISPOSICIONES')]

    tipo = models.CharField(verbose_name='Tipo', max_length=3, choices=tipos, primary_key=True)

    diccionario = dict(tipos)

    @property
    def descripcion(self):
        return self.diccionario[self.tipo]
        #return Tipo.diccionario[self.tipo]
