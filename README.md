# AlyTaloWPF
WPF -kurssitehtävä; ÄlytaloWPF - Älytalon käyttöliittymä, kurssitehtävä WPF-sovellusten toteuttaminen visual studiolla -kurssilla.
- Jaettu kolmeen osaan; valot, talonlämpö ja sauna (jokainen toiminto omassa boksissaan).
- Käyttöliittymässä mahdollisuus kuunnella sen käyttö-ohjeet INFO-napista, jolloin käyttäjä kuulee ensiksi englanniksi ohjeet ja sitten ohjeet tulevat näkyviin suomeksi tekstinä.

Käyttäjällä mahdollisuus valita talonsa valojen päällä (tai pois päältä) olo ja valojen kirkkaus;
- Valojen päällä olo valitaan ON tai OFF -napeista, kirkkaus valitaan sliderista nappien alapuolelta.
- Käyttäjä näkee valitessaan valojen päällä olon ja kirkkauden tekstiboksissa

Talon lämpötila;
- Lämpötila valitaan kirjoittamalla aste väliltä 19-32 tekstiboksiin, ja klikkaamalla "aseta lämpötila" -nappia.
- Ensimmäisellä käyttökerralla käyttäjän tulee antaa aste myös viimeisin lämpötila -kohtaan.
- Jos käyttäjä kirjoittaa luvun, joka on yli 32 tai alle 19, ohjelma ilmoittaa asiasta ja pyytää antamaan uuden lämpötilan.
- Viimeisin lämpötila jää muistiin (näkyy seuraavalla käyttökerralla "viimeisin lämpötila"-kohdassa)

Sauna lämpeämään;
- Sauna laitetaan lämpeämään ON -napista (ja pois päältä OFF-napista).
- Saunan lämpötila näkyy tekstiboksissa ja erillisessä mittarissa.

Käyttäjä voi myös kuunnella talonsa tiedot klikkaamalla "kuuntele" -napista; 
- Ohjelma kertoo englanniksi talontiedot (Valot, niiden kirkkaus, talonlämpötila ja onko sauna päällä, jos on niin lämpeääkö).
