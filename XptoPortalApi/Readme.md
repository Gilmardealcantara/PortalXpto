# Dotnet core 2.2 Boilerplate - WepApi Projects

### Dotnet Template

Install

```
dotnet new -i .
```

Uninstall

```
dotnet new -u # see all projects PATHs
dotnet new -u <PATH-PROJECT>
```

Use

```
ls
# The first character must be Uppercase or all characters must be Lowercase
dotnet new dotnetapi -n <PROJECT_NAME> -db <CONNECTION_STRING>
cd <PROJECT_NAME>
dotnet test
rm -f .git/index
git reset
git add .
git commit -m "Init XptoPortalApi project"
git remote rm origin
git remote add origin <REPO_URL>
git push -u origin
dotnet restore
dotnet test
```

### GIT

Remove last Commit

```
git reset HEAD^ --hard
git push origin -f
```

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
export CONNECTION_STRING="Server=9.6.48.179;Database=boilerplatedotnet;Uid=gilmar;Pwd=123456;"
# win
set CONNECTION_STRING="Server=9.6.48.179;Database=XptoPortalApi;Uid=gilmar;Pwd=123456;"
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

## config migrate

```
https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects
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
dotnet user-secrets set "USER_LOGIN" '<USER_LOGIN>'
dotnet user-secrets set "USER_PWD" '<USER_PWD>'
dotnet user-secrets list
dotnet test # for all
```

### Build

```
dotnet restore
dotnet build
dotnet publish ./XptoPortalApi.sln -c Release -o ../build
```

### Docker

```
# run Build Before
docker build -t XptoPortalApi:v01 .
# docker run -it -p 80:80 XptoPortalApi:v01
docker tag XptoPortalApi:v01 9.6.48.179:5000/XptoPortalApi
docker push 9.6.48.179:5000/XptoPortalApi
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

### TODO

- Fix dependents tests
- add UserApi variable in deploy

- Base Repository pattern

  - Base Repository as Abstract ok
  - Create MyModelRepository ok
  - Generic server side paginate in repository ok
  - Create GetBy like where ok
  - atualizer BaseEntity ok
  - Criar tests de unidade para o repositório ok
  - usar moq no servico e controler ok

- Add FluentValidation ok

- Tests com F#
- User Polly to inprove resilience and try fail
- add tool httprepl
- Base path for swagger
- Swagger security
- Helth Checks

- Update Account Branch
- Implements DDD
- Add Inteface In Domain Session
- Add Session of Control Inversion
- Implements Create and Update Tracking with https://www.meziantou.net/entity-framework-core-generate-tracking-columns.htm

* see - https://www.youtube.com/watch?v=QBdiNZl3pZ8

## continuos integrations

- Runs this steps
- Set envivariable
  APP_NAME: Name of app, image, deployment, services and ingress
  REGISTRY: Addres for you registry
- Run 'buildAndPushApp.sh' script
- Apply k8s yaml

example:

```
export APP_NAME="boilerplate-dotnet"
export REGISTRY="9.6.48.179:5000"

./deploy/buildAndPushApp.sh
kubectl apply -f ./deploy/deployment.yaml
kubectl apply -f ./deploy/service.yaml
kubectl apply -f ./deploy/ingress.yaml
kubectl -n portal-rh get all
```

### test Coverage

```
$ dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:Exclude="[xunit.*]*%2c[XptoPortalApi.EFMigrate*]*"
$ dotnet tool install dotnet-reportgenerator-globaltool --tool-path tools
# Unit
$ ./tools/reportgenerator -reports:./tests/Unit/coverage.cobertura.xml -targetdir:./tests/Unit/CodeCoverage/ -reporttypes:"HtmlInline_AzurePipelines;Cobertura"
$ open ./tests/Unit/CodeCoverage/index.htm
# Integration
$ ./tools/reportgenerator -reports:./tests/Integration/coverage.cobertura.xml -targetdir:./tests/Integration/CodeCoverage/ -reporttypes:"HtmlInline_AzurePipelines;Cobertura"
$ open ./tests/Integration/CodeCoverage/index.htm
```
