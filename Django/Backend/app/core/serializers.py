from rest_framework import serializers

from core.models import *


class AnaliticaSerializer(serializers.ModelSerializer):

    class Meta:
        model = Analitica
        fields = '__all__'
        read_only_fields = ['id', 'cuenta', 'concepto', 'documento']
        depth = 1


class CirbeSerializer(serializers.ModelSerializer):

    class Meta:
        model = Cirbe
        fields = '__all__'
        read_only_fields = ['id']
        depth = 1


class ContabilidadSerializer(serializers.ModelSerializer):

    class Meta:
        model = Contabilidad
        fields = '__all__'
        read_only_fields = ['id', 'concepto', 'codigo', 'origen']
        depth = 1


class ContratoSerializer(serializers.ModelSerializer):

    entidad_nombre = serializers.ReadOnlyField()
    cuentas = serializers.ReadOnlyField()
    operacion = serializers.ReadOnlyField()
    tipo = serializers.ReadOnlyField()
    #dispuesto_real = serializers.ReadOnlyField()
    plazos_amortizacion = serializers.ReadOnlyField()
    cuota = serializers.ReadOnlyField()
    pendiente_anual = serializers.ReadOnlyField()

    class Meta:
        model = Contrato
        fields = '__all__'
        #read_only_fields = ['id']


class ContratoChoicesSerializer(serializers.ModelSerializer): #Serializer): ## PARA USAR SE HA DE CREAR UN CONTRATO FAKE, USARLO Y NO SALVARLO

    #valoracion = serializers.ChoiceField(choices=Contrato.Valoracion)
    #periodificacion = serializers.ChoiceField(choices=Contrato.Periodificacion)

    choices = serializers.SerializerMethodField()

    class Meta:
        model = User
        fields = '__all__'

    def get_choices(self, obj):
        valoracionChoices = [c[1] for c in Contrato.valoracion]
        periodificacionChoices = [c[1] for c in Contrato.periodificacion]
        return valoracionChoices, periodificacionChoices


class CrudosSerializer(serializers.ModelSerializer):

    class Meta:
        model = Crudos
        fields = '__all__'
        read_only_fields = ['id', 'cuenta', 'concepto', 'documento']


class DocumentoSerializer(serializers.ModelSerializer):

    class Meta:
        model = Documento
        fields = '__all__'
        read_only_fields = ['id']


class EmpresaSerializer(serializers.ModelSerializer):

    documents = serializers.ReadOnlyField()

    class Meta:
        model = Empresa
        exclude = ('configFile', )

class ExtraccionesSerializer(serializers.ModelSerializer):

    documento = serializers.ReadOnlyField()

    class Meta:
        model = Empresa
        fields = '__all__'

class Extracciones_ErroresSerializer(serializers.ModelSerializer):

    class Meta:
        model = Empresa
        fields = '__all__'

"""
class OperacionSerializer(serializers.ModelSerializer):

    class Meta:
        model = Operacion
        fields = '__all__'
        read_only_fields = ['id']
"""

class PoolSerializer(serializers.ModelSerializer):

    contrato = ContratoSerializer()

    class Meta:
        model = Pool
        fields = '__all__' #('id', 'cuenta', 'concepto', 'dispuesto', 'contrato', 'documento')
        read_only_fields = ['id'] #, 'contrato.id', 'documento.id']
        depth = 1


class RatioSerializer(serializers.ModelSerializer):

    class Meta:
        model = Ratio
        fields = '__all__'
        read_only_fields = ['id']
        depth = 1

