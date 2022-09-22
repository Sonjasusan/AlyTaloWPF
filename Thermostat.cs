using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlyTaloWPF
{
    class Thermostat
    {
        private int tavoiteLampo; //Käytetään metodiin, jossa asetetaan tavoitelämpötila
        public int Temperature
        {
            get
            {
                return tavoiteLampo;
            }
            set
            {
                if ((value > 15) && (value <= 32))
                {
                    tavoiteLampo = value;
                }
                else
                {
                    tavoiteLampo = 0;
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

            public void SetTemperature(int UserInputTemperature)
        {
            if (((UserInputTemperature >= 15) &&(UserInputTemperature <= 32)))
            {
                tavoiteLampo = UserInputTemperature;
            }
            else
            {
                tavoiteLampo = 18;
                throw new ArgumentOutOfRangeException();
            }
          }
        public int GetTemperature()
        {
            return tavoiteLampo;
        }


    }
}
