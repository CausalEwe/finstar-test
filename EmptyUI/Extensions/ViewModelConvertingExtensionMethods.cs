namespace EmptyUI.Extensions;

using EmptyBusiness.Models;
using EmptyDAL.Entities;
using Models;

public static class StrangeItemConvertingExtensionMethods
{
    public static StrangeItemBusinessModel ToBusinessModel(this StrangeItemCreateModel strangeItem)
    {
        return new StrangeItemBusinessModel
        {
            Code = strangeItem.Code,
            Value = strangeItem.Value,
        };
    }

    public static StrangeItemViewModel ToViewModel(this StrangeItemBusinessModel strangeItem)
    {
        return new StrangeItemViewModel
        {
            Id = strangeItem.Id,
            Code = strangeItem.Code,
            Value = strangeItem.Value,
        };
    }
}