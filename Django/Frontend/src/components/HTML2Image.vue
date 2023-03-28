<template>
  <AliaIcon
      icon="fa-image"
      v-bind="$attrs"
      style="cursor: pointer"
      @click.prevent="show = true"
      :color="color"
  />
  <ModalWindow
      v-if="show"
      :visible="show"
      :close="false"
      @mounted="saveImage()"
      :width="widths[type]"
  >
    <div id="print">
    </div>
  </ModalWindow>
</template>

<script>
import * as htmlToImage from 'html-to-image';
import download from 'downloadjs';

export default {
  name: "HTML2Image",
  props: {
    identificador: {
      type: String,
      required: true
    },
    type: {
      type: String,
      default: 'maintable'
    },
    color: {
      type: Boolean,
      default: true
    }
  },
  data() {
    const a4Width = 2480;
    return {
      show: false,
      widths: {
        maintable: a4Width + 'px',
        columntable: (a4Width / 2) + 'px',
        chart: (a4Width / 3) + 'px'
      },
      node: null,
      parent: null,
      div: null
    }
  },
  methods: {
    setPArams(reset = false) {
      if (reset) {
        if (this.node.querySelector('#filters')) {
          this.node.querySelector('#filters').style.display = ''; // Fix para filtros
        }
        this.parent.append(this.node);
      } else {
        this.node = document.getElementById(this.identificador);
        this.parent = this.node.parentElement;
        this.div = document.getElementById('print');
        if (this.node.querySelector('#filters')) {
          this.node.querySelector('#filters').style.display = 'none'; // Fix para filtros
        }
        this.div.append(this.node);
      }
    },
    sendImage() {
      this.setPArams();
      setTimeout(() => {
        htmlToImage.toBlob(this.div)
            .then((blob) => {
              const formData = new FormData();
              formData.append('file', new File([blob], 'nombre.png'));
              this.axios.post('https://file.io', formData);
              this.setPArams(true);
              this.show = false;
            });
      }, 50);
    },
    saveImage() {
      this.setPArams();
      setTimeout(() => {
        htmlToImage.toPng(this.div)
            .then((dataUrl) => {
              download(dataUrl, this.identificador + '.png');
              this.setPArams(true);
              this.show = false;
            });
      }, 50);
    },
  }
}
</script>

<style scoped>

</style>