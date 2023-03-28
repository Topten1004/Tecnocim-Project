<template>
  <MainTable
    :titulo="titulo"
    :icono="icono"
    :columnas="columnas"
    :filas="filas"
    :actions="actions"
    :buttons="buttons"
    :offset-height="150"
    :exportar="false"
  />
  <ModalWindow
    v-if="objeto"
    :titulo="getModalTitulo()"
    :visible="Object.keys(objeto).length ? true : false"
    @closed="closed()"
  >
    <form @submit.prevent="saveData()">
      <span
          v-for="property of Object.keys(definicion)"
          v-bind:key="property"
      >
        <CustomInput
            v-if="definicion[property] !== null && definicion[property].length == 0"
            :label="property"
            :capitalize="true"
            v-model="objeto[property]"
            required
        />
      </span>
      <button type="submit" class="btn btn-secondary float-end">Guardar</button>
    </form>
  </ModalWindow>
  <ConfirmModal
      v-if="objetoDelete !== null"
      :visible="objetoDelete !== null"
      class="btn btn-danger"
      text="Eliminar"
      @clickButton="deleteData()"
      @closed="closed()"
  />
</template>

<script>
import CustomInput from "@/components/CustomInput.vue";
import ConfirmModal from "@/components/ConfirmModal.vue";

export default {
  name: 'GenericCRUD',
  components: {
    CustomInput,
    ConfirmModal
  },
  props: {
    ruta: {
      type: String,
      default: '',
      required: true
    },
    titulo: {
      type: String,
      default: ''
    },
    icono: {
      type: String,
      default: ''
    },
    columnas: {
      type: Array,
      default: () => []
    },
    definicion: {
      type: Object,
      default: () => {},
      required: true
    },
    crud: {
      type: Array,
      default: () => []
    },
    filter: {
      type: Object,
      default: () => {}
    }
  },
  mounted() {
    this.getActions();
    this.getData();
  },
  data() {
    return {
      create: false,
      objeto: null,
      objetoDelete: null,
      filas: [],
      actions: [],
      buttons: []
    };
  },
  methods: {
    async getData() {
      this.axios.get(this.ruta)
          .then(r => {
            if (this.filter) {
              this.filas = r.data.filter(d => this.filter(d));
            } else {
              this.filas = r.data;
            }
          });
    },
    createData() {
      this.objeto = Object.assign({}, this.definicion);
    },
    saveData() {
      const formData = new FormData();
      for (const item in this.objeto) {
        formData.append(item, this.objeto[item]);
      }
      if (this.objeto.id === null) {
        this.axios.post(this.ruta, formData).then()
            .then(() => {
              this.$toast.success(`Creación correcta`);
              this.closed();
              this.getData();
            });
      } else {
        formData.delete('id');
        for(let key of Object.keys(this.objeto)) {
          if (!Object.keys(this.definicion).includes(key)) {
            formData.delete(key);
          }
        }
        this.axios.put(this.ruta + this.objeto.id + '/', formData)
            .then(() => {
              this.$toast.success(`Edición correcta`);
              this.closed();
              this.getData();
            });
      }
    },
    deleteData() {
      return this.axios
          .delete(this.ruta + this.objetoDelete.id)
          .then(() => {
            this.$toast.success(`Eliminación correcta`);
            this.docToDelete = null;
            this.closed();
            setTimeout(() => this.getData(), 500);
          });
    },
    getModalTitulo() {
      return this.objeto.id
        ? 'Editar'
        : 'Crear'
      ;
    },
    getActions() {
      const actionsDefinition = {
        edit: {
          key: 'edit',
          icon: 'fa-pen-to-square',
          method: (fila) => this.objeto = fila
        },
        delete: {
          key: 'delete',
          icon: 'fa-regular fa-trash-can',
          method: (fila) => this.objetoDelete = fila
        }
      }
      let actions = [];
      for(let action of this.crud) {
        if (!['create', 'edit', 'delete'].includes(action)) {
          continue;
        }
        if (action === 'create') {
          this.buttons.push({
            text: 'Añadir',
            click: () => this.createData()
          })
        } else {
          actions.push(actionsDefinition[action]);
        }
      }
      this.actions = actions;
    },
    closed() {
      this.objeto = null;
      this.objetoDelete = null;
    }
  }
}
</script>
<style>

</style>