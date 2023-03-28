<template>
    <HTML2Image
        :identificador="identificador"
        type="columntable"
        class="float-end fs-5 text-light"
        style="margin-bottom: -40px; padding: 10px 10px 10px 0px"
    />
    <table :id='identificador' class="table table-hover">
        <thead v-if="titulo" class="fs-6 background-alia">
            <tr>
                <th scope="col" :colspan="columnas.length" class="text-light">
                    <span v-if='icono'>
                        <AliaIcon icon="fa-diagram-project" :color="false"/>
                    </span>
                    &nbsp;&nbsp;{{ titulo }}
                </th>
            </tr>
        </thead>
        <thead class="fs-6">
            <tr>
                <th 
                    v-for="(columna, index) in columnas"
                    v-bind:key="columna.key"
                    scope="col"
                    :class="{ 'text-center': index !== 0 }"
                >
                    {{ columna.value }}
                </th>
            </tr>
        </thead>
        <tbody>
            <tr
                v-for="fila in filas"
                v-bind:key="fila"
                @click.prevent="selectable && (!('selectable' in fila) || fila.selectable) ? selectFila(fila) : null"
                :class="getFilaCLass(fila)"
                :style="{ 'cursor': selectable && (!('selectable' in fila) || fila.selectable) ? 'pointer' : 'initial' }"
            >
                <th
                    v-for="(columna, index) in columnas"
                    v-bind:key="columna.key"
                    :class="{ 
                        'text-center': (index !== 0),
                        'background-alia-gradiente': (index == (columnas.length - 1)) || ('background' in columna && columna.background),
                        'text-light': (index == (columnas.length - 1)) || ('background' in columna && columna.background),
                        'fw-normal': (index !== 0)
                    }"
                >
                  <span
                      v-if="!('type' in columna)"
                  >
                    <span v-if="fila[columna.key] && typeof fila[columna.key] === 'string'">
                      {{ fila[columna.key].at(0).toUpperCase() + fila[columna.key].slice(1, fila[columna.key].length) }}
                    </span>
                    <span v-if="typeof fila[columna.key] !== 'string'">
                      {{ fila[columna.key] }}
                    </span>
                  </span>
                  <span
                    v-if="columna.type == 'currency'"
                  >
                    {{ getCurrencyFormat(fila[columna.key]) }}
                  </span>
                  <span
                      v-if="'type' in columna && columna.type == 'format'"
                  >
                    {{ fila[columna.key] ?? 0 }} {{ columna.format }}
                  </span>
                </th>
            </tr>
        </tbody>
    </table>
</template>

<script>

export default {
    name: 'ColumnTable',
    props: {
        titulo: {
            type: String,
            default: ''
        },
        icono: {
            type: String,
            default: 'pool.png'
        },
        columnas: {
            type: Array,
            default: () => []
        },
        filas: {
            type: Array,
            default: () => []
        },
        selectable: {
            type: Boolean,
            default: false
        }
    },
    data() {
      const identificador = this.generateString();
      return {
        identificador: identificador,
        filasSelected: []
      }
    },
    methods: {
        selectFila(fila) {
          if (this.filasSelected.find(f => f == fila)) {
            this.filasSelected.splice(this.filasSelected.findIndex(f => f == fila), 1);
          } else {
            this.filasSelected.push(fila);
          }
          this.$emit('selected', this.filasSelected);
        },
        getFilaCLass(fila) {
          let filaCLass = {
            'background-alia text-light': this.filasSelected.find(f => f == fila)
          };
          if ('class' in fila && fila.class) {
            filaCLass[fila.class] = true;
          }
          return filaCLass;
        }
    }
}
</script>

<style>
</style>