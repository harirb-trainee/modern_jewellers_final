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
    public async Task<IActionResult> InventoryPartial(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10,
        [FromQuery] int? categoryId = null,
        [FromQuery] int? polishId = null,
        [FromQuery] int? colorId = null,
        [FromQuery] string? searchTerm = null,
        [FromQuery] bool onlyLowStock = false)
    {  
         var data = await PrepareInventoryDashboard(pageNumber, pageSize, categoryId, polishId, colorId, searchTerm, onlyLowStock);

         ViewBag.CategoryList = _catelogService.GetCategoryList();
         ViewBag.PolishList = _catelogService.GetPolishList();
         ViewBag.ColorList = _catelogService.GetColorList();

        return PartialView(data);
    }

    public async Task<IActionResult> InventoryTablePartial(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10,
        [FromQuery] int? categoryId = null,
        [FromQuery] int? polishId = null,
        [FromQuery] int? colorId = null,
        [FromQuery] string? searchTerm = null,
        [FromQuery] bool onlyLowStock = false)
    {  
         var data = await PrepareInventoryDashboard(pageNumber, pageSize, categoryId, polishId, colorId, searchTerm, onlyLowStock);
         return PartialView("_InventoryTable", data);
    }

    private async Task<InventoryDashboardVM> PrepareInventoryDashboard(int pageNumber, int pageSize, int? categoryId, int? polishId, int? colorId, string? searchTerm, bool onlyLowStock)
    {
         if (pageNumber < 1) pageNumber = 1;
         if (pageSize < 1) pageSize = 10;
         if (pageSize > 100) pageSize = 100;
         
         var data = await _inventoryService.GetInventoryDashboardAsync();
         int totalCount;
         data.ItemList = _inventoryService.GetItemList(pageNumber, pageSize, out totalCount, categoryId, polishId, colorId, searchTerm, onlyLowStock);
         
         data.CurrentPage = pageNumber;
         data.PageSize = pageSize;
         data.TotalCount = totalCount;
         data.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

         data.SelectedCategoryId = categoryId;
         data.SelectedPolishId = polishId;
         data.SelectedColorId = colorId;
         data.SearchTerm = searchTerm;
         data.OnlyLowStock = onlyLowStock;

         return data;
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
        var data = _catelogService.GetPolishList(onlyActive: true);
        return PartialView("_PolishListPartial", data);
    }
    public IActionResult LoadCategoryList()
    {
        var data = _catelogService.GetCategoryList(onlyActive: true);
        return PartialView("_CategoryListPartial", data);
    }
    public IActionResult LoadSubCategoryList()
    {
        var data = _catelogService.GetSubCategoryList(onlyActive: true);
        return PartialView("_SubCategoryList", data);
    }

    public IActionResult LoadStoneList()
    {
        var data = _catelogService.GetStoneList(onlyActive: true);
        return PartialView("_StoneListPartial", data);
    }

    public IActionResult LoadFittingList()
    {
        var data = _catelogService.GetFittingList(onlyActive: true);
        return PartialView("_FittingListPartial", data);
    }

    public IActionResult LoadPatternList()
    {
        var data = _catelogService.GetPatternList(onlyActive: true);
        return PartialView("_PatternListPartial", data);
    }

    public IActionResult LoadColorList()
    {
        var data = _catelogService.GetColorList(onlyActive: true);
        return PartialView("_ColorListPartial", data);
    }

    // Keeping placeholders for old UI if needed, but pointing them to new logic or similar
    public IActionResult LoadMaterialList() => LoadStoneList();
    public IActionResult LoadAttributeList() => LoadFittingList();

    #region Stone
    [HttpGet]
    public IActionResult LoadAddStoneModal(int id = 0)
    {
        StoneTypeVM model;
        if (id > 0)
        {
            var stone = _catelogService.GetStoneById(id);
            model = new StoneTypeVM { Id = stone.StoneTypeId, Name = stone.Name, Description = stone.Description, IsActive = stone.Status ?? true };
        }
        else model = new StoneTypeVM();
        return PartialView("_AddStoneModal", model);
    }

    [HttpPost]
    public async Task<IActionResult> SaveStone([FromBody] StoneTypeVM model)
    {
        if (model == null) return Json(new { success = false, message = "Invalid data." });
        return model.Id > 0 ? await _catelogService.UpdateStoneAsync(model) : await _catelogService.CreateStoneAsync(model);
    }
    #endregion

    #region Fitting
    [HttpGet]
    public IActionResult LoadAddFittingModal(int id = 0)
    {
        FittingTypeVM model;
        if (id > 0)
        {
            var fitting = _catelogService.GetFittingById(id);
            model = new FittingTypeVM { Id = fitting.FittingTypeId, Name = fitting.Name, Description = fitting.Description, IsActive = fitting.Status ?? true };
        }
        else model = new FittingTypeVM();
        return PartialView("_AddFittingModal", model);
    }

    [HttpPost]
    public async Task<IActionResult> SaveFitting([FromBody] FittingTypeVM model)
    {
        if (model == null) return Json(new { success = false, message = "Invalid data." });
        return model.Id > 0 ? await _catelogService.UpdateFittingAsync(model) : await _catelogService.CreateFittingAsync(model);
    }
    #endregion

    #region Pattern
    [HttpGet]
    public IActionResult LoadAddPatternModal(int id = 0)
    {
        PatternVM model;
        if (id > 0)
        {
            var pattern = _catelogService.GetPatternById(id);
            model = new PatternVM { Id = pattern.PatternId, Name = pattern.Name, Description = pattern.Description, IsActive = pattern.Status ?? true };
        }
        else model = new PatternVM();
        return PartialView("_AddPatternModal", model);
    }

    [HttpPost]
    public async Task<IActionResult> SavePattern([FromBody] PatternVM model)
    {
        if (model == null) return Json(new { success = false, message = "Invalid data." });
        return model.Id > 0 ? await _catelogService.UpdatePatternAsync(model) : await _catelogService.CreatePatternAsync(model);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteStone(int id) => await _catelogService.DeleteStoneAsync(id);

    [HttpDelete]
    public async Task<IActionResult> DeleteFitting(int id) => await _catelogService.DeleteFittingAsync(id);

    [HttpDelete]
    public async Task<IActionResult> DeletePattern(int id) => await _catelogService.DeletePatternAsync(id);

    #endregion

    #region Color
    [HttpGet]
    public IActionResult LoadAddColorModal(int id = 0)
    {
        ColorVM model;
        if (id > 0)
        {
            var color = _catelogService.GetColorById(id);
            model = new ColorVM { Id = color.ColorId, Name = color.Name, HexCode = color.HexCode, IsActive = color.Status ?? true };
        }
        else model = new ColorVM();
        return PartialView("_AddColorModal", model);
    }

    [HttpPost]
    public async Task<IActionResult> SaveColor([FromBody] ColorVM model)
    {
        if (model == null) return Json(new { success = false, message = "Invalid data." });
        return model.Id > 0 ? await _catelogService.UpdateColorAsync(model) : await _catelogService.CreateColorAsync(model);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteColor(int id) => await _catelogService.DeleteColorAsync(id);

    #endregion

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
                // Status = polishType.Status == true ? "active" : "inactive",
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
    public async Task<IActionResult> LoadAddItemModal(string id = null)
    {
        AddItemVM model = new AddItemVM();
        if (!string.IsNullOrEmpty(id))
        {
            model = await _inventoryService.GetItemById(id);
        }
        
        ViewBag.CategoryList = _catelogService.GetCategoryList(onlyActive: true);
        ViewBag.PolishList = _catelogService.GetPolishList(onlyActive: true);
        ViewBag.ColorList = _catelogService.GetColorList(onlyActive: true);
        ViewBag.SubCategoryList = _catelogService.GetSubCategoryList(onlyActive: true);
        ViewBag.Stones = _catelogService.GetStoneList(onlyActive: true);
        ViewBag.Fittings = _catelogService.GetFittingList(onlyActive: true);
        ViewBag.Patterns = _catelogService.GetPatternList(onlyActive: true);
        
        return PartialView("_AddItemModal", model);
    }

    [HttpPost]
    public async Task<IActionResult> AddEditItem([FromForm] AddItemVM model, bool isEdit = false)
    {
        if (model == null)
        {
            return Json(new { success = false, message = "Invalid data." });
        }

        if (isEdit)
        {
            return await _inventoryService.UpdateItem(model);
        }

        return await _inventoryService.AddNewItem(model);
    }

    public async Task<IActionResult> DeleteItem(string id)
    {
        var result = await _inventoryService.DeleteItem(id);
        return result;
    }

    [HttpGet]
    public async Task<IActionResult> LoadItemDetailsModal(string id)
    {
        var model = await _inventoryService.GetItemById(id);
        if (model == null) return NotFound();

        return PartialView("_ItemDetailsModal", model);
    }

    #endregion
    

}
