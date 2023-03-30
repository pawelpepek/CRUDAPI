﻿namespace CRUDAPI.Entities;

public class ProductAmount:IIdentifiable
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal Amount { get; set; }

    public virtual Product Product { get; set; }
    public virtual Order Order { get; set; }
}
