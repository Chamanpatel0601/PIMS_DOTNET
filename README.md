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

##Response
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjOWYyNWVhNS01MDBhLTQ2ZGEtYTUyNi1jNDEwOGM0NTk4ZjkiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW5pc3RyYXRvciIsImV4cCI6MTc1ODg3MzcwMywiaXNzIjoiUElNU19ET1RORVQiLCJhdWQiOiJQSU1TX0RPVE5FVF9VU0VSUyJ9.gysi-KNfBpSmh2ZWD91yuMyg53-1s1zH17iZyeakpSg",
    "user": {
        "userId": "c9f25ea5-500a-46da-a526-c4108c4598f9",
        "username": "admin",
        "email": "admin@example.com",
        "roleId": 1,
        "roleName": "Administrator",
        "isActive": true,
        "createdDate": "2025-09-25T21:48:53.1959192",
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjOWYyNWVhNS01MDBhLTQ2ZGEtYTUyNi1jNDEwOGM0NTk4ZjkiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IkFkbWluaXN0cmF0b3IiLCJuYmYiOjE3NTg4NjY1MDMsImV4cCI6MTc1ODg3MzcwMywiaWF0IjoxNzU4ODY2NTAzfQ.-avGHS6co15t7EEQWk06-o7DmkQ_kljYjD_zU3Mn3u4"
    }
}

## GET ALL USER 

Create a new request.
Select GET.
URL:
https://localhost:7175/api/User
Go to the Authorization tab:
Type: Bearer Token
Paste the copied token in the Token box.
then sent it 
it will give all user details


##Adjust Inventory

POST /api/Inventory/adjust

{
  "productId": "786370e4-15ba-4d19-986f-f1577e6a7a5d",
  "quantityChange": 50,
  "reason": "Initial stock load",
  "userId": "ef739fa8-4ede-462a-bd69-5484f52ea302"
}


Create Inventory

POST /api/Inventory/create

{
  "inventoryId": "5d453405-1f01-416d-97da-39f6f808ee2e",
  "productId": "786370e4-15ba-4d19-986f-f1577e6a7a5d",
  "quantity": 50,
  "warehouseLocation": "Warehouse A",
  "lowStockThreshold": 5,
  "lastUpdated": "2025-09-25T19:16:58.602043"
}



##Post category 

https://localhost:7175/api/Category
go to body and select raw and add category like
{
    "categoryName": "Stationery",
    "description": "Office supplies"
}
and then send it 
the response you get 
{
    "categoryId": 6,
    "categoryName": "Stationery",
    "description": "Office supplies for different location"
}

## get category
select get and 
https://localhost:7175/api/Category
and sent it 
{
        "categoryId": 1,
        "categoryName": "Electronics",
        "description": "Electronic gadgets and devices"
    },
    {
        "categoryId": 2,
        "categoryName": "Clothing",
        "description": "Apparel and fashion items"
    },
    {
        "categoryId": 4,
        "categoryName": "Furniture",
        "description": "Home furniture"
    },
    {
        "categoryId": 5,
        "categoryName": "Stationery",
        "description": "Office supplies"
    },



