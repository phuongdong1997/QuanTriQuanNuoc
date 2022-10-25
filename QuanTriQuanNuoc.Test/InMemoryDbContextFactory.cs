using Microsoft.EntityFrameworkCore;
using QuanTriQuanNuoc.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanTriQuanNuoc.Test
{
    public class InMemoryDbContextFactory
    {
        public ApplicationDbContext GetApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(databaseName: "InMemoryApplicationDatabase")
                       .Options;
            var dbContext = new ApplicationDbContext(options);

            return dbContext;
        }
    }
}