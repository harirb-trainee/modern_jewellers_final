using System;
using System.Collections.Generic;

namespace MJ;

public partial class Item
{
    public string ItemId { get; set; } = null!;

    public string ItemName { get; set; } = null!;

    public decimal ItemPrice { get; set; }

    public int? ItemQuantity { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string? PhotoUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Color { get; set; }

    public int? ItemPolish { get; set; }

    public int? ItemCategory { get; set; }

    public int? ItemSubCategory { get; set; }

    public virtual Color? ColorNavigation { get; set; }

    public virtual ProductHead? ItemCategoryNavigation { get; set; }

    public virtual PolishType? ItemPolishNavigation { get; set; }

    public virtual ProductHeadType? ItemSubCategoryNavigation { get; set; }
}
