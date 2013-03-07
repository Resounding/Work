﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class CrewListWorkOrderModel
    {
        private static long nextOrder = 0;

        public CrewListWorkOrderModel(WorkOrder workOrder)
        {
            Id = workOrder.Id;
            Customer = workOrder.Customer;
            Description = workOrder.Description;
            IsComplete = workOrder.IsComplete;
            Category = workOrder.Category == null ? string.Empty : workOrder.Category.Name;
            AssignedTo = workOrder.Crew == null ? "Unassigned" : workOrder.Crew.Name;
            Date = workOrder.Date.HasValue ? workOrder.Date.Value.ToString("dd-MMM-yyyy hh:mm tt") : string.Empty;
            Order = workOrder.Date.HasValue ? workOrder.Date.Value.Ticks : CrewListWorkOrderModel.nextOrder++;
            Duration = workOrder.Duration;
        }

        public Guid Id { get; set; }
        public String Customer { get; set; }
        public String Description { get; set; }
        public bool IsComplete { get; set; }
        public string Duration { get; set; }

        public long Order { get; set; }
        
        public string AssignedTo { get; set; }
        public string Date { get; set; }

        public string Category { get; set; }

        public string ImageUrl
        {
            get
            {
                return VirtualPathUtility.ToAbsolute("~/img/" + Category + ".png");
            }
        }

        public string TileClass
        {
            get { return Category.ToLower(); }
        }
    }
}