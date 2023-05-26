using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class GetRequestedEventDto
    {
        public int EventId { get; set; }
        public string Phone { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public int EventCategory { get; set; }
        public decimal EventLatitude { get; set; }
        public decimal EventLongitude { get; set; }
        public int EventCount { get; set; }
        public int ActiveCount { get; set; }
        public string EventStatus { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventCreateDate { get; set; }
        public DateTime EventLastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public string RequestStatus { get; set; }
    }
}
