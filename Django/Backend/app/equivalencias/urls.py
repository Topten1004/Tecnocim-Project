from rest_framework.routers import DefaultRouter
from django.urls import path, include
from equivalencias import views

router = DefaultRouter()

router.register(r'tipo', views.TipoViewSet)
router.register(r'natinterv', views.NatIntervViewSet)
router.register(r'solcol', views.SolColViewSet)
router.register(r'real', views.RealViewSet)
router.register(r'personal', views.PersonalViewSet)
router.register(r'plazo', views.PlazoViewSet)
router.register(r'situoper', views.SituOperViewSet)
router.register(r'moneda', views.MonedaViewSet)
router.register(r'entidad', views.EntidadViewSet)
router.register(r'producto', views.ProductoViewSet)
#router.register(r'contratoChoices', views.ContratoChoicesViewSet)

app_name = 'complement'

urlpatterns = [path('', include(router.urls)),
               path('solcolchoices', views.SolColChoices, name='solcolchoices')]
#path('entidad', views.EntidadViewSet.as_view(), name='entidad'),