<template>
  <div></div>
</template>

<script>
export default {
  name: "LogoutApp",
  data() {
    return {
      email: "",
      clave: "",
    };
  },
  mounted() {
    localStorage.clear();
    this.$router.push('/login');
  },
};
</script>
