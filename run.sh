# Reset
NC='\033[0m'       # Text Reset

# Regular Colors
Green='\033[0;32m'        # Green



dotnet restore

echo -e "${Green}Mapper test${NC}"
dotnet test UserManager.AppService.Test/UserManager.AppService.Test.csproj --filter Category=Unit -v q >> test.txt

echo -e "${Green}Integration test${NC}"
dotnet test UserManager.AppService.Test/UserManager.AppService.Test.csproj --filter Category=Integration -v q >> test.txt

echo -e "${Green}Static End to End test${NC}"
dotnet test UserManager.WebAPI.Test/UserManager.WebAPI.Test.csproj --filter Category=StaticE2E -v q >> test.txt

echo -e "${Green}End to End test${NC}"
dotnet test UserManager.WebAPI.Test/UserManager.WebAPI.Test.csproj --filter Category=E2E -v q >> test.txt

echo -e "${Green}Test complete${NC}"
dotnet run --project UserManager.Startup/UserManager.Startup.csproj
