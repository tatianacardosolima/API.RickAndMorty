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

        ///<summary>Endpoint que realiza consulta dos personagens trazendo somente aqueles que participam de mais de um episódio. </summary>
        ///<param name="status">Status do personagem alive, dead or unknown</param>
        ///<param name="species">Espécies dos personagens</param>
        ///<returns>Lista dos personagens</returns>
        ///<remarks>
        ///Esse código é uma demonstração de consumo de api utilizando Task.        
        ///</remarks>
        ///
        ///<response code = "404">Não foi encontrado personagens na base</response>
        ///<response code = "200">Consulta realizado com sucesso</response>
        [HttpGet()]
        public async Task<IActionResult> Get(string status, string species)
        {
            var cacheList = _cacheService.GetCacheByKey($"character-{status}-{species}");
            if (cacheList != null) return Ok((List<CharacterResultDTO>)cacheList);

            var characters = await _charactersService.GetAsync(status, species);
            _cacheService.AddCache($"character-{status}-{species}", characters, 20);
            return Ok(characters);
        }

       

    }
}
