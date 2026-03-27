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
    public List<PolishType> GetPolishList(bool onlyActive = false)
    {
        var query = _context.PolishTypes.AsQueryable();
        if (onlyActive) query = query.Where(p => p.Status == true);
        return query.OrderBy(p => p.PolishTypeId).ToList();
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
            Status =  true,
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
        // existingPolish.Status = model.Status == "active" ? true : false;

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
    public List<ProductHead> GetCategoryList(bool onlyActive = false)
    {
        var query = _context.ProductHeads.AsQueryable();
        if (onlyActive) query = query.Where(p => p.Status == true);
        return query.OrderBy(p => p.ProductHeadId).ToList();
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
    public List<ProductHeadTypeVM> GetSubCategoryList(bool onlyActive = false)
    {
        var query = _context.ProductHeadTypes.AsQueryable();
        if (onlyActive) query = query.Where(p => p.Status == true);

        var result = (from pht in query
                      join ph in _context.ProductHeads on pht.ProductHeadId equals ph.ProductHeadId
                      select new Domain.ViewModels.ProductHeadTypeVM
                      {
                          Id = pht.ProductHeadTypeId,
                          Name = pht.Name,
                          Description = pht.Description,
                          IsActive = pht.Status == true ? true : false,
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


    #region Color
    public List<Color> GetColorList(bool onlyActive = false)
    {
        var query = _context.Colors.AsQueryable();
        if (onlyActive) query = query.Where(c => c.Status == true);
        return query.OrderBy(c => c.Name).ToList();
    }

    public async Task<JsonResult> CreateColorAsync(ColorVM model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Name)) return new JsonResult(new { success = false, message = "Name is required." });

            bool alreadyExists = await _context.Colors.AnyAsync(c => c.Name.ToLower() == model.Name.Trim().ToLower());
            if (alreadyExists) return new JsonResult(new { success = false, message = "A color with this name already exists." });

            var color = new Color { Name = model.Name, HexCode = model.HexCode, Status = model.IsActive };
            _context.Colors.Add(color);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Color added successfully." });
        }
        catch (Exception ex) { return new JsonResult(new { success = false, message = ex.Message }); }
    }

    public Color GetColorById(int id)
    {
        return _context.Colors.Find(id)!;
    }

    public async Task<JsonResult> UpdateColorAsync(ColorVM model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Name)) return new JsonResult(new { success = false, message = "Name is required." });

            var color = await _context.Colors.FindAsync(model.Id);
            if (color == null) return new JsonResult(new { success = false, message = "Not found" });

            bool alreadyExists = await _context.Colors.AnyAsync(c => c.ColorId != model.Id && c.Name.ToLower() == model.Name.Trim().ToLower());
            if (alreadyExists) return new JsonResult(new { success = false, message = "Another color with this name already exists." });

            color.Name = model.Name;
            color.HexCode = model.HexCode;
            color.Status = model.IsActive;
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Color updated successfully." });
        }
        catch (Exception ex) { return new JsonResult(new { success = false, message = ex.Message }); }
    }

    public async Task<JsonResult> DeleteColorAsync(int id)
    {
        try
        {
            var color = await _context.Colors.FindAsync(id);
            if (color == null) return new JsonResult(new { success = false, message = "Color not found." });
            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Color deleted successfully." });
        }
        catch (Exception ex) { return new JsonResult(new { success = false, message = "Cannot delete as it is being used by items." }); }
    }
    #endregion

    #region Stone
    public List<StoneType> GetStoneList(bool onlyActive = false)
    {
        var query = _context.StoneTypes.AsQueryable();
        if (onlyActive) query = query.Where(s => s.Status == true);
        return query.OrderBy(s => s.Name).ToList();
    }

    public async Task<JsonResult> CreateStoneAsync(StoneTypeVM model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Name)) return new JsonResult(new { success = false, message = "Name is required." });
            
            bool alreadyExists = await _context.StoneTypes.AnyAsync(s => s.Name.ToLower() == model.Name.Trim().ToLower());
            if (alreadyExists) return new JsonResult(new { success = false, message = "A stone type with this name already exists." });

            var stone = new StoneType { Name = model.Name, Description = model.Description, Status = model.IsActive };
            _context.StoneTypes.Add(stone);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Stone type added successfully." });
        }
        catch (Exception ex) { return new JsonResult(new { success = false, message = ex.Message }); }
    }

    public StoneType GetStoneById(int id)
    {
        return _context.StoneTypes.Find(id)!;
    }

    public async Task<JsonResult> UpdateStoneAsync(StoneTypeVM model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Name)) return new JsonResult(new { success = false, message = "Name is required." });

            var stone = await _context.StoneTypes.FindAsync(model.Id);
            if (stone == null) return new JsonResult(new { success = false, message = "Not found" });

            bool alreadyExists = await _context.StoneTypes.AnyAsync(s => s.StoneTypeId != model.Id && s.Name.ToLower() == model.Name.Trim().ToLower());
            if (alreadyExists) return new JsonResult(new { success = false, message = "Another stone type with this name already exists." });

            stone.Name = model.Name;
            stone.Description = model.Description;
            stone.Status = model.IsActive;
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Stone type updated successfully." });
        }
        catch (Exception ex) { return new JsonResult(new { success = false, message = ex.Message }); }
    }

    public async Task<JsonResult> DeleteStoneAsync(int id)
    {
        try
        {
            var stone = await _context.StoneTypes.FindAsync(id);
            if (stone == null) return new JsonResult(new { success = false, message = "Stone type not found." });
            _context.StoneTypes.Remove(stone);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Stone type deleted successfully." });
        }
        catch (Exception ex) { return new JsonResult(new { success = false, message = "Cannot delete as it is being used by items." }); }
    }
    #endregion

    #region Fitting
    public List<FittingType> GetFittingList(bool onlyActive = false)
    {
        var query = _context.FittingTypes.AsQueryable();
        if (onlyActive) query = query.Where(f => f.Status == true);
        return query.OrderBy(f => f.Name).ToList();
    }

    public async Task<JsonResult> CreateFittingAsync(FittingTypeVM model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Name)) return new JsonResult(new { success = false, message = "Name is required." });

            bool alreadyExists = await _context.FittingTypes.AnyAsync(f => f.Name.ToLower() == model.Name.Trim().ToLower());
            if (alreadyExists) return new JsonResult(new { success = false, message = "A fitting type with this name already exists." });

            var fitting = new FittingType { Name = model.Name, Description = model.Description, Status = model.IsActive };
            _context.FittingTypes.Add(fitting);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Fitting type added successfully." });
        }
        catch (Exception ex) { return new JsonResult(new { success = false, message = ex.Message }); }
    }

    public FittingType GetFittingById(int id)
    {
        return _context.FittingTypes.Find(id)!;
    }

    public async Task<JsonResult> UpdateFittingAsync(FittingTypeVM model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Name)) return new JsonResult(new { success = false, message = "Name is required." });

            var fitting = await _context.FittingTypes.FindAsync(model.Id);
            if (fitting == null) return new JsonResult(new { success = false, message = "Not found" });

            bool alreadyExists = await _context.FittingTypes.AnyAsync(f => f.FittingTypeId != model.Id && f.Name.ToLower() == model.Name.Trim().ToLower());
            if (alreadyExists) return new JsonResult(new { success = false, message = "Another fitting type with this name already exists." });

            fitting.Name = model.Name;
            fitting.Description = model.Description;
            fitting.Status = model.IsActive;
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Fitting type updated successfully." });
        }
        catch (Exception ex) { return new JsonResult(new { success = false, message = ex.Message }); }
    }

    public async Task<JsonResult> DeleteFittingAsync(int id)
    {
        try
        {
            var fitting = await _context.FittingTypes.FindAsync(id);
            if (fitting == null) return new JsonResult(new { success = false, message = "Fitting type not found." });
            _context.FittingTypes.Remove(fitting);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Fitting type deleted successfully." });
        }
        catch (Exception ex) { return new JsonResult(new { success = false, message = "Cannot delete as it is being used by items." }); }
    }
    #endregion

    #region Pattern
    public List<Pattern> GetPatternList(bool onlyActive = false)
    {
        var query = _context.Patterns.AsQueryable();
        if (onlyActive) query = query.Where(p => p.Status == true);
        return query.OrderBy(p => p.Name).ToList();
    }

    public async Task<JsonResult> CreatePatternAsync(PatternVM model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Name)) return new JsonResult(new { success = false, message = "Name is required." });

            bool alreadyExists = await _context.Patterns.AnyAsync(p => p.Name.ToLower() == model.Name.Trim().ToLower());
            if (alreadyExists) return new JsonResult(new { success = false, message = "A pattern with this name already exists." });

            var pattern = new Pattern { Name = model.Name, Description = model.Description, Status = model.IsActive };
            _context.Patterns.Add(pattern);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Pattern added successfully." });
        }
        catch (Exception ex) { return new JsonResult(new { success = false, message = ex.Message }); }
    }

    public Pattern GetPatternById(int id)
    {
        return _context.Patterns.Find(id)!;
    }

    public async Task<JsonResult> UpdatePatternAsync(PatternVM model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Name)) return new JsonResult(new { success = false, message = "Name is required." });

            var pattern = await _context.Patterns.FindAsync(model.Id);
            if (pattern == null) return new JsonResult(new { success = false, message = "Not found" });

            bool alreadyExists = await _context.Patterns.AnyAsync(p => p.PatternId != model.Id && p.Name.ToLower() == model.Name.Trim().ToLower());
            if (alreadyExists) return new JsonResult(new { success = false, message = "Another pattern with this name already exists." });

            pattern.Name = model.Name;
            pattern.Description = model.Description;
            pattern.Status = model.IsActive;
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Pattern updated successfully." });
        }
        catch (Exception ex) { return new JsonResult(new { success = false, message = ex.Message }); }
    }

    public async Task<JsonResult> DeletePatternAsync(int id)
    {
        try
        {
            var pattern = await _context.Patterns.FindAsync(id);
            if (pattern == null) return new JsonResult(new { success = false, message = "Pattern not found." });
            _context.Patterns.Remove(pattern);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Pattern deleted successfully." });
        }
        catch (Exception ex) { return new JsonResult(new { success = false, message = "Cannot delete as it is being used by items." }); }
    }
    #endregion
}
