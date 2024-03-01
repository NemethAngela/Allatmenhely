
namespace backend.Models
{
    public partial class Enquery
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Phone { get; set; }
        public int AnimalId { get; set; }
        public string Email { get; set; } = null!;

        public virtual Animal Animal { get; set; } = null!;
    }
}
