<template>
  <ModalWindow
      v-if="visible"
      :titulo="titulo"
      :buttons="buttons"
      :visible="visible"
      :body="false"
      @closed="$emit('closed')"
  />
</template>
<script>
import ModalWindow from "@/components/ModalWindow.vue";

export default {
  name: 'ConfirmModal',
  components: {
    ModalWindow
  },
  props: {
    titulo: {
      type: String,
      default: '¿Está seguro?'
    },
    text: {
      type: String,
      default: 'Aceptar'
    },
    class: {
      type: String,
      default: 'btn btn-primary'
    },
    visible: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      buttons: [{
        key: 'action',
        text: this.text,
        class: this.class,
        click: () => this.$emit('clickButton')
      }]
    }
  }
}
</script>
<style>
</style>