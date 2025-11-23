import api from "./api";

export default {
  async getAll() {
    const r = await api.get("/Producto");
    return r.data;
  },
  async getById(id) {
    const r = await api.get(`/Producto/${id}`);
    return r.data;
  },
  async create(payload) {
    const r = await api.post("/Producto", payload);
    return r.data;
  },
  async update(id, payload) {
    const r = await api.put(`/Producto/${id}`, payload);
    return r.data;
  },
  async remove(id) {
    const r = await api.delete(`/Producto/${id}`);
    return r.data;
  }
};
