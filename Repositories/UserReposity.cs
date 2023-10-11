using AppClients.Models;

namespace AppClients.Repositories
{
    public static class UserReposity
    {
        public static User Get(string username, 
            string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, 
                    UserName = "Admin",
                    Password = "123",
                    Role = TypeUser.Admin
                }
            };

            return users.First(x =>
                x.UserName == username &&
                x.Password == password);
        }
    }
}
