using Data.DB;
using Data.Entities;
using Data.Repositories.Base;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : 
        RepositoryBase<UserEntity>, 
        IUserRepository
    {
        public UserRepository(DEVDbContext repositoryContext) : 
            base(repositoryContext)
        {
        }

    }
}
