<template>
    <div class="container-fluid">
      <div
          v-for="type of Object.keys(datos)"
          v-bind:key="type"
          class="row"
      >
        <div
            class="col-8"
        >
          <ColumnTable
            :titulo="datos[type].titulo"
            :columnas='datos[type].columnas'
            :filas='datos[type].data'
            :selectable="true"
            @selected="selected($event, type)"
          />
        </div>
        <div
            class="col-4"
        >
          <PieChart
              v-if="datos[type].values.length && datos[type].porcentajes.length"
              :titulo="datos[type].titulo"
              :keys='datos[type].values'
              :values='datos[type].porcentajes'
              :values-aux="datos[type].porcentajesAux"
              :bg-color-aux="datos[type].bgColorAux"
              :border="true"
          >
            <template #header>
              <AliaIcon
                  v-if="datos[type].data.length"
                  :icon='icons.icon'
                  :class='icons.class'
                  :style="icons.style"
                  @click="showChartModal(type)"
              />
            </template>
          </PieChart>
          <ModalWindow
              v-if="datos[type].modal"
              :titulo="datos[type].titulo"
              :visible="datos[type].modal"
              :max-width="modal.maxWidth"
              @closed="closed"
          >
            <template #icono>
              <AliaIcon
                  :icon='modal.icons.icon'
                  :class='modal.icons.class'
              />
            </template>
            <PieChart
                v-if="datos[type].values.length && datos[type].porcentajes.length"
                :keys='datos[type].values'
                :values='datos[type].porcentajes'
                :values-aux="datos[type].porcentajesAux"
                :bg-color-aux="datos[type].bgColorAux"
                :padding="pieChart.padding"
                :selectable="true"
            />
          </ModalWindow>
        </div>
      </div>
  </div>
</template>
<script>
import PieChart from '../../components/PieChart.vue';
import ColumnTable from '../../components/ColumnTable.vue';
import ModalWindow from '../../components/ModalWindow.vue';

export default {
  name: "PoolCirbe",
  components: {
    ColumnTable,
    PieChart,
    ModalWindow
  },
  data() {
    return {
      chartKeys: [],
      chartValues: [],
      datos: {
        total: {
          titulo: 'TOTAL POR ENTIDADES',
          values: [],
          deudas: [],
          porcentajes: [],
          data: [],
          modal: false,
          columnas: [
            {
              key: 'entidad',
              value: 'Entidad'
            },
            {
              key: 'total',
              value: 'Total',
              type: 'currency'
            },
            {
              key: 'porcentaje',
              value: '%',
              type: 'format',
              format: '%'
            }
          ]
        },
        inversion: {
          titulo: 'INVERSIÓN POR ENTIDADES',
          values: [],
          deudas: [],
          porcentajes: [],
          data: [],
          modal: false,
          columnas: [
            {
              key: 'entidad',
              value: 'Entidad'
            },
            {
              key: 'pendiente',
              value: 'Total',
              type: 'currency'
            },
            {
              key: 'porcentaje',
              value: '%',
              type: 'format',
              format: '%'
            }
          ]
        },
        circulante: {
          titulo: 'CIRCULANTE POR ENTIDADES',
          values: [],
          deudas: [],
          porcentajes: [],
          data: [],
          modal: false,
          columnas: [
            {
              key: 'entidad',
              value: 'Entidad'
            },
            {
              key: 'dispuesto',
              value: 'Total dispuesto',
              type: 'currency'
            },
            {
              key: 'porcentaje_dispuesto',
              value: '%',
              background: true,
              type: 'format',
              format: '%'
            },
            {
              key: 'limite',
              value: 'Total máximo',
              type: 'currency'
            },
            {
              key: 'porcentaje_limite',
              value: '%',
              type: 'format',
              format: '%'
            }
          ]
        },
        producto: {
          titulo: 'DEUDA POR PRODUCTO',
          values: [],
          deudas: [],
          porcentajes: [],
          data: [],
          modal: false,
          columnas: [
            {
              key: 'producto',
              value: 'Producto'
            },
            {
              key: 'dispuesto',
              value: 'Total dispuesto',
              type: 'currency'
            },
            {
              key: 'porcentaje_dispuesto',
              value: '%',
              background: true,
              type: 'format',
              format: '%'
            },
            {
              key: 'limite',
              value: 'Total máximo',
              type: 'currency'
            },
            {
              key: 'porcentaje_limite',
              value: '%',
              type: 'format',
              format: '%'
            }
          ]
        }
      },
      //
      pieChart: {
        padding: 150
      },
      modal: {
        icons: {
          icon: 'fa-chart-pie',
          class: 'me-3 fs-5'
        },
        maxWidth: '860px'
      },
      icons: {
        icon: 'fa-up-right-from-square',
        class: 'fs-5',
        style: 'cursor: pointer;'
      }
    };
  },
  mounted() {
    this.LoadData();
  },
  methods: {
    async LoadData() {
      this.axios.get('/core/deuda_agregada/?CIF=' + JSON.parse(localStorage.getItem('company')).CIF)
        .then(r => {
          const responsedatos = r.data;

          for(const type of Object.keys(this.datos)) {
            const data = responsedatos[type];
            this.datos[type].data = data;
            this.datos[type].data.push({
              entidad: 'Total',
              total: responsedatos[type].map(f => {return f.total}).reduce((sum, current) => {return sum + current}),
              porcentaje: Math.round(responsedatos[type].map(f => {return f.porcentaje}).reduce((sum, current) => {return sum + current})),
              pendiente: responsedatos[type].map(f => {return f.pendiente}).reduce((sum, current) => {return sum + current}),
              dispuesto: responsedatos[type].map(f => {return f.dispuesto}).reduce((sum, current) => {return sum + current}),
              porcentaje_dispuesto: Math.round((responsedatos[type].map(f => {return f.porcentaje_dispuesto}).reduce((sum, current) => {return sum + current})) * 100) / 100,
              limite: responsedatos[type].map(f => {return f.limite}).reduce((sum, current) => {return sum + current}),
              porcentaje_limite: Math.round((responsedatos[type].map(f => {return f.porcentaje_limite}).reduce((sum, current) => {return sum + current})) * 100) / 100,
              class: 'background-alia-gradiente text-light',
              selectable: false
            });
            this.setData(responsedatos[type], type);
          }
        });
    },
    showChartModal(chart) {
      for(const chartName of Object.keys(this.datos)) {
        this.datos[chartName].modal = false;
      }
      this.datos[chart].modal = true;
    },
    closed() {
      for(const chartName of Object.keys(this.datos)) {
        this.datos[chartName].modal = false;
      }
    },
    selected($event, type) {
      this.datos[type].values = [];
      this.datos[type].porcentajes = [];
      setTimeout(() => {
        const data = $event.length > 0
                ? this.datos[type].data.filter(d => $event.includes(d))
                : this.datos[type].data
        ;
        this.setData(data, type);
      });
    },
    setData(data, type) {
      this.datos[type].values = data.filter(d => d.entidad !== 'Total').map(d => d[type !== 'producto' ? 'entidad' : 'producto']).map(v => v.at(0).toUpperCase() + v.slice(1, v.length));
      this.datos[type].porcentajes = data.filter(d => d.entidad !== 'Total').map(d => d.porcentaje ?? d.porcentaje_limite);
      this.datos[type].porcentajesAux = [];
      // Porcentajes auxiliares
      let porcentajesAux = [];
      let bgColorAux = [];
      for(const index in this.datos[type].porcentajes) {
        const porcentaje = this.datos[type].porcentajes[index];
        const porcentajeDispuesto = this.datos[type].data[index]?.porcentaje_dispuesto;
        const restoPorcentaje = 100 - porcentajeDispuesto;
        const valorDispuesto = porcentaje * (porcentajeDispuesto / 100);
        const valorResto = porcentaje * (restoPorcentaje / 100);
        if (['circulante', 'producto'].includes(type)) {
          if (valorDispuesto) {
            porcentajesAux.push(valorDispuesto);
            bgColorAux.push('#A6A6A6');
          }
          if (valorResto) {
            porcentajesAux.push(valorResto);
            bgColorAux.push('#B1D8B7');
          }
        }
      }
      this.datos[type].porcentajesAux = porcentajesAux;
      this.datos[type].bgColorAux = bgColorAux;
    }
  }
};
</script>
