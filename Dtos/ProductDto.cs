using WebApplication1.Models;

namespace WebApplication1.Dtos;

public sealed class ProductDto
{
    public string Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public ICollection<ProductUnit> ProductUnits { get; set; }
}
