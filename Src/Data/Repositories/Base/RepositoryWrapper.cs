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

        public RepositoryWrapper(
            DEVDbContext repoContext, 
            IUserRepository user, 
            IUserRefreshTokenRepository userRefreshToken) 
        {
            _repoContext = repoContext;
            _user = user;
            _userRefreshToken = userRefreshToken;
        }

        public IUserRepository User => _user;
        
        public IUserRefreshTokenRepository UserRefreshToken=> _userRefreshToken;

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
