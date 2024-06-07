using ApiCadastro.Models;

namespace ApiCadastro.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "batman", Password = "batman", Role = "manager" },
                new User { Id = 1, Username = "robin", Password = "robin", Role = "employee" }
            };

            return users.FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase) && x.Password == password);
        }
    }
}
