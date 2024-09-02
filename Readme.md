# .NET Core API - WebApi CleanArchitecture

## Description
- .Net 8.0
- PostgreSQL
- Entity Framework Core
- Swagger
- MediatR
- FluentValidation
- Serilog
- Docker

## appsettings.json
```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
        "DefaultConnection": "",
        "PostgresqlConnection": "Host=localhost;Port=5432;Database=xxx_db;Username=xxxuser;Password=xxxxxx_password;"
    },
    "TokenKey": "",
    "ValidIssuer": "",
    "ValidAudience": ""
}
```

## Run
```bash
> cd Docker

> docker-compose up
```

## Migration
```
dotnet ef database update -s Presentation\CleanArchitecture.WebAPI -p Infrastructure\CleanArchitecture.Persistence
```

