using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class BaggingAreaScale
    {
        // Attributes
        private int weight;
        private int expected;
        private List <int> allowedDifference = new List<int>();
        private SelfCheckout selfCheckout;

        
        // Operations
        public int GetCurrentWeight()
        {

            return weight;
        }

        public bool IsWeightOk()
        {
            if (allowedDifference.Count() == 0)
                return (expected == weight);
            else
                return ((expected + allowedDifference.Last()) == weight);
        }

        public int GetExpectedWeight(List<DisplayList> displayList)
        {
            expected=(0);
            displayList.ForEach(i => expected += i.GetWeight());
            if (allowedDifference.Count() == 0)
                return expected;
            else
                return (expected + allowedDifference.Last());
        }

        public void SetExpectedWeight(int expected)
        {
            this.expected = expected;
        }

        public void ProductWeightCorrect()
        { 
            if (allowedDifference.Count() == 0)
            {
                allowedDifference.Add(0);
            }
            else
            {
                allowedDifference.Add(allowedDifference.Last());
            }

        }

        public void OverrideWeight()
        {
            allowedDifference.Add(weight - expected);
        }

        public void Reset()
        {
            allowedDifference.Clear();
            expected = weight = 0;
        }

        public void LinkToSelfCheckout(SelfCheckout sc)
        {
          selfCheckout = sc;
        }

        // NOTE: In reality the difference wouldn't be passed in here, the
        //       scale would detect the change and notify the self checkout
        public void WeightChangeDetected(int difference)
        {
            weight += difference;
            selfCheckout.BaggingAreaWeightChanged();
        }

        public void RemoveWeight()
        {
            if (allowedDifference.Count() == 1)
            {
                weight = expected;
                allowedDifference.RemoveAt(allowedDifference.Count() - 1);
            }
            else if(allowedDifference.Count() == 0) 
            {
                weight = expected;             
                allowedDifference.RemoveAt(allowedDifference.Count() - 1);
            }
            else
            {
                weight = expected + allowedDifference[allowedDifference.Count() - 2];
                allowedDifference.RemoveAt(allowedDifference.Count() - 1);
            }
        }
    }
}