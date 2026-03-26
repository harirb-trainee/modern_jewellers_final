namespace MJ.Domain.ViewModels;

public class InventoryDashboardVM
{
    public List<ItemVM> ItemList { get; set; }
    public int TotalItems { get; set; }
    public int LowStockItems { get; set; }

    public int TotalCategories { get; set; }
    public decimal TotalInventoryValue { get; set; }

    // Pagination properties
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    // Filter properties
    public int? SelectedCategoryId { get; set; }
    public int? SelectedPolishId { get; set; }
    public int? SelectedColorId { get; set; }
    public string? SearchTerm { get; set; }
    public bool OnlyLowStock { get; set; } = false;
}
