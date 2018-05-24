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
using DevExpress.ExpressApp.Utils;
using FacilityInfo.GlobalObjects.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Objektdefinition")]
    [ImageName("server_components_16")]
    [XafDefaultProperty("Bezeichnung")]
    public class boFIObjekt : BaseObject
    {
        private Type _objekttyp;
        private System.String _bezeichnung;
        private boMandant _mandant;

        
        

        public boFIObjekt(Session session)
            : base(session)
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
            //hat sich an der Collection was geändert
           

        }
      


        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
         

        }

        
        #region Properties
        [XafDisplayName("Objekttyp")]
        [ImmediatePostData(true)]
        [ValueConverter(typeof(TypeToStringConverter))]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [RuleRequiredField]
        [RuleUniqueValue]
        public Type Objekttyp
        {
            get
            {
                return _objekttyp;
            }
            set
            {
                SetPropertyValue("Objekttyp", ref _objekttyp, value);
            }
        }
        [XafDisplayName("Bezeichnung")]
        [RuleRequiredField]
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
        [XafDisplayName("Datenfelder")]
        [Association("boFIObjekt-boDatenItem"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boDatenItem> lstDatenFelder
        {
            get
            {
                return GetCollection<boDatenItem>("lstDatenFelder");
            }
        }

        [XafDisplayName("Checklisten")]
        [Association("boFIObjekt-boCheckliste"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boCheckliste> lstCheckListen
        {
            get
            {
                return GetCollection<boCheckliste>("lstCheckListen");
            }
        }

        [XafDisplayName("Bearbeitungszeiten")]
        [Association("boFIObjekt-boBearbeitungsZeit"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boBearbeitungsZeit> lstBearbeitungszeiten
        {
            get
            {
                return GetCollection<boBearbeitungsZeit>("lstBearbeitungszeiten");
            }
        }

        [XafDisplayName("Mandant")]
        [Association("boMandant-boFiObjekt"), DevExpress.ExpressApp.DC.Aggregated]
        public boMandant Mandant
        {
            get
            {
                return _mandant;
            }
            set
            {
                SetPropertyValue("Mandant", ref _mandant, value);
            }
        }

        [XafDisplayName("Technikdefinitionen")]
        [Association("boFIObjekt-fitechnikDefinition")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<fiTechnikDefinition> lstTechnikDefinitionen
        {
            get
            {
                return GetCollection<fiTechnikDefinition>("lstTechnikDefinitionen");
            }
        }

        #endregion
    }
}