using System;
using System.Collections.Generic;
using System.Linq;
using Gee.Q12.Discounts;
using Gee.Q12.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gee.Q12.UnitTests
{
    [TestClass]
    public class BuyTwoGetOneFreeDiscountTests
    {
        //
        // Note that, testing for thrown exception is not tested in here, because throwing exceptions are a part of the "DefaultBuyItemsAndGetFreeItemsDiscount".
        // The unit tests in that class, should handle those scenarios, and for the sake of simplicity I have not provided them
        //
        private BuyTwoGetOneFreeDiscount _buyTwoGetOneFreeDiscount;

        [TestInitialize]
        public void Initialize()
        {
            _buyTwoGetOneFreeDiscount = new BuyTwoGetOneFreeDiscount();
        }

        [TestMethod]
        public void When_There_Are_No_Items_There_Will_Be_No_Discounts()
        {
            //
            // Arrange
            //
            var cart = new Cart();
            //
            // Act
            //
            _buyTwoGetOneFreeDiscount.Apply(cart);
            //
            // Assert
            //
            Assert.IsFalse(cart.HasPromoBit, "The discount must not be set");
        }

        [TestMethod]
        public void When_There_Are_Items_Less_Than_Three_There_Will_Be_No_Discounts()
        {
            //
            // Arrange
            //
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem {OrderRowId = 1, UnitPrice = 100, Quantity = 1},
                    new CartItem {OrderRowId = 2, UnitPrice = 200, Quantity = 1},
                }
            };
            //
            // Act
            //
            _buyTwoGetOneFreeDiscount.Apply(cart);
            //
            // Assert
            //
            Assert.IsFalse(cart.HasPromoBit, "The discount must not be set");
        }

        [TestMethod]
        public void When_There_Are_Exactly_Three_Items_There_Must_Be_A_Discount()
        {
            //
            // Arrange
            //
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem {OrderRowId = 1, UnitPrice = 100, Quantity = 1},
                    new CartItem {OrderRowId = 1, UnitPrice = 150, Quantity = 1},
                    new CartItem {OrderRowId = 1, UnitPrice = 250, Quantity = 1}
                }
            };
            //
            // Act
            //
            _buyTwoGetOneFreeDiscount.Apply(cart);
            //
            // Assert
            //
            Assert.IsTrue(cart.HasPromoBit, "There must be a discount");
            Assert.AreEqual(0, cart.CartItems[0].RowTotal, "The first item must be free");
        }

        [TestMethod]
        public void When_There_Are_Items_Which_Are_More_Than_Three_But_Also_A_Multiples_Of_Three_There_Must_Be_A_Discount()
        {
            //
            // Arrange
            //
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem {OrderRowId = 1, UnitPrice = 100, Quantity = 1},
                    new CartItem {OrderRowId = 1, UnitPrice = 150, Quantity = 1},
                    new CartItem {OrderRowId = 1, UnitPrice = 250, Quantity = 7}
                }
            };
            //
            // Act
            //
            _buyTwoGetOneFreeDiscount.Apply(cart);
            //
            // Assert
            //
            Assert.IsTrue(cart.HasPromoBit, "There must be a discount");

            Assert.AreEqual(6, cart.CartItems.Sum(item => item.Quantity), "The expected billable items does not match");

            Assert.AreEqual(0, cart.CartItems[0].RowTotal, "The expected total does not match");
            Assert.AreEqual(0, cart.CartItems[1].RowTotal, "The expected total must be zero");
            Assert.AreEqual(250*6, cart.CartItems[2].RowTotal, "The expected total does not match");
        }

        [TestMethod]
        public void When_There_Are_Items_Which_Are_More_Than_Three_But_Also_Not_A_Multiple_Of_Three_There_Must_Be_A_Discount()
        {
            //
            // Arrange
            //
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem {OrderRowId = 1, UnitPrice = 100, Quantity = 1},
                    new CartItem {OrderRowId = 1, UnitPrice = 150, Quantity = 1},
                    new CartItem {OrderRowId = 1, UnitPrice = 250, Quantity = 2},
                    new CartItem {OrderRowId = 1, UnitPrice = 300, Quantity = 3},
                }
            };
            //
            // Act
            //
            _buyTwoGetOneFreeDiscount.Apply(cart);
            //
            // Assert
            //
            Assert.IsTrue(cart.HasPromoBit, "There must be a discount");

            Assert.AreEqual(5, cart.CartItems.Sum(item => item.Quantity), "The expected billable items does not match");

            Assert.AreEqual(0, cart.CartItems[0].RowTotal, "The first item must be free");
            Assert.AreEqual(0, cart.CartItems[1].RowTotal, "The expected total must be zero");
            Assert.AreEqual(250*2, cart.CartItems[2].RowTotal, "The expected total must be zero");
            Assert.AreEqual(300 * 3, cart.CartItems[3].RowTotal, "The expected total does not match");
        }
    }
}
