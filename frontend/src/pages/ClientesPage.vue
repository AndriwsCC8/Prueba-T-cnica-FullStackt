<template>
  <div class="page-container">
    <h1 class="title">Clientes</h1>

    <div class="form-card">
      <h2>Nuevo Cliente</h2>

      <input v-model="form.nombre" type="text" placeholder="Nombre" />
      <input v-model="form.email" type="email" placeholder="Correo" />
      <input v-model="form.telefono" type="text" placeholder="Teléfono" />

      <button @click="saveCliente">Guardar</button>
    </div>

    <div class="list-card">
      <h2>Listado</h2>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Email</th>
            <th>Teléfono</th>
            <th></th>
          </tr>
        </thead>

        <tbody>
          <tr v-for="c in clientes" :key="c.id">
            <td>{{ c.id }}</td>
            <td>{{ c.nombre }}</td>
            <td>{{ c.email }}</td>
            <td>{{ c.telefono }}</td>
            <td>
              <button class="delete" @click="deleteCliente(c.id)">Eliminar</button>
            </td>
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
      clientes: [],
      form: {
        nombre: "",
        email: "",
        telefono: ""
      }
    };
  },

  async created() {
    await this.loadClientes();
  },

  methods: {
    async loadClientes() {
      const res = await axios.get("http://localhost:5210/api/Clientes");
      this.clientes = res.data;
    },

    async saveCliente() {
      await axios.post("http://localhost:5210/api/Clientes", this.form);
      await this.loadClientes();

      this.form = { nombre: "", email: "", telefono: "" };
    },

    async deleteCliente(id) {
      await axios.delete(`http://localhost:5210/api/Clientes/${id}`);
      await this.loadClientes();
    }
  }
};
</script>

<style scoped>
.page-container {
  padding: 20px;
  color: #fff;
}

.form-box, .table-container {
  background: #141414;
  padding: 20px;
  border-radius: 10px;
  margin-bottom: 20px;
}

label {
  display: block;
  margin-bottom: 5px;
}

input {
  width: 100%;
  padding: 8px;
  margin-bottom: 10px;
  border-radius: 5px;
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

