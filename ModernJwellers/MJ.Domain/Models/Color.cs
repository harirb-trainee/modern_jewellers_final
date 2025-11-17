using System;
using System.Collections.Generic;

namespace MJ;

public partial class Color
{
    public int ColorId { get; set; }

    public string Name { get; set; } = null!;

    public string? HexCode { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
