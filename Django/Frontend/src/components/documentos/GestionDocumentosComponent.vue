<template>
  <StepTitle icon="fa-paperclip" titulo="Carga de documentos financieros - Gestión de documentos"
    :descripcion="getDescripcion()" />
  <div class="row">
    <div class="col-12 d-flex justify-content-center p-3">
      <select v-model="documento.tipoDoc" class="form-control w-auto" @change="changeTipoDoc($event.target.value)">
        <option v-for='tipo in tipos' v-bind:key="tipo.key" :value="tipo.key">
          {{ tipo.text }}
        </option>
      </select>
      <input class="form-control w-auto" type="date" required v-model="documento.fecha" />
      <div>
        <input style="display: none" type="file" id="file" name="file" @change="onFileChange" v-bind:accept="extension"
          required />
        <button v-bind:onclick="loadFile" class="btn btn-primary text-uppercase w-auto" type="button">
          Adjuntar
        </button>
      </div>
      <div class="mx-2">
        <AliaIcon v-if="!(documento.fecha && documento.file)" icon="fa-regular fa-circle" :color='false'
          class="fs-1 text-secondary" />
        <AliaIcon v-if="documento.fecha && documento.file" icon="fa-regular fa-circle-check" class='fs-1' />
      </div>
      <button v-bind:disabled="!(documento.fecha && documento.file)" class="btn btn-primary text-uppercase w-auto"
        @click="upload()">
        Cargar documento
      </button>
    </div>
  </div>
  <div class="row mb-3" :class="{ invisible: !documento.filename }">
    <div class="col-12 text-center">
      <AliaIcon icon='fa-file' /> <span class="text-decoration-underline">{{ documento.filename }}</span>
    </div>
  </div>
  <div class="row">
    <MainTable 
      v-if="documentos" 
      :columnas="mainTable.columnas"
      :filas="documentos"
      :offset-height="508"
      :extra-actions="true"
    >
      <template #extra-actions="{ fila }">
        <SpinnerAlia v-if="!fila.status"/>
        <a
            v-if="getDocumentoPendiente(fila.id)"
            href="#"
            data-bs-toggle="tooltip"
            data-bs-placement="left"
            data-bs-html="true"
            :title="getTitle(fila.id)"
            @click.prevent="showDocumentoPendienteModal(fila.id)"
        >
          <AliaIcon
              icon="fa-triangle-exclamation"
              class="me-2 text-danger"
          />
        </a>
        <AliaIcon
            v-if="fila.status"
            style="cursor: pointer"
            icon="fa-regular fa-trash-can"
            @click="docToDelete = fila"
        />
      </template>
    </MainTable>
  </div>
  <ConfirmModal
    :visible="docToDelete !== null"
    class="btn btn-danger"
    text="Eliminar"
    @clickButton="deleteDocumento()"
    @closed="closed()"
  />
  <ModalWindow
      v-if="formData"
      titulo="Conceptos pendientes (Sugerencia)"
      :visible="formData !== null"
      max-width="600px"
      @closed="closed()"
  >
    <CustomInput
      label="Variación de existencias"
      v-model="conceptosPendientes.sugerencia_variacion"
      type="number"
      required
    />
    <CustomInput
        label="Amortización"
        v-model="conceptosPendientes.sugerencia_amortizacion"
        type="number"
        required
    />
    <CustomInput
        label="Impuestos"
        v-model="conceptosPendientes.sugerencia_impuestos"
        type="number"
        required
    />
    <div class="row">
      <div class="col-12 text-center">
        <AliaIcon icon="fa-file" class="me-3" />
        <span>
          {{ formData.get('file').name }}
        </span>
      </div>
    </div>
    <button
        :disabled="
          isNaN(conceptosPendientes.sugerencia_variacion)
          || isNaN(conceptosPendientes.sugerencia_amortizacion)
          || isNaN(conceptosPendientes.sugerencia_impuestos)
          || conceptosPendientes.sugerencia_variacion.length == 0
          || conceptosPendientes.sugerencia_amortizacion.length == 0
          || conceptosPendientes.sugerencia_impuestos.length == 0
        "
        class="btn btn-primary save text-capitalize float-end mt-3"
        @click.prevent="saveDocumentoPendiente()"
    >
      <span v-if="!loading">
          guardar
      </span>
      <SpinnerAlia v-if="loading" :color="false" />
    </button>
  </ModalWindow>
</template>
<script>
import StepTitle from './StepTitle.vue';
import MainTable from '../MainTable.vue';
import ConfirmModal from "@/components/ConfirmModal.vue";
import SpinnerAlia from "@/components/Spinner.vue";
import ModalWindow from "@/components/ModalWindow.vue";
import CustomInput from "@/components/CustomInput.vue";

export default {
  name: "GestionDocumentos",
  components: {
    CustomInput,
    ModalWindow,
    SpinnerAlia,
    ConfirmModal,
    StepTitle,
    MainTable
  },
  mounted() {
    this.setExtension(this.documento.tipoDoc);
    this.getDocumentos();
  },
  data() {
    return {
      documentos: [],
      mainTable: {
        columnas: [
          {
            key: 'origen',
            value: 'tipo'
          },
          {
            key: 'documento',
            class: 'text-start'
          },
          {
            key: 'fecha',
            type: 'date'
          }
        ]
      },
      //
      years: this.getYears(),
      //
      tipos: [
        {
          key: 'BSS',
          text: 'Balance de  sumas y saldos'
        },
        {
          key: 'Cirbe',
          text: 'CIRBE'
        },
        {
          key: 'Modelo200',
          text: 'Modelo200'
        }
      ],
      //
      documento: {
        tipoDoc: 'BSS',
        fecha: '',
        file: null,
        filename: ''
      },
      extension: '',
      extensiones: {
        BSS: '.csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel',
        Cirbe: 'application/pdf',
        Modelo200: 'application/pdf',
      },
      docToDelete: null,
      formData: null,
      documentoPendienteId: null,
      conceptosPendientes: {
        sugerencia_amortizacion: 0,
        sugerencia_impuestos: 0,
        sugerencia_variacion: 0
      },
      loading: false
    }
  },
  methods: {
    getYears() {
      const currentYear = (new Date).getFullYear();
      return [currentYear - 2, currentYear - 1]
    },
    getDescripcion() {
      return 'Por favor, adjunta los documentos financieros correspondientes ' + this.years[0] + ', ' + this.years[1] + ' y el último balance del año en curso.';
    },
    setExtension(tipo) {
      this.extension = this.extensiones[tipo];
    },
    loadFile() {
      document.getElementById("file").click();
    },
    changeTipoDoc(value) {
      this.setExtension(value);
    },
    onFileChange($event) {
      var fileData = $event.target.files[0];
      this.documento.file = fileData;
      this.documento.filename = this.documento.file.name;
    },
    getFormData() {
      let formData = new FormData();
      formData.append('CIF', JSON.parse(localStorage.getItem('company')).CIF);
      formData.append('fecha', this.documento.fecha);
      formData.append('tipoDoc', this.documento.tipoDoc);
      formData.append('file', this.documento.file);

      return formData;
    },
    async upload(formDataParam = null) {
      const filename = this.documento.filename;

      let notification = this.$toast.info('Cargando documento <b>' + filename + '</b>', {
        dismissible: false,
        duration: false
      });

      let formData = this.getFormData();
      for(const item in this.getFormData() ?? formDataParam) {
        formData.append(item, (this.getFormData() ?? formDataParam).get(item));
      }

      setTimeout(() => {
        this.getTodosDocumentos();
        this.resetForm();
      }, 500);

      return this.axios
        .post('/sources/upload/', formDataParam ?? this.getFormData())
        .then(r => {
          if (!formDataParam) {
            if(
                'conceptos_pendientes' in r.data
                && Object.keys(r.data.conceptos_pendientes).some(k => k.includes('sugerencia'))
            ) {
              formData.append('amortizacion', r.data.conceptos_pendientes.sugerencia_amortizacion ?? 0);
              formData.append('impuestos', r.data.conceptos_pendientes.sugerencia_impuestos ?? 0);
              formData.append('variacion', r.data.conceptos_pendientes.sugerencia_variacion ?? 0);

              this.addDocumentoPendiente(r.data.conceptos_pendientes.documento, formData);
              this.$parent.$parent.$parent.conceptosPendientes[r.data.conceptos_pendientes.documento] = formData;
              this.$toast.warning('Documento <b>' + filename + '</b> necesita revisión');
              return;
            }
          }
          this.$toast.success('Documento <b>' + filename + '</b> cargado con éxito');
        })
        .finally(() => {
          setTimeout(() => this.getTodosDocumentos(), 500);
          notification.destroy();
        });
    },
    getTodosDocumentos() {
      this.documentos = [];
      this.getDocumentos('BSS')
        .then(() => this.getDocumentos('Modelo200')
          .then(() => this.getDocumentos('Cirbe')
            .then(() => this.show = true)
          )
        );
    },
    async getDocumentos(tipoDoc = null) {
      const params = {
        empresa__CIF: JSON.parse(localStorage.getItem('company')).CIF
      };
      if (tipoDoc) {
        params.origen = tipoDoc;
      }
      return await this.axios
        .get("/core/documentos/", {
          params: params,
        })
        .then((r) => {
          this.documentos = this.documentos
                              .concat(r.data.map(d => { d.documento = d.documento.split('/').at(-1); return d; }))
                              .map(d => {d.class = this.getDocumentoPendiente(d.id) ? 'bg-warning': '';return d;})
                            ;
        });
    },
    deleteDocumento() {
      return this.axios
        .delete("/core/documentos/" + this.docToDelete.id)
        .then(() => {
          this.$toast.success(`Documento eliminado`);
          this.docToDelete = null;
        })
        .finally(() => setTimeout(() => this.getTodosDocumentos(), 500));
    },
    resetForm() {
      this.documento.fecha = '';
      this.documento.file = null;
      this.documento.filename = '';
      document.getElementById('file').value = ''
    },
    closed() {
      this.docToDelete = null
      this.formData = null;
      this.conceptosPendientes.sugerencia_amortizacion = 0;
      this.conceptosPendientes.sugerencia_variacion = 0;
      this.conceptosPendientes.sugerencia_impuestos = 0;
    },
    getTitle(id) {
      const documentoPendiente = this.getDocumentoPendiente(id);
      return `Amortización: ${documentoPendiente.get('amortizacion')}, Variación de existencias: ${documentoPendiente.get('variacion')}, Impuestos: ${documentoPendiente.get('impuestos')}`;
    },
    getDocumentosPendientes() {
      return this.$parent.$parent.$parent.conceptosPendientes;
    },
    addDocumentoPendiente(id, data) {
      this.$parent.$parent.$parent.conceptosPendientes[id] = data;
    },
    getDocumentoPendiente(id) {
      return this.$parent.$parent.$parent.conceptosPendientes[id];
    },
    removeDocumentoPendiente(id) {
      delete this.$parent.$parent.$parent.conceptosPendientes[id];
    },
    showDocumentoPendienteModal(id) {
      this.documentoPendienteId = id;
      const documentoPendiente = this.getDocumentoPendiente(id);
      this.conceptosPendientes.sugerencia_amortizacion = documentoPendiente.get('amortizacion');
      this.conceptosPendientes.sugerencia_variacion = documentoPendiente.get('variacion');
      this.conceptosPendientes.sugerencia_impuestos = documentoPendiente.get('impuestos');
      this.formData = documentoPendiente;
    },
    saveDocumentoPendiente() {
      this.loading = true;
      this.formData.set('amortizacion', this.conceptosPendientes.sugenrencia_amortizacion);
      this.formData.set('variacion', this.conceptosPendientes.sugenrencia_variacion);
      this.formData.set('impuestos', this.conceptosPendientes.sugenrencia_impuestos);
      this.upload(this.formData)
        .then(() => {
          this.removeDocumentoPendiente(this.documentoPendienteId);
          this.formData = null;
          this.documentoPendienteId = null;
          this.getDocumentos();
        })
        .finally(() => this.loading = false)
    }
  }
}
</script>
<style>

</style>