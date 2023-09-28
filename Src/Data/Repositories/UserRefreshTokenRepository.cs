using Data.DB;
using Data.Entities;
using Data.Repositories.Base;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRefreshTokenRepository : 
        RepositoryBase<UserRefreshToken>, 
        IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(
            DEVDbContext repositoryContext) : 
            base(repositoryContext)
        {
        }

    }
}
