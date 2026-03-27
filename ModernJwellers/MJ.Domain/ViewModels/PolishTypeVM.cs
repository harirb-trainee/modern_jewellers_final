using System.ComponentModel.DataAnnotations;

namespace MJ.Domain.ViewModels;

public class PolishTypeVM
{
    public int Id { get; set; }
    public string PolishName { get; set; }
    [StringLength(100)]
    public string Description { get; set; }
    // public bool Status { get; set; } =true; 

}
