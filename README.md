# PIMS_DOTNET Project

## Project Overview
**PIMS_DOTNET** is a Product Inventory Management System built with ASP.NET Core 8 and Entity Framework Core.  
The system supports user authentication with JWT, role-based access control (RBAC), product and inventory management, category management, and audit logging.  

---

## Features
- User registration and login with JWT authentication  
- Role-based access control (Admin/User roles)  
- CRUD operations for Products, Categories, Inventory, and Roles  
- Inventory transactions and stock management  
- Audit logs for tracking system actions  
- Swagger/OpenAPI integration for testing endpoints  

---

## Technologies Used
- **Backend:** ASP.NET Core 8.0  
- **ORM:** Entity Framework Core  
- **Database:** SQL Server  
- **Authentication:** JWT (JSON Web Tokens)  
- **Mapping:** AutoMapper  
- **Testing:** Postman  
- **Documentation:** Swagger  

---

## Prerequisites
- .NET 8 SDK  
- SQL Server installed and running  
- Git installed  

---

## Setup Instructions

### 1. Clone the repository
```bash
git clone https://github.com/<Chamanpatel0601>/PIMS_DOTNET.git
cd PIMS_DOTNET


### instructions for JWT in appsettings.json:
"Jwt": {
  "Key": "<ThisIsASecretKeyForJWTGeneration123>",
  "Issuer": "PIMS_DOTNET",
  "Audience": "PIMS_DOTNET_USERS",
  "ExpiresInMinutes": 60
}

## Testing API

POST to register a user:

POST /api/User/register
{
  "username": "admin",
  "email": "admin@example.com",
  "password": "Admin123!",
  "roleId": 1
}

POST to login:

POST /api/User/login
{
  "username": "admin",
  "password": "Admin123!"
}

##Get all users (JWT required)
GET /api/User
Header:
Authorization: Bearer <JWT_TOKEN>

###Get user by ID (JWT required)
GET /api/User/{userId}
Header:
Authorization: Bearer <JWT_TOKEN>

###Get all roles



