using MediatR;

namespace API.RickAndMorty.Commands
{
    public class FilterCharectersRequest: IRequest<DefaultResponse>
    {
        public FilterCharectersRequest()
        {
            
        }
        public FilterCharectersRequest(string status, string species)
        {
            Status = status;
            Species = species;
        }
        public string Status { get; set; }
        public string Species { get; set; }
    }
}
