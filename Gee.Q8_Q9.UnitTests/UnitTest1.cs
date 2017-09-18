using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using Gee.Q8_Q9.DAL;
using Gee.Q8_Q9.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Gee.Q8_Q9.UnitTests
{
    [TestClass]
    public class ProductRepositoryTests
    {   
        private ProductRepository _productRepository;

        [TestInitialize]
        public void Initialize()
        {
            var countries = new Mock<DbSet<Country>>();
            SetupMockDbSet(countries, new List<Country>
            {
                new Country {CountryId = 1, Name = "Australia"},
                new Country {CountryId = 2, Name = "Hong Kong"}
            }.AsQueryable());

            var warehouses = new Mock<DbSet<Warehouse>>();
            SetupMockDbSet(warehouses, new List<Warehouse>
            {
                new Warehouse {WarehouseId = 1, CountryId =  1, Name = "AUS Warehouse"},
                new Warehouse {WarehouseId = 2, CountryId =  2, Name = "Hong Kong Warehouse"}
            }.AsQueryable());

            var products = new Mock<DbSet<Product>>();
            SetupMockDbSet(products, new List<Product>
            {
                new Product {ProductId = 1, Name="Nike Shoes"},
                new Product {ProductId = 2, Name="Adidas Shoes"}
            }.AsQueryable());

            var warehouseProducts = new Mock<DbSet<WarehouseProduct>>();
            SetupMockDbSet(warehouseProducts, new List<WarehouseProduct>
            {
                new WarehouseProduct {WarehouseProductId = 1, WarehouseId = 1, ProductId =  1, IsSoldOut = true, ProductPrice = 100},
                new WarehouseProduct {WarehouseProductId = 2, WarehouseId = 1, ProductId =  2, IsSoldOut = false, ProductPrice = 200},

                new WarehouseProduct {WarehouseProductId = 3, WarehouseId = 2, ProductId =  1, IsSoldOut = true, ProductPrice = 300},
                new WarehouseProduct {WarehouseProductId = 4, WarehouseId = 2, ProductId =  2, IsSoldOut = true, ProductPrice = 400}

            }.AsQueryable());

            var mockContext = new Mock<StoreDbContext>();
            mockContext.Setup(c => c.Countries).Returns(countries.Object);
            mockContext.Setup(c => c.Warehouses).Returns(warehouses.Object);
            mockContext.Setup(c => c.WarehouseProducts).Returns(warehouseProducts.Object);
            mockContext.Setup(c => c.Products).Returns(products.Object);

            _productRepository = new ProductRepository(mockContext.Object);
        }

        private void SetupMockDbSet<T>(Mock<DbSet<T>> mockDbSet, IQueryable<T> data ) where T:class 
        {
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }

        [TestMethod]
        public void For_An_Existing_Country_There_Must_Be_Products()
        {
            //
            // Act
            //
            var australianProducts = _productRepository.GetProductsInCountry(1).ToList();
            var hongKongProducts = _productRepository.GetProductsInCountry(2).ToList();
            //
            // Assert
            //
            Assert.AreEqual(2, australianProducts.Count(), "The expected AUS product types does not match");
            Assert.AreEqual(true, australianProducts[0].IsSoldOut, "The expected AUS product is different");
            Assert.AreEqual(100, australianProducts[0].Price, "The expected AUS product is different");
            Assert.AreEqual(false, australianProducts[1].IsSoldOut, "The expected AUS product is different");
            Assert.AreEqual(200, australianProducts[1].Price, "The expected AUS product is different");

            Assert.AreEqual(2, hongKongProducts.Count(), "The expected HKG product types does not match");
            Assert.AreEqual(true, hongKongProducts[0].IsSoldOut, "The expected HKG product is different");
            Assert.AreEqual(300, hongKongProducts[0].Price, "The expected HKG product is different");
            Assert.AreEqual(true, hongKongProducts[1].IsSoldOut, "The expected HKG product is different");
            Assert.AreEqual(400, hongKongProducts[1].Price, "The expected HKG product is different");
        }

        [TestMethod]
        public void For_An_Non_Existing_Country_There_Must_Not_Be_Any_Products()
        {
            //
            // Act
            //
            var walhalla = _productRepository.GetProductsInCountry(666).ToList();
            //
            // Assert
            //
            Assert.AreEqual(0, walhalla.Count, "There must not be any products");
        }
    }
}
