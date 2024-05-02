namespace API.RickAndMorty.Exceptions
{
    public class NotFoundException : Exception
    {

        public NotFoundException(string message) : base(message) { }

        public static void ThrowWhen(bool resp, string erroMessage)
        {
            if (!resp)
                return;

            throw new NotFoundException(erroMessage);
        }
    }
}
