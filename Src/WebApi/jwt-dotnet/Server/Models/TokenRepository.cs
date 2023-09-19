using Server.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class TokenRepository:IDisposable
    {
        DEVContext context = new DEVContext();
        //This method is used to check and validate the user credentials
        public ClientMaster ValidateClient(string ClientID, string ClientSecret)
        {
            return context.ClientMasters.FirstOrDefault(user =>
             user.ClientID == ClientID
            && user.ClientSecret == ClientSecret);
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}