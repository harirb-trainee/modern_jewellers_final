using Microsoft.AspNetCore.Mvc;

namespace MJ.Controllers;

public class CatelogController : Controller
{
    public IActionResult CatalogPartial() => PartialView("_CatalogPartial");

    public IActionResult LoadPolishList()
    {
        // var data = _context.PolishTypes.ToList();
        return PartialView("_PolishListPartial");
    }

    // public IActionResult LoadCategoryList()
    // {
    //     var data = _context.Categories.ToList();
    //     return PartialView("_CategoryListPartial", data);
    // }

    // public IActionResult LoadAttributeList()
    // {
    //     var data = _context.Attributes.ToList();
    //     return PartialView("_AttributeListPartial", data);
    // }

    // public IActionResult LoadMaterialList()
    // {
    //     var data = _context.Materials.ToList();
    //     return PartialView("_MaterialListPartial", data);
    // }
}
