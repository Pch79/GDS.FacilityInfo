﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace GDS.KwpAddOns.KwpData72.KwpDataModel.BNWINS_Demo_new
{

    public partial class WartAuftraege : XPLiteObject
    {
        string fAuftragsNr;
        [Key]
        [Size(15)]
        public string AuftragsNr
        {
            get { return fAuftragsNr; }
            set { SetPropertyValue<string>("AuftragsNr", ref fAuftragsNr, value); }
        }
        string fAnlagenNr;
        [Indexed(Name = @"AnlagenNr")]
        [Size(15)]
        public string AnlagenNr
        {
            get { return fAnlagenNr; }
            set { SetPropertyValue<string>("AnlagenNr", ref fAnlagenNr, value); }
        }
        DateTime fAnlageDatum;
        public DateTime AnlageDatum
        {
            get { return fAnlageDatum; }
            set { SetPropertyValue<DateTime>("AnlageDatum", ref fAnlageDatum, value); }
        }
        string fStatus;
        [Size(15)]
        public string Status
        {
            get { return fStatus; }
            set { SetPropertyValue<string>("Status", ref fStatus, value); }
        }
        string fBetreff;
        [Size(255)]
        public string Betreff
        {
            get { return fBetreff; }
            set { SetPropertyValue<string>("Betreff", ref fBetreff, value); }
        }
        DateTime fTerminDatum;
        public DateTime TerminDatum
        {
            get { return fTerminDatum; }
            set { SetPropertyValue<DateTime>("TerminDatum", ref fTerminDatum, value); }
        }
        DateTime fTerminUhrZeit;
        public DateTime TerminUhrZeit
        {
            get { return fTerminUhrZeit; }
            set { SetPropertyValue<DateTime>("TerminUhrZeit", ref fTerminUhrZeit, value); }
        }
        string fMonteur;
        [Size(50)]
        public string Monteur
        {
            get { return fMonteur; }
            set { SetPropertyValue<string>("Monteur", ref fMonteur, value); }
        }
        string fVerarbVoreinst;
        [Size(SizeAttribute.Unlimited)]
        public string VerarbVoreinst
        {
            get { return fVerarbVoreinst; }
            set { SetPropertyValue<string>("VerarbVoreinst", ref fVerarbVoreinst, value); }
        }
        int fAngelegtMK;
        [Indexed(Name = @"AngelegtMk")]
        public int AngelegtMK
        {
            get { return fAngelegtMK; }
            set { SetPropertyValue<int>("AngelegtMK", ref fAngelegtMK, value); }
        }
        string fAuftraggeber;
        public string Auftraggeber
        {
            get { return fAuftraggeber; }
            set { SetPropertyValue<string>("Auftraggeber", ref fAuftraggeber, value); }
        }
        double fAbtNr;
        public double AbtNr
        {
            get { return fAbtNr; }
            set { SetPropertyValue<double>("AbtNr", ref fAbtNr, value); }
        }
        DateTime fCheckDate;
        public DateTime CheckDate
        {
            get { return fCheckDate; }
            set { SetPropertyValue<DateTime>("CheckDate", ref fCheckDate, value); }
        }
        string fKoArt;
        [Size(SizeAttribute.Unlimited)]
        public string KoArt
        {
            get { return fKoArt; }
            set { SetPropertyValue<string>("KoArt", ref fKoArt, value); }
        }
        string fRohstPr;
        [Size(SizeAttribute.Unlimited)]
        public string RohstPr
        {
            get { return fRohstPr; }
            set { SetPropertyValue<string>("RohstPr", ref fRohstPr, value); }
        }
        string fLohnVorg;
        [Size(SizeAttribute.Unlimited)]
        public string LohnVorg
        {
            get { return fLohnVorg; }
            set { SetPropertyValue<string>("LohnVorg", ref fLohnVorg, value); }
        }
        string fProjAdr;
        [Size(24)]
        public string ProjAdr
        {
            get { return fProjAdr; }
            set { SetPropertyValue<string>("ProjAdr", ref fProjAdr, value); }
        }
        string fSachBearb;
        [Size(20)]
        public string SachBearb
        {
            get { return fSachBearb; }
            set { SetPropertyValue<string>("SachBearb", ref fSachBearb, value); }
        }
        string fextAuftragsNr;
        [Size(30)]
        public string extAuftragsNr
        {
            get { return fextAuftragsNr; }
            set { SetPropertyValue<string>("extAuftragsNr", ref fextAuftragsNr, value); }
        }
        string fTerminText;
        [Size(255)]
        public string TerminText
        {
            get { return fTerminText; }
            set { SetPropertyValue<string>("TerminText", ref fTerminText, value); }
        }
        bool fDruckeVertrPausch;
        public bool DruckeVertrPausch
        {
            get { return fDruckeVertrPausch; }
            set { SetPropertyValue<bool>("DruckeVertrPausch", ref fDruckeVertrPausch, value); }
        }
        string feCheck;
        [Size(3)]
        public string eCheck
        {
            get { return feCheck; }
            set { SetPropertyValue<string>("eCheck", ref feCheck, value); }
        }
        int fEbene;
        [Indexed(Name = @"Ebene")]
        public int Ebene
        {
            get { return fEbene; }
            set { SetPropertyValue<int>("Ebene", ref fEbene, value); }
        }
        string fInfo1;
        [Size(20)]
        public string Info1
        {
            get { return fInfo1; }
            set { SetPropertyValue<string>("Info1", ref fInfo1, value); }
        }
        string fInfo2;
        [Size(20)]
        public string Info2
        {
            get { return fInfo2; }
            set { SetPropertyValue<string>("Info2", ref fInfo2, value); }
        }
        string fInfo3;
        [Size(20)]
        public string Info3
        {
            get { return fInfo3; }
            set { SetPropertyValue<string>("Info3", ref fInfo3, value); }
        }
        string fMittelLohn;
        [Size(255)]
        public string MittelLohn
        {
            get { return fMittelLohn; }
            set { SetPropertyValue<string>("MittelLohn", ref fMittelLohn, value); }
        }
        string fErfasstUser;
        [Size(8)]
        public string ErfasstUser
        {
            get { return fErfasstUser; }
            set { SetPropertyValue<string>("ErfasstUser", ref fErfasstUser, value); }
        }
        DateTime fErfasstDatum;
        public DateTime ErfasstDatum
        {
            get { return fErfasstDatum; }
            set { SetPropertyValue<DateTime>("ErfasstDatum", ref fErfasstDatum, value); }
        }
        string fGeaendertUser;
        [Size(8)]
        public string GeaendertUser
        {
            get { return fGeaendertUser; }
            set { SetPropertyValue<string>("GeaendertUser", ref fGeaendertUser, value); }
        }
        DateTime fGeaendertDatum;
        public DateTime GeaendertDatum
        {
            get { return fGeaendertDatum; }
            set { SetPropertyValue<DateTime>("GeaendertDatum", ref fGeaendertDatum, value); }
        }
        string fKondition;
        [Size(5)]
        public string Kondition
        {
            get { return fKondition; }
            set { SetPropertyValue<string>("Kondition", ref fKondition, value); }
        }
        string fKategorie;
        [Indexed(Name = @"Kategorie")]
        [Size(255)]
        public string Kategorie
        {
            get { return fKategorie; }
            set { SetPropertyValue<string>("Kategorie", ref fKategorie, value); }
        }
        int ffkMontStrgTermine;
        [Indexed(Name = @"fkMontStrgTermine")]
        public int fkMontStrgTermine
        {
            get { return ffkMontStrgTermine; }
            set { SetPropertyValue<int>("fkMontStrgTermine", ref ffkMontStrgTermine, value); }
        }
        string fHauptMonteur;
        [Size(50)]
        public string HauptMonteur
        {
            get { return fHauptMonteur; }
            set { SetPropertyValue<string>("HauptMonteur", ref fHauptMonteur, value); }
        }
        DateTime fAusfDatum;
        public DateTime AusfDatum
        {
            get { return fAusfDatum; }
            set { SetPropertyValue<DateTime>("AusfDatum", ref fAusfDatum, value); }
        }
        string fZuProjNr;
        [Size(15)]
        public string ZuProjNr
        {
            get { return fZuProjNr; }
            set { SetPropertyValue<string>("ZuProjNr", ref fZuProjNr, value); }
        }
        decimal fPlanStunden;
        public decimal PlanStunden
        {
            get { return fPlanStunden; }
            set { SetPropertyValue<decimal>("PlanStunden", ref fPlanStunden, value); }
        }
    }

}
