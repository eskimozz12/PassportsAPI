using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportsAPI.EfCore
{
    [Table("history")]
    public class PassportsHistory
    {
        [Required]
        public int id { get; set; }
        public int Series { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }
        public DateTime ChangeTime { get; set; }
    }
}
