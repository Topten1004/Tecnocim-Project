import { createRouter, createWebHashHistory } from 'vue-router';

const router = createRouter({

     routes: [
          {
               path: '/',
               redirect: '/documentos'
          },
          {
               path: '/documentos',
               component: () => import('../views/DocumentosView.vue'),
          },
          {
               path: '/pool',
               component: () => import('../views/Pool.vue'),
          },
          {
               path: '/pool/cirbe',
               component: () => import('../views/pool/PoolCirbe.vue'),
          },
          {
               path: '/ratios',
               component: () => import('../views/Ratios.vue'),
          },
          {
               path: '/deuda',
               component: () => import('../views/Deuda.vue'),
          },
          {
               path: '/deuda/cobertura',
               component: () => import('../views/Cobertura.vue'),
          },
          {
               path: '/analitica/eva',
               component: () => import('../views/EVA.vue'),
          },
          {
               path: '/analitica/eoaf',
               component: () => import('../views/EOAF.vue'),
          },
          {
               path: '/contabilidad/balance',
               component: () => import('../views/Balance.vue'),
          },
          {
               path: '/contabilidad/masas',
               component: () => import('../views/Masas.vue'),
          },
          {
               path: '/balance',
               component: () => import('../views/Balance.vue'),
          },
          {
               path: '/analitica',
               component: () => import('../views/Analitica.vue'),
          },
          {
               path: '/gestion/empresas',
               component: () => import('../views/EmpresaCrud.vue'),
          },
          {
               path: '/gestion/entidades',
               component: () => import('../views/EntidadCrud.vue'),
          }
     ],
     history: createWebHashHistory()
});
export default router;
