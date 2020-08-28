namespace Authentication.Entities
{
    public class UserDb
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Service { get; set; }
    }
}
