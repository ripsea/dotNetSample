using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Auth.Basic
{
    internal class UserValidate
    {
        public static bool Login(string username, string password)
        {
            UsersBL userBL = new UsersBL();
            var UserLists = userBL.GetUsers();
            return UserLists.Any(user =>
                user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                && user.Password == password);
        }

        // This method is used to return the User Details
        public static User GetUserDetails(string username, string password)
        {
            UsersBL userBL = new UsersBL();
            return userBL.GetUsers().FirstOrDefault(user =>
                user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                && user.Password == password);
        }
    }
}
