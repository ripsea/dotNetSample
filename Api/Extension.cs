using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public static class Extension
    {
        public static IEnumerable<FileInfoData> FindByDuration(
            this IEnumerable<FileInfoData> fileInfoData
            , DateTime startDate
            , DateTime endDate)
        {
            if (fileInfoData == null)
            { throw new ArgumentNullException(nameof(fileInfoData)); }

            IEnumerable<FileInfoData> filterItems =
                fileInfoData
                .Where(y => y.QueryYYYYMMDD >= startDate)
                .Where(y => y.QueryYYYYMMDD <= endDate);

            return filterItems;
        }
    }
}
