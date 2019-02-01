using System;
using System.Collections.Generic;

namespace Instict2K19.DataModel
{

    public class Event
    {
        public string Name { get; set; }
        public double Fees { get; set; }
        public int? TeamMemberCount { get; set; }
        public bool IsChargePerPerson { get; set; }
    }

    public class SubGategory
    {
        public string Name { get; set; }
        public List<Event> Events { get; set; }
    }

    public class Category
    {
        public List<SubGategory> SubGategories { get; set; }
        public string Name { get; set; }
    }

    public class EventModel
    {
        public List<Category> Categories { get; set; }
    }
}
