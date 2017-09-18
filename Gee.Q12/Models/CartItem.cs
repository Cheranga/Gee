namespace Gee.Q12.Models
{
    public class CartItem
    {
        public int OrderId { get; set; }
        public int OrderRowId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public decimal RowTotal
        {
            get { return UnitPrice*Quantity; }
        }
    }
}