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

    public partial class AngebotFw : XPLiteObject
    {
        string fANGEBOTSNR;
        [Key]
        [Size(18)]
        public string ANGEBOTSNR
        {
            get { return fANGEBOTSNR; }
            set { SetPropertyValue<string>("ANGEBOTSNR", ref fANGEBOTSNR, value); }
        }
        string fIsoCodeFw;
        [Size(3)]
        public string IsoCodeFw
        {
            get { return fIsoCodeFw; }
            set { SetPropertyValue<string>("IsoCodeFw", ref fIsoCodeFw, value); }
        }
        decimal fMATERIALWERT;
        public decimal MATERIALWERT
        {
            get { return fMATERIALWERT; }
            set { SetPropertyValue<decimal>("MATERIALWERT", ref fMATERIALWERT, value); }
        }
        decimal fLOHNWERT;
        public decimal LOHNWERT
        {
            get { return fLOHNWERT; }
            set { SetPropertyValue<decimal>("LOHNWERT", ref fLOHNWERT, value); }
        }
        decimal fLOHNSTD;
        public decimal LOHNSTD
        {
            get { return fLOHNSTD; }
            set { SetPropertyValue<decimal>("LOHNSTD", ref fLOHNSTD, value); }
        }
        decimal fZUABSCHLAGWERT;
        public decimal ZUABSCHLAGWERT
        {
            get { return fZUABSCHLAGWERT; }
            set { SetPropertyValue<decimal>("ZUABSCHLAGWERT", ref fZUABSCHLAGWERT, value); }
        }
        decimal fUSTBASIS;
        public decimal USTBASIS
        {
            get { return fUSTBASIS; }
            set { SetPropertyValue<decimal>("USTBASIS", ref fUSTBASIS, value); }
        }
        decimal fUSTWERT;
        public decimal USTWERT
        {
            get { return fUSTWERT; }
            set { SetPropertyValue<decimal>("USTWERT", ref fUSTWERT, value); }
        }
        decimal fBRUTTOWERT;
        public decimal BRUTTOWERT
        {
            get { return fBRUTTOWERT; }
            set { SetPropertyValue<decimal>("BRUTTOWERT", ref fBRUTTOWERT, value); }
        }
        bool fKonv2Euro;
        public bool Konv2Euro
        {
            get { return fKonv2Euro; }
            set { SetPropertyValue<bool>("Konv2Euro", ref fKonv2Euro, value); }
        }
    }

}
