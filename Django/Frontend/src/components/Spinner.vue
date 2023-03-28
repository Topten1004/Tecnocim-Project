<template>
  <span
        class="spinner-grow spinner-grow-sm"
        :class="{ 'text-alia': color, 'text-white': !color }"
        role="status"
        aria-hidden="true"
  ></span>
</template>

<script>
export default {
  name: 'SpinnerAlia',
  props: {
    color: {
      type: Boolean,
      default: true
    }
  }
}
</script>
<style>
.text-white {
  color: #000;
}
</style>