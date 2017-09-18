namespace Gee.Q8_Q9.Models
{
    public class WarehouseProduct
    {
        public int WarehouseProductId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public bool IsSoldOut { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        public virtual Product Product{ get; set; }
    }
}