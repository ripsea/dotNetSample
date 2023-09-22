using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Common
{
    internal class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Byte[] RowVersion { get; set; }
    }
}
