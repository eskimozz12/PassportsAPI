namespace passports.Models
{
    public class Passports
    {
        public int Series { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }
        public DateTime ChangeTime { get; set; }


    }
}
