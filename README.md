## Step 1: Clone Repository
git clone https://github.com/kumarangp/ProductManagement

## Step 2:
cd ProductManagement

dotnet restore

## Modify appsettings.json and apply migration
Please modifty server name in appsettings.json(web api, mvc project)

dotnet ef database update --project src/ProductManagement.Infrastructure

## Run web api project
dotnet run --project src/ProductManagement.API

Use **ProductManagement.API.http** file to test api 

## Run mvc project
dotnet run --project src/ProductManagement.Web
