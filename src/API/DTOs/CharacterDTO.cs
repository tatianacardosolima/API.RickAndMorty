namespace API.RickAndMorty.DTOs
{    

    public class CharacterDTO
    {
        public CharacterInfoDTO info { get; set; }
        public List<CharacterResultDTO> results { get; set; }
    }

    public class CharacterInfoDTO
    {
        public int count { get; set; }
        public int pages { get; set; }
        public string next { get; set; }
        public object prev { get; set; }
    }

    public class CharacterResultDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string species { get; set; }
        public string type { get; set; }
        public string gender { get; set; }
        public CharacterOriginDTO origin { get; set; }
        public CharacterLocationDTO location { get; set; }
        public string image { get; set; }
        public string[] episode { get; set; }
        public string url { get; set; }
        public DateTime created { get; set; }
    }

    public class CharacterOriginDTO
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class CharacterLocationDTO
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}