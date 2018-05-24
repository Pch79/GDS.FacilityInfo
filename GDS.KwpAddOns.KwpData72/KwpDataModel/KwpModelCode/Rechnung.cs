using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using static GDS.KwpAddOns.KwpData72.StaticData.StaticValues;
using System.Linq;
using System.Linq.Expressions;
using GDS.KwpAddOns.KwpData72.StaticData;
using System.Text.RegularExpressions;
using DevExpress.ExpressApp.DC;

namespace GDS.KwpAddOns.KwpData72.KwpDataModel.BNWINS_Demo_new
{

    public partial class Rechnung
    {
        public Rechnung(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Eigene Properties

        //[DevExpress.ExpressApp.DC.XafDisplayName("Erlöskonto")]
        //public 

        //eine Liste der Rechnungspositionen gruppiert nach Kostenarten
        [DevExpress.ExpressApp.DC.XafDisplayName("H-Summe")]
        public System.Decimal Hsum
        {
            get
            {
                decimal retval = 0;
                var Hquery = from RechPos item in this.RechPosCollection
                             where item.SHKennzeichen == "H"
                             select item.RechnungsPosition_PositionsBetrag;
                retval = Convert.ToDecimal(Hquery.Sum());
                return retval;
            }
        }


        [DevExpress.ExpressApp.DC.XafDisplayName("S-Summe")]
        public System.Decimal Ssum
        {
            get
            {
                decimal retval = 0;
                var Squery = from RechPos item in this.RechPosCollection
                             where item.SHKennzeichen == "S"
                             select item.RechnungsPosition_PositionsBetrag;
                retval = Convert.ToDecimal(Squery.Sum());
                return retval;
            }
        }

        [XafDisplayName("Positionssaldo")]
        public System.Decimal PosSaldo
        {
            get
            {
                return Math.Abs(this.Ssum - this.Hsum);
            }
        }
        


        [DevExpress.ExpressApp.DC.XafDisplayName("Rechnungsadresse")]
        public adrAdressen Rechnung_RechnungsAdresse
        {
            get
            {
                adrAdressen retVal;
                if (this.ADRKUERZ != null)
                {
                    retVal = this.Session.FindObject<adrAdressen>(new BinaryOperator("AdrNrGes", this.ADRKUERZ, BinaryOperatorType.Equal));
                }
                else
                {
                    retVal = null;
                }

                return retVal;
            }
        }

        [XafDisplayName("Betragspositionen")]
        public IList<RechPos> lstBetragspositionen
        {
            get
            {

                IList<RechPos> lstPos;
                var BetragsPosQuery = from RechPos item in this.RechPosCollection
                                      where item.RechnungsPosition_PositionsBetrag > 0
                                      select item;
                lstPos = BetragsPosQuery.ToList<RechPos>();

                return lstPos;
            }
        }


        public System.Double PositionsSummeBrutto
        {
            get
            {
                double retVal;
              
                var posSummeBrutto = from RechPos item in this.RechPosCollection
                                    select item.PositionsbetragBrutto;
                retVal = posSummeBrutto.Sum();

                return retVal;
            }
        }

        //aus den Betragspostitionen die mit dem maximalen Betrag ermitteln
        public RechPos MaxBetragPosition
        {
            get
            {
                RechPos retVal;
                if (this.lstBetragspositionen.Count > 0)
                {
                    //das MAximum rausfinden

                    var maxValue = this.lstBetragspositionen.Max(x => x.RechnungsPosition_PositionsBetrag);
                    var result = this.lstBetragspositionen.First(x => x.RechnungsPosition_PositionsBetrag == maxValue);

                    retVal = result;
                    
                        }
                else
                {
                    retVal = null;
                }
                
                return retVal;
            }
        }
        //Rechnungsbetrag netto und Positiosnbetag nett
        //die nettosumme der Positionen muss den USTBAsiswert ergeb

        public System.Double PositionsSummeNetto
        {
            get
            {
                double retVal;
                var posSummeNetto = from RechPos item in this.RechPosCollection
                                    select item.RechnungsPosition_PositionsBetrag;
                retVal = posSummeNetto.Sum();

                return retVal;
            }
        }

        //Steuersumme aus den Positionen
        public System.Double Ustsumme
        {
            get
            {
                double retVal;
                double steuerfaktor = this.MwstSatz / 100;
                var steuerSummeQuery = from RechPos item in this.Rechnungspositionen
                                     select item.Steueranteil;
                retVal = steuerSummeQuery.Sum();
                return retVal;
            }
        }

        public System.Double UstDifferenz
        {
            get
            {
                System.Double retVal;
                //den ustwert aus der Rechnung m,it dem aus der Summenbildung vergleichen


                double steuerteil;
                double differenz;

                
                differenz = ((this.USTWERT * 100) - (this.Ustsumme * 100));
                retVal = differenz / 100;


                return Math.Round(retVal,2);
            }
        }

        //Rundungsdifferenz ust berechenen
        public System.Double RundungsdifferenzUst
        {
            get
            {
                System.Double retVal;
                double steuerteil;
                double differenz;

                steuerteil= this.USTBASIS * (this.MwstSatz / 100);
                   differenz = ((this.USTWERT * 100) -(steuerteil * 100));
                retVal = differenz/100;
                   
                
                return retVal;
            }
        }

        public System.Decimal Steueranteil
        {
            get
            {
                decimal steueranteil;
                 steueranteil = Convert.ToDecimal(this.USTBASIS * (this.MwstSatz / 100));
                return steueranteil;
            }
        }

        //Die Summe der Positionen *Mwst Satz
        public System.Double BruttowertBerechnet
        {
            get
            {
                double retVal;
                double steuerfaktor = this.MwstSatz / 100;
                double steueranteil;
                steueranteil = this.PositionsSummeNetto * steuerfaktor;

                retVal = this.PositionsSummeNetto + steueranteil;

                return retVal;
            }
        }


        //Stueranteil der Positionen
        public System.Double SteueranteilBerechnet
        {
            get
            {
                
                double steuerfaktor = this.MwstSatz / 100;
                double steueranteil;
                steueranteil = this.PositionsSummeNetto * steuerfaktor;

                //retVal = this.PositionsSummeNetto + steueranteil;

                return steueranteil;
            }
        }

        //wenn die Abweichung der Positionen 
        //die Betragsstärkste Position
        



        //dann b rauch ich die Position mit dem grössten betrag

        [XafDisplayName("Gesamtabzug")]
        public System.Double Gesamtabzug
        {
            get
            {
                return this.ZESonstAbzug + this.ZUABSCHLAGWERT;
            }
        }

        public System.Double PositionssummeBereingt
        {
            get
            {
                return this.PositionsSummeNetto + this.Gesamtabzug;
            }
        }
        [DevExpress.ExpressApp.DC.XafDisplayName("Rechnungsbetreff")]
        public System.String Rechnung_ClearBetreff
        {
            get
            {
                var retVal = string.Empty;
                //die Zeilenumbrüche entfernen
                var oldBetreff = string.Empty;
                if (this.BETREFF != null && this.BETREFF.Length > 1)
                {
                    oldBetreff = this.BETREFF;
                    if (oldBetreff.Contains("\r") || oldBetreff.Contains("\n"))
                    {
                        retVal = Regex.Replace(oldBetreff, @"\r\n?|\n", " ");
                    }
                    else
                    {
                        retVal = oldBetreff;
                    }
                }
                else
                {
                    retVal = oldBetreff;
                }
                return retVal;
            }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName("Rechnungstyp")]
        public KwpRechnungsTypen Rechnung_RechnungsTyp
        {
            get
            {

                KwpRechnungsTypen rechtyp = (KwpRechnungsTypen)Enum.ToObject(typeof(KwpRechnungsTypen), this.RECHTYP);
                return rechtyp;
            }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName("Erlösgruppe")]
        public ErlGrp Rechnung_ErloesGruppe
        {
            get
            {
                ErlGrp retVal;
                retVal = this.Session.FindObject<ErlGrp>(new BinaryOperator("ABTNR", this.ABTEILUNG, BinaryOperatorType.Equal));

                return retVal;
            }
        }


        [DevExpress.ExpressApp.DC.XafDisplayName("Rechnungsjahr")]
        public Int32 RechnungsJahr
        {
            get
            {
                Int32 retVal = 0;
                if (this.ERSTDAT != null)
                {
                    retVal = ERSTDAT.Year;
                }
                return retVal;
            }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName("Rechnungsmonat")]
        public Int32 Rechnung_RechnungsMonat
        {
            get
            {
                Int32 retVal = 0;
                if (this.ERSTDAT != null)
                {
                    retVal = ERSTDAT.Month;
                }
                return retVal;
            }
        }


        [DevExpress.ExpressApp.DC.XafDisplayName("Monatsbezeichnung")]
        public string Rechnung_RechnungsMonatName
        {
            get
            {
                Int32 retVal = 0;
                if (this.ERSTDAT != null)
                {
                    retVal = ERSTDAT.Month;
                }
                return Enum.GetName(typeof(StaticValues.Monat), retVal);
            }
        }



        [DevExpress.Xpo.DisplayName(@"OP-Betrag")]
        [PersistentAlias("Iif([RECHTYP] = 4, Iif([ZAHLBETR] > 0, 0, [RestBetrag]), [RestBetrag])")]
        public decimal OPBetrag
        {
            get
            {
                // Abschlagsrechnung mit Zahlung werden mit 0 ausgegeben
                decimal retVal = 0;

                if (this.Rechnung_RechnungsTyp == KwpRechnungsTypen.Abschlagszahlung && ZAHLBETR > 0)
                    return 0;
                double verrechnetAz = 0;
                //return Convert.ToDecimal(BRUTTOWERT-VerrBetrag-ZESonstAbzug-ZAHLBETR);
                if (this.Rechnung_RechnungsTyp != KwpRechnungsTypen.Gutschrift)
                {
                    //wenn es sich um eine SR handelt b rauch ich die darin verrechneten AZ
                    if (this.Rechnung_RechnungsTyp == KwpRechnungsTypen.Schlussrechnung)
                    {
                        XPCollection<Rechnung> lstVerrechnetAz = new XPCollection<Rechnung>(this.Session, new BinaryOperator("AZVerrechnetIn", this.RECHNR, BinaryOperatorType.Equal));
                        if (lstVerrechnetAz.Count > 0)
                        {
                            //die verrechneten Beträge abfragen
                            var verrechnetQuery = (from Rechnung item in lstVerrechnetAz
                                                   select item.VerrBetrag).Sum();
                            verrechnetAz = verrechnetQuery;
                        }
                    }
                    retVal = Convert.ToDecimal(BRUTTOWERT - VerrBetrag - ZESonstAbzug - verrechnetAz);
                }

                //bei Gutschriften macht ein OP-betrag keinen Sinn

                return retVal;
            }
        }

        public String OpStatus
        {
            get
            {
                try
                {
                    if (ZAHLUNGSTATUS == "0")
                        return "offen";
                    if (ZAHLUNGSTATUS == "1")
                        return "Teilweise";
                    if (ZAHLUNGSTATUS == "2")
                        return "bezahlt";
                    if (ZAHLUNGSTATUS == "8")
                        return "Teilzahlung";
                    if (ZAHLUNGSTATUS == "9")
                        return "Verrechnet";
                }
                catch (Exception ex)
                {
                    return "N/A";
                }
                return "offen";
            }
        }

        [ModelDefault("Caption", "fällig KW")]
        public String FälligKW
        {
            get
            {
                String t = "KW 0";
                try
                {
                    if (FälligTage != null)
                    {

                        if (FälligTage < 1)
                            t = "KW 0";
                        if (FälligTage > 0 && FälligTage < 8)
                            t = "KW 1";
                        if (FälligTage > 7 && FälligTage < 15)
                            t = "KW 2";
                        if (FälligTage > 14 && FälligTage < 22)
                            t = "KW 3";
                        if (FälligTage > 21 && FälligTage < 29)
                            t = "KW 4";
                        if (FälligTage > 28 && FälligTage < 36)
                            t = "KW 5";
                        if (FälligTage > 35 && FälligTage < 43)
                            t = "KW 6";
                        if (FälligTage > 42)
                            t = "KW 7";
                    }
                }
                catch (Exception ex)
                {
                    return "N/A";
                }
                return t;
            }
        }

        [ModelDefault("Caption", "KW 0")]
        public decimal KW0
        {
            get
            {
                Decimal t = 0;
                try
                {
                    if (FälligTage != null)
                    {
                        //if (FälligTage ==0)
                        if (FälligTage < 1)
                        {
                            return OPBetrag;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
                return t;
            }
        }

        [DevExpress.Xpo.DisplayName(@"Tage Fälligkeit")]
        [PersistentAlias("Iif(IsNullOrEmpty([NETTODATUM]) = False, DateDiffDay(Now(), [NETTODATUM]), 0)")]
        public int FälligTage
        {
            get { return (int)(EvaluateAlias("FälligTage")); }
        }

        [ModelDefault("Caption", "KW 1")]
        public decimal KW1
        {
            get
            {
                Decimal t = 0;
                try
                {
                    if (FälligTage != null)
                    {
                        if (FälligTage > 0 && FälligTage < 8)
                        {
                            return OPBetrag;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
                return t;
            }
        }
        [ModelDefault("Caption", "KW 2")]
        public decimal KW2
        {
            get
            {
                decimal t = 0;
                try
                {
                    if (FälligTage != null)
                    {
                        if (FälligTage > 7 && FälligTage < 15)
                        {
                            return OPBetrag;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
                return t;
            }

        }
        [ModelDefault("Caption", "KW 3")]
        public decimal KW3
        {
            get
            {
                Decimal t = 0;
                try
                {
                    if (FälligTage != null)
                    {
                        if (FälligTage > 14 && FälligTage < 22)
                        {
                            return OPBetrag;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
                return t;
            }

        }
        [ModelDefault("Caption", "KW 4")]
        public decimal KW4
        {
            get
            {
                Decimal t = 0;
                try
                {
                    if (FälligTage != null)
                    {
                        if (FälligTage > 21 && FälligTage < 29)
                        {
                            return OPBetrag;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
                return t;
            }
        }

        [ModelDefault("Caption", "KW 5")]
        public decimal KW5
        {
            get
            {
                Decimal t = 0;
                try
                {
                    if (FälligTage != null)
                    {
                        if (FälligTage > 28 && FälligTage < 36)
                        {
                            return OPBetrag;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
                return t;
            }
        }
        [ModelDefault("Caption", "KW 6")]
        public decimal KW6
        {
            get
            {
                Decimal t = 0;
                try
                {
                    if (FälligTage != null)
                    {
                        if (FälligTage > 35 && FälligTage < 43)
                        {
                            return OPBetrag;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
                return t;
            }
        }

        [ModelDefault("Caption", "KW 7 ff")]
        public decimal KW7
        {
            get
            {
                Decimal t = 0;
                try
                {
                    if (FälligTage != null)
                    {
                        if (FälligTage > 42)
                        {
                            return OPBetrag;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
                return t;
            }
        }
        [DevExpress.ExpressApp.DC.XafDisplayNameAttribute("Rechnungspositionen")]
        public XPCollection<RechPos> Rechnungspositionen
        {
            get
            {
                XPCollection<RechPos> lstRechPos = new XPCollection<RechPos>(this.Session, new BinaryOperator("VorgangsNr", this.RECHNR, BinaryOperatorType.Equal));
                return lstRechPos;
            }
        }


        [DevExpress.ExpressApp.DC.XafDisplayName("Zahlungen zur Rechnung")]
        public XPCollection<RechZahlungen> Rechnung_Zahlungen
        {
            get
            {
                XPCollection<RechZahlungen> lstZahlungen = new XPCollection<RechZahlungen>(this.Session, new BinaryOperator("RechNr", this.RECHNR, BinaryOperatorType.Equal));
                return lstZahlungen;
            }
        }
        //kann ich hier gleich die schon verrechenten Positionen ausgeben??

        
        #endregion
    }
}
