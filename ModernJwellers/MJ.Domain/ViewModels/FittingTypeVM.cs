using System.ComponentModel.DataAnnotations;

namespace MJ.Domain.ViewModels;

public class FittingTypeVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [StringLength(100)]
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
