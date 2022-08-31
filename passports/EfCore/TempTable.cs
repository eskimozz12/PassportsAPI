using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportsAPI.EfCore
{
    [Table("temptable")]
    public class TempTable
    {
        [Required]
        public int id { get; set; }
        [Column("series")]
        public int Series { get; set; }
        [Column("number")]
        public int Number { get; set; }
    }
}
