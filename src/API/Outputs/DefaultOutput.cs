namespace API.RickAndMorty.Outputs
{
    public class DefaultOutput
    {
        public DefaultOutput(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        public DefaultOutput(bool success, string message)
        {
            Success = success;
            Message = message;            
        }
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
