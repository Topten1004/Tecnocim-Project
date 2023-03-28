<template>
  <div v-if="!show" class="text-center">
    <SpinnerAlia />
  </div>
  <div v-if="show"
      class="row"
  >
    <div class="col-8">
      <MainTable
        :titulo="getTitulo()"
        :icono="mainTable.icono"
        :filas="mainTable.data"
        :columnas="mainTable.columnas"
      />
    </div>
    <div class="col-4">
      <BarChart
          v-if="show"
          :titulo="barChart.titulo"
          :keys='barChart.keys'
          :values='barChart.values'
          :padding="100"
          :border="true"
      />
    </div>
  </div>
</template>
<script>

import BarChart from "@/components/BarChart.vue";

export default {
  components: {BarChart},
  data(){
    return {
      show: false,
      year: 0,
      mainTable: {
        titulo: 'Estado de valor añadido EVA',
        icono: 'fa-area-chart',
        data: [],
        columnas: [
          {
            key: 'cuenta',
            width: '600px'
          },
          {
            key: 'Magnitud',
            type: 'currency',
            value: '€',
            colored: true,
          },
          {
            key: 'Beneficios',
            type: 'currency',
            value: 'Beneficio',
            colored: true,
          },
          {
            key: 'Beneficios/Ventas',
            type: 'format',
            format: '%',
            value: '% beneficio sobre ventas',
            colored: true,
          }
        ]
      },
      barChart: {
        titulo: 'Porcentaje margen bruto',
        icono: 'fa-area-chart',
        keys: [],
        value: []
      }
    }
  },
  mounted() {
    this.getData();
  },
  methods: {
    getData() {
      this.axios.get('sources/analitica/?CIF=' + JSON.parse(localStorage.getItem('company')).CIF + '&tipo=EVA')
          .then(r => {
            const final = ['Beneficios']
            let data = Object.values(r.data)[0];
            this.year = Object.keys(r.data)[0];
            let parent = null;
            let parent2 = null;
            let chartData = {}
            for(const item of data) {
              if (final.includes((item.concepto))) {
                item.class = 'fw-bold background-alia-gradiente text-light';
                item.colored = false;
                continue;
              }
              if (item.concepto == item.cuenta) { // Grupo
                if (item.concepto == item.raiz) {
                  if (item.Magnitud != 0) {
                    parent = this.generateString(10);
                    item.children = parent;
                  }
                  item.class = 'fw-bold text-secondary';
                  if (item.raiz != 'ventas') {
                    chartData[item.raiz] = item['Beneficios/Ventas'];
                  }
                } else { // Subgrupo
                  parent2 = this.generateString(10);
                  item.children = parent2;
                  item.parent = parent;
                  item.class += ' background-alia-intensisty-01';
                }
              } else { // Hijo de subgrupo
                item.parent = parent2;
                item.tdClass = 'ps-3';
                item.class += ' background-alia-intensisty-02';
              }
            }

            this.mainTable.data = data;
            this.barChart.keys = Object.keys(chartData);
            this.barChart.values =  Object.values(chartData);
            this.show = true;
          });
    },
    getTitulo() {
      const year = this.$FormatFecha(this.year);
      return `${this.mainTable.titulo} (${year})`;
    }
  }
}
</script>
<style>

</style>