import axios from 'axios';

const API_URL = '/api/auth'; // Usamos el proxy configurado en vite.config.js

const login = async (email, password) => {
    try {
        const response = await axios.post(`${API_URL}/login`, { email, password });
        
        // Si el login es exitoso, almacenamos el token JWT
        if (response.data.token) {
            localStorage.setItem('userToken', response.data.token);
        }
        return response.data;
    } catch (error) {
        // Manejo de errores (ej. credenciales inválidas)
        console.error("Login failed:", error.response?.data || error.message);
        throw error; 
    }
};

const logout = () => {
    localStorage.removeItem('userToken');
};

const authService = {
    login,
    logout,
    getToken: () => localStorage.getItem('userToken')
};

export default authService;