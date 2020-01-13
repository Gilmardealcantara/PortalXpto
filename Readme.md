# Portal XPTO

O portal XPTO é uma aplicação genérica que serve de “vitrine” de diversas outras aplicações.

O projeto consiste em uma arquitetura de microserviços e microfront-ends.

Aqui temos 5 projetos em um único repositório para uso de prova de consceito.
Idealmente os projetos podem ter arquiteturas e usuar tecnologias direntes, desde que tenham o mesmo
padrão de desing.

O projeto FrontEndXptoPortal é cliente do portal, onde os usuários tem acesso.
Basicamente uma tela de login e um grid de iframes com os links para as aplicações App1, App2 e App3.

Os Apps estão armazenados no banco de dados do ApiXptoPortal e para serem acessados depentem de um token que é gerado quando o usuário loga no portal XPTO.

A autenticação é feita via token JWT e deve ser entendida por todos o serviçõs que serão acessados através do portal.

## Tecnologias

#### FrontEndXptoPortal

- React
- Redux
- Redux Saga
- React Hooks
- Material-UI
- React-Router-Dom
- Typescript
- Prettier e EsLint

#### ApXptoPortal

- dotnet core 2.2.106
- Ef Core Migrations
- Unit/Integration Test with xUnit
- Swagger
- Repository Pattern
- IdentityContext
- JWT Auth

#### App1, App2, App3

- Donet Core
- React
- Material-UI

## Run Projects

Run App1, App2, App3

```
cd App*/ClientApp/
yarn install
cd ..
dotnet restore
dotnet run
```

ApiXptoPortal

```
dotnet restore
dotnet test
# check Project Readme to run migration in mysql database
dotnet run --project src/Api
```

FrontEndXptoPortal

```
yarn install
yarn start
```
