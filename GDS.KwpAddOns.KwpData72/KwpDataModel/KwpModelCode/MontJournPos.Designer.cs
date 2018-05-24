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

    public partial class MontJournPos : XPLiteObject
    {
        string fPosKey;
        [Key]
        public string PosKey
        {
            get { return fPosKey; }
            set { SetPropertyValue<string>("PosKey", ref fPosKey, value); }
        }
        MontJourn ffkMontjournID;
        [Association(@"MontJournPosReferencesMontJourn")]
        public MontJourn fkMontjournID
        {
            get { return ffkMontjournID; }
            set { SetPropertyValue<MontJourn>("fkMontjournID", ref ffkMontjournID, value); }
        }
        double fPosSaNT;
        public double PosSaNT
        {
            get { return fPosSaNT; }
            set { SetPropertyValue<double>("PosSaNT", ref fPosSaNT, value); }
        }
        double fPosMenge;
        public double PosMenge
        {
            get { return fPosMenge; }
            set { SetPropertyValue<double>("PosMenge", ref fPosMenge, value); }
        }
        double fPosAufmMenge;
        public double PosAufmMenge
        {
            get { return fPosAufmMenge; }
            set { SetPropertyValue<double>("PosAufmMenge", ref fPosAufmMenge, value); }
        }
        double fPosPEinh;
        public double PosPEinh
        {
            get { return fPosPEinh; }
            set { SetPropertyValue<double>("PosPEinh", ref fPosPEinh, value); }
        }
        double fPosMatM;
        public double PosMatM
        {
            get { return fPosMatM; }
            set { SetPropertyValue<double>("PosMatM", ref fPosMatM, value); }
        }
        double fPosMatZ;
        public double PosMatZ
        {
            get { return fPosMatZ; }
            set { SetPropertyValue<double>("PosMatZ", ref fPosMatZ, value); }
        }
        double fPosMat;
        public double PosMat
        {
            get { return fPosMat; }
            set { SetPropertyValue<double>("PosMat", ref fPosMat, value); }
        }
        double fPosZubM;
        public double PosZubM
        {
            get { return fPosZubM; }
            set { SetPropertyValue<double>("PosZubM", ref fPosZubM, value); }
        }
        double fPosZubZ;
        public double PosZubZ
        {
            get { return fPosZubZ; }
            set { SetPropertyValue<double>("PosZubZ", ref fPosZubZ, value); }
        }
        double fPosZub;
        public double PosZub
        {
            get { return fPosZub; }
            set { SetPropertyValue<double>("PosZub", ref fPosZub, value); }
        }
        double fPosLohnW;
        public double PosLohnW
        {
            get { return fPosLohnW; }
            set { SetPropertyValue<double>("PosLohnW", ref fPosLohnW, value); }
        }
        double fPosLohnB;
        public double PosLohnB
        {
            get { return fPosLohnB; }
            set { SetPropertyValue<double>("PosLohnB", ref fPosLohnB, value); }
        }
        double fPosLohnM;
        public double PosLohnM
        {
            get { return fPosLohnM; }
            set { SetPropertyValue<double>("PosLohnM", ref fPosLohnM, value); }
        }
        double fPosLohn;
        public double PosLohn
        {
            get { return fPosLohn; }
            set { SetPropertyValue<double>("PosLohn", ref fPosLohn, value); }
        }
        double fPosEP;
        public double PosEP
        {
            get { return fPosEP; }
            set { SetPropertyValue<double>("PosEP", ref fPosEP, value); }
        }
        double fPosGP;
        public double PosGP
        {
            get { return fPosGP; }
            set { SetPropertyValue<double>("PosGP", ref fPosGP, value); }
        }
        double fPosAZtBNetto;
        public double PosAZtBNetto
        {
            get { return fPosAZtBNetto; }
            set { SetPropertyValue<double>("PosAZtBNetto", ref fPosAZtBNetto, value); }
        }
        double fPosAZtWNetto;
        public double PosAZtWNetto
        {
            get { return fPosAZtWNetto; }
            set { SetPropertyValue<double>("PosAZtWNetto", ref fPosAZtWNetto, value); }
        }
        double fPosKoArt;
        public double PosKoArt
        {
            get { return fPosKoArt; }
            set { SetPropertyValue<double>("PosKoArt", ref fPosKoArt, value); }
        }
        double fPosNMatM;
        public double PosNMatM
        {
            get { return fPosNMatM; }
            set { SetPropertyValue<double>("PosNMatM", ref fPosNMatM, value); }
        }
        double fPosNMatZ;
        public double PosNMatZ
        {
            get { return fPosNMatZ; }
            set { SetPropertyValue<double>("PosNMatZ", ref fPosNMatZ, value); }
        }
        double fPosNMat;
        public double PosNMat
        {
            get { return fPosNMat; }
            set { SetPropertyValue<double>("PosNMat", ref fPosNMat, value); }
        }
        double fPosNZubM;
        public double PosNZubM
        {
            get { return fPosNZubM; }
            set { SetPropertyValue<double>("PosNZubM", ref fPosNZubM, value); }
        }
        double fPosNZubZ;
        public double PosNZubZ
        {
            get { return fPosNZubZ; }
            set { SetPropertyValue<double>("PosNZubZ", ref fPosNZubZ, value); }
        }
        double fPosNZub;
        public double PosNZub
        {
            get { return fPosNZub; }
            set { SetPropertyValue<double>("PosNZub", ref fPosNZub, value); }
        }
        double fPosVK1;
        public double PosVK1
        {
            get { return fPosVK1; }
            set { SetPropertyValue<double>("PosVK1", ref fPosVK1, value); }
        }
        double fPosVK2;
        public double PosVK2
        {
            get { return fPosVK2; }
            set { SetPropertyValue<double>("PosVK2", ref fPosVK2, value); }
        }
        double fPosVK3;
        public double PosVK3
        {
            get { return fPosVK3; }
            set { SetPropertyValue<double>("PosVK3", ref fPosVK3, value); }
        }
        double fPosBrutto;
        public double PosBrutto
        {
            get { return fPosBrutto; }
            set { SetPropertyValue<double>("PosBrutto", ref fPosBrutto, value); }
        }
        double fPosAZtW;
        public double PosAZtW
        {
            get { return fPosAZtW; }
            set { SetPropertyValue<double>("PosAZtW", ref fPosAZtW, value); }
        }
        double fPosAZtB;
        public double PosAZtB
        {
            get { return fPosAZtB; }
            set { SetPropertyValue<double>("PosAZtB", ref fPosAZtB, value); }
        }
        double fPosAZt;
        public double PosAZt
        {
            get { return fPosAZt; }
            set { SetPropertyValue<double>("PosAZt", ref fPosAZt, value); }
        }
        double fPosMwst;
        public double PosMwst
        {
            get { return fPosMwst; }
            set { SetPropertyValue<double>("PosMwst", ref fPosMwst, value); }
        }
        DateTime fPosDatErfas;
        public DateTime PosDatErfas
        {
            get { return fPosDatErfas; }
            set { SetPropertyValue<DateTime>("PosDatErfas", ref fPosDatErfas, value); }
        }
        DateTime fPosDatAend;
        public DateTime PosDatAend
        {
            get { return fPosDatAend; }
            set { SetPropertyValue<DateTime>("PosDatAend", ref fPosDatAend, value); }
        }
        DateTime fPosDatKalk;
        public DateTime PosDatKalk
        {
            get { return fPosDatKalk; }
            set { SetPropertyValue<DateTime>("PosDatKalk", ref fPosDatKalk, value); }
        }
        string fPosProj;
        [Indexed(Name = @"IX_PosProj")]
        [Size(60)]
        public string PosProj
        {
            get { return fPosProj; }
            set { SetPropertyValue<string>("PosProj", ref fPosProj, value); }
        }
        string fPosTitel;
        [Indexed(Name = @"IX_PosTitel")]
        [Size(20)]
        public string PosTitel
        {
            get { return fPosTitel; }
            set { SetPropertyValue<string>("PosTitel", ref fPosTitel, value); }
        }
        string fPosNr;
        [Indexed(Name = @"IX_PosNr")]
        [Size(20)]
        public string PosNr
        {
            get { return fPosNr; }
            set { SetPropertyValue<string>("PosNr", ref fPosNr, value); }
        }
        string fPosUNr;
        [Size(6)]
        public string PosUNr
        {
            get { return fPosUNr; }
            set { SetPropertyValue<string>("PosUNr", ref fPosUNr, value); }
        }
        string fPosSaKZ;
        [Size(1)]
        public string PosSaKZ
        {
            get { return fPosSaKZ; }
            set { SetPropertyValue<string>("PosSaKZ", ref fPosSaKZ, value); }
        }
        string fPosSaSZ;
        [Size(1)]
        public string PosSaSZ
        {
            get { return fPosSaSZ; }
            set { SetPropertyValue<string>("PosSaSZ", ref fPosSaSZ, value); }
        }
        string fPosZsKZ;
        [Size(1)]
        public string PosZsKZ
        {
            get { return fPosZsKZ; }
            set { SetPropertyValue<string>("PosZsKZ", ref fPosZsKZ, value); }
        }
        string fPosTexKZ;
        [Size(1)]
        public string PosTexKZ
        {
            get { return fPosTexKZ; }
            set { SetPropertyValue<string>("PosTexKZ", ref fPosTexKZ, value); }
        }
        string fPosArtNr;
        [Indexed(Name = @"IX_PosArtNr")]
        [Size(19)]
        public string PosArtNr
        {
            get { return fPosArtNr; }
            set { SetPropertyValue<string>("PosArtNr", ref fPosArtNr, value); }
        }
        string fPosEinh;
        [Size(8)]
        public string PosEinh
        {
            get { return fPosEinh; }
            set { SetPropertyValue<string>("PosEinh", ref fPosEinh, value); }
        }
        string fPosText1;
        [Indexed(Name = @"IX_Postext1")]
        [Size(80)]
        public string PosText1
        {
            get { return fPosText1; }
            set { SetPropertyValue<string>("PosText1", ref fPosText1, value); }
        }
        string fPosText2;
        [Indexed(Name = @"IX_Postext2")]
        [Size(80)]
        public string PosText2
        {
            get { return fPosText2; }
            set { SetPropertyValue<string>("PosText2", ref fPosText2, value); }
        }
        string fPosLText;
        [Size(SizeAttribute.Unlimited)]
        public string PosLText
        {
            get { return fPosLText; }
            set { SetPropertyValue<string>("PosLText", ref fPosLText, value); }
        }
        string fPosZTab;
        [Size(SizeAttribute.Unlimited)]
        public string PosZTab
        {
            get { return fPosZTab; }
            set { SetPropertyValue<string>("PosZTab", ref fPosZTab, value); }
        }
        string fPosAZtEH;
        [Size(10)]
        public string PosAZtEH
        {
            get { return fPosAZtEH; }
            set { SetPropertyValue<string>("PosAZtEH", ref fPosAZtEH, value); }
        }
        string fPosKalkMerk;
        [Size(1)]
        public string PosKalkMerk
        {
            get { return fPosKalkMerk; }
            set { SetPropertyValue<string>("PosKalkMerk", ref fPosKalkMerk, value); }
        }
        string fPosVonErfas;
        [Size(10)]
        public string PosVonErfas
        {
            get { return fPosVonErfas; }
            set { SetPropertyValue<string>("PosVonErfas", ref fPosVonErfas, value); }
        }
        string fPosVonAend;
        [Size(10)]
        public string PosVonAend
        {
            get { return fPosVonAend; }
            set { SetPropertyValue<string>("PosVonAend", ref fPosVonAend, value); }
        }
        string fPosVerarb;
        [Size(SizeAttribute.Unlimited)]
        public string PosVerarb
        {
            get { return fPosVerarb; }
            set { SetPropertyValue<string>("PosVerarb", ref fPosVerarb, value); }
        }
        decimal fBruttoByIncl;
        public decimal BruttoByIncl
        {
            get { return fBruttoByIncl; }
            set { SetPropertyValue<decimal>("BruttoByIncl", ref fBruttoByIncl, value); }
        }
        DateTime fCheckDate;
        public DateTime CheckDate
        {
            get { return fCheckDate; }
            set { SetPropertyValue<DateTime>("CheckDate", ref fCheckDate, value); }
        }
        int fLvSeite;
        public int LvSeite
        {
            get { return fLvSeite; }
            set { SetPropertyValue<int>("LvSeite", ref fLvSeite, value); }
        }
        bool fUPosDrucken;
        public bool UPosDrucken
        {
            get { return fUPosDrucken; }
            set { SetPropertyValue<bool>("UPosDrucken", ref fUPosDrucken, value); }
        }
        float fPosNachlass;
        public float PosNachlass
        {
            get { return fPosNachlass; }
            set { SetPropertyValue<float>("PosNachlass", ref fPosNachlass, value); }
        }
        float fDelNotiz;
        public float DelNotiz
        {
            get { return fDelNotiz; }
            set { SetPropertyValue<float>("DelNotiz", ref fDelNotiz, value); }
        }
        byte[] fPosBild;
        [Size(SizeAttribute.Unlimited)]
        public byte[] PosBild
        {
            get { return fPosBild; }
            set { SetPropertyValue<byte[]>("PosBild", ref fPosBild, value); }
        }
        bool fLtDrucken;
        public bool LtDrucken
        {
            get { return fLtDrucken; }
            set { SetPropertyValue<bool>("LtDrucken", ref fLtDrucken, value); }
        }
        string fAufmassZeile;
        [Size(255)]
        public string AufmassZeile
        {
            get { return fAufmassZeile; }
            set { SetPropertyValue<string>("AufmassZeile", ref fAufmassZeile, value); }
        }
        string fPosInfo;
        [Indexed(Name = @"IX_PosInfo")]
        [Size(255)]
        public string PosInfo
        {
            get { return fPosInfo; }
            set { SetPropertyValue<string>("PosInfo", ref fPosInfo, value); }
        }
        string fPosKeyORG;
        [Indexed(Name = @"IX_PosKeyOrg")]
        [Size(65)]
        public string PosKeyORG
        {
            get { return fPosKeyORG; }
            set { SetPropertyValue<string>("PosKeyORG", ref fPosKeyORG, value); }
        }
        DateTime fCheckDateORG;
        public DateTime CheckDateORG
        {
            get { return fCheckDateORG; }
            set { SetPropertyValue<DateTime>("CheckDateORG", ref fCheckDateORG, value); }
        }
        string fPosSync;
        [Size(20)]
        public string PosSync
        {
            get { return fPosSync; }
            set { SetPropertyValue<string>("PosSync", ref fPosSync, value); }
        }
        string fSerienNrGUID;
        [Indexed(Name = @"SerienNrGUID")]
        [Size(40)]
        public string SerienNrGUID
        {
            get { return fSerienNrGUID; }
            set { SetPropertyValue<string>("SerienNrGUID", ref fSerienNrGUID, value); }
        }
        [Association(@"MontJournPosMatReferencesMontJournPos")]
        public XPCollection<MontJournPosMat> MontJournPosMats { get { return GetCollection<MontJournPosMat>("MontJournPosMats"); } }
    }

}
