# ZAM AI MART - Start Blazor Client
Write-Host "Starting ZAM AI MART Blazor Client..." -ForegroundColor Cyan
Write-Host "Make sure the API is running at http://localhost:5000" -ForegroundColor Yellow
Write-Host ""
Set-Location "src/ZamAiMart.Client"
dotnet run --urls "http://localhost:5001"
