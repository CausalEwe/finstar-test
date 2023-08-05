using Microsoft.AspNetCore.Mvc;

namespace EmptyUI.Controllers;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmptyDAL.Interfaces;
using Extensions;
using Models;

[Route("api/strangeItems")]
public class StrangeItemController : Controller
{
    private readonly IStrangeItemManager strangeItemManager;

    public StrangeItemController(IStrangeItemManager strangeItemManager)
    {
        this.strangeItemManager = strangeItemManager;
    }

    [HttpGet]
    [Route("GetByFilter")]
    public async Task<IReadOnlyCollection<StrangeItemViewModel>> GetByFilter([FromQuery] StrangeItemFilter filter,
        CancellationToken cancellationToken = default)
    {
        var strangeItems = await this.strangeItemManager.GetAsync(filter.From, filter.Count, filter.FindCode, filter.FindValue, cancellationToken);

        return strangeItems.Select(x => x.ToViewModel()).ToList();
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<StrangeItemViewModel> GetById(int id,
        CancellationToken cancellationToken = default)
    {
        var strangeItem = await this.strangeItemManager.GetByIdAsync(id, cancellationToken);

        return strangeItem?.ToViewModel();
    }

    [HttpPost]
    [Route("Create")]
    public async Task Create([FromBody] StrangeItemCreateModel[] strangeItems,
        CancellationToken cancellationToken = default)
    {
        var entityItems = strangeItems.Select(x => x.ToBusinessModel()).ToList();

        await this.strangeItemManager.CreateAsync(entityItems, cancellationToken);
    }
}