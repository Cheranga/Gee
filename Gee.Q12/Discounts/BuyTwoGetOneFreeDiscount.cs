namespace Gee.Q12.Discounts
{
    public class BuyTwoGetOneFreeDiscount : DefaultBuyItemsAndGetFreeItemsDiscount
    {
        public BuyTwoGetOneFreeDiscount() : base(2,1)
        {
        }
    }
}