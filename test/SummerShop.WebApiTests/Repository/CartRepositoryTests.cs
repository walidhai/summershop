using Moq;
using SummerShop.Application.Models.Dto;
using SummerShop.Application.Models.Dto.Cart;
using SummerShop.Application.Models.Dto.CartItem;
using SummerShop.Application.Models.Mappings;
using SummerShop.Application.Repositories;
using SummerShop.Data;
using SummerShop.Domain.Entities;
using SummerShop.WebApi.Domain;

namespace SummerShop.WebApiTests.Repository;

public class CartRepositoryTests
    {
        private readonly Mock<ShopDbContext> _mockDbContext;
        private readonly Mock<ProductRepository> _mockProductRepository;
        private readonly CartRepository _cartRepository;

        public CartRepositoryTests()
        {
            _mockDbContext = new Mock<ShopDbContext>();
            _mockProductRepository = new Mock<ProductRepository>(_mockDbContext.Object); // Mock the ProductRepository
            //_cartRepository = new CartRepository(_mockDbContext.Object, _mockProductRepository.Object);
        }
    }