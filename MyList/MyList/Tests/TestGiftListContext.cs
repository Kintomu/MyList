using Microsoft.EntityFrameworkCore;
using MyList.Data_Access;

namespace MyList.Tests;

public class TestGiftListContext : GiftListContext
{
    public TestGiftListContext(DbContextOptions<TestGiftListContext> options) : base(options)
    {
    }
}