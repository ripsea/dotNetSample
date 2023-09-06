using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Utilities
{
    public static class Utility
    {
        /// <summary>
        /// 取得檔案路徑Segment/Section
        /// </summary>
        /// <param name="filePath">full file name and path</param>
        /// <returns>>取得檔案路徑Segment/Section</returns>
        public static IList<string> GetDirectorySegments(string filePath)
        {
            var directorySegments = new List<string>();
            if (string.IsNullOrEmpty(filePath))
            {
                return directorySegments;
            }

            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Directory == null)
            {
                return directorySegments;
            }

            for (var currentDirectory = fileInfo.Directory;
                currentDirectory != null;
                currentDirectory = currentDirectory.Parent)
            {
                directorySegments.Insert(0, currentDirectory.Name);
            }

            return directorySegments;
        }

        /// <summary>
        /// 取得第幾層的資料夾路徑或檔案List
        /// </summary>
        /// <remarks>layers limited</remarks>
        /// <param name="path">file root</param>
        /// <param name="layer">how many layers</param>
        /// <param name="returnType">files or dirs or all</param>
        /// <returns></returns>
        /*
             * D:/Doc/UnitTest
                - 2022                  depth=1
                    - 20220506          depth=2
                        220506-1.txt
                    22-1.txt
                - 2023
                1.txt
             */
        public static IEnumerable<string> EnumerateSubDirectoriesOrFiles(
            string path
            , int layer
            , DirectorieOrFileTypeEnum returnType)
        {
            var result = new List<string>();
            if (!Directory.Exists(path))
            {
                return result;
            }
            layer--;

            foreach (var subPath
                in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
            {
                if (layer == 0 && returnType == DirectorieOrFileTypeEnum.File)
                {
                    result.AddRange(Directory.EnumerateFiles(subPath, "*").ToList());
                    break;
                }

                if (layer == 0 && returnType == DirectorieOrFileTypeEnum.All)
                {
                    result.AddRange(Directory.EnumerateFiles(subPath, "*").ToList());
                }

                result.AddRange(layer == 0
                    ? new List<string> { subPath }
                    : EnumerateSubDirectoriesOrFiles(subPath, layer, returnType));
            }

            return result;
        }
    }
}
