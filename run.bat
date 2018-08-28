dotnet restore
dotnet ef database update --startup-project UserManager.Startup/UserManager.Startup.csproj --project UserManager.Dal/UserManager.Dal.csproj
dotnet test UserManager.AppService.Test/UserManager.AppService.Test.csproj
dotnet run --project UserManager.Startup/UserManager.Startup.csproj
