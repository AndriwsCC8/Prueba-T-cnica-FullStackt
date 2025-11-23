<template>
  <div class="dashboard">
    <Navbar />

    <div class="content">
      <Sidebar />

      <main>
        <h1>Bienvenido al Sistema de Ventas</h1>

        <div class="cards">
          <div class="card">📦 Productos</div>
          <div class="card">🧑 Clientes</div>
          <div class="card">💰 Ventas</div>
        </div>
      </main>
    </div>
  </div>
</template>

<script>
import Navbar from "./Navbar.vue"
import Sidebar from "./Sidebar.vue"

export default {
  components: {
    Navbar,
    Sidebar
  }
}
</script>

<style>
.content {
  display: flex;
}

main {
  flex: 1;
  padding: 20px;
}

.cards {
  display: flex;
  gap: 20px;
  margin-top: 20px;
}

.card {
  width: 200px;
  padding: 20px;
  background: #2563eb;
  color: white;
  text-align: center;
  font-size: 20px;
  border-radius: 10px;
}
</style>
