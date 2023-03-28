// vue js
import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

// http
import axios from 'axios'
import VueAxios from 'vue-axios'

// bootstrap
import 'bootstrap/dist/css/bootstrap.css';

// Styling
import './css/app.css'

/* import the fontawesome core */
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

// import icons
import { 
     faCloudArrowUp,
     faDiagramProject,
     faAreaChart,
     faFile,
     faChartPie,
     faChartSimple,
     faGear,
     faArrowsRotate,
     faCircleUser,
     faRightFromBracket,
     faTableList,
     faSquareFull,
     faCaretRight,
     faCaretLeft,
     faPaperclip,
     faBuilding,
     faPlay,
     faFilePen,
     faUpRightFromSquare,
     faTriangleExclamation,
     faChevronUp,
     faChevronDown,
     faGears,
     faBuildingColumns,
     faPenToSquare,
     faChartLine,
     faUpload,
     faTableColumns,
     faArrowUpShortWide,
     faChartColumn,
     faImage
} from '@fortawesome/free-solid-svg-icons'

import{
     faCircle,
     faCircleCheck,
     faTrashCan,
} from '@fortawesome/free-regular-svg-icons'

/* add icons to the library */
library.add(
     faCloudArrowUp,
     faDiagramProject,
     faAreaChart,
     faFile,
     faChartPie,
     faChartSimple,
     faGear,
     faArrowsRotate,
     faCircleUser,
     faRightFromBracket,
     faTableList,
     faSquareFull,
     faCaretRight,
     faCaretLeft,
     faPaperclip,
     faCircle,
     faCircleCheck,
     faTrashCan,
     faBuilding,
     faPlay,
     faFilePen,
     faUpRightFromSquare,
     faTriangleExclamation,
     faChevronUp,
     faChevronDown,
     faGears,
     faBuildingColumns,
     faPenToSquare,
     faChartLine,
     faUpload,
     faChartColumn,
     faTableColumns,
     faArrowUpShortWide,
     faChartColumn,
     faImage
)

//
import AliaIcon from './components/AliaIcon.vue'
import SpinnerAlia from './components/Spinner.vue'
import GenericCRUD from "@/components/GenericCRUD.vue";
import MainTable from "@/components/MainTable.vue";
import ModalWindow from "@/components/ModalWindow.vue";
import HeaderTitle from "@/components/HeaderTitle.vue";
import HTML2Image from "@/components/HTML2Image.vue";

// Notificaciones
import Toaster from "@meforma/vue-toaster";

// import ChartRender from './components/chart-flujos-tesoreria.vue';

// axios config
const apiIp = process.env.VUE_APP_API_IP || 'localhost:8000';
axios.defaults.baseURL = `http://${apiIp}/api`;

// user token
var _token = localStorage.getItem('user-token');
if (_token) {
     axios.defaults.headers.common['Authorization'] = 'Token ' + _token;
}

// create app
var app = createApp(App);

// app.config.globalProperties.$Block = () => {
//      document.querySelector('.loader').style.display = 'block';
// }

// app.config.globalProperties.$BlockPop = () => {
//      document.querySelector('.loaderpop').style.display = 'block';
// }

// app.config.globalProperties.$UnBlock = () => {
//      document.querySelector('.loader').style.display = 'none';
// }

// app.config.globalProperties.$UnBlockPop = () => {
//      document.querySelector('.loaderpop').style.display = 'none';
// }


// TODO: Use formats and libraries
app.config.globalProperties.$FormatNumero = (n, type = null) => {
     if (n == undefined || n == 0) {
          return "--"
     }
     if(type) {
          return `${n} ${type}`;
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


app.config.globalProperties.$FormatFecha = (f, day = true) => {
     var date = new Date(f);
     if (!isNaN(date.getTime())) {
          if (day) {
               return ("0" + date.getDate()).slice(-2) + '/' + ("0" + (date.getMonth() + 1)).slice(-2) + '/' + date.getFullYear();
          } else {
               return ("0" + (date.getMonth() + 1)).slice(-2) + '/' + date.getFullYear();
          }
     }
}

app.config.globalProperties.getNumberFormat = (number, locale = 'es') => {
     if (isNaN(number)) {
          number = 0;
     }
     return new Intl.NumberFormat(locale, {
          minimumFractionDigits: 2,
          useGrouping: true
     }).format(number)
}

app.config.globalProperties.getCurrencyFormat = (number, currency = 'EUR', locale = 'es') => {
     if (isNaN(number)) {
          number = 0;
     }
     return new Intl.NumberFormat(locale, {
          style: 'currency',
          currency: currency,
          minimumFractionDigits: 2,
          useGrouping: true
     }).format(number)
}

app.config.globalProperties.getDateFormat = (date) => {
     if (!date) {
          return '';
     }
     const dateJS = new Date(date);
     if (dateJS == 'Invalid Date') {
          return date;
     }
     if (date.split('-').length < 3) {
          return dateJS.toLocaleDateString('es-ES', { year: 'numeric', month: '2-digit'});
     }
     return dateJS.toLocaleDateString('es-ES', { year: 'numeric', month: '2-digit', day: '2-digit' });
}

app.config.globalProperties.generateString = (length = 10) => {
     let result           = '';
     let characters       = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
     let charactersLength = characters.length;
     for (let i = 0; i < length; i++) {
          result += characters.charAt(Math.floor(Math.random() * charactersLength));
     }
     return result;
}

//app.config.globalProperties.$globalVariable = '' // global variable
import VueNextSelect from 'vue-next-select'
localStorage.removeItem('show-pool');
// app.component('ChartRender', ChartRender);
app.component('FontAwesomeIcon', FontAwesomeIcon)
app.component('AliaIcon', AliaIcon)
app.component('SpinnerAlia', SpinnerAlia)
app.component('GenericCRUD', GenericCRUD)
app.component('MainTable', MainTable)
app.component('ModalWindow', ModalWindow)
app.component('HeaderTitle', HeaderTitle)
app.component('vue-select', VueNextSelect)
app.component('HTML2Image', HTML2Image)
app.use(router);
app.use(VueAxios, axios);
app.use(Toaster);
app.mount('#app');

import 'bootstrap/dist/js/bootstrap.js';
