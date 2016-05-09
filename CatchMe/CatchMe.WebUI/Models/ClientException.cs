namespace CatchMe.WebUI.Models
{
    public class ClientException
    {
        public string User { get; set; }

        public string Url { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public string Time { get; set; }
    }
}