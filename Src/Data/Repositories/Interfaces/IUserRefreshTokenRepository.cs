using Data.Entities;
using Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUserRefreshTokenRepository : IRepositoryBase<UserRefreshToken>
    {
    }
}
