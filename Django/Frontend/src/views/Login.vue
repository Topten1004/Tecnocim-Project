<template>
  <section class="pt-5 pb-5 mt-0 align-items-center d-flex" style="min-height: 100vh;"> 
    <div class="container">
      <div class="row  justify-content-center align-items-center d-flex-row text-center h-100">                            
          <form @submit.prevent ='iniciarSesion' class=login>
              <div class="logo mb-5">
                  <div></div>
              </div>                      
                <div class="form-group ">                          
                  <div class='user'></div>
                    <input class="form-control i-login" placeholder="Correo electrónico" type="text" required v-model="email" >
                </div>                           
                <div class="form-group mt-4">       
                  <div class='pass'></div>
                  <input class="form-control i-login " placeholder="Contraseña" type="password" required v-model="clave">
                </div>                    
                <div class="form-group mt-5">
                  <button type="submit" class="btn btn-primary btn-block form-control text-uppercase"> Iniciar sesión </button>
                </div>
                <div class="dvlink mt-3" v-if="false">
                  <a  href="/recovery">¿Olvidaste tu contraseña?</a>
                </div>
          </form>            
        </div>
    </div>
  </section>    
</template>

<script>
export default {
     name:  'LoginApp',     
     data() {
        return {
            error:'',
            email: '',
            clave:'' 
        }
    }, mounted(){
      const token = localStorage.getItem('token');
      if(token) {
        this.$router.push('/');
      }
    },
    methods: {
      async iniciarSesion(){
        let payload = {
            email: this.email,
            password: this.clave                              
        } ;                           
        return await this.axios.post('/user/token/', payload).then(r=>{
          var _tokenStorage = r.data.token;
          this.axios.defaults.headers.common['Authorization'] ='Token '+ _tokenStorage;
          localStorage.setItem('token',  _tokenStorage);
          localStorage.setItem('email',  this.email);
          location.href = '/';
        }).catch(err => {
          console.log(err.response);
          this.error ="Error iniciando sesion, usuario o contraseña incorrecta";
        });
        }
      }
    }
</script>