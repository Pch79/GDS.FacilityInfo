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

    public partial class MontJournPosMat : XPLiteObject
    {
        MontJournPos fPosKey;
        [Association(@"MontJournPosMatReferencesMontJournPos")]
        public MontJournPos PosKey
        {
            get { return fPosKey; }
            set { SetPropertyValue<MontJournPos>("PosKey", ref fPosKey, value); }
        }
        string fProjNr;
        [Size(15)]
        public string ProjNr
        {
            get { return fProjNr; }
            set { SetPropertyValue<string>("ProjNr", ref fProjNr, value); }
        }
        string fArtNr;
        [Indexed(Name = @"IX_ArtNr")]
        [Size(25)]
        public string ArtNr
        {
            get { return fArtNr; }
            set { SetPropertyValue<string>("ArtNr", ref fArtNr, value); }
        }
        string fPosArt;
        [Size(1)]
        public string PosArt
        {
            get { return fPosArt; }
            set { SetPropertyValue<string>("PosArt", ref fPosArt, value); }
        }
        double fMenge;
        public double Menge
        {
            get { return fMenge; }
            set { SetPropertyValue<double>("Menge", ref fMenge, value); }
        }
        double fMengeNetto;
        public double MengeNetto
        {
            get { return fMengeNetto; }
            set { SetPropertyValue<double>("MengeNetto", ref fMengeNetto, value); }
        }
        decimal fNettoMat;
        public decimal NettoMat
        {
            get { return fNettoMat; }
            set { SetPropertyValue<decimal>("NettoMat", ref fNettoMat, value); }
        }
        decimal fNettoRohst;
        public decimal NettoRohst
        {
            get { return fNettoRohst; }
            set { SetPropertyValue<decimal>("NettoRohst", ref fNettoRohst, value); }
        }
        string fSatzSZ;
        [Size(1)]
        public string SatzSZ
        {
            get { return fSatzSZ; }
            set { SetPropertyValue<string>("SatzSZ", ref fSatzSZ, value); }
        }
        short fKoArt;
        public short KoArt
        {
            get { return fKoArt; }
            set { SetPropertyValue<short>("KoArt", ref fKoArt, value); }
        }
        DateTime fCheckDate;
        public DateTime CheckDate
        {
            get { return fCheckDate; }
            set { SetPropertyValue<DateTime>("CheckDate", ref fCheckDate, value); }
        }
        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>("ID", ref fID, value); }
        }
        string fEinheit;
        [Size(4)]
        public string Einheit
        {
            get { return fEinheit; }
            set { SetPropertyValue<string>("Einheit", ref fEinheit, value); }
        }
    }

}
