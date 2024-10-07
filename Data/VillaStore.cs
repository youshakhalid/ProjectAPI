using ProjectAPI.Models;
using ProjectAPI.Models.DTO;

namespace ProjectAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
             new VillaDTO { Id = 1, Name = "Beach Villa", Sqft=100,Occupancys=3 },
             new VillaDTO { Id = 2, Name = "Pool Villa",Sqft=200,Occupancys=4 }
        };
    }
}
