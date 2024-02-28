using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryConsoleApp
{
    public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            // konfiguracijos uskelimas is appsettings.josn
            IConfigurationRoot confoguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            //connection string pasiemimas
            string connectionString = confoguration.GetConnectionString("DefaultConnection");
            var builder = new DbContextOptionsBuilder<LibraryDbContext>();
            builder.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 28)));
            return new LibraryDbContext(builder.Options);
        }
    }
}
