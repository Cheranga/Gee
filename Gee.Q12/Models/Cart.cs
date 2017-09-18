using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gee.Q12.Models
{
    public class Cart
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool HasPromoBit { get; set; }
        public List<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public bool IsEmpty
        {
            get { return CartItems == null || CartItems.Any() == false; }
        }
    }
}
