# FrontEndXptoPortal

### Run

```
cd src/Api
dotnet run
dotnet watch run
 # or
dotnet run --project src/Api/Api.csproj
dotnet watch --project src/Api/Api.csproj  run
```

### Migrate

```
# create a mysql database - you can use docker-compose file in docker folder
export CONNECTION_STRING="Server=localhost;Database=xptoPortal;Uid=root;Pwd=123456;"
cd src/Api

dotnet ef migrations --project ../EFMigrate add InitialMigrate
dotnet ef migrations script
dotnet ef database update

```

# Revert migrate

```
dotnet ef database update <previous-migration-name> or 0
dotnet ef migrations  --project ../EFMigrate remove
```

### Tests

Options

```
dotnet test # run all
dotnet test --filter <Method>
dotnet test --filter Category=<CategoryName> # light tests
dotnet test --filter DisplayName~<DisplayName>
dotnet test --filter FullyQualifiedName~XptoPortalApi.Tests.Unit.Repository.MyModelsUnitTest

```

#### Run Unit Tests

```
cd tests/Unit
dotnet test
```

#### Run Integration Tests

```
cd tests/Integration/
dotnet user-secrets set "Key" '<Value>' # if need
dotnet user-secrets list
dotnet test # for all
```

### Build

```
dotnet restore
dotnet build
dotnet publish ./XptoPortalApi.sln -c Release -o ../build
```

### Features

- dotnet 2.2.106

  - Provide data in JSON and XML
  - Dependency injection
  - FCA Security integration
  - Nuget Template for local install
  - Implementes Unit and Integration tests with [xunit](https://xunit.net/)
  - Generic Programing
  - Provider Gzip Compression for https Responses
  - [Memory cache](https://medium.com/@renato.groffe/asp-net-core-2-0-implementando-cache-em-apis-rest-cd2df219f13b)
  - default JWT auth

- Tets
  - [fake data with Bogus](https://github.com/bchavez/Bogus)
  - [Mock objects woth Moq](https://github.com/Moq/moq4/wiki/Quickstart)
  - [fluent FluentValidation](https://fluentvalidation.net/)
