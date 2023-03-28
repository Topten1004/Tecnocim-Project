<template>
  <header class="mb-3 border-bottom main-content sticky-top">
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <div class="container-fluid">
        <a class="navbar-brand" href="#">
          <img src="../img/logo.png" :color='false' />
        </a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDropdown"
          aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNavDropdown">
          <ul class="col navbar-nav">
            <li class="nav-item text-center">
              <router-link to="/documentos" class="nav-link" aria-current="page">
                <AliaIcon icon="fa-cloud-arrow-up" :color='false' /> Documentos
              </router-link>
            </li>
            <div class="dropdown text-center pt-2">
              <a href="#" class="d-block link-dark text-decoration-none dropdown-toggle" id="dropdownPool"
                 data-bs-toggle="dropdown" aria-expanded="false">
                <AliaIcon icon="fa-diagram-project" :color='false' /> Pool
              </a>
              <ul class="dropdown-menu text-small m-3" aria-labelledby="dropdownPool">
                <li class="nav-item">
                  <router-link to="/pool" class="nav-link">
                    <AliaIcon icon="fa-diagram-project" :color='false' /> Pool bancario
                  </router-link>
                </li>
                <li class="nav-item">
                  <router-link to="/pool/cirbe" class="nav-link">
                    <AliaIcon icon="fa-chart-pie" :color='false' /> Cirbe
                  </router-link>
                </li>
              </ul>
            </div>
            <li class="nav-item">
              <router-link to="/ratios" class="nav-link">
                <AliaIcon icon="fa-area-chart" :color='false' /> Ratios
              </router-link>
            </li>
            <div class="dropdown text-center pt-2 ms-2">
              <a href="#" class="d-block link-dark text-decoration-none dropdown-toggle" id="dropdownDeuda"
                 data-bs-toggle="dropdown" aria-expanded="false">
                <AliaIcon icon="fa-area-chart" :color='false' /> Deuda
              </a>
              <ul class="dropdown-menu text-small m-3" aria-labelledby="dropdownDeuda">
                <li class="nav-item">
                  <router-link to="/deuda/cobertura" class="nav-link">
                    <AliaIcon icon="fa-area-chart" :color='false' /> Cobertura
                  </router-link>
                </li>
                <li class="nav-item">
                  <router-link to="/deuda" class="nav-link">
                    <AliaIcon icon="fa-chart-pie" :color='false' /> Análisis
                  </router-link>
                </li>
              </ul>
            </div>
            <div class="dropdown text-center pt-2 ms-2">
              <a href="#" class="d-block link-dark text-decoration-none dropdown-toggle" id="dropdownDeuda"
                 data-bs-toggle="dropdown" aria-expanded="false">
                <AliaIcon icon="fa-diagram-project" :color='false' /> Contabilidad
              </a>
              <ul class="dropdown-menu text-small m-3" aria-labelledby="dropdownDeuda">
                 <li class="nav-item">
                   <router-link :key="contabilidad-balance" to="/contabilidad/balance" class="nav-link">
                     <AliaIcon icon="fa-area-chart" :color='false' /> Balance
                  </router-link>
                </li>
                <li class="nav-item">
                  <router-link :key="contabilidad-masas" to="/contabilidad/masas" class="nav-link">
                    <AliaIcon icon="fa-chart-pie" :color='false' /> Masas
                  </router-link>
                </li>
              </ul>
            </div>
<!--            <li class="nav-item text-center">-->
<!--              <router-link to="/balance" class="nav-link">-->
<!--                <AliaIcon icon="fa-chart-pie" :color='false' /> Balance-->
<!--              </router-link>-->
<!--            </li>-->
            <div class="dropdown text-center pt-2 mx-2">
              <a href="#" class="d-block link-dark text-decoration-none dropdown-toggle" id="dropdownAnalitica"
                 data-bs-toggle="dropdown" aria-expanded="false">
                <AliaIcon icon="fa-chart-simple" :color='false' /> Analítica
              </a>
              <ul class="dropdown-menu text-small m-3" aria-labelledby="dropdownAnalitica">
                <li class="nav-item">
                  <router-link to="/analitica" class="nav-link">
                    <AliaIcon icon="fa-chart-simple" :color='false' /> Analítica
                  </router-link>
                </li>
                <li class="nav-item">
                  <router-link to="/analitica/eva" class="nav-link">
                    <AliaIcon icon="fa-chart-column" :color='false' /> EVA
                  </router-link>
                </li>
                <li class="nav-item">
                  <router-link to="/analitica/EOAF" class="nav-link">
                    <AliaIcon icon="fa-table-columns" :color='false' /> EOAF
                  </router-link>
                </li>
              </ul>
            </div>
<!--            <li class="nav-item text-center">-->
<!--              <router-link to="/documentos" class="nav-link disabled">-->
<!--                <AliaIcon icon="fa-file" :color='false' /> Informes-->
<!--              </router-link>-->
<!--            </li>-->
<!--            <li class="nav-item text-center">-->
<!--              <router-link to="/documentos" class="nav-link disabled">-->
<!--                <AliaIcon icon="fa-chart-simple" :color='false' /> Ajustes-->
<!--              </router-link>-->
<!--            </li>-->
            <div class="dropdown text-center pt-2">
              <a href="#" class="d-block link-dark text-decoration-none dropdown-toggle" id="dropdownPool"
                 data-bs-toggle="dropdown" aria-expanded="false">
                <AliaIcon icon="fa-gear" :color='false' /> Gestión
              </a>
              <ul class="dropdown-menu text-small m-3" aria-labelledby="dropdownPool">
                <li class="nav-item">
                  <router-link to="/gestion/empresas" class="nav-link">
                    <AliaIcon icon="fa-building" :color='false' /> Empresas
                  </router-link>
                </li>
                <li class="nav-item">
                  <router-link to="/gestion/entidades" class="nav-link">
                    <AliaIcon icon="fa-building-columns" :color='false' /> Entidades
                  </router-link>
                </li>
              </ul>
            </div>
          </ul>
          <ul class="navbar-nav text-end me-3">
            <li class="nav-item" @click="showModalEmpresa()" style="cursor: pointer;">
              <button class="btn btn-primary nav-link text-center fw-bold text-light">
                <AliaIcon icon="fa-arrows-rotate" :color='false' /> {{ empresa }}
              </button>
            </li>
          </ul>
          <div class="dropdown text-end ms-1">
            <a href="#" class="d-block link-dark text-decoration-none dropdown-toggle text-center" id="dropdownUser"
              data-bs-toggle="dropdown" aria-expanded="false">
              <AliaIcon icon="fa-circle-user" :color='false' /> {{ getUser() }}
            </a>
            <ul class="dropdown-menu text-small" aria-labelledby="dropdownUser">
              <li class="nav-item" @click="logOut()">
                <a class="dropdown-item" href="#">
                  <AliaIcon icon="fa-right-from-bracket" :color='false' /> Cerrar sesión
                </a>
              </li>
              <!-- <li><hr class="dropdown-divider"></li> -->
            </ul>
          </div>
        </div>
      </div>
    </nav>
  </header>
  <ModalEmpresa 
    v-if="showModal"
    @closed="closedModal()"
  />
</template>

<script>
import ModalEmpresa from '../components/ModalEmpresa.vue';

export default {
  name: "NavBar",
  components: {
    ModalEmpresa
  },
  data() {
    return {
      empresa: this.getEmpresa(),
      showModal : false
    }
  },
  methods: {
    getEmpresa() {
      return JSON.parse(localStorage.getItem('company')).nombre;
    },
    getUser() {
      return localStorage.getItem('email');
    },
    showModalEmpresa() {
      this.showModal = true;
    },
    closedModal() {
      this.showModal = false;
    },
    logOut() {
      localStorage.clear();
      location.href = '/';
    }
  }
}
</script>

<style>

</style>
