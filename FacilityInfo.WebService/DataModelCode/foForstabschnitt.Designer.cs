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

    public partial class foForstabschnitt : XPCustomObject
    {
        Guid fOid;
        [Key(true)]
        public Guid Oid
        {
            get { return fOid; }
            set { SetPropertyValue<Guid>("Oid", ref fOid, value); }
        }
        boKunde fBesitzer;
        [Association(@"foForstabschnittReferencesboKunde")]
        public boKunde Besitzer
        {
            get { return fBesitzer; }
            set { SetPropertyValue<boKunde>("Besitzer", ref fBesitzer, value); }
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
        decimal fFlaeche;
        public decimal Flaeche
        {
            get { return fFlaeche; }
            set { SetPropertyValue<decimal>("Flaeche", ref fFlaeche, value); }
        }
        [Association(@"foHiebReferencesfoForstabschnitt")]
        public XPCollection<foHieb> foHiebs { get { return GetCollection<foHieb>("foHiebs"); } }
    }

}
