<template>
  <div class="page-container">
    <h1 class="title">Ventas</h1>

    <div class="form-card">
      <h2>Registrar Venta</h2>

      <select v-model="form.clienteId">
        <option disabled value="">Seleccionar Cliente</option>
        <option v-for="c in clientes" :key="c.id" :value="c.id">
          {{ c.nombre }}
        </option>
      </select>

      <input v-model.number="form.total" type="number" placeholder="Total" />
      <input v-model="form.fecha" type="datetime-local" />

      <button @click="saveVenta">Registrar</button>
    </div>

    <div class="list-card">
      <h2>Listado</h2>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Cliente</th>
            <th>Total</th>
            <th>Fecha</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="v in ventas" :key="v.id">
            <td>{{ v.id }}</td>
            <td>{{ v.cliente.nombre }}</td>
            <td>{{ v.total }}</td>
            <td>{{ new Date(v.fecha).toLocaleString() }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      ventas: [],
      clientes: [],
      form: {
        clienteId: "",
        total: 0,
        fecha: new Date().toISOString()
      }
    };
  },

  async created() {
    await this.loadClientes();
    await this.loadVentas();
  },

  methods: {
    async loadClientes() {
      const res = await axios.get("http://localhost:5210/api/Clientes");
      this.clientes = res.data;
    },

    async loadVentas() {
      const res = await axios.get("http://localhost:5210/api/Ventas");
      this.ventas = res.data;
    },

    async saveVenta() {
      await axios.post("http://localhost:5210/api/Ventas", this.form);
      await this.loadVentas();

      this.form.total = 0;
    }
  }
};
</script>

<style scoped>
.page-container {
  padding: 20px;
  color: #fff;
}

.section-box {
  background: #141414;
  padding: 20px;
  border-radius: 10px;
  margin-bottom: 20px;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th {
  background: #E50914;
  padding: 10px;
  color: white;
}

td {
  padding: 10px;
  border-bottom: 1px solid #333;
}

button {
  background: #E50914;
  border: none;
  padding: 8px 12px;
  border-radius: 5px;
  color: white;
  cursor: pointer;
}
</style>

