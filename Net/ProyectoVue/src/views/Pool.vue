<template>
  <div class="wrapper d-flex align-items-stretch">
    <Sidebar></Sidebar>
    <div id="content">
      <div class="container-fluid">
        <Navbar></Navbar>
        <div class="dds box-60">
          <div class="pool-tittle">Pool Bancario - Prestamos</div>
          <div class="col-12 row">
            <div class="row dds rt-header">
              <div class="col-1">entidad</div>
              <div class="col">producto</div>
              <div class="col col-1">Concepto</div>
              <div class="col col-1">i. inicial</div>
              <div class="col col-1">dispuesto</div>
              <div class="col col-1">disponible</div>
              <div class="col">inicio</div>
              <div class="col">fin</div>
              <div class="col">f. pago</div>
              <div class="col">carencia</div>
              <div class="col">#</div>
              <div class="col">Cuota</div>
              <!-- <div class="col">Valoración</div> -->
              <!-- <div class="col">Notas</div> -->
              <div class="col">precio</div>
              <!-- <div class="col">Poliza Digitalizada</div>
              <div class="col">Minimis</div> -->
            </div>
            <div class="rt-pools-details">
              <div class="row rt-pools" v-for="pool in PoolsPrestamos" v-bind:key="pool" >
                <div class="col  col-1">{{pool.contrato.entidad_nombre }}</div>
                <div class="col center">{{ pool.contrato.producto }}</div>
                <div class="col col-1 center">{{ pool.concepto }}</div>        
                <div class="col col-1 center"> {{ $FormatNumero(pool.contrato.limite) }}</div>
                <div class="col col-1 center">{{ $FormatNumero(pool.dispuesto) }}</div>
                <div class="col col-1 center">{{ $FormatNumero(pool.contrato.limite -pool.dispuesto ) }}</div>
                <div class="col center">{{ pool.contrato.inicio }}</div>
                <div class="col center">{{ pool.contrato.vencimiento }}</div>
                <div class="col center">{{ getPeriodificacion(pool.contrato.periodificacion) }}</div>
                <div class="col center">{{ pool.contrato.carencia }}</div>
                <div class="col center">{{ pool.contrato.plazos_amortizacion }}</div>
                <div class="col center">{{ pool.contrato.cuota }}</div>
                <!-- <div class="col center">{{pool.contrato.valoracion == 5 ? " ★ ★ ★ ★ ★" : pool.contrato.valoracion == 4 ? " ★ ★ ★ ★ " : pool.contrato.valoracion == 3 ? " ★ ★ ★ " : pool.contrato.valoracion == 2 ? " ★ ★ " : pool.contrato.valoracion == 1 ? " ★ " : "" }}</div>
                <div class="col center">{{ pool.contrato.notas }}</div> -->
                <div class="col center">{{ pool.contrato.precio }}</div>
                <!-- <div class="col center"> {{ pool.contrato.digitalizada ? "SI" : "NO" }}</div>
                <div class="col center">{{ pool.contrato.minimis ? "SI" : "NO" }}</div> -->
              </div>
            </div>
          </div>
          <div class="row total-pool">
            <div class="col">Total</div>
            <div class="col">{{ $FormatNumero(totalPrestamos) }}</div>
          </div>
          <div class="pool-tittle">Pool Bancario - Polizas</div>
          <div class="col-12 row">
            <div class="row dds rt-header">
              <div class="col-1">entidad</div>
              <div class="col">producto</div>
              <div class="col col-1">Concepto</div>
              <div class="col col-1">i. inicial</div>
              <div class="col col-1">dispuesto</div>
              <div class="col col-1">disponible</div>
              <div class="col">inicio</div>
              <div class="col">fin</div>
              <div class="col">f. pago</div>
              <div class="col">carencia</div>
              <div class="col">#</div>
              <div class="col">Cuota</div>
              <!-- <div class="col">Valoración</div> -->
              <!-- <div class="col">Notas</div> -->
              <div class="col">precio</div>
              <!-- <div class="col">Poliza Digitalizada</div>
              <div class="col">Minimis</div> -->
            </div>
            <div class="rt-pools-details">
              <div
                class="row rt-pools"
                v-for="pool in PoolsOtros"
                v-bind:key="pool"
              >
              <div class="col  col-1">{{pool.contrato.entidad_nombre }}</div>
                <div class="col center">{{ pool.contrato.producto }}</div>
                <div class="col col-1 center">{{ pool.concepto }}</div>        
                <div class="col col-1 center"> {{ $FormatNumero(pool.contrato.limite) }}</div>
                <div class="col col-1 center">{{ $FormatNumero(pool.dispuesto) }}</div>
                <div class="col col-1 center">{{ $FormatNumero(pool.contrato.limite -pool.dispuesto ) }}</div>
                <div class="col center">{{ pool.contrato.inicio }}</div>
                <div class="col center">{{ pool.contrato.vencimiento }}</div>
                <div class="col center">{{ getPeriodificacion(pool.contrato.periodificacion) }}</div>
                <div class="col center">{{ pool.contrato.carencia }}</div>
                <div class="col center">{{ pool.contrato.plazos_amortizacion }}</div>
                <div class="col center">{{ pool.contrato.cuota }}</div>
                <!-- <div class="col center">{{pool.contrato.valoracion == 5 ? " ★ ★ ★ ★ ★" : pool.contrato.valoracion == 4 ? " ★ ★ ★ ★ " : pool.contrato.valoracion == 3 ? " ★ ★ ★ " : pool.contrato.valoracion == 2 ? " ★ ★ " : pool.contrato.valoracion == 1 ? " ★ " : "" }}</div>
                <div class="col center">{{ pool.contrato.notas }}</div> -->
                <div class="col center">{{ pool.contrato.precio }}</div>
                <!-- <div class="col center"> {{ pool.contrato.digitalizada ? "SI" : "NO" }}</div>
                <div class="col center">{{ pool.contrato.minimis ? "SI" : "NO" }}</div> -->
              </div>
            </div>
          </div>
          <div class="row total-pool">
            <div class="col">Total</div>
            <div class="col">{{ $FormatNumero(totalOtros) }}</div>
          </div>          
          <div class="pool-tittle">Pool Bancario - Compras</div>
          <div class="col-12 row">
            <div class="row dds rt-header">
              <div class="col-1">entidad</div>
              <div class="col">producto</div>
              <div class="col col-1">Concepto</div>
              <div class="col col-1">i. inicial</div>
              <div class="col col-1">dispuesto</div>
              <div class="col col-1">disponible</div>
              <div class="col">inicio</div>
              <div class="col">fin</div>
              <div class="col">f. pago</div>
              <div class="col">carencia</div>
              <div class="col">#</div>
              <div class="col">Cuota</div>
              <!-- <div class="col">Valoración</div> -->
              <!-- <div class="col">Notas</div> -->
              <div class="col">precio</div>
              <!-- <div class="col">Poliza Digitalizada</div>
              <div class="col">Minimis</div> -->
            </div>
            <div class="rt-pools-details">
              <div
                class="row rt-pools"
                v-for="pool in PoolsCompras"
                v-bind:key="pool"
              >
              <div class="col  col-1">{{pool.contrato.entidad_nombre }}</div>
                <div class="col center">{{ pool.contrato.producto }}</div>
                <div class="col col-1 center">{{ pool.concepto }}</div>        
                <div class="col col-1 center"> {{ $FormatNumero(pool.contrato.limite) }}</div>
                <div class="col col-1 center">{{ $FormatNumero(pool.dispuesto) }}</div>
                <div class="col col-1 center">{{ $FormatNumero(pool.contrato.limite -pool.dispuesto ) }}</div>
                <div class="col center">{{ pool.contrato.inicio }}</div>
                <div class="col center">{{ pool.contrato.vencimiento }}</div>
                <div class="col center">{{ getPeriodificacion(pool.contrato.periodificacion) }}</div>
                <div class="col center">{{ pool.contrato.carencia }}</div>
                <div class="col center">{{ pool.contrato.plazos_amortizacion }}</div>
                <div class="col center">{{ pool.contrato.cuota }}</div>
                <!-- <div class="col center">{{pool.contrato.valoracion == 5 ? " ★ ★ ★ ★ ★" : pool.contrato.valoracion == 4 ? " ★ ★ ★ ★ " : pool.contrato.valoracion == 3 ? " ★ ★ ★ " : pool.contrato.valoracion == 2 ? " ★ ★ " : pool.contrato.valoracion == 1 ? " ★ " : "" }}</div>
                <div class="col center">{{ pool.contrato.notas }}</div> -->
                <div class="col center">{{ pool.contrato.precio }}</div>
                <!-- <div class="col center"> {{ pool.contrato.digitalizada ? "SI" : "NO" }}</div>
                <div class="col center">{{ pool.contrato.minimis ? "SI" : "NO" }}</div> -->
              </div>
            </div>
          </div>
          <div class="row total-pool">
            <div class="col">Total</div>
            <div class="col">{{ $FormatNumero(totalCompras) }}</div>
          </div>
          <div class="pool-tittle">Pool Bancario - Ventas</div>
          <div class="col-12 row">
            <div class="row dds rt-header">
              <div class="col-1">entidad</div>
              <div class="col">producto</div>
              <div class="col col-1">Concepto</div>
              <div class="col col-1">i. inicial</div>
              <div class="col col-1">dispuesto</div>
              <div class="col col-1">disponible</div>
              <div class="col">inicio</div>
              <div class="col">fin</div>
              <div class="col">f. pago</div>
              <div class="col">carencia</div>
              <div class="col">#</div>
              <div class="col">Cuota</div>
              <!-- <div class="col">Valoración</div> -->
              <!-- <div class="col">Notas</div> -->
              <div class="col">precio</div>
              <!-- <div class="col">Poliza Digitalizada</div>
              <div class="col">Minimis</div> -->
            </div>
            <div class="rt-pools-details">
              <div
                class="row rt-pools"
                v-for="pool in PoolsVentas"
                v-bind:key="pool"
              >
              <div class="col  col-1">{{ pool.contrato.entidad_nombre }}</div>
                <div class="col center">{{ pool.contrato.producto }}</div>
                <div class="col col-1 center">{{ pool.concepto }}</div>        
                <div class="col col-1 center"> {{ $FormatNumero(pool.contrato.limite) }}</div>
                <div class="col col-1 center">{{ $FormatNumero(pool.dispuesto) }}</div>
                <div class="col col-1 center">{{ $FormatNumero(pool.contrato.limite -pool.dispuesto ) }}</div>
                <div class="col center">{{ pool.contrato.inicio }}</div>
                <div class="col center">{{ pool.contrato.vencimiento }}</div>
                <div class="col center">{{ getPeriodificacion(pool.contrato.periodificacion) }}</div>
                <div class="col center">{{ pool.contrato.carencia }}</div>
                <div class="col center">{{ pool.contrato.plazos_amortizacion }}</div>
                <div class="col center">{{ pool.contrato.cuota }}</div>
                <!-- <div class="col center">{{pool.contrato.valoracion == 5 ? " ★ ★ ★ ★ ★" : pool.contrato.valoracion == 4 ? " ★ ★ ★ ★ " : pool.contrato.valoracion == 3 ? " ★ ★ ★ " : pool.contrato.valoracion == 2 ? " ★ ★ " : pool.contrato.valoracion == 1 ? " ★ " : "" }}</div>
                <div class="col center">{{ pool.contrato.notas }}</div> -->
                <div class="col center">{{ pool.contrato.precio }}</div>
                <!-- <div class="col center"> {{ pool.contrato.digitalizada ? "SI" : "NO" }}</div>
                <div class="col center">{{ pool.contrato.minimis ? "SI" : "NO" }}</div> -->
              </div>
            </div>
          </div>
          <div class="row total-pool">
            <div class="col">Total</div>
            <div class="col">{{ $FormatNumero(totalVentas) }}</div>
          </div>          
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import Sidebar from "../components/sidebar.vue";
import Navbar from "../components/navbar.vue";
export default {
  name: "PoolDue",
  components: { Sidebar, Navbar },
  data() {
    return {
      Pools:[],
      PoolsPrestamos: [],
      PoolsOtros: [],
      PoolsVentas: [],
      PoolsCompras: [],

      entidades: [],
      totalDispuesto: "",
      totalPrestamos: "",
      totalOtros: "",
      totalVentas: "",
      totalCompras: "",
    };
  },
  methods: {
    getPeriodificacion(periodificacion) {
      switch (periodificacion) {
        case 1:
          return "Mensual";
        case 3:
          return "Trimestral";
        case 4:
          return "Cuatrimestral";
        case 6:
          return "Semestral";
        case 12:
          return "Anual";
        default:
          return "Otros";
      }
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
      // await this.axios.get("/complement/entidad/?id=0").then((r) => {
      //   this.entidades = Object.values(r.data);
      // });

      await this.axios
        .get("/core/pool/", {
          params: {
            CIF: localStorage.getItem("empresa"),
            fk_en_cuenta: "False",
            fecha_en_cuenta: "True",
          },
        })
        .then((r) => {
          this.Pools = Object.values(r.data);
          this.totalDispuesto = 0;

          var totales = Object.values(this.Pools[0]);

          this.totalPrestamos = 0;
          this.totalVentas = 0;
          this.totalCompras = 0;
          this.totalOtros = 0;

          for (let i = 0; i < totales.length; i++) {

            //console.log(totales[i]);

            switch (totales[i].contrato.tipo) {
              case "prestamos":
                    this.PoolsPrestamos.push(totales[i]);
                    this.totalPrestamos += totales[i].dispuesto;
                break;
                case "ventas":
                    this.PoolsVentas.push(totales[i]);
                    this.totalVentas += totales[i].dispuesto;
                break;
                case "compras":
                    this.PoolsCompras.push(totales[i]);
                    this.totalCompras += totales[i].dispuesto;
                break;
              default:
                    this.PoolsOtros.push(totales[i]);
                    this.totalOtros += totales[i].dispuesto;
                break;
            }
            
          }
        });
    },
  },
  mounted() {
    this.LoadData();
  },
};
</script>
