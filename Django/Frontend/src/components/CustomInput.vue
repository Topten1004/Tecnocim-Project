<template>
    <div class="mb-3">
        <label v-if="label" class="form-label" :class="{ 'text-capitalize': capitalize }">{{ label }}</label>
        <span v-if="isRequired()" class="text-danger"> * </span>
        <input
            v-bind="$attrs"
            class="form-control"
            :value="modelValue"
            @input="$emit('update:modelValue', $event.target.value)"
        />
    </div>
</template>

<script>
export default {
    name: 'CustomInput',
    props: {
        label: {
            type: String,
            default: ''
        },
        capitalize: {
          type: Boolean,
          default: false
        },
        modelValue: [String, Number]
    },
    methods: {
        isRequired() {
            return 'required' in this.$attrs;
        }
    }
}
</script>

<style>
</style>