dotnet restore
dotnet test UserManager.AppService.Test/UserManager.AppService.Test.csproj --filter Category=Unit -v q
dotnet test UserManager.AppService.Test/UserManager.AppService.Test.csproj --filter Category=Integration -v q
dotnet test UserManager.AppService.Test/UserManager.AppService.Test.csproj --filter Category=E2E -v q
dotnet run --project UserManager.Startup/UserManager.Startup.csproj
