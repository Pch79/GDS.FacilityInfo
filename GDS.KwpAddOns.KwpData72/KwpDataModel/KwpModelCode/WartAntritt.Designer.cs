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

    public partial class WartAntritt : XPLiteObject
    {
        short fKennziffer;
        [Key]
        public short Kennziffer
        {
            get { return fKennziffer; }
            set { SetPropertyValue<short>("Kennziffer", ref fKennziffer, value); }
        }
        string fBeschreibung;
        [Size(50)]
        public string Beschreibung
        {
            get { return fBeschreibung; }
            set { SetPropertyValue<string>("Beschreibung", ref fBeschreibung, value); }
        }
        int fFarbe;
        public int Farbe
        {
            get { return fFarbe; }
            set { SetPropertyValue<int>("Farbe", ref fFarbe, value); }
        }
    }

}
