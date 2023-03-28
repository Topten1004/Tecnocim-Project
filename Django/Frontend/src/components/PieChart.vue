<template>
    <div v-if="titulo">
        <div class="row mb-3">
            <div class="col-9">
                <HeaderTitle
                    :icono="icono"
                    :titulo="titulo"
                />
            </div>
            <div class="col-2 p-0 mt-2 text-end m-auto">
                <slot name="header"></slot>
                <HTML2Image
                    :identificador="identificador"
                    type="chart"
                    class="ms-1 fs-5"
                />
            </div>
        </div>
    </div>
    <Pie
      :id="identificador"
      :chart-data="chartData"
      :chart-options="chartOptions"
      :class="{ border: border }"
    />
</template>
<script>
// Chartjs
import { Pie } from 'vue-chartjs'
import { Chart as ChartJS, ArcElement } from 'chart.js'
import ChartDataLabels from 'chartjs-plugin-datalabels'
import uniqolor from "uniqolor";

ChartJS.register(ArcElement, ChartDataLabels);

export default {
    name: 'PieChart',
    components: {Pie},
    props: {
        icono: {
          type: String,
          default: 'fa-chart-pie'
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
        valuesAux: {
          required: false,
          type: Array,
          default: () => []
        },
        bgColorAux: {
          required: false,
          type: Array,
          default: () => []
        },
        border: {
            type: Boolean
        },
        padding: {
            type: Number,
            default: 50
        },
        fontsize: {
            type: String,
            default: '12px'
        }
    },
    data() {
        const identificador = this.generateString();

        // Datasets
        let datasets = [
          {
            id: 'main',
            data: this.values,
            backgroundColor: this.keys.map(k => {return uniqolor(k).color;}), // this.bgColor ?? backgroundColor,
            hoverOffset: 20
          },
        ];
        // Dataset auxiliar
        if(this.valuesAux.length) {
          datasets.push({
            id: 'aux',
            data: this.valuesAux,
            backgroundColor: this.bgColorAux,
            hoverOffset: 20,
          });
        }

        return {
            identificador: identificador,
            colorsCount: 0,
            chartData: {
                labels: this.keys,
                datasets: datasets,
            },
            chartOptions: {
                layout: {
                    padding: this.padding
                },
                responsive: true,
                plugins: {
                    datalabels: {
                        labels: {
                            title: {
                                font: {
                                    weight: 'bold',
                                    size: this.fontsize
                                }
                            },
                        },
                        anchor: 'end',
                        align: 'end',
                        formatter: function(value, context) {
                          if (context.dataset.id !== 'main') {
                            return '';
                          }
                          let sum = 0;
                          let dataArr = context.chart.data.datasets[0].data;
                          dataArr.map(data => {
                            sum += data;
                          });
                          let percentage = (value*100 / sum).toFixed(2)+"%";
                          return context.chart.data.labels[context.dataIndex] + '\n' + percentage;
                        }
                    }
                }
            },
        }
    },
}
</script>