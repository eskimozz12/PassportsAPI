using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportsAPI.EfCore
{
    [Table("history")]
    public class PassportsHistory
    {
        [Required]
        public int id { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
        [Column("changetime")]
        public DateTime ChangeTime { get; set; }
        [Column("passportid")]
        public int PassportId { get; set; }
        public InactivePassports Passport { get; set; }
    }
}
