<template>
  <!-- Login -->
  <div v-if="!isLogged">
    <LoginApp />
  </div>
  <!-- Logged -->
  <div v-if="isLogged">
    <NavBar v-if='isCompany'/>
    <div
      v-if="isCompany" 
      class="container-fluid main-content">
      <RouterView></RouterView>
    </div>
    <ModalEmpresa
      v-if='!isCompany'
    />
  </div>
</template>

<script>
// import { Tooltip } from "bootstrap";
import LoginApp from './views/Login.vue';
import NavBar from './components/NavBar.vue';
import ModalEmpresa from './components/ModalEmpresa.vue';
// new Tooltip(document.body, {
//   selector: "[data-bs-toggle='tooltip']",
// });

export default {
  name: "App",
  components: {
    LoginApp,
    NavBar,
    ModalEmpresa
  },
  mounted() {
    this.axios.interceptors.response.use((response) => {
      return response;
    }, (error) => {
      // Si Unauthorized le mandamos a login
      if(error.response.status == 401) {
        localStorage.clear();
        this.$toast.error('Su sesión ha caducado');
        location.href = '/';
        return Promise.reject(error);
      } else {
        // Captura de errores genérica
        let mensajeError = '';
        if(error.response && 'data' in error.response && typeof error.response.data == 'object' && 'error' in error.response.data) {
          if(typeof error.response.data.error !== 'string') {
            mensajeError = error.response.data.error.error;
          } else {
            mensajeError = error.response.data.error;
          }
        } else {
          if (error.response.status == 400) {
            const errors = error.response.data;
            for(const key of Object.keys(errors)) {
              const listErrors = errors[key];
              for(const err of listErrors) {
                console.log(`${key}: ${err}`);
                this.$toast.error(`${key}: ${err}`, { duration: 10000, preventDuplicates: true });
              }
            }
          } else {
            mensajeError = 'Ocurrió un error';
          }
        }
        if(mensajeError) {
          this.$toast.error(mensajeError, { duration: 10000, preventDuplicates: true });
        }
      }
      return Promise.reject(error);
    });
    // Token
    const token = localStorage.getItem('token');
    if (token) {
      this.axios.defaults.headers.common['Authorization'] = `Token ${token}`;
    }
  },
  data() {
    // Muestreo de barra de navegación
    const token = localStorage.getItem('token') ? true : false;
    const empresa = localStorage.getItem('company') ? true : false;
    return {
      isLogged: token,
      isCompany: empresa,
      conceptosPendientes: []
    }
  }
};
</script>

<style>
</style>