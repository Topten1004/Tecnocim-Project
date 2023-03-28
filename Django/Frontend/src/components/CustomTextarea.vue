<template>
    <div class="mb-3">
        <label v-if="label" class="form-label">{{ label }}</label>
        <span v-if="isRequired()" class="text-danger"> * </span>
        <textarea
            v-bind="$attrs"
            class="form-control"
            :value="modelValue"
            @input="$emit('update:modelValue', $event.target.value)"
        >
        </textarea>
    </div>
</template>

<script>
export default {
    name: 'CustomTextarea',
    props: {
        label: {
            type: String,
            default: '' 
        },
        modelValue: String,
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