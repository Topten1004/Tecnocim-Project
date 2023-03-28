<template>
  <div class=" modal-backdrop">
    <div v-if="OpenClose" class="modal fade show" tabindex="-1" aria-modal="true" role="dialog" style="display:block">
      <div class="modal-dialog modal-dialog-centered" :class="{ 'modal-fullscreen': fullscreen }"
        :style="{ 'max-width': width ? 'initial' : maxWidth, 'width': width }">
        <div class="modal-content">
          <div class="modal-header">
            <slot name='icono'></slot>
            <h5 v-if='titulo' class="modal-title">{{ titulo }}</h5>
            <button v-if="close" type="button" @click="OpenCloseFun()" class="btn-close"></button>
          </div>
          <div v-if='body' class="modal-body">
            <slot></slot>
          </div>
          <div v-if='buttons.length' class="modal-footer">
            <button
              v-for="button in buttons"
              v-bind:key="button.key"
              :type="button.type ?? button"
              @submit="button.submit"
              @click="button.click"
              :class="button.class ?? 'btn btn-primary'"
              :disabled="loading"
              :style="{ display: (!('show' in button) || ('show' in button && button.show())) ? 'initial' : 'none'}"
            >
              <span v-if="!loading">
                {{ button.text }}
              </span>
              <SpinnerAlia v-if="loading" :color="false" />
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ModalWindow',
  props: {
    titulo: String,
    visible: Boolean,
    fullscreen: {
      type: Boolean,
      default: false
    },
    maxWidth: {
      type: String,
      default: '500px'
    },
    width: {
      type: String,
      default: ''
    },
    buttons: {
      type: Array,
      default: () => []
    },
    loading: {
      type: Boolean,
      default: false
    },
    body: {
      type: Boolean,
      default: true
    },
    close: {
      type: Boolean,
      default: true
    }
  },
  mounted() {
    this.$emit('mounted');
    // document.querySelectorAll('.main-content').forEach(c => c.style = 'opacity: 0.5');
  },
  data() {
    return {
      OpenClose: this.visible
    }
  },
  methods: {
    OpenCloseFun() {
      this.OpenClose = false;
      this.$emit('closed', this.OpenClose);
    },
  },
  watch: {
    visible: function (newVal) { // watch it
      this.OpenClose = newVal;
      // console.log('new' +newVal+ '==' +oldVal)
    }
  },
  unmounted() {
    // document.querySelectorAll('.main-content').forEach(c => c.style = '');
  }
}
</script>

<style>
/* .modal-backdrop {
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    background-color: rgba(0, 0, 0, 0.3);
    display: flex;
    justify-content: center;
    align-items: center;
  } */
</style>