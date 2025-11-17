namespace MJ.Domain.ViewModels;

public class ProductHeadTypeVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; } = true;
    public string CreatedBy { get; set; }  
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public  string ParentCategoryName { get; set; }
    public int ProductHeadId { get; set; }
}
