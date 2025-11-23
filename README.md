#  Prueba Técnica Full-Stack: Gestión de Ventas

##  Resumen del Proyecto y Arquitectura
Este proyecto implementa un sistema de gestión de ventas utilizando una **Arquitectura en Capas** (Domain, Application, Infrastructure, API) como lo requiere la prueba. Se aplican principios como la **Separación de Intereses** y **SOLID**.

**Tecnologías Utilizadas:**
* **Backend:** C# / .NET 8, SQL Server, Entity Framework Core, JWT para Autenticación.
* **Frontend:** Vue.js 3 con Composition API y Vite.

**Funcionalidad Implementada:**
* **Backend:** API REST con **CRUD completo** para Productos, Clientes y Ventas. Implementación completa de **Autenticación JWT** (Login y Registro).

##  Instrucciones para Ejecutar y Validar el Proyecto

**IMPORTANTE: Fallo en la Integración de la SPA.** La configuración de archivos estáticos (`Program.cs`) está implementada para servir la SPA, pero un error de *middleware* en Kestrel/IIS Express impide que el `index.html` sirva correctamente el JS/CSS compilado en producción (pantalla en blanco). Para la validación, por favor ejecute el Backend y el Frontend en **servidores separados**.

### A. Verificación de la Base de Datos
1.  Abrir SQL Server Management Studio (SSMS) o Azure Data Studio.
2.  Ejecutar el script **`db_script.sql`** que se encuentra en la raíz del proyecto para crear la base de datos y su estructura de tablas.

### B. Ejecución del Backend (API y Validación JWT)
1.  **Navegar a la API:**
    ```bash
    cd backend/GestionVentas.API
    ```
2.  **Iniciar el Servidor:**
    ```bash
    dotnet run
    ```
    (El servidor se iniciará en **`http://localhost:5210`** ).

3.  **Validar la Seguridad (JWT) en Swagger:**
    * Abrir **`http://localhost:5210/swagger`**.
    * **Registro (POST /api/Auth/register):** Crear un usuario de prueba.
    * **Login (POST /api/Auth/login):** Iniciar sesión y **copiar el Token JWT** devuelto.
    * **Autorización:** Usar el botón **Authorize** de Swagger, pegar el token en formato `Bearer [Token]` y verificar que se puede acceder a `GET /api/Cliente` (status 200).

### C. Ejecución del Frontend (Interfaz de Usuario)
1.  **Navegar al Frontend:**
    ```bash
    cd frontend
    ```
2.  **Instalar Dependencias:**
    ```bash
    npm install
    ```
3.  **Iniciar Servidor de Desarrollo:**
    ```bash
    npm run dev
    ```
    (El frontend se abrirá en **`http://localhost:5173`** o similar). La interfaz de Vue.js será visible y está configurada para consumir la API en el puerto 5210.
