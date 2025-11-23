<template>
  <div id="app">
    <HeaderNav />
    <main class="container-fluid main-area">
      <router-view />
    </main>
    <footer class="text-center text-muted py-3">© 2025 GestionVentas</footer>
  </div>
</template>

<script>
import HeaderNav from "./components/HeaderNav.vue";
export default {
  components: { HeaderNav }
};
</script>

<style>
/* small safe styling here */
#app { min-height: 100vh; display:flex; flex-direction:column; background:#141414; color:#fff; }
.main-area { padding: 24px 36px; flex:1; }
footer { background: #0f0f0f; }
</style>
