using Microsoft.EntityFrameworkCore;
using MyGiftList.Models;

namespace MyGiftList.Data_Access;

public class GiftListContext : DbContext
{
    public DbSet<GiftList> GiftLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=GiftList.db");
    }

}