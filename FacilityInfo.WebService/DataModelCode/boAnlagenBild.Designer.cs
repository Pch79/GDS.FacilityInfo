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

    public partial class boAnlagenBild : XPCustomObject
    {
        Guid fOid;
        [Key(true)]
        public Guid Oid
        {
            get { return fOid; }
            set { SetPropertyValue<Guid>("Oid", ref fOid, value); }
        }
        byte[] fOriginalbild;
        [Size(SizeAttribute.Unlimited)]
        [MemberDesignTimeVisibility(true)]
        public byte[] Originalbild
        {
            get { return fOriginalbild; }
            set { SetPropertyValue<byte[]>("Originalbild", ref fOriginalbild, value); }
        }
        byte[] fBild;
        [Size(SizeAttribute.Unlimited)]
        [MemberDesignTimeVisibility(true)]
        public byte[] Bild
        {
            get { return fBild; }
            set { SetPropertyValue<byte[]>("Bild", ref fBild, value); }
        }
        int fSortPosition;
        public int SortPosition
        {
            get { return fSortPosition; }
            set { SetPropertyValue<int>("SortPosition", ref fSortPosition, value); }
        }
        boAnlage fAnlage;
        [Association(@"boAnlagenBildReferencesboAnlage")]
        public boAnlage Anlage
        {
            get { return fAnlage; }
            set { SetPropertyValue<boAnlage>("Anlage", ref fAnlage, value); }
        }
        string fBildtitel;
        public string Bildtitel
        {
            get { return fBildtitel; }
            set { SetPropertyValue<string>("Bildtitel", ref fBildtitel, value); }
        }
        string fBeschreibung;
        public string Beschreibung
        {
            get { return fBeschreibung; }
            set { SetPropertyValue<string>("Beschreibung", ref fBeschreibung, value); }
        }
        fiBildtitel fTitel;
        [Association(@"boAnlagenBildReferencesfiBildtitel")]
        public fiBildtitel Titel
        {
            get { return fTitel; }
            set { SetPropertyValue<fiBildtitel>("Titel", ref fTitel, value); }
        }
        BildKategorie fBildKategorie;
        [Association(@"boAnlagenBildReferencesBildKategorie")]
        public BildKategorie BildKategorie
        {
            get { return fBildKategorie; }
            set { SetPropertyValue<BildKategorie>("BildKategorie", ref fBildKategorie, value); }
        }
    }

}
