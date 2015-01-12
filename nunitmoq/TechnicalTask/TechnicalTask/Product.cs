using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalTask
{
    public interface IProduct
    {
        string Name { get; set; }
        decimal Cost { get; set ; }
        decimal SalesTaxes { get;  }
        decimal GrossPrice { get;  }
        bool IsImported { get; set; }
        string PrintNet(IPrintingDecorator printer);
        string PrintGross(IPrintingDecorator printer);
    }
    
    /// <summary>
    /// Base class for all Products
    /// </summary>
    public class Product :IProduct
    {
        private decimal _cost;
        private decimal _salesTaxes;
        private decimal _grossPrice;
        private bool _isImported;
        

        public string Name { get; set; }
        public decimal Cost { get { return _cost; } set { SetCost(value); } }
        public decimal SalesTaxes { get{ return _salesTaxes; } }
        public decimal GrossPrice { get{ return _grossPrice; } }
        public bool IsImported { get{ return _isImported;} set { SetIsImported(value); } }

        //read the basic sales tax rate (currently 10) from the application settings
        protected decimal BasicSalesTaxRate = Properties.Settings.Default.DefaultBasicSalesTaxRate;
        //read the import duty rate (currently 5) from the application settings
        private decimal ImportDutyRate = Properties.Settings.Default.ImportDutyRate;
        
        private void SetCost(decimal value)
        {
            _cost = value;
            CalculateTaxes();
        }

        private void SetIsImported(bool value)
        {
            _isImported = value;
            CalculateTaxes();
        }
        
        /// <summary>
        /// calculate and total the applicable taxes
        /// calculate the price after tax
        /// </summary>
        private void CalculateTaxes()
        {
            decimal taxes = CalculateBasicSalesTax();
            if (_isImported)
            {
                taxes += CalculateImportDuty();
            }
            _salesTaxes = taxes;
            _grossPrice = _cost + _salesTaxes;
        }


        private decimal CalculateBasicSalesTax()
        {
            return RoundUpToNearestPoint05((_cost * BasicSalesTaxRate) / 100);
        }

        private decimal CalculateImportDuty()
        {
            return RoundUpToNearestPoint05((_cost * ImportDutyRate) / 100);
        }

        /// <summary>
        /// Rounds up a decimal to the nearest 0.05
        /// </summary>
        /// <param name="value">the value to be rounded</param>
        /// <returns>the rounded up value</returns>
        private decimal RoundUpToNearestPoint05(decimal value)
        {
            //round value to nearest 0.05 - up or down)
            decimal roundedValue = Math.Round(value / 5, 2)* 5;
            
            // now round up if it rounded down
            if (roundedValue - value <0)
            {
            roundedValue += 0.05m; 
            }

            return roundedValue; 
        }

        /// <summary>
        /// print details without regard to any taxes
        /// </summary>
        /// <param name="printer">the decorator class to print to</param>
        /// <returns>the string set to be printed</returns>
        public string PrintNet(IPrintingDecorator printer)
        {
            string imported = IsImported ? "imported " : "";
            return printer.Print("1 " + imported + Name + " at " + _cost.ToString());
        }

        //print details after tax has been included
        public string PrintGross(IPrintingDecorator printer)
        {
            string imported = IsImported ? "imported " : "";
            return printer.Print("1 " + imported + Name + ": " + _grossPrice.ToString());
        }
    }
    
    /// <summary>
    /// Any basic sales tax exempt product sohould inherit from this abstract class
    /// </summary>
    public abstract class TaxExemptProduct : Product
    {
        //Constructor
        protected  TaxExemptProduct()
        {
            //read the exempt rate (currently zero) from tha application settings
            BasicSalesTaxRate = Properties.Settings.Default.ExemptBasicSalesTaxRate;
        }
    }

    /// <summary>
    /// Subclasses for Products that are not subject to Basic Sales Tax 
    /// </summary>
    public class Book : TaxExemptProduct
    {
    }

    public class Food : TaxExemptProduct
    {
    }

    public class MedicalProduct : TaxExemptProduct
    {
    }

}
