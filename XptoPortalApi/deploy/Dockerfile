FROM microsoft/dotnet:2.2-sdk AS build
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime

WORKDIR /app
COPY ./build .

# For Oracle DB
ENV TZ=America/Sao_Paulo

EXPOSE APPPORT

ENTRYPOINT ["dotnet", "XptoPortalApi.Api.dll"]
