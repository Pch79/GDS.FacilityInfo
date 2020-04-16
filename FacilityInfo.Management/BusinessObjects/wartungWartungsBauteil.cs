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
using FacilityInfo.Hersteller.BusinessObjects;

namespace FacilityInfo.Wartung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Wartungsbauteil (Ersatzteil)")]
    public class wartungWartungsBauteil : BaseObject
    {
        private Int32 _anzahl;
        private wartungWartungsPosition _wartungsPosition;
        private fiBauteil _bauteil;
        private String _beschreibung;
        private String _notitz;
        private String _bezeichnung;
        public wartungWartungsBauteil(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Anzahl")]
        public Int32 Anzahl
        {
            get { return _anzahl; }
            set { SetPropertyValue("Anzahl", ref _anzahl, value); }
        }
        [XafDisplayName("Bezeichnung")]
        public String Bezeichnung
        {
            get { return _bezeichnung; }
            set { SetPropertyValue("Bezeichnung", ref _bezeichnung, value); }
        }
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public String Beschreibung
        {
            get { return _beschreibung; }
            set { SetPropertyValue("Beschreibung", ref _beschreibung, value); }
        }
        [XafDisplayName("Notitz")]
        public String Notitz
        {
            get { return _notitz; }
            set { SetPropertyValue("Notitz", ref _notitz, value); }
        }

        [XafDisplayName("Bauteil")]
        [DataSourceProperty("lstAvailableBauteile")]
        public fiBauteil Bauteil
        {
            get { return _bauteil; }
            set { SetPropertyValue("Bauteil", ref _bauteil, value); }
        }

        private List<fiBauteil> lstAvailableBauteile
        {
            get { return getAvailableBauteile(); }
        }
        public  List<fiBauteil> getAvailableBauteile()
        {
            List<fiBauteil> lstBauteile = new List<fiBauteil>();
            if(this.WartungsPosition != null && this.WartungsPosition.WartungsPlan != null && this.WartungsPosition.WartungsPlan.HerstellerProdukt != null)
            {
     if  (this.WartungsPosition.WartungsPlan.HerstellerProdukt.lstBauteile != null)
                {
                    lstBauteile.AddRange(this.WartungsPosition.WartungsPlan.HerstellerProdukt.lstBauteile);
                }
                        
            }
            return lstBauteile;
        }

        
      

        [XafDisplayName("Wartungsposition")]
        [Association("wartungWartungsPosition-wartungWartungsBauteil")]

        public wartungWartungsPosition WartungsPosition
        {
            get { return _wartungsPosition; }
            set { SetPropertyValue("WartungsPosition", ref _wartungsPosition, value); }
        }
        #endregion

        //TODO: Bauteil aus der Liste wählen oder insgesamt auswählen????#

    }
}