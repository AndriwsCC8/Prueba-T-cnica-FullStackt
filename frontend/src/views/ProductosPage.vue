<template>
  <div class="page-container">
    <h1 class="title">Productos</h1>

    <div class="form-card">
      <h2>Nuevo Producto</h2>

      <input v-model="form.nombre" type="text" placeholder="Nombre" />
      <input v-model="form.descripcion" type="text" placeholder="Descripción" />
      <input v-model.number="form.precio" type="number" placeholder="Precio" />
      <input v-model.number="form.stock" type="number" placeholder="Stock" />

      <button @click="saveProduct">Guardar</button>
    </div>

    <div class="list-card">
      <h2>Listado</h2>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Precio</th>
            <th>Stock</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="p in productos" :key="p.id">
            <td>{{ p.id }}</td>
            <td>{{ p.nombre }}</td>
            <td>{{ p.precio }}</td>
            <td>{{ p.stock }}</td>
            <td>
              <button class="delete" @click="deleteProduct(p.id)">Eliminar</button>
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
      productos: [],
      form: {
        nombre: "",
        descripcion: "",
        precio: 0,
        stock: 0
      }
    };
  },

  async created() {
    await this.loadProducts();
  },

  methods: {
    async loadProducts() {
      const res = await axios.get("http://localhost:5210/api/Productos");
      this.productos = res.data;
    },

    async saveProduct() {
      await axios.post("http://localhost:5210/api/Productos", this.form);
      await this.loadProducts();

      this.form = { nombre: "", descripcion: "", precio: 0, stock: 0 };
    },

    async deleteProduct(id) {
      await axios.delete(`http://localhost:5210/api/Productos/${id}`);
      await this.loadProducts();
    }
  }
};
</script>

<style scoped>
/* Estilo empresarial elegante */
.page-container {
  width: 90%;
  margin: auto;
  margin-top: 40px;
  color: #fff;
}

.title {
  font-size: 32px;
  margin-bottom: 25px;
  text-align: center;
  font-weight: bold;
}

.form-card,
.list-card {
  background: #141414;
  padding: 25px;
  border-radius: 12px;
  margin-bottom: 40px;
  box-shadow: 0 0 10px #000;
}

input {
  display: block;
  width: 100%;
  margin-bottom: 15px;
  padding: 12px;
  border-radius: 6px;
  background: #1f1f1f;
  border: 1px solid #444;
  color: white;
}

button {
  width: 100%;
  padding: 12px;
  background: #e50914;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  color: white;
  font-size: 16px;
}

button:hover {
  background: #b20710;
}

table {
  width: 100%;
  margin-top: 15px;
  border-collapse: collapse;
}

th, td {
  padding: 12px;
  border-bottom: 1px solid #333;
}

.delete {
  background: #b20710;
}
</style>
