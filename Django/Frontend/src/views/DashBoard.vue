<template>  
  <div class="wrapper d-flex align-items-stretch">
    <Sidebar></Sidebar>
    <div id="content">
      <div class="container-fluid">
        <Navbar></Navbar>       
      </div>
    </div>
  </div>
</template>

<script>
import Sidebar from "../components/sidebar.vue";
import Navbar from "../components/NavBar.vue";
export default {
  name: "DashBoard",
  components: { Sidebar, Navbar},
};
</script>
