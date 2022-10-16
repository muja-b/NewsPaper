namespace NewsPaper.Models
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public List<string> Error { get; set; }
        public string Refreshtoken { get; set; }
        public string Token { get; set; }
    }
}
