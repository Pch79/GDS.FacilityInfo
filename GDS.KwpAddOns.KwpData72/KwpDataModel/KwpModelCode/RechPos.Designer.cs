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

    public partial class RechPos : XPLiteObject
    {
        string fVorgangsKey;
        [Key]
        [Size(83)]
        public string VorgangsKey
        {
            get { return fVorgangsKey; }
            set { SetPropertyValue<string>("VorgangsKey", ref fVorgangsKey, value); }
        }
        Rechnung fVorgangsNr;
        [Size(18)]
        [Association(@"RechPosReferencesRechnung")]
        public Rechnung VorgangsNr
        {
            get { return fVorgangsNr; }
            set { SetPropertyValue<Rechnung>("VorgangsNr", ref fVorgangsNr, value); }
        }
        string fPosKey;
        [Size(65)]
        public string PosKey
        {
            get { return fPosKey; }
            set { SetPropertyValue<string>("PosKey", ref fPosKey, value); }
        }
        string fArtNr;
        [Indexed(Name = @"ArtNr")]
        [Size(19)]
        public string ArtNr
        {
            get { return fArtNr; }
            set { SetPropertyValue<string>("ArtNr", ref fArtNr, value); }
        }
        float fMengen;
        public float Mengen
        {
            get { return fMengen; }
            set { SetPropertyValue<float>("Mengen", ref fMengen, value); }
        }
        short fKoArt;
        [Indexed(Name = @"KoArt")]
        public short KoArt
        {
            get { return fKoArt; }
            set { SetPropertyValue<short>("KoArt", ref fKoArt, value); }
        }
        decimal fVkMat;
        public decimal VkMat
        {
            get { return fVkMat; }
            set { SetPropertyValue<decimal>("VkMat", ref fVkMat, value); }
        }
        decimal fVkLohn;
        public decimal VkLohn
        {
            get { return fVkLohn; }
            set { SetPropertyValue<decimal>("VkLohn", ref fVkLohn, value); }
        }
        decimal fEkMat;
        public decimal EkMat
        {
            get { return fEkMat; }
            set { SetPropertyValue<decimal>("EkMat", ref fEkMat, value); }
        }
        float fStunden;
        public float Stunden
        {
            get { return fStunden; }
            set { SetPropertyValue<float>("Stunden", ref fStunden, value); }
        }
        bool fKonv2Euro;
        public bool Konv2Euro
        {
            get { return fKonv2Euro; }
            set { SetPropertyValue<bool>("Konv2Euro", ref fKonv2Euro, value); }
        }
        string fPosText;
        public string PosText
        {
            get { return fPosText; }
            set { SetPropertyValue<string>("PosText", ref fPosText, value); }
        }
        string fEinheit;
        [Size(8)]
        public string Einheit
        {
            get { return fEinheit; }
            set { SetPropertyValue<string>("Einheit", ref fEinheit, value); }
        }
    }

}
