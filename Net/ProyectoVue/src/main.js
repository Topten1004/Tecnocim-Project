import { createApp } from 'vue'
import App from './App.vue'
import 'bootstrap/dist/css/bootstrap.css'
import axios from 'axios'
import VueAxios from 'vue-axios'
import router from './router'
import './css/app.css'
import 'bootstrap/dist/js/bootstrap.bundle.js'


import ChartRender from './components/chart-flujos-tesoreria.vue';

axios.defaults.baseURL = 'http://38.242.155.155:8000/api'


var _token = localStorage.getItem('userIdentity');
if (_token) {
     axios.defaults.headers.common['Authorization'] = 'Token ' + _token;
}
var app = createApp(App);

app.config.globalProperties.$Block = () => {
     document.querySelector('.loader').style.display = 'block';
}

app.config.globalProperties.$BlockPop = () => {
     document.querySelector('.loaderpop').style.display = 'block';
}

app.config.globalProperties.$UnBlock = () => {
     document.querySelector('.loader').style.display = 'none';
}

app.config.globalProperties.$UnBlockPop = () => {
     document.querySelector('.loaderpop').style.display = 'none';
}

app.config.globalProperties.$FormatNumero = (n) => {
     if (n == undefined || n == 0) {
          return "--"
     }
     return new Intl.NumberFormat('de-DE', { style: 'currency', currency: 'EUR' }).format(n);
}

app.config.globalProperties.$ConverToInt = (n) => {
     if(n == undefined || n == 0){
          return 0;
     }
     return n;
} 


app.config.globalProperties.$FormatNumeroNoSign = (n) => {
     if (n == undefined || n == 0) {
          return "--"
     }

     return (n).toLocaleString('de-DE', {
          maximumFractionDigits: 2
     });
}


app.config.globalProperties.$FormatFecha = (f) => {
     var date = new Date(f);
     if (!isNaN(date.getTime())) {
          return ("0" + date.getDate()).slice(-2) + '/' + ("0" + (date.getMonth() + 1)).slice(-2) + '/' + date.getFullYear();
     }
}




//app.config.globalProperties.$globalVariable = '' // global variable


app.component('ChartRender', ChartRender);
app.use(router);
app.use(VueAxios, axios);
app.mount('#app');


