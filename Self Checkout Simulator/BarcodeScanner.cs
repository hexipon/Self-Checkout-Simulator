using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class BarcodeScanner
    {
      private SelfCheckout selfCheckout;
      public void BarcodeDetected()
      {
        selfCheckout.BarcodeWasScanned(ProductsDAO.GetRandomProductBarcode());

      }

      public void LinkToSelfCheckout(SelfCheckout selfCheckout)
      {
        this.selfCheckout = selfCheckout;
      }
    }
}