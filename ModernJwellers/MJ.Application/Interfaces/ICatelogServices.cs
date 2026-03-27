using Microsoft.AspNetCore.Mvc;
using MJ.Domain.ViewModels;

namespace MJ.Application.Interfaces;
public interface ICatelogServices
{

    #region Polish
    public List<PolishType> GetPolishList(bool onlyActive = false);
    public PolishType GetPolishByIdAsync(int? id);
    public Task<JsonResult> CreatePolishAsync(PolishTypeVM model);
    public Task<JsonResult> UpdatePolishAsync(PolishTypeVM model);
    public Task<JsonResult> DeletePolishAsync(int? id);
    #endregion

    #region ProductHead
    public List<ProductHead> GetCategoryList(bool onlyActive = false);
    public ProductHead GetCategoryByIdAsync(int? id);
    public Task<JsonResult> CreateCategoryAsync(ProductHeadVM model);
    public Task<JsonResult> UpdateCategoryAsync(ProductHeadVM model);
    public Task<JsonResult> DeleteCategoryAsync(int? id);
    #endregion

    #region ProductHeadType
    public List<ProductHeadTypeVM> GetSubCategoryList(bool onlyActive = false);
    public ProductHeadTypeVM GetSubCategoryByIdAsync(int? id);
    public Task<JsonResult> CreateSubCategoryAsync(ProductHeadTypeVM model);
    public Task<JsonResult> UpdateSubCategoryAsync(ProductHeadTypeVM model);
    public Task<JsonResult> DeleteSubCategoryAsync(int? id);

    #endregion
    #region Color
    public List<Color> GetColorList(bool onlyActive = false);
    public Color GetColorById(int id);
    public Task<JsonResult> CreateColorAsync(ColorVM model);
    public Task<JsonResult> UpdateColorAsync(ColorVM model);
    public Task<JsonResult> DeleteColorAsync(int id);
    #endregion

    #region Stone
    public List<StoneType> GetStoneList(bool onlyActive = false);
    public StoneType GetStoneById(int id);
    public Task<JsonResult> CreateStoneAsync(StoneTypeVM model);
    public Task<JsonResult> UpdateStoneAsync(StoneTypeVM model);
    public Task<JsonResult> DeleteStoneAsync(int id);
    #endregion

    #region Fitting
    public List<FittingType> GetFittingList(bool onlyActive = false);
    public FittingType GetFittingById(int id);
    public Task<JsonResult> CreateFittingAsync(FittingTypeVM model);
    public Task<JsonResult> UpdateFittingAsync(FittingTypeVM model);
    public Task<JsonResult> DeleteFittingAsync(int id);
    #endregion

    #region Pattern
    public List<Pattern> GetPatternList(bool onlyActive = false);
    public Pattern GetPatternById(int id);
    public Task<JsonResult> CreatePatternAsync(PatternVM model);
    public Task<JsonResult> UpdatePatternAsync(PatternVM model);
    public Task<JsonResult> DeletePatternAsync(int id);
    #endregion

}
