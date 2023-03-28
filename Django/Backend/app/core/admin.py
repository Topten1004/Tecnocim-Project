from django.contrib import admin
from django.contrib.auth.admin import UserAdmin as BaseUserAdmin
from django.utils.translation import gettext_lazy as _

from core import models


class UserAdmin(BaseUserAdmin):
    """Define the admin pages for users."""
    ordering = ['id']
    list_display = ['email', 'name']
    fieldsets = (
        (None, {'fields': ('email', 'password')}),
        (_('Personal Info'), {'fields': ('name',)}),
        (
            _('Permissions'),
            {
                'fields': (
                    'is_active',
                    'is_staff',
                    'is_superuser',
                )
            }
        ),
        (_('Important dates'), {'fields': ('last_login',)}),
    )
    readonly_fields = ['last_login']

    add_fieldsets = (
        (None, {
            'classes': ('wide',),
            'fields': (
                'email',
                'password1',
                'password2',
                'name',
                'is_active',
                'is_staff',
                'is_superuser',
            ),
        }),
    )

class EmpresaAdmin(admin.ModelAdmin):
    list_display = ('__str__', 'get_config_file_link', )
    exclude = ('configFile', )

    def get_config_file_link(self, obj):
        from django.utils.html import format_html
        if obj.configFile:
            path = '{0}{1}'.format('/documents/', obj.configFile.split('/documents/')[1])
            return format_html("<a href='{0}' download>{0}</a>".format(path))
        return None

class DocumentoAdmin(admin.ModelAdmin):
    search_fields = ('documento', )

class CrudosAdmin(admin.ModelAdmin):
    search_fields = ('documento__documento', 'cuenta', )

admin.site.register(models.User, UserAdmin)
admin.site.register(models.Empresa, EmpresaAdmin)
admin.site.register(models.Documento, DocumentoAdmin)
admin.site.register(models.Extracciones)
admin.site.register(models.Extracciones_Errores)
admin.site.register(models.Contabilidad)
admin.site.register(models.Crudos, CrudosAdmin)
admin.site.register(models.Analitica)
admin.site.register(models.Pool)
admin.site.register(models.Contrato)
admin.site.register(models.Cirbe)
admin.site.register(models.Ratio)