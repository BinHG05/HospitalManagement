# Hospital Management System

## 🚀 Quick Start - Hướng dẫn cài đặt

### Yêu cầu hệ thống
- Visual Studio 2019/2022
- .NET Framework 4.7.2
- Docker Desktop

### Bước 1: Clone dự án
```bash
git clone https://github.com/YOUR_USERNAME/HospitalManagement.git
cd HospitalManagement
```

### Bước 2: Khởi động Database (Docker)
```bash
cd docker
docker-compose up -d
```

Đợi khoảng 30 giây để SQL Server khởi động hoàn toàn.

### Bước 3: Restore Database Schema
Chạy script SQL để tạo cấu trúc database:

**Cách 1: Dùng SSMS (SQL Server Management Studio)**
1. Kết nối đến `localhost,1433` với user `sa`, password `YourStrong@123`
2. Mở và chạy file `docker/init-db.sql`

**Cách 2: Dùng Docker command**
```bash
docker exec -it hospital_sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong@123" -C -i /scripts/init-db.sql
```

### Bước 4: Mở Visual Studio
1. Mở file `HospitalManagement.sln`
2. Build solution (Ctrl+Shift+B)
3. Chạy ứng dụng (F5)

---

## 📁 Cấu trúc dự án (MVP Pattern)

```
HospitalManagement/
├── Config/              # Cấu hình ứng dụng
├── Infrastructure/      # Common utilities (AppTheme, etc.)
├── Logs/               # Logging
├── Models/
│   ├── EF/             # Entity Framework DbContext
│   └── Entities/       # Entity classes
├── Presenters/         # MVP Presenters
├── Repositories/       # Repository pattern
│   ├── Interfaces/
│   └── Implementations/
├── Services/           # Business logic services
│   ├── Interfaces/
│   └── Implementations/
├── Session/            # User session management
└── Views/
    ├── Forms/          # WinForms (Patient, Doctor, Admin)
    ├── Interfaces/     # View interfaces
    └── UserControls/   # Reusable controls
```

---

## 🔧 Cấu hình Database

**Connection String** (App.config):
```
Server=localhost,1433;Database=HospitalManagement;User Id=sa;Password=YourStrong@123;TrustServerCertificate=True
```

**Docker Container:**
- Port: `1433`
- User: `sa`
- Password: `YourStrong@123`
- Database: `HospitalManagement`

---

## 👥 Tài khoản test

| Username | Password | Role |
|----------|----------|------|
| admin | admin123 | admin |
| doctor1 | doctor123 | doctor |
| patient1 | patient123 | patient |

---

## 🔄 Regenerate EF Models (nếu cần)

Nếu database schema thay đổi, chạy lệnh sau trong Package Manager Console:

```powershell
Scaffold-DbContext "Server=localhost,1433;Database=HospitalManagement;User Id=sa;Password=YourStrong@123;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/Entities -Context HospitalDbContext -ContextDir Models/EF -DataAnnotations -UseDatabaseNames -Force
```
