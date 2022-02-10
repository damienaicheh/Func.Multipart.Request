# Unit test multipart requests in your C# Azure Functions

Sample source code for the following blog post :

##### English version :
[https://damienaicheh.github.io/azure/azure-functions/dotnet/unit-tests/2022/02/10/unit-test-multipart-request-azure-function-en](https://damienaicheh.github.io/azure/azure-functions/dotnet/unit-tests/2022/02/10/unit-test-multipart-request-azure-function-en)

##### French version :
[https://damienaicheh.github.io/azure/azure-functions/dotnet/unit-tests/2022/02/10/unit-test-multipart-request-azure-function-fr](https://damienaicheh.github.io/azure/azure-functions/dotnet/unit-tests/2022/02/10/unit-test-multipart-request-azure-function-fr)


## Run Unit Tests:

`dotnet test tests/Func.SendFile.Tests/Func.SendFile.Tests.csproj --configuration Release  --collect "Code Coverage" --logger trx`
