using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalTask
{
    public interface IReceipt
    {
        int ItemLineCount { get; }
        decimal TotalSalesTaxes{ get; }
        decimal TotalPrice { get; }
        int PrintGross();
    }

    public class Receipt : IReceipt
    {
        private decimal _salesTaxes;
        private decimal _totalPrice;
        private IPrintingDecorator _printer;

        public decimal TotalSalesTaxes{ get {return _salesTaxes;} }
        public decimal TotalPrice { get { return _totalPrice; } }
        public int ItemLineCount { get { return _items.Count; } }
        private IList<IProduct> _items;

        //Constructor
        public Receipt(IShoppingBasket basket,IPrintingDecorator printer)
        {
            _printer = printer;
            _items = basket.Products;
            CalculateTotals();
        }

        /// <summary>
        /// Total up all the taxes and final prices for each product in the receipt
        /// </summary>
        private void CalculateTotals()
        {
            _salesTaxes = 0.00M;
            _totalPrice = 0.00M;
            foreach (IProduct product in _items)
            {
                _salesTaxes += product.SalesTaxes;
                _totalPrice += product.GrossPrice;
            }

        }

        /// <summary>
        /// prints out the list of items in the receipt
        /// </summary>
        /// <returns>number of lines printed exluding the totals</returns>
        public int PrintGross()
        {
            int count = 0;
            foreach (IProduct product in _items)
            {
                product.PrintGross(_printer);
                count++;
            }
            
            PrintSalesTaxes();
            PrintTotal();
            return count;
        }

        private void PrintSalesTaxes()
        {
            _printer.Print("Sales Taxes: " + _salesTaxes);
        }

        private void PrintTotal()
        {
            _printer.Print("Total: " + _totalPrice);
        }
    }
}
