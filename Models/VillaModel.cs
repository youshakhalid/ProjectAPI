using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Models
{
    public class VillaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }

        public string Details { get; set; }
        [Required]
        public int Sqft { get; set; }
        public double Rate { get; set; }
        [Required]
        public int Occupancys { get; set; }


        public string ImgUrl { get; set; }
        public string Amenity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
