using System.Collections.Generic;

namespace Api.Models.Auth.Basic
{
    internal class UsersBL
    {
        public List<User> GetUsers()
        {
            // In Real-time you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we are hardcoded the data
            List<User> userList = new List<User>();
            userList.Add(new User()
            {
                ID = 101,
                UserName = "Iris",
                Password = "123456",
                Roles = "Admin",
                Email = "Admin@a.com",
            });
            userList.Add(new User()
            {
                ID = 101,
                UserName = "Ripsea",
                Password = "abcdef",
                Roles = "Admin,Superadmin",
                Email = "BothUser@a.com",
            });
            return userList;
        }
    }
}