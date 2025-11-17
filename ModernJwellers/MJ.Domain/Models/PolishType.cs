using System;
using System.Collections.Generic;

namespace MJ;

public partial class PolishType
{
    public int PolishTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
