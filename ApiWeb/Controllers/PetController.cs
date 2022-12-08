using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PetController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await this.dbContext.Pets.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Pet>>> CreatePet([FromBody] Pet pet)
        {
            await this.dbContext.Pets.AddAsync(pet);
            await this.dbContext.SaveChangesAsync();
            var pets = await this.dbContext.Pets.ToListAsync();

            return Ok(pets);
        }

        [HttpPut]
        public async Task<ActionResult<Pet>> UpdatePet(Pet request)
        {
            Pet pet = this.dbContext.Pets.Find((Pet pet)=> pet.Id == request.Id);
            if (pet == null)
            {
                return BadRequest(request);
            }

            pet.Name = request.Name;
            pet.Species = request.Species;
            pet.OwnerId = request.OwnerId;

            return pet;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Pet>>> GetPetsByOwnerId(int id)
        {
            List<Pet> pets = (List<Pet>) this.dbContext.Pets.Where((Pet x) => x.OwnerId == id);
            if (pets.Count() == 0)
            {
                return NotFound("pet(s) not Found");
            }
            return Ok(pets);
        }
    }
}
