namespace EmptyDAL.Abstract;

using Entities;

public interface IStrangeItemRepository
{
    Task CreateAsync(StrangeItem[] strangeItems,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<StrangeItem>> GetByFilterAsync(int from, int count, int findCode, string findValue, int findId,
        CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(int findCode, string findValue, int findId,
        CancellationToken cancellationToken = default);

    public void RemoveAllAsync();
}