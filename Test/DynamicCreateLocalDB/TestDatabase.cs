using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicCreateLocalDB
{
    public class TestDatabase
    {
        //參考範例:
        //建DB測試資料範例 - https://kevintsengtw.blogspot.com/2016/10/repository-localdb-part1.html
        //測試相關file處理 - https://sau001.wordpress.com/2019/02/24/net-core-unit-tests-how-to-deploy-files-without-using-deploymentitem/
        //MSTest with EF Code First - https://martinwilley.com/net/code/localdbtest.html

        private const string LocalDbMasterConnectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";

        private const string TestConnectionString =
           @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog={0};Integrated Security=True;
                 MultipleActiveResultSets=True;AttachDBFilename={1}.mdf";

        string DatabaseName { get; set; }

        public TestDatabase(string databaseName)
        {
            DatabaseName = databaseName;
        }

        /// <summary>
        /// 建立DB
        /// </summary>
        void CreateDB()
        {
            //先看看有沒有相同的DB存在，如果有的話卸離並移除
            this.DetachDatabase();

            var fileName = this.CleanupDatabase();

            using (var connection = new SqlConnection(LocalDbMasterConnectionString))
            {
                var commandText = new StringBuilder();
                //Create DB的語法
                commandText.AppendFormat(
                 "CREATE DATABASE {0} ON (NAME = N'{0}', FILENAME = '{1}.mdf');",
                 this.DatabaseName,
                 fileName);

                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = commandText.ToString();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Detaches the database.
        /// </summary>
        private void DetachDatabase()
        {
            using (var connection = new SqlConnection(LocalDbMasterConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = string.Format("exec sp_detach_db '{0}'", this.DatabaseName);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    Console.WriteLine("Could not detach");
                }
            }
        }

        /// <summary>
        /// Cleanups the database.
        /// </summary>
        /// <returns>System.String.</returns>
        private string CleanupDatabase()
        {
            var fileName = string.Concat(@"G:\", this.DatabaseName);
            try
            {
                var mdfPath = string.Concat(fileName, ".mdf");
                var ldfPath = string.Concat(fileName, "_log.ldf");

                var mdfExists = File.Exists(mdfPath);
                var ldfExists = File.Exists(ldfPath);

                if (mdfExists) File.Delete(mdfPath);
                if (ldfExists) File.Delete(ldfPath);
            }
            catch
            {
                Console.WriteLine("Could not delete the files (open in Visual Studio?)");
            }
            return fileName;
        }
    }
}
