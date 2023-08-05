namespace EmptyDAL.Interfaces;

using Abstract;
using EmptyBusiness.Extensions;
using EmptyBusiness.Models;
using Entities;

public interface IStrangeItemManager
{
    Task CreateAsync(IReadOnlyCollection<StrangeItemBusinessModel> strangeItems,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<StrangeItemBusinessModel>> GetAsync(int from, int count, int findCode, string findValue,
        CancellationToken cancellationToken = default);

    Task<StrangeItemBusinessModel> GetByIdAsync(int id,
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

    public async Task<IReadOnlyCollection<StrangeItemBusinessModel>> GetAsync(int from, int count, int findCode, string findValue,
        CancellationToken cancellationToken = default)
    {
        var result = await this.strangeItemRepository.GetByFilterAsync(from, count, findCode, findValue, cancellationToken);

        return result.Select(x => x.ToBusinessModel()).ToList();
    }

    public async Task<StrangeItemBusinessModel> GetByIdAsync(int id,
        CancellationToken cancellationToken = default)
    {
        var item = await this.strangeItemRepository.GetByIdAsync(id, cancellationToken);

        return item?.ToBusinessModel();
    }
}