namespace MJ.Domain.ViewModels;

public class InventoryDashboardVM
{
    public List<ItemVM> ItemList { get; set; }
    public int TotalItems { get; set; }
    public int LowStockItems { get; set; }

    public int TotalCategories { get; set; }
    public decimal TotalInventoryValue { get; set; }

}
