using API.RickAndMorty.DTOs;
using API.RickAndMorty.Exceptions;
using API.RickAndMorty.Interfaces.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Text.Json;

namespace API.RickAndMorty.Services
{
    public class CharactersService : ICharactersService
    {
        private readonly string? _urlBase;
        

        public CharactersService(IConfiguration configuration)
        {
            _urlBase = configuration["RickAndMorty:BaseUrl"];
            
        }

        public async Task<List<CharacterResultDTO>> GetAsync(string status, string species)
        {

            ServiceException.ThrowWhen(status == null, "O status é obrigatório.");
            ServiceException.ThrowWhen(species == null, "A espécie é obrigatória.");

            var urlFinal = $"{_urlBase}character/?";

            // essa parte não esta no escopo, coloque apenas para testar a paginação.
            if (status != "all") urlFinal += $"&status={status}";
            if (species != "all") urlFinal += $"&species={species}";

            var list = new List<CharacterResultDTO>();
            var client = new RestClient();
            var request = new RestRequest($"{urlFinal}&page=1", Method.Get);
            

            request.AddHeader("Content-Type", "application/json");

            var response = await client.ExecuteAsync(request);

            NotFoundException.ThrowWhen(response.StatusCode == System.Net.HttpStatusCode.NotFound, "Nenhum personagem foi encontrado.");

            var result = JsonSerializer.Deserialize<CharacterDTO>(response.Content);
            if (result != null && result.info.pages >= 1)
            {
                list.AddRange(result.results.Where(x => x.episode.Length>1));

                Task[] runnedTask = new Task[result.info.pages-1];

                var idx = 0;
                
                for (int page = 2; page <= result.info.pages; page++)
                {
                    int currentPage = page;
                    runnedTask[idx++] = Task.Run(async () =>
                    {
                        client = new RestClient();

                        request = new RestRequest($"{urlFinal}&page={currentPage}", Method.Get);
                       // request = new RestRequest($"{_urlBase}character/?page={currentPage}", Method.Get);
                        request.AddHeader("Content-Type", "application/json");
                        response = await client.ExecuteAsync(request);
                        result = JsonSerializer.Deserialize<CharacterDTO>(response.Content);
                        if (result != null)
                        {
                            list.AddRange(result.results.Where(x => x.episode.Length > 1));
                        }
                    });
                }

                Task.WaitAll(runnedTask);
                
            }
            
            
            return  list.OrderBy(x=>x.id).ToList();
        }

        
    }
}
