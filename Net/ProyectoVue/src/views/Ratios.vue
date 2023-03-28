<template>
  <div class="wrapper d-flex align-items-stretch">
    <Sidebar></Sidebar>
    <div id="content">
      <div class="container-fluid">
        <Navbar></Navbar>
        <div class="dds box-60">
          <div class="pool-tittle">Ratios</div>
          <div class="col-12 row">
            <div class="row dds rt-header">
              <div class="col text-left">Concepto</div>              
              <div class="col">Magnitud</div>              
            </div>
            <div class="rt-pools-details">
              <div class="row rt-pools" v-for="ratio in  Ratios" v-bind:key="ratio" > 
              <div class="col upper ">
                        {{ratio.concepto}}
              </div>
              <div class="col center">
                        {{ $FormatNumeroNoSign(ratio.magnitud)}}
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
  name: "RatiosAccount",
  components: { Sidebar, Navbar },
  data() {
    return {
      Ratios: [],
      totalDispuesto: "",
    };
  },
  methods: {
    async LoadData() {
      await this.axios.get("/core/ratio/?documento__empresa__CIF=" +localStorage.getItem("empresa") )
        .then((r) => {
            this.Ratios = r.data;
            console.log(this.Ratios);
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


