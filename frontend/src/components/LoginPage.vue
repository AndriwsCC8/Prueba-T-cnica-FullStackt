<template>
  <div class="login-container">
    <h1>Sistema de Ventas</h1>
    
    <div class="login-box">
      <input v-model="email" type="email" placeholder="Correo" />
      <input v-model="password" type="password" placeholder="Contraseña" />

      <button @click="login">Ingresar</button>

      <p class="error" v-if="error">{{ error }}</p>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      email: "",
      password: "",
      error: ""
    }
  },
  methods: {
    async login() {
      try {
        const res = await fetch("http://localhost:5179/api/Auth/login", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            email: this.email,
            password: this.password
          })
        })

        const data = await res.json()

        if (!data.isSuccess) {
          this.error = data.message
          return
        }

        localStorage.setItem("token", data.token)
        this.$router.push("/dashboard")
      } catch (err) {
        this.error = "Error en el servidor"
      }
    }
  }
}
</script>

<style>
.login-container {
  height: 100vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.login-box {
  width: 300px;
  padding: 20px;
  border: 1px solid #ddd;
  border-radius: 10px;
}

.login-box input {
  width: 100%;
  padding: 10px;
  margin-bottom: 10px;
}

button {
  width: 100%;
  padding: 10px;
  cursor: pointer;
}

.error {
  margin-top: 10px;
  color: red;
  text-align: center;
}
</style>
