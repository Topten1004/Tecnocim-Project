from rest_framework.routers import DefaultRouter
from django.urls import path, include
from core import views

router = DefaultRouter()
router.register(r'empresas', views.EmpresaViewSet)
router.register(r'documentos', views.DocumentoViewSet)
router.register(r'extracciones', views.ExtraccionesViewSet)
router.register(r'extracciones_errores', views.Extracciones_ErroresViewSet)
router.register(r'contabilidad', views.ContabilidadViewSet)
router.register(r'ratio', views.RatioViewSet)
router.register(r'crudos', views.CrudosViewSet)
router.register(r'analitica', views.AnaliticaViewSet)
router.register(r'contrato', views.ContratoViewSet)
router.register(r'pool', views.PoolViewSet)
router.register(r'cirbe', views.CirbeViewSet)
router.register(r'cirbe_sin_contrato', views.CirbeContratoViewSet)
#router.register(r'contratoChoices', views.ContratoChoicesViewSet)

app_name = 'base'

urlpatterns = [path('', include(router.urls)),
               path('servicioDeuda/', views.ServicioDeudaView, name='poolView'),
               path('deuda_agregada/', views.deuda_agregada, name='deuda_agregada'),
               #path('poolAlia/', views.pool_for_alia, name='poolAlia'),
               #path('ratioshorizontales/', views.RatiosHorizontales, name='ratios_horizontales'),
               #path('ratiosverticales/', views.RatiosVerticales, name='ratios_verticales'),
               path('ratiosintegrados/', views.RatiosIntegrados, name='ratios_integrados'),
               path('cargacontratos/', views.upload_contratos, name='carga_contratos')
               ]
