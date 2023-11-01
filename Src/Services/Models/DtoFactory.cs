using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public static class DtoFactory
    {
        public static TokenResultDto TokenResultDto()
        {
            return new TokenResultDto
            {
                Success = true,
                Errors = null
            };
        }
    }
}
