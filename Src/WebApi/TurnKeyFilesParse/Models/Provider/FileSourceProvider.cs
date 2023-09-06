using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Caching;
using Api.Utilities;

namespace Api
{
    public class FileSourceProvider
    {
        // fields
        private static ObjectCache _cache = MemoryCache.Default;
        // proterties
        public CacheEntryRemovedCallback OnFileContentsCacheRemove;

        public string PolicyType { get; set; }

        public IEnumerable<string> GetTurnKeyItemCache(
            TurnKeyFile turnKeyFile) 
        {
            IEnumerable<string> fileList 
                = _cache[turnKeyFile.CacheKey] as IEnumerable<string>;

            if (fileList != null && fileList.Count() > 0)
            {
                return fileList;
            }

            var targetFiles =  Utility.EnumerateSubDirectoriesOrFiles(
                turnKeyFile.TargetFolder
                , turnKeyFile.TargetFolderLayer
                , turnKeyFile.TargetType);

            var policy = new CacheItemPolicy();
            policy.RemovedCallback = OnFileContentsCacheRemove;
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(86400);

            // set cache
            _cache.Set(turnKeyFile.CacheKey
                , targetFiles
                , policy);
            return _cache[turnKeyFile.CacheKey] as IEnumerable<string>;
        }

    }
}