import { createRouter, createWebHistory } from 'vue-router';
import authService from '../services/authService'; // Importa el servicio

const routes = [
    { path: '/login', name: 'Login', component: () => import('@/pages/LoginPage.vue') },
    { 
        path: '/', 
        name: 'Dashboard', 
        component: () => import('@/pages/DashboardPage.vue'), 
        meta: { requiresAuth: true } // Agrega el meta campo para rutas protegidas
    },
    // Agrega meta: { requiresAuth: true } a todas las rutas de gestión (Productos, Clientes, Ventas)
    { 
        path: '/productos', 
        name: 'Products', 
        component: () => import('@/pages/ProductsPage.vue'), 
        meta: { requiresAuth: true } 
    },
    // ... otras rutas protegidas
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

// Navigation Guard global
router.beforeEach((to, from, next) => {
    const isAuthenticated = authService.getToken();

    if (to.meta.requiresAuth && !isAuthenticated) {
        // Si la ruta requiere autenticación y no hay token, redirigir a Login
        next({ name: 'Login' });
    } else if (to.name === 'Login' && isAuthenticated) {
        // Si ya está autenticado e intenta ir a Login, redirigir a Dashboard
        next({ name: 'Dashboard' });
    } else {
        // Permitir el acceso
        next();
    }
});

export default router;