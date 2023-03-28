import { createWebHistory, createRouter } from 'vue-router'
import DashBoard from '../views/DashBoard.vue'
import Login from '../views/Login.vue'
import Home from '../views/Home.vue'
import Upload from '../views/Upload.vue'
import Recovery from '../views/Recovery.vue'
import PoolDue from '../views/Pool.vue'
import RatiosAccount  from '../views/Ratios.vue'
import Logout  from '../views/Logout.vue'
import DeudaService  from '../views/Deuda.vue'
import BalancePyG  from '../views/Balance.vue'


const routes = [
     {
          path: '/',
          Name: 'Login',
          component: Login
     },
     {
          path: '/logout',
          Name: 'Logout',
          component: Logout
     },
     {
          path: '/Login',
          Name: 'Login',
          component: Login
     },
     {
          path: '/DashBoard',
          Name: 'DashBoard',
          component: DashBoard
     },
     {
          path: '/Home',
          Name: 'Home',
          component: Home
     },
     {
          path: '/Upload',
          Name: 'Upload',
          component: Upload
     },
     {
          path: '/Recovery',
          Name: 'Recovery',
          component: Recovery
     }
     ,
     {
          path: '/Pool',
          Name: 'PoolDue',
          component: PoolDue
     },
     {
          path: '/Ratios',
          Name: 'RatiosAccount',
          component: RatiosAccount
     },
     {
          path: '/Deuda',
          Name: 'DeudaService',
          component: DeudaService
     },
     {
          path: '/Balance',
          Name: 'BalancePyG',
          component: BalancePyG
     }
];

const router = createRouter({
     history: createWebHistory(),
     routes
});

export default router;