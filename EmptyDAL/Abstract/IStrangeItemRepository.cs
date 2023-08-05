namespace EmptyDAL.Abstract;

using Entities;

public interface IStrangeItemRepository
{
    Task CreateAsync(StrangeItem[] strangeItems,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<StrangeItem>> GetByFilterAsync(int from, int count, int findCode, string findValue,
        CancellationToken cancellationToken = default);

    Task<StrangeItem> GetByIdAsync(int id,
        CancellationToken cancellationToken = default);

    public void RemoveAllAsync();
}