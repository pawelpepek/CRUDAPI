﻿using CRUDAPI.Entities.Enums;
using CrudCore.Objects;
using System.ComponentModel;

namespace CRUDAPI.Entities;

[DisplayName("produkt")]
public class Product : IIdentifiable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public decimal Price { get; set; }
    public ProductQulity Quality { get; set; }

    public virtual List<ProductAmount> ProductAmounts { get; set; }
}
