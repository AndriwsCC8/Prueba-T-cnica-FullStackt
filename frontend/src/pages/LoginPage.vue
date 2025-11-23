<template>
  <div class="login-wrap">
    <div class="login-box">
      <h2 class="mb-3">Iniciar sesión</h2>
      <form @submit.prevent="doLogin">
        <input v-model="email" type="email" placeholder="Correo" required />
        <input v-model="password" type="password" placeholder="Contraseña" required />
        <button class="btn btn-danger btn-block w-100 mt-2">Entrar</button>
        <p class="text-danger mt-2" v-if="error">{{ error }}</p>
      </form>
    </div>
  </div>
</template>

<script>
//import authService from "../services/authService";
export default {
  data() { return { email: "", password: "", error: null }; },
  methods: {
    async doLogin() {
      this.error = null;
      //const r = await authService.login(this.email, this.password);
      if (!r.success) { this.error = r.message; return; }
      localStorage.setItem("token", r.token);
      this.$router.push("/dashboard");
    }
  }
};
</script>

<style scoped>
.login-wrap{
  display:flex; align-items:center; justify-content:center;
  min-height:70vh; background:linear-gradient(180deg,#141414,#0f0f0f);
}
.login-box{ width:360px; padding:28px; background:rgba(20,20,20,0.7); border-radius:8px; color:#fff; }
input{ width:100%; padding:10px; margin:8px 0; background:#222; border:1px solid #333; color:#fff; border-radius:4px; }
</style>
