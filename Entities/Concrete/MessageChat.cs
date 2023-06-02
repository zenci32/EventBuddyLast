using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class MessageChat
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string MessageText { get; set; }
        public string PhoneNumber { get; set; }
        public int EventId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
