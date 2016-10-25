using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Can
    {

        private Light _purchaseableLight;
        private Light _soldOutLight;
        private CanDispenser _canDispenser;
        private CoinReturnButton _crb;
        public CoinReturnButton CoinReturn
        {
            get
            {
                return _crb;
            }
            set
            {
                _crb = value;
            }
        }


        private string _name;

        private int _count;
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }
        }

        private int _price;

        /// <summary>
        /// The Can construcor setting all the values and passing the objects to control and display Can information.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="price"></param>
        /// <param name="pl"></param>
        /// <param name="sol"></param>
        /// <param name="cd"></param>
        /// <param name="crb"></param>
        public Can(string name, int count, int price, Light pl, Light sol, CanDispenser cd, CoinReturnButton crb)
        {
            _name = name;
            _count = count;
            _price = price;
            _purchaseableLight = pl;
            _soldOutLight = sol;
            _canDispenser = cd;
            _crb = crb;
        }

        /// <summary>
        /// Purchases a can. Reaches through to the controller and "presses" the coin return.
        /// </summary>
        public void PurchaseCan()
        {
            if (_purchaseableLight.IsOn())
            {
                _canDispenser.Actuate();
                _count--;
                Coin.TotalInsValue -= _price;
                UpdateLights(Coin.TotalInsValue);
                _crb.ButtonPressed();
            }
        }

        /// <summary>
        /// Updates the lights corresponding to the can.
        /// </summary>
        /// <param name="amount"></param>
        public void UpdateLights(int amount)
        {
            if (amount >= _price && !_soldOutLight.IsOn())
                _purchaseableLight.TurnOn();
            else
                _purchaseableLight.TurnOff();
            if (_count <= 0)
                _soldOutLight.TurnOn();
            else
                _soldOutLight.TurnOff();
        }

    }
}
