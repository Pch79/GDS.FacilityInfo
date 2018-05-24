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

    public partial class SerienNrLog : XPLiteObject
    {
        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>("ID", ref fID, value); }
        }
        SerienNr ffkSerienNrID;
        [Association(@"SerienNrLogReferencesSerienNr")]
        public SerienNr fkSerienNrID
        {
            get { return ffkSerienNrID; }
            set { SetPropertyValue<SerienNr>("fkSerienNrID", ref ffkSerienNrID, value); }
        }
        short fBuchungsGrund;
        public short BuchungsGrund
        {
            get { return fBuchungsGrund; }
            set { SetPropertyValue<short>("BuchungsGrund", ref fBuchungsGrund, value); }
        }
        string fBuchungsArt;
        [Size(5)]
        public string BuchungsArt
        {
            get { return fBuchungsArt; }
            set { SetPropertyValue<string>("BuchungsArt", ref fBuchungsArt, value); }
        }
        string fVorgangsNr;
        [Indexed(Name = @"IX_SerienNrLog_VorgangsNr")]
        [Size(55)]
        public string VorgangsNr
        {
            get { return fVorgangsNr; }
            set { SetPropertyValue<string>("VorgangsNr", ref fVorgangsNr, value); }
        }
        string fAdressKey;
        [Indexed(Name = @"IX_SerienNrLog_AdressKey")]
        [Size(24)]
        public string AdressKey
        {
            get { return fAdressKey; }
            set { SetPropertyValue<string>("AdressKey", ref fAdressKey, value); }
        }
        DateTime fAnlageAm;
        public DateTime AnlageAm
        {
            get { return fAnlageAm; }
            set { SetPropertyValue<DateTime>("AnlageAm", ref fAnlageAm, value); }
        }
        string fAnlageDurch;
        [Size(8)]
        public string AnlageDurch
        {
            get { return fAnlageDurch; }
            set { SetPropertyValue<string>("AnlageDurch", ref fAnlageDurch, value); }
        }
        string fBezugsID;
        [Indexed(Name = @"IX_SerienNrLog_BezugsID")]
        [Size(40)]
        public string BezugsID
        {
            get { return fBezugsID; }
            set { SetPropertyValue<string>("BezugsID", ref fBezugsID, value); }
        }
    }

}
