# ZAM AI MART 🤖

> **Discover the Best AI Tools in One Place**
>
> Developed by **Ahmad Faraz Siddiqui**

A production-ready AI tools directory website built with Blazor WebAssembly, ASP.NET Core Web API, and PostgreSQL.

---

## 🚀 Tech Stack

| Layer | Technology |
|-------|-----------|
| Frontend | Blazor WebAssembly (.NET 9) |
| Styling | Tailwind CSS |
| Backend | ASP.NET Core Web API (.NET 9) |
| Database | PostgreSQL + Entity Framework Core 9 |
| Architecture | Clean Architecture |

---

## 📁 Project Structure

```
ZAM AI MART/
├── ZamAiMart.sln
└── src/
    ├── ZamAiMart.Core/              # Domain - Models, DTOs, Interfaces
    │   ├── Entities/
    │   │   ├── AITool.cs
    │   │   └── Category.cs
    │   ├── DTOs/
    │   │   ├── AIToolDto.cs
    │   │   └── CategoryDto.cs
    │   ├── Interfaces/
    │   │   ├── IAIToolRepository.cs
    │   │   ├── ICategoryRepository.cs
    │   │   ├── IAIToolService.cs
    │   │   └── ICategoryService.cs
    │   └── Enums/
    │       └── PricingType.cs
    │
    ├── ZamAiMart.Infrastructure/    # Data Access - EF Core, Repositories, Services
    │   ├── Data/
    │   │   ├── AppDbContext.cs
    │   │   └── SeedData.cs          # 200+ AI tools!
    │   ├── Repositories/
    │   │   ├── AIToolRepository.cs
    │   │   └── CategoryRepository.cs
    │   ├── Services/
    │   │   ├── AIToolService.cs
    │   │   └── CategoryService.cs
    │   └── DependencyInjection.cs
    │
    ├── ZamAiMart.API/               # ASP.NET Core Web API
    │   ├── Controllers/
    │   │   ├── CategoriesController.cs
    │   │   └── ToolsController.cs
    │   ├── Program.cs
    │   └── appsettings.json
    │
    └── ZamAiMart.Client/            # Blazor WebAssembly Frontend
        ├── Layout/
        │   ├── MainLayout.razor
        │   ├── NavMenu.razor
        │   └── Footer.razor
        ├── Pages/
        │   ├── Home.razor           # Hero, Featured, Categories, Latest
        │   ├── Tools.razor          # Tools directory with search & filters
        │   ├── Categories.razor     # Browse by category
        │   ├── About.razor          # About the platform
        │   ├── Contact.razor        # Contact info + form
        │   └── Admin/
        │       ├── Dashboard.razor  # Admin overview
        │       ├── ManageTools.razor # Add/Edit/Delete tools
        │       └── ManageCategories.razor
        ├── Shared/
        │   ├── ToolCard.razor       # Reusable tool card component
        │   ├── CategoryCard.razor   # Reusable category card
        │   └── LoadingSpinner.razor
        ├── Services/
        │   └── ApiService.cs        # HTTP client for API calls
        └── wwwroot/
            ├── index.html           # Tailwind CSS + dark mode setup
            └── css/app.css
```

---

## ⚙️ Setup & Installation

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL 14+](https://www.postgresql.org/download/)
- Node.js (optional, for Tailwind CLI in production)

### 1. Configure Database

Edit `src/ZamAiMart.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=ZamAiMart;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

### 2. Run the API

```powershell
cd src/ZamAiMart.API
dotnet run
```

API runs at `http://localhost:5000`

> **Note:** The database is automatically created and seeded with 200+ AI tools on first run.

### 3. Run the Blazor Client

```powershell
cd src/ZamAiMart.Client
dotnet run
```

Client runs at `http://localhost:5001` (or the port shown in terminal)

> Make sure the API URL in `Program.cs` matches your API port.

---

## 🌐 API Endpoints

### Categories
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/categories` | Get all categories |
| GET | `/api/categories/{id}` | Get category by ID |
| POST | `/api/categories` | Create category |
| PUT | `/api/categories/{id}` | Update category |
| DELETE | `/api/categories/{id}` | Delete category |

### AI Tools
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/tools` | Get all tools (paginated, searchable) |
| GET | `/api/tools/{id}` | Get tool by ID |
| GET | `/api/tools/featured` | Get featured tools |
| GET | `/api/tools/latest` | Get latest tools |
| GET | `/api/tools/category/{categoryId}` | Get tools by category |
| POST | `/api/tools` | Create tool |
| PUT | `/api/tools/{id}` | Update tool |
| DELETE | `/api/tools/{id}` | Delete tool |

### Search Query Parameters
```
GET /api/tools?query=chatgpt&categoryId=1&isFree=true&page=1&pageSize=20
```

---

## 🎨 Features

### Public Site
- **Homepage** - Hero, Featured Tools, Category Grid, Latest Tools
- **AI Tools Directory** - Searchable, filterable grid with pagination
- **Category Browser** - Browse tools by 10 categories
- **Tool Cards** - Logo, name, description, price in ₹, visit button
- **About Page** - Platform info + developer credit
- **Contact Page** - Contact info + form
- **Dark Mode** - Toggle with localStorage persistence
- **Responsive Design** - Mobile-first, works on all devices

### Admin Dashboard
- **Dashboard** - Stats overview + recent tools
- **Manage Tools** - Add, Edit, Delete AI tools
- **Manage Categories** - Add, Edit, Delete categories

### AI Tool Categories
1. 💬 AI Chatbots
2. 🎨 AI Image Generators
3. 🎬 AI Video Generators
4. 💻 AI Coding Tools
5. ✍️ AI Writing Tools
6. 🎙️ AI Voice Tools
7. 🎵 AI Music Generators
8. 📈 AI Marketing Tools
9. ⚡ AI Productivity Tools
10. 🖌️ AI Design Tools

---

## 💰 Pricing Display

All prices are shown in **Indian Rupees (₹)**:
- **FREE** - Free tools
- **₹499/month** - Monthly subscription
- **₹1999/year** - Annual subscription
- **Freemium** - Free + paid tiers

---

## 👨‍💻 Developer

**Ahmad Faraz Siddiqui**  
Full-Stack Developer | AI Enthusiast

📞 +91-9161007123  
📍 Sultanpur, Uttar Pradesh - 228001, India

---

## © License

© 2026 ZAM AI MART. Developed by Ahmad Faraz Siddiqui.
