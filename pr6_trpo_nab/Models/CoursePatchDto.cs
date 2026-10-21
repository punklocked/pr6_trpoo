namespace pr6_trpo_nab.Models
{
    public class CoursePatchDto
    {

        public string? Title { get; set; }

        public string? Teacher { get; set; }

        public int? DurationHours { get; set; }

        public decimal? Price { get; set; }

        public bool? IsActive { get; set; }
    }
}
