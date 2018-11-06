namespace SimbaToursEastAfrica.Controllers
{
    public class UserDetails
    {
        public string emailAddress { get; set; }
        public string password { get; set; }
        public string repassword { get; set; }
        public bool keepLoggedIn { get; set; }
    }

    public class UserRole
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class ResetPassword
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string Repassword { get; set; }
    }
}