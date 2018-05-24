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
using FacilityInfo.Anlagen.BusinessObjects;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using FacilityInfo.GlobalObjects.EnumStore;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Messung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Messung")]
    [ImageName("system_monitor")]

    public class boMessung : BaseObject
    {
        private boAnlage _anlage;
        private System.DateTime _messdatum;
        private PermissionPolicyUser _durchfuehrender;
        private boMesstyp _messtyp;
        private enmWorkflowStatus _status;
        private System.String _notiz;
        private System.DateTime _plandatum;


        public boMessung(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Messdatum = DateTime.Now;
            this.Durchfuehrender = this.Session.GetObjectByKey<PermissionPolicyUser>(SecuritySystem.CurrentUserId);
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            //alle geräte durchgehen und für jedes Gerät das in der Anlage verzeichent ist eine Probe anlegen
            switch (propertyName)
            {
                case "Messtyp":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (this.Anlage != null)
                        {
                            //erstmal alles löschen was schon da ist
                            if (this.lstProben != null)
                            {
                                this.Session.Delete(this.lstProben);
                            }
                            if (newValue != null)
                            {
                                boMesstyp curMesstyp = (boMesstyp)newValue;
                                /*
                                if (curMesstyp.lstGeraete != null)
                                {
                                    generateProben(curMesstyp);
                                }
                                */
                            }

                        }
                    }
                    break;
            }
        }

        private void generateProben(boMesstyp curTyp)
        {
            /*
            boMesstyp curMesstyp = this.Session.GetObjectByKey<boMesstyp>(curTyp.Oid);
            //dann feststellen b in der Anlage die zur Messung gehört Geräte vorhanden sind die auch in dem Messtyp vorhanden sind
            if(curMesstyp.lstGeraete != null && this.Anlage != null)
            {
                foreach(boGeraet item in curMesstyp.lstGeraete)
                {
                    //gibt es ein Anlagengerät dazu??
                    fiAnlagenGeraet anlagenItem = this.Session.FindObject<fiAnlagenGeraet>(new GroupOperator(new BinaryOperator("Geraet.Oid", item.Oid, BinaryOperatorType.Equal), new BinaryOperator("Anlage.Oid", this.Anlage.Oid, BinaryOperatorType.Equal)));
                    if(anlagenItem != null)
                    {
                        boMessprobe messprobe = new boMessprobe(this.Session);
                        messprobe.Anlagengeraet = anlagenItem;

                        this.lstProben.Add(messprobe);

                    }
                }
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
                    retVal = (this.Anlage.Mandant != null) ? this.Anlage.Mandant : null;
                    return retVal;
                }
            }
        
        
        [XafDisplayName("Plandatum")]
        public System.DateTime Plandatum
        {
            get
            {
                return _plandatum;
            }
            set
            {
                SetPropertyValue("Plandatum", ref _plandatum, value);
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
            
        [XafDisplayName("Status")]
        public enmWorkflowStatus Status
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


        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var anlage = string.Empty;
                var messtyp = string.Empty;
                anlage = (this.Anlage!=null)?this.Anlage.AnlagenNummer:"N/A";
                messtyp = (this.Messtyp != null)?this.Messtyp.Bezeichnung:"N/A";
                retVal = string.Format("{0} - {1}", anlage, messtyp);
                
                return retVal;
            }
        }


        [XafDisplayName("Messdatum")]
        [ReadOnly(true)]
        public System.DateTime Messdatum
        {
            get
            {
                return _messdatum;
            }
            set
            {
                SetPropertyValue("Messdatum", ref _messdatum, value);
            }
        }

        [XafDisplayName("Durchführender")]
        [ReadOnly(true)]
        public PermissionPolicyUser Durchfuehrender
        {
            get
            {
                return _durchfuehrender;
            }
            set
            {
                SetPropertyValue("Durchfuehrender", ref _durchfuehrender, value);
            }
        }


        [XafDisplayName("Messtyp")]
        [RuleRequiredField]
        [ImmediatePostData]
        public boMesstyp Messtyp
        {
            get
            {
                return _messtyp;
            }
            set
            {
                SetPropertyValue("Messtyp", ref _messtyp, value);
            }
        }



        [XafDisplayName("Anlage")]
        [RuleRequiredField]
        [Association("boAnlage-boMessung")]
        public boAnlage Anlage
        {
            get
            {
                return _anlage;
            }
            set
            {
                SetPropertyValue("Anlage", ref _anlage, value);
            }
        }
        [XafDisplayName("Proben")]
        [Association("boMessung-boMessprobe"),DevExpress.Xpo.Aggregated]
        public XPCollection<boMessprobe> lstProben
        {
            get
            {
                return GetCollection<boMessprobe>("lstProben");
            }
        }
        //die Probe hat dann die definierten Werte

        [XafDisplayName("Dateien")]
        [Association("boMessung-fiMessungAttachment")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<fiMessungAttachment> lstDokumente
        {
            get
            {
                return GetCollection<fiMessungAttachment>("lstDokumente");
            }
        }
        #endregion
    }
}