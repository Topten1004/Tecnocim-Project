<template>
  <div v-if="titulo">
    <div className="row mb-3">
      <div className="col-9">
        <HeaderTitle
            :icono="icono"
            :titulo="titulo"
        />
      </div>
      <div className="col-2 p-0 mt-2 text-end m-auto">
        <slot name="header"></slot>
        <HTML2Image
            :identificador="identificador"
            type="chart"
            class="ms-1 fs-5"
        />
      </div>
    </div>
  </div>
  <Bar
      :id="identificador"
      :chart-data="chartData"
  />
</template>
<script>
// Chartjs
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
} from 'chart.js'
import { Bar } from 'vue-chartjs'

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
);

export default {
  name: 'BarChart',
  components: {Bar},
  props: {
    icono: {
      type: String,
      default: 'fa-chart-column'
    },
    titulo: {
      type: String
    },
    keys: {
      required: true,
      type: Array,
      default: () => []
    },
    values: {
      required: true,
      type: Array,
      default: () => []
    },
  },
  data() {
    const identificador = this.generateString();
    return {
      identificador: identificador,
      chartData: {
        labels: this.keys,
        datasets: [
          {
            data: this.values,
            backgroundColor: '#008ea5',
          }
        ],
      },
    }
  }
}
</script>