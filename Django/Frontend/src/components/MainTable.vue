<template>
    <div class="row" v-if="icono || titulo">
        <div class="col-10 mb-3">
          <HeaderTitle
            :icono="icono"
            :titulo="titulo"
          />
        </div>
      <div class="col-2 mt-2 text-end">
        <button
            v-for="button in buttons"
            v-bind:key="button.key"
            :type="button.type ?? button"
            @click="button.click"
            :class="button.class ?? 'ms-1 btn btn-primary'"
        >
              <span>
                {{ button.text }}
              </span>
        </button>
        <AliaIcon v-if="opened && !hidden" icon="fa-arrow-up-short-wide" style="cursor: pointer" @click.prevent="hideFilas()" class="me-2"/>
        <HTML2Image
            v-if="exportar && rows.length && !noData"
            :identificador="identificador"
            class="ms-1 fs-5"
        />
      </div>
    </div>
    <div class="scrollable" :style="{'max-height': maxHeight}">
        <table :id='identificador' class="table table-hover table-bordered">
<!--            <thead v-if="titulo" class="fs-6 background-alia">-->
<!--                <tr>-->
<!--                    <th scope="col" :colspan="columnas.length" class="text-light">-->
<!--                        <span v-if='icono'>-->
<!--                            <img src='../img/pool.png' />-->
<!--                        </span>-->
<!--                        &nbsp;&nbsp;{{ titulo }}-->
<!--                    </th>-->
<!--                </tr>-->
<!--            </thead>-->
            <thead v-if='header' class="fs-6 sticky-top">
                <tr class="table-light table-alia text-light">
                    <th v-for="(columna, index) in columnas" v-bind:key="columna.key" scope="col"
                        :class="{ 'text-center': index !== 0 || ('center' in columna && columna.center) }">
                        <span class="text-capitalize">{{ 'value' in columna ? columna.value : columna.key }}</span>
                    </th>
                    <th v-if="actions || extraActions" scope="col">
                        &nbsp;
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr id='filters' v-if="filas.length > 0 && columnas.filter(c => 'filter' in c).length > 0">
                  <td
                      v-for="(columna) in columnas"
                      v-bind:key="columna.key"
                      :style="{ width: ('width' in columna && columna.width) ? columna.width : 'initial'}"
                  >
                    <span v-if="'filter' in columna">
                      <span v-if="columna.filter == 'select'">
                        <CustomSelect
                            :options="getSelectOptions(columna)"
                            :multiple="'multiple' in columna && columna.multiple"
                            :settings="{multiple: true}"
                            @select="resetRows()"
                            v-model="filters[columna.key]"
                        />
                      </span>
                      <span v-if="columna.filter == 'number'">
                        <div>
<!--                          <span-->
<!--                              class="float-start mt-2"-->
<!--                              style="width: 10%"-->
<!--                          >-->
<!--                            >=-->
<!--                          </span>-->
                          <CustomInput
                              class="float-end"
                              style="width: 100%"
                              type="number"
                              @change="resetRows()"
                              @keyup="resetRows()"
                              v-model="filters[columna.key].min"
                          />
                        </div>
                        <div>
                           <CustomInput
                               class="float-start"
                               style="width: 100%"
                               type="number"
                               @change="resetRows()"
                               @keyup="resetRows()"
                               v-model="filters[columna.key].max"
                           />
<!--                            <span-->
<!--                                class="float-end mt-2"-->
<!--                                style="width: 10%"-->
<!--                            >-->
<!--                            &lt;=-->
<!--                          </span>-->
                        </div>
                      </span>
                      <span v-if="columna.filter == 'currency'">
                        <div>
<!--                          <span-->
<!--                              class="float-start mt-2"-->
<!--                              style="width: 10%"-->
<!--                          >-->
<!--                            >=-->
<!--                          </span>-->
                          <CurrencyInput
                              class="float-end"
                              style="width: 100%"
                              v-model.number='filters[columna.key].min'
                              :options="{
                              currency: 'EUR'
                            }"
                            @change="resetRows()"
                            @keyup="resetRows()"
                          />
                        </div>
                        <div>
                           <CurrencyInput
                               class="float-start"
                               style="width: 100%"
                               v-model.number='filters[columna.key].max'
                               :options="{
                              currency: 'EUR'
                            }"
                             @change="resetRows()"
                             @keyup="resetRows()"
                           />
<!--                            <span-->
<!--                                class="float-end mt-2"-->
<!--                                style="width: 10%"-->
<!--                            >-->
<!--                            &lt;=-->
<!--                          </span>-->
                        </div>
                      </span>
                      <span v-if="columna.filter == 'date'">
                        <CustomInput
                          type="date"
                          @change="resetRows()"
                          v-model="filters[columna.key].min"
                        />
                        <CustomInput
                            type="date"
                            @change="resetRows()"
                            v-model="filters[columna.key].max"
                        />
                      </span>
                    </span>
                  </td>
                </tr>
                <tr v-if="!rows.length">
                  <td
                      :colspan="columnas.length + (actions || extraActions ? 1: 0)"
                      class="text-center"
                  >
                    <span
                        v-if="noData"
                        class="fw-bold"
                    >
                       Sin datos
                    </span>
                    <span
                        v-if="!noData"
                    >
                       <SpinnerAlia />
                    </span>
                  </td>
                </tr>
                <tr
                    v-for="fila in rows"
                    v-bind:key="fila"
                    v-bind:class="fila.class"
                    :style="{ cursor: 'children' in fila ? 'pointer' : 'initial' }"
                    :data-parent="'parent' in fila ? fila.parent : false"
                    :data-children="'children' in fila ? fila.children : false"
                    @click.prevent="'children' in fila ? toggleChildren(fila.children): {}"
                >
                    <td
                        v-for="(columna, index) in columnas"
                        v-bind:key="columna.key"
                        :class="getTdClass(index, columna, fila)"
                        @click.prevent="'method' in columna ? columna.method(fila) : null"
                        :style="{ width: ('width' in columna && columna.width) ? columna.width : 'initial'}"
                    >
                      <span
                          :class="columna.class"
                      >
                        <span
                            v-if="!('type' in columna) || !columna.type"
                        >
                          <span v-if="fila[columna.key] && typeof fila[columna.key] === 'string'">
                            {{ fila[columna.key].at(0).toUpperCase() + fila[columna.key].slice(1, fila[columna.key].length) }}
                          </span>
                          <span v-if="typeof fila[columna.key] !== 'string'">
                            {{ fila[columna.key] }}
                          </span>
                          <span v-if="index == 0 && 'children' in fila && fila.children !== null">
                            <span
                                :id="`${fila.children}-open`"
                                style="display: none"
                            >
                              <AliaIcon icon="fa-chevron-up" class="float-end" />
                            </span>
                            <span
                                :id="`${fila.children}-close`"
                            >
                              <AliaIcon icon="fa-chevron-down" class="float-end" />
                            </span>
                          </span>
                        </span>
                        <span
                          v-if="fila[columna.key] !== 'hidden'"
                          :class="{
                            'text-success' : 'colored' in columna && columna.colored && (!('colored' in fila) || ('colored' in fila && fila.colored)) && ((fila[columna.key] ?? 0) > 0),
                            'text-danger' : 'colored' in columna && columna.colored && (!('colored' in fila) || ('colored' in fila && fila.colored)) && ((fila[columna.key] ?? 0) < 0),
                          }"
                        >
                          <span v-if="noReplaces.includes(fila[columna.key])">
                            {{ fila[columna.key] }}
                          </span>
                          <span v-else-if="zeroValue !== null && !fila[columna.key]">
                            {{ zeroValue }}
                          </span>
                          <span v-else-if="infinityValue !== null && (fila[columna.key] > 1000000000 || fila[columna.key] === infinityCondition )">
                            {{ infinityValue }}
                          </span>
                          <span v-else>
                            <span
                                v-if="'type' in columna && columna.type == 'currency'"
                            >
                              {{ getCurrencyFormat(fila[columna.key], columna.currency ?? 'EUR') }}
                            </span>
                            <span
                                v-if="'type' in columna && columna.type == 'date'"
                            >
                              {{ getDateFormat(fila[columna.key]) }}
                            </span>
                            <span
                                v-if="'type' in columna && columna.type == 'format'"
                            >
                              {{ getNumberFormat(fila[columna.key] ?? 0) }} {{ columna.format }}
                            </span>
                            <span
                                v-if="'type' in columna && columna.type == 'dynamic'"
                            >
                              <span v-if="fila[columna.property] == '€'">
                                {{ getCurrencyFormat(fila[columna.key], columna.currency) }}
                              </span>
                              <span v-if="fila[columna.property] == 'date'">
                                {{ getDateFormat(fila[columna.key]) }}
                              </span>
                              <span v-if="!['€', 'date'].includes(fila[columna.property])">
                                {{ getNumberFormat(fila[columna.key] ?? 0) }} {{ fila[columna.property] ?? (fila[columna.property] === null ? '%' : '') }}
                              </span>
                            </span>
                          </span>
                        </span>
                      </span>
                    </td>
                    <td v-if="actions || extraActions" class="text-end">
                        <slot name="extra-actions" :fila="fila"></slot>
                        <a v-for="action in actions" v-bind:key="action.key" href="#" @click="action.method(fila)" class="ms-1">
                            <AliaIcon :icon="action.icon" />
                        </a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import CustomSelect from "@/components/CustomSelect.vue";
import CurrencyInput from "@/components/CurrencyInput.vue";
import CustomInput from "@/components/CustomInput.vue";

const $ = require("jquery");

export default {
    name: 'MainTable',
    components: {CustomInput, CurrencyInput, CustomSelect},
    props: {
        titulo: {
            type: String,
            default: ''
        },
        icono: {
            type: String,
            default: ''
        },
        header: {
            type: Boolean,
            default: true
        },
        columnas: {
            type: Array,
            default: () => []
        },
        filas: {
            type: Array,
            default: () => []
        },
        actions: {
            type: Array,
            value: () => []
        },
        extraActions: {
            type: Boolean,
            default: false
        },
        offsetHeight: {
            type: [Number, Boolean],
            default: false
        },
        buttons: {
          type: Array,
          default: () => []
        },
        exportar: {
          type: Boolean,
          default: true
        },
        opened: {
          type: Boolean,
          default: false
        },
        hiddenIds: {
          type: Array,
          default: () => []
        },
        zeroValue: {
          type: String,
          default: null
        },
        infinityValue : {
          type: String,
          default: null
        },
        infinityCondition : {
          type: String,
          default: ''
        },
        footer: {
          type: Boolean,
          default: false
        },
        noReplaces: {
          type: Array,
          default: () => []
        }
    },
    mounted() {
      if (this.offsetHeight) {
        this.setMaxHeight();
        window.addEventListener('resize', this.setMaxHeight);
      }
      setTimeout(() => {
        if (!this.filas.length) {
          this.noData = true;
        }
      },
      5000);
      if (!this.opened) {
          $('tr[data-parent]:not([data-parent="false"]):visible').hide();
      } else {
        if (this.hiddenIds.length > 0) {
          this.hiddenIds.forEach(hiddenId => $(`tr[data-parent='${hiddenId}']`).hide());
        }
        this.setHidden();
      }
      if (this.footer && this.rows.length > 0) {
        this.rows.push(this.getFooterSum());
      }
    },
    watch: {
      rows() {
        if (this.footer) {
          this.rows.push(this.getFooterSum());
        }
      },
      filas() {
        this.rows = JSON.parse(JSON.stringify(this.filas));
        this.noData = this.rows.length === 0;
      }
    },
    data() {
        const identificador = this.generateString();
        return {
            identificador: identificador,
            maxHeight: 999999999,
            noData: false,
            hidden: true,
            rows: JSON.parse(JSON.stringify(this.filas)),
            filters: this.getFilters(),
            selectOptions: []
        }
    },
    methods: {
        toggleChildren(children, hide = false) {
          if(!children || children === 'false') {
            return;
          }
          const selector = `tr[data-parent='${children}']`;
          if ($(selector).is(':visible') || hide) {
            $(selector).each((index, element) => {
              if ($(element).has('data-children')) {
                this.toggleChildren($(element).attr('data-children'), true)
              }
              $(element).hide();
            });
            $(`#${children}-open`).hide();
            $(`#${children}-close`).show();
          } else {
            $(selector).show();
            $(`#${children}-open`).show();
            $(`#${children}-close`).hide();
          }
          this.setHidden();
        },
        setMaxHeight() {
            this.maxHeight = (window.innerHeight - this.offsetHeight) + 'px';
        },
        getTdClass(index, columna, fila) {
          let tdClass = {
            'text-start': index === 0,
            'text-center': ((index !== 0) && !columna.class) || ('center' in columna && columna.center),
            'text-decoration-underline': 'method' in columna,
            'pointer': 'method' in columna
          }
          if (index === 0 && 'tdClass' in fila) {
            tdClass[fila.tdClass] = true;
          }
          if (index !== 0) {
            if ('tdClass' in columna) {
              tdClass[columna.tdClass] = true;
            }
          }
          return tdClass;
        },
        hideFilas() {
          $('#' + this.identificador + ' tr[data-parent]:not([data-parent="false"]):visible').hide();
          this.setHidden();
        },
        setHidden() {
          this.hidden =  $('#' + this.identificador + ' tr[data-parent]:not([data-parent="false"]):visible').length === 0;
        },
        getColumnValues(columna) {
          return [...new Set(this.filas.map(d => {return d[columna.key];}))]; // filas o rows según se requiera
        },
        getSelectOptions(columna) {
          return this.getColumnValues(columna).map(v => {return {id:v , text: (v.at(0).toUpperCase() + v.slice(1, v.length))}});
        },
        resetRows() {
          let values = [];
          let min = 0;
          let max = 0;

          this.rows = JSON.parse(JSON.stringify(this.filas));
          for(const columna of this.columnas) {
            for(const filterKey of Object.keys(this.filters)) {
              min = 0;
              max = 0;
              if (filterKey == columna.key) {
                switch (columna.filter) {
                  case 'select':
                    values = this.filters[columna.key];
                    if (values.length > 0) {
                      this.rows = this.rows.filter(r => values.includes(r[columna.key]));
                    }
                    break;
                  case 'date':
                    min = this.filters[columna.key].min === '' ? '1900-01-01' : this.filters[columna.key].min;
                    max = this.filters[columna.key].max === '' ? '3000-01-01' : this.filters[columna.key].max;
                    this.rows = this.rows.filter(r => r[columna.key] >= min && r[columna.key] <= max);
                    break;
                  case 'number':
                  case 'currency':
                    min = this.filters[columna.key].min ?? -999999999999;
                    max = this.filters[columna.key].max ?? 999999999999;
                    if (!(min == 0 && max == 0)) {
                      this.rows = this.rows.filter(r => +r[columna.key] >= min && +r[columna.key] <= max);
                    }
                    break;
                }
              }
            }
          }
          this.noData = this.rows.length === 0;
        },
        getFilters() {
          const schemas = {
            select: [],
            date: {
              min: '',
              max: ''
            },
            number: {
              min: 0,
              max: 0
            },
            currency: {
              min: 0,
              max: 0
            }
          }
          let filters = {};
          for(const columna of this.columnas) {
            if ('filter' in columna) {
              filters[columna.key] = JSON.parse(JSON.stringify(schemas[columna.filter]));
            }
          }
          return filters;
        },
        getFooterSum() {
          let footer = {class: 'fw-bold background-alia-gradiente text-light'};
          const columnas = this.columnas.map(c => {return c.key;});
          // let total = null;
          // let isDate = false;
          for(const columna of columnas) {
            // total = 0;
            const tipoDato = this.rows.length > 0 ? typeof(this.rows[0][columna]) : null;
            switch (tipoDato) {
              case "string":
                // isDate = Object.keys(this.filters[columna]).includes('min');
                // if(!isDate) {
                // total = this.filters[columna].length;
                // footer[columna] = total == 0 ? 'Sin filtro' : `${total} opción/es seleccionada/s`;
                // }
                break;
              case "number":
                footer[columna] = this.rows.map(f => {return f[columna]}).reduce((sum, current) => {return sum + current})
            }
          }
          return footer;
        }
    },
    unmounted() {
      if (this.offsetHeight) {
        window.removeEventListener('resize', this.setMaxHeight);
      }
    }
}
</script>

<style>
.scrollable {
    /* max-height: 500px; */
    overflow-x: auto;
}

.pointer {
  cursor: pointer;
  color: blue;
}
</style>