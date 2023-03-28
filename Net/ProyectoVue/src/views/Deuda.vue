<template>
  <div class="wrapper d-flex align-items-stretch">
    <Sidebar></Sidebar>
    <div id="content">
      <div class="container-fluid">
        <Navbar></Navbar>
        <div class="dds box-60">
          <div class="pool-tittle">análisis de servicio de deuda</div>
          <div class="col-12 row">
            <div class="row dds rt-header">
              <div class="col">fechas</div>
              <div class="col" v-for="cdeuda in Deudas" v-bind:key="cdeuda">
                {{ cdeuda.entidad }}
              </div>
            </div>
            <div class="rt-pools-details" v-html="resultado"></div>
            <div class="rt-pools-details">
              <div class="row total-deuda">
                <div class="col center">TOTAL</div>
                <div class="col center" v-for="total in Totales" v-bind:key="total">
                  {{ $FormatNumero(total) }}
                </div>
              </div>
            </div>
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
  name: "DeudaService",
  components: { Sidebar, Navbar },
  data() {
    return {
      resultado: "",
      resultadoTotal: "",
      Deudas: [],
      Fechas: [],
      totalDispuesto: "",
      Totales: [],
    };
  },
  methods: {
    async LoadData() {
      var form = new FormData();
      form.append("CIF", localStorage.getItem("empresa"));
      await this.axios
        .post("/core/servicioDeuda/", form)
        .then((r) => {
          var deudas = [];
          var n0 = Object.values(r.data);
          var n1 = Object.values(n0[0]);

          for (let i = 0; i < n1.length; i++) {
            var _keys = Object.keys(n1[i]);

            if (_keys[0] == "fechas") {
              this.Fechas = n1[i].fechas;
            } else {
              deudas.push(n1[i]);
            }
          }
          this.Deudas = deudas;
          this.Totales = [];

          for (let index = 0; index < this.Deudas.length; index++) {
            this.Totales.push(0);
          }

          console.log(this.Deudas);
          for (let i = 0; i < this.Fechas.length; i++) {
            this.resultado += "<div class='row rt-pools'>";
            this.resultado +=
              "<div class='col center'>" +
              this.$FormatFecha(this.Fechas[i]) +
              "</div>";
            for (let j = 0; j < this.Deudas.length; j++) {
              var d = this.Deudas[j].pagos[i];

              this.Totales[j] = this.Totales[j] + this.$ConverToInt(d);

              this.resultado +=
                "<div class='col center'>" + this.$FormatNumero(d) + "</div>";
            }
            this.resultado += "</div>";
          }

          console.log(this.Totales);
        })
        .catch((err) => {
          this.$UnBlock();
          console.log(err.response);
        });
    },
  },
  mounted() {
    this.LoadData();
  },
};
</script>
