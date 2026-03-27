namespace MJ.Application.Services;
using MJ.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MJ.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

public class InventoryService : IInventoryService
{
    private readonly MJDbContext _context;

    public InventoryService(MJDbContext context)
    {
        _context = context;
    }

    public List<ItemVM> GetItemList(int pageNumber, int pageSize, out int totalCount, int? categoryId = null, int? polishId = null, int? colorId = null, string? searchTerm = null, bool onlyLowStock = false)
    {
        var query = _context.Items.AsQueryable();

        // Apply Search
        if (!string.IsNullOrEmpty(searchTerm))
        {
            var searchPattern = $"%{searchTerm}%";
            query = query.Where(i => EF.Functions.ILike(i.ItemName, searchPattern) || 
                                     EF.Functions.ILike(i.ItemId, searchPattern) ||
                                     (i.ItemCategoryNavigation != null && EF.Functions.ILike(i.ItemCategoryNavigation.Name, searchPattern)) ||
                                     (i.ItemPolishNavigation != null && EF.Functions.ILike(i.ItemPolishNavigation.Name, searchPattern)));
        }

        // Apply Low Stock Filter
        if (onlyLowStock)
        {
            query = query.Where(i => i.ItemQuantity < (i.Threshold ?? 10));
        }

        // Apply filters
        if (categoryId.HasValue && categoryId > 0)
            query = query.Where(i => i.ItemCategory == categoryId);
        
        if (polishId.HasValue && polishId > 0)
            query = query.Where(i => i.ItemPolish == polishId);

        if (colorId.HasValue && colorId > 0)
            query = query.Where(i => i.Color == colorId);

        totalCount = query.Count();

        var items = query
            .OrderByDescending(i => i.CreatedAt ?? DateTime.MinValue)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(i => new ItemVM
            {
                ItemId = i.ItemId,
                ItemName = i.ItemName,
                PolishType = i.ItemPolishNavigation != null ? i.ItemPolishNavigation.Name : string.Empty,
                PolishTypeId = i.ItemPolishNavigation != null ? i.ItemPolishNavigation.PolishTypeId : 0,
                CategoryId = i.ItemCategoryNavigation != null ? i.ItemCategoryNavigation.ProductHeadId : 0,
                CategoryName = i.ItemCategoryNavigation != null ? i.ItemCategoryNavigation.Name : string.Empty,
                subcategoryId = i.ItemSubCategoryNavigation != null ? i.ItemSubCategoryNavigation.ProductHeadTypeId : 0,
                subCategoryName = i.ItemSubCategoryNavigation != null ? i.ItemSubCategoryNavigation.Name : string.Empty,
                Item_color = i.ColorNavigation != null ? i.ColorNavigation.Name : string.Empty,
                Item_Quantity = i.ItemQuantity ?? 0,
                Item_price = i.ItemPrice,
                Status = true,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedAt ?? DateTime.Now,
                PhotoUrl = i.PhotoUrl ?? string.Empty,
                LowStockThreshold = i.Threshold ?? 10,
            }).ToList();

        return items;
    }

    public async Task<InventoryDashboardVM> GetInventoryDashboardAsync()
    {
        var totalItems = await _context.Items.CountAsync();
        var totalCategories = await _context.ProductHeads.CountAsync();
        var totalSubCategories = await _context.ProductHeadTypes.CountAsync();
        var lowStockItems = await _context.Items.Where(i => i.ItemQuantity < (i.Threshold ?? 10)).CountAsync();
        var total_invntory_value = await _context.Items.SumAsync(i => i.ItemPrice * (i.ItemQuantity ?? 0));

        return new InventoryDashboardVM
        {
            TotalItems = totalItems,
            TotalCategories = totalCategories,
            TotalInventoryValue = total_invntory_value,
            // TotalSubCategories = totalSubCategories,
            LowStockItems = lowStockItems
        };
    }

    public async Task<JsonResult> AddNewItem(AddItemVM itemVM)
    {
        try
        {
            var newItem = new Item
            {
                ItemId = itemVM.MJCode,
                ItemName = itemVM.ItemName ?? string.Empty,
                ItemPrice = itemVM.ItemPrice ?? 0,
                ItemQuantity = itemVM.StockQuantity,
                Threshold = itemVM.ThreshHoldQuantity,
                Color = itemVM.ColorId,
                ItemPolish = itemVM.PolishId,
                ItemCategory = int.TryParse(itemVM.Category, out int catId) ? catId : null,
                StoneId = itemVM.StoneId,
                FittingId = itemVM.FittingId,
                PatternId = itemVM.PatternId,
                Description = itemVM.Description,
                CreatedAt = DateTime.Now,
                CreatedBy = itemVM.AddedBy ?? "Admin"
            };

            if (itemVM.ItemImage1 != null)
            {
                // Get the category name for the path
                string categoryPath = "General";
                if (int.TryParse(itemVM.Category, out int categoryId))
                {
                    var category = await _context.ProductHeads.FindAsync(categoryId);
                    if (category != null)
                    {
                        categoryPath = string.Join("_", category.Name.Split(Path.GetInvalidFileNameChars()));
                    }
                }

                var extension = Path.GetExtension(itemVM.ItemImage1.FileName);
                var fileName = $"{itemVM.MJCode}{extension}";
                var relativePath = Path.Combine("images/items", categoryPath);
                var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
                
                // Ensure directory exists
                Directory.CreateDirectory(absolutePath);

                var fullFilePath = Path.Combine(absolutePath, fileName);
                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await itemVM.ItemImage1.CopyToAsync(stream);
                }
                newItem.PhotoUrl = "/" + Path.Combine(relativePath, fileName).Replace("\\", "/");
            }

            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true, message = "Item added successfully." });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = "Error: " + ex.Message });
        }
    }

    public async Task<JsonResult> DeleteItem(string itemId)
    {
        var item = await _context.Items.FindAsync(itemId);
        if (item == null)
        {
            return new JsonResult(new { success = false, message = "Item not found." });
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();

        return new JsonResult(new { success = true, message = "Item deleted successfully." });
    }

    public async Task<AddItemVM> GetItemById(string itemId)
    {
        var item = await _context.Items
            .Include(i => i.ItemCategoryNavigation)
            .Include(i => i.ColorNavigation)
            .Include(i => i.ItemPolishNavigation)
            .Include(i => i.StoneNavigation)
            .Include(i => i.FittingNavigation)
            .Include(i => i.PatternNavigation)
            .Include(i => i.ItemSubCategoryNavigation)
            .FirstOrDefaultAsync(i => i.ItemId == itemId);

        if (item == null) return null;

        return new AddItemVM
        {
            MJCode = item.ItemId,
            ItemName = item.ItemName,
            ItemPrice = item.ItemPrice,
            StockQuantity = item.ItemQuantity,
            ThreshHoldQuantity = item.Threshold,
            Description = item.Description,
            Category = item.ItemCategory?.ToString(),
            PolishId = item.ItemPolish,
            ColorId = item.Color,
            PatternId = item.PatternId,
            StoneId = item.StoneId,
            FittingId = item.FittingId,
            ItemImageUrl1 = item.PhotoUrl,
            
            // Populate Display Names
            CategoryName = item.ItemCategoryNavigation?.Name,
            subCategoryName = item.ItemSubCategoryNavigation?.Name,
            PolishTypeName = item.ItemPolishNavigation?.Name,
            ColorName = item.ColorNavigation?.Name,
            StoneName = item.StoneNavigation?.Name,
            FittingName = item.FittingNavigation?.Name,
            PatternName = item.PatternNavigation?.Name
        };
    }

    public async Task<JsonResult> UpdateItem(AddItemVM itemVM)
    {
        try
        {
            var item = await _context.Items.FindAsync(itemVM.MJCode);
            if (item == null) return new JsonResult(new { success = false, message = "Item not found." });

            item.ItemName = itemVM.ItemName ?? string.Empty;
            item.ItemQuantity = itemVM.StockQuantity;
            item.Threshold = itemVM.ThreshHoldQuantity;
            item.Description = itemVM.Description;
            item.ItemPrice = itemVM.ItemPrice ?? 0;
            item.ItemPolish = itemVM.PolishId;
            item.Color = itemVM.ColorId;
            item.ItemCategory = int.TryParse(itemVM.Category, out int catId) ? catId : null;
            item.StoneId = itemVM.StoneId;
            item.FittingId = itemVM.FittingId;
            item.PatternId = itemVM.PatternId;
            item.UpdatedAt = DateTime.Now;
            item.UpdatedBy = itemVM.UpdatedBy ?? "Admin";

            if (itemVM.ItemImage1 != null)
            {
                // Organizing image in category folders
                string categoryPath = "General";
                if (item.ItemCategory != null)
                {
                    var category = await _context.ProductHeads.FindAsync(item.ItemCategory);
                    if (category != null)
                    {
                        categoryPath = string.Join("_", category.Name.Split(Path.GetInvalidFileNameChars()));
                    }
                }

                var extension = Path.GetExtension(itemVM.ItemImage1.FileName);
                var fileName = $"{itemVM.MJCode}{extension}";
                var relativePath = Path.Combine("images/items", categoryPath);
                var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
                Directory.CreateDirectory(absolutePath);

                var fullFilePath = Path.Combine(absolutePath, fileName);
                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await itemVM.ItemImage1.CopyToAsync(stream);
                }
                item.PhotoUrl = "/" + Path.Combine(relativePath, fileName).Replace("\\", "/");
            }

            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, message = "Item updated successfully." });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = "Error: " + ex.Message });
        }
    }
}
