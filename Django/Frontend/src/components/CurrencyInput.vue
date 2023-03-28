<template>
  <div class="mb-3">
    <label v-if="label" class="form-label">{{ label }}</label>
    <span v-if="isRequired()" class="text-danger"> * </span>
    <input
      v-bind="$attrs"
      class="form-control"
      ref="inputRef"
      type="text"
      :value="modelValue"
      @input="$emit('update:modelValue', $event.target.value)"
    />
    <small v-if="isMin() && options.valueRange.min" class="form-text text-muted">Importe mínimo: {{ getCurrencyFormat(options.valueRange.min) }}</small>
  </div>
  
  </template>
  
  <script>
  import { useCurrencyInput } from 'vue-currency-input'
  
  export default {
    name: 'CurrencyInput',
    props: {
      label: {
        type: String,
        default: ''
      },
      modelValue: Number,
      options: {
        type: Object,
        default: () => Object({
            currency: 'EUR'
        })
      }
    },
    setup(props) {
      const { inputRef } = useCurrencyInput(props.options)
  
      return { inputRef }
    },
    methods: {
        isRequired() {
            return 'required' in this.$attrs;
        },
        isMin() {
          return 'valueRange' in this.options && 'min' in this.options.valueRange;
        }
    }
  }
  </script>