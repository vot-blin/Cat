using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat.Core.Entities
{
    public class Breed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Cat> Cats { get; set; }
        public ICollection<RingBreed> RingBreeds { get; set; }
    }
}
