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

    public partial class ProjektNachtrag : XPLiteObject
    {
        string fProjNachtragID;
        [Key]
        [Size(25)]
        public string ProjNachtragID
        {
            get { return fProjNachtragID; }
            set { SetPropertyValue<string>("ProjNachtragID", ref fProjNachtragID, value); }
        }
        string fProjNr;
        [Indexed(Name = @"ProjNr")]
        [Size(15)]
        public string ProjNr
        {
            get { return fProjNr; }
            set { SetPropertyValue<string>("ProjNr", ref fProjNr, value); }
        }
        int fNachtrag;
        public int Nachtrag
        {
            get { return fNachtrag; }
            set { SetPropertyValue<int>("Nachtrag", ref fNachtrag, value); }
        }
        short fStatus;
        [Indexed(Name = @"Status")]
        public short Status
        {
            get { return fStatus; }
            set { SetPropertyValue<short>("Status", ref fStatus, value); }
        }
        string fBemerkung;
        [Size(255)]
        public string Bemerkung
        {
            get { return fBemerkung; }
            set { SetPropertyValue<string>("Bemerkung", ref fBemerkung, value); }
        }
        string fbeauftragt;
        [Size(50)]
        public string beauftragt
        {
            get { return fbeauftragt; }
            set { SetPropertyValue<string>("beauftragt", ref fbeauftragt, value); }
        }
        string fUserErfasst;
        [Size(8)]
        public string UserErfasst
        {
            get { return fUserErfasst; }
            set { SetPropertyValue<string>("UserErfasst", ref fUserErfasst, value); }
        }
        DateTime fDatumErfasst;
        public DateTime DatumErfasst
        {
            get { return fDatumErfasst; }
            set { SetPropertyValue<DateTime>("DatumErfasst", ref fDatumErfasst, value); }
        }
        string fHinweis;
        [Size(255)]
        public string Hinweis
        {
            get { return fHinweis; }
            set { SetPropertyValue<string>("Hinweis", ref fHinweis, value); }
        }
        DateTime fDatumbeauftragt;
        public DateTime Datumbeauftragt
        {
            get { return fDatumbeauftragt; }
            set { SetPropertyValue<DateTime>("Datumbeauftragt", ref fDatumbeauftragt, value); }
        }
    }

}
