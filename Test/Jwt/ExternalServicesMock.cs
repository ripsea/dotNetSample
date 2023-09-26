using Moq;
using Services.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JwtTest
{
    public class ExternalServicesMock
    {
        public Mock<IJWTManagerRepository> JWTManagerRepository { get; }
        public Mock<IUserServiceRepository> UserServiceRepository { get; }

        public ExternalServicesMock()
        {
            JWTManagerRepository = new Mock<IJWTManagerRepository>();
            UserServiceRepository = new Mock<IUserServiceRepository>(); 
        }

        /// <summary>
        /// GetMocks allows the factory to always have the entire list of mocks 
        /// without having to manually add the mocks to a list,
        /// which can sometimes be forgotten and result in time wasted trying 
        /// to identify the issue.
        /// </summary>
        /// <returns>Type, object</returns>
        public IEnumerable<(Type, object)> GetMocks()
        {
            return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(x =>
                {
                    var underlyingType = x.PropertyType.GetGenericArguments()[0];
                    var value = x.GetValue(this) as Mock;

                    return (underlyingType, value.Object);
                })
                .ToArray();
        }
    }
}
