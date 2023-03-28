<template>
  <div class="container">
    <GenericCRUD
        :titulo="titulo"
        :icono="icono"
        :columnas="columnas"
        :ruta="ruta"
        :definicion="definicion"
        :crud="crud"
        :filter="filter"
    />
  </div>
</template>
<script>
export default {
  data() {
    return {
      titulo: 'Entidades no financieras',
      icono: 'fa-building-columns',
      ruta: '/complement/entidad/',
      columnas: [
        {
          key: 'nombre'
        }
      ],
      definicion: {
        id: null,
        nombre: ''
      },
      crud: ['create', 'edit'],
    }
  },
  methods: {
    filter (data) {
      return data.codigo == null ? data : null;
    }
  }
}
</script>
<style>

</style>