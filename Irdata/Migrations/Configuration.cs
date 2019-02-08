namespace Irdata.Migrations
{
    using Irdata.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Irdata.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed=true;
        }

        protected override void Seed(Irdata.Models.ApplicationDbContext context)
        {
            context.Accounts.AddOrUpdate(account => account.Name,
                            new Account { Name = "Local" },
                            new Account { Name = "Admin" });
        }
    }
}
