namespace PocJwt
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();

            users.Add(
                new User
                {
                    Username = "alan",
                    Password = "1234",
                    Role = "manager"
                });
            users.Add(
                new User
                {
                    Username = "admin",
                    Password = "1234",
                    Role = "admin"
                }
            );
            users.Add(
                new User
                {
                    Username = "forAll",
                    Password = "1234",
                    Role = "amin"
                }
            );

            return users
                .Where(user => (user.Username == username) && (user.Password == password))
                .FirstOrDefault();
        }
    }
}