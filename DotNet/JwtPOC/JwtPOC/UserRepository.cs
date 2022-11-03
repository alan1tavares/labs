namespace PocJwt
{
    public static class UserRepository
    {
        public static User Get()
        {
            return new User()
            {
                Username = "alan",
                Password = "1234",
                Role = "manager"
            };
        }
    }
}