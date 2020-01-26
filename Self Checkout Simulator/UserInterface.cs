using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Self_Checkout_Simulator
{
  public partial class UserInterface : Form
    {
        // Attributes
        SelfCheckout selfCheckout;
        BarcodeScanner barcodeScanner;
        BaggingAreaScale baggingAreaScale;
        LooseItemScale looseItemScale;
        ScannedProducts scannedProducts;
        private List<DisplayList> displayList;

        // Constructor
        public UserInterface()
        {
            InitializeComponent();
            displayList = new List<DisplayList>();
            baggingAreaScale = new BaggingAreaScale();
            scannedProducts = new ScannedProducts();
            barcodeScanner = new BarcodeScanner();
            looseItemScale = new LooseItemScale();
            selfCheckout = new SelfCheckout(baggingAreaScale, scannedProducts, looseItemScale);
            barcodeScanner.LinkToSelfCheckout(selfCheckout);
            baggingAreaScale.LinkToSelfCheckout(selfCheckout);
            looseItemScale.LinkToSelfCheckout(selfCheckout);
            UpdateDisplay();
        }

        // Operations
        private void UserScansProduct(object sender, EventArgs e)
        {
            barcodeScanner.BarcodeDetected();
            UpdateDisplay();
        }

        private void UserPutsProductInBaggingAreaCorrect(object sender, EventArgs e)
        {
          baggingAreaScale.WeightChangeDetected(selfCheckout.GetCurrentProduct().GetWeight());
          baggingAreaScale.ProductWeightCorrect();
          UpdateDisplay();
        }

        private void UserPutsProductInBaggingAreaIncorrect(object sender, EventArgs e)
        {
          baggingAreaScale.WeightChangeDetected(new Random().Next(20, 100));
          UpdateDisplay();
        }

        private void UserSelectsALooseProduct(object sender, EventArgs e)
        {
          selfCheckout.LooseProductSelected();
          UpdateDisplay();
        }

        private void UserWeighsALooseProduct(object sender, EventArgs e)
        {
          looseItemScale.WeightChangeDetected(new Random().Next(20, 100));
          UpdateDisplay();
        }

        private void AdminOverridesWeight(object sender, EventArgs e)
        {
          selfCheckout.AdminOverrideWeight();
          UpdateDisplay();
        }

        private void UserChoosesToPay(object sender, EventArgs e)
        {
          selfCheckout.UserPaid();
          UpdateDisplay();
        }

        void UpdateDisplay()
        {
            displayList.Clear();
            lbBasket.Items.Clear();
            lblScreen.Text = selfCheckout.GetPromptForUser();

            List<Product> temp = scannedProducts.GetProducts();
            for (int i = 0; i < temp.Count; i++)
            {

                if (displayList.Count != 0)
                {
                    for (int j = 0; j < displayList.Count; j++)
                    {
                        if (displayList[j].GetName() == temp[i].GetName())
                        {
                            displayList[j].AddExistingItem(temp[i].CalculatePrice());
                            break;

                        }
                        if (displayList.Count - 1 == j)
                        {
                            DisplayList s = new DisplayList(temp[i].GetName(), temp[i].CalculatePrice(),temp[i].GetWeight());
                            displayList.Add(s);
                            break;
                        }
                    }
                }
                else
                { 
                    DisplayList s = new DisplayList(temp[i].GetName(), temp[i].CalculatePrice(), temp[i].GetWeight());
                    displayList.Add(s);
                }

            }
            displayList.ForEach(i => lbBasket.Items.Add(("£" + (i.GetPrice() * 0.01f).ToString("0.00") + "|" + i.GetName() + "|x" + i.GetQuantity())));
            lblTotalPrice.Text = "£" + (scannedProducts.CalculatePrice() * 0.01f).ToString("0.00");
            lblBaggingAreaExpectedWeight.Text = baggingAreaScale.GetExpectedWeight(displayList).ToString("n2");
            lblBaggingAreaCurrentWeight.Text = baggingAreaScale.GetCurrentWeight().ToString("n2");
            btnUserScansBarcodeProduct.Enabled = (baggingAreaScale.IsWeightOk() && selfCheckout.GetCurrentProduct() == null);
            btnUserSelectsLooseProduct.Enabled = (baggingAreaScale.IsWeightOk() && selfCheckout.GetCurrentProduct() == null);
            btnUserWeighsLooseProduct.Enabled = looseItemScale.IsEnabled();
            btnUserPutsProductInBaggingAreaCorrect.Enabled = (!looseItemScale.IsEnabled() && !(selfCheckout.GetCurrentProduct() == null));
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = (!looseItemScale.IsEnabled() && !(selfCheckout.GetCurrentProduct() == null));
            btnUserChooseToPay.Enabled = (baggingAreaScale.IsWeightOk() && scannedProducts.HasItems() && selfCheckout.GetCurrentProduct() == null);
            
            btnRemoveProduct.Enabled = (baggingAreaScale.IsWeightOk() && scannedProducts.HasItems() && selfCheckout.GetCurrentProduct() == null && !scannedProducts.RemoveRequestAccepted());

            btnAdminOverridesWeight.Enabled = (!baggingAreaScale.IsWeightOk() && scannedProducts.HasItems() && selfCheckout.GetCurrentProduct() == null) && !scannedProducts.RemoveRequestAccepted();
            btnConfirmRemove.Enabled = scannedProducts.RemoveRequestAccepted();

        }

        private void btnConfirmRemove_Click(object sender, EventArgs e)
        {
            scannedProducts.RemoveLastProduct();
            UpdateDisplay();

            baggingAreaScale.RemoveWeight();
            scannedProducts.RequestRemove();
            UpdateDisplay();
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            scannedProducts.RequestRemove();
            UpdateDisplay();
            
        }
    }
}