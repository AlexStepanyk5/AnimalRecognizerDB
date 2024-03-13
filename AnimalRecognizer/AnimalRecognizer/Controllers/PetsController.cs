using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalRecognizer.Data;
using AnimalRecognizer.Model;
using AnimalRecognizer.DTOs;
using AutoMapper;

namespace AnimalRecognizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly AnimalRecognizerDBContext _context;
        private readonly IMapper _mapper;

        public PetsController(AnimalRecognizerDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pet>>> GetPets()
        {
            var pets = await _context.Pets.Include(p => p.Image)
                                          .Include(p => p.Contact)
                                          .ToListAsync();

            var mappedPets = pets.Select(pet => _mapper.Map<PetDTO>(pet));
            
            return Ok(mappedPets);
        }

        [HttpGet("{type}")]
        public async Task<ActionResult<List<Pet>>> GetPetByType(string type)
        {
            if (_context.Pets == null)
            {
                return NotFound();
            }
             var pets = _context.Pets.AsQueryable();
            if (type == "Cat")
            {
                pets = pets.Where(p => p.Type == Pet.PetType.Cat);
                
                await pets.Include(p => p.Image)
                                 .Include(p => p.Contact)
                                 .ToListAsync();

                var mappedPets = pets.Select(pet => _mapper.Map<PetDTO>(pet));

                return Ok(mappedPets);
            }
            else if (type == "Dog")
            {
                pets = pets.Where(p => p.Type == Pet.PetType.Dog);
                await pets.Include(p => p.Image)
                                  .Include(p => p.Contact)
                                  .ToListAsync();

                var mappedPets = pets.Select(pet => _mapper.Map<PetDTO>(pet));

                return Ok(mappedPets);
            }
            else
            {
                return NotFound();
            }

        }
      
        [HttpGet("{type}/{colour}")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPetByTypeAndColour(string type, string colour)
        {
            if (_context.Pets == null)
            {
                return NotFound();
            }

            if (type == "Cat")
            {
                var pets = _context.Pets.AsQueryable();

                pets = pets.Where(p => p.Type == Pet.PetType.Cat && p.Colour == colour);

                await pets.Include(p => p.Image)
                                 .Include(p => p.Contact)
                                 .ToListAsync();

                var mappedPets = pets.Select(pet => _mapper.Map<PetDTO>(pet));

                return Ok(mappedPets);
            }
            else if (type == "Dog")
            {
                var pets = _context.Pets.AsQueryable();

                pets = pets.Where(p => p.Type == Pet.PetType.Dog && p.Colour == colour);

                await pets.Include(p => p.Image)
                                 .Include(p => p.Contact)
                                 .ToListAsync();

                var mappedPets = pets.Select(pet => _mapper.Map<PetDTO>(pet));

                return Ok(mappedPets);
            }

            return NotFound();

        }


        [HttpGet("{type}/{sterilized}/{passport}")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPetByTypeAndOther(string type, bool sterilized, bool passport)
        {
            if (_context.Pets == null)
            {
                return NotFound();
            }

            if (type == "Cat")
            {
                var pets = _context.Pets.AsQueryable();

                pets = pets.Where(p => p.Type == Pet.PetType.Cat && p.Sterilized == sterilized && p.Passport == passport);

                await pets.Include(p => p.Image)
                                 .Include(p => p.Contact)
                                 .ToListAsync();

                var mappedPets = pets.Select(pet => _mapper.Map<PetDTO>(pet));

                return Ok(mappedPets);
            }
            else if (type == "Dog")
            {
                var pets = _context.Pets.AsQueryable();

                pets = pets.Where(p => p.Type == Pet.PetType.Dog && p.Sterilized == sterilized && p.Passport == passport);

                await pets.Include(p => p.Image)
                                 .Include(p => p.Contact)
                                 .ToListAsync();

                var mappedPets = pets.Select(pet => _mapper.Map<PetDTO>(pet));

                return Ok(mappedPets);
            }

            return NotFound();

        }

        [HttpGet("{type}/{colour}/{sterilized}/{passport}")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPetByTypeColourOther(string type, string colour, bool sterilized, bool passport)
        {
            if (_context.Pets == null)
            {
                return NotFound();
            }

            if (type == "Cat")
            {
                var pets = _context.Pets.AsQueryable();

                pets = pets.Where(p => p.Type == Pet.PetType.Cat && p.Colour == colour && p.Sterilized == sterilized && p.Passport == passport);

                await pets.Include(p => p.Image)
                                 .Include(p => p.Contact)
                                 .ToListAsync();

                var mappedPets = pets.Select(pet => _mapper.Map<PetDTO>(pet));

                return Ok(mappedPets);
            }
            else if (type == "Dog")
            {
                var pets = _context.Pets.AsQueryable();

                pets = pets.Where(p => p.Type == Pet.PetType.Dog && p.Colour == colour && p.Sterilized == sterilized && p.Passport == passport);

                await pets.Include(p => p.Image)
                                 .Include(p => p.Contact)
                                 .ToListAsync();

                var mappedPets = pets.Select(pet => _mapper.Map<PetDTO>(pet));

                return Ok(mappedPets);
            }

            return NotFound();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, Pet pet)
        {
            if (id != pet.Id)
            {
                return BadRequest();
            }

            _context.Entry(pet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(Pet pet)
        {
            if (_context.Pets == null)
            {
                return Problem("Entity set 'AnimalRecognizerDBContext.Pets'  is null.");
            }
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPets), new { id = pet.Id }, pet);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            if (_context.Pets == null)
            {
                return NotFound();
            }
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetExists(int id)
        {
            return (_context.Pets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
