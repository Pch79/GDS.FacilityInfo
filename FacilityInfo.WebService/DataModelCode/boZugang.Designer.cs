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

    public partial class boZugang : XPCustomObject
    {
        Guid fOid;
        [Key(true)]
        public Guid Oid
        {
            get { return fOid; }
            set { SetPropertyValue<Guid>("Oid", ref fOid, value); }
        }
        boZugangsTyp fTyp;
        [Association(@"boZugangReferencesboZugangsTyp")]
        public boZugangsTyp Typ
        {
            get { return fTyp; }
            set { SetPropertyValue<boZugangsTyp>("Typ", ref fTyp, value); }
        }
        string fBemerkung;
        public string Bemerkung
        {
            get { return fBemerkung; }
            set { SetPropertyValue<string>("Bemerkung", ref fBemerkung, value); }
        }
        string fWert;
        public string Wert
        {
            get { return fWert; }
            set { SetPropertyValue<string>("Wert", ref fWert, value); }
        }
    }

}
