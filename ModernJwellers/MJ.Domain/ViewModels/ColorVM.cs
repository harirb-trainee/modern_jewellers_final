namespace MJ.Domain.ViewModels;

public class ColorVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? HexCode { get; set; }
    public bool IsActive { get; set; } = true;
}
