using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OdataAutomapperServerQuery.Models;

namespace OdataAutomapperServerQuery.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MyDbContext(serviceProvider.GetRequiredService<DbContextOptions<MyDbContext>>());

            if (!context.Database.EnsureCreated())
            {
                context.Database.Migrate();
            }

            if (context.ProjectReport.Any())
            {
                return;   // DB has been seeded
            }

            context.ProjectReport.AddRange(
                new ProjectReport
                {
                    OptionNo = 1001,
                    CreatedBy = "user1"
                },
                new ProjectReport
                {
                    OptionNo = 1002,
                    CreatedBy = "user1"
                },
                new ProjectReport
                {
                    OptionNo = 1003,
                    CreatedBy = "user1"
                },
                new ProjectReport
                {
                    OptionNo = 1004,
                    CreatedBy = "user1"
                }
            );
            context.SaveChanges();
        }
    }
}
