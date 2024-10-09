using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Models;
using ProjectAPI.Models.DTO;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VillaAPIController> _logger;

        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Getting All Villas");
            return Ok(_context.Villas);

        }


        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(typeof(VillaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(VillaDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(VillaDTO), StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Get Villa Error With Id " + id);
                return BadRequest();
            }

            var villa = _context.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            if (_context.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custom Error", "Name Already Exists");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id < 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            VillaModel model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImgUrl = villaDTO.ImgUrl,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                Occupancys = villaDTO.Occupancys,
            };
            _context.Villas.Add(model);
            _context.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }

        [HttpDelete("{id}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int? id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _context.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _context.Villas.Remove(villa);
            return NoContent();

        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (id == 0 || villaDTO.Id == id)
            {
                return BadRequest();
            }
            var villa = _context.Villas.FirstOrDefault(u => u.Id == id);

            if(villa == null)
            {
                return NotFound();
            }

            VillaModel model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImgUrl = villaDTO.ImgUrl,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                Occupancys = villaDTO.Occupancys,
            };

            _context.Villas.Update(model);
            _context.SaveChanges();


            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> document)
        {
            if (document == null || id == null)
            {
                return BadRequest();
            }
            var villa = _context.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            VillaDTO villaDTO= new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImgUrl = villa.ImgUrl,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
                Occupancys = villa.Occupancys,
            };

            document.ApplyTo(villaDTO,ModelState);

            VillaModel model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImgUrl = villaDTO.ImgUrl,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                Occupancys = villaDTO.Occupancys,
            };

            _context.Villas.Update(model);
            _context.SaveChanges();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}


 