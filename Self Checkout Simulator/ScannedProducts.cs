using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class ScannedProducts
    {
        // Attributes
        private List<Product> products = new List<Product>();
        private bool requestRemoveProduct = false;

        // Operations
        public List<Product> GetProducts()
        {
            return products;
        }

        public int CalculateWeight()
        {   
            return products.Sum(i => i.GetWeight());
        }

        public int CalculatePrice()
        {
          return products.Sum(i => i.CalculatePrice());

        }

        public void Reset()
        {
            products.Clear();
        }

        public void Add(Product p)
        {
            products.Add(p);
        }

        public bool HasItems()
        {
            return (products.Count > 0);
        }

        public void RemoveLastProduct()
        {
            if (products.Count != 0) 
                products.Remove(products.ElementAt(products.Count-1));
        }
        public void RequestRemove()
        {
            requestRemoveProduct = !requestRemoveProduct;
        }
        public bool RemoveRequestAccepted()
        {
            return requestRemoveProduct;
        }
    }
}
