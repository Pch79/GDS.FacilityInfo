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

    public partial class boMassnahmenArt : XPCustomObject
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
        string fBeschreibung;
        [Size(SizeAttribute.Unlimited)]
        public string Beschreibung
        {
            get { return fBeschreibung; }
            set { SetPropertyValue<string>("Beschreibung", ref fBeschreibung, value); }
        }
        MediaDataObject fIcon;
        [Association(@"boMassnahmenArtReferencesMediaDataObject")]
        public MediaDataObject Icon
        {
            get { return fIcon; }
            set { SetPropertyValue<MediaDataObject>("Icon", ref fIcon, value); }
        }
        [Association(@"boMassnahmeReferencesboMassnahmenArt")]
        public XPCollection<boMassnahme> boMassnahmes { get { return GetCollection<boMassnahme>("boMassnahmes"); } }
        [Association(@"boMassnahmenArtlstMassnahmenArt_boMADatenItemlstDatenFelderReferencesboMassnahmenArt")]
        public XPCollection<boMassnahmenArtlstMassnahmenArt_boMADatenItemlstDatenFelder> boMassnahmenArtlstMassnahmenArt_boMADatenItemlstDatenFelders { get { return GetCollection<boMassnahmenArtlstMassnahmenArt_boMADatenItemlstDatenFelder>("boMassnahmenArtlstMassnahmenArt_boMADatenItemlstDatenFelders"); } }
    }

}
