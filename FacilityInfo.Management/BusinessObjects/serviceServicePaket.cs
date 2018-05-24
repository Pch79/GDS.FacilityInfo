using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Management.EnumStore;
using System;
using System.Linq;

namespace FacilityInfo.Service.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Servicepaket")]
    [ImageName("category_16")]
    public class serviceServicePaket : BaseObject
    {
        private String _bezeichnung;
        private String _beschreibung;
        //das ganze noch je Systembezeichnung kategoriesieren
        //je nach Anlagengruppe
        private boAnlagenGruppe _anlagenGruppe;

        private Int32 _turnus;
        private enmTurnus _turnusBezug;


        
 


        //kalkulierte duser aus den einzzelnen Serviceitems
        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public serviceServicePaket(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties

        //die dauer aus den einzelnen Items
        [XafDisplayName("Turnus")]
        public Int32 Turnus
        {
            get
            {
                return _turnus;
            }
            set
            {
                SetPropertyValue("Turnus", ref _turnus, value);
            }
        }
        [XafDisplayName("Turnusbezug")]
        public enmTurnus TurnusBezug
        {
            get
            {
                return _turnusBezug;
            }
            set
            {
                SetPropertyValue("TurnusBezug", ref _turnusBezug, value);
            }
        }
        [XafDisplayName("Duer (berechnet)")]
        public Decimal DauerBerechnet
        {
            get
            {
                Decimal retVal = 0;
                if(this.lstServiceItems != null)
                {
                    retVal = this.lstServiceItems.Select(t => t.DauerNominell).Sum();
                }
                return retVal;
            }
        }

        
        [XafDisplayName("Anlagengruppe")]
        [Association("boAnlagenGruppe-serviceServicePaket")]
        public boAnlagenGruppe AnlagenGruppe
        {
            get
            {
                return _anlagenGruppe;
            }
            set
            {
                SetPropertyValue("AnlagenGruppe", ref _anlagenGruppe, value);
            }
        }
        [XafDisplayName("Bezeichnung")]
        public String Bezeichnung
        {
            get
            {
                return _bezeichnung;
            }
            set
            {
                SetPropertyValue("Bezeichnung", ref _bezeichnung, value);
            }
        }
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public String Beschreibung
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

       
        //Anlagengruppe hat gleichzzeitig Anlagenarten



        [XafDisplayName("Servicepunkte")]
        [Association("serviceServicePaket-serviceServiceItem")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<serviceServiceItem> lstServiceItems
        {
            get
            {
                return GetCollection<serviceServiceItem>("lstServiceItems");
            }
        }


        #endregion
    }
}