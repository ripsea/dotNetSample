using Data.DB;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public RepositoryWrapper(
            DEVDbContext repoContext, 
            IUserRepository user, 
            IUserRefreshTokenRepository userRefreshToken,
            UserManager<IdentityUser> userManager) 
        {
            _repoContext = repoContext;
            _user = user;
            _userRefreshToken = userRefreshToken;
            _userManager = userManager;
        }

        public IUserRepository User => _user;
        
        public IUserRefreshTokenRepository UserRefreshToken=> _userRefreshToken;

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
