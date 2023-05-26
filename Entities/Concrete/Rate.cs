using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Rate
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public decimal RateTotal { get; set; }
        public int VoterCount { get; set; }
    }
}
