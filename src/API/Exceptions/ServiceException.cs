namespace API.RickAndMorty.Exceptions
{
    public class ServiceException : Exception
    {

        public ServiceException(string message) : base(message) { }

        public static void ThrowWhen(bool resp, string erroMessage)
        {
            if (!resp)
                return;

            throw new ServiceException(erroMessage);
        }
    }
}
