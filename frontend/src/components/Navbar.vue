<template>
  <nav class="navbar">
    <h2>Panel de Control</h2>

    <button @click="logout">Cerrar sesión</button>
  </nav>
</template>

<script>
export default {
  methods: {
    logout() {
      localStorage.removeItem("token")
      this.$router.push("/")
    }
  }
}
</script>

<style>
.navbar {
  height: 60px;
  background: #1e3a8a;
  color: white;
  padding: 15px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.navbar button {
  background: #dc2626;
  padding: 8px 15px;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}
</style>
