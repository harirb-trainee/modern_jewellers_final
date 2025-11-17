namespace MJ.Domain.ViewModels;

public class ItemVM
{
    public string ItemId { get; set; }
    public string ItemName { get; set; }
    public string PolishType { get; set; }
    public int PolishTypeId { get; set; }
    public decimal Item_price { get; set; }
    public string Item_color { get; set; }
    public bool Status { get; set; } = true;
    public string CreatedBy { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int subcategoryId { get; set; }
    public string subCategoryName { get; set; }
    public int Item_Quantity { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
