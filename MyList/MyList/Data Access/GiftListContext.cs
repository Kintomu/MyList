using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyList.Models;
using MyList.Tests;

namespace MyList.Data_Access;

public class GiftListContext : DbContext
{
    public GiftListContext(DbContextOptions<TestGiftListContext> options) : base(options)
    {
    }
    public DbSet<GiftList> GiftLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=GiftList.db");
    }

}

public class DataInitializer : DbCommandInterceptor
{
    public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
    {
        Process(command, eventData);
        return result;
    }

    private void Process(DbCommand command, CommandEventData eventData)
    {
        if (command.CommandText.StartsWith("CREATE TABLE"))
        {
            command.CommandText += @";
                INSERT INTO GiftLists (ListName, Description) VALUES ('Birthday Gifts', 'Gifts for upcoming birthdays');
                INSERT INTO GiftLists (ListName, Description) VALUES ('Holiday Gifts', 'Presents for the holiday season');";
        }
    }
}