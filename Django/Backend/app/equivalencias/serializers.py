from rest_framework import serializers

from core.models import Empresa
from equivalencias.models import Tipo, NatInterv, SolCol, Real, Personal, SituOper, Moneda, Entidad, Producto


class EmpresaSerializer(serializers.ModelSerializer):
    class Meta:
        model = Empresa
        fields = '__all__'
        read_only_fields = ['id']


class EntidadSerializer(serializers.ModelSerializer):
    class Meta:
        model = Entidad
        fields = '__all__'
        read_only_fields = ['id']


class MonedaSerializer(serializers.ModelSerializer):
    class Meta:
        model = Moneda
        fields = '__all__'
        read_only_fields = ['id']


class NatIntervSerializer(serializers.ModelSerializer):
    class Meta:
        model = NatInterv
        fields = '__all__'
        read_only_fields = ['id']


class PersonalSerializer(serializers.ModelSerializer):
    class Meta:
        model = Personal
        fields = '__all__'
        read_only_fields = ['id']


class PlazoSerializer(serializers.ModelSerializer):
    class Meta:
        model = SituOper
        fields = '__all__'
        read_only_fields = ['id']


class ProductoSerializer(serializers.ModelSerializer):

    class Meta:
        model = Producto
        fields = '__all__'
        read_only_fields = ['id']


class RealSerializer(serializers.ModelSerializer):
    class Meta:
        model = Real
        fields = '__all__'
        read_only_fields = ['id']


class SituOperSerializer(serializers.ModelSerializer):
    class Meta:
        model = SituOper
        fields = '__all__'
        read_only_fields = ['id']


class SolColSerializer(serializers.ModelSerializer):

    choices = serializers.SerializerMethodField()

    class Meta:
        model = SolCol
        fields = '__all__'
        read_only_fields = ['id']

    def get_choices(self, obj):
        return SolCol.tipos

"""
class SolColChoicesSerializer(serializers.ModelSerializer):

    choices = serializers.SerializerMethodField()

    class Meta:
        model = SolCol
        fields = '__all__' #['id']
        read_only_fields = ['id']

    def get_choices(self, obj):
        return SolCol.tipos
"""


class TipoSerializer(serializers.ModelSerializer):

    class Meta:
        model = Tipo
        fields = '__all__'
        read_only_fields = ['id']

