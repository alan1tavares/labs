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
                    Roles = new List<string>() { "manager" }
                });
            users.Add(
                new User
                {
                    Username = "admin",
                    Password = "1234",
                    Roles = new List<string>() { "admin" }
                }
            );
            users.Add(
                new User
                {
                    Username = "forAll",
                    Password = "1234",
                    Roles = new List<string>() { "manager", "admin" }
                }
            );
            users.Add(
                new User
                {
                    Username = "none",
                    Password = "1234",
                    Roles = new List<string>(){"none"}
                }
            );

            return users
                .Where(user => (user.Username == username) && (user.Password == password))
                .FirstOrDefault();
        }
    }
}