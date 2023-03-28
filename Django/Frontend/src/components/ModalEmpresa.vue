<template>
    <ModalWindow
        titulo="Selecciona un cliente"
        :visible="true"
        max-width="1000px"
        @closed="closed()"
    >
    <template #icono>
      <AliaIcon icon='fa-building' :color="false"  class="me-2"/>
    </template>
    <MainTable
      v-if="empresas"
      :columnas="mainTable.columnas"
      :filas="empresas"
      :actions="mainTable.actions"
      :width="1000"
    />
  </ModalWindow>
</template>

<script>
import ModalWindow from './ModalWindow.vue';
import MainTable from './MainTable.vue';

export default {
  name: 'ModalEmpresa',
  components: {
    ModalWindow,
    MainTable,
  },
  props: {
    visible: {
        type: Boolean,
        default: false
    }
  },
  mounted() {
    setTimeout(() => this.getEmpresas());
  },
  data() {
    return {
        empresas : [],
        //
        mainTable: {
            columnas: [
                {
                    key: 'CIF'
                },
                {
                    key: 'nombre'
                },
                {
                    key: 'email',
                    value: 'Correo electrónico'
                }
                ],
                actions: [
                    {
                        key: 'seleccionar',
                        icon: 'fa-play',
                        method: empresa => this.selectEmpresa(empresa)
                    }
                ]
            }
        }
  },
  methods: {
    async getEmpresas() {
        await this.axios.get("/core/empresas/").then((r) => {
            this.empresas = r.data;
        });
    },
    selectEmpresa(empresa) {
      localStorage.setItem('company', JSON.stringify(empresa));
      location.reload();
    },
    closed() {
        this.$emit('closed', true);
    }
  }
}
</script>