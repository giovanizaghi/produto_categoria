using System;

namespace Authentication.Entities
{
    public class AuthResult
    {
        public bool authenticated { get; set; }
        public DateTime created { get; set; }
        public DateTime expiration { get; set; }
        public string accessToken { get; set; }
        public string message { get; set; }
    }
}
