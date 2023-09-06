using Api;
using System.ComponentModel.DataAnnotations;

namespace TestProject
{
    [TestFixture]
    public class Tests
    {
        string directoryTestBasePath = @"D:/Doc/UnitTest";
        [OneTimeSetUp]
        public void OneTimeInit()
        {
        }

        [SetUp]
        public void EveryTestInit()
        {

        }

        [Test]	//°õ¦æ¶¶§Ç
        public void Test1()
        {
            CreateTestFile();
            //E0501 target = new E0501();
            //target.GetFileInfoData();
        }

        [TearDown]
        public void Cleanup()
        {

        }

        public void CreateTestFile()
        {
            /*
         * D:/Doc/UnitTest
            - 2022                  depth=1
                - 20220506          depth=2
                    220506-1.txt
                22-1.txt
            - 2023
            1.txt
         */

            if (File.Exists(directoryTestBasePath))
            { File.Delete(directoryTestBasePath); }
            DirectoryInfo directoryTestBaseDirInfo
                = new DirectoryInfo(directoryTestBasePath);
            directoryTestBaseDirInfo.Create();

            DirectoryInfo dir1 = new DirectoryInfo(Path.Combine(directoryTestBasePath, "2022"));
            dir1.Create();
            FileInfo file1 = new FileInfo(Path.Combine(dir1.FullName, "22-1.txt"));
            file1.Create();
            dir1 = new DirectoryInfo(Path.Combine(dir1.FullName, "20220506"));
            dir1.Create();
            file1 = new FileInfo(Path.Combine(dir1.FullName, "220506-1.txt"));
            file1.Create();

            dir1 = new DirectoryInfo(Path.Combine(directoryTestBasePath, "2023"));
            dir1.Create();

            file1 = new FileInfo(Path.Combine(directoryTestBaseDirInfo.FullName, "1.txt"));
            file1.Create();
        }
    }
}