using System;
using System.Linq;
using ITDB.Models;

namespace ITDB.Tool
{
    public static class DbInitializer
    {
        public static void Initialize(DBContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }
            var Categories = new Category[]
            {
                new Category{ Logo="",BannerLogo="",Name="科技",Sort=0 },
                new Category{ Logo="",BannerLogo="",Name="生活",Sort=1 }
            };
            foreach (Category s in Categories)
            {
                context.Categories.Add(s);
            }
            context.SaveChanges();
        }
    }
}
