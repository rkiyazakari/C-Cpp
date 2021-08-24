using System.Linq;
using ProjectServer.Models;

namespace ProjectServer.Services
{
    public class AuthService
    {
        public static User GetUserFromId(int userId)
        {
            var context = DbServices.Instance.Context;
            return context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public static User GetUserFromUsername(string username)
        {
            var context = DbServices.Instance.Context;
            return context.Users.FirstOrDefault(u => u.Username == username);
        }

        public static User LogUserIn(string username, string password)
        {
            var dbContext = DbServices.Instance.Context;
            var dbUser = GetUserFromUsername(username);

            if (dbUser == null) return null;

            return BCrypt.Net.BCrypt.Verify(password, dbUser.Password) ? dbUser : null;
        }

        public static User RegisterUser(User newUser)
        {
            var dbContext = DbServices.Instance.Context;

            //TODO: check if username is taken before adding it

            try
            {
                dbContext.Users.Add(newUser); // TODO: handle error
                dbContext.SaveChanges();
                return GetUserFromUsername(newUser.Username);
            }
            catch
            {
                return null;
            }
        }


        public static bool IsLoggedIn(User user)
        {
            return user != null;
        }


        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}