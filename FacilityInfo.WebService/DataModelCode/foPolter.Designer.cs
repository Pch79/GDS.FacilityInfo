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

    public partial class foPolter : XPCustomObject
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
        string fNotiz;
        [Size(SizeAttribute.Unlimited)]
        public string Notiz
        {
            get { return fNotiz; }
            set { SetPropertyValue<string>("Notiz", ref fNotiz, value); }
        }
        DateTime fErstellungsdatum;
        public DateTime Erstellungsdatum
        {
            get { return fErstellungsdatum; }
            set { SetPropertyValue<DateTime>("Erstellungsdatum", ref fErstellungsdatum, value); }
        }
        [Association(@"foStammReferencesfoPolter")]
        public XPCollection<foStamm> foStamms { get { return GetCollection<foStamm>("foStamms"); } }
    }

}
