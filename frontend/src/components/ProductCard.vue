<template>
  <div class="card product-card">
    <img :src="image" class="card-img-top" alt="product" />
    <div class="card-body">
      <h5 class="card-title">{{ product.nombre }}</h5>
      <p class="card-text small">{{ product.descripcion }}</p>
      <div class="d-flex justify-content-between align-items-center mt-2">
        <strong>{{ formatMoney(product.precio) }}</strong>
        <div>
          <button class="btn btn-sm btn-outline-light me-1" @click="$emit('edit', product)">Editar</button>
          <button class="btn btn-sm btn-danger" @click="$emit('delete', product.id)">Eliminar</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: { product: { type: Object, required: true } },
  computed: {
    image() {
      // placeholder image
      return "https://picsum.photos/seed/" + (this.product.id || 1) + "/300/160";
    }
  },
  methods: {
    formatMoney(v) {
      return new Intl.NumberFormat("es-DO", { style: "currency", currency: "DOP" }).format(v);
    }
  }
};
</script>

<style scoped>
.product-card {
  background: #181818;
  color: #fff;
  border: none;
  border-radius: 6px;
  overflow: hidden;
}
.card-img-top { height: 160px; object-fit: cover; }
.card-body { padding: 12px; }
.btn-outline-light { color: #fff; border-color: rgba(255,255,255,0.2); }
</style>
