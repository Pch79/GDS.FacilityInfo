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
namespace FacilityInfo.WebService.FacilityInfo_Data
{

    public partial class foStaerkeklasse : XPCustomObject
    {
        Guid fOid;
        [Key(true)]
        public Guid Oid
        {
            get { return fOid; }
            set { SetPropertyValue<Guid>("Oid", ref fOid, value); }
        }
        string fBezeichnung;
        public string Bezeichnung
        {
            get { return fBezeichnung; }
            set { SetPropertyValue<string>("Bezeichnung", ref fBezeichnung, value); }
        }
        decimal fDurchmesserMin;
        public decimal DurchmesserMin
        {
            get { return fDurchmesserMin; }
            set { SetPropertyValue<decimal>("DurchmesserMin", ref fDurchmesserMin, value); }
        }
        decimal fDurchmesserMax;
        public decimal DurchmesserMax
        {
            get { return fDurchmesserMax; }
            set { SetPropertyValue<decimal>("DurchmesserMax", ref fDurchmesserMax, value); }
        }
        int fNummer;
        public int Nummer
        {
            get { return fNummer; }
            set { SetPropertyValue<int>("Nummer", ref fNummer, value); }
        }
    }

}
