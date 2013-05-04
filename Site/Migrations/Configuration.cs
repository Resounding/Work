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

        public void Reset(WorkOrderContext context)
        {
            context.Database.ExecuteSqlCommand("Delete dbo.WorkOrderLogs");
            context.Database.ExecuteSqlCommand("Delete dbo.WorkOrders");
            context.Database.ExecuteSqlCommand("Delete dbo.WorkOrderCategories");
            context.Database.ExecuteSqlCommand("Delete dbo.Crews");
            
            Seed(context);

            context.SaveChanges();
        }

        protected override void Seed(WorkOrderContext context)
        {
            if (context.WorkOrderCategories.Count() == 0) {

                var construction = new WorkOrderCategory { Id = Guid.NewGuid(), Name = "Construction", CssClass = "" };
                var maintenance = new WorkOrderCategory { Id = Guid.NewGuid(), Name = "Maintenance", CssClass = "" };
                context.WorkOrderCategories.AddOrUpdate(construction, maintenance);

                if (context.Crews.Count() == 0) {
                    var jim = new Crew { Id = Guid.NewGuid(), Name = "Jim Robertson" };
                    var sam = new Crew { Id = Guid.NewGuid(), Name = "Sam Philips" };
                    var terry = new Crew { Id = Guid.NewGuid(), Name = "Terry Johnson" };
                    var pete = new Crew { Id = Guid.NewGuid(), Name = "Pete Smith" };
                    context.Crews.AddOrUpdate(jim, sam, terry, pete);

                    if (context.WorkOrders.Count() == 0) {

                        var wo = default(WorkOrder);

                        var monday = DateTime.Today;
                        while (monday.DayOfWeek == DayOfWeek.Saturday || monday.DayOfWeek == DayOfWeek.Sunday) {
                            monday = monday.AddDays(1);
                        }
                        while (monday.DayOfWeek != DayOfWeek.Monday) {
                            monday = monday.AddDays(-1);
                        }

                        // New Deck: 5 day job
                        for (var i = 0; i < 5; i++) {
                            wo = new WorkOrder {
                                Id = Guid.NewGuid(),
                                CrewId = jim.Id,
                                Crew = jim,
                                CategoryId = construction.Id,
                                Category = construction,
                                Customer = "Mr. & Mrs. Ellington",
                                Date = monday.AddDays(i),
                                Description = "The Ellingtons would like to add a deck in the back, out the dining room patio doors. " +
                                    "Currently, there's a patio there, but it's in bad shape. They want to go with Trexx decking.",
                                Duration = "5d"
                            };
                            context.WorkOrders.AddOrUpdate(wo);
                        }

                        // Maintenance Jobs
                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = sam.Id,
                            Crew = sam,
                            CategoryId = maintenance.Id,
                            Category = maintenance,
                            Customer = "John Jones",
                            Date = monday,
                            Description = "John needs a new faucet in the kitchen. Counter may need some reinforcement as well, as the " +
                                "old one has been leaking, so there's some water damage.",
                            Duration = "4h"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = sam.Id,
                            Crew = sam,
                            CategoryId = maintenance.Id,
                            Category = maintenance,
                            Customer = "Frank & Gladys Jackson",
                            Date = monday,
                            Description = "The Jacksons have leaky set of taps in their basement laundry tub. Could be just a new washer, but may " +
                                "need a new set of taps.",
                            Duration = "4h"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = sam.Id,
                            Crew = sam,
                            CategoryId = maintenance.Id,
                            Category = maintenance,
                            Customer = "Sally Smith",
                            Date = monday.AddDays(1),
                            Description = "Miss Smith has a wooden front door that she'd like replaced with an insulated steel door. Would like the new door to be red.",
                            Duration = "1d"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = sam.Id,
                            Crew = sam,
                            CategoryId = maintenance.Id,
                            Category = maintenance,
                            Customer = "Mr. & Mrs. McArthur",
                            Date = monday.AddDays(2),
                            Description = "The McArthurs have a broken spindle on their stairway leading upstairs. Off-the-shelf pattern, so can get a new one at Home Depot.",
                            Duration = "2h"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = sam.Id,
                            Crew = sam,
                            CategoryId = maintenance.Id,
                            Category = maintenance,
                            Customer = "Mrs. Brown",
                            Date = monday.AddDays(2),
                            Description = "Mrs. Brown has a cloggged tub. Could be that it just needs Draino, but might need a power snake to clean it out.",
                            Duration = "2h"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = sam.Id,
                            Crew = sam,
                            CategoryId = maintenance.Id,
                            Category = maintenance,
                            Customer = "Mr. & Mrs. MacGillicuddy",
                            Date = monday.AddDays(2),
                            Description = "Garage door is stuck. Motor is likely OK - just came off the rail. May need new cable.",
                            Duration = "2h"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = sam.Id,
                            Crew = sam,
                            CategoryId = maintenance.Id,
                            Category = maintenance,
                            Customer = "Patrick O'Reilly",
                            Date = monday.AddDays(2),
                            Description = "Patrick would like a new electrical outlet in his den. Junction box nearby.",
                            Duration = "2h"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = sam.Id,
                            Crew = sam,
                            CategoryId = construction.Id,
                            Category = construction,
                            Customer = "Pete & Jane Tripp",
                            Date = monday.AddDays(3),
                            Description = "The Tripps would like some new light fixtures - one in the Dining Room and then a matching one in the kitchen. " +
                                "These will need to be wired in, and secured in the ceiling.",
                            Duration = "1d"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = sam.Id,
                            Crew = sam,
                            CategoryId = maintenance.Id,
                            Category = maintenance,
                            Customer = "Tom & Sally Peters",
                            Date = monday.AddDays(4),
                            Description = "Stair is broken on the front step & needs to be replaced.",
                            Duration = "4h"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = sam.Id,
                            Crew = sam,
                            CategoryId = maintenance.Id,
                            Category = maintenance,
                            Customer = "Mr. & Mrs. Thompson",
                            Date = monday.AddDays(4),
                            Description = "There are several sections of deck railing that need to be replaced. The deck itself is pretty solid, but " +
                                "the railings are getting rotten. Patch up what you can & replace what you have to.",
                            Duration = "4h"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        // Terry Jobs
                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = terry.Id,
                            Crew = terry,
                            CategoryId = construction.Id,
                            Category = construction,
                            Customer = "Al Bennings",
                            Date = monday,
                            Description = "Al lives in an older home with a fuse panel & would like it upgraded to breakers. He also has some aluminum wiring that should be replaced.",
                            Duration = "2d"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = terry.Id,
                            Crew = terry,
                            CategoryId = construction.Id,
                            Category = construction,
                            Customer = "Al Bennings",
                            Date = monday.AddDays(1),
                            Description = "Al lives in an older home with a fuse panel & would like it upgraded to breakers. He also has some aluminum wiring that should be replaced.",
                            Duration = "2d"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = terry.Id,
                            Crew = terry,
                            CategoryId = construction.Id,
                            Category = construction,
                            Customer = "Steve Alberts",
                            Date = monday.AddDays(2),
                            Description = "Steve is building an addition, and needs the wiring done. He is doing most of the work himself, but needs us to come in " +
                                "and wire it up for him.",
                            Duration = "2d"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = terry.Id,
                            Crew = terry,
                            CategoryId = construction.Id,
                            Category = construction,
                            Customer = "Steve Alberts",
                            Date = monday.AddDays(3),
                            Description = "Steve is building an addition, and needs the wiring done. He is doing most of the work himself, but needs us to come in " +
                                "and wire it up for him.",
                            Duration = "2d"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = terry.Id,
                            Crew = terry,
                            CategoryId = construction.Id,
                            Category = construction,
                            Customer = "Polly Ebbertson",
                            Date = monday.AddDays(4),
                            Description = "Polly would like some new pot lights in her Living Room. She'll need 12 lights, and it's vaulted ceiling, " +
                                "so we'll need the long ladder.",
                            Duration = "1d"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        // Pete's jobs
                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = pete.Id,
                            Crew = pete,
                            CategoryId = construction.Id,
                            Category = construction,
                            Customer = "Jeremy & Sonya Phillips",
                            Date = monday,
                            Description = "The Phillips need a new roof. Remove the old one (need dumpster), and shingle the new one. They would like " +
                                "brown architectural shingles, which will be delivered on Saturday.",
                            Duration = "3d"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = pete.Id,
                            Crew = pete,
                            CategoryId = construction.Id,
                            Category = construction,
                            Customer = "Jeremy & Sonya Phillips",
                            Date = monday.AddDays(1),
                            Description = "The Phillips need a new roof. Remove the old one (need dumpster), and shingle the new one. They would like " +
                                "brown architectural shingles, which will be delivered on Saturday.",
                            Duration = "3d"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = pete.Id,
                            Crew = pete,
                            CategoryId = construction.Id,
                            Category = construction,
                            Customer = "Jeremy & Sonya Phillips",
                            Date = monday.AddDays(2),
                            Description = "The Phillips need a new roof. Remove the old one (need dumpster), and shingle the new one. They would like " +
                                "brown architectural shingles, which will be delivered on Saturday.",
                            Duration = "3d"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = pete.Id,
                            Crew = pete,
                            CategoryId = maintenance.Id,
                            Category = maintenance,
                            Customer = "Paul Matthewson",
                            Date = monday.AddDays(3),
                            Description = "Paul would like the windows replaced in all the bedrooms in the house.",
                            Duration = "2d"
                        };
                        context.WorkOrders.AddOrUpdate(wo);

                        wo = new WorkOrder {
                            Id = Guid.NewGuid(),
                            CrewId = pete.Id,
                            Crew = pete,
                            CategoryId = construction.Id,
                            Category = construction,
                            Customer = "Paul Matthewson",
                            Date = monday.AddDays(4),
                            Description = "Paul would like the windows replaced in all the bedrooms in the house.",
                            Duration = "2d"
                        };
                        context.WorkOrders.AddOrUpdate(wo);
                    }
                }
            }
        }
    }
}
