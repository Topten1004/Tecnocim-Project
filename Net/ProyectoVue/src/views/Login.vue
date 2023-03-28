<template>
    <section class="pt-5 pb-5 mt-0 align-items-center d-flex" style="min-height: 100vh;"> 
        <div class="container-fluid">
            <div class="row  justify-content-center align-items-center d-flex-row text-center h-100">                            
                  <form @submit.prevent ='iniciarSesion' class=login>
                      <div class="logo">
                          <div></div>
                      </div>                      
                        <div class="form-group ">                          
                          <div class='user'></div>
                            <input class="form-control i-login" placeholder="email" type="text" required v-model="email" >
                        </div>                           
                        <div class="form-group mt-4">       
                          <div class='pass'></div>
                          <input class="form-control i-login " placeholder="password" type="password" required v-model="clave">
                        </div>                    
                        <div class="form-group mt-4">                          
                        {{error}}
                        </div>   
                        <div class="form-group mt-5">
                          <button type="submit" class="btn btn-primary btn-block form-control"> LOGIN </button>
                        </div>
                        <div class="dvlink mt-3">
                          <a  href="/recovery">Forgot password?</a>
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
            localStorage.clear();
            
          },
     methods: {
                    async iniciarSesion(){
                      this.$Block();
                          var payload = {
                              email: this.email,
                              password: this.clave                              
                          } ;                           
                          await this.axios.post('/user/token/', payload).then(r=>{
                            var _tokenStorage = r.data.token;
                              this.axios.defaults.headers.common['Authorization'] ='Token '+ _tokenStorage;
                              localStorage.setItem('userIdentity',  _tokenStorage);
                              localStorage.setItem('userLogin',  this.email);                             
                              this.$router.push('/Dashboard');
                              this.$UnBlock();
                          }).catch(err => {
                              console.log(err.response);
                              this.error ="Error iniciando sesion, usuario o contraseña incorrecta";
                              this.$UnBlock();
                           });
                    }
               }
     }
</script>