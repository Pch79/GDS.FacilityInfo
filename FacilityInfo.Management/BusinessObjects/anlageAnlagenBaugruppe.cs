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

namespace FacilityInfo.Anlagen.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlagenkomponente")]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("plugin_link_16")]
    public class anlageAnlagenbaugruppe : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _notizen;
        private anlageBauGruppe _bauGruppe;

        private boHersteller _hersteller;
        private System.String _equipmentcode;
        private boAnlage _anlage;
        private System.Int32 _anzahl;

        


        public anlageAnlagenbaugruppe(Session session)
            : base(session)
        {
        }
        #region Methoden

        
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            /*
            switch(propertyName)
            {
                case "Komponente":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (newValue != null)
                        {
                            //steht schon was in der Bezeichnung?
                            var curBezeichnung = string.Empty;
                            curBezeichnung = this.Bezeichnung;
                            if (this.Bezeichnung != string.Empty)
                            {
                                this.Bezeichnung = String.Format("{0} - {1}", ((AnKomponente)newValue).Bezeichnung, curBezeichnung);
                            }
                            else
                            {
                                this.Bezeichnung = string.Format("{0}", ((AnKomponente)newValue).Bezeichnung);
                            }
                        }
                        else
                        {
                            this.Bezeichnung = string.Empty;
                        }
                    }
                    break;
                case "Anlage":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        this.Bezeichnung = string.Format("{0}_{1}", this.Bezeichnung, getCount().ToString());
                    }

                        break;

                
            }
            */
        }

        
        /*
        private System.Int32 getCount()
        {
            Int32 retVal = 0;
            boAnlage curAnlage = this.Anlage;
            if (curAnlage.lstAnlagenkomponenten != null)
            {
                var countQuery = (from AnAnlagenKomponente item in curAnlage.lstAnlagenkomponenten
                                  where item.Komponente.Oid == this.Komponente.Oid
                                  select item).Count();
                retVal = countQuery;
            }

           
            return retVal;

        }
        */
        
        #endregion


        #region Properties
       
       
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
        [XafDisplayName("Notiz")]
        [Size(-1)]
        public System.String Notizen
        {
            get
            {
                return _notizen;
            }
            set
            {
                SetPropertyValue("Notizen", ref _notizen, value);
            }
        }
        [XafDisplayName("Baugruppe")]
        [RuleRequiredField]
        public anlageBauGruppe BauGruppe
        {
            get
            {
                return _bauGruppe;
            }
            set
            {
                SetPropertyValue("BauGruppe", ref _bauGruppe, value);
            }
        }

        //der Hersteler ist hier nicht relavant
        /*
        [XafDisplayName("Hersteller")]
        public boHersteller Hersteller
        {
            get
            {
                return _hersteller;
            }
            set
            {
                SetPropertyValue("Hersteller", ref _hersteller, value);
            }
        }
        */
        [XafDisplayName("Equipmentcode")]
        public System.String Equipmentcode
        {
            get
            {
                return _equipmentcode;
            }
            set
            {
                SetPropertyValue("Equipmentcode", ref _equipmentcode, value);
            }
        }
        
        [XafDisplayName("Anlage")]
        [Association("boAnlage-anlageAnlagenbaugruppe")]
        [RuleRequiredField]
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
        
        //Herstellerprodukte
        [XafDisplayName("Herstellerprodukte")]
        [Association("anlageAnlagenBaugruppe-fiHerstellerProdukt")]
        public XPCollection<fiHerstellerProdukt> lstHerstellerProdukte
        {
            get { return GetCollection<fiHerstellerProdukt>("lstHerstellerProdukte"); }
        }
        #endregion
    }
}