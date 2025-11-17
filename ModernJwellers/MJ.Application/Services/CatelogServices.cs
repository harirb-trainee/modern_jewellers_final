using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MJ.Application.Interfaces;
using MJ.Domain.ViewModels;

namespace MJ.Application.Services;

public class CatelogServices : ICatelogServices
{
    private readonly MJDbContext _context;
    public CatelogServices(MJDbContext context)
    {
        _context = context;
    }

    #region Polish
    public List<PolishType> GetPolishList()
    {
        return _context.PolishTypes.OrderBy(p => p.PolishTypeId).ToList();
    }
    public PolishType GetPolishByIdAsync(int? id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid PolishType ID.", nameof(id));
        }

        var data = _context.PolishTypes.FirstOrDefault(x => x.PolishTypeId == id);

        if (data == null)
        {
            throw new KeyNotFoundException($"PolishType with ID {id} was not found.");
        }

        return data;
    }
    public async Task<JsonResult> CreatePolishAsync(PolishTypeVM model)
    {
        var isPolishExixted = await _context.PolishTypes.FirstOrDefaultAsync(x => x.Name == model.PolishName.Trim());
        if (isPolishExixted != null)
        {
            return new JsonResult(new { success = false, message = "Polish type already exists." });
        }
        var newPolish = new PolishType
        {
            Name = model.PolishName,
            Description = model.Description,
            Status = model.Status == "active" ? true : false,
        };

        _context.PolishTypes.Add(newPolish);
        await _context.SaveChangesAsync();
        return new JsonResult(new { success = true, message = "Polish type created successfully." });
    }

    public async Task<JsonResult> UpdatePolishAsync(PolishTypeVM model)
    {
        var existingPolish = await _context.PolishTypes.FirstOrDefaultAsync(x => x.PolishTypeId == model.Id);
        if (existingPolish == null)
        {
            return new JsonResult(new { success = false, message = "Polish type not found." });
        }

        var isPolishExixted = await _context.PolishTypes.FirstOrDefaultAsync(x => x.Name.ToLower() == model.PolishName.ToLower().Trim() && x.PolishTypeId != model.Id);
        if (isPolishExixted != null)
        {
            return new JsonResult(new { success = false, message = "Polish type already exists." });
        }

        existingPolish.Name = model.PolishName;
        existingPolish.Description = model.Description;
        existingPolish.Status = model.Status == "active" ? true : false;

        _context.PolishTypes.Update(existingPolish);
        await _context.SaveChangesAsync();
        return new JsonResult(new { success = true, message = "Polish type updated successfully." });
    }

    public async Task<JsonResult> DeletePolishAsync(int? id)
    {
        var existingPolish = await _context.PolishTypes.FirstOrDefaultAsync(x => x.PolishTypeId == id);
        if (existingPolish == null)
        {
            return new JsonResult(new { success = false, message = "Polish type not found." });
        }

        _context.PolishTypes.Remove(existingPolish);
        await _context.SaveChangesAsync();
        return new JsonResult(new { success = true, message = "Polish type deleted successfully." });
    }
    #endregion

    #region ProductHead
    public List<ProductHead> GetCategoryList()
    {
        return _context.ProductHeads.OrderBy(p => p.ProductHeadId).ToList();
    }

    public ProductHead GetCategoryByIdAsync(int? id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid ProductHead ID.", nameof(id));
        }

        var data = _context.ProductHeads.FirstOrDefault(x => x.ProductHeadId == id);

        if (data == null)
        {
            throw new KeyNotFoundException($"ProductHead with ID {id} was not found.");
        }

        return data;
    }

    public async Task<JsonResult> CreateCategoryAsync(ProductHeadVM model)
    {
        var isCategoryExixted = await _context.ProductHeads.FirstOrDefaultAsync(x => x.Name == model.Name.Trim());
        if (isCategoryExixted != null)
        {
            return new JsonResult(new { success = false, message = "ProductHead already exists." });
        }
        var newCategory = new ProductHead
        {
            Name = model.Name,
            Description = model.Description,
            Status = model.IsActive,
        };

        _context.ProductHeads.Add(newCategory);
        await _context.SaveChangesAsync();
        return new JsonResult(new { success = true, message = "ProductHead created successfully." });
    }
    public async Task<JsonResult> UpdateCategoryAsync(ProductHeadVM model)
    {
        var existingCategory = await _context.ProductHeads.FirstOrDefaultAsync(x => x.ProductHeadId == model.ProductHeadId);
        if (existingCategory == null)
        {
            return new JsonResult(new { success = false, message = "ProductHead not found." });
        }

        var isCategoryExixted = await _context.ProductHeads.FirstOrDefaultAsync(x => x.Name.ToLower() == model.Name.ToLower().Trim() && x.ProductHeadId != model.ProductHeadId);
        if (isCategoryExixted != null)
        {
            return new JsonResult(new { success = false, message = "ProductHead already exists." });
        }

        existingCategory.Name = model.Name;
        existingCategory.Description = model.Description;
        existingCategory.Status = model.IsActive;
        _context.ProductHeads.Update(existingCategory);
        await _context.SaveChangesAsync();
        return new JsonResult(new { success = true, message = "ProductHead updated successfully." });
    }
    public async Task<JsonResult> DeleteCategoryAsync(int? id)
    {
        var existingCategory = await _context.ProductHeads.FirstOrDefaultAsync(x => x.ProductHeadId == id);
        if (existingCategory == null)
        {
            return new JsonResult(new { success = false, message = "ProductHead not found." });
        }

        _context.ProductHeads.Remove(existingCategory);
        await _context.SaveChangesAsync();
        return new JsonResult(new { success = true, message = "ProductHead deleted successfully." });
    }
    #endregion

    #region ProductHeadType
    public List<ProductHeadTypeVM> GetSubCategoryList()
    {
        var result = (from pht in _context.ProductHeadTypes
                      join ph in _context.ProductHeads on pht.ProductHeadId equals ph.ProductHeadId
                      select new Domain.ViewModels.ProductHeadTypeVM
                      {
                          Id = pht.ProductHeadTypeId,
                          Name = pht.Name,
                          Description = pht.Description,
                          IsActive = pht.Status == true ? true : false,
                          //   CreatedBy = ,
                          //   CreatedDate = pht.CreatedDate,
                          ParentCategoryName = ph.Name
                      }).OrderBy(x=>x.Id).ToList();
        return result;
    }


    public ProductHeadTypeVM GetSubCategoryByIdAsync(int? id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid ProductHeadType ID.", nameof(id));
        }

        var data = (from pht in _context.ProductHeadTypes
                    join ph in _context.ProductHeads on pht.ProductHeadId equals ph.ProductHeadId
                    where pht.ProductHeadTypeId == id
                    select new ProductHeadTypeVM
                    {
                        Id = pht.ProductHeadTypeId,
                        Name = pht.Name,
                        Description = pht.Description,
                        IsActive = true,
                        //   CreatedBy = ,
                        //   CreatedDate = pht.CreatedDate,
                        ParentCategoryName = ph.Name
                    }).FirstOrDefault();

        if (data == null)
        {
            throw new KeyNotFoundException($"ProductHeadType with ID {id} was not found.");
        }

        return data;
    }

    public async Task<JsonResult> CreateSubCategoryAsync(ProductHeadTypeVM model)
    {
        var isSubCategoryExixted = await _context.ProductHeadTypes.FirstOrDefaultAsync(x => x.Name == model.Name.Trim());
        if (isSubCategoryExixted != null)
        {
            return new JsonResult(new { success = false, message = "ProductHeadType already exists." });
        }
        var newSubCategory = new ProductHeadType
        {
            Name = model.Name,
            Description = model.Description,
            ProductHeadId = model.ProductHeadId,
            Status = model.IsActive,
            // CreatedBy = model.CreatedBy,
            // CreatedDate = model.CreatedDate

        };

        _context.ProductHeadTypes.Add(newSubCategory);
        await _context.SaveChangesAsync();
        return new JsonResult(new { success = true, message = "ProductHeadType created successfully." });
    }

    public async Task<JsonResult> UpdateSubCategoryAsync(ProductHeadTypeVM model)
    {
        var existingSubCategory = await _context.ProductHeadTypes.FirstOrDefaultAsync(x => x.ProductHeadTypeId == model.Id);
        if (existingSubCategory == null)
        {
            return new JsonResult(new { success = false, message = "ProductHeadType not found." });
        }

        var isSubCategoryExixted = await _context.ProductHeadTypes.FirstOrDefaultAsync(x => x.Name.ToLower() == model.Name.ToLower().Trim() && x.ProductHeadTypeId != model.Id);
        if (isSubCategoryExixted != null)
        {
            return new JsonResult(new { success = false, message = "ProductHeadType already exists." });
        }

        existingSubCategory.Name = model.Name;
        existingSubCategory.Description = model.Description;
        existingSubCategory.ProductHeadId = model.ProductHeadId;
        existingSubCategory.Status = model.IsActive;

        _context.ProductHeadTypes.Update(existingSubCategory);
        await _context.SaveChangesAsync();
        return new JsonResult(new { success = true, message = "ProductHeadType updated successfully." });
    }
    public async Task<JsonResult> DeleteSubCategoryAsync(int? id)
    {
        var existingSubCategory = await _context.ProductHeadTypes.FirstOrDefaultAsync(x => x.ProductHeadTypeId == id);
        if (existingSubCategory == null)
        {
            return new JsonResult(new { success = false, message = "ProductHeadType not found." });
        }

        _context.ProductHeadTypes.Remove(existingSubCategory);
        await _context.SaveChangesAsync();
        return new JsonResult(new { success = true, message = "ProductHeadType deleted successfully." });
    }
    #endregion


}
