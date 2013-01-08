using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class Crew
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Crew;
            if (obj == null) return false;

            return other.Id == Id;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}