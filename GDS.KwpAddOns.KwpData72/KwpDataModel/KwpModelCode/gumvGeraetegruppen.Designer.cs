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

    public partial class gumvGeraetegruppen : XPLiteObject
    {
        int fGeraetegruppeid;
        [Key]
        public int Geraetegruppeid
        {
            get { return fGeraetegruppeid; }
            set { SetPropertyValue<int>("Geraetegruppeid", ref fGeraetegruppeid, value); }
        }
        string fGeraetegruppe;
        [Indexed(Name = @"Geraetegruppe")]
        [Size(15)]
        public string Geraetegruppe
        {
            get { return fGeraetegruppe; }
            set { SetPropertyValue<string>("Geraetegruppe", ref fGeraetegruppe, value); }
        }
    }

}
