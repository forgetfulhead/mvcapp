using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;

//Helper Classes for setup code common across several fixtures
namespace TechnicalTask
{
    public static class MockFactoryHelper
    {
        /// <summary>
        /// Creates a mock PrintingDecorator
        /// which returns the string passed to the Print method when Print is called
        /// </summary>
        /// <returns>Mock PrintingDecorator</returns>
        public static Mock<IPrintingDecorator> CreateMockPrinter()
        {
            var mockPrinter = new Mock<IPrintingDecorator>();
            mockPrinter.Setup(printer => printer.Print(It.IsAny<string>())).Returns((string s) => s);
            return mockPrinter;
        }

        /// <summary>
        /// Creates a mock Shopping Basket
        /// which returns the list of products passedin when GetProducts is called
        /// </summary>
        /// <param name="products">list of products the mock should return</param>
        /// <returns>Mock ShoppingBasket</returns>
        public static Mock<IShoppingBasket> CreateMockShoppingBasket(List<IProduct> products)
        {
            var mockBasket = new Mock<IShoppingBasket>();
            mockBasket.Setup(mbasket => mbasket.Products).Returns(products);

            return mockBasket;
        }

    }

  /*  public static class ConcreteFactoryHelper
    {
        public static Product CreateDefaultProduct()
        {
            return new Product();
        }

        public static Product CreateBookProduct()
        {
            return new Book();
        }
      
    }*/
      
}