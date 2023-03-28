<template>
  <div>
    <AliaIcon v-if='icono' :icon="icono" class="me-3 fs-5"/>
    <span v-if="titulo" class="fs-5 fw-bold text-secondary">
      {{ titulo }}
  </span>
  </div>
</template>

<script>
export default {
  name: "HeaderTitle",
  props: {
    icono: {
      type: String,
      required: false
    },
    titulo: {
      type: String,
      required: false
    },
  }
}
</script>

<style scoped>

</style>