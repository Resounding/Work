namespace Site.Migrations
{
    using Site.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Site.Models.WorkOrderContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Site.Models.WorkOrderContext context)
        {
            if (context.WorkOrderCategories.Count() == 0) {

                var installation = new WorkOrderCategory { Id = Guid.NewGuid(), Name = "Installation", CssClass = "" };
                var maintenance = new WorkOrderCategory { Id = Guid.NewGuid(), Name = "Maintenance", CssClass = "" };
                context.WorkOrderCategories.AddOrUpdate(installation, maintenance);


                if (context.Crews.Count() == 0) {
                    var jim = new Crew { Id = Guid.NewGuid(), Name = "Jim Robertson" };
                    var sam = new Crew { Id = Guid.NewGuid(), Name = "Sam Philips" };
                    var terry = new Crew { Id = Guid.NewGuid(), Name = "Terry Johnson" };
                    context.Crews.AddOrUpdate(jim, sam, terry);


                    if (context.WorkOrders.Count() == 0) {
                        var workOrder1 = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = jim.Id,
                            Crew = jim,
                            CategoryId = installation.Id,
                            Category = installation,
                            Customer = "Mr. & Mrs. Ellington",
                            Date = new DateTime(2013, 1, 7, 10, 0, 0),
                            Description = "The Ellingtons live in an older home with a fuse panel & would like it upgraded to breakers. They also have some aluminum wiring that should be replaced.",
                        };
                        context.WorkOrders.AddOrUpdate(workOrder1);

                        var workOrder2 = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = sam.Id,
                            Crew = sam,
                            CategoryId = maintenance.Id,
                            Category = maintenance,
                            Customer = "Mr. Samuelson",
                            Date = new DateTime(2013, 1, 7, 8, 0, 0),
                            Description = "Mr. Samuelson has a leaky faucet that needs replacing. It's a Moen that we put in a number of years ago, so it's under warranty.",
                        };
                        context.WorkOrders.AddOrUpdate(workOrder2);

                        var workOrder3 = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = sam.Id,
                            Crew = sam,
                            CategoryId = maintenance.Id,
                            Category = maintenance,
                            Customer = "Jeff Adams",
                            Date = new DateTime(2013, 1, 7, 10, 0, 0),
                            Description = "Jeff needs a new tap installed in his bathroom. He's purchased the unit and will leave it on the counter. He will be at work, but he'll leave the back door unlocked.",
                        };
                        context.WorkOrders.AddOrUpdate(workOrder3);
                    }
                }
            }
        }
    }
}
