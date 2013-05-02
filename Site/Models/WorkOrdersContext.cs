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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new WorkOrderMap());
            modelBuilder.Configurations.Add(new WorkOrderLogMap());
        }

        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderLog> WorkOrderLogs { get; set; }
        public DbSet<WorkOrderCategory> WorkOrderCategories { get; set; }
        public DbSet<Crew> Crews { get; set; }

        public void Reset()
        {
            var configuration = new Configuration();            
            configuration.Reset(this);
        }
    }
}