import os

from django.db.models.signals import post_save, post_delete
# I have used django user model to use post save, post delete.
from django.contrib.auth.models import User
from django.dispatch import receiver

from core.models import Pool, Contrato, Documento


@receiver(post_delete, sender=Pool)
def contrato_delete(sender, pool, *args, **kwargs):
    if pool.contrato:
        cuentas = Pool.objects.filter(contrato=pool.contrato).count()
        if cuentas == 0:
            Contrato.objects.get(id=pool.contrato.id).delete()
            #print('Contrato borrado')
    #print('Contrato todavía tiene vínculos con Pool')
    pass

"""
@receiver(post_delete, sender=Documento)
def documento_delete(sender, documento, *args, **kwargs):
    original = os.path.basename(documento.name)
    extension = os.path.splitext(documento.name)[1]
    print(original, extension)
    if extension == 'pdf':
        pdf = original + '.pdf'
        json_doc = original + '.json'
        print(pdf, json_doc)
        print(os.path.isfile(pdf), os.path.isfile(json_doc))
        if os.path.isfile(pdf):
            os.remove(pdf)
        if os.path.isfile(json_doc):
            os.remove(json_doc)
    else:
        bss = original + '.xlsx'
        unmerged = original + '_unmerged.xlsx'
        results = original + 'resultados.xlsx'
        if os.path.isfile(bss):
            os.remove(bss)
        if os.path.isfile(results):
            os.remove(results)
        if os.path.isfile(unmerged) and documento.extraccion.resultado == 'ok':
            os.remove(unmerged)
    pass
    
"""