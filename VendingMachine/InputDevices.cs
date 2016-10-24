//////////////////////////////////////////////////////////////////////
//      Vending Machine (Actuators.cs)                              //
//      Written by Masaaki Mizuno, (c) 2006, 2007, 2008, 2010, 2011 //
//                      for Learning Tree Course 123P, 252J, 230Y   //
//                 also for KSU Course CIS501                       //  
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    // For each class, you can (must) add fields and overriding constructors

    public class CoinInserter
    {
        // add a field to specify an object that CoinInserted() will first visit
        private Coin _coinAttached;

        public Coin CoinAttached
        {
            get
            {
                return _coinAttached;
            }
        }

        // rewrite the following constructor with a constructor that takes an object
        // to be set to the above field
        public CoinInserter(Coin c)
        {
            _coinAttached = c;
        }
        public void CoinInserted()
        {
            // You can add only one line here
            _coinAttached.InsertCoin();
        }

    }

    public class PurchaseButton
    {
        // add a field to specify an object that ButtonPressed() will first visit

        private Can _canAttached;

        public Can CanAttached
        {
            get
            {
                return _canAttached;
            }
        }

        public PurchaseButton(Can c)
        {
            _canAttached = c;
        }
        public void ButtonPressed()
        {
            // You can add only one line here
            _canAttached.PurchaseCan();
        }
    }

    public class CoinReturnButton
    {
        // add a field to specify an object that Button Pressed will visit
        private VendingMachine _vendingMachineAttached;

        // replace the following default constructor with a constructor that takes
        // an object to be set to the above field
        public CoinReturnButton(VendingMachine vm)
        {
            _vendingMachineAttached = vm;
        }
        public void ButtonPressed()
        {
            // You can add only one line here
            _vendingMachineAttached.ReturnCoins(Coin.TotalInsValue);
        }
    }
}
