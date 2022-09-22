using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlyTaloWPF.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TalonLampoTesti()// Talon lämpötila voidaan asettaa vähintään 19 ja max 32
        {
            Thermostat t = new Thermostat();

            t.Temperature = 19;

            try
            {
                t.Temperature = 8;  //Yritetään asettaa lämmöksi 8

                Assert.Fail("Poikkeuksen nosto epäonnistui");
            }
            catch (ArgumentOutOfRangeException)
            {
                //Poikkeuksen nosto onnistui - Ohjelmassa pyytää antamaan uuden arvon josse on alle 19 tai yli 32
            }

        }

        [TestMethod]//Saunan lämmitys katkeaa kun se saavuttaa 100 astetta
        public void SaunaTesti()
        {
            Sauna saunanLampo = new Sauna();
            saunanLampo.NykyinenLampo = 100;
            try
            {
                saunanLampo.NykyinenLampo = 104;

                Assert.Fail("Poikkeusta ei ole");
            }
            catch (ArgumentOutOfRangeException)
            {
                //Onnistui - poikkeusta ei ole

            }
        }

        [TestMethod]
       public void ValoSliderTesti() //Valonslideri maximi on 100
        {
            Lights sliderivalue = new Lights();
            sliderivalue.SwitchON(90);
            try
            {
                sliderivalue.SwitchON(110);
                Assert.Fail("Epäonnistui");
            }
            catch (ArgumentOutOfRangeException)
            {
                //Onnistui, otettiin exception kiinni
            }
        }
    }
}
