namespace Task.Service.Exceptions
{
    public class NotFoundException
    {
        public int StatusCode = 401;
        public NotFoundException(string message) : base(message)
        { }
    }
}
