namespace EmptyDAL.Entities;

public class StrangeItem : IEntityBase
{
    public int Id { get; set; }

    public int Code { get; set; }

    public string Value { get; set; }
}