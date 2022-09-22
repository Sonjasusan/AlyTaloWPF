using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlyTaloWPF
{
    class Sauna
    {
        public Boolean Switched { get; set; }
        public double NykyinenLampo { get; set; }
 
        private int saunanLampotila;
        
        public void SaunaOn()
        {
            Switched = true;
            //NykyinenLampo++;

        }
        public void SaunaOff()
        {
            Switched = false;
            //NykyinenLampo--;
        }

        public void SetMaxLampotila(int MaxLampotila)
        {
            saunanLampotila = 100;
            //if ((NykyinenLampo >= 18) && (MaxLampotila <= 100))
            //{
            //    saunanLampotila = 100;
            //}
            //else
            //{
            //    saunanLampotila = 19;
            //}
        }

        public int GetMaxLampoTila()
        {
            return saunanLampotila;

        }
        
    }
}
