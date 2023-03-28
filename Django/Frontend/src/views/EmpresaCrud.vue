<template>
  <div class="container">
    <GenericCRUD
        :titulo="titulo"
        :icono="icono"
        :columnas="columnas"
        :ruta="ruta"
        :definicion="definicion"
        :crud="crud"
    />
  </div>
</template>
<script>
export default {
  data() {
    return {
      titulo: 'Empresas',
      icono: 'fa-building',
      ruta: '/core/empresas/',
      columnas: [
        {
          key: 'CIF'
        },
        {
          key: 'nombre'
        }
      ],
      definicion: {
        id: null,
        CIF: '' ,
        nombre: '',
        contacto: null,
        telefono: '',
        email: '',
        configFile: '.env.local.old'
      },
      crud: ['create', 'edit']
    }
  }
}
</script>
<style>

</style>