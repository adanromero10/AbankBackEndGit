# API REST en .NET 8 con MySQL

## Instalación
1. Clonar el repositorio: 'git clone https://github.com/usuario/repo.git'
2. Configurar la base de datos PostgreSQL
3. Configurar 'appsettings.json'
4. Ejecutar la aplicación: 'dotnet run'

## Endpoints
### Autenticación
- **POST** `/api/v1/auth/login`

### Usuarios
- **GET** '/api/v1/users' (Requiere autenticación JWT)
