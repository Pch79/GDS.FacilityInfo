using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace GDS.KwpAddOns.KwpData72.StaticData
{
    public static class StaticValues
    {
        public enum KwpActivityStatus
        {
            [DisplayName("offen")]
            offen = 100,
            [DisplayName("in Bearbeitung")]
            inBearbeitung = 200,
            [DisplayName("zurückgestellt")]
            zurückgestellt = 210,
            [DisplayName("erledigt")]
            erledigt = 300
        }

        public enum KwpBestellStatus
        {
            [DisplayName("Bestellung angelegt")]
            BestellungAngelegt = 0,
            [DisplayName("Bestellung erfasst")]
            BestellungErfasst = 1,
            [DisplayName("Bestellung gedruckt")]
            BestellungGedruckt = 2,
            [DisplayName("Teillieferung")]
            Teillieferung = 3,
            [DisplayName("Lieferung erfolgt")]
            LieferungErfolgt = 4,
            [DisplayName("Bestellung abgeschlossen")]
            BestellungAbgeschlossen = 7,
            [DisplayName("Bestellung storniert")]
            BestellungStorniert = 90
        }

        public enum KwpRegieStatus
        {
            [DisplayName("nicht Freigegeben")]
            nichtFreigegeben = 0,
            [DisplayName("Auftrag erhalten ohne Rechnung")]
            AuftragErhaltenOhneRechnung = 1,
            [DisplayName("Auftrag Rechnung gedruckt")]
            AuftragRechnungGedruckt = 2,
            [DisplayName("ohne Rechnung abgeschlossen")]
            ohneRechnungAbgeschlossen = 3,
            [DisplayName("Auftrag noch nicht vergeben")]
            AuftragNochNichtVergeben = 4,
            [DisplayName("Auftrag erhalten")]
            AuftragErhalten = 5,
            [DisplayName("Auftrag abgeschlossen")]
            AuftragAbgeschlossen = 6,
            [DisplayName("Auftrag nicht erhalten")]
            AuftragNichtErhalten = 7
        }

        public enum KwpProjektStatus
        {
            [DisplayName("Auftrag noch nicht vergebn")]
            AuftragNochNichtVergeben = 0,

            [DisplayName("Auftrag nicht erhalten")]
            AuftragNichtErhalten = 1,

            [DisplayName("Bauvorhaben neu ausgeshrieben")]
            BauvorhabenNeuAusgeschrieben = 2,

            [DisplayName("Bauvorhaben mit Ersatzangebot")]
            BauvorhabenMitErsatzangebot = 3,

            [DisplayName("Auftragsvergabe zurückgestellt")]
            AuftragsvergabeZurueckgestellt = 4,

            [DisplayName("Auftrag nicht erhalten, zu teuer")]
            AuftragNichtErhaltenZuTeuer = 5,

            [DisplayName("Auftrag nicht erhalten, sonstige Gründe")]
            AuftragNichtErhaltenSonstigeGruende = 6,

            [DisplayName("Auftrag zugesagt")]
            AuftragZugesagt = 7,

            [DisplayName("Auftrag eingegangen")]
            AuftragEingegnagen = 8,

            [DisplayName("Auftrag abgeschlossen")]
            AuftragAbgeschlossen = 9

        }

        public enum KwpRechnungsTypen
        {
            [DisplayName("Rechnung")]
            Rechnung = 0,
            [DisplayName("Gutschrift")]
            Gutschrift = 2,
            [DisplayName("Abschlagszahlung")]
            Abschlagszahlung = 3,
            [DisplayName("Schlussrechnung")]
            Schlussrechnung = 4
        }

        public enum Monat
        {
            Januar = 1,
            Februar = 2,
            März = 3,
            April = 4,
            Mai = 5,
            Juni = 6,
            Juli = 7,
            August = 8,
            September = 9,
            Oktober = 10,
            November = 11,
            Dezember = 12

        }

        public enum AdressArt
        {
            [DisplayName("Kunde")]
            Kunde = 0,
            [DisplayName("Lieferant")]
            Lieferant = 1,
            [DisplayName("Interessent")]
            Interessent=2,
            [DisplayName("Sonstige")]
            Sonstige = 3
        }
    }

}
