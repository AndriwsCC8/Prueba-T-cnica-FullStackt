<template>
  <nav class="navbar navbar-dark navbar-expand-lg header-netflix">
    <div class="container-fluid">
      <a class="navbar-brand fw-bold" href="#" @click.prevent="$router.push('/dashboard')">G-Ventas</a>
      <div class="ms-auto">
        <button class="btn btn-outline-light me-2" @click="$router.push('/productos')">Productos</button>
        <button class="btn btn-outline-light me-2" @click="$router.push('/dashboard')">Dashboard</button>
        <button class="btn btn-danger" @click="logout">Cerrar sesión</button>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  methods: {
    logout() {
      localStorage.removeItem("token");
      this.$router.push("/login");
    }
  }
};
</script>

<style scoped>
.header-netflix {
  background: linear-gradient(90deg,#e50914 0%, #b20710 100%);
  padding: 12px 24px;
}
.navbar-brand { font-size: 1.4rem; letter-spacing: 1px; }
</style>
