# **API de Gestión de Usuarios**

Este proyecto implementa una API REST para gestionar usuarios utilizando **ASP.NET Core 8** y **SQLite** como base de datos. La API está configurada para aplicar migraciones automáticamente al inicio y proporciona datos iniciales mediante seeding.

---

## **Requisitos Previos**

- **.NET 8 SDK:** [Descargar aquí](https://dotnet.microsoft.com/download).
- **Visual Studio Code** u otro editor compatible con .NET.
- **Postman** (opcional para probar los endpoints).

---

## **Configuración Inicial**

### **Clonar el repositorio**
```bash
git clone https://github.com/DavidZeballos/IDWM_Prueba2
cd UserApi
```

### **Verificar configuración de la base de datos**
- Asegúrate de que el archivo `appsettings.json` tenga la siguiente configuración para SQLite:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=users.db"
     }
   }
   ```

### **Ejecutar la aplicación**
No necesitas comandos manuales de migración. Simplemente ejecuta:
```bash
dotnet run
```

### **Qué sucede al iniciar**
1. Se aplicarán automáticamente las migraciones pendientes.
2. Si no existe, se creará la base de datos `users.db`.
3. Se inicializarán datos de prueba en la tabla `Users`.

---

## **Endpoints Disponibles**

### **Base URL**
```
http://localhost:5000/api
```

### **Lista de Endpoints**

#### **1. Obtener todos los usuarios**
- **URL:** `/user`
- **Método:** `GET`
- **Descripción:** Retorna una lista de todos los usuarios registrados.

#### **2. Crear un usuario**
- **URL:** `/user`
- **Método:** `POST`
- **Descripción:** Permite registrar un nuevo usuario en la base de datos.
- **Cuerpo de la Solicitud (JSON):**
   ```json
   {
     "name": "Carlos Díaz",
     "email": "carlos.diaz@example.com",
     "birthDate": "1995-06-30",
     "gender": "Masculino"
   }
   ```