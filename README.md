# ZAM AI MART

> A full production-ready AI tools directory with INR pricing, built by **Ahmad Faraz Siddiqui**.

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Backend API | .NET 8 Web API |
| Admin Panel | .NET 8 Blazor Server |
| Frontend | .NET 10 Blazor WebAssembly |
| Database | MySQL (local) |
| ORM | Entity Framework Core 8 + Pomelo MySQL |
| Auth | JWT (API) + Cookie Auth (Admin) |
| API Docs | Swagger / OpenAPI |
| Architecture | Clean Architecture |

---

## 📁 Project Structure

```
src/
├── ZamAiMart.Core/            # Entities, DTOs, Interfaces (.NET 8)
├── ZamAiMart.Infrastructure/  # DbContext, Repositories, Services (.NET 8)
├── ZamAiMart.API/             # REST API + Swagger (.NET 8)  → http://localhost:5000
├── ZamAiMart.Admin/           # Blazor Server Admin Panel   → http://localhost:5002
└── ZamAiMart.Client/          # Blazor WASM Frontend        → http://localhost:5001
```

---

## 🗄️ Database Setup

### 1. Install & Start MySQL

**Option A — MySQL Community Server:**
```
https://dev.mysql.com/downloads/mysql/
```

**Option B — XAMPP (includes MySQL):**
```
https://www.apachefriends.org/
```

**Option C — Docker:**
```bash
docker run --name zam-mysql -e MYSQL_ROOT_PASSWORD=yourpassword -p 3306:3306 -d mysql:8.0
```

### 2. Create the database

```sql
CREATE DATABASE zam_ai_mart_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

### 3. Set your MySQL password

Edit **both** config files and replace `yourpassword`:

- `src/ZamAiMart.API/appsettings.json`
- `src/ZamAiMart.Admin/appsettings.json`

```json
{
  "ConnectionStrings": {
    "MySqlConnection": "Server=localhost;Database=zam_ai_mart_db;User=root;Password=yourpassword;"
  }
}
```

---

## ▶️ Running the Application

### Start the API

```bash
cd src/ZamAiMart.API
dotnet run
```

- API: `http://localhost:5000`
- Swagger: `http://localhost:5000/swagger`
- Tables are auto-created and seeded on first run ✅

### Start the Admin Panel

```bash
cd src/ZamAiMart.Admin
dotnet run
```

- Admin: `http://localhost:5002`
- Default login: **admin / Admin@1234**

### Start the Frontend (Optional)

```bash
cd src/ZamAiMart.Client
dotnet run
```

- Frontend: `http://localhost:5001`

---

## 🗃️ Database Schema

### AIWebsites

| Column | Type | Notes |
|---|---|---|
| Id | int | Primary key, auto-increment |
| Name | varchar(200) | Tool name |
| Category | varchar(100) | Category name |
| PriceINR | decimal(18,2) | Price in Indian Rupees |
| IsFree | boolean | True = FREE |
| WebsiteURL | varchar(500) | Original website URL |
| Description | text | Tool description |
| LogoURL | varchar(500) | Logo image URL |
| CreatedAt | datetime | Auto timestamp |

### Categories

| Column | Type |
|---|---|
| Id | int |
| CategoryName | varchar(100) |

### AdminUsers

| Column | Type |
|---|---|
| Id | int |
| Username | varchar(100) |
| PasswordHash | text (BCrypt) |

---

## 📡 API Endpoints

### AI Websites

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | `/api/aiwebsites` | Public | Get all websites |
| GET | `/api/aiwebsites?category=AI+Chatbots` | Public | Filter by category |
| GET | `/api/aiwebsites/{id}` | Public | Get by ID |
| POST | `/api/aiwebsites` | Admin JWT | Create |
| PUT | `/api/aiwebsites/{id}` | Admin JWT | Update |
| DELETE | `/api/aiwebsites/{id}` | Admin JWT | Delete |

### Categories

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | `/api/categories` | Public | Get all categories |
| POST | `/api/categories` | Admin JWT | Create category |

### Auth

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/auth/login` | Get JWT token |

**Login request:**
```json
{
  "username": "admin",
  "password": "Admin@1234"
}
```

---

## 🔑 Admin Panel

Visit `http://localhost:5002`

- **Login:** admin / Admin@1234
- **Dashboard:** Stats overview (total tools, free/paid split, categories)
- **AI Websites:** Full CRUD — Add, Edit, Delete, Upload Logo URL, Filter by category
- **Categories:** Add new categories

---

## 💰 Pricing Display

- Tools with `IsFree = true` show **FREE** tag
- Paid tools display **₹{price}** (e.g. ₹1,660)
- Click any tool card → opens the original website in a new tab

---

## 🔐 Security Notes

- Admin passwords are hashed with **BCrypt** (never stored as plain text)
- API uses **JWT Bearer** tokens (8-hour expiry)
- Admin panel uses **cookie authentication** with sliding expiration
- Change the JWT secret in `appsettings.json` before production deployment

---

## 🔄 Apply Migrations (EF Core)

The app uses `EnsureCreated()` to auto-create tables. For production migrations:

```bash
# From project root
cd src/ZamAiMart.API

dotnet ef migrations add InitialCreate --project ../ZamAiMart.Infrastructure
dotnet ef database update
```

---

## 🐳 Docker (Optional — API only)

```bash
cd src/ZamAiMart.API
docker build -t zam-api .
docker run -p 5000:5000 -e "ConnectionStrings__MySqlConnection=..." zam-api
```

---

*Developed by Ahmad Faraz Siddiqui*
