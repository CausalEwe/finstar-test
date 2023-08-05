namespace EmptyDAL.Repositories;

using System.Linq.Expressions;
using Abstract;
using Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;

public class StrangeItemRepository : IStrangeItemRepository
{
    private readonly StrangeItemDbContext context;

    public StrangeItemRepository(StrangeItemDbContext context)
    {
        this.context = context;
    }

    public async Task CreateAsync(StrangeItem[] strangeItems,
        CancellationToken cancellationToken = default)
    {
        await this.context.StrangeItems.AddRangeAsync(strangeItems, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public void RemoveAllAsync()
    {
        //todo: context.StrangeItems.ExecuteSqlCommand("TRUNCATE TABLE [StrangeItems]");

        this.context.StrangeItems.RemoveRange(this.context.StrangeItems);
    }

    public async Task<IReadOnlyCollection<StrangeItem>> GetByFilterAsync(int from, int count, int findCode, string findValue,
        CancellationToken cancellationToken = default)
    {
        IQueryable<StrangeItem> strangeItems = this.context.StrangeItems;

        if (findCode != 0)
        {
            strangeItems = strangeItems.Where(x => x.Code == findCode);
        }

        if (!string.IsNullOrEmpty(findValue))
        {
            strangeItems = strangeItems.Where(x => x.Value.Contains(findValue));
        }

        //todo: не было идей фильтрации, если позволяет БЛ желательно избавиться от contains

        return await strangeItems.Skip(from).Take(count).ToListAsync(cancellationToken);
    }

    public async Task<StrangeItem> GetByIdAsync(int id,
        CancellationToken cancellationToken = default)
    {
        return await this.context.StrangeItems.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}