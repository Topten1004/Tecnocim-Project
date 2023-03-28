<template>
  <div class="container-fluid">
    <div class="row">
      <span
        v-for="key of Object.keys(titulos)"
        v-bind:key="key"
      >
        <MainTable
            v-if="pools && pools[key]"
            :icono="icono"
            :titulo="getTitulo(titulos[key])"
            :columnas="getColumnas(key)"
            :filas="pools[key]"
            :footer="true"
        />
      </span>
    </div>
    <ModalWindow
        v-if="cirbe"
        :titulo="modal.titulo"
        :visible="cirbe ? true : false"
        :max-width="modal.maxWidth"
        @closed="closed()"
    >
      <div class="row px-2">
        <div
            v-for="(dato, key) in cirbe"
            v-bind:key="key"
        >
          <div
              v-if="!cirbes.discard.includes(key)"
              class="row"
          >
            <div  class="col-6 border">
              <p
                  class="my-auto fs-6 text-start text-capitalize"
              >
                <span class="fw-bold">{{ key.replace('_', ' ') }}</span>
              </p>
            </div>
            <div  class="col-6 border">
              <p
                  v-if='typeof dato !== "object"'
                  class='my-auto fs-6'
              >
                {{ dato }}
              </p>
              <p
                  v-if='typeof dato === "object" && dato'
                  class='my-auto fs-6'
              >
                <span v-if="!Array.isArray(dato)">{{ dato.tipo }}</span>
                <span v-if="Array.isArray(dato)">{{ dato.map(d => d.tipo).join(', ') }}</span>
              </p>
            </div>
          </div>
        </div>
      </div>
    </ModalWindow>
  </div>
</template>
<script>
import ModalWindow from "../components/ModalWindow.vue";
import MainTable from "@/components/MainTable.vue";

const poolsTypes = [
  'prestamos',
  'otros',
  'ventas',
  'compras'
];

export default {
  name: "PoolDue",
  components: {
    MainTable,
    ModalWindow
  },
  data() {
    return {
      year: 0,
      icono: 'fa-diagram-project',
      titulos: {
        prestamos: 'Pool bancario - Préstamos',
        otros: 'Pool bancario - Pólizas',
        compras: 'Pool bancario - Compras',
        ventas: 'Pool bancario - Ventas',
      },
      //
      pools: {},
      totales: {},
      //
      //
      entidades: [],
      //
      cirbe: null,
      cirbes: {
        discard: [
          'id',
          'entidad_nombre',
          'entidad',
          'contrato',
          'extraccion',
          'documento'
        ]
      },
      modal: {
        titulo: 'Cirbe',
        maxWidth: '1000px',
        visible: 0
      },
      //
      mensajeError: ''
    };
  },
  methods: {
    getPeriodificacion(periodificacion) {
      const periodificaciones = {
        1: 'Mensual',
        3: 'Trimestral',
        4: 'Cuatrimestral',
        6: 'Semestral',
        12: 'Anual'
      }
      return periodificaciones[periodificacion] ?? 'Otros'
    },
    getEntidad(code) {
      var r = "No Disponible";
      this.entidades.forEach((e) => {
        if (Object.keys(e) == code) {
          r = Object.values(e)[0];
          return;
        }
      });
      return r;
    },
    async LoadData() {
      await this.axios
        .get("/core/pool/", {
          params: {
            CIF: JSON.parse(localStorage.getItem('company')).CIF,
            fk_en_cuenta: "False",
            fecha_en_cuenta: "True",
          },
        })
        .then((r) => {
          let pools = Object.values(r.data)[0];
          this.year = Object.keys(r.data)[0];
          pools.map(p => {
            p.contrato__entidad_nombre = p.contrato.entidad_nombre;
            p.contrato__producto = p.contrato.producto;
            p.contrato__limite = p.contrato.limite;
            p.disponible = p.contrato__limite - p.dispuesto;
            p.contrato__inicio = p.contrato.inicio;
            p.contrato__vencimiento = p.contrato.vencimiento;
            p.contrato__periodificacion = this.getPeriodificacion(p.contrato.periodificacion);
            p.contrato__carencia = p.contrato.carencia;
            p.contrato__plazos_amortizacion = p.contrato.plazos_amortizacion;
            p.contrato__cuota = p.contrato.cuota;
            p.contrato__precio = p.contrato.precio;
            p.is_cirbe = Object.keys(p.cirbe).length ? 'Sí' : 'No';
            return p;
          })

          for(const poolType of poolsTypes) {
            this.pools[poolType] = pools.filter(p => p.contrato.tipo == poolType);
            this.pools[poolType].forEach(p => this.totales[poolType] += p.disuesto);
          }

          // this.pools['otros'] = pools.filter(p => !poolsTypes.includes(p.contrato.tipo));
          // this.pools['otros'].forEach(p => this.totales['otros'] += p.disuesto);

        });
    },
    closed() {
      this.cirbe = null;
    },
    getColumnas(tipo) {
      let columnas = [
        {
          key: 'contrato__entidad_nombre',
          value: 'Entidad',
          filter: 'select',
          multiple: true
        },
        {
          key: 'contrato__producto',
          value: 'Producto',
          class: 'd-flex justify-content-center text-capitalize',
          filter: 'select',
          multiple: true
        },
        {
          key: 'concepto',
          width: '300px',
          filter: 'select',
          multiple: true
        },
        {
          key: 'contrato__limite',
          value: tipo == 'prestamos' ? 'Cuantía' : 'Limite',
          type: 'currency',
          filter: 'currency'
        },
        {
          key: 'dispuesto',
          value: tipo == 'prestamos' ? 'Pendiente' : 'Dispuesto',
          type: 'currency',
          filter: 'currency'
        },
        {
          key: 'disponible',
          type: 'currency',
          filter: 'currency'
        },
        {
          key: 'contrato__inicio',
          type: 'date',
          value: 'Inicio',
          filter: 'date'
        },
        {
          key: 'contrato__vencimiento',
          type: 'date',
          value: 'Vencimiento',
          filter: 'date'
        },
        {
          key: 'contrato__periodificacion',
          value: 'Periodicidad',
          filter: 'select',
          multiple: true
        },
        {
          key: 'contrato__carencia',
          value: 'Carencia',
          filter: 'number'
        },
        {
          key: 'contrato__plazos_amortizacion',
          value: 'Plazos',
          filter: 'number'
        },
        {
          key: 'contrato__cuota',
          value: 'Cuota',
          type: 'currency',
          filter: 'number'
        },
        {
          key: 'contrato__precio',
          value: 'Precio',
          type: 'format',
          format: '%',
          filter: 'number'
        },
        {
          key: 'is_cirbe',
          value: 'Cirbe',
          width: '150px',
          filter: 'select',
          multiple: true,
          method: (fila) => Object.keys(fila.cirbe).length ? this.cirbe = fila.cirbe : null
        }
      ];

      if (tipo !== 'prestamos') {
        columnas = columnas.filter(c => !['contrato__carencia', 'contrato__plazos_amortizacion', 'contrato__cuota'].includes(c.key));
      } else {
        columnas = columnas.filter(c => c.key !== 'disponible');
      }

      if (['compras', 'ventas'].includes(tipo)) {
        columnas = columnas.filter(c => c.key !== 'is_cirbe');
      }

      return columnas;
    },
    getTitulo(titulo) {
      const year = this.$FormatFecha(this.year);
      return `${titulo} (${year})`;
    }
  },
  mounted() {
    this.LoadData();
  },
};
</script>
