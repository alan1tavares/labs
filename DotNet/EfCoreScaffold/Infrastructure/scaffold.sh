dotnet ef dbcontext scaffold "Host=localhost:5432;Database=scaffold;Username=postgres;Password=1234;" \
    Npgsql.EntityFrameworkCore.PostgreSQL \
    -c ScaffoldContext