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

    public partial class WartSelektVertrag : XPLiteObject
    {
        string fSelekt;
        [Key]
        [Size(20)]
        public string Selekt
        {
            get { return fSelekt; }
            set { SetPropertyValue<string>("Selekt", ref fSelekt, value); }
        }
    }

}
