# 📦 Inventory Management System API

A complete **Inventory Management System REST API** built with **ASP.NET Core 8**, **Entity Framework Core**, and the **CQRS pattern using MediatR**.  
The system supports secure authentication, inventory tracking, warehouse management, and transaction logging.

---

## 🚀 Features

- 🔐 **JWT Authentication & Authorization**
- 👤 **User Management with ASP.NET Identity**
- 📦 **Product Management (CRUD)**
- 🏬 **Warehouse Management**
- 📊 **Stocktaking System**
- 🔄 **Transaction Logging**
- 🧩 **CQRS Pattern with MediatR**
- 📘 **Swagger API Documentation**

---

## 🛠️ Technologies Used

- **ASP.NET Core 8.0** – Web API
- **Entity Framework Core 8.0**
- **SQL Server**
- **JWT Authentication**
- **ASP.NET Identity**
- **MediatR (CQRS Pattern)**
- **Swagger / OpenAPI**

---

## 📂 Project Structure
Inventory Management System/<br>
├── Controllers/ # API Controllers<br>
├── Models/ # Entity Models<br>
│ ├── ApplicationUser.cs<br>
│ ├── Product.cs<br>
│ ├── Warehouse.cs<br>
│ ├── Transaction.cs<br>
│ └── Stocktaking.cs<br>
├── Interfaces/ # Repository Interfaces<br>
├── Repositories/ # Data Access Layer<br>
├── CQRS/ # CQRS Pattern<br>
│ ├── Commands/<br>
│ ├── Queries/<br>
│ └── Handlers/<br>
├── Program.cs # Application Startup<br>
├── appsettings.json # Configuration<br>
└── Inventory Management System.csproj<br>
