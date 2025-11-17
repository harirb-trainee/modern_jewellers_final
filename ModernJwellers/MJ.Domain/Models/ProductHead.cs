using System;
using System.Collections.Generic;

namespace MJ;

public partial class ProductHead
{
    public int ProductHeadId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<ProductHeadType> ProductHeadTypes { get; set; } = new List<ProductHeadType>();
}
