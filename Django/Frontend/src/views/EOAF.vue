<template>
  <HTML2Image
      class="float-end fs-5"
      identificador="eoaf"
      type="columntable"
  />
  <div>
    <div v-if="this.years.length > 0" class="row fw-bold fs-5 text-center mb-3">
      <div class="col-12 text-center">
        {{ getTitulo() }}
      </div>
    </div>
    <div id="eoaf" v-if="show">
      <div
          class="row fw-bold fs-4 text-center mb-3"
      >
        <div class="col-6">
          <span>ORIGENES</span>
        </div>
        <div class="col-6">
          <span>APLICACIONES</span>
        </div>
      </div>
      <!--  LARGO PLAZO -->
      <div
          class="row"
      >
        <div class="col-12 text-center background-alia text-light fw-bold">
          <span class="fs-5">LARGO PLAZO</span>
        </div>
      </div>
      <div
          class="row mt-2"
      >
        <div
            v-for="key in Object.keys(mainTables).filter(k => k.includes('lp'))"
            v-bind:key="key"
            class="col-6"
        >
          <MainTable
              :titulo="mainTables[key].titulo"
              :filas="mainTables[key].data"
              :columnas="getColumnas(key.includes('Origen') ? 'bg-lightgreen' : 'bg-lightpink')"
              :header="false"
              :exportar="false"
          />
        </div>
      </div>
      <div class="row">
        <div class="col-6">
          <MainTable
              :filas="totales.lpOrigen"
              :columnas="columnasTotal"
              :header="false"
              :exportar="false"
          />
          <MainTable
              v-if="totales.lpDisminucion[0].total"
              :filas="totales.lpDisminucion"
              :columnas="columnasTotal"
              :header="false"
              :exportar="false"
          />
        </div>
        <div class="col-6">
          <MainTable
              :filas="totales.lpAplicacion"
              :columnas="columnasTotal"
              :header="false"
              :exportar="false"
          />
          <MainTable
              v-if="totales.lpAumento[0].total"
              :filas="totales.lpAumento"
              :columnas="columnasTotal"
              :header="false"
              :exportar="false"
          />
        </div>
      </div>
      <!--  CORTO PLAZO -->
      <div
          class="row"
      >
        <div class="col-12 text-center background-alia text-light fw-bold">
          <span class="fs-5">CORTO PLAZO</span>
        </div>
      </div>
      <div
          class="row mt-2"
      >
        <div
            v-for="key in Object.keys(mainTables).filter(k => k.includes('cp'))"
            v-bind:key="key"
            class="col-6"
        >
          <MainTable
              :titulo="mainTables[key].titulo"
              :filas="mainTables[key].data"
              :columnas="getColumnas(key.includes('Origen') ? 'bg-lightgreen' : 'bg-lightpink')"
              :header="false"
              :exportar="false"
          />
        </div>
      </div>
      <div class="row">
        <div class="col-6">
          <MainTable
              :filas="totales.cpOrigen"
              :columnas="columnasTotal"
              :header="false"
              :exportar="false"
          />
          <MainTable
              v-if="totales.cpAumento[0].total"
              :filas="totales.cpAumento"
              :columnas="columnasTotal"
              :header="false"
              :exportar="false"
          />
        </div>
        <div class="col-6">
          <MainTable
              :filas="totales.cpAplicacion"
              :columnas="columnasTotal"
              :header="false"
              :exportar="false"
          />
          <MainTable
              v-if="totales.cpDisminucion[0].total"
              :filas="totales.cpDisminucion"
              :columnas="columnasTotal"
              :header="false"
              :exportar="false"
          />
        </div>
      </div>
      <!-- TOTALES -->
      <div
          class="row"
      >
        <div class="col-12 text-center background-alia text-light fw-bold">
          <span class="fs-5">TOTALES</span>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-6">
          <MainTable
              :filas="totales.origen"
              :columnas="columnasTotal"
              :header="false"
              :exportar="false"
          />
        </div>
        <div class="col-6">
          <MainTable
              :filas="totales.aplicacion"
              :columnas="columnasTotal"
              :header="false"
              :exportar="false"
          />
        </div>
      </div>
    </div>
  </div>
</template>
<script>

import HTML2Image from "@/components/HTML2Image.vue";

export default {
  components: {HTML2Image},
  data(){
    return {
      show: false,
      years: [],
      mainTables: {},
      totales: {
        lpOrigen: [{
          texto: 'Total',
          total: 0
        }],
        lpAplicacion: [{
          texto: 'Total',
          total: 0
        }],
        lpAumento: [{
          texto: 'Aumento circulante (Pasivo LP – Activo LP)',
          total: 0
        }],
        lpDisminucion: [{
          texto: 'Disminución circulante (Pasivo LP – Activo LP)',
          total: 0
        }],
        cpOrigen: [{
          texto: 'Total',
          total: 0
        }],
        cpAplicacion: [{
          texto: 'Total',
          total: 0
        }],
        cpAumento: [{
          texto: 'Aumento circulante (Activo CP – Pasivo CP)',
          total: 0
        }],
        cpDisminucion: [{
          texto: 'Disminución circulante (Activo CP – Pasivo CP)',
          total: 0
        }],
        origen: [{
          texto: 'Total',
          total: 0
        }],
        aplicacion: [{
          texto: 'Total',
          total: 0
        }],
      },
      columnas: [
        {
          key: 'denominacion',
          width: '50%',
          class: 'fw-bold'
        },
        {
          key: 'data',
          type: 'currency',
          width: '50%',
          tdClass: 'bg-lightpink'
        }
      ],
      columnasTotal: [
        {
          key: 'texto',
          width: '50%',
          class: 'fw-bold'
        },
        {
          key: 'total',
          type: 'currency',
          width: '50%',
          tdClass: 'bg-light'
        }
      ]
    }
  },
  mounted() {
    this.getData();
  },
  methods: {
    mapOrigen(d) {
      d.data = d.origen;
      delete d['aplicacion'];
      delete d['origen'];
      return d;
    },
    mapAplicacion(d) {
      d.data = d.aplicacion;
      delete d['aplicacion'];
      delete d['origen'];
      return d;
    },
    getData() {
      this.axios.get('sources/analitica/?CIF=' + JSON.parse(localStorage.getItem('company')).CIF + '&tipo=EOAF')
          .then(r => {
            let data = r.data['EOAF'];
            this.years = r.data['comparacion'];
            const plazos = ['lp', 'cp'];
            const campos = ['Activo', 'Pasivo'];
            const keys = ['Origen', 'Aplicacion'];

            for(const plazo of plazos) {
              for(const campo of campos) {
                for(const key of keys) {
                  this.mainTables[`${plazo}${campo}${key}`] = {titulo: [campo, key === 'Origen' ? (campo === 'Activo' ? 'Disminución' : 'Aumento') : (campo === 'Pasivo' ? 'Disminución' : 'Aumento')].join(' - '), data: []};
                  this.mainTables[`${plazo}${campo}${key}`].data = JSON.parse(JSON.stringify(data))
                                                          .filter(d =>
                                                              d.plazo == plazo.toUpperCase()
                                                              && d.campo == campo.toLowerCase()
                                                              && d[key.toLowerCase()] != 0
                                                          )
                                                          .map(key == 'Origen' ? this.mapOrigen : this.mapAplicacion)
                  ;
                  this.mainTables[`${plazo}${campo}${key}`].data.forEach(v => this.totales[`${plazo}${key}`][0].total += v.data)
                }
              }
            }
            this.totales.origen[0].total = this.totales.lpOrigen[0].total + this.totales.cpOrigen[0].total;
            this.totales.aplicacion[0].total = this.totales.lpAplicacion[0].total + this.totales.cpAplicacion[0].total;

            let lpResta = this.totales.lpOrigen[0].total - this.totales.lpAplicacion[0].total;
            this.totales[lpResta > 0 ? 'lpAumento' : 'lpDisminucion'][0].total = Math.abs(lpResta);

            let cpResta = this.totales.cpOrigen[0].total - this.totales.cpAplicacion[0].total;
            this.totales[cpResta > 0 ? 'cpDisminucion' : 'cpAumento'][0].total = Math.abs(cpResta);

            this.show = true;
          });
    },
    getColumnas(bgColor) {
      return [
        {
          key: 'denominacion',
          width: '50%',
          class: 'fw-bold'
        },
        {
          key: 'data',
          type: 'currency',
          width: '50%',
          tdClass: bgColor
        }
      ]
    },
    getTitulo() {
      const inicio = this.$FormatFecha(this.years[0]);
      const fin = this.$FormatFecha(this.years[1]);
      return `${inicio} - ${fin}`;
    }
  }
}
</script>
<style>

</style>