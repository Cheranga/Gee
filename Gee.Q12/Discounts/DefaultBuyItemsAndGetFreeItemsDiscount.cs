using System;
using System.Linq;
using Gee.Q12.Interfaces;
using Gee.Q12.Models;

namespace Gee.Q12.Discounts
{
    //
    // TODO: Add unit tests for this class
    //
    public class DefaultBuyItemsAndGetFreeItemsDiscount : IDiscount<Cart>
    {
        private readonly int _buyAmount;
        private readonly int _freeAmount;

        private int ThresholdCount
        {
            get { return _buyAmount + _freeAmount; }
        }

        public DefaultBuyItemsAndGetFreeItemsDiscount(int buyAmount, int freeAmount)
        {
            if (buyAmount <= 0 || freeAmount <= 0)
            {
                throw new ArgumentOutOfRangeException("Both buy amount and free amount must be greater than zero");
            }

            if (buyAmount < freeAmount)
            {
                throw new ArgumentException("freeAmount", "Are you trying to take me out of business???");
            }

            _buyAmount = buyAmount;
            _freeAmount = freeAmount;
        }

        public virtual void Apply(Cart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException("cart", "cart is null, cannot apply discount");
            }
            //
            // Set the promo flag to false
            //
            cart.HasPromoBit = false;
            if (cart.IsEmpty)
            {
                return;
            }
            //
            // Get the total number of items and check them agains the threshold amount.
            // If the total < threshold count, no need to continue, because there's nothing to calculate
            //
            var totalNumberOfItems = cart.CartItems.Sum(x => x.Quantity);

            if (totalNumberOfItems < (ThresholdCount))
            {
                return;
            }
            //
            // Get the number of free items
            //
            var numberOfFreeItems = totalNumberOfItems / ThresholdCount;
            //
            // Order cart items by price
            //
            var sortedCartItemsByPrice = cart.CartItems.OrderBy(item => item.UnitPrice).ToList();

            foreach (var cartItem in sortedCartItemsByPrice)
            {
                if (numberOfFreeItems <= 0)
                {
                    break;
                }

                if (cartItem.Quantity >= numberOfFreeItems)
                {
                    cartItem.Quantity -= numberOfFreeItems;
                    break;
                }

                numberOfFreeItems -= cartItem.Quantity;
                cartItem.Quantity = 0;
            }
            //
            // Set the promo flag as true
            //
            cart.HasPromoBit = true;

        }
    }
}
