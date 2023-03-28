<template>
    <font-awesome-icon :icon="icon" :class="{'text-alia': color }" />
</template>
<script>
export default {
    name: 'AliaIcon',
    props: {
        color: {
            type: Boolean,
            default: true
        },
        icon: {
            type: String,
            default: 'fa-user'
        }
    }
}
</script>
<style>
</style>