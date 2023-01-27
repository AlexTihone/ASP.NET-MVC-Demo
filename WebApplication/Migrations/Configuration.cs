namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication.Models.BookDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication.Models.BookDBContext context)
        {
            context.Books.AddOrUpdate(i => i.Name,
                new Book { Id = "9b0896fa-3880-4c2e-bfd6-925c87f22878", Name = "CQRS for Dummies", ValidStatus = true },
                new Book { Id = "0550818d-36ad-4a8d-9c3a-a715bf15de76", Name = "Visual Studio Tips", ValidStatus = true },
                new Book { Id = "8e0f11f1-be5c-4dbc-8012-c19ce8cbe8e1", Name = "NHibernate Cookbook", ValidStatus = true }
            );

        }
    }
}
