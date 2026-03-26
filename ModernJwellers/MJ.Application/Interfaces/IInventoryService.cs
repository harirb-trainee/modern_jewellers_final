using Microsoft.AspNetCore.Mvc;
using MJ.Domain.ViewModels;

namespace MJ.Application.Interfaces;

public interface IInventoryService
{
    public List<ItemVM> GetItemList(int pageNumber, int pageSize, out int totalCount, int? categoryId = null, int? polishId = null, int? colorId = null, string? searchTerm = null, bool onlyLowStock = false);
    public  Task<InventoryDashboardVM> GetInventoryDashboardAsync();
    public Task<AddItemVM> GetItemById(string itemId);
    public Task<JsonResult> AddNewItem(AddItemVM itemVM);
    public Task<JsonResult> UpdateItem(AddItemVM itemVM);
    public Task<JsonResult> DeleteItem(string itemId);
}
