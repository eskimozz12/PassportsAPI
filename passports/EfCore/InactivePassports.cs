using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportsAPI.EfCore
{
    [Table("passports")]
    public class InactivePassports
    {
        [Required]
        public int id { get; set; }
        public int Series { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime ChangeTime { get; set; }
        [MaxLength(5)]
        public List<PassportsHistory> History { get; set; }

    }
}
