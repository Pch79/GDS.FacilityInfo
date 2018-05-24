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

    public partial class boCheckItem : XPCustomObject
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
        boCheckItemKategorie fKategorie;
        [Association(@"boCheckItemReferencesboCheckItemKategorie")]
        public boCheckItemKategorie Kategorie
        {
            get { return fKategorie; }
            set { SetPropertyValue<boCheckItemKategorie>("Kategorie", ref fKategorie, value); }
        }
        [Association(@"boCheckEntryReferencesboCheckItem")]
        public XPCollection<boCheckEntry> boCheckEntries { get { return GetCollection<boCheckEntry>("boCheckEntries"); } }
        [Association(@"boCheckItemLiegenschaftReferencesboCheckItem")]
        public XPCollection<boCheckItemLiegenschaft> boCheckItemLiegenschafts { get { return GetCollection<boCheckItemLiegenschaft>("boCheckItemLiegenschafts"); } }
        [Association(@"boCheckItemValuelstItemValues_boCheckItemlstCheckItemsReferencesboCheckItem")]
        public XPCollection<boCheckItemValuelstItemValues_boCheckItemlstCheckItems> boCheckItemValuelstItemValues_boCheckItemlstCheckItemss { get { return GetCollection<boCheckItemValuelstItemValues_boCheckItemlstCheckItems>("boCheckItemValuelstItemValues_boCheckItemlstCheckItemss"); } }
        [Association(@"boChecklisteCheckListe_boCheckItemCheckItemsReferencesboCheckItem")]
        public XPCollection<boChecklisteCheckListe_boCheckItemCheckItems> boChecklisteCheckListe_boCheckItemCheckItemss { get { return GetCollection<boChecklisteCheckListe_boCheckItemCheckItems>("boChecklisteCheckListe_boCheckItemCheckItemss"); } }
        [Association(@"boLGCheckEntryReferencesboCheckItem")]
        public XPCollection<boLGCheckEntry> boLGCheckEntries { get { return GetCollection<boLGCheckEntry>("boLGCheckEntries"); } }
    }

}
