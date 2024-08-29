using System.ComponentModel.DataAnnotations;

namespace SummerShop.Application.Models.Dto;

public class AddProductModel
{
    [Required]
    public string ProductName { get; set; }
    [Required]
    public double Price { get; set; }
}