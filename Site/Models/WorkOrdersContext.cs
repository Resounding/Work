using Site.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class WorkOrderContext : DbContext
    {
        static WorkOrderContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WorkOrderContext, Configuration>());
        }

        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderLog> WorkOrderLogs { get; set; }
        public DbSet<WorkOrderCategory> WorkOrderCategories { get; set; }
        public DbSet<Crew> Crews { get; set; }
    }
}