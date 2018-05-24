using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Security.Strategy;
using FacilityInfo.Building.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Messung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Probe")]
    [XafDefaultProperty("Proben Nummer")]
    [ImageName("injection")]
    public class boMessprobe : BaseObject
    {
        private boMessung _messung;
        private System.String _probennummer;
        private System.String _beschriftung;
        private System.DateTime _entnahmezeitpunkt;
        private System.String _notiz;
        private PermissionPolicyUser _probennehmer;
        //Entnahmestelle
        private fiGebaeude _gebaeude;
        private fiRaum _raum;
        private fiEbene _ebene;
        //private boGeraet _geraet;
        private AnPruefPunkt _pruefpunkt;

        //private fiAnlagenGeraet _anlagengeraet;

        public boMessprobe(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Entnahmezeitpunkt = DateTime.Now;
            this.Probennehmer = (PermissionPolicyUser)this.Session.GetObjectByKey<PermissionPolicyUser>(SecuritySystem.CurrentUserId);

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            /*
           //hier muss ich die Werte dann anschreiben
           switch(propertyName)
            {
                case "Messung":
                    //hier ist ja der Messtyp festgelegt. -> daher weiss ich welche Werte uich betrachten muss
                    boMesstyp curTyp = (this.Messung != null) ? this.Messung.Messtyp : null;
                    if(curTyp!= null)
                    {
                        if(this.lstMessItemData.Count>0)
                        {
                            this.Session.Delete(this.lstMessItemData);
                        }
                        //gibt es hier Einträge bei den Messwerten
                        if(curTyp.lstMessItemEntries!=null)
                        {
                            foreach(boMessitemEntry itementry in curTyp.lstMessItemEntries)
                            {
                                //gibt es den Eintrag schon???
                                boMessItemData curmessdata = this.Session.FindObject<boMessItemData>(new GroupOperator((new BinaryOperator("Messprobe.Oid", this.Oid, BinaryOperatorType.Equal)), new BinaryOperator("MessItemEntry.Oid", itementry.Oid, BinaryOperatorType.Equal)));
                                if(curmessdata == null)
                                {
                                    curmessdata = new boMessItemData(this.Session);
                                    curmessdata.Messprobe = this.Session.GetObjectByKey<boMessprobe>(this.Oid);
                                    curmessdata.MessItemEntry = this.Session.GetObjectByKey<boMessitemEntry>(itementry.Oid);
                                    curmessdata.Save();
                                    this.lstMessItemData.Add(curmessdata);
                                }

                            }
                        }
                    }
                    break;
            }
            */
        }


        #region Properties

        [XafDisplayName("Mandant")]
        public boMandant Mandant
        {
            get
            {
                boMandant retVal;
                retVal = (this.Messung.Mandant != null) ? this.Messung.Mandant : null;
                return retVal;
            }
        }

        [XafDisplayName("Notiz")]
        [Size(-1)]
        public System.String Notiz
        {
            get
            {
                return _notiz;
            }
            set
            {
                SetPropertyValue("Notiz", ref _notiz, value);
            }
        }


        [XafDisplayName("Prüfpunkt")]
        public AnPruefPunkt Pruefpunkt
        {
            get
            {
                return _pruefpunkt;
            }
            set
            {
                SetPropertyValue("Pruefpunkt", ref _pruefpunkt, value);
            }
        }
        /*
        [XafDisplayName("Anlagengerät")]
        [DataSourceCriteria("Anlage.Oid = '@Messung.Anlage.Oid'")]
        public fiAnlagenGeraet Anlagengeraet
        {
            get
            {
                return _anlagengeraet;
            }
            set
            {
                SetPropertyValue("Anlagengeraet", ref _anlagengeraet, value);
            }
        }
        */
      
        //Entnahmestelle
        [XafDisplayName("Gebäude")]
        [DataSourceCriteria("Liegenschaft.Oid = '@this.Messung.Anlage.Liegenschaft.Oid'")]
        public fiGebaeude Gebaeude
        {
            get
            {
                return _gebaeude;
            }
            set
            {
                SetPropertyValue("Gebaeude", ref _gebaeude, value);
            }
        }

        [XafDisplayName("Raum")]
        [DataSourceCriteria("Ebene.Oid='@this.Ebene.Oid'")]
        public fiRaum Raum
        {
            get
            {
                return _raum;
            }
            set
            {
                SetPropertyValue("Raum", ref _raum, value);
            }
        }


        [XafDisplayName("Etage")]
        [DataSourceCriteria("Gebaeude.Oid='@this.Gebaeude.Oid'")]
        public fiEbene Ebene
        {
            get
            {
                return _ebene;
            }
            set
            {
                SetPropertyValue("Ebene", ref _ebene, value);
            }
        }
        
         [XafDisplayName("Probennehmer")]
         [ReadOnly(true)]
         public PermissionPolicyUser Probennehmer
        {
            get
            {
                return _probennehmer;
            }
            set
            {
                SetPropertyValue("Probennehmer", ref _probennehmer, value);
            }
        }
        [XafDisplayName("Entnahmezeitpunkt")]
        [ReadOnly(true)]
        public System.DateTime Entnahmezeitpunkt
        {
            get
            {
                return _entnahmezeitpunkt;
            }
            set
            {
                SetPropertyValue("Entnahmezeitpunkt", ref _entnahmezeitpunkt, value);
            }
        }

        [XafDisplayName("Beschriftung")]
        //[RuleRequiredField]
        public System.String Beschriftung
        {
            get
            {
                return _beschriftung;
            }
            set
            {
                SetPropertyValue("Beschriftung", ref _beschriftung, value);
            }
        }

        [XafDisplayName("Probennummer")]
        //[RuleRequiredField]
        public System.String Probennummer
        {
            get
            {
                return _probennummer;
            }
            set
            {
                SetPropertyValue("Probennummer", ref _probennummer, value);
            }
        }
        [XafDisplayName("Messeinträge")]
        [Association("boMessprobe-boMessItemData"),DevExpress.Xpo.Aggregated]
        public XPCollection<boMessItemData> lstMessItemData
        {
            get
            {
                return GetCollection<boMessItemData>("lstMessItemData");
            }
        }

        [XafDisplayName("Messung")]
        [Association("boMessung-boMessprobe")]
        public boMessung Messung
        {
            get
            {
                return _messung;
            }
            set
            {
                SetPropertyValue("Messung", ref _messung, value);
            }
        }
        #endregion
    }
}