using AspNetApi.Models.TurnKeyItem;
using AspNetApi.Models.TurnKeyItem.E0501;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Api
{
    public class TurnKeyItemFactory
    {
        public static ITurnKeyService GetTurnKeyItem(TurnKeyItemEnum item)
        {
            var path
                = ConfigurationManager
                .AppSettings
                .Get(string.Format("{0}FolderPath", item));

            if (!Directory.Exists(path))
            { throw new DirectoryNotFoundException(path); }

            switch (item)
            {
                case TurnKeyItemEnum.E0501:
                    {
                        TurnKeyFile turnKeyFile
                            = new TurnKeyFile(itemId:item, 
                            targetFolder:path, 
                            targetFolderLayer:3);
                        
                        return new E0501(turnKeyFile);
                    }
                    /*
                case TurnKeyItemEnum.SummaryResult:
                    {
                        TurnKeyFile turnKeyFile
                            = new TurnKeyFile(itemId: item,
                            targetFolder: path,
                            targetFolderLayer: 2);

                        return new SummaryResult(turnKeyFile);
                    }
                    */
                default: return null;
            }
        }
    }
}
