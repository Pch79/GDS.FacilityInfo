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

    public partial class LagerBestand : XPLiteObject
    {
        string fBestandsID;
        [Key]
        [Size(30)]
        public string BestandsID
        {
            get { return fBestandsID; }
            set { SetPropertyValue<string>("BestandsID", ref fBestandsID, value); }
        }
        int fLagerID;
        [Indexed(Name = @"IX_LagerID")]
        public int LagerID
        {
            get { return fLagerID; }
            set { SetPropertyValue<int>("LagerID", ref fLagerID, value); }
        }
        string fLagerNr;
        [Indexed(Name = @"IX_LagerNr")]
        [Size(25)]
        public string LagerNr
        {
            get { return fLagerNr; }
            set { SetPropertyValue<string>("LagerNr", ref fLagerNr, value); }
        }
        float fMinBestand;
        public float MinBestand
        {
            get { return fMinBestand; }
            set { SetPropertyValue<float>("MinBestand", ref fMinBestand, value); }
        }
        float fMaxBestand;
        public float MaxBestand
        {
            get { return fMaxBestand; }
            set { SetPropertyValue<float>("MaxBestand", ref fMaxBestand, value); }
        }
    }

}
