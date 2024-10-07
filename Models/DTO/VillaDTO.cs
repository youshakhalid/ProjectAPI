using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Models.DTO
{
    public class VillaDTO
    {
        public int Id{ get; set; }
        [Required, MaxLength(30)]
        public string Name{ get; set; }
        [Required]
        public int Sqft{ get; set; }
        [Required]
        public int Occupancys { get; set; }
    }
}
