using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SimbaToursEastAfrica.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SimbaToursEastAfricaDbContext>
    {
        public SimbaToursEastAfricaDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<SimbaToursEastAfricaDbContext>();
            var connectionString = configuration.GetConnectionString("SimbaToursEastAfrica");
            builder.UseSqlServer(connectionString);
            return new SimbaToursEastAfricaDbContext(builder.Options);
        }
    }

    public class DesignTimeIdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<IdentityDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new IdentityDbContext(builder.Options);
        }
    }
}
