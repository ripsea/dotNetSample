using Server.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class UserRepository : IDisposable
    {

        DEVContext context = new DEVContext();
        //This method is used to check and validate the user credentials
        public UserMaster ValidateUser(string username, string password)
        {
            try
            {
                UserMaster userMaster = context.UserMasters.FirstOrDefault(user =>
                    user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                    && user.UserPassword == password);
                return userMaster;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}