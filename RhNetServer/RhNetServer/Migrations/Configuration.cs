namespace RhNetServer.Migrations
{
    using Microsoft.AspNet.Identity.Owin;
    using RhNetServer.App_Start;
    using RhNetServer.Entities.Adm;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<RhNetServer.Contexts.RhNetContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RhNetServer.Contexts.RhNetContext context)
        {
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
