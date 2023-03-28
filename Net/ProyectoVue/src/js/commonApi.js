function fn1(){
     console.log("Método global uno")
   }
   function fn2(){
     console.log("Método global dos")
   }
   function fn3(){
     console.log("Método global tres")
   }
   function fn4(){
     console.log("Método global cuatro")
   }
   
   // Combina los cuatro métodos públicos globales en un objeto y exponlo
   export default {
     fn1,
     fn2,
     fn3,
     fn4,
   }