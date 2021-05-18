# Calendario applicaton
>Description will be provided later

## Cheatsheet

 - dotnet add package Microsoft.EntityFrameworkCore.InMemory
 - dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
 - dotnet add package Microsoft.EntityFrameworkCore.Design
 - dotnet add package Microsoft.EntityFrameworkCore.SqlServer
 - dotnet tool install -g dotnet-aspnet-codegenerator
 - docker run -d -p 5432:5432 --env-file ./.env postgres
 - dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
 - dotnet ef migrations add -p ./Calendario.Infrastructure/Calendario.Infrastructure.csproj -s ./Calendario.Web/Calendario.Web.csproj Initial

## Requred .env file format
```
POSTGRES_PASSWORD=<password>
POSTGRES_USER=<db_username>
POSTGRES_DB=calendario_db
DB_PORT=5432
DB_HOST=localhost
CALENDARIO_ADMIN_LOGIN=root
CALENDARIO_ADMIN_NAME=<admin displayed name>
CALENDARIO_ADMIN_PASSWORD=<admin password>
```


