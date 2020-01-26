using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    abstract class Product
    {
        // Attributes
        protected int barcode;
        protected string name;
        protected int weightInGrams;

        // Operations
        public string GetName()
        {
            return name;
        }

        public int GetBarcode()
        {
            return barcode;
        }
        public int GetWeight()
        {
            return weightInGrams;
        }
        public void SetWeight(int weightInGrams)
        {
            this.weightInGrams = weightInGrams;
        }

        public abstract int CalculatePrice();

        public abstract bool IsLooseProduct();
        // TODO: Use the class diagram for details of other operations
    }

    class PackagedProduct : Product
    {
        // Attributes
        private int priceInPence;
        // Constructor
        public PackagedProduct(int barcode, string name, int priceInPence, int weightInGrams)
        {
            this.barcode = barcode;
            this.name = name;
            this.priceInPence = priceInPence;
            this.weightInGrams = weightInGrams;
        }

        // Operations
        public override int CalculatePrice()
        {
            return priceInPence;
        }

        public override bool IsLooseProduct()
        {
            return false;
        }
    }

    class LooseProduct : Product
    {
        // Attributes
        private int pencePer100g;

        // Constructor
        public LooseProduct(int barcode, string name, int pencePer100g)
        {
            this.barcode = barcode;
            this.name = name;
            this.pencePer100g = pencePer100g;
        }

        // Operations
        public int GetPencePer100g()
        {
            return pencePer100g;
        }

        public override int CalculatePrice()
        {
            return ((pencePer100g * weightInGrams) / 100);
        }
        public override bool IsLooseProduct()
        {
            return true;
        }
    }
}