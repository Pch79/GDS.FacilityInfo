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

    public partial class ArchivHeader : XPLiteObject
    {
        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>("ID", ref fID, value); }
        }
        string fBetreff;
        [Size(255)]
        public string Betreff
        {
            get { return fBetreff; }
            set { SetPropertyValue<string>("Betreff", ref fBetreff, value); }
        }
        string fBemerkung;
        [Size(255)]
        public string Bemerkung
        {
            get { return fBemerkung; }
            set { SetPropertyValue<string>("Bemerkung", ref fBemerkung, value); }
        }
        string fDokumentTyp;
        [Indexed(Name = @"IX_DokumentTyp")]
        [Size(255)]
        public string DokumentTyp
        {
            get { return fDokumentTyp; }
            set { SetPropertyValue<string>("DokumentTyp", ref fDokumentTyp, value); }
        }
        int fStrukturFix;
        [Indexed(Name = @"IX_StrukturFix")]
        public int StrukturFix
        {
            get { return fStrukturFix; }
            set { SetPropertyValue<int>("StrukturFix", ref fStrukturFix, value); }
        }
        int fHVorgangsArt;
        public int HVorgangsArt
        {
            get { return fHVorgangsArt; }
            set { SetPropertyValue<int>("HVorgangsArt", ref fHVorgangsArt, value); }
        }
        string fHVorgangsNr;
        [Size(80)]
        public string HVorgangsNr
        {
            get { return fHVorgangsNr; }
            set { SetPropertyValue<string>("HVorgangsNr", ref fHVorgangsNr, value); }
        }
        string fHAdrNrGes1;
        [Size(24)]
        public string HAdrNrGes1
        {
            get { return fHAdrNrGes1; }
            set { SetPropertyValue<string>("HAdrNrGes1", ref fHAdrNrGes1, value); }
        }
        string fHAdrNrGes2;
        [Size(24)]
        public string HAdrNrGes2
        {
            get { return fHAdrNrGes2; }
            set { SetPropertyValue<string>("HAdrNrGes2", ref fHAdrNrGes2, value); }
        }
        string fHAdrNrGes3;
        [Size(24)]
        public string HAdrNrGes3
        {
            get { return fHAdrNrGes3; }
            set { SetPropertyValue<string>("HAdrNrGes3", ref fHAdrNrGes3, value); }
        }
        int fHBezugsArt1;
        public int HBezugsArt1
        {
            get { return fHBezugsArt1; }
            set { SetPropertyValue<int>("HBezugsArt1", ref fHBezugsArt1, value); }
        }
        string fHBezugsNr1;
        public string HBezugsNr1
        {
            get { return fHBezugsNr1; }
            set { SetPropertyValue<string>("HBezugsNr1", ref fHBezugsNr1, value); }
        }
        int fHBezugsArt2;
        public int HBezugsArt2
        {
            get { return fHBezugsArt2; }
            set { SetPropertyValue<int>("HBezugsArt2", ref fHBezugsArt2, value); }
        }
        string fHBezugsNr2;
        public string HBezugsNr2
        {
            get { return fHBezugsNr2; }
            set { SetPropertyValue<string>("HBezugsNr2", ref fHBezugsNr2, value); }
        }
        string fKategorie;
        [Size(255)]
        public string Kategorie
        {
            get { return fKategorie; }
            set { SetPropertyValue<string>("Kategorie", ref fKategorie, value); }
        }
        string fBenutzerfeld1;
        [Size(255)]
        public string Benutzerfeld1
        {
            get { return fBenutzerfeld1; }
            set { SetPropertyValue<string>("Benutzerfeld1", ref fBenutzerfeld1, value); }
        }
        string fBenutzerfeld2;
        [Size(255)]
        public string Benutzerfeld2
        {
            get { return fBenutzerfeld2; }
            set { SetPropertyValue<string>("Benutzerfeld2", ref fBenutzerfeld2, value); }
        }
        string fBenutzerfeld3;
        [Size(255)]
        public string Benutzerfeld3
        {
            get { return fBenutzerfeld3; }
            set { SetPropertyValue<string>("Benutzerfeld3", ref fBenutzerfeld3, value); }
        }
        string fBenutzerfeld4;
        [Size(255)]
        public string Benutzerfeld4
        {
            get { return fBenutzerfeld4; }
            set { SetPropertyValue<string>("Benutzerfeld4", ref fBenutzerfeld4, value); }
        }
        string fBenutzerfeld5;
        [Size(255)]
        public string Benutzerfeld5
        {
            get { return fBenutzerfeld5; }
            set { SetPropertyValue<string>("Benutzerfeld5", ref fBenutzerfeld5, value); }
        }
        bool fPrivat;
        public bool Privat
        {
            get { return fPrivat; }
            set { SetPropertyValue<bool>("Privat", ref fPrivat, value); }
        }
        bool fManuellArchiviert;
        public bool ManuellArchiviert
        {
            get { return fManuellArchiviert; }
            set { SetPropertyValue<bool>("ManuellArchiviert", ref fManuellArchiviert, value); }
        }
        bool fGeloescht;
        public bool Geloescht
        {
            get { return fGeloescht; }
            set { SetPropertyValue<bool>("Geloescht", ref fGeloescht, value); }
        }
        string fGeaendertVon;
        [Size(10)]
        public string GeaendertVon
        {
            get { return fGeaendertVon; }
            set { SetPropertyValue<string>("GeaendertVon", ref fGeaendertVon, value); }
        }
        DateTime fGeaendertAm;
        [Indexed(Name = @"IX_GeaendertAm")]
        public DateTime GeaendertAm
        {
            get { return fGeaendertAm; }
            set { SetPropertyValue<DateTime>("GeaendertAm", ref fGeaendertAm, value); }
        }
        string fErstelltVon;
        [Size(10)]
        public string ErstelltVon
        {
            get { return fErstelltVon; }
            set { SetPropertyValue<string>("ErstelltVon", ref fErstelltVon, value); }
        }
        DateTime fErstelltAm;
        [Indexed(Name = @"IX_ErstelltAm")]
        public DateTime ErstelltAm
        {
            get { return fErstelltAm; }
            set { SetPropertyValue<DateTime>("ErstelltAm", ref fErstelltAm, value); }
        }
        [Association(@"ArchivFilesReferencesArchivHeader")]
        public XPCollection<ArchivFiles> ArchivFilesCollection { get { return GetCollection<ArchivFiles>("ArchivFilesCollection"); } }
        [Association(@"ArchivRelationsReferencesArchivHeader")]
        public XPCollection<ArchivRelations> ArchivRelationsCollection { get { return GetCollection<ArchivRelations>("ArchivRelationsCollection"); } }
        [Association(@"ArchivVerzeichnisLinkReferencesArchivHeader")]
        public XPCollection<ArchivVerzeichnisLink> ArchivVerzeichnisLinks { get { return GetCollection<ArchivVerzeichnisLink>("ArchivVerzeichnisLinks"); } }
    }

}
