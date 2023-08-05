namespace EmptyBusiness.Extensions;

using EmptyDAL.Entities;
using Models;

public static class StrangeItemConvertingExtensionMethods
{
    public static StrangeItem ToEntityModel(this StrangeItemBusinessModel strangeItem)
    {
        return new StrangeItem
        {
            Code = strangeItem.Code,
            Value = strangeItem.Value,
        };
    }

    public static StrangeItemBusinessModel ToBusinessModel(this StrangeItem strangeItem)
    {
        return new StrangeItemBusinessModel
        {
            Id = strangeItem.Id,
            Code = strangeItem.Code,
            Value = strangeItem.Value,
        };
    }
}