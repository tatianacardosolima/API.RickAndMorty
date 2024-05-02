using API.RickAndMorty.DTOs;
using API.RickAndMorty.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/characters")]

    public class CharacterController : ControllerBase
    {
        private readonly ICharactersService _charactersService;

        public CharacterController(ICharactersService charactersService)
        {
            _charactersService  = charactersService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get(string status, string species)
        {
            var characters = await _charactersService.GetAsync(status, species);
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var character = _charactersService.GetById(id);
            if (character == null) 
            {
                return NotFound();
            }
            return Ok(character);
        }
     

    }
}
