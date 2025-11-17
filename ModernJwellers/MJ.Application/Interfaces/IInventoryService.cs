using Microsoft.AspNetCore.Mvc;
using MJ.Domain.ViewModels;

namespace MJ.Application.Interfaces;

public interface IInventoryService
{
    public List<ItemVM> GetItemList();
    public  Task<InventoryDashboardVM> GetInventoryDashboardAsync();
    // public Task<ItemVM> GetItemDetailsById(int itemId);
    // public Task<JsonResult> UpdateItem(ItemVM itemVM);
    // public Task<JsonResult> AddNewItem(ItemVM itemVM);
    public Task<JsonResult> DeleteItem(int itemId);
}
