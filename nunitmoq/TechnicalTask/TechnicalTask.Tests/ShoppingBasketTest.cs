using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using TechnicalTask;

namespace TechnicalTask.Tests
{
	[TestFixture]
    public class ShoppingBasketTest
    {
        private ShoppingBasket _basket;

        [SetUp]
        public void SetUp()
        {
            _basket = new ShoppingBasket(MockFactoryHelper.CreateMockPrinter().Object);
        }

        [Test]
        public void NewBasket()
        {
            Assert.IsNotNull(_basket.Products, "Basket not initialized");
            Assert.AreEqual(0, _basket.Products.Count, "New Basket Not Empty");
        }

		[Test]
		public void AddProduct()
		{
            _basket.AddProduct(new Product());
            Assert.AreEqual(1, _basket.Products.Count(), "Unexpected item count in Basket");
		}
        
        [Test]
        public void TestPrintEmptyItemBasket()
        {
            int productsPrinted = _basket.PrintNet();
            Assert.AreEqual(0, productsPrinted, "Incorrect number of products printed");
        }

        [Test]
        public void TestPrintSingleItemBasket()
        {
            Product product = new Product();
            product.Name = "shoes";
            product.Cost = 12.49M;

            _basket.AddProduct(product);

            int productsPrinted = _basket.PrintNet();
            Assert.AreEqual(1, productsPrinted, "Incorrect number of products printed");
        }

        [Test]
        public void TestPrintMultipleItemBasket()
        {
            Product product = new Product();
            product.Name = "shoes";
            product.Cost = 12.49M;

            Product product2 = new Product();
            product2.Name = "hat";
            product2.Cost = 52.23M;

            _basket.AddProduct(product);
            _basket.AddProduct(product2);

            int productsPrinted = _basket.PrintNet();
            Assert.AreEqual(2, productsPrinted, "Incorrect number of products printed");
        }

        
    }
}
