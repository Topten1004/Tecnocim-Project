<template>
  <div v-if="!show" class="text-center">
    <SpinnerAlia />
  </div>
  <div
      v-if="show"
      class="container-fluid"
  >
    <div class="row">
      <div class="col-4">
        <LineChart
            :titulo="lineChart.titulo"
            :keys="lineChart.keys"
            :values="lineChart.values"
        />
      </div>
      <div class="col-8">
        <MainTable
            :titulo="mainTable.titulo"
            :icono="mainTable.icono"
            :filas="mainTable.data"
            :columnas="getColumnas()"
        />
      </div>
    </div>
  </div>
</template>
<script>
import LineChart from "../components/LineChart.vue"

export default {
  name: 'CoberturaView',
  components: {LineChart},
  data(){
    return {
      year: 0,
      show: false,
      mainTable: {
        titulo: 'EBITDA vs Servicio de deuda',
        data: [],
      },
      lineChart: {
        titulo: 'COBERTURA',
        icono: 'fa-chart-pie',
        label: '% de cobertura',
        keys: [],
        values: []
      }
    }
  },
  mounted() {
    this.getData();
  },
  methods: {
    getData() {
      this.axios.get('sources/cobertura/?CIF=' + JSON.parse(localStorage.getItem('company')).CIF)
          .then(r => {
            let data = Object.values(r.data)[0];
            this.year = this.$FormatFecha(Object.keys(r.data)[0].split(' ')[1]);

            let cobertura = data.find(d => d.concepto == 'cobertura');
            cobertura.class = 'background-alia-gradiente text-light fw-bold';
            data.find(d => d.concepto == 'EBITDA').unidades = '€';
            data.find(d => d.concepto == 'Servicio').unidades = '€';
            cobertura.unidades = '%';

            this.mainTable.data = data;

            const keys = this.getYears();
            this.lineChart.keys = keys;
            this.lineChart.values = [
              (cobertura[keys.at(0)] > 1000 ? 1000 : cobertura[keys.at(0)]) ?? 0,
              (cobertura[keys.at(1)] > 1000 ? 1000 : cobertura[keys.at(1)]) ?? 0,
              (cobertura[keys.at(2)] > 1000 ? 1000 : cobertura[keys.at(2)]) ?? 0,
            ];
            this.show = true;
          })
    },
    getYears() {
      return Object.keys(this.mainTable.data[0]).filter(key => key.includes('20') && !isNaN(parseInt(key)));
    },
    getColumnas() {
      const numColumnas = 6;
      const width = 100/numColumnas;
      return [
        {
          key: 'concepto',
          width: width + '%'
        },
        {
          key: this.year,
          type: 'dynamic',
          property: 'unidades',
          width: width + '%'
        },
        {
          key: 'tendencia ' + this.getYears().at(-2) + '-' + this.getYears().at(-1),
          value: 'Tendencia',
          type: 'format',
          format: '%',
          width: width + '%'
        },
        {
          key: this.getYears().at(-2),
          type: 'dynamic',
          property: 'unidades',
          width: width + '%'
        },
        {
          key: 'tendencia ' + this.getYears().at(-3) + '-' + this.getYears().at(-2),
          value: 'Tendencia',
          type: 'format',
          format: '%',
          width: width + '%'
        },
        {
          key: this.getYears().at(-3),
          type: 'dynamic',
          property: 'unidades',
          width: width + '%'
        },
      ];
    },
  }
}
</script>
<style>

</style>