using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportsAPI.EfCore
{
    [Table("passports")]
    public class InactivePassports
    {
        [Required]
        public int id { get; set; }
        [Column("series")]
        public int Series { get; set; }
        [Column("number")]
        public int Number { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; } = false;
        [Column("changetime")]
        public DateTime ChangeTime { get; set; }

    }
}
