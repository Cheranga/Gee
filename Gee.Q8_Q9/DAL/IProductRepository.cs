using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gee.Q8_Q9.DTO;

namespace Gee.Q8_Q9.DAL
{
    public interface IProductRepository
    {
        IEnumerable<ProductDto> GetProductsInCountry(int countryId);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductDto> GetProductsInCountry(int countryId)
        {
            var productsInCountry = _context.Warehouses
                                            .Where(warehouse => warehouse.CountryId == countryId)
                                            .Join(_context.WarehouseProducts,
                                                warehouse => warehouse.WarehouseId,
                                                warehouseProduct => warehouseProduct.WarehouseId,
                                                (warehouse, warehouseProduct) => new
                                                {
                                                    ProductId = warehouseProduct.ProductId,
                                                    IsSoldOut = warehouseProduct.IsSoldOut,
                                                    Price = warehouseProduct.ProductPrice
                                                })
                                            .Join(_context.Products,
                                                arg => arg.ProductId,
                                                product => product.ProductId,
                                                (arg, product) => new ProductDto
                                                {
                                                    Name = product.Name,
                                                    Price = arg.Price,
                                                    IsSoldOut = arg.IsSoldOut
                                                });

            return productsInCountry;
        }
    }
}
