using API.RickAndMorty.DTOs;

namespace API.RickAndMorty.Interfaces.IServices
{
    public interface ICharactersService
    {
        
        CharacterResultDTO GetById(int id);

        Task<List<CharacterResultDTO>> GetAsync(string status, string species);
    }
}
