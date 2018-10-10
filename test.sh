# Reset
NC='\033[0m'       # Text Reset

# Regular Colors
Green='\033[0;32m'
Red='\033[0;31m'

echo -e "${Green}Mapper test${NC}"
dotnet test UserManager.AppService.Test/UserManager.AppService.Test.csproj --filter Category=Unit -v q > test.txt
test=$(grep -Eo "tests: [0-9]{1,4}. " test.txt | tr -dc '0-9,\n')
passed=$(grep -Eo "Passed: [0-9]{1,4}. " test.txt | tr -dc '0-9,\n')

if [ $test -eq $passed ]; then
	echo -e "${Green} all $test test(s) passed"
else
	echo -e "${Red} only passed $passed test(s)"
fi

echo -e "${Green}Integration test${NC}"
dotnet test UserManager.AppService.Test/UserManager.AppService.Test.csproj --filter Category=Integration -v q > test.txt
test=$(grep -Eo "tests: [0-9]{1,4}. " test.txt | tr -dc '0-9,\n')
passed=$(grep -Eo "Passed: [0-9]{1,4}. " test.txt | tr -dc '0-9,\n')

if [ $test -eq $passed ]; then
	echo -e "${Green} all $test test(s) passed"
else
	echo -e "${Red} only passed $passed test(s)"
fi

echo -e "${Green}Static End to End test${NC}"
dotnet test UserManager.WebAPI.Test/UserManager.WebAPI.Test.csproj --filter Category=StaticE2E -v q > test.txt
test=$(grep -Eo "tests: [0-9]{1,4}. " test.txt | tr -dc '0-9,\n')
passed=$(grep -Eo "Passed: [0-9]{1,4}. " test.txt | tr -dc '0-9,\n')

if [ $test -eq $passed ]; then
	echo -e "${Green} all $test test(s) passed"
else
	echo -e "${Red} only passed $passed test(s)"
fi

echo -e "${Green}End to End test${NC}"
dotnet test UserManager.WebAPI.Test/UserManager.WebAPI.Test.csproj --filter Category=UserE2E -v q > test.txt
test=$(grep -Eo "tests: [0-9]{1,4}. " test.txt | tr -dc '0-9,\n')
passed=$(grep -Eo "Passed: [0-9]{1,4}. " test.txt | tr -dc '0-9,\n')

if [ $test -eq $passed ]; then
	echo -e "${Green} all $test test(s) passed"
else
	echo -e "${Red} only passed $passed test(s)"
fi