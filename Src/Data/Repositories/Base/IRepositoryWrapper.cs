using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Base
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IUserRefreshTokenRepository UserRefreshToken { get; }
        void Save();
    }
}
