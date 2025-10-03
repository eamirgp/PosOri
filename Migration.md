cd C:\Users\Amir\source\repos\Pos\Pos.Persistence

dotnet tool update --global dotnet-ef

# Eliminar la base de datos si existe
dotnet ef database drop -f -s ..\Pos.Api\Pos.Api.csproj

# Eliminar la migración
dotnet ef migrations remove -s ..\Pos.Api\Pos.Api.csproj

# Crear nueva migración
dotnet ef migrations add Initial -s ..\Pos.Api\Pos.Api.csproj

# Actualizar la base de datos
dotnet ef database update -s ..\Pos.Api\Pos.Api.csproj