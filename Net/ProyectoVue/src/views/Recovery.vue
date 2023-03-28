<template>
    <section class="pt-5 pb-5 mt-0 align-items-center d-flex" style="min-height: 100vh;"> 
        <div class="container-fluid">
            <div class="row  justify-content-center align-items-center d-flex-row text-center h-100">                            
                  <form @submit.prevent ='iniciarSesion' class=login>
                      <div class="logo">
                          <div></div>
                      </div>                                                              
                        <div class="form-group ">           
                          <label class="title">¿olvidaste la contraseña?</label>
                          <label class="texto">No hay necesidad de preocuparse. Estamos aquí para ayudar. Haga clic en el botón de abajo para recibir un correo electrónico de recuperación de contraseña.</label>               
                          <div class='user'></div>
                            <input class="form-control i-login "  type="text" required v-model="email" placeholder="email">
                        </div>                                                   
                        <div class="form-group mt-5">
                          <button type="submit" class="btn btn-primary btn-block form-control"> ENVIAR LINK </button>
                        </div>
                  </form>            
                </div>
            </div>
        </section>    
</template>

<script>
export default {
     name:  'RecoveryApp',     
     data() {
               return {
                    email: ''
               }
          }, 
     methods: {
                    async iniciarSesion(){
                          var payload = {
                              email: this.email
                          } ;                           
                          await this.axios.post('/user/token/', payload).then(r=>{
                            var _tokenStorage = r.data.token;
                              this.axios.defaults.headers.common['Authorization'] ='Token '+ _tokenStorage;
                              localStorage.setItem('userIdentity',  _tokenStorage);
                              localStorage.setItem('userLogin',  this.email);
                             this.GetEmpresa() ;
                              this.$router.push('/home');
                          }).catch(err => {
                              console.log(err.response);
                           });
                    },async   GetEmpresa(){
                            await this.axios.get('/core/empresas/')
                            .then(r =>{
                              console.log(r.data);
                            localStorage.setItem("empresa",r.data[0].CIF);
                          }).catch(err => {
                              console.log(err.response);
                           });
                  }
               }
     }
</script>