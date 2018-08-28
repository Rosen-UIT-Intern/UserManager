# UserManager  
## setup migration  
### Visual Studio  
open solution in Visual Studio  
build the whole solution  
make UserManager.Startup the startup project  
open Package Manager Console  
set Default project to UserManager.Startup  
run ```update-database```  

### dotnet-cli  
```
dotnet restore
cd UserManager.Startup
dotnet ef database update
dotnet run
```
