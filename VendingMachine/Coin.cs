using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Coin
    {
        private static int _totalInsValue;
        public static int TotalInsValue
        {
            get
            {
                return _totalInsValue;
            }
            set
            {
                _totalInsValue = value;
            }
        }


        /// <summary>
        /// The given Coin's dispenser module.
        /// </summary>
        private CoinDispenser _cd;
        
        /// <summary>
        /// The coin value (in Yen).
        /// </summary>
        private int _value;

        /// <summary>
        /// The value getter.
        /// </summary>
        public int Value
        {
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// The total available coin of this type (including inserted coins).
        /// </summary>
        private int _coinCount;
        public int CoinCount
        {
            get
            {
                return _coinCount;
            }
            set
            {
                _coinCount = value;
            }
        }


        public Coin(int value, int changeToStart, CoinDispenser cd)
        {
            _cd = cd;
            _value = value;
            _coinCount = changeToStart;
        }

        /// <summary>
        /// Inserts a coin, adding to total number and total inserted counts.
        /// </summary>
        public void InsertCoin()
        {
            _coinCount++;
            _totalInsValue += _value;
        }

        public void ResetValues(int changeToStart)
        {
            _coinCount = changeToStart;
            _totalInsValue = 0;
        }
    }
}
