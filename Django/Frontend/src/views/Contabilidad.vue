<template>
  <div v-if="!show" class="text-center">
    <SpinnerAlia />
  </div>
  <div
      v-if="show"
      class="container-fluid">
    <div
          v-for="balance of Object.keys(balances)"
          v-bind:key="balance"
          class="row">
      <MainTable
          v-if="balances[balance]"
          :icono="mainTables.icono"
          :titulo="balances[balance].titulo"
          :columnas="getColumnas(balance, getTipo())"
          :filas="balances[balance].data"
          :opened="true"
          :hidden-ids="hiddenIds"
          zero-value=""
          infinity-value=""
      />
    </div>
  </div>
</template>
<script>
import MainTable from "@/components/MainTable.vue";

export default {
  name: "ContabilidadView",
  components: {
    MainTable,
  },
  props: {
    tipo: {
      type: String,
      default: 'balance'
    }
  },
  mounted() {
    this.getData().then(() => this.show = true);
  },
  data() {
    return {
      show: false,
      balances: {
        activo: {
          titulo: 'ACTIVO',
          data: []
        },
        pasivo: {
          titulo: 'PASIVO',
          data: []
        },
        resultados: {
          titulo: 'RESULTADOS',
          data: []
        }
      },
      ages: [],
      mainTables: {
        icono: 'fa-chart-pie'
      },
      hiddenIds: []
    };
  },
  methods: {
    getData() {
      return this.axios
        .get("/sources/balance/?CIF=" + JSON.parse(localStorage.getItem('company')).CIF)
        .then((r) => {
          const balances = Object.values(r.data)[0];
          const balancesTipos = Object.keys(this.balances);
          for(const balance of balancesTipos) {
            const balancesFilter = balances.filter(b => b.campo == balance);
            let randoms = [];
            let bgColors = [null, null, null, 'background-alia-intensisty-01', 'background-alia-intensisty-02', 'background-alia-intensisty-03', 'background-alia-intensisty-04'];
            for(let i=0; i < balancesFilter.length; i++) {
              if (balancesFilter[i].prioridad == 2) {
                randoms = [null,null,  this.generateString()];
              }
              const diff = balancesFilter[i + 1]?.prioridad - balancesFilter[i].prioridad;
              if (diff > 0) {
                randoms.push(this.generateString(10));
              }
              balancesFilter[i].parent = randoms[balancesFilter[i].prioridad - 1];
              if(diff > 0) {
                balancesFilter[i].children = randoms[balancesFilter[i].prioridad];
              }
              if (diff < 0) {
                for(let j = diff; j < 0; j++) {
                  randoms.pop();
                }
                randoms[randoms.length - 1] = this.generateString(10);
              }
              if (balancesFilter[i].prioridad == 1) {
                balancesFilter[i].class = 'background-alia-gradiente text-light fw-bold';
              }
              if (balancesFilter[i].prioridad == 2) {
                balancesFilter[i].class = 'fw-bold text-secondary';
              }
              if (balancesFilter[i].prioridad > 3) {
                if (!this.hiddenIds.find(h => h == balancesFilter[i].parent)) {
                  this.hiddenIds.push(balancesFilter[i].parent);
                }
              }
              if (balancesFilter[i].prioridad > 2) {
                balancesFilter[i].class += ' ' + bgColors[balancesFilter[i].prioridad];
              }
            }
            this.balances[balance].data = balancesFilter.map(b => {b.tdClass = `ps-${b.prioridad}`; return b;});
          }
        });
    },
    getYears(balance) {
      return Object.keys(this.balances[balance].data[0]).filter(key => key.includes('20'));
    },
    getColumnas(balance, tipo = 'balance') {
      let columnas = [
        {
          key: 'denominacion',
          value: 'Denominación',
          width: '600px'
        },
        {
          key: this.getYears(balance).at(-1),
          value: this.$FormatFecha(this.getYears(balance).at(-1)),
          type: 'currency'
        },

        {
          key: (tipo == 'balance' ? 'evolucion' : 'masa') + '1',
          value: '%',
          type: 'format',
          format: '%'
        },
        {
          key: this.getYears(balance).at(-2),
          type: 'currency'
        },
        {
          key: (tipo == 'balance' ? 'evolucion' : 'masa') + '2',
          value: '%',
          type: 'format',
          format: '%'
        },
        {
          key: this.getYears(balance).at(-3),
          type: 'currency'
        },
      ];

      if (tipo != 'balance') {
        columnas.push({
          key: 'masa3',
          value: '%',
          type: 'format',
          format: '%'
        });
      }

      return columnas;
    },
    getTipo() {
      return location.href.split('/').at(-1);
    }
  },
};
</script>
