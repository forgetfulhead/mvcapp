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
    public class ProductTest
    {
        #region Standard Product Tests

        [Test]
        public void CreateNewProductIsProduct()
        {
            IProduct product = new Product(); 
            Assert.IsInstanceOf(typeof(Product), product, "created Product not of type product");
        }

        [Test]
        public void ProductCheckName()
        {
            IProduct product = new Product();
            product.Name = "book"; 
            Assert.AreEqual("book", product.Name, "Product name incorrect");
        }

        [Test]
        public void ProductCheckCost()
        {
            IProduct product = new Product();         
            product.Cost = 12.49M;
            Assert.AreEqual(12.49M, product.Cost, "Product cost incorrect");
        }

        [Test]
        public void ProductZeroPriceBasicSalesTax()
        {
            IProduct product = new Product();
            product.Cost = 0.00M;
            Assert.AreEqual(0.00M, product.SalesTaxes, "Total Product Sales Taxes not zero");
        }

        [Test]
        public void ProductBasicSalesTax()
        {
            IProduct product = new Product();
            product.Cost = 14.99M;
            Assert.AreEqual(1.50M, product.SalesTaxes, "Total Product Basic Sales Taxes incorrect");
        }

        [Test]
        public void ProductFinalPrice()
        {
            IProduct product = new Product();
            product.Cost = 10.00M;
            Assert.AreEqual(11.00M, product.GrossPrice, "Product price after tax incorrect");
        }
        #endregion

        #region Imported Product Tests

        [Test]
        public void ImportedProductTotalSalesTax()
        {
            IProduct product = new Product();
            product.Cost = 10.00M;
            product.IsImported = true;
            Assert.AreEqual(1.50M, product.SalesTaxes, "Total Imported Product Sales Taxes incorrect");
        }      

        [Test]
        public void ImportedProductFinalPrice()
        {
            IProduct product = new Product();
            product.Cost = 10.00M;
            product.IsImported = true;
            Assert.AreEqual(11.50M, product.GrossPrice, "Product price after tax incorrect");
        }
        #endregion

        #region Basic Sales Tax Exempt Product Tests

        [Test]
        public void CreateNewProductCheckIsBook()
        {
            IProduct product = new Book();
            Assert.IsInstanceOf(typeof(Book), product, "Product not a book");
        }

        [Test]
        public void BookZeroPriceBasicSalesTax()
        {
            IProduct product = new Book();
            product.Cost = 0.00M;
            Assert.AreEqual(0.00M, product.SalesTaxes, "Total Book Sales Taxes not zero");
        }


        [Test]
        public void BookBasicSalesTax()
        {
            IProduct product = new Book();
            product.Cost = 12.49M;
            Assert.AreEqual(0.00m, product.SalesTaxes, "Total Book Sales Taxes incorrect");
        }

        [Test]
        public void BookFinalPrice()
        {
            IProduct product = new Book();
            product.Cost = 10.00M;
            Assert.AreEqual(10.00M, product.GrossPrice, "Book price after tax incorrect");
        }
        #endregion

        #region Imported Tax Exempt Product Tests

        [Test]
        public void ImportedBookSalesTax()
        {
            IProduct product = new Book();
            product.Cost = 10.00M;
            product.IsImported = true;
            Assert.AreEqual(0.50M, product.SalesTaxes, "Total Product Sales Taxes incorrect");
        }       

        [Test]
        public void ImportedBookFinalPrice()
        {
            IProduct product = new Book();
            product.Cost = 10.00M;
            product.IsImported = true;
            Assert.AreEqual(10.50M, product.GrossPrice, "Imported Book price after tax incorrect");
        }
        #endregion  
     
        #region PrintingTests

        [Test]
        public void ProductPrintNet()
        {
            IProduct product = new Book();
            product.Name = "book";
            product.Cost = 10.00M;
            string printedString = product.PrintNet(MockFactoryHelper.CreateMockPrinter().Object);
            const string expectedString = "1 book at 10.00";
            Assert.AreEqual(expectedString, printedString, "Product Net Price printed incorrectly");
        }

        [Test]
        public void ProductPrintGross()
        {
            IProduct product = new Book();
            product.Name = "book";
            product.Cost = 10.00M;
            string printedString = product.PrintGross(MockFactoryHelper.CreateMockPrinter().Object);
            const string expectedString = "1 book: 10.00";
            Assert.AreEqual(expectedString, printedString, "Product Gross Price printed incorrectly");
        }

        [Test]
        public void ImportedProductPrintNet()
        {
            IProduct product = new Book();
            product.Name = "book";
            product.IsImported = true;
            product.Cost = 10.00M;
            string printedString = product.PrintNet(MockFactoryHelper.CreateMockPrinter().Object);
            const string expectedString = "1 imported book at 10.00";
            Assert.AreEqual(expectedString, printedString, "Imported Product Net Price printed incorrectly");
        }

        [Test]
        public void ImportedProductPrintGross()
        {
            IProduct product = new Book();
            product.Name = "book";
            product.IsImported = true;
            product.Cost = 10.00M;
            string printedString = product.PrintGross(MockFactoryHelper.CreateMockPrinter().Object);
            const string expectedString = "1 imported book: 10.50";
            Assert.AreEqual(expectedString, printedString, "Imported Product Gross Price printed incorrectly");
        }

        #endregion
       
    }
}
