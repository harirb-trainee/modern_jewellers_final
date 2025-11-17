using Microsoft.AspNetCore.Mvc;
using MJ.Application.Interfaces;
using MJ.Domain.ViewModels;
namespace MJ.Controllers;

public class InventoryController : Controller
{
    private readonly ICatelogServices _catelogService;
    private readonly IInventoryService _inventoryService;
    public InventoryController(ICatelogServices catelogService, IInventoryService inventoryService)
    {
        _catelogService = catelogService;
        _inventoryService = inventoryService;
    }
   
    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> InventoryPartial()
    {  
         var data = await _inventoryService.GetInventoryDashboardAsync();
         data.ItemList = _inventoryService.GetItemList();
        return PartialView(data);
    }
    public IActionResult CatelogPartial()
    {
        return PartialView();
    }

    public IActionResult CategoriesPartial()
    {
        return PartialView();
    }

    public IActionResult ReportsPartial()
    {
        return PartialView();
    }

    public IActionResult StaffPartial()
    {
        return PartialView();
    }

    public IActionResult LoadPolishList()
    {
        var data = _catelogService.GetPolishList();
        return PartialView("_PolishListPartial", data);
    }
    public IActionResult LoadCategoryList()
    {
        var data = _catelogService.GetCategoryList();
        return PartialView("_CategoryListPartial", data);
    }
    public IActionResult LoadSubCategoryList()
    {
        var data = _catelogService.GetSubCategoryList();
        return PartialView("_SubCategoryList", data);
    }

    public IActionResult LoadMaterialList()
    {
        var data = _catelogService.GetPolishList();
        return PartialView("_MaterialListPartial", data);
    }
    public IActionResult LoadAttributeList()
    {
        var data = _catelogService.GetPolishList();
        return PartialView("_AttributeListPartial", data);
    }

    #region Polish
    [HttpGet]
    public IActionResult LoadAddPolishModal(int id = 0)
    {
        PolishTypeVM model;

        if (id > 0)
        {
            var polishType = _catelogService.GetPolishByIdAsync(id);
            model = new PolishTypeVM
            {
                Id = polishType.PolishTypeId,
                PolishName = polishType.Name,
                Description = polishType.Description,
                Status = polishType.Status == true ? "active" : "inactive",
            };
        }
        else
        {
            model = new PolishTypeVM();
        }

        return PartialView("_AddPolishModal", model);
    }

    [HttpPost]
    public async Task<IActionResult> SavePolish([FromBody] PolishTypeVM model)
    {
        if (model == null)
        {
            return Json(new { success = false, message = "Invalid data." });
        }
        if (model.Id > 0)
        {

            return await _catelogService.UpdatePolishAsync(model); ;
        }

        else
        {
            return await _catelogService.CreatePolishAsync(model);
        }
    }

    public async Task<IActionResult> DeletePolish(int id)
    {
        var result = await _catelogService.DeletePolishAsync(id);
        return result;
    }

    #endregion

    #region ProductHead
    [HttpGet]
    public IActionResult LoadCategoryModal(int id = 0)
    {
        try
        {

            ProductHeadVM model;

            if (id > 0)
            {
                var productHead = _catelogService.GetCategoryByIdAsync(id);
                model = new ProductHeadVM
                {
                    ProductHeadId = productHead.ProductHeadId,
                    Name = productHead.Name,
                    Description = productHead.Description != null ? productHead.Description : "null",
                    IsActive = productHead.Status == true ? true : false,
                };
            }
            else
            {
                model = new ProductHeadVM();
            }

            return PartialView("_AddCategoryModel", model);
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            Console.WriteLine($"Error in LoadCategoryModal: {ex.Message}");
            return StatusCode(500, "An error occurred while loading the modal.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddEditCategory([FromBody] ProductHeadVM model)
    {

        if (model == null)
        {
            return Json(new { success = false, message = "Invalid data." });
        }
        if (model.ProductHeadId > 0)
        {

            return await _catelogService.UpdateCategoryAsync(model); ;
        }

        else
        {
            return await _catelogService.CreateCategoryAsync(model);
        }
    }

    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _catelogService.DeleteCategoryAsync(id);
        return result;
    }

    #endregion

    #region ProductHead Type
    [HttpGet]
    public IActionResult LoadSubCategoryModal(int id = 0)
    {
        try
        {
            ProductHeadTypeVM model;
            if (id > 0)
            {
                model = _catelogService.GetSubCategoryByIdAsync(id);
            }
            else
            {
                model = new ProductHeadTypeVM();
            }
            ViewBag.CategoryList = _catelogService.GetCategoryList();
            return PartialView("_AddSubCategoryModal", model);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in LoadCategoryModal: {ex.Message}");
            return StatusCode(500, "An error occurred while loading the modal.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddEditSubCategory([FromBody] ProductHeadTypeVM model)
    {

       if (model == null)
        {
            return Json(new { success = false, message = "Values Missing or Parent Category is null." });
        }
        if (model.Id > 0)
        {

            return await _catelogService.UpdateSubCategoryAsync(model); ;
        }

        else
        {
            return await _catelogService.CreateSubCategoryAsync(model);
        }
    }
    public async Task<IActionResult> DeleteSubCategory(int id)
    {
        var result = await _catelogService.DeleteSubCategoryAsync(id);
        return result;
    }

    #endregion

    #region Item
    [HttpGet]
    public IActionResult LoadAddItemModal(int id = 0)
    {
        return PartialView("_AddItemModal");
    }

    [HttpPost]
    public async Task<IActionResult> AddEditItem([FromBody] ProductHeadTypeVM model)
    {
        return Json(new { success = false, message = "Not Implemented Yet.Send mssg to Hari to confirm implementation" });

    //    if (model == null)
    //     {
    //         return Json(new { success = false, message = "Values Missing or Parent Category is null." });
    //     }
    //     if (model.Id > 0)
    //     {

    //         return await _catelogService.UpdateSubCategoryAsync(model); ;
    //     }

    //     else
    //     {
    //         return await _catelogService.CreateSubCategoryAsync(model);
    //     }
    }
    public async Task<IActionResult> DeleteItem(int id)
    {
        var result = await _inventoryService.DeleteItem(id);
        return result;
    }

    #endregion
    

}
