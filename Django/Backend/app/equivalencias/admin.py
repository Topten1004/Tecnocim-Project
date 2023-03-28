from django.contrib import admin

from equivalencias import models

admin.site.register(models.Entidad)
admin.site.register(models.Tipo)
admin.site.register(models.Plazo)
admin.site.register(models.Producto)
admin.site.register(models.Moneda)
admin.site.register(models.SolCol)
admin.site.register(models.Real)
admin.site.register(models.Personal)
admin.site.register(models.NatInterv)
admin.site.register(models.SituOper)