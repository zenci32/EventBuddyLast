using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class RequestEventNotifyDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }

        public string InviterPhone { get; set; }

        public string InviterName { get; set; }

        public int EventCount { get; set; }

        public int ActiveCount { get; set; }
        public decimal?  Rate { get; set; }

    }
}
