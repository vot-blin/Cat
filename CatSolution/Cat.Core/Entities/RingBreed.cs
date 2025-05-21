using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat.Core.Entities
{
    public class RingBreed
    {
        public int RingId { get; set; }
        public int BreedId { get; set; }
        public Ring Ring { get; set; }
        public Breed Breed { get; set; }
    }
}
