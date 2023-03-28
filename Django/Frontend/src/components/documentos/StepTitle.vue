<template>
  <div class="row">
    <div class="col-12 text-center">
      <AliaIcon :icon="icon" class="text-alia me-2" /> <span class="fs-5">{{ titulo }}</span>
      <p v-if="descripcion" class="fst-italic">{{ descripcion }}</p>
    </div>
  </div>
</template>

<script>
export default {
  name: "StepTitle",
  components: {},
  props: {
    icon: {
      required: true,
      type: String
    },
    titulo: {
      required: true,
      type: String
    },
    descripcion: {
      required: true,
      type: String
    }
  }
}
</script>

<style>

</style>