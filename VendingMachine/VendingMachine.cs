//////////////////////////////////////////////////////////////////////
//      Vending Machine (Form1.cs)                                  //
//      Written by Masaaki Mizuno, (c) 2006, 2007, 2008, 2010       //
//                      for Learning Tree Course 123P, 252J, 230Y   //
//                 also for KSU Course CIS501                       //  
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VendingMachine
{
    public partial class VendingMachine : Form
    {
        // Static Constants
        public const int NUMCANTYPES = 4;
        public const int NUMCOINTYPES = 4;
        public static readonly int[] NUMCANS = {4,4,4,4};
        public static readonly int[] CANPRICES = { 120, 170, 130, 110 };
        public static readonly string[] CANNAMES = { "Coca-Cola", "Pepsi", "Dr. Pepper", "Sprite" };

        public static readonly int[] COINVALUES = { 10, 50, 100, 500 };
        public static readonly int[] NUMCOINS = { 15, 10, 5, 2 };
            // 10Yen, 50Yen, 100Yen, 500Yen
      
        // Boundary Objects
        private AmountDisplay amountDisplay;
        private DebugDisplay displayPrice0, displayPrice1, displayPrice2, displayPrice3;
        private DebugDisplay displayNum10Yen, displayNum50Yen, displayNum100Yen, displayNum500Yen;
        private DebugDisplay displayName0, displayName1, displayName2, displayName3;
        private DebugDisplay displayNumCans0, displayNumCans1, displayNumCans2, displayNumCans3;
        private Light soldOutLight0, soldOutLight1, soldOutLight2, soldOutLight3;
        private TimerLight noChangeLight;
        private Light purchasableLight0, purchasableLight1, purchasableLight2, purchasableLight3;
        private CoinDispenser coinDispenser10Yen, coinDispenser50Yen, coinDispenser100Yen, coinDispenser500Yen;
        private CanDispenser canDispenser0, canDispenser1, canDispenser2, canDispenser3;
        private CoinInserter coinInserter10Yen, coinInserter50Yen, coinInserter100Yen, coinInserter500Yen;
        private PurchaseButton purchaseButton0, purchaseButton1, purchaseButton2, purchaseButton3;
        private CoinReturnButton coinReturnButton;

        // Declare fields for your entity and control objects
        public Coin[] _coinArray;
        public Can[] _canArray;
        public Controller _control;
        
        
        //ENABLE CONTROL CLASS
        //public Controller _control;


        public VendingMachine()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            amountDisplay = new AmountDisplay(txtAmount);

            displayNum10Yen = new DebugDisplay(txtNum10Yen);
            displayNum50Yen = new DebugDisplay(txtNum50Yen);
            displayNum100Yen = new DebugDisplay(txtNum100Yen);
            displayNum500Yen = new DebugDisplay(txtNum500Yen);
            displayPrice0 = new DebugDisplay(txtPrice0);
            displayPrice1 = new DebugDisplay(txtPrice1);
            displayPrice2 = new DebugDisplay(txtPrice2);
            displayPrice3 = new DebugDisplay(txtPrice3);

            displayName0 = new DebugDisplay(txtName0); //These should be normal displays...?
            displayName1 = new DebugDisplay(txtName1);
            displayName2 = new DebugDisplay(txtName2);
            displayName3 = new DebugDisplay(txtName3);

            displayNumCans0 = new DebugDisplay(txtNumCan0);
            displayNumCans1 = new DebugDisplay(txtNumCan1);
            displayNumCans2 = new DebugDisplay(txtNumCan2);
            displayNumCans3 = new DebugDisplay(txtNumCan3);

            soldOutLight0 = new Light(pbxSOLight0, Color.Orange);
            soldOutLight1 = new Light(pbxSOLight1, Color.Orange);
            soldOutLight2 = new Light(pbxSOLight2, Color.Orange);
            soldOutLight3 = new Light(pbxSOLight3, Color.Orange);

            noChangeLight = new TimerLight(pbxNoChange, Color.Red, timer1);

            purchasableLight0 = new Light(pbxPurLight0, Color.Aqua);
            purchasableLight1 = new Light(pbxPurLight1, Color.Aqua);
            purchasableLight2 = new Light(pbxPurLight2, Color.Aqua);
            purchasableLight3 = new Light(pbxPurLight3, Color.Aqua);

            coinDispenser10Yen = new CoinDispenser(txtChange10Yen);
            coinDispenser50Yen = new CoinDispenser(txtChange50Yen);
            coinDispenser100Yen = new CoinDispenser(txtChange100Yen);
            coinDispenser500Yen = new CoinDispenser(txtChange500Yen);

            // All candispensers share the same output textbox for simulation
            canDispenser0 = new CanDispenser(txtCanDispenser, CANNAMES[0]);
            canDispenser1 = new CanDispenser(txtCanDispenser, CANNAMES[1]);
            canDispenser2 = new CanDispenser(txtCanDispenser, CANNAMES[2]);
            canDispenser3 = new CanDispenser(txtCanDispenser, CANNAMES[3]);

            _coinArray = new Coin[NUMCOINTYPES];
            _canArray = new Can[NUMCANTYPES];

            _control = new Controller(amountDisplay, noChangeLight);

            //ENABLE CONTROL CLASS
            coinReturnButton = new CoinReturnButton(this);
            //coinReturnButton = new CoinReturnButton(new Controller(amountDisplay, noChangeLight,/* coinReturnButton,*/ _coinArray, _canArray));

            

            _canArray[0] = new Can(CANNAMES[0], NUMCANS[0], CANPRICES[0], purchasableLight0, soldOutLight0, canDispenser0, coinReturnButton);
            _canArray[1] = new Can(CANNAMES[1], NUMCANS[1], CANPRICES[1], purchasableLight1, soldOutLight1, canDispenser1, coinReturnButton);
            _canArray[2] = new Can(CANNAMES[2], NUMCANS[2], CANPRICES[2], purchasableLight2, soldOutLight2, canDispenser2, coinReturnButton);
            _canArray[3] = new Can(CANNAMES[3], NUMCANS[3], CANPRICES[3], purchasableLight3, soldOutLight3, canDispenser3, coinReturnButton);

            purchaseButton0 = new PurchaseButton(_canArray[0]);
            purchaseButton1 = new PurchaseButton(_canArray[1]);
            purchaseButton2 = new PurchaseButton(_canArray[2]);
            purchaseButton3 = new PurchaseButton(_canArray[3]);

            _coinArray[0] = new Coin(COINVALUES[0], NUMCOINS[0], coinDispenser10Yen);
            _coinArray[1] = new Coin(COINVALUES[1], NUMCOINS[1], coinDispenser50Yen);
            _coinArray[2] = new Coin(COINVALUES[2], NUMCOINS[2], coinDispenser100Yen);
            _coinArray[3] = new Coin(COINVALUES[3], NUMCOINS[3], coinDispenser500Yen);

            coinInserter10Yen  = new CoinInserter(_coinArray[0]);
            coinInserter50Yen  = new CoinInserter(_coinArray[1]);
            coinInserter100Yen = new CoinInserter(_coinArray[2]);
            coinInserter500Yen = new CoinInserter(_coinArray[3]);

            _control.Coins = _coinArray;
            _control.Cans = _canArray;

            // Display debug information
            displayCanPricesAndNames();
            updateDebugDisplays();
        }

 
        private void btnCoinInserter10Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter10Yen.CoinInserted();
            updateDebugDisplays();
        }

        private void btnCoinInserter50Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter50Yen.CoinInserted();
            updateDebugDisplays();
        }

        private void btnCoinInserter100Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter100Yen.CoinInserted();
            updateDebugDisplays();
        }

        private void btnCoinInserter500Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter500Yen.CoinInserted();
            updateDebugDisplays();
        }

        private void btnPurButtn0_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton0.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnPurButton1_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton1.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnPurButton2_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton2.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnPurButton3_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton3.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnCoinReturn_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinReturnButton.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnChangePickedUp_Click(object sender, EventArgs e)
        {
            // This is just for a simulation
            coinDispenser10Yen.Clear();
            coinDispenser50Yen.Clear();
            coinDispenser100Yen.Clear();
            coinDispenser500Yen.Clear();
        }

        private void btnCanPickedUp_Click(object sender, EventArgs e)
        {
            // This is just for a simulation
            canDispenser0.Clear(); // since all canDispenser objects accesses the
                                      // same textbox object
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            //ENABLE CONTROL CLASS

            Coin.TotalInsValue = 0;
            for (int i = 0; i < _coinArray.Length; i++)
            {
                _coinArray[i].ResetValues(NUMCOINS[i]);
            }

            for (int i = 0; i < _coinArray.Length; i++)
            {
                _canArray[i].Count = NUMCANS[i];
            }

            //coinReturnButton.Control.ResetControl(NUMCOINS, NUMCANS);


            updateDebugDisplays();
        }

        private void displayCanPricesAndNames()
        {
            displayPrice0.Display("\\" + CANPRICES[0]);
            displayPrice1.Display("\\" + CANPRICES[1]);
            displayPrice2.Display("\\" + CANPRICES[2]);
            displayPrice3.Display("\\" + CANPRICES[3]);
            displayName0.Display(CANNAMES[0]);
            displayName1.Display(CANNAMES[1]); 
            displayName2.Display(CANNAMES[2]);
            displayName3.Display(CANNAMES[3]);
        }

        /// <summary>
        /// Refreshes the displays. Called by updateDebugDisplays for ease of use.
        /// </summary>
        private void UpdateDisplays()
        {
            amountDisplay.DisplayAmount(Coin.TotalInsValue);

            for (int i = 0; i < _canArray.Length; i++)
            {
                _canArray[i].UpdateLights(Coin.TotalInsValue);
            }
        }

        /// <summary>
        /// Updates the debug displays.
        /// </summary>
        private void updateDebugDisplays()
        {
            // ZM: IMO, this should happen in DebugDisplay, but the Can Names need to show and they are
            // currently set as DebugDisplays, not just normal text displays.
#if DEBUG
            displayNum10Yen.Display(_coinArray[0].CoinCount);
            displayNum50Yen.Display(_coinArray[1].CoinCount);
            displayNum100Yen.Display(_coinArray[2].CoinCount);
            displayNum500Yen.Display(_coinArray[3].CoinCount);
            displayNumCans0.Display(_canArray[0].Count);
            displayNumCans1.Display(_canArray[1].Count);
            displayNumCans2.Display(_canArray[2].Count);
            displayNumCans3.Display(_canArray[3].Count);
#else
            displayNum10Yen.Display("");
            displayNum50Yen.Display("");
            displayNum100Yen.Display("");
            displayNum500Yen.Display("");
            displayNumCans0.Display("");
            displayNumCans1.Display("");
            displayNumCans2.Display("");
            displayNumCans3.Display("");
#endif

            UpdateDisplays();
        }

        /// <summary>
        /// Returns coins based on the current credit the user has accrued.
        /// Turns on "No Change" light for 3s if there is not a sufficient stock of change in the machine.
        /// </summary>
        /// <param name="change"></param>
        public void ReturnCoins(int change)
        {
            int[] numCoinsToReturn = new int[_coinArray.Length];

            for (int i = _coinArray.Length - 1; i >= 0; i--)
            {
                numCoinsToReturn[i] = (change / _coinArray[i].Value);
                if (numCoinsToReturn[i] > _coinArray[i].CoinCount)
                {
                    numCoinsToReturn[i] = _coinArray[i].CoinCount;
                    change -= numCoinsToReturn[i] * _coinArray[i].Value;
                }
                else
                {
                    change -= numCoinsToReturn[i] * _coinArray[i].Value;
                }
                _coinArray[i].CoinCount -= numCoinsToReturn[i];
            }

            Coin.TotalInsValue = change;

            coinDispenser500Yen.Actuate(numCoinsToReturn[3]);
            coinDispenser100Yen.Actuate(numCoinsToReturn[2]);
            coinDispenser50Yen.Actuate(numCoinsToReturn[1]);
            coinDispenser10Yen.Actuate(numCoinsToReturn[0]);

            if (change > 0)
            {
                noChangeLight.TurnOn3Sec();
            }

            updateDebugDisplays();
        }
    }
}