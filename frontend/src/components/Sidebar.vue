<template>
  <aside class="sidebar">
    <ul>
      <li @click="$router.push('/dashboard')">Dashboard</li>
      <li>Productos</li>
      <li>Clientes</li>
      <li>Ventas</li>
    </ul>
  </aside>
</template>

<style>
.sidebar {
  width: 200px;
  height: 100vh;
  background: #f3f4f6;
  padding-top: 20px;
  border-right: 1px solid #ddd;
}

.sidebar ul {
  list-style: none;
  padding: 0;
}

.sidebar li {
  padding: 15px;
  cursor: pointer;
}

.sidebar li:hover {
  background: #e5e7eb;
}
</style>
