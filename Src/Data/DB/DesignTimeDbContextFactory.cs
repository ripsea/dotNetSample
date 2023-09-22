using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Data.DB;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DEVDbContext>
{
    public DEVDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) //會在類別程式庫專案執行，由同目錄取JSON
            .AddJsonFile("appSettings.json")
            .Build();

        var cnStr = config.GetConnectionString("DefaultConnection"); //TODO: 正式應用時且連線字串含密碼時應加密儲存
        if (string.IsNullOrEmpty(cnStr)) throw new ApplicationException("Missing .config connection string [MYDB]");
        var ctxBuilder = 
            new DbContextOptionsBuilder<DEVDbContext>();
        ctxBuilder.UseSqlServer(cnStr);
        return new DEVDbContext(ctxBuilder.Options);
    }
}