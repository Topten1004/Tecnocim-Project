  <template>
    <div v-if="!show" class="text-center">
      <SpinnerAlia />
    </div>
    <div v-if="show"
        class="container-fluid"
    >
      <div class="row">
        <MainTable
            :titulo="getTitulo()"
            :icono="icono"
            :columnas="columnas"
            :filas="analitica"
            :values-colored="true"
            zero-value=""
            infinity-value=""
        />
      </div>
    </div>
</template>

<script>
import MainTable from "@/components/MainTable.vue";

export default {
  name: "AnaliticaComponent",
  components: {
    MainTable
  },
  data() {
    return {
        show: false,
        year: 0,
        analitica: [],
        titulo: 'Analítica',
        icono: 'fa-chart-simple',
        columnas: [
          {
            key: 'cuenta',
            width: '600px'
          },
          {
            key: 'magnitud',
            type: 'currency',
            colored: true
          },
          {
            key: 'porcentaje_gastos',
            value: '% sobre gastos',
            type: 'format',
            format: '%'
          },
          {
            key: 'porcentaje_ventas',
            value: '% sobre ventas',
            type: 'format',
            format: '%'
          },
          {
            key: 'porcentaje_ingresos',
            value: '% sobre ingresos',
            type: 'format',
            format: '%'
          }
        ]
    }
  },
  mounted() {
    this.getAnalitica();
  },
  methods: {
    getAnalitica() {
        this.axios.get('sources/analitica/?CIF=' + JSON.parse(localStorage.getItem('company')).CIF)
            .then(r => {
              const final = ['Ventas', 'Ingresos', 'Gastos', 'Beneficios', 'BAII', 'BAI', 'EBITDA']
              let data = Object.values(r.data)[0];
              this.year = Object.keys(r.data)[0];
              let parent = null;
              let parent2 = null;
              // let bgColors = [null, null, null, 'background-alia-intensisty-01', 'background-alia-intensisty-02', 'background-alia-intensisty-03', 'background-alia-intensisty-04'];
              for(const item of data) {
                  if (final.includes((item.concepto))) {
                    item.class = 'fw-bold background-alia-gradiente text-light';
                    item.colored = false;
                    continue;
                  }
                  if (item.concepto == item.cuenta) { // Grupo
                    if (item.concepto == item.raiz) {
                      if (item.magnitud != 0) {
                        parent = this.generateString(10);
                        item.children = parent;
                      }
                      item.class = 'fw-bold text-secondary';
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
                this.analitica = data;
                this.show = true;
            });

    },
    getTitulo() {
      const year = this.$FormatFecha(this.year);
      return `${this.titulo} (${year})`;
    }
  }
};
</script>