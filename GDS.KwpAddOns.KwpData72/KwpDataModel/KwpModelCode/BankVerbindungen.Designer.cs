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

    public partial class BankVerbindungen : XPLiteObject
    {
        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>("ID", ref fID, value); }
        }
        string fKtoNr;
        [Size(20)]
        public string KtoNr
        {
            get { return fKtoNr; }
            set { SetPropertyValue<string>("KtoNr", ref fKtoNr, value); }
        }
        string fKtoBezeichnung;
        [Size(50)]
        public string KtoBezeichnung
        {
            get { return fKtoBezeichnung; }
            set { SetPropertyValue<string>("KtoBezeichnung", ref fKtoBezeichnung, value); }
        }
        string fBLZ;
        [Size(8)]
        public string BLZ
        {
            get { return fBLZ; }
            set { SetPropertyValue<string>("BLZ", ref fBLZ, value); }
        }
        string fBankName;
        [Size(50)]
        public string BankName
        {
            get { return fBankName; }
            set { SetPropertyValue<string>("BankName", ref fBankName, value); }
        }
        string fBankOrt;
        [Size(50)]
        public string BankOrt
        {
            get { return fBankOrt; }
            set { SetPropertyValue<string>("BankOrt", ref fBankOrt, value); }
        }
        string fIBAN;
        [Size(34)]
        public string IBAN
        {
            get { return fIBAN; }
            set { SetPropertyValue<string>("IBAN", ref fIBAN, value); }
        }
        string fBIC;
        [Size(11)]
        public string BIC
        {
            get { return fBIC; }
            set { SetPropertyValue<string>("BIC", ref fBIC, value); }
        }
        string fErfasstDurch;
        [Size(8)]
        public string ErfasstDurch
        {
            get { return fErfasstDurch; }
            set { SetPropertyValue<string>("ErfasstDurch", ref fErfasstDurch, value); }
        }
        DateTime fErfasstAm;
        public DateTime ErfasstAm
        {
            get { return fErfasstAm; }
            set { SetPropertyValue<DateTime>("ErfasstAm", ref fErfasstAm, value); }
        }
        string fGeaendertDurch;
        [Size(8)]
        public string GeaendertDurch
        {
            get { return fGeaendertDurch; }
            set { SetPropertyValue<string>("GeaendertDurch", ref fGeaendertDurch, value); }
        }
        DateTime fGeaendertAm;
        public DateTime GeaendertAm
        {
            get { return fGeaendertAm; }
            set { SetPropertyValue<DateTime>("GeaendertAm", ref fGeaendertAm, value); }
        }
        short fDefaultAbbuchKto;
        public short DefaultAbbuchKto
        {
            get { return fDefaultAbbuchKto; }
            set { SetPropertyValue<short>("DefaultAbbuchKto", ref fDefaultAbbuchKto, value); }
        }
    }

}
