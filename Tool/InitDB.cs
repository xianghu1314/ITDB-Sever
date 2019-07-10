using System;
using System.Threading.Tasks;
using ITDB.Models;
namespace ITDB.Tool {
    public class InitDB {
        public async static Task Init (IServiceProvider service) {
            var db = service.GetService<DBContext>();

            if (db.Database != null && db.Database.EnsureCreated ()) {

                // Article article = new Article
                // {
                //     Title = "test",
                //     Description = "SMBlog Test"
                // };

                // db.Articles.Add(article);
                await db.SaveChangesAsync ();
            }
        }
    }
}