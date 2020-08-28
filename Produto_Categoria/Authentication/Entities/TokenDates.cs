using System;

namespace Authentication.Entities
{
    public class TokenDates
    {
        public DateTime NotBefore { get; set; }

        public DateTime Expires { get; set; }

        public TokenDates(DateTime _notBefore, DateTime _expires)
        {
            NotBefore = _notBefore;
            Expires = _expires;
        }
    }
}
