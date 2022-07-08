namespace Authorization.Models.Repositories
{
    public class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new()
                {
                    Id = 1,
                    UserName = "batman",
                    Password = "batman",
                    Role = "batman"
                }
            };

            return users
                .FirstOrDefault(x =>
                    x.UserName == username
                    && x.Password == password
                );
        }
    }
}
