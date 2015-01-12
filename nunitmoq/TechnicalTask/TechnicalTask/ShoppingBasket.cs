using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalTask
{
    public interface IShoppingBasket
    {
        void AddProduct(Product product);
        IList<IProduct> Products { get; }
        int PrintNet();
        
    }
    
    public class ShoppingBasket : IShoppingBasket
    {

        private IList<IProduct> _items;
        private IPrintingDecorator _printer;
        public IList<IProduct> Products{ get { return _items; }}
        
        //Constructor
        public ShoppingBasket(IPrintingDecorator printer)
        {
            _printer = printer;
            //start off with an empty basket
            _items = new List<IProduct> { };
        }

        public void AddProduct(Product product)
        {
            _items.Add(product);
        } 

        /// <summary>
        /// prints out the list of items in the basket
        /// </summary>
        /// <returns>number of lines printed</returns>
        public int PrintNet()
        {
            int count = 0;
            foreach (Product product in _items)
            {
                product.PrintNet(_printer);
                count++;
            }
            return count;
        }

    }
}
