<template>
  <div class="dv-nav">
    <nav class="navbar">
      <div class="client">cliente</div>
      <div class="split"></div>
      <div class="business" v-bind:onclick="showClient">{{ empresa }}</div>
      <div class="ico"></div>
      <div class="user-in">
        <span>Luis Perez Herbera</span>
        <span>Consultor financiero</span>
      </div>
      <div>
        <div></div>
        <div></div>
        <div></div>
      </div>
    </nav>
  </div>
  <div class="cl-selector">
    <div class="cl-finder row">
      <div class="col">Selecciona un cliente</div>
      <div class="col">
        <input type="text" />
        <button>Buscar</button>
      </div>
    </div>
    <div class="cl-title row">
      <div class="col-2"># cliente</div>
      <div class="col-5">Nombre comercial</div>
      <div class="col-3">e-mail</div>
      <div class="col-1">Acción</div>
    </div>
    <div class="cl-detail">
      <div
        class="cl-detail-line row"
        v-for="cliente in Clientes"
        v-bind:key="cliente"
      >
        <div class="col-2">{{ cliente.CIF }}</div>
        <div class="col-5">{{ cliente.nombre }}</div>
        <div class="col-3">{{ cliente.email }}</div>

        <div class="col-1 cl-action" @click="Redirect(cliente.documents, cliente.CIF, cliente.nombre)">
          
        </div>

      </div>
    </div>
  </div>
</template>
<script>
const $ = require("jquery");
window.$ = $;

export default {
  name: "NavBar",
  components: {},
  data() {
    return {
      Clientes: [],
      empresa: "SELECCIONE UN CLIENTE",
    };
  },
  mounted() {
    if(localStorage.getItem("empresaStr")  !=  null){
      this.empresa = localStorage.getItem("empresaStr");
    }else{
      $(".cl-selector").toggleClass("show");
    }
    
    this.axios.get("/core/empresas").then((r) => {
      this.Clientes = r.data;
    });
  },
  methods: {
    Redirect(item, empresa, nombre) {

      console.log(item);
      console.log(empresa);
      console.log(nombre);

      localStorage.setItem("empresa", empresa);
      localStorage.setItem("empresaStr", nombre);
      this.empresa = nombre;
      
       if ( item == null || item[1] == "0") {          
           this.$router.push("/Upload");
           //this.$router.go(0);
       }else
       {
          this.$router.go(0);
       }

      $(".cl-selector").toggleClass("show");
    },
    showClient() {
      $(".cl-selector").toggleClass("show");
    },
  },
};
</script>
