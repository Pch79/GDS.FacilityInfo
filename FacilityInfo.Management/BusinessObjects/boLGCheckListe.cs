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
using FacilityInfo.Management.DomainComponents;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using FacilityInfo.Management.EnumStore;
using FacilityInfo.Liegenschaft.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Liegenschaftscheckliste")]
    [ImageName("application_home")]
        
    public class boLGCheckListe : BaseObject,IcheckListe
    {
        private boLiegenschaft _liegenschaft;
        private System.String _bezeichnung;
        private enmMassnahmenStatus _status;
        private System.DateTime _erstellungsdatum;
        private PermissionPolicyUser _ersteller;
        private System.DateTime _datumFertigstellung;
        public boLGCheckListe(Session session)
            : base(session)
        {
        }

        [XafDisplayName("Prüfpunkte")]
        [Association("boLgCheckliste-boLGCheckEntry"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boLGCheckEntry> lstLGCheckEntries
        {
            get
            {
                return GetCollection<boLGCheckEntry>("lstLGCheckEntries");
            }
        }


        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-boLGCheckListe")]
        public boLiegenschaft Liegenschaft
        {
            get
            {
                return _liegenschaft;
            }
            set
            {
                SetPropertyValue("Liegenschaft", ref _liegenschaft, value);
            }
        }


        [XafDisplayName("Bezeichnung")]
        public string bezeichnung
        {
            get
            {
                return _bezeichnung;   
            }

            set
            {
                SetPropertyValue("bezeichnung",ref _bezeichnung, value);
            }
        }
       
        [XafDisplayName("Fertigstellungsdatum")]
        [ReadOnly(true)]
        public DateTime datumFertigstellung
        {
            get
            {
                return _datumFertigstellung;
            }

            set
            {
                SetPropertyValue("datumFertigstellung", ref _datumFertigstellung, value);
            }
        }

        [XafDisplayName("Ersteller")]
        [ReadOnly(true)]
        public PermissionPolicyUser ersteller
        {
            get
            {
                return _ersteller;
            }

            set
            {
                SetPropertyValue("ersteller", ref _ersteller, value);
            }
        }

        [XafDisplayName("Erstellungsdatum")]
        [ReadOnly(true)]
        public DateTime erstellungsdatum
        {
            get
            {
                return _erstellungsdatum;
            }

            set
            {
                SetPropertyValue("erstellungsdatum", ref _erstellungsdatum, value);
            }
        }

        [XafDisplayName("Bearbeitungsstatus")]
        [ReadOnly(true)]
        public enmMassnahmenStatus status
        {
            get
            {
                return _status;
            }

            set
            {
                SetPropertyValue("status", ref _status, value);
            }
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public Int32 checkItemCount
        {
            get
            {
                Int32 retVal = 0;
                retVal = this.lstLGCheckEntries.Count;
                return retVal;
            }
        }

        //[ManyToManyAlias()]
       

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

            this.status = enmMassnahmenStatus.offen;
            this.erstellungsdatum = DateTime.Now;
            this.ersteller = this.Session.GetObjectByKey<PermissionPolicyUser>(SecuritySystem.CurrentUserId);
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "status":
                    if((enmMassnahmenStatus)newValue == enmMassnahmenStatus.erledigt)
                    {
                        this.datumFertigstellung = DateTime.Now;
                    }
                    break;
            }
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            
           

        }

    }
}