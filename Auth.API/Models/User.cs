namespace Auth.API.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public int role { get; set; }
        public string email { get; set; }
        public string pwdHash { get; set; }
    }
}