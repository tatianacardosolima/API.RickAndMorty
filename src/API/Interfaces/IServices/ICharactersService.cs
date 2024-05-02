using API.RickAndMorty.DTOs;

namespace API.RickAndMorty.Interfaces.IServices
{
    public interface ICharactersService
    {                
        Task<List<CharacterResultDTO>> GetAsync(string status, string species);
    }
}
