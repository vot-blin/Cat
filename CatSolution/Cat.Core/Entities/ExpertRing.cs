using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat.Core.Entities
{
    public class ExpertRing
    {
        public int ExpertId { get; set; }
        public int RingId { get; set; }
        public Expert Expert { get; set; }
        public Ring Ring { get; set; }
    }
}
