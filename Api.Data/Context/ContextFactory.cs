using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            // Criar as migrations
            // var connectionString = "Server=localhost;Port=3306;Database=Course;Uid=root;Pwd=root";
            // var connectionString = "Server=localhost,1433;Database=dbAPI;User ID=sa;Password=#sql123456";
            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            if (Environment.GetEnvironmentVariable("DATABASE").ToLower() == "SQLSERVER".ToLower())
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
            else
            {
                optionsBuilder.UseMySql(connectionString);
            }
            return new MyContext(optionsBuilder.Options);
        }
    }
}
