<template>
  <div class="wrapper d-flex align-items-stretch">
    <Sidebar></Sidebar>
    <div id="content">
      <div class="container-fluid">
        <Navbar></Navbar>
        <div class="dds box-60 box-scroll">
          <div class="pool-tittle">BALANCE DE SITUACIÓN ACTIVOS</div>
          <div class="col-12 row  mb-50">
            <div class="row dds rt-header">
              <div class="col text-left">Denominación</div>
              <div class="col">Magnitud</div>
            </div>
            <div class="rt-pools-details">
              <div
                class="row rt-pools"
                v-for="_activo in activo"
                v-bind:key="_activo"
              >
                <div class="col upper " v-bind:class="{
                      prioridad1: _activo.prioridad == '1'
                    }">
                  <div
                    v-bind:class="{
                      prioridad2: _activo.prioridad == '2',
                      prioridad4: _activo.prioridad == '4'                      
                      
                    }"
                  >
                    {{ _activo.denominacion }}
                  </div>
                </div>
                <div
                  class="col center"
                  v-bind:data-prioridad="_activo.prioridad"
                  v-bind:class="{ prioridad2: _activo.prioridad == '2',
                      prioridad1: _activo.prioridad == '1' }"
                >
                  {{ $FormatNumero(_activo.magnitud) }}
                </div>
              </div>
            </div>
          </div>
          <div class="pool-tittle">BALANCE DE SITUACIÓN PASIVOS</div>
          <div class="col-12 row mb-50">
            <div class="row dds rt-header">
              <div class="col text-left">Denominación</div>
              <div class="col">Magnitud</div>
            </div>
            <div class="rt-pools-details">
              <div
                class="row rt-pools"
                v-for="_activo in pasivo"
                v-bind:key="_activo"
              >
                <div class="col upper " v-bind:class="{
                      prioridad1: _activo.prioridad == '1'
                    }">
                  <div
                    v-bind:class="{
                      prioridad2: _activo.prioridad == '2',
                      prioridad4: _activo.prioridad == '4'                      
                      
                    }"
                  >
                    {{ _activo.denominacion }}
                  </div>
                </div>
                <div
                  class="col center"
                  v-bind:data-prioridad="_activo.prioridad"
                  v-bind:class="{ prioridad2: _activo.prioridad == '2',
                      prioridad1: _activo.prioridad == '1' }"
                >
                  {{ $FormatNumero(_activo.magnitud) }}
                </div>
              </div>
            </div>
          </div>
          <div class="pool-tittle">BALANCE DE SITUACIÓN RESULTADOS</div>
          <div class="col-12 row mb-50">
            <div class="row dds rt-header">
              <div class="col text-left">Denominación</div>
              <div class="col">Magnitud</div>
            </div>
            <div class="rt-pools-details">
              <div
                class="row rt-pools"
                v-for="_activo in resultado"
                v-bind:key="_activo"
              >
                <div class="col upper " v-bind:class="{
                      prioridad1: _activo.prioridad == '1'
                    }">
                  <div
                    v-bind:class="{
                      prioridad2: _activo.prioridad == '2',
                      prioridad4: _activo.prioridad == '4'                      
                      
                    }"
                  >
                    {{ _activo.denominacion }}
                  </div>
                </div>
                <div
                  class="col center"
                  v-bind:data-prioridad="_activo.prioridad"
                  v-bind:class="{ prioridad2: _activo.prioridad == '2',
                      prioridad1: _activo.prioridad == '1' }"
                >
                  {{ $FormatNumero(_activo.magnitud) }}
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
  name: "BalancePyG",
  components: { Sidebar, Navbar },
  data() {
    return {
      activo: [],
      pasivo: [],
      resultado: [],
    };
  },
  methods: {
    async LoadData() {
      var form = new FormData();
      form.append("CIF", localStorage.getItem("empresa"));
      await this.axios
        .post("/sources/balanceSingle/", form)
        .then((r) => {
          var tmp = r.data;

          for (let i = 0; i < tmp.length; i++) {
            if (tmp[i].campo == "activo") {
              this.activo.push(tmp[i]);
            } else if (tmp[i].campo == "pasivo") {
              this.pasivo.push(tmp[i]);
            } else if (tmp[i].campo == "resultados") {
              this.resultado.push(tmp[i]);
            }
          }
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
