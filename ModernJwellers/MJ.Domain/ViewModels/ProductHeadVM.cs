namespace MJ.Domain.ViewModels;

public class ProductHeadVM
{
    public int ProductHeadId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
