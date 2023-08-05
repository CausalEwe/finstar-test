namespace EmptyDAL;

using Entities;
using Microsoft.EntityFrameworkCore;

public class StrangeItemDbContext : DbContext
{
    public StrangeItemDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<StrangeItem> StrangeItems { get; set; }
}