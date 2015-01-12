using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalTask
{
    class Program
    {
        private static IPrintingDecorator _printer;

        /// <summary>
        /// Create and process the various inputs
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //create Console printer 
            _printer = new ConsolePrintingDecorator();

            RunInput("1",CreateInput1());
            RunInput("2",CreateInput2());
            RunInput("3",CreateInput3());
        }

        /// <summary>
        /// Process an Individual Basket
        /// </summary>
        /// <param name="name">convenient string Identifier for the Basket</param>
        /// <param name="basket">basket containing products ready to be processed</param>
        private static void RunInput(string name, ShoppingBasket basket)
        {
            Console.WriteLine("Input " + name + ":");
            basket.PrintNet();
                       
            Console.WriteLine();

            Console.WriteLine("Output " + name + ":");
            var receipt = CreateReceipt(basket);
            receipt.PrintGross();
            Console.ReadLine();

        }

        /// <summary>
        /// create items for processing Input 1
        /// </summary>
        /// <returns>shopping basket containing Input 1 items</returns>
        private static ShoppingBasket CreateInput1()
        {
            var basket = new ShoppingBasket(_printer);

            Product product1 = new Book();
            product1.Name = "book";
            product1.Cost = 12.49M;

            Product product2 = new Product();
            product2.Name = "music CD";
            product2.Cost = 14.99M;

            Product product3 = new Food();
            product3.Name = "chocolate bar";
            product3.Cost = 0.85M;

            basket.AddProduct(product1);
            basket.AddProduct(product2);
            basket.AddProduct(product3);

            return basket;
        }

        /// <summary>
        /// create items for processing Input 2
        /// </summary>
        /// <returns>shopping basket containing Input 2 items</returns>
        private static ShoppingBasket CreateInput2()
        {
            ShoppingBasket basket = new ShoppingBasket(_printer);

            Product product1 = new Food();
            product1.Name = "box of chocolates";
            product1.Cost = 10.00M;
            product1.IsImported = true;

            Product product2 = new Product();
            product2.Name = "bottle of perfume";
            product2.Cost = 47.50M;
            product2.IsImported = true;

            basket.AddProduct(product1);
            basket.AddProduct(product2);          

            return basket;
        }

        /// <summary>
        /// create items for processing Input 3
        /// </summary>
        /// <returns>shopping basket containing Input 3 items</returns>
        private static ShoppingBasket CreateInput3()
        {
            ShoppingBasket basket = new ShoppingBasket(_printer);

            Product product1 = new Product();
            product1.Name = "bottle of perfume";
            product1.Cost = 27.99M;
            product1.IsImported = true;

            Product product2 = new Product();
            product2.Name = "bottle of perfume";
            product2.Cost = 18.99M;            

            Product product3 = new MedicalProduct();
            product3.Name = "packet of headache pills";
            product3.Cost = 9.75M;

            Product product4 = new Food();
            product4.Name = "box of chocolates";
            product4.Cost = 11.25M;
            product4.IsImported = true;

            basket.AddProduct(product1);
            basket.AddProduct(product2);
            basket.AddProduct(product3);
            basket.AddProduct(product4);

            return basket;
        }

        private static Receipt CreateReceipt(ShoppingBasket basket)
        {
            return new Receipt(basket, _printer);
        }
    }
}
