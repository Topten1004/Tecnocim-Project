<template>
  <div class="title-box">
    <div class="rt-ico-ratio"></div>
    <div>CONTABILIDAD ANALÍTICA</div>
  </div>
  <div class="box">
    <div class="box-title">
      <div>Ratios rentabilidad</div>
    </div>
    <div class="rt-details">
        <GChart   :settings="{packages: ['bar']}"     :data="chartData" :options="chartOptions" :createChart="(el, google) => new google.charts.Bar(el)" @ready="onChartReady"/>  
    </div>
  </div>
  <div class="box-footer">
    <button>ver más</button>
  </div>
</template>

<script>

import { GChart } from 'vue-google-charts'

export default {
  name: "AccountingBox",
   components: {
    GChart
  },
  data () {
    return {
      chartsLib: null, 
      chartData: [
        ['Year', 'Sales', 'Expenses', 'Profit'],
        ['2014', 1000, 400, 200],
        ['2015', 1170, 460, 250],
        ['2016', 660, 1120, 300],
        ['2017', 1030, 540, 350]
      ]
    } 
  },
  computed: {
    chartOptions () {
      if (!this.chartsLib) return null
      return this.chartsLib.charts.Bar.convertOptions({
        chart: {
        },
        bars: 'horizontal', // Required for Material Bar Charts.
        hAxis: { format: 'decimal' },
        height: 400,
        colors: ['#1b9e77', '#d95f02', '#7570b3']
      })
    }
  },
  methods: {
    onChartReady (chart, google) {
      this.chartsLib = google
    }
  }
}
</script>
