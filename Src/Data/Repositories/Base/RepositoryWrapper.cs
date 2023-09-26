using Data.DB;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DEVDbContext _repoContext;
        private IUserRepository _user;
        private IUserRefreshTokenRepository _userRefreshToken;
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        public IUserRefreshTokenRepository UserRefreshToken
        {
            get
            {
                if (_userRefreshToken == null)
                {
                    _userRefreshToken = new UserRefreshTokenRepository(_repoContext);
                }
                return _userRefreshToken;
            }
        }

        internal RepositoryWrapper(DEVDbContext repositoryContext) => 
            _repoContext = repositoryContext;
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
