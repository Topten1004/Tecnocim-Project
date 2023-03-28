from django.urls import path
from sources import views

app_name = 'sources'

urlpatterns = [path('ratios/', views.ratios, name='ratios'),
               path('upload/', views.upload, name='upload'),
               path('balance/', views.balancePyG, name='balance'),
               #path('balanceSingle/', views.balancePyGSingle, name='balanceSingle'),
               path('reviewPool/', views.review_pool, name='reviewPool'),
               path('sinContrato/', views.get_cuentas_sin_contrato, name='sinContrato'),
               path('analitica/', views.analitica, name='analitica'),
               path('cobertura/', views.cobertura, name='cobertura')]