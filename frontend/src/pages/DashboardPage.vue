<template>
  <div>
    <div class="hero">
      <h1>Panel Administrativo</h1>
      <p class="lead">Resumen de ventas y productos</p>
    </div>

    <div class="row mt-4">
      <div class="col-md-6">
        <div class="card dash-card p-3">
          <h5>Total ventas (ejemplo)</h5>
          <h2>RD$ 0.00</h2>
        </div>
      </div>
      <div class="col-md-6">
        <div class="card dash-card p-3">
          <h5>Productos</h5>
          <h2>0</h2>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default { name: "DashboardPage" };
</script>

<style scoped>
.hero{ padding: 28px; background: linear-gradient(90deg,#e50914,#b20710); color:#fff; border-radius:8px; }
.dash-card{ background:#151515; color:#fff; border:none; border-radius:8px; }
</style>
