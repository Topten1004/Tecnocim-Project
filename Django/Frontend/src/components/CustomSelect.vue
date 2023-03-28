<template>
    <div class="mb-3">
        <label v-if="label" class="form-label">{{ label }}</label>
        <span v-if="isRequired()" class="text-danger"> * </span>
        <select
            v-if="!multiple"
            v-bind="$attrs"
            class="form-control"
            :value="modelValue"
            @input="$emit('update:modelValue', $event.target.value)"
        >
            <option :value="defaultValue.id">{{ defaultValue.text }}</option>
            <option
                v-for="option in options"
                v-bind:value="option.id"
                v-bind:key="option.id"
            >
                {{ option.text }}
            </option>
        </select>
        <Select2
            v-if="multiple"
            v-bind="$attrs"
            :modelValue="modelValue"
            :options="options"
            @input="$emit('update:modelValue', $event.target.value)"
            :placeholder="defaultValue.text"
        />
    </div>
</template>

<script>
// TODO: Sigue dando warning de v-model para Select2 con arrays
// TODO: Implementar búsuqeda para Select2 múltiple, si es que se puede

const $ = require("jquery");
import Select2 from 'vue3-select2-component';

export default {
    name: 'CustomSelect',
    components: {
        Select2
    },
    props: {
        label: {
            type: String,
            default: '' 
        }, 
        modelValue: [String, Number, Boolean],
        defaultValue: {
            type: Object,
            default: () => Object({
                id: '',
                text: 'Seleccione una opción'
            })
        },
        options: {
            type: Array,
            default: () => []
        },
        multiple: {
            type: Boolean,
            default: false
        }
    },
    mounted() {
        if (this.multiple) {
          $(document).on('select2:open', () => {
            document.querySelector('.select2-search__field').focus();
          });
          if (this.$attrs.settings && this.$attrs.settings.multiple) {
            this.interval = setInterval(() => $('.select2-search__field').prop('disabled', true), 500);
          }
        }
    },
    data() {
        return {
          interval: null
        }
    },
    methods: {
      isRequired() {
        return 'required' in this.$attrs;
      }
    },
    unmounted() {
      clearInterval(this.interval);
    }
}
</script>