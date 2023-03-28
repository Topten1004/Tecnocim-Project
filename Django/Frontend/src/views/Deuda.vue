<template>
  <div v-if="!show" class="text-center">
    <SpinnerAlia />
  </div>
  <div v-if="show"
      class="container-fluid">
    <div class="row mb-3 text-secondary">
      <div class="col-12 text-center">
        <span class="fs-5 fw-bold">Total: {{ getCurrencyFormat(total) }}</span>
      </div>
    </div>
    <div id="filtro-fechas">
      <div class="row px-5">
        <div class="col-6 ps-5">
          <CustomInput
              v-model="fechaInicio"
              type="month"
          />
        </div>
        <div class="col-6 pe-5">
          <CustomInput
              v-model="fechaFin"
              type="month"
          />
        </div>
      </div>
      <div class="row">
        <div class="col-12 text-center">
          <button class="btn btn-primary" @click="setData()">Aplicar fechas</button>
        </div>
      </div>
    </div>
    <div class="row">
      <MainTable
          :titulo="getTitulo()"
          icono="fa-chart-pie"
          :columnas="columnas"
          :filas="deuda"
          :exportar="true"
      />
    </div>
  </div>
</template>
<script>
import MainTable from "../components/MainTable.vue";
import CustomInput from "@/components/CustomInput.vue";
const columnWidths = {
  fecha: '150px'
};
export default {
  name: "DeudaService",
  components: {
    CustomInput,
    MainTable
  },
  mounted() {
    this.getData().then(() => this.show = true);
  },
  data() {
    return {
      show: false,
      year: 0,
      titulo: 'Deuda',
      icono: 'fa-diagram-project',
      columnas: [
        {
          key: 'fecha',
          value: 'ENTIDADES',
          width: columnWidths.fecha,
          type: 'date',
          center: true
        }
      ],
      deuda: [],
      data: [],
      fechaInicio: null,
      fechaFin: null,
      total: 0
    };
  },
  methods: {
    async getData() {
      return this.axios
        .get("/core/servicioDeuda/?CIF=" + JSON.parse(localStorage.getItem('company')).CIF)
        .then((r) => {
          this.data = Object.values(r.data)[0];
          this.year = Object.keys(r.data)[0];
          this.setData(true);
        });
    },
    setData(first = false) {
      this.show = false;
      this.total = 0;

      this.deuda = [];
      const fechas = this.data.at(-1).fechas.map(fecha => {
        const fechaSplitted = fecha.split('-');
        return [fechaSplitted[0],fechaSplitted[1].length === 1 ? `0${fechaSplitted[1]}` : fechaSplitted[1]].join('-');
      }).filter(fecha => fecha >= (this.fechaInicio ?? '1900-01') && fecha <= (this.fechaFin ?? '3000-01'));
      let totales = {unidades: '€'};

      this.deuda.push({fecha: 'Importe inicial', class: 'background-alia-gradiente text-light fw-bold', unidades: '€'});
      this.deuda.push({fecha: 'Importe pendiente', class: 'background-alia-gradiente text-light fw-bold', unidades: '€'});
      this.deuda.push({fecha: 'Fecha inicio', class: 'background-alia-gradiente text-light fw-bold', unidades: 'date'});
      this.deuda.push({fecha: 'Fecha vencimiento', class: 'background-alia-gradiente text-light fw-bold', unidades: 'date'});
        for(const banco of this.data.slice(0, -1)) {
          if (first) {
            this.columnas.push({key: banco.cuenta, value: banco.entidad, type: 'dynamic', property: 'unidades', width: columnWidths.fecha});
          }
          this.deuda.find(d => d.fecha == 'Importe inicial')[[banco.cuenta]] = banco.cuantia;
          this.deuda.find(d => d.fecha == 'Importe pendiente')[[banco.cuenta]] = banco.pendiente;
          this.deuda.find(d => d.fecha == 'Fecha inicio')[[banco.cuenta]] = banco.inicio;
          this.deuda.find(d => d.fecha == 'Fecha vencimiento')[[banco.cuenta]] = banco.vencimiento;

        let cantidad = 0;
        let fechaActual = null;
        let coloresFecha = ['background-alia-intensisty-01', ''];
        let colorFecha = coloresFecha[0];
        for(const i in fechas) {
          const fecha = fechas[i];
          const pago = banco.pagos[i];
          const fila = this.deuda.find(d => d.fecha == fecha)
          const soloAnio = fecha.split('-')[0];
          if (soloAnio !== fechaActual) {
            fechaActual = soloAnio;
            colorFecha = coloresFecha[colorFecha == coloresFecha[0] ? 1 : 0];
          }
          if(fila) {
            fila[banco.cuenta] = pago;
          } else {
            this.deuda.push({fecha: fecha, unidades: '€', [banco.cuenta]: pago, class: colorFecha});
          }
          if (!Object.keys(totales).includes(banco.cuenta)) {
            totales[banco.cuenta] = 0;
          }
          cantidad = isNaN(pago) ? 0 : pago
          totales[banco.cuenta] += cantidad;
          this.total += cantidad;
        }
      }

      this.deuda.push({fecha: 'Total', class: 'background-alia-gradiente text-light fw-bold', ...totales});
      this.show = true;
    },
    getTitulo() {
      const year = this.$FormatFecha(this.year);
      return `${this.titulo} (${year})`;
    }
  }
};
</script>
