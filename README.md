# SistemaInventarioPrueba

## Requisitos

### Backend
- .NET 8.0
- Visual Studio 2022
- SQL Server

### Frontend
- Nodejs
- Angular v16 (npm install -g @angular/cli@16)

## Ejecucion del Backend
- Ejecutar el script de base de datos
- Abrir la solucion encontrada en ServicioProductos/SistemaInventarios
- Asegurarse que el proyecto de inicio sea SistemaInventarios.ProductosApi
- Revisar que el archivo SistemaInventarios.ProductosApi/appsettings.json en DefaultConnection tenga la cadena de conexion a la base de datos que se va a utilizar
- En caso de querer cambiar los puertos del API (no recomendable) ir a SistemaInventarios.ProductosApi/Properties/launchSettings y cambiarlos en applicationUrl
- Correr la aplicacion en https (https://localhost:7040)
- Abrir la solucion encontrada en ServicioTransacciones/SistemaInventarioTransacciones
- Asegurarse que el proyecto de inicio sea SistemaInventarioTransacciones.Api
- Revisar que el archivo SistemaInventarioTransacciones.Api/appsettings.json en DefaultConnection tenga la cadena de conexion a la base de datos que se va a utilizar
- En caso de querer cambiar los puertos del API (no recomendable) ir a SistemaInventarioTransacciones.Api/Properties/launchSettings y cambiarlos en applicationUrl
- Correr la aplicacion en https (https://localhost:7177)
- Se le quito el swagger, funciona con endpoints

## Ejecucion del FrontEnd
- El proyecto se encuentra en FrontEnd/SistemaInventarioFront/sistema-inventario
- En caso de cambiar los puertos de los apis sistema-inventario/src/app/services/common/global.ts cambiar urlProductos o urlTransacciones dado el caso
- Abrir en una terminal el proyecto
- Ejecutar npm install 
- Ejecutar ng serve
- Abrir http://localhost:4200 para ejecutar la aplicacion
## Evidencias
### Pantalla de Inicio
![Captura de pantalla 2025-05-29 163303](https://github.com/user-attachments/assets/697fadf7-19a5-4b0f-9087-e309b7c67216)

### Listado de productos con paginacion
![Captura de pantalla 2025-05-29 163322](https://github.com/user-attachments/assets/e014ab26-f275-44bd-a7dc-c72b4c443eed)

### Pantallas para creacion de productos

![Captura de pantalla 2025-05-29 163346](https://github.com/user-attachments/assets/369b3507-a268-432c-9202-4aae0c076ce0)
![Captura de pantalla 2025-05-29 163405](https://github.com/user-attachments/assets/e8a462de-89e5-47eb-a990-602a9c3689e8)
![Captura de pantalla 2025-05-29 163547](https://github.com/user-attachments/assets/5df47540-0691-4bed-8483-102ce23fb696)

### Pantalla para actualizacion de productos

![Captura de pantalla 2025-05-29 163631](https://github.com/user-attachments/assets/70061da7-f943-4469-afab-b776881f9306)
![Captura de pantalla 2025-05-29 163704](https://github.com/user-attachments/assets/c11d6c2d-6a03-4e6e-be16-47d21a74ede8)

### Eliminacion de productos

![Captura de pantalla 2025-05-29 163722](https://github.com/user-attachments/assets/777e05d3-4eb4-46a9-80bd-19e588e765ed)
![Captura de pantalla 2025-05-29 163749](https://github.com/user-attachments/assets/2268fac7-590a-48f7-b21d-4f69e87f999c)

### Listado de transacciones con paginacion

![Captura de pantalla 2025-05-29 165854](https://github.com/user-attachments/assets/a46a0be1-1181-41f2-9677-37c4f61e1e69)

### Pantallas para creacion de transacciones

![Captura de pantalla 2025-05-29 165259](https://github.com/user-attachments/assets/1949cbb2-fbfc-40a0-b331-fbc4f6fa8de3)
![Captura de pantalla 2025-05-29 165350](https://github.com/user-attachments/assets/3bc49f94-ddf6-44d7-90c8-bb44b9810a9b)

- Validacion Stock
![Captura de pantalla 2025-05-29 165749](https://github.com/user-attachments/assets/2a0405e6-6998-4dc7-b9c4-4ef6d99aaad1)
![Captura de pantalla 2025-05-29 165832](https://github.com/user-attachments/assets/3cf093d0-a6e3-49e2-8a45-550c142253c9)

### Pantalla para edicion de transacciones
![Captura de pantalla 2025-05-29 170021](https://github.com/user-attachments/assets/c3b271da-0cf0-4082-bb63-775aba59ca10)

### Eliminacion de transacciones
![Captura de pantalla 2025-05-29 170241](https://github.com/user-attachments/assets/b14bcd85-cdf3-475c-be91-a6f7f7a10d5f)

### Filtros
- Filtro de productos
![Captura de pantalla 2025-05-29 163823](https://github.com/user-attachments/assets/0ae74d0a-48c0-42d4-a2dd-c4a69401c44e)

- Filtros de transacciones
  - Filtro por productos
    ![Captura de pantalla 2025-05-29 170601](https://github.com/user-attachments/assets/cd51613b-d504-4b88-a095-c47e5f258dfd)
  - Filtro por fechas
    ![Captura de pantalla 2025-05-29 170851](https://github.com/user-attachments/assets/0a7d6929-d3a3-4a59-9c9a-17991ac38fe7)
    ![Captura de pantalla 2025-05-29 170946](https://github.com/user-attachments/assets/70f75c28-bbf7-4029-b641-8ff014cdc0f4)

