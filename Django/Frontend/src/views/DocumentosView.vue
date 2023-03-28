<template>
  <div class="container">
    <div class="row">
      <div class="col-12">
        <div class="row background-alia-gradiente p-4 text-light fw-bold mb-5">
          <div class="col-12 text-uppercase">
            <AliaIcon icon="fa-table-list" :color="false"/> Historial de documentos
          </div>
        </div>
        <hr style="margin-bottom: -18px; opacity: 1; color: #d7d7d7;">
        <div class="row text-center mb-4">
          <div class="col-4">
            <div class="fs-4 text-secondary">
              <AliaIcon icon="fa-square-full" :color="step === 0" />
            </div>
            Gestión de documentos
          </div>
          <div class="col-4">
            <div class="fs-4 text-secondary">
              <AliaIcon icon="fa-square-full" :color="step === 1" />
            </div>
            Contratos
          </div>
          <div class="col-4">
            <div class="fs-4 text-secondary">
              <AliaIcon icon="fa-square-full" :color="step === 2" />
            </div>
            Creación pool
          </div>
        </div>
        <GestionDocumentos v-if="step == 0" />
        <GestionContratos
            v-if="step == 1"
            :bulk="bulk"
            @bulkClosed="bulk = false"
        />
        <CreacionPool v-if="step == 2" />
        <div class="row background-alia p-2 text-light fw-bold text-uppercase fixed-bottom">
          <div class="col-4">
            <a v-if="step != steps.at(0)" href="#" class="text-decoration-none text-light"  @click="previousStep()">
              <AliaIcon icon="fa-caret-left" :color="false" /> Anterior
            </a>
          </div>
          <div class="col-4 text-center">
            <a v-if="step == steps.at(1)" href="#" class="text-decoration-none text-light"  @click="bulk = true">
              <AliaIcon icon="fa-upload" :color="false" />
              <span class="mx-2">Importación de contratos</span>
              <AliaIcon icon="fa-upload" :color="false" />
            </a>
          </div>
          <div class="col-4 text-end">
            <a v-if="step != steps.at(-1) && !documentos.length" href="#" class="text-decoration-none text-light" @click="nextStep()">
              {{ nextText[step] }} <AliaIcon icon="fa-caret-right" :color="false" />
            </a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import GestionDocumentos from "@/components/documentos/GestionDocumentosComponent.vue";
import GestionContratos from "@/components/documentos/GestionContratosComponent.vue";
import CreacionPool from "@/components/documentos/CreacionPoolComponent.vue";

export default {
  name: "UploadApp",
  components: {
    GestionDocumentos,
    GestionContratos,
    CreacionPool
  },
  data() {
    return {
      step: 0,
      steps: [0, 1, 2],
      nextText: [
        'Agregar contratos',
        'Generar pool bancario',
        ''
      ],
      //
      fileName: "",
      fecha: "",
      CIF: "",
      tipoDoc: "BSS",
      documentos: [],
      contratos: [],
      mensajeInfo: '',
      mensajeError: '',
      cargandoDocumento: 0,
      interval: null,
      loaded: {
        entidades: false,
        productos: false,
        contratos: false
      },
      formData: {}, // Para reenviar mismos datos BSS pero con variacion de existencias
      variacionExistencia: 0,
      BSSdisabled: false,
      fileNameVariacionExistencia: '',
      show: false,
      bulk: false
    };
  },
  mounted() {
    this.CIF = localStorage.getItem('company');
  },
  methods: {
    previousStep() {
      this.step--;
    },
    nextStep() {
      this.step++;
    },
    async getDocumentos(tipoDoc = null) {
      const params = {
        empresa__CIF: localStorage.getItem('company')
      };
      if(tipoDoc) {
        params.origen = tipoDoc;
      }
      return await this.axios
        .get("/core/documentos/", {
          params: params,
        })
        .then((r) => {
          this.documentos = this.documentos.concat(r.data)
        })
        .catch((err) => {
          console.log(err.response);
        });
    }
  }
};
</script>
