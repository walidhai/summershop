using System.ComponentModel.DataAnnotations;

namespace SummerShop.WebApi.Models.Product;

public class AddProductModel
{
    [Required]
    public string ProductName { get; set; }
    [Required]
    public double Price { get; set; }
}