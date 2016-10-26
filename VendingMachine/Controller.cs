using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Controller
    {
        private AmountDisplay _ad;
        private TimerLight _noChangeLight;
        //private CoinReturnButton _crb;

        private Coin[] _coinArray;
        public Coin[] Coins
        {
            get
            {
                return _coinArray;
            }
            set
            {
                _coinArray = value;
            }
        }

        private Can[] _canArray;
        public Can[] Cans
        {
            get
            {
                return _canArray;
            }
            set
            {
                _canArray = value;
            }
        }

        public Controller(AmountDisplay ad, TimerLight noChange)
        {
            _ad = ad;
            _noChangeLight = noChange;
        }

        public Controller(AmountDisplay ad, TimerLight noChange, /*CoinReturnButton crb,*/ Coin[] coinArray, Can[] canArray)
        {
            _ad = ad;
            _noChangeLight = noChange;
            //_crb = crb;
            _coinArray = coinArray;
            _canArray = canArray;
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

            for (int i = 0; i < _coinArray.Length; i++)
            {
                _coinArray[i].CD.Actuate(numCoinsToReturn[i]);
            }

            if (change > 0)
            {
                _noChangeLight.TurnOn3Sec();
            }

            UpdateDisplays();
        }

        /// <summary>
        /// Refreshes the displays. Called by updateDebugDisplays for ease of use.
        /// </summary>
        public void UpdateDisplays()
        {
            _ad.DisplayAmount(Coin.TotalInsValue);

            for (int i = 0; i < _canArray.Length; i++)
            {
                _canArray[i].UpdateLights(Coin.TotalInsValue);
            }
        }


        /// <summary>
        /// Resets the machine to its begin state.
        /// </summary>
        /// <param name="numCoins"></param>
        /// <param name="numCans"></param>
        public void ResetControl(int[] numCoins, int[] numCans)
        {
            Coin.TotalInsValue = 0;
            for (int i = 0; i < _coinArray.Length; i++)
            {
                _coinArray[i].ResetValues(numCoins[i]);
            }

            for (int i = 0; i < _coinArray.Length; i++)
            {
                _canArray[i].Count = numCans[i];
            }

            UpdateDisplays();
        }
    }
}
