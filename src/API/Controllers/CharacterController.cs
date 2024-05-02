using API.RickAndMorty.DTOs;
using API.RickAndMorty.Interfaces.IServices;
using API.RickAndMorty.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/characters")]

    public class CharacterController : ControllerBase
    {
        private readonly ICharactersService _charactersService;
        private readonly ICacheService _cacheService;

        public CharacterController(ICharactersService charactersService, ICacheService cacheService)
        {
            _charactersService  = charactersService;
            _cacheService = cacheService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get(string status, string species)
        {
            var cacheList = _cacheService.GetCacheByKey($"character-{status}-{species}");
            if (cacheList != null) return Ok((List<CharacterResultDTO>)cacheList);

            var characters = await _charactersService.GetAsync(status, species);
            _cacheService.AddCache($"character-{status}-{species}", characters, 20);
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
