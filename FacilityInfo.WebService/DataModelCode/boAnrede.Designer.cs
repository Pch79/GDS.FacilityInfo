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

    public partial class boAnrede : XPCustomObject
    {
        Guid fOid;
        [Key(true)]
        public Guid Oid
        {
            get { return fOid; }
            set { SetPropertyValue<Guid>("Oid", ref fOid, value); }
        }
        string fText;
        public string Text
        {
            get { return fText; }
            set { SetPropertyValue<string>("Text", ref fText, value); }
        }
        int fGeschlecht;
        public int Geschlecht
        {
            get { return fGeschlecht; }
            set { SetPropertyValue<int>("Geschlecht", ref fGeschlecht, value); }
        }
        string fBreifanrede;
        public string Breifanrede
        {
            get { return fBreifanrede; }
            set { SetPropertyValue<string>("Breifanrede", ref fBreifanrede, value); }
        }
        string fFremdsystemId;
        public string FremdsystemId
        {
            get { return fFremdsystemId; }
            set { SetPropertyValue<string>("FremdsystemId", ref fFremdsystemId, value); }
        }
        [Association(@"boAdresseReferencesboAnrede")]
        public XPCollection<boAdresse> boAdresses { get { return GetCollection<boAdresse>("boAdresses"); } }
        [Association(@"boKontaktReferencesboAnrede")]
        public XPCollection<boKontakt> boKontakts { get { return GetCollection<boKontakt>("boKontakts"); } }
    }

}
