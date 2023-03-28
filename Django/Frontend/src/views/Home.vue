<template>
     <div>Test</div>     
     <span> {{monedas}} </span>
</template>

<script>
export default{
     name:  'HomeApp',    
     async mounted()
     {
               await this.axios
               .get('/core/empresas/')
               .then(response => {
                    this.monedas = response.data
               }).catch(err => {
                              console.log(err.response);
               });
     }, data(){
          return {
               monedas: []
          }
     }
}
</script>