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

    public partial class boServiceItemEntry : XPCustomObject
    {
        Guid fOid;
        [Key(true)]
        public Guid Oid
        {
            get { return fOid; }
            set { SetPropertyValue<Guid>("Oid", ref fOid, value); }
        }
        boService fService;
        [Association(@"boServiceItemEntryReferencesboService")]
        public boService Service
        {
            get { return fService; }
            set { SetPropertyValue<boService>("Service", ref fService, value); }
        }
        boServiceItem fServiceitem;
        [Association(@"boServiceItemEntryReferencesboServiceItem")]
        public boServiceItem Serviceitem
        {
            get { return fServiceitem; }
            set { SetPropertyValue<boServiceItem>("Serviceitem", ref fServiceitem, value); }
        }
        string fBemerkung;
        [Size(500)]
        public string Bemerkung
        {
            get { return fBemerkung; }
            set { SetPropertyValue<string>("Bemerkung", ref fBemerkung, value); }
        }
        string fErgebniseintrag;
        public string Ergebniseintrag
        {
            get { return fErgebniseintrag; }
            set { SetPropertyValue<string>("Ergebniseintrag", ref fErgebniseintrag, value); }
        }
        boServiceItemResult fServiceItemResult;
        [Association(@"boServiceItemEntryReferencesboServiceItemResult")]
        public boServiceItemResult ServiceItemResult
        {
            get { return fServiceItemResult; }
            set { SetPropertyValue<boServiceItemResult>("ServiceItemResult", ref fServiceItemResult, value); }
        }
    }

}
