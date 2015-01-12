using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechnicalTask;
using Moq;

namespace TechnicalTask.Tests
{
    [TestFixture]
    public class ReceiptTest
    {
        [Test]
        public void CreateEmptyReceipt()
        {
            var receipt = SetupEmptyProductReceipt();

            Assert.IsNotNull(receipt.ItemLineCount, "ItemLineCount is Null");
            Assert.AreEqual(0, receipt.ItemLineCount, "ItemLinecount is Not Zero");
        }

        [Test]
        public void EmptyReceiptTotal()
        {
            var receipt = SetupEmptyProductReceipt();

            Assert.AreEqual(0M, receipt.TotalPrice, "TotalCost is not Zero");    

        }

        [Test]
        public void EmptyReceiptSalesTaxes()
        {   
            var receipt = SetupEmptyProductReceipt();

            Assert.AreEqual(0M, receipt.TotalSalesTaxes, "TotalSaleTaxes is not Zero");
        }

        [Test]
        public void PrintEmptyReceipt()
        {
            var receipt = SetupEmptyProductReceipt();
            int productsPrinted = receipt.PrintGross();
            Assert.AreEqual(0, productsPrinted, "Incorrect number of products printed");
        }
        
        [Test]
        public void CreateSingleProductReceipt()
        {
            var receipt = SetupSingleProductReceipt(); 

            //the Receipt should now contain one item only
            Assert.IsNotNull(receipt, "Receipt not Created");
            Assert.AreEqual(1, receipt.ItemLineCount, "Receipt has incorrect number of Items");
        }

        [Test]
        public void CheckSingleProductReceiptSalesTax()
        {
            var receipt = SetupSingleProductReceipt(); 

            //the Receipt Sales Taxes should equal the Sales Taxes for the mock product       
            Assert.AreEqual(0.5M, receipt.TotalSalesTaxes, "Receipt Sales Taxes incorrect");
        }

        [Test]
        public void CheckSingleProductReceiptTotalPrice()
        {
            var receipt = SetupSingleProductReceipt(); 

            //the Receipt total should now equal the Gross Price for the mock product          
            Assert.AreEqual(10.5M, receipt.TotalPrice, "Receipt Total incorrect");
        }

        [Test]
        public void PrintSingleProductReceipt()
        {
            var receipt = SetupSingleProductReceipt(); 
            int productsPrinted = receipt.PrintGross();
            Assert.AreEqual(1, productsPrinted, "Incorrect number of products printed");
        }

        [Test]
        public void CreateMultipleProductReceipt()
        {
            var receipt = SetupMultipleProductReceipt();

            //the Receipt should now contain one item only
            Assert.IsNotNull(receipt, "Receipt not Created");
            Assert.AreEqual(2, receipt.ItemLineCount, "Receipt has incorrect number of Items");
        }

        [Test]
        public void CheckMultipleProductReceiptSalesTax()
        {
            var receipt = SetupMultipleProductReceipt();

            //the Receipt Sales Taxes should equal the total of Sales Taxes for the mock products       
            Assert.AreEqual(2M, receipt.TotalSalesTaxes, "Receipt Sales Taxes incorrect");
        }

        [Test]
        public void CheckMultipleProductReceiptTotalPrice()
        {
            var receipt = SetupMultipleProductReceipt();

            //the Receipt total should now equal the total of Gross Prices for the mock products          
            Assert.AreEqual(25.49M, receipt.TotalPrice, "Receipt Total incorrect");
        }

        [Test]
        public void PrintMultipleProductReceipt()
        {
            var receipt = SetupMultipleProductReceipt();
            int productsPrinted = receipt.PrintGross();
            Assert.AreEqual(2, productsPrinted, "Incorrect number of products printed");
        }
        
        /// <summary>
        /// Setup for creating an empty Receipt
        /// </summary>
        /// <returns></returns>
        private IReceipt SetupEmptyProductReceipt()
        {
            //create empty mock basket and a mock printer instance
            Mock<IShoppingBasket> mockBasket = MockFactoryHelper.CreateMockShoppingBasket(new List<IProduct>());
            Mock<IPrintingDecorator> mockPrinter = MockFactoryHelper.CreateMockPrinter();

            //Pass Mock Basket and the printer to a new Receipt
            Receipt receipt = new Receipt(mockBasket.Object, mockPrinter.Object);

            //verify that the Basket is read by the Receipt
            mockBasket.Verify(mbasket => mbasket.Products);

            return receipt;
        }

        /// <summary>
        /// Common Setup code for creating a single product receipt
        /// </summary>
        /// <returns></returns>
        private IReceipt SetupSingleProductReceipt()
        {
            //Create a Mock product to add to the Basket
            var mockProduct = new Mock<IProduct>();
            mockProduct.Setup(mproduct => mproduct.SalesTaxes).Returns(0.5M);
            mockProduct.Setup(mproduct => mproduct.Name).Returns("xbox game");
            mockProduct.Setup(mproduct => mproduct.GrossPrice).Returns(10.5M);
            var productList = new List<IProduct>();
            productList.Add(mockProduct.Object);

            //create mock basket with a mock product and a mock printer instance
            Mock<IShoppingBasket> mockBasket = MockFactoryHelper.CreateMockShoppingBasket(productList);
            Mock<IPrintingDecorator> mockPrinter = MockFactoryHelper.CreateMockPrinter();
            
            //Pass Basket and the printer to a new Receipt
            var receipt = new Receipt(mockBasket.Object, mockPrinter.Object);

            //verify that the Basket is read by the Receipt
            mockBasket.Verify(mbasket => mbasket.Products);

            return receipt;
        }

        /// <summary>
        /// Common Setup code for creating a multiple product receipt
        /// </summary>
        /// <returns></returns>
        private IReceipt SetupMultipleProductReceipt()
        {
            //Create a Mock product to add to the Receipt
            var mockProduct = new Mock<IProduct>();
            mockProduct.Setup(mproduct => mproduct.SalesTaxes).Returns(0.5M);
            mockProduct.Setup(mproduct => mproduct.Name).Returns("xbox game");
            mockProduct.Setup(mproduct => mproduct.GrossPrice).Returns(10.5M);

            //Create another Mock product to add to the Receipt
            var secondMockProduct = new Mock<IProduct>();
            secondMockProduct.Setup(mproduct => mproduct.SalesTaxes).Returns(1.5M);
            secondMockProduct.Setup(mproduct => mproduct.Name).Returns("music CD");
            secondMockProduct.Setup(mproduct => mproduct.GrossPrice).Returns(14.99M);

            var productList = new List<IProduct>();
            productList.Add(mockProduct.Object);
            productList.Add(secondMockProduct.Object);

            //create mock basket with a mock product and a mock printer instance
            Mock<IShoppingBasket> mockBasket = MockFactoryHelper.CreateMockShoppingBasket(productList);
            Mock<IPrintingDecorator> mockPrinter = MockFactoryHelper.CreateMockPrinter(); ;

            //Pass Basket and the printer to a new Receipt
            var receipt = new Receipt(mockBasket.Object, mockPrinter.Object);

            //verify that the Basket is read by the Receipt
            mockBasket.Verify(mbasket => mbasket.Products);

            return receipt;
        }
    

      
    }
}
