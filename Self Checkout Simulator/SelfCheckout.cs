using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class SelfCheckout
    {
        // Attributes
        private Product currentProduct;
        private BaggingAreaScale baggingArea;
        private ScannedProducts scannedProducts;
        private LooseItemScale looseItemScale;
        // Constructor
        public SelfCheckout(BaggingAreaScale baggingArea, ScannedProducts scannedProducts, LooseItemScale looseItemScale)
        {
          this.baggingArea = baggingArea;
          this.scannedProducts = scannedProducts;
          this.looseItemScale = looseItemScale;
        }

        // Operations
        public void LooseProductSelected()
        {
            currentProduct = ProductsDAO.GetRandomLooseProduct();
            looseItemScale.Enable();
        }

        public void LooseItemAreaWeightChanged(int weightOfLooseItem)
        {
            currentProduct.SetWeight(weightOfLooseItem);
            scannedProducts.Add(currentProduct);
            baggingArea.SetExpectedWeight(scannedProducts.CalculateWeight());
            looseItemScale.Disable();
        }

        public void BarcodeWasScanned(int barcode)
        {
            currentProduct = ProductsDAO.SearchUsingBarcode(barcode);
            scannedProducts.Add(currentProduct);
            baggingArea.SetExpectedWeight(scannedProducts.CalculateWeight());
        }

        public void BaggingAreaWeightChanged()
        {
            currentProduct = null;
        }

        public void UserPaid()
        {
            scannedProducts.Reset();
            baggingArea.Reset();
        }

        public string GetPromptForUser()
        {
            if ((scannedProducts.HasItems() && baggingArea.IsWeightOk()) && (currentProduct == null))
                return "Scan an item or pay.";
            if (baggingArea.IsWeightOk() && (currentProduct == null))
                return "Scan an item.";
            if (looseItemScale.IsEnabled())
                return "Place item on scale.";
            if ((currentProduct != null) && !looseItemScale.IsEnabled())
                return "Place the item in the bagging area.";
            if (!scannedProducts.HasItems() || baggingArea.IsWeightOk())
                return "ERROR: UNKNWON STATE";
            return "Please wait, assistant is on the way.";
        }

        public Product GetCurrentProduct()
        {
            return currentProduct;
        }

        public void AdminOverrideWeight()
        {
            baggingArea.OverrideWeight();

        }
     
    }
}