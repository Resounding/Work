﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class EditWorkOrderViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public String Customer { get; set; }
        public String Description { get; set; }
        public Guid CrewId { get; set; }
        public IEnumerable<Crew> Crews { get; set; }
        public string Duration { get; set; }
        public IEnumerable<string> Durations { get; set; }
        public Guid CategoryId { get; set; }
        public IEnumerable<WorkOrderCategory> Categories { get; set; }
    }
}