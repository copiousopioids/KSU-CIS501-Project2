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

        private static CoinReturnButton _crb;
        public static CoinReturnButton CoinReturn
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

        public Can(string name, int count, int price, Light pl, Light sol, CanDispenser cd)
        {
            _name = name;
            _count = count;
            _price = price;
            _purchaseableLight = pl;
            _soldOutLight = sol;
            _canDispenser = cd;
        }

        public void PurchaseCan()
        {
            if (_purchaseableLight.IsOn())
            {
                _canDispenser.Actuate();
                _count--;
                Coin.TotalInsValue -= _price;
                UpdateLights(Coin.TotalInsValue);
                Can.CoinReturn.ButtonPressed();
            }
        }

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
