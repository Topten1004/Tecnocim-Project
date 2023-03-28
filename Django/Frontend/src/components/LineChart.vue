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
  <Line
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
  PointElement,
  LineElement,
  Title,
  Tooltip,
} from 'chart.js'
import { Line } from 'vue-chartjs'

ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
);

export default {
  name: 'LineChart',
  components: {Line},
  props: {
    icono: {
      type: String,
      default: 'fa-chart-line'
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
            backgroundColor: '#213865',
            borderColor: '#008ea5',
          }
        ],
      },
    }
  }
}
</script>