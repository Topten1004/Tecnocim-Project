<template>
  <StepTitle
    icon="fa-paperclip"
    titulo="Completar contratos"
    descripcion="Por favor complete la información de los contratos"
  />
  <div class="row">
      <MainTable
        :columnas="mainTable.columnas"
        :filas="contratos"
        :buttons="mainTable.buttons"
        :actions="mainTable.actions"
        :extra-actions="true"
        :offset-height="400"
      />
      <form v-if="pool" @submit.prevent="saveContrato()">
        <ModalWindow
            :titulo="(bulk ? 'Revisión' : 'Completando') + ' contrato de la cuenta ' + pool.cuenta"
            max-width="1000px"
            :visible="pool ? true : false"
            @mounted="modalMounted()"
            @closed="modalClosed()"
            :buttons="modal.buttons"
            v-bind:loading="modal.loading"
        >
          <template #icono>
            <AliaIcon icon='fa-file' class="me-2"/>
          </template>
          <div class="row text-start">
            <div class="col-12">
              <div class="row" v-if="!bulk">
                <div class="col-12">
                  <CustomInput
                      label="Concepto"
                      v-model="pool.concepto"
                      tabindex="-1"
                      readonly
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-6">
                  <CustomSelect
                      v-if="getEntidadesOptions()"
                      :multiple="true"
                      label="Entidad"
                      v-model="pool.contrato.entidad"
                      :options="getEntidadesOptions()"
                      @select="onSelectEntidad($event)"
                      :disabled="bulk"
                      required
                  />
                </div>
                <div class="col-6">
                  <CustomSelect
                      label="Tipo de producto"
                      v-model="pool.contrato.producto"
                      :options="getProductosOptions()"
                      :disabled="bulk"
                      required
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-3">
                  <CurrencyInput
                      label="Importe inicial"
                      v-model.number='pool.contrato.limite'
                      :options="{
                              currency: 'EUR',
                              valueRange: {min: pool.dispuesto}
                            }"
                      :disabled="bulk"
                      required
                  />
                </div>
                <!-- <div class="col-3">
                  <CurrencyInput
                    label="Dispuesto"
                    v-model.number='pool.dispuesto'
                    readonly
                  />
                </div> -->
                <div class="col-3">
                  <CustomInput
                      label="Carencia"
                      type="number"
                      step="any"
                      v-model.number="pool.contrato.carencia"
                      :disabled="bulk"
                      required
                  />
                </div>
                <div class="col-6">
                  <CustomSelect
                      label="Forma de pago"
                      v-model.number="pool.contrato.periodificacion"
                      :options="formasPago"
                      :disabled="bulk"
                      required
                  />
                </div>
              </div>
              <div class="row">
                <div :class="{
                            'col-3': !pool.cuenta.startsWith('52'),
                            'col-6': pool.cuenta.startsWith('52'),
                          }"
                >
                  <CustomInput
                      label="Fecha de incio"
                      type="date"
                      v-model="pool.contrato.inicio"
                      @change="onChangeInicio()"
                      :disabled="bulk"
                      required
                  />
                </div>
                <div :class="{
                            'col-3': !pool.cuenta.startsWith('52'),
                            'col-6': pool.cuenta.startsWith('52'),
                          }"
                >
                  <CustomInput
                      label="Fecha de fin"
                      type="date"
                      v-model="pool.contrato.vencimiento"
                      @change="onChangeVencimiento()"
                      :disabled="bulk"
                      required
                  />
                </div>
                <div
                    v-if="!pool.cuenta.startsWith('52')"
                    class="col-6"
                >
                  <CustomSelect
                      label="Cuenta asociada"
                      v-model="pool.contrato.cuentas[0][1]"
                      :options="getCuentasAsociadasOptions()"
                      :default-value="{id: '', text: 'Sin cuenta asociada'}"
                      :disabled="bulk"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-12">
                  <CustomSelect
                      label="Operacion asociada"
                      v-model="pool.contrato.operacion.id"
                      :options="cirbesOptions"
                      :disabled="!cirbesOptions.length || bulk"
                      :default-value="{id: '', text: 'Sin operación asociada'}"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-3">
                  <CustomInput
                      label="Precio"
                      type="number"
                      v-model="pool.contrato.precio"
                      min="0"
                      max="100"
                      step="any"
                      :disabled="bulk"
                      required
                  />
                </div>
                <div class="col-3">
                  <CustomSelect
                      label="Póliza digitalizada"
                      v-model="pool.contrato.digitalizada"
                      :default-value="{id: false, text: 'No'}"
                      :options="[
                              {
                                id: true,
                                text: 'Sí'
                              }
                            ]"
                      :disabled="bulk"
                      required
                  />
                </div>
                <div class="col-3">
                  <CustomSelect
                      label="Minimis"
                      v-model="pool.contrato.minimis"
                      :default-value="{id: false, text: 'No'}"
                      :options="[
                              {
                                id: true,
                                text: 'Sí'
                              }
                            ]"
                      :disabled="bulk"
                      required
                  />
                </div>
              </div>
              <div class="row" v-if="!bulk">
                <div class="col-12">
                  <CustomTextarea
                      label="Notas"
                      v-model="pool.contrato.notas"
                  />
                </div>
              </div>
            </div>
          </div>
          <div
              v-if="bulk"
              class="row"
          >
            <div v-if="!modal.loading" class="col-12 text-center">
              <div class="alert alert-info mb-0" role="alert">
                <span>Revisando contrato <span class="fw-bold">{{ (bulkaction.current + 1) }}</span> de <span class="fw-bold">{{ bulkaction.pools.length }}</span></span>
              </div>
            </div>
            <div v-if="modal.loading">
              <div class="alert alert-info mb-0 text-center" role="alert">
                <span class="fw-bold">Creando documentos: {{ bulkaction.percentage }}%</span>
              </div>
              <div class="progress p-0">
                <div
                    class="progress-bar progress-bar-striped background-alia"
                    role="progressbar"
                    aria-label="Info striped example"
                    :style="{width: `${bulkaction.percentage}%`}"
                    :aria-valuenow="bulkaction.percentage"
                    aria-valuemin="0"
                    aria-valuemax="100"></div>
              </div>
              <div class="alert alert-warning mb-0 text-center" role="alert">
                <span class="fw-bold">NO CIERRE EL NAVEGADOR WEB</span>
              </div>
            </div>

          </div>
        </ModalWindow>
      </form>
  </div>
    <ModalWindow
      v-if="bulk && !pool"
      :visible="bulk"
      titulo="Importación de contratos"
      icono="fa-upload"
      @closed="bulkClosed()"
      :buttons="bulkaction.buttons"
      :loading="bulkaction.loading"
    >
      <template #icono>
        <AliaIcon icon="fa-upload" class="me-2"/>
      </template>
      <div v-if="!bulkaction.loading">
        <div class="row text-center">
          <div class="col-12">
            <span>Seleccione un archivo para comenzar</span>
          </div>
        </div>
        <div class="row text-center mt-3">
          <div class="col-12">
            <input style="display: none"
                   type="file"
                   id="file"
                   name="file"
                   @change="onFileChange"
                   accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
                   required
            />
            <button v-bind:onclick="loadFile" class="btn btn-primary text-uppercase w-auto" type="button">
              Seleccionar archivo
            </button>
            <a id='plantilla' style="display: none" download="contratos.xlsx" :href="'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + contratobase64">Plantilla</a>
          </div>
        </div>
      </div>
      <div v-if="bulkaction.loading" class="row text-center">
        <div class="col-12">
          <SpinnerAlia />
        </div>
      </div>
      <div v-if="bulkaction.file" class="row text-center mt-3">
        <div class="col-12">
          <AliaIcon icon="fa-file" /> {{ bulkaction.filename }}
        </div>
      </div>
    </ModalWindow>
</template>
<script>
import StepTitle from './StepTitle.vue';
import CustomSelect from './../CustomSelect.vue';
import CurrencyInput from './../CurrencyInput.vue';
import CustomInput from './../CustomInput.vue';
import CustomTextarea from './../CustomTextarea.vue';
import contrato from "@/documents/contrato.js";

export default {
  name: "GestionContratos",
  components: {
    StepTitle,
    //
    CustomSelect,
    CurrencyInput,
    CustomInput,
    CustomTextarea
  },
  props: {
    bulk: {
      type: Boolean,
      default: false
    }
  },
  mounted() {
    this.getEntidades();
    this.getProductos();
    // this.getCirbes();
    this.getContratos();
  },
  data() {
    return {
      pool: null,
      contratobase64: contrato.contratobase46,
      contratos: [],
      contrato: {
        cirbe: {
          id: ''
        },
        entidad: '',
        producto: null,
        concepto: '',
        importe: 0,
        limite: 0,
        dispuesto: 0,
        carencia: 0,
        inicio: '',
        vencimiento: '',
        precio: 0,
        digitalizada: false,
        minimis: false,
        notas: '',
        operacion: {
          operacion: null
        },
        cuentas: [
          [
            'fake',
            ''
          ],
          null
        ]
      },
      //
      entidades: [],
      productos: [],
      cirbesOptions: [],
      //
      mainTable: {
        columnas: [
          {
            key: 'cuenta'
          },
          {
            key: 'otra_cuenta',
            value: 'Otra cuenta'
          },
          {
            key: 'dispuesto',
            type: 'currency'
          },
          {
            key: 'concepto',
            class: 'text-right'
          }
        ],
        actions: [
          {
            key: 'seleccionar',
            icon: 'fa-file-pen',
            method: pool => this.selectPool(pool)
          }
        ]
      },
      modal: {
        titulo: '',
        buttons: [
          {
            key: 'save',
            text: 'Guardar',
            type: 'submit'
          }
        ],
        loading: false
      },
      formasPago: [
        {
          id: 1,
          text: 'Mensual'
        },
        {
          id: 3,
          text: 'Trimestral'
        },
        {
          id: 4,
          text: 'Cuatrimestral'
        },
        {
          id: 6,
          text: 'Semestral'
        },
        {
          id: 12,
          text: 'Anual'
        },
      ],
      bulkaction: this.getBulkAction()
    }
  },
  methods: {
    async getContratos() {
      await this.axios
        .get("/core/pool/", {
          params: {
            CIF: JSON.parse(localStorage.getItem('company')).CIF
          },
        })
        .then((r) => {
          const data = Object.values(r.data)[0]
            .map(pool => {pool.otra_cuenta = this.getCuentaAsociada(pool); return pool;})
            .map(pool => {pool.contrato ? (pool.contrato.entidad = this.mapEntidad(pool.contrato.entidad)) : null; return pool;})
            .map(pool => {pool.class = pool.contrato ? (pool.cuenta.startsWith('17') && !this.getCuentaAsociada(pool) ? 'con-activo sin-asociar' : 'con-activo' ) : ''; return pool;})
          ;
          this.contratos = data;
        });
    },
    mapEntidad(id) {
      const codigo = this.entidades.find(e => e.id == id).codigo;
      return ("0000" + codigo).slice(-4);
    },
    getCuentaAsociada(pool) {
      return pool.contrato == null ? "" : (pool.contrato.cuentas[0].filter(c => c != pool.cuenta))[0];
    },
    selectPool(currentPool) {
      let pool = JSON.parse(JSON.stringify(currentPool));
      if (!pool.contrato) {
        pool.contrato = { ...this.contrato };
      } else {
        if (!pool.contrato.operacion) {
          pool.contrato.operacion = { operacion: null };
        }
        if (
            pool.contrato.cuentas[0].length > 1
            && pool.contrato.cuentas[0][1]
        ) {
          const cuenta = this.contratos.find(c => c.cuenta == pool.contrato.cuentas[0][1]);
          pool.contrato.cuentas[0][1] = cuenta ? cuenta.id : null;
        }
      }
      this.pool = JSON.parse(JSON.stringify(pool));
    },
    async getEntidades() {
      this.axios.get("/complement/entidad/?id=0").then((r) => {
        this.entidades = r.data;
      });
    },
    getEntidadesOptions() {
      return this.entidades.map(entidad => { return {id: entidad.codigo, text: entidad.codigo ? `${entidad.nombre} (${entidad.codigo})` : entidad.nombre }; });
    },
    async getProductos() {
      this.axios.get("/complement/producto").then((r) => {
        this.productos = r.data;
      });
    },
    getProductosOptions() {
      return this.productos.map(p => Object({id: Object.values(p)[0], text: Object.values(p)[0]}));
    },
    modalMounted() {
      this.isCirbe(true);
      if (this.bulk) {
        this.modal.buttons = [
          {
            key: 'checked',
            text: 'Revisar anterior',
            type: 'button',
            click: () => this.previous(),
            class: 'btn btn-primary me-auto',
            show: () => this.bulkaction.current !== 0
          },
          {
            key: 'checked',
            text: 'Revisar siguiente',
            type: 'button',
            click: () => this.next(),
            show: () => this.bulkaction.current !== (this.bulkaction.pools.length - 1)
          },
          {
            key: 'checked',
            text: 'Finalizar',
            type: 'button',
            click: () => this.saveContratos(),
            show: () => this.bulkaction.current === (this.bulkaction.pools.length - 1),
            class: 'btn btn-success'
          }
        ];
      } else {
        this.modal.buttons = [
          {
            key: 'save',
            text: 'Guardar',
            type: 'submit'
          }
        ];
      }
    },
    modalClosed() {
      this.pool = null;
      this.modal.loading = false;
      this.bulkaction = this.getBulkAction();
    },
    getCuentasAsociadasOptions() {
      return this.contratos.filter(c => c.cuenta.startsWith('52')).map(c => { return { id: c.id, text: [c.cuenta, c.concepto].join(' ')} });
    },
    async getCirbes(current = false) {
      this.axios.get("/core/cirbe_sin_contrato/", {
        params: {
          CIF: JSON.parse(localStorage.getItem('company')).CIF,
          inicio: this.pool.contrato.inicio,
          vencimiento: this.pool.contrato.vencimiento,
          entidad: this.pool.entidad
        }
      }).then((r) => {
        let cirbes = [];
        const operacion = this.pool.contrato?.operacion;
        if(current && (operacion && operacion.operacion)) {
          cirbes.push(operacion);
        } else {
          this.pool.contrato.operacion.id = '';
        }
        cirbes = cirbes.concat(r.data);
        this.cirbesOptions = cirbes.length ? cirbes.map(c => { return { id: c.id, text: ['entidad_nombre' in c ? c.entidad_nombre : c.entidad.nombre, c.operacion].join(' - ')} }) : [];
        // this.cirbesOptions = cirbes.length ? cirbes.map(c => { return { id: c.id, text: c.operacion } }) : [];
      });
    },
    async isCirbe(current = false) {
      const re = /[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])/;
      if(
        re.exec(this.pool.contrato.inicio) != null
        && re.exec(this.pool.contrato.vencimiento) != null
      ) {
        if (
            +this.pool.contrato.inicio.split('-')[0] > 1900
            && +this.pool.contrato.vencimiento.split('-')[0] > 1900
        ) {
          await this.getCirbes(current);
          return;
        }
      }

      this.cirbesOptions = [];
      return false;
    },
    onSelectEntidad($event) {
      // Socios
      if($event.id == 0) {
        this.pool.contrato.producto = 'excepcion';
      }

      this.isCirbe();
    },
    onChangeInicio() {
      this.isCirbe();
    },
    onChangeVencimiento() {
      this.isCirbe();
    },
    async saveContrato(poolParam = null) {
      this.modal.loading = true;

      const pool = poolParam ?? this.pool;

      let contrato = {
        inicio: pool.contrato.inicio,
        vencimiento: pool.contrato.vencimiento,
        entidad: this.entidades.find(e => e.codigo == pool.contrato.entidad).id,
        producto: pool.contrato.producto,
        limite: pool.contrato.limite,
        precio: pool.contrato.precio,
        moneda: 'EUR',
        minimis: pool.contrato.minimis === 'true' ? 'True' : 'False',
        digitalizada: pool.contrato.digitalizada === 'true' ? 'True' : 'False',
        valoracion: 5,
        periodificacion: pool.contrato.periodificacion,
        carencia: pool.contrato.carencia,
        notas: pool.contrato.notas ?? '',
        id_cuenta: pool.contrato.cuentas[0][1] ?? null,
        id_pool: pool.id
      }

      if(pool.contrato.operacion?.id) {
        contrato.id_cirbe = pool.contrato.operacion.id;
      }

      let formData = new FormData();
      for(var key in contrato) {
        formData.append(key, contrato[key]);
      }

      return this.axios
        .post("/core/contrato/", formData)
        .then(() => {
          if (poolParam == null) { // Solo paraq gestion masiva de contratos
            this.$toast.success(`Contrato de la cuenta ${this.pool.cuenta} guardado`);
            this.getContratos();
            this.modalClosed();
          }
        })
        .finally(() => this.modal.loading = false);
    },
    loadFile() {
      document.getElementById("file").click();
    },
    onFileChange($event) {
      var fileData = $event.target.files[0];
      this.bulkaction.file = fileData;
      this.bulkaction.filename = this.bulkaction.file.name;
      this.bulkaction.buttons = [
        {
          click: this.sendFile,
          text: 'PROCESAR ARCHIVO',
          class: 'btn btn-success'
        }
      ]
    },
    async sendFile() {
      this.bulkaction.loading = true;
      this.$toast.info( `Procesando contratos de archivo <b>${this.bulkaction.filename}</b>`)

      let formData = new FormData();
      formData.append('CIF', JSON.parse(localStorage.getItem('company')).CIF);
      formData.append('file', this.bulkaction.file);

      return this.axios
          .post('/core/cargacontratos/', formData)
          .then(r => {
            if (!r.data.contratos.length) {
              this.$toast.error('El archivo no contiene contratos');
              return;
            }
            this.bulkaction.pools = [];
            this.bulkaction.pools = this.mapBulkPools(r.data.contratos, r.data.parametros);
            this.selectPool(this.bulkaction.pools[this.bulkaction.current]);
            this.$toast.success( `Contratos de archivo <b>${this.bulkaction.filename}</b> procesados`)
          })
          .finally(() => this.bulkaction.loading = false);
    },
    mapBulkPools(contratos, parametros) {
      let pools = [];
      let pool = {};
      for(const index in contratos) {
        pool = {
          cuenta: parametros[index].Cuenta.toString(),
          contrato: contratos[index]
        };
        pool.contrato.cuentas[0] = [0, parametros[index]['Cuenta Asociada']];
        pool.contrato.entidad = this.mapEntidad(pool.contrato.entidad);
        pool.id =  parametros[index].cuenta_id;
        pools.push(pool);
      }
      return pools;
    },
    descargarPlantilla () {
      document.getElementById("plantilla").click();
    },
    bulkClosed() {
      this.$emit('bulkClosed');
      this.bulkaction = this.getBulkAction();
    },
    saveContratos() {
      this.modal.loading = true;
      this.bulkaction.percentage = 0;
      const uno = 100 / this.bulkaction.pools.length;
      this.bulkaction.pools.forEach(pool => {
        // Fix de cuenta a id de cuenta
        if (
            pool.contrato.cuentas.length > 0
            && pool.contrato.cuentas[0].length > 1
            && pool.contrato.cuentas[0][1]
        ) {
          pool.contrato.cuentas[0][1] = this.contratos.find(c => c.cuenta == pool.contrato.cuentas[0][1])?.id;
        }
        this.saveContrato(pool)
            .then()
            .finally(() => {
              this.bulkaction.percentage += uno;
              if (this.bulkaction.percentage >= 100) {
                this.$toast.success(`Todos los contratos del archivo <b>${this.bulkaction.filename}</b> han sido creados`);
                this.pool = false;
                this.modalClosed();
                this.bulkClosed();
                this.getContratos();
              }
            });
      });
    },
    next() {
      this.bulkaction.current++;
      this.selectPool(this.bulkaction.pools.at(this.bulkaction.current));
    },
    previous() {
      this.bulkaction.current--;
      this.selectPool(this.bulkaction.pools.at(this.bulkaction.current));
    },
    getBulkAction() {
      return {
        file: null,
        filename: '',
        buttons: [
          {
            click: this.descargarPlantilla,
            text: 'PLANTILLA',
          }
        ],
        loading: false,
        pools: [],
        current: 0,
        percentage: 0
      }
    }
  }
}
</script>
<style>
</style>