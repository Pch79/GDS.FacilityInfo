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

    public partial class UBRAHMEN : XPLiteObject
    {
        short fRahmenNR;
        [Key]
        public short RahmenNR
        {
            get { return fRahmenNR; }
            set { SetPropertyValue<short>("RahmenNR", ref fRahmenNR, value); }
        }
        string fRahmenBez;
        [Size(50)]
        public string RahmenBez
        {
            get { return fRahmenBez; }
            set { SetPropertyValue<string>("RahmenBez", ref fRahmenBez, value); }
        }
        string fRahmenVMK;
        [Size(255)]
        public string RahmenVMK
        {
            get { return fRahmenVMK; }
            set { SetPropertyValue<string>("RahmenVMK", ref fRahmenVMK, value); }
        }
        string fRahmenVorg;
        [Size(SizeAttribute.Unlimited)]
        public string RahmenVorg
        {
            get { return fRahmenVorg; }
            set { SetPropertyValue<string>("RahmenVorg", ref fRahmenVorg, value); }
        }
        string fRahmenFeld;
        [Size(SizeAttribute.Unlimited)]
        public string RahmenFeld
        {
            get { return fRahmenFeld; }
            set { SetPropertyValue<string>("RahmenFeld", ref fRahmenFeld, value); }
        }
        string fRahmenVFeld;
        [Size(SizeAttribute.Unlimited)]
        public string RahmenVFeld
        {
            get { return fRahmenVFeld; }
            set { SetPropertyValue<string>("RahmenVFeld", ref fRahmenVFeld, value); }
        }
        string fRahmenVInh;
        [Size(50)]
        public string RahmenVInh
        {
            get { return fRahmenVInh; }
            set { SetPropertyValue<string>("RahmenVInh", ref fRahmenVInh, value); }
        }
        string fRahmenTrennz;
        [Size(50)]
        public string RahmenTrennz
        {
            get { return fRahmenTrennz; }
            set { SetPropertyValue<string>("RahmenTrennz", ref fRahmenTrennz, value); }
        }
    }

}
