﻿using EntityFrameworkCorePagination.Nuget.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private AppDbContext context;

    public ProductsController()
    {
        context = new();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> CreateProducts()
    {
        IList<Unit> units = new List<Unit>()
        {
            new Unit(){ Name = "Paket"},
            new Unit(){ Name = "Adet"},
            new Unit(){ Name = "KG"},
        };

        IList<Category> categories = new List<Category>()
        {
            new Category() {Name = "Sebze"},
            new Category() {Name = "Meyve"},
            new Category() {Name = "Abur Cubur"},
        };
        
        IList<Product> products = new List<Product>();
        IList<ProductUnit> productUnits = new List<ProductUnit>();

        for (int i = 0; i < 10000; i++)
        {
            Random rdm = new();

            Product product = new()
            {
                Name = "Product " + i,
                Code = "PRD" + i,
                CategoryId = categories[rdm.Next(0,2)].Id
            };
            products.Add(product);

            foreach (var unit in units)
            {
                ProductUnit productUnit = new()
                {
                    ProductId = product.Id,
                    UnitId = unit.Id,
                    Price = i * 10
                };
                productUnits.Add(productUnit);
            }
        }

        await context.Categories.AddRangeAsync(categories);
        await context.Units.AddRangeAsync(units);
        await context.Products.AddRangeAsync(products);
        await context.ProductUnits.AddRangeAsync(productUnits);
        await context.SaveChangesAsync();

        return Ok(new { Message = "Ürünler başarıyla oluşturuldu" });
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetProducts(int pageNumber, int pageSize)
    {
        PaginationResult<ProductDto> response =
            await context.Products
            .Include(p=> p.Category)
            .Select(s=> new ProductDto
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                Category = s.Category,
                ProductUnits = 
                    context.ProductUnits
                    .Where(p=> p.ProductId == s.Id)
                    .Include(p=> p.Unit)
                    .ToList()
            })
            .ToPagedListAsync(pageNumber, pageSize);

        return Ok(response);

    }
}
