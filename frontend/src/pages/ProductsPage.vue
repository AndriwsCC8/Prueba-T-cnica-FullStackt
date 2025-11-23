<template>
  <div>
    <div class="d-flex justify-content-between align-items-center mb-3">
      <h3>Productos</h3>
      <div>
        <button class="btn btn-outline-light me-2" @click="fetchProducts">Actualizar</button>
        <button class="btn btn-danger" @click="goCreate">Nuevo Producto</button>
      </div>
    </div>

    <div class="row g-3">
      <div v-for="p in productos" :key="p.id" class="col-md-3">
        <ProductCard :product="p" @edit="onEdit" @delete="onDelete" />
      </div>
    </div>

    <!-- simple modal for create/edit -->
    <div v-if="showForm" class="modal-backdrop">
      <div class="modal-content-custom">
        <h5>{{ editing ? 'Editar' : 'Crear' }} producto</h5>
        <form @submit.prevent="save">
          <input v-model="form.nombre" placeholder="Nombre" required />
          <input v-model="form.descripcion" placeholder="Descripción" />
          <input type="number" v-model.number="form.precio" placeholder="Precio" required />
          <input type="number" v-model.number="form.stock" placeholder="Stock" required />
          <div class="mt-2 text-end">
            <button class="btn btn-outline-light me-2" type="button" @click="closeForm">Cerrar</button>
            <button class="btn btn-danger" type="submit">Guardar</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import ProductCard from "../components/ProductCard.vue";
import productService from "../services/productService";

export default {
  components: { ProductCard },
  data() {
    return { productos: [], showForm:false, editing:false, form:{nombre:'',descripcion:'',precio:1,stock:0}, editId:null };
  },
  mounted(){ this.fetchProducts(); },
  methods:{
    async fetchProducts(){
      try{
        this.productos = await productService.getAll();
      }catch(e){ console.error(e); alert("Error al cargar productos"); }
    },
    goCreate(){ this.editing=false; this.editId=null; this.form={nombre:'',descripcion:'',precio:1,stock:0}; this.showForm=true; },
    onEdit(prod){ this.editing=true; this.editId=prod.id; this.form={...prod}; this.showForm=true; },
    async onDelete(id){
      if(!confirm("Eliminar producto?")) return;
      await productService.remove(id);
      this.fetchProducts();
    },
    closeForm(){ this.showForm=false; },
    async save(){
      try{
        if(this.editing) await productService.update(this.editId, this.form);
        else await productService.create(this.form);
        this.showForm=false;
        this.fetchProducts();
      }catch(e){ alert("Error guardando: "+(e?.response?.data ?? e.message)); }
    }
  }
}
</script>

<style scoped>
.modal-backdrop{ position:fixed; inset:0; display:flex; align-items:center; justify-content:center; background:rgba(0,0,0,0.7); }
.modal-content-custom{ background:#1b1b1b; padding:20px; border-radius:8px; width:420px; color:#fff; }
input{ width:100%; padding:8px; margin:8px 0; background:#222; border:1px solid #333; color:#fff; border-radius:4px; }
</style>
