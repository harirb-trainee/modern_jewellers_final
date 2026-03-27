using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MJ.Domain.ViewModels;

public class AddItemVM
{
    public string? ItemName { get; set; }
    public string MJCode { get; set; }
    public decimal? ItemPrice { get; set; }
    [StringLength(100)]
    public string? Description { get; set; }
    public string? Category { get; set; }

    public int? StockQuantity { get; set; }
    public int? ThreshHoldQuantity { get; set; }
    
    public int? PolishId { get; set; }
    public int? PatternId { get; set; }
    public int? StoneId { get; set; }
    public int? ColorId { get; set; }
    public int? FittingId { get; set; }
    public decimal? Weight { get; set; }
    public IFormFile? ItemImage1 { get; set; }
    public IFormFile? ItemImage2 { get; set; }
    public string? ItemImageUrl1 { get; set; }
    public string? ItemImageUrl2 { get; set; }

    //for logs
    public string? AddedBy { get; set; }
    public DateTime AddedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Display names for Modal
    public string? CategoryName { get; set; }
    public string? subCategoryName { get; set; }
    public string? PolishTypeName { get; set; }
    public string? ColorName { get; set; }
    public string? StoneName { get; set; }
    public string? FittingName { get; set; }
    public string? PatternName { get; set; }
}
