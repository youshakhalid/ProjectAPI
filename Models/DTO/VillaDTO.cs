using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public int Occupancys { get; set; }
        public int Sqft { get; set; }
        public string ImgUrl { get; set; }
        public string Amenity { get; set; }
    }
}
