using System;
using System.Collections.Generic;

namespace backend.Models
{
    public partial class Animal
    {
        public Animal()
        {
            Enqueries = new HashSet<Enquery>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int KindId { get; set; }
        public int Age { get; set; }
        public ulong? IsMale { get; set; }
        public ulong IsNeutered { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public ulong IsActive { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Kind Kind { get; set; } = null!;
        public virtual ICollection<Enquery> Enqueries { get; set; }
    }
}
