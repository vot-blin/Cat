using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Cat.Core.Entities
{
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PedigreeNumber { get; set; }
        public string ParentNames { get; set; }
        public DateTime LastVaccination { get; set; }
        public string OwnerInfo { get; set; }
        public string Medal { get; set; }
        public bool IsDisqualified { get; set; }

        public int BreedId { get; set; }
        public Breed Breed { get; set; }

        public int ClubId { get; set; }
        public Club Club { get; set; }

        public int? RingId { get; set; }
        public Ring Ring { get; set; }

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
