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

    public partial class rebEinReStatus : XPLiteObject
    {
        int fpkEinReStatus;
        [Key]
        public int pkEinReStatus
        {
            get { return fpkEinReStatus; }
            set { SetPropertyValue<int>("pkEinReStatus", ref fpkEinReStatus, value); }
        }
        string fName;
        [Size(50)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        [Association(@"rebEinRechnungReferencesrebEinReStatus")]
        public XPCollection<rebEinRechnung> rebEinRechnungs { get { return GetCollection<rebEinRechnung>("rebEinRechnungs"); } }
    }

}
