namespace Task.Api.Helpers
{
    public class Response
    {
        public int StatusCode {get;set;}
        public string Message { get;set; }
        public dynamic Data {get; set; }
    }
}
