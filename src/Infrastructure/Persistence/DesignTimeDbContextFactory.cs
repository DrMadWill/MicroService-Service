using Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Context;
using AppContext = Persistence.Context.AppContext;

namespace Persistence;

public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<AppContext>
{
    public AppContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<AppContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
        return new(dbContextOptionsBuilder.Options);
    }
}