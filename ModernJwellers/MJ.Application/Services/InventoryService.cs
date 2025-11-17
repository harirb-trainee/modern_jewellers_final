namespace MJ.Application.Services;
using MJ.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MJ.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

public class InventoryService : IInventoryService
{
    private readonly MJDbContext _context;

    public InventoryService(MJDbContext context)
    {
        _context = context;
    }

    public List<ItemVM> GetItemList()
    {
        var items = _context.Items.Select(i => new ItemVM
        {
            ItemId = i.ItemId,
            ItemName = i.ItemName,
            PolishType = i.ItemPolishNavigation.Name ?? string.Empty,
            PolishTypeId = i.ItemPolishNavigation.PolishTypeId,
            CategoryId = i.ItemCategoryNavigation.ProductHeadId,
            CategoryName = i.ItemCategoryNavigation.Name ?? string.Empty,
            subcategoryId = i.ItemSubCategoryNavigation.ProductHeadTypeId,
            subCategoryName = i.ItemSubCategoryNavigation.Name ?? string.Empty,
            Item_color = i.ColorNavigation.Name ?? string.Empty,
            Item_Quantity = i.ItemQuantity ?? 0,
            Item_price = i.ItemPrice,
            Status = true,
            CreatedBy = i.CreatedBy,
            CreatedDate = DateTime.Now,
        }).ToList();

        return items;
    }

    public async Task<InventoryDashboardVM> GetInventoryDashboardAsync()
    {
        var totalItems = await _context.Items.CountAsync();
        var totalCategories = await _context.ProductHeads.CountAsync();
        var totalSubCategories = await _context.ProductHeadTypes.CountAsync();
        var lowStockItems = await _context.Items.Where(i => i.ItemQuantity < 10).CountAsync();
        var total_invntory_value = await _context.Items.SumAsync(i => i.ItemPrice * (i.ItemQuantity ?? 0));

        return new InventoryDashboardVM
        {
            TotalItems = totalItems,
            TotalCategories = totalCategories,
            TotalInventoryValue = total_invntory_value,
            // TotalSubCategories = totalSubCategories,
            LowStockItems = lowStockItems
        };
    }

    public async Task<JsonResult> DeleteItem(int itemId)
    {
        var item = await _context.Items.FindAsync(itemId);
        if (item == null)
        {
            return new JsonResult(new { success = false, message = "Item not found." });
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();

        return new JsonResult(new { success = true, message = "Item deleted successfully." });
    }
}
