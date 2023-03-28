<template>
  <div v-if="!show" class="text-center">
    <SpinnerAlia />
  </div>
  <div class="container-fluid" v-if="show">
    <div
        v-for="ratio in Object.keys(ratios)"
        v-bind:key="ratio"
        class="row"
    >
      <div class="col-12">
        <MainTable
            :icono="mainTables.icono"
            :titulo="ratios[ratio].titulo"
            :columnas="getColumnas(ratio)"
            :filas="ratios[ratio].data"
            :opened="true"
            :noReplaces="['', 'inf', '-inf']"
        />
      </div>
    </div>
  </div>
</template>
<script>
import MainTable from "../components/MainTable.vue";
import SpinnerAlia from "@/components/Spinner.vue";

export default {
  name: "RatiosAccount",
  components: {
    SpinnerAlia,
    MainTable
  },
  mounted() {
    this.getData().then(() => {
      this.show = true;
    });
  },
  data() {
    return {
      show: false,
      years: [],
      ratios: {
        working: {
          titulo: 'Working capital',
          data: []
        },
        endeudamiento: {
          titulo: 'Endeudamiento',
          data: []
        },
        rentabilidad: {
          titulo: 'Rentabilidad',
          data: []
        },
        cuenta: {
          titulo: 'Cuenta de resultados',
          data: []
        },
        retorno: {
          titulo: 'Retorno en años',
          data: []
        },
      },
      mainTables: {
        columnas: [],
        icono: 'fa-area-chart'
      }
    };
  },
  methods: {
    async getData() {
      return this.axios.all([
        this.getRatios('working capital'),
        this.getRatios('endeudamiento'),
        this.getRatios('rentabilidad'),
        this.getRatios('cuenta de resultados'),
        this.getRatios('retorno en años'),
      ]);
    },
    async getRatios(table) {
      return this.axios.get("/core/ratiosintegrados/?CIF=" + JSON.parse(localStorage.getItem('company')).CIF + '&tabla=' + table)
        .then(r => {
          if (table) {
            // this.ratios[table.includes(' ') ? table.split(' ')[0]: table].data = Object.values(r.data)[0];
            let data = Object.values(r.data)[0];
            let auxData = JSON.parse(JSON.stringify(data));
            const years = Object.keys(auxData[0]).filter(key => key.includes('20'))
            let finalData = [];
            let subgrupos = {};
            for(const i in data) {
              const subgrupo = data[i].subgrupo;
              if (!Object.keys(subgrupos).includes(subgrupo)) {
                subgrupos[subgrupo] = data[i];
                subgrupos[subgrupo].children = this.generateString();
                subgrupos[subgrupo].class = 'fw-bold';
              } else {
                for(const key of Object.keys(subgrupos[subgrupo])) {
                  if (typeof data[i][key] === 'number' || data[i][key] === null) {
                    // subgrupos[subgrupo][key] = 'hidden';
                  }
                }
                delete data[i];
              }
            }
            for(const key in subgrupos) {
              subgrupos[key].ratio = subgrupos[key].subgrupo;
              finalData.push(subgrupos[key]);
              finalData = finalData.concat(auxData.filter(i => i.subgrupo == key).map(i => {i.parent = subgrupos[key].children; return i;}));
            }
            for(let i=0;i<finalData.length;i++) {
              if (finalData[i].children) {
                if (finalData[i].ratio == finalData[i+1].ratio) {
                  finalData[i+1].remove = true;
                } else {
                  finalData[i][years.at(-1)] = 'hidden';
                  finalData[i]['evolucion1'] = 'hidden';
                  finalData[i][years.at(-2)] = 'hidden';
                  finalData[i]['evolucion2'] = 'hidden';
                  finalData[i][years.at(-3)] = 'hidden';
                }
              }
            }
            finalData = finalData.filter(i => !i.remove);
            finalData.map(d => {d.class += !('class' in d) ? ' background-alia-intensisty-01' : ' ';return d;}) // TODO: Implementar en componente principal
            this.ratios[table.includes(' ') ? table.split(' ')[0]: table].data = finalData;
          }
        });
    },
    getYears(table) {
      return Object.keys(this.ratios[table].data[0]).filter(key => key.includes('20'));
    },
    getColumnas(table) {
      const width = '250px';
      return [
        {
          // key: 'subgrupo',
          key: 'ratio',
          value: '',
          width: '300px'
        },
        {
          key: this.getYears(table).at(-1),
          type: 'dynamic',
          property: 'unidades',
          width: width
        },
        {
          key: 'evolucion1',
          value: 'Evolución 1',
          type: 'format',
          format: '%',
          width: width
        },
        {
          key: this.getYears(table).at(-2),
          type: 'dynamic',
          property: 'unidades',
          width: width
        },
        {
          key: 'evolucion2',
          value: 'Evolución 2',
          type: 'format',
          format: '%',
          width: width
        },
        {
          key: this.getYears(table).at(-3),
          type: 'dynamic',
          property: 'unidades',
          width: width
        }
      ];
    }
  }
};
</script>


