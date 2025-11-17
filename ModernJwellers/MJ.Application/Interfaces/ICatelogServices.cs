using Microsoft.AspNetCore.Mvc;
using MJ.Domain.ViewModels;

namespace MJ.Application.Interfaces;
public interface ICatelogServices
{

    #region Polish
    public List<PolishType> GetPolishList();
    public PolishType GetPolishByIdAsync(int? id);
    public Task<JsonResult> CreatePolishAsync(PolishTypeVM model);
    public Task<JsonResult> UpdatePolishAsync(PolishTypeVM model);
    public Task<JsonResult> DeletePolishAsync(int? id);
    #endregion

    #region ProductHead
    public List<ProductHead> GetCategoryList();
    public ProductHead GetCategoryByIdAsync(int? id);
    public Task<JsonResult> CreateCategoryAsync(ProductHeadVM model);
    public Task<JsonResult> UpdateCategoryAsync(ProductHeadVM model);
    public Task<JsonResult> DeleteCategoryAsync(int? id);
    #endregion

    #region ProductHeadType
    public List<ProductHeadTypeVM> GetSubCategoryList();
    public ProductHeadTypeVM GetSubCategoryByIdAsync(int? id);
    public Task<JsonResult> CreateSubCategoryAsync(ProductHeadTypeVM model);
    public Task<JsonResult> UpdateSubCategoryAsync(ProductHeadTypeVM model);
    public Task<JsonResult> DeleteSubCategoryAsync(int? id);

    #endregion


}
