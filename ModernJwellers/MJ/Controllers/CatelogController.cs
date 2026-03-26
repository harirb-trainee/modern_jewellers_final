using Microsoft.AspNetCore.Mvc;
using MJ.Application.Interfaces;
using MJ.Domain.ViewModels;

namespace MJ.Controllers;

public class CatelogController : Controller
{
    private readonly ICatelogServices _catelogService;
    public CatelogController(ICatelogServices catelogService)
    {
        _catelogService = catelogService;
    }

    public IActionResult CatalogPartial() => PartialView("_CatalogPartial");

    #region Stone
    public IActionResult StonePartial()
    {
        var data = _catelogService.GetStoneList();
        return PartialView("_StoneListPartial", data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStone(StoneTypeVM model)
    {
        return await _catelogService.CreateStoneAsync(model);
    }
    #endregion

    #region Fitting
    public IActionResult FittingPartial()
    {
        var data = _catelogService.GetFittingList();
        return PartialView("_FittingListPartial", data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFitting(FittingTypeVM model)
    {
        return await _catelogService.CreateFittingAsync(model);
    }
    #endregion

    #region Pattern
    public IActionResult PatternPartial()
    {
        var data = _catelogService.GetPatternList();
        return PartialView("_PatternListPartial", data);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePattern(PatternVM model)
    {
        return await _catelogService.CreatePatternAsync(model);
    }
    #endregion
}
