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

    public partial class boGeraeteart : XPCustomObject
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
        string fKuerzel;
        public string Kuerzel
        {
            get { return fKuerzel; }
            set { SetPropertyValue<string>("Kuerzel", ref fKuerzel, value); }
        }
        MediaDataObject fIcon;
        [Association(@"boGeraeteartReferencesMediaDataObject")]
        public MediaDataObject Icon
        {
            get { return fIcon; }
            set { SetPropertyValue<MediaDataObject>("Icon", ref fIcon, value); }
        }
        string fBeschreibung;
        [Size(SizeAttribute.Unlimited)]
        public string Beschreibung
        {
            get { return fBeschreibung; }
            set { SetPropertyValue<string>("Beschreibung", ref fBeschreibung, value); }
        }
        [Association(@"boGeraetReferencesboGeraeteart")]
        public XPCollection<boGeraet> boGeraets { get { return GetCollection<boGeraet>("boGeraets"); } }
        [Association(@"boGeraetReferencesboGeraeteart1")]
        public XPCollection<boGeraet> boGeraets1 { get { return GetCollection<boGeraet>("boGeraets1"); } }
    }

}
