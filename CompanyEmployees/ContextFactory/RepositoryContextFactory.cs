using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace CompanyEmployees.ContextFactory
{
	public class RepositoryContextFactory: IDesignTimeDbContextFactory<RepositoryContext>
	{
		public RepositoryContextFactory()
		{
		}

        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            string dbConnectionString = configuration.GetConnectionString("sqlConnection");

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString),
                b => b.MigrationsAssembly("CompanyEmployees"));

            return new RepositoryContext(builder.Options);
        }
    }
}

