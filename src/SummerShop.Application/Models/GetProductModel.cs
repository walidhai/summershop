using System.ComponentModel.DataAnnotations;

namespace SummerShop.Application.Models;

public class GetProductModel
{
    [Required]
    public string ProductName { get; set; }
    [Required]
    public double Price { get; set; }
}