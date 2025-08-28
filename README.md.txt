# Real Estate Search Portal — Quick Start

.NET 8 Web API (EF Core + SQL Server) with React (Vite + MUI).

## Set these values
Edit `backend/src/WebApi/appsettings.Development.json`:
- ConnectionStrings.Default (choose one)
   - SQL Server: `Data Source=.;Initial Catalog=RealEstate;User ID=.;Password=YourPassword;MultipleActiveResultSets=True;TrustServerCertificate=True`
- Jwt.Key: set to a long random string

Optional: Edit `frontend/.env` if needed
- `VITE_API_URL=https://localhost:7112`

## Run these commands (PowerShell)
Backend
```powershell
cd backend
dotnet tool restore
dotnet build .\RealEstate.sln
dotnet dotnet-ef database update --project .\src\Infrastructure\Infrastructure.csproj --startup-project .\src\WebApi\WebApi.csproj
dotnet dev-certs https --trust
dotnet run --project .\src\WebApi\WebApi.csproj --launch-profile https
```

Frontend (new window)
```powershell
cd frontend
npm install --no-fund --no-audit
npm run dev
```

## Open
- API Swagger: https://localhost:7112/swagger
- App: http://localhost:5173

Login/Register first. Use the Bearer token for protected endpoints.
