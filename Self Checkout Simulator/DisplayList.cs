using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Checkout_Simulator
{
    class DisplayList
    {
        private string name;
        private int price;
        private int quantity;
        private int weight;
        
        public DisplayList(string name, int price, int weight)
        {
            this.name = name;
            this.price = price;
            this.weight = weight;
            quantity = 1;
        }

        public void AddExistingItem(int price)
        {
            this.price += price;
            this.weight += weight;
            quantity += 1;
        }

        public String GetName()
        {
            return name;
        }
        public int GetPrice()
        {
            return price;
        }
        public int GetQuantity()
        {
            return quantity;
        }

        public int GetWeight()
        {
            return weight;
        }


    }
}
