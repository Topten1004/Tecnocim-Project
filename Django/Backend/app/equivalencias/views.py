from drf_spectacular.utils import extend_schema, extend_schema_view, OpenApiParameter, OpenApiTypes
from rest_framework import viewsets
from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework.authentication import TokenAuthentication
from rest_framework.decorators import api_view
from rest_framework.permissions import IsAuthenticated

from equivalencias.models import *
from equivalencias import serializers
from equivalencias.models import Real, Personal
from equivalencias.serializers import EntidadSerializer


class EntidadViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.EntidadSerializer

    queryset = Entidad.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]

    @extend_schema(parameters=[OpenApiParameter(name='codigo', description='código de entidad prestataria', required=False, type=str),
                               OpenApiParameter(name='id', description='id de entidad prestataria', required=False, type=str)])
    def list(self, request, *args, **kwargs):

        try:
            codigo = self.request.query_params.get('codigo')
        except:
            codigo = False
        try:
            identificador = int(self.request.query_params.get('id'))
        except:
            identificador = False

        if codigo:
            entidad = Entidad.objects.get(codigo=codigo).id
            #print(codigo, entidad, dictionaries.entidades[entidad - 1])
            return Response([{'codigo': codigo, 'nombre': entidad.nombre}])
        else:
            if identificador:
                entidad = Entidad.objects.get(id=identificador).codigo
                #print(identificador, entidad, dictionaries.entidades[identificador-1])
                return Response([{'codigo': entidad.codigo, 'nombre': entidad.nombre}])
            return Response(EntidadSerializer(Entidad.objects.all(), many=True).data)


class MonedaViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.MonedaSerializer

    queryset = Moneda.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]


class NatIntervViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.NatIntervSerializer

    queryset = NatInterv.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]


class PersonalViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.PersonalSerializer

    queryset = Personal.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]


class PlazoViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.PlazoSerializer

    queryset = Plazo.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]


class RealViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.RealSerializer

    queryset = Real.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]


class SituOperViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.SituOperSerializer

    queryset = SituOper.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]


class SolColViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.SolColSerializer

    queryset = SolCol.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]

### EJEMPLO DE VIEWS QUE PERMITEN RECUPERAR LAS CHOICES. NECESARIO PARA TODAS LAS TABLAS DE EQUIVALENCIAS
@api_view(['GET'])
def SolColChoices(request):

    return Response(SolCol.tipos)


class ProductoViewSet(viewsets.ModelViewSet):

    serializer_class = serializers.ProductoSerializer

    queryset = Producto.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]


class TipoViewSet(viewsets.ModelViewSet):
    serializer_class = serializers.TipoSerializer

    queryset = Tipo.objects.all()
    authentication_classes = [TokenAuthentication]
    permission_classes = [IsAuthenticated]



