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

    public partial class WartTerminNrBlock : XPLiteObject
    {
        string fNrBlockKey;
        [Key]
        [Size(26)]
        public string NrBlockKey
        {
            get { return fNrBlockKey; }
            set { SetPropertyValue<string>("NrBlockKey", ref fNrBlockKey, value); }
        }
        string fTerminKey;
        [Indexed(Name = @"AuftragsNr")]
        [Size(20)]
        public string TerminKey
        {
            get { return fTerminKey; }
            set { SetPropertyValue<string>("TerminKey", ref fTerminKey, value); }
        }
        string fNrKz;
        [Indexed(Name = @"NrKz")]
        [Size(2)]
        public string NrKz
        {
            get { return fNrKz; }
            set { SetPropertyValue<string>("NrKz", ref fNrKz, value); }
        }
        string fNr;
        [Size(30)]
        public string Nr
        {
            get { return fNr; }
            set { SetPropertyValue<string>("Nr", ref fNr, value); }
        }
        double fMenge;
        public double Menge
        {
            get { return fMenge; }
            set { SetPropertyValue<double>("Menge", ref fMenge, value); }
        }
    }

}
