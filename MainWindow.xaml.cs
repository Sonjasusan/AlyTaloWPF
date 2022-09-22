using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Speech.Synthesis;

namespace AlyTaloWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Sauna sauna = new Sauna();
        Thermostat talonLampotila = new Thermostat();
        public DispatcherTimer SaunaHeater = new DispatcherTimer();
        public DispatcherTimer SaunaCooler = new DispatcherTimer();

       

        Boolean KeittioSwitched = true; //Keittiön valot
        Boolean OloHuoneSwitched = true; //Olohuoneen valot
        Boolean Switched = true; //saunan switchi

        public MainWindow()
        {
            InitializeComponent();

            //Saunan mittari timeri
            SaunaHeater.Tick += SaunaHeater_Tick;
            SaunaHeater.Interval = new TimeSpan(0, 0, 0, 1);
            
            SaunaCooler.Tick += SaunaCooler_Tick;
            SaunaCooler.Interval = new TimeSpan(0, 0, 0, 1);
           
        }

        private void ValoSlider_keittio_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var ValoSlider = sender as Slider;
            double value = ValoSlider.Value;
            txtKeittioValot.Text = "Kirkkaus " + value.ToString("0") + " %  /" + ValoSlider.Maximum + " %";
        }
        private void ValoSlider_Olohuone_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var ValoSlider = sender as Slider;
            double value = ValoSlider_Olohuone.Value;
            txtOlohuoneValot.Text = "Kirkkaus " + value.ToString("0") + " %  /" + ValoSlider.Maximum + " %";
        }

        //Olohuoneen valot päälle
        private void btnOlohuoneOn_Click(object sender, RoutedEventArgs e)
        {
            OloHuoneSwitched = true;
            ValoSlider_Olohuone.Value = 100;
            txtOlohuoneValot.Text = "Olohuoneen valot päällä " + ValoSlider_Olohuone.Value;
        }

        //Olohuone valot pois
        private void btnOlohuoneOff_Click(object sender, RoutedEventArgs e)
        {
            OloHuoneSwitched = false;
            ValoSlider_Olohuone.Value = 0;
            txtOlohuoneValot.Text = "";
        }

        //Keittiönvalot päälle
        private void btnKeittioOn_Click(object sender, RoutedEventArgs e)
        {
            KeittioSwitched = true;
            ValoSlider_keittio.Value = 100;
            txtKeittioValot.Text = "Keittiönvalot päällä " + ValoSlider_keittio.Value;
        }

        //Keittiönvalot pois
        private void btnKeittioOff_Click(object sender, RoutedEventArgs e)
        {
            KeittioSwitched = false;
            ValoSlider_keittio.Value = 0;
            txtKeittioValot.Text = "";
        }

        //Aiemmin asetetun arvon muistaminen asetuksen avulla (=project-properties-settings-string):
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.textbox = txtTalonLampo.Text;
            Properties.Settings.Default.Save();
        }
        //Kun wpf-sovellus käynnistyy lämpötilan teksti säilyy kentässä
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTalonLampo.Text = Properties.Settings.Default.textbox;
        }

        private void btnAsetaLampoTila_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                talonLampotila.Temperature = int.Parse(txtAsetaLampo.Text);
                txtTalonLampo.Text = txtAsetaLampo.Text;
                
                sauna.NykyinenLampo = int.Parse(txtAsetaLampo.Text);
                cgSaunaGauge.CurrentValue = sauna.NykyinenLampo;
               
            }
            catch (Exception)
            {
                MessageBox.Show ("Talon lämpötila voi olla vähintään 19 ja enintään 32 astetta, aseta uusi lämpötila!");
                txtAsetaLampo.Text = "";
                txtAsetaLampo.Focus();
            }

        }
        private void SaunaHeater_Tick(object sender, EventArgs e)
        {
            sauna.NykyinenLampo+=0.5;
            cgSaunaGauge.CurrentValue = sauna.NykyinenLampo;
            txtSaunapaalla.Text = "Saunaa lämmitetään " + "\n"+ sauna.NykyinenLampo.ToString();
            if (sauna.NykyinenLampo >=100)
            {
                SaunaHeater.Stop();
                txtSaunapaalla.Text = "Sauna on lämmitetty, asteita " + sauna.NykyinenLampo.ToString() + "\n" + "Hyviä löylyjä!";
            }
            
        }

        private void SaunaCooler_Tick(object sender, EventArgs e)
        {
            sauna.NykyinenLampo--;
            txtSaunapaalla.Text = "Saunaa viilennetään, asteita " + sauna.NykyinenLampo.ToString();
            cgSaunaGauge.CurrentValue = sauna.NykyinenLampo;
            if (sauna.NykyinenLampo <= int.Parse(txtAsetaLampo.Text))
            {
                SaunaCooler.Stop();
                txtSaunapaalla.Text = txtAsetaLampo.Text;
                
             }
   
        }

        private void btnSaunaPaalle_Click(object sender, RoutedEventArgs e)
        {
            Switched = true;
            txtSaunapaalla.Text = "SAUNA PÄÄLLÄ";
            btnIndicator.Background = Brushes.Orange;
            SaunaCooler.Stop();

            if (!SaunaHeater.IsEnabled)
            {
                
                SaunaHeater.Start();
            }
            sauna.SaunaOn();
            txtSaunapaalla.Text = "SAUNA PÄÄLLÄ " +sauna.NykyinenLampo.ToString();
            cgSaunaGauge.CurrentValue = sauna.NykyinenLampo;

        }

        private void btnSaunaPois_Click(object sender, RoutedEventArgs e)
        {
            Switched = false;
            sauna.SaunaOff();
            btnIndicator.Background = Brushes.LightGray;
            SaunaHeater.Stop();
            SaunaCooler.Start();
        }

        private void btnPuhe_Click(object sender, RoutedEventArgs e)
        {
                SpeechSynthesizer synth = new SpeechSynthesizer();

                // Configure the audio output.   
                synth.SetOutputToDefaultAudioDevice();

                // Speak a string.  
                synth.Speak("Hi there! Welcome to hear your current house statistics.");
                synth.Speak("Current temperature at your house is on: " + txtTalonLampo.Text + "celsius.");

            if (OloHuoneSwitched== true)
            {
                synth.Speak("Living room lights are on "  + "Slider value is " + ValoSlider_Olohuone.Value.ToString());
            }
            else if (OloHuoneSwitched== false)
            {
                synth.Speak("Living room lights are off" +"Slider value is " 
                    + ValoSlider_Olohuone.Value.ToString());
            }

            if (KeittioSwitched == true)
            {
                synth.Speak("Kitchen lights are on " + "Slider value is "
               + ValoSlider_keittio.Value.ToString());
            }

            else if (KeittioSwitched == false)
            {
                synth.Speak("Kitchen lights are off " + "Slider value is " + ValoSlider_keittio.Value.ToString("0"));
            }

            if (Switched== true)
            {
                synth.Speak("Your sauna is on.");
                synth.Speak("Sauna is currently warming up at: " + sauna.NykyinenLampo.ToString() + "celsius. " +
                    "Sauna will be warm in less than 15 minutes.");
            }
            else if (Switched==false)
            {
                synth.Speak("Sauna is not on right now. Current temperature in sauna is the same as in the house.");
            }
            
           
            }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();

            //Audio output 
            synth.SetOutputToDefaultAudioDevice();

            synth.Speak("Hi there! You clicked the info button. So, here is some info how to use this app.");
            synth.Speak("Kitchen and living room lights can be switched through buttons on, or off.");
            synth.Speak("For the dimming, just slide the valoslider under both lights.");
            synth.Speak("For both lights there is a textbox, which shows you if the lights are switched on, or off and what is the current dimming slider value.");
            synth.Speak("Lastly inputted house temperature is showing in textbox.");
            synth.Speak("For choosing house temperature just write the value and save it by clicking the button under it.");
            synth.Speak("House temperature can be between 19 and 32 celsius. If you input value under or over that, the app will ask you to input a new value.");
            synth.Speak("Sauna can be controlled through buttons on, or off.");
            synth.Speak("To sauna, there is also a textbox which shows you the current temperature in sauna.");
            synth.Speak("Under the sauna buttons there is also a circular gauge, which shows also the temperature and if sauna is currently warming or cooling.");
            synth.Speak("Next there will come a message box which tells you info how to use this app in finnish.");
            synth.Speak("After that, you can choose these things and when you are ready, click the button next to this button to" +
           "hear your house statistics.");

            MessageBox.Show("Ohjelman käyttö-ohje:" + "\n" + "VALOT:"+ "\n" +"- Olohuoneen ja keittiönvaloja voit hallita ON ja OFF-painikkeista" +"\n" +
                "- Valonkirkkautta hallitaan valosliderilla, joka on molempien valojen asetusten alapuolella." + " Lisäksi molemmille valoille on textboxit, jotka kertovat sen hetkisen tilanteen." 
                +"\n" + "TALON LÄMPÖTILA:" + "\n" + "- Ohjelma näyttää aiemmin annetun lämpötila-arvon textboxissa."
                +" Talon lämpötila asetetaan antamalla arvo, aseta lämpötila- napin yläpuolella." + "\n" +
                "- Talon lämpötila voi olla vähintään 19 ja enintään 32 astetta, jos syötät ali tai yli menevän arvon ohjelma pyytää sinua antamaan uuden arvon."+
                "\n" +"SAUNA:" + "\n" + "- Saunaa hallitaan samalla tapaa kuin valoja; ON ja OFF-painikkeista." + "\n"+
                "- Saunan tilanteen näet textboxista. Ja mittarista." + "\n" + "- Sauna lämpeää maximissaan 100 asteeseen."+ "\n"+
                "- Saunan viilentäminen alkaa kun painat OFF-nappia." + "\n"+ "- Halutessasi voit kuunnella talon tiedot, painamalla kuuntele-nappia."+
                "\n" + "- Kuuntelu-ominaisuus toimii parhaiten kun olet valinnut valojen asennot, lämpötilan ja saunan.");
        }
    }
    }

    

