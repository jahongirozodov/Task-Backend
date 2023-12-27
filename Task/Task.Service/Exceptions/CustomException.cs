namespace Task.Service.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode;
        public CustomException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
