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

    public partial class PersonalJahreswerte : XPLiteObject
    {
        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>("ID", ref fID, value); }
        }
        int fJahresZahl;
        [Indexed(Name = @"IX_PersonalJahreswerteJahresZahl")]
        public int JahresZahl
        {
            get { return fJahresZahl; }
            set { SetPropertyValue<int>("JahresZahl", ref fJahresZahl, value); }
        }
        Personal fPersonalNummer;
        [Association(@"PersonalJahreswerteReferencesPersonal")]
        public Personal PersonalNummer
        {
            get { return fPersonalNummer; }
            set { SetPropertyValue<Personal>("PersonalNummer", ref fPersonalNummer, value); }
        }
        double fEBUrlaub;
        public double EBUrlaub
        {
            get { return fEBUrlaub; }
            set { SetPropertyValue<double>("EBUrlaub", ref fEBUrlaub, value); }
        }
        double fEBKrank;
        public double EBKrank
        {
            get { return fEBKrank; }
            set { SetPropertyValue<double>("EBKrank", ref fEBKrank, value); }
        }
        double fEBGleitzeit;
        public double EBGleitzeit
        {
            get { return fEBGleitzeit; }
            set { SetPropertyValue<double>("EBGleitzeit", ref fEBGleitzeit, value); }
        }
        double fUrlaubsAnspruch;
        public double UrlaubsAnspruch
        {
            get { return fUrlaubsAnspruch; }
            set { SetPropertyValue<double>("UrlaubsAnspruch", ref fUrlaubsAnspruch, value); }
        }
        double fUrlaubNeu;
        public double UrlaubNeu
        {
            get { return fUrlaubNeu; }
            set { SetPropertyValue<double>("UrlaubNeu", ref fUrlaubNeu, value); }
        }
    }

}
