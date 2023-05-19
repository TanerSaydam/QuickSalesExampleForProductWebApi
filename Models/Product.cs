using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public sealed class Product
{
    public Product()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    [ForeignKey("Category")]
    public string CategoryId { get; set; }
    public Category Category { get; set; }
}

public sealed class Category
{
    public Category()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string Name { get; set; }
}

public sealed class Unit
{
    public Unit()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }    
    public string Name { get; set; }
}

public sealed class ProductUnit
{
    public ProductUnit()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public decimal Price { get; set; }

    [ForeignKey("Product")]
    public string ProductId { get; set; }
    public Product Product { get; set; }

    [ForeignKey("Unit")]
    public string UnitId { get; set; }
    public Unit Unit { get; set; }
}
