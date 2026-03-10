# ZAM AI MART - Start API Server
Write-Host "Starting ZAM AI MART API Server..." -ForegroundColor Cyan
Write-Host "API will run at: http://localhost:5000" -ForegroundColor Green
Write-Host "Make sure PostgreSQL is running and connection string is configured in appsettings.json" -ForegroundColor Yellow
Write-Host ""
Set-Location "src/ZamAiMart.API"
dotnet run --urls "http://localhost:5000"
