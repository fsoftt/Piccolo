# Piccolo

## Migrations

### Prerequisites
dotnet tool install --global dotnet-ef

### Add migrations
dotnet ef migrations add initialCreate --project src/Infrastructure --startup-project src/API

### Execute migrations
dotnet ef database update --project src/Infrastructure --startup-project src/API