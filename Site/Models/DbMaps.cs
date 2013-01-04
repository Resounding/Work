using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class WorkOrderMap : EntityTypeConfiguration<WorkOrder>
    {
        public WorkOrderMap()
        {
            Property(w => w.Description).IsMaxLength();
            HasOptional(w => w.Category).WithMany(c => c.WorkOrders).HasForeignKey(w => w.CategoryId);
        }
    }
}