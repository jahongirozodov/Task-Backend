namespace Task.Service.Exceptions
{
    public class NotFoundException : Exception
    {
        public int StatusCode = 401;
        public NotFoundException(string message) : base(message)
        { }
    }
}
