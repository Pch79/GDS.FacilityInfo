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

    public partial class wflowArten : XPLiteObject
    {
        int fArtId;
        [Key(true)]
        public int ArtId
        {
            get { return fArtId; }
            set { SetPropertyValue<int>("ArtId", ref fArtId, value); }
        }
        string fArt;
        [Indexed(Name = @"IX_wflowArtenArt")]
        [Size(30)]
        public string Art
        {
            get { return fArt; }
            set { SetPropertyValue<string>("Art", ref fArt, value); }
        }
        int fHintergrundFarbe;
        public int HintergrundFarbe
        {
            get { return fHintergrundFarbe; }
            set { SetPropertyValue<int>("HintergrundFarbe", ref fHintergrundFarbe, value); }
        }
        int fTextFarbe;
        public int TextFarbe
        {
            get { return fTextFarbe; }
            set { SetPropertyValue<int>("TextFarbe", ref fTextFarbe, value); }
        }
        string fBetreff;
        [Size(255)]
        public string Betreff
        {
            get { return fBetreff; }
            set { SetPropertyValue<string>("Betreff", ref fBetreff, value); }
        }
        int fTerminAktivitaet;
        [Indexed(Name = @"IX_wflowArtenTerminAktivitaet")]
        public int TerminAktivitaet
        {
            get { return fTerminAktivitaet; }
            set { SetPropertyValue<int>("TerminAktivitaet", ref fTerminAktivitaet, value); }
        }
        string fBenutzerfeld1Bezeichnung;
        [Size(30)]
        public string Benutzerfeld1Bezeichnung
        {
            get { return fBenutzerfeld1Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld1Bezeichnung", ref fBenutzerfeld1Bezeichnung, value); }
        }
        string fBenutzerfeld2Bezeichnung;
        [Size(30)]
        public string Benutzerfeld2Bezeichnung
        {
            get { return fBenutzerfeld2Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld2Bezeichnung", ref fBenutzerfeld2Bezeichnung, value); }
        }
        string fBenutzerfeld3Bezeichnung;
        [Size(30)]
        public string Benutzerfeld3Bezeichnung
        {
            get { return fBenutzerfeld3Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld3Bezeichnung", ref fBenutzerfeld3Bezeichnung, value); }
        }
        string fBenutzerfeld4Bezeichnung;
        [Size(30)]
        public string Benutzerfeld4Bezeichnung
        {
            get { return fBenutzerfeld4Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld4Bezeichnung", ref fBenutzerfeld4Bezeichnung, value); }
        }
        string fBenutzerfeld5Bezeichnung;
        [Size(30)]
        public string Benutzerfeld5Bezeichnung
        {
            get { return fBenutzerfeld5Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld5Bezeichnung", ref fBenutzerfeld5Bezeichnung, value); }
        }
        string fBenutzerfeld6Bezeichnung;
        [Size(30)]
        public string Benutzerfeld6Bezeichnung
        {
            get { return fBenutzerfeld6Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld6Bezeichnung", ref fBenutzerfeld6Bezeichnung, value); }
        }
        string fBenutzerfeld7Bezeichnung;
        [Size(30)]
        public string Benutzerfeld7Bezeichnung
        {
            get { return fBenutzerfeld7Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld7Bezeichnung", ref fBenutzerfeld7Bezeichnung, value); }
        }
        string fBenutzerfeld8Bezeichnung;
        [Size(30)]
        public string Benutzerfeld8Bezeichnung
        {
            get { return fBenutzerfeld8Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld8Bezeichnung", ref fBenutzerfeld8Bezeichnung, value); }
        }
        string fBenutzerfeld9Bezeichnung;
        [Size(30)]
        public string Benutzerfeld9Bezeichnung
        {
            get { return fBenutzerfeld9Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld9Bezeichnung", ref fBenutzerfeld9Bezeichnung, value); }
        }
        string fBenutzerfeld10Bezeichnung;
        [Size(30)]
        public string Benutzerfeld10Bezeichnung
        {
            get { return fBenutzerfeld10Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld10Bezeichnung", ref fBenutzerfeld10Bezeichnung, value); }
        }
        string fBenutzerfeld11Bezeichnung;
        [Size(50)]
        public string Benutzerfeld11Bezeichnung
        {
            get { return fBenutzerfeld11Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld11Bezeichnung", ref fBenutzerfeld11Bezeichnung, value); }
        }
        string fBenutzerfeld12Bezeichnung;
        [Size(50)]
        public string Benutzerfeld12Bezeichnung
        {
            get { return fBenutzerfeld12Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld12Bezeichnung", ref fBenutzerfeld12Bezeichnung, value); }
        }
        string fBenutzerfeld13Bezeichnung;
        [Size(50)]
        public string Benutzerfeld13Bezeichnung
        {
            get { return fBenutzerfeld13Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld13Bezeichnung", ref fBenutzerfeld13Bezeichnung, value); }
        }
        string fBenutzerfeld14Bezeichnung;
        [Size(50)]
        public string Benutzerfeld14Bezeichnung
        {
            get { return fBenutzerfeld14Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld14Bezeichnung", ref fBenutzerfeld14Bezeichnung, value); }
        }
        string fBenutzerfeld15Bezeichnung;
        [Size(50)]
        public string Benutzerfeld15Bezeichnung
        {
            get { return fBenutzerfeld15Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld15Bezeichnung", ref fBenutzerfeld15Bezeichnung, value); }
        }
        string fBenutzerfeld16Bezeichnung;
        [Size(50)]
        public string Benutzerfeld16Bezeichnung
        {
            get { return fBenutzerfeld16Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld16Bezeichnung", ref fBenutzerfeld16Bezeichnung, value); }
        }
        string fBenutzerfeld17Bezeichnung;
        [Size(50)]
        public string Benutzerfeld17Bezeichnung
        {
            get { return fBenutzerfeld17Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld17Bezeichnung", ref fBenutzerfeld17Bezeichnung, value); }
        }
        string fBenutzerfeld18Bezeichnung;
        [Size(50)]
        public string Benutzerfeld18Bezeichnung
        {
            get { return fBenutzerfeld18Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld18Bezeichnung", ref fBenutzerfeld18Bezeichnung, value); }
        }
        string fBenutzerfeld19Bezeichnung;
        [Size(50)]
        public string Benutzerfeld19Bezeichnung
        {
            get { return fBenutzerfeld19Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld19Bezeichnung", ref fBenutzerfeld19Bezeichnung, value); }
        }
        string fBenutzerfeld20Bezeichnung;
        [Size(50)]
        public string Benutzerfeld20Bezeichnung
        {
            get { return fBenutzerfeld20Bezeichnung; }
            set { SetPropertyValue<string>("Benutzerfeld20Bezeichnung", ref fBenutzerfeld20Bezeichnung, value); }
        }
        string fFormularPfad;
        [Size(255)]
        public string FormularPfad
        {
            get { return fFormularPfad; }
            set { SetPropertyValue<string>("FormularPfad", ref fFormularPfad, value); }
        }
        bool fCheckListe;
        public bool CheckListe
        {
            get { return fCheckListe; }
            set { SetPropertyValue<bool>("CheckListe", ref fCheckListe, value); }
        }
        short fAutoArchivierung;
        public short AutoArchivierung
        {
            get { return fAutoArchivierung; }
            set { SetPropertyValue<short>("AutoArchivierung", ref fAutoArchivierung, value); }
        }
        long fArchivOrdnerID;
        public long ArchivOrdnerID
        {
            get { return fArchivOrdnerID; }
            set { SetPropertyValue<long>("ArchivOrdnerID", ref fArchivOrdnerID, value); }
        }
        short fBenutzerFeld1Format;
        public short BenutzerFeld1Format
        {
            get { return fBenutzerFeld1Format; }
            set { SetPropertyValue<short>("BenutzerFeld1Format", ref fBenutzerFeld1Format, value); }
        }
        short fBenutzerFeld2Format;
        public short BenutzerFeld2Format
        {
            get { return fBenutzerFeld2Format; }
            set { SetPropertyValue<short>("BenutzerFeld2Format", ref fBenutzerFeld2Format, value); }
        }
        short fBenutzerFeld3Format;
        public short BenutzerFeld3Format
        {
            get { return fBenutzerFeld3Format; }
            set { SetPropertyValue<short>("BenutzerFeld3Format", ref fBenutzerFeld3Format, value); }
        }
        short fBenutzerFeld4Format;
        public short BenutzerFeld4Format
        {
            get { return fBenutzerFeld4Format; }
            set { SetPropertyValue<short>("BenutzerFeld4Format", ref fBenutzerFeld4Format, value); }
        }
        short fBenutzerFeld5Format;
        public short BenutzerFeld5Format
        {
            get { return fBenutzerFeld5Format; }
            set { SetPropertyValue<short>("BenutzerFeld5Format", ref fBenutzerFeld5Format, value); }
        }
        short fBenutzerFeld6Format;
        public short BenutzerFeld6Format
        {
            get { return fBenutzerFeld6Format; }
            set { SetPropertyValue<short>("BenutzerFeld6Format", ref fBenutzerFeld6Format, value); }
        }
        short fBenutzerFeld7Format;
        public short BenutzerFeld7Format
        {
            get { return fBenutzerFeld7Format; }
            set { SetPropertyValue<short>("BenutzerFeld7Format", ref fBenutzerFeld7Format, value); }
        }
        short fBenutzerFeld8Format;
        public short BenutzerFeld8Format
        {
            get { return fBenutzerFeld8Format; }
            set { SetPropertyValue<short>("BenutzerFeld8Format", ref fBenutzerFeld8Format, value); }
        }
        short fBenutzerFeld9Format;
        public short BenutzerFeld9Format
        {
            get { return fBenutzerFeld9Format; }
            set { SetPropertyValue<short>("BenutzerFeld9Format", ref fBenutzerFeld9Format, value); }
        }
        short fBenutzerFeld10Format;
        public short BenutzerFeld10Format
        {
            get { return fBenutzerFeld10Format; }
            set { SetPropertyValue<short>("BenutzerFeld10Format", ref fBenutzerFeld10Format, value); }
        }
        [Association(@"wflowAktivitaetenReferenceswflowArten")]
        public XPCollection<wflowAktivitaeten> wflowAktivitaetens { get { return GetCollection<wflowAktivitaeten>("wflowAktivitaetens"); } }
        [Association(@"wflowArtGruppenReferenceswflowArten")]
        public XPCollection<wflowArtGruppen> wflowArtGruppens { get { return GetCollection<wflowArtGruppen>("wflowArtGruppens"); } }
        [Association(@"wflowArtUsersReferenceswflowArten")]
        public XPCollection<wflowArtUsers> wflowArtUsersCollection { get { return GetCollection<wflowArtUsers>("wflowArtUsersCollection"); } }
        [Association(@"wflowAutoAktivitaetenReferenceswflowArten")]
        public XPCollection<wflowAutoAktivitaeten> wflowAutoAktivitaetens { get { return GetCollection<wflowAutoAktivitaeten>("wflowAutoAktivitaetens"); } }
        [Association(@"wflowAutoAktivitaetenReferenceswflowArten1")]
        public XPCollection<wflowAutoAktivitaeten> wflowAutoAktivitaetens1 { get { return GetCollection<wflowAutoAktivitaeten>("wflowAutoAktivitaetens1"); } }
    }

}
