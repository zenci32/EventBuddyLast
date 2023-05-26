using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class EventDto
    {
        public string Phone { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public int EventCategory { get; set; }
        public decimal EventLatitude { get; set; }
        public decimal EventLongitude { get; set; }
        public int EventCount { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventFinishDate { get; set; }

    }
}
