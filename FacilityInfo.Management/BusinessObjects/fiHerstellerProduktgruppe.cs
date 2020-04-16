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

using FacilityInfo.Parameter.BusinessObjects;

namespace FacilityInfo.Hersteller.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Produktgruppe")]
    [XafDefaultProperty("Bezeichnung")]
    
    public class fiHerstellerProduktgruppe : BaseObject
    {
       
        private System.String _bezeichnung;
        private System.String _notiz;

        //Steuerungsvariablen
    
       
        public fiHerstellerProduktgruppe(Session session)
            : base(session)
        {
            //this.Session.ObjectSaved += Session_ObjectSaved;
        }

        private void Session_ObjectSaved(object sender, ObjectManipulationEventArgs e)
        {
          
        }

       
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            //die Datenfelder bei allen zugeordenten Produkten nachziehen
          
        }



        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
          
        }

        #region Properties
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

        [XafDisplayName("Bezeichnung")]
        public System.String Bezeichnung
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


        [XafDisplayName("Icon")]
       // [ImageEditor]
       // [Delayed]
        public byte[] Icon
        {
            get
            {
                return GetPropertyValue < byte[]>("Icon");
            }
            set
            {
                SetPropertyValue<byte[]>("Icon", value);
            }
        }

        [XafDisplayName("Parameter (Produktgruppe)")]
        [Association("fiHerstellerProduktgruppe-parameterProduktGruppeParam"),DevExpress.Xpo.Aggregated]
        public XPCollection<parameterProduktGruppeParam> lstParameterProduktGruppe
        {
            get
            {
                return GetCollection<parameterProduktGruppeParam>("lstParameterProduktGruppe");
            }
        }

       
        #endregion
    }
}