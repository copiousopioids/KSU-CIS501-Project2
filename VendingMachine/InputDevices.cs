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
        private Coin _coinAttached;

        public Coin CoinAttached
        {
            get
            {
                return _coinAttached;
            }
        }

        public CoinInserter(Coin c)
        {
            _coinAttached = c;
        }
        public void CoinInserted()
        {
            _coinAttached.InsertCoin();
        }

    }

    public class PurchaseButton
    {

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
            _canAttached.PurchaseCan();
        }
    }

    public class CoinReturnButton
    {
        private VendingMachine _vmAttached;
        //private Controller _controlAttached;

        public CoinReturnButton(VendingMachine vm)
        {
            _vmAttached = vm;
        }
        public void ButtonPressed()
        {
            _vmAttached.ReturnCoins(Coin.TotalInsValue);
        }

        //public CoinReturnButton(Controller ctrl)
        //{
        //    _controlAttached = ctrl;
        //}
        //public void ButtonPressed()
        //{
        //    _controlAttached.ReturnCoins(Coin.TotalInsValue);
        //}
    }
}
