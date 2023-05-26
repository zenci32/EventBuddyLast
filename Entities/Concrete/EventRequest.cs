using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class EventRequest
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string InviterPhone { get; set; }
        public string Status { get; set; }
    }
}
