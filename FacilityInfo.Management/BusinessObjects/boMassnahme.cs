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
using FacilityInfo.Management.EnumStore;
using FacilityInfo.GlobalObjects.BusinessObjects;
using FacilityInfo.GlobalObjects.EnumStore;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Maßnahme")]
    [ImageName("table")]
    
    public class boMassnahme : BaseObject
    {
        private System.String _betreff;
        private System.String _beschreibung;
        private System.DateTime _erfassungsdatum;
        private System.DateTime _zieldatum;
        //erfasser
        private enmMassnahmenStatus _status;
        private boMassnahmenArt _art;
        private enmZustand _prioritaet;
       

        public boMassnahme(Session session)
            : base(session)
        {
        }

        #region Methoden
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Erfassungsdatum = DateTime.Now;
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case "Art":
                    if (newValue != null)
                    {
                        
                        if (oldValue != null)
                        {
                            boMassnahmenArt oldAnlagenArt = (boMassnahmenArt)oldValue;
                            boMassnahmenArt newAnlagenArt = (boMassnahmenArt)newValue;
                            if (oldAnlagenArt.Oid != newAnlagenArt.Oid)
                            {
                                //evtl noch prüfen ob schon werte da sind????
                                this.Session.Delete(this.lstMADatenfelder);
                            }
                        }
                    }
                    generateDataFields(this.Art);
                    break;


                case "Prioritaet":
                    boFIObjekt curFiObjekt = this.Session.FindObject<boFIObjekt>(new BinaryOperator("Objekttyp", typeof(boMassnahme), BinaryOperatorType.Equal));
                    if (curFiObjekt != null)
                    {
                        boBearbeitungsZeit curZeitItem = this.Session.FindObject<boBearbeitungsZeit>(new GroupOperator(new BinaryOperator("FiObjekt.Oid", curFiObjekt.Oid, BinaryOperatorType.Equal), new BinaryOperator("Zustand", this.Prioritaet, BinaryOperatorType.Equal)));
                        if (curZeitItem != null)
                        {
                            this.Zieldatum = curZeitItem.getZielDate(this.Erfassungsdatum);
                        }
                        else
                        {
                            this.Zieldatum = DateTime.MinValue;
                        }
                    }
                    else
                    {
                        this.Zieldatum = DateTime.MinValue;
                    }
                    break;
            }
        }



        protected override void OnSaved()
        {
            base.OnSaved();
        }

        private void generateDataFields(boMassnahmenArt curMassnahmenArt)
        {
            if (curMassnahmenArt != null)
            {
                //die Einträge löschen

                if (curMassnahmenArt.lstDatenFelder.Count > 0)
                {
                    //die einzelnen Felder durchgehen und erzeugen

                    foreach (boMADatenItem datenItem in curMassnahmenArt.lstDatenFelder)
                    {

                        boMADatenEntry curEntry = this.Session.FindObject<boMADatenEntry>(new GroupOperator(new BinaryOperator("Massnahme.Oid", this.Oid, BinaryOperatorType.Equal), new BinaryOperator("Datenfeld", datenItem.Oid, BinaryOperatorType.Equal)));
                        if (curEntry == null)
                        {
                            curEntry = new boMADatenEntry(this.Session);
                            curEntry.Massnahme = this.Session.GetObjectByKey<boMassnahme>(this.Oid);
                            curEntry.Datenfeld = this.Session.GetObjectByKey<boDatenItem>(datenItem.DatenItem.Oid);
                            curEntry.Save();
                            this.lstMADatenfelder.Add(curEntry);

                        }
                    }
                }
            }
            else
            {
                //die datenfleder wieder löschen
                this.Session.Delete(this.lstMADatenfelder);
            }
            this.Save();
            this.Session.CommitTransaction();
        }

        #endregion

        #region Properties

        [XafDisplayName("Priorität")]
        [ImmediatePostData(true)]
        public enmZustand Prioritaet
        {
            get
            {
                return _prioritaet;
            }
            set
            {
                SetPropertyValue("Prioritaet", ref _prioritaet, value);
            }
        }
        [Association("boMassnahme-boMADatenEntry"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boMADatenEntry> lstMADatenfelder
        {
            get
            {
                return GetCollection<boMADatenEntry>("lstMADatenfelder");
            }
        }

        [XafDisplayName("Massnahmentyp")]
        public enmMassnahmenTyp MassnahmenTyp
        {
            get
            {
                Type curType = this.GetType();
                if (curType == typeof(boMassnahme))
                    return enmMassnahmenTyp.Sonstige;

                if (curType == typeof(boLGMassnahme))
                    return enmMassnahmenTyp.Liegenschaftsmassnahme;

                if (curType == typeof(boANMassnahme))
                    return enmMassnahmenTyp.Anlagenmassnahme;

              

                return enmMassnahmenTyp.Sonstige;
            }
        }


        [XafDisplayName("Art")]
        [ImmediatePostData(true)]
        public boMassnahmenArt Art
        {
            get
            {
                return _art;
            }
            set
            {
                SetPropertyValue("Art", ref _art, value);
            }
        }


        [XafDisplayName("Status")]
        [ImmediatePostData(true)]
        public enmMassnahmenStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                SetPropertyValue("Status", ref _status, value);
            }
        }


        [XafDisplayName("Zieldatum")]
        [ModelDefault("DisplayFormat","{0:dd:MM:yyyy HH:MM")]
        public System.DateTime Zieldatum
        {
            get
            {
                return _zieldatum;
            }
            set
            {
                SetPropertyValue("Zieldatum", ref _zieldatum, value);
            }
        }

            
        [XafDisplayName("Erfassungsdatum")]
        [ModelDefault("DisplayFormat", "{0:dd:MM:yyyy HH:MM")]
        
        public System.DateTime Erfassungsdatum
        {
            get
            {
                return _erfassungsdatum;
            }
            set
            {
                SetPropertyValue("Erfassungsdatum", ref _erfassungsdatum, value);
            }
        }


        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public System.String Beschreibung
        {
            get
            {
                return _beschreibung;
            }
            set
            {
                SetPropertyValue("Beschreibung", ref _beschreibung, value);
            }
        }
        [XafDisplayName("Betreff")]
        public System.String Betreff
        {
            get
            {
                return _betreff;
            }
            set
            {
                SetPropertyValue("Betreff", ref _betreff, value);
            }
        }

        [XafDisplayName("Dateien und Dokumente")]
        [Association("boMassnahme-boMAAttachment"),DevExpress.Xpo.Aggregated]
        public XPCollection<boMAAttachment> lstMAAttachments
        {
            get
            {
                return GetCollection<boMAAttachment>("lstMAAttachments");
            }
        }
        #endregion

    }
}