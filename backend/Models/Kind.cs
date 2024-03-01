
namespace backend.Models
{
    public partial class Kind
    {
        public Kind()
        {
            Animals = new HashSet<Animal>();
        }

        public int Id { get; set; }
        public string Kind1 { get; set; } = null!;

        public virtual ICollection<Animal> Animals { get; set; }
    }
}
