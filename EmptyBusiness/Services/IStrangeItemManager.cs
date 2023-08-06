namespace EmptyBusiness.Services;

using EmptyDAL.Abstract;
using Extensions;
using Models;

public interface IStrangeItemManager
{
    Task CreateAsync(IReadOnlyCollection<StrangeItemBusinessModel> strangeItems,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<StrangeItemBusinessModel>> GetAsync(int from, int count, int findCode, string findValue, int findId,
        CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(int findCode, string findValue, int findId,
        CancellationToken cancellationToken = default);
}

public class StrangeItemManager : IStrangeItemManager
{
    private readonly IStrangeItemRepository strangeItemRepository;

    public StrangeItemManager(IStrangeItemRepository strangeItemRepository)
    {
        this.strangeItemRepository = strangeItemRepository;
    }

    public async Task CreateAsync(IReadOnlyCollection<StrangeItemBusinessModel> strangeItems,
        CancellationToken cancellationToken = default)
    {
        this.strangeItemRepository.RemoveAllAsync();

        var items = strangeItems.Select(x => x.ToEntityModel()).OrderBy(x => x.Code).ToArray();

        await this.strangeItemRepository.CreateAsync(items, cancellationToken);
    }

    public async Task<IReadOnlyCollection<StrangeItemBusinessModel>> GetAsync(int from, int count, int findCode, string findValue, int findId,
        CancellationToken cancellationToken = default)
    {
        var result = await this.strangeItemRepository.GetByFilterAsync(from, count, findCode, findValue, findId, cancellationToken);

        return result.Select(x => x.ToBusinessModel()).ToList();
    }

    public async Task<int> GetCountAsync(int findCode, string findValue, int findId,
        CancellationToken cancellationToken = default)
    {
        return await this.strangeItemRepository.GetCountAsync(findCode, findValue, findId, cancellationToken);
    }
}