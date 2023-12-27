namespace Task.Service.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public int StatusCode = 403;
        public AlreadyExistException(string message) : base(message)
        { }
    }
}
