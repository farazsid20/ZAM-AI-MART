# ZAM AI MART - Start Both API and Client
Write-Host "Starting ZAM AI MART..." -ForegroundColor Magenta
Write-Host ""

# Start API in background
Write-Host "[1/2] Starting API Server on http://localhost:5000..." -ForegroundColor Cyan
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$PSScriptRoot\src\ZamAiMart.API'; dotnet run --urls 'http://localhost:5000'"

Start-Sleep -Seconds 3

# Start Client
Write-Host "[2/2] Starting Blazor Client on http://localhost:5001..." -ForegroundColor Green
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$PSScriptRoot\src\ZamAiMart.Client'; dotnet run --urls 'http://localhost:5001'"

Write-Host ""
Write-Host "ZAM AI MART is starting!" -ForegroundColor Magenta
Write-Host "API:    http://localhost:5000" -ForegroundColor Cyan
Write-Host "Client: http://localhost:5001" -ForegroundColor Green
