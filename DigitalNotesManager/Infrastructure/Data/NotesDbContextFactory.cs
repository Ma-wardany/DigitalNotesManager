using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalNotesManager.Infrastructure.Data
{
    //internal class NotesDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    //{
    //    public DataContext CreateDbContext(string[] args)
    //    {
    //        IConfiguration config = new ConfigurationBuilder()
    //            .SetBasePath(Directory.GetCurrentDirectory())
    //            .AddJsonFile("appsettings.json")
    //            .Build();

    //        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
    //        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

    //        return new DataContext(optionsBuilder.Options);
    //    }
    //}
}
