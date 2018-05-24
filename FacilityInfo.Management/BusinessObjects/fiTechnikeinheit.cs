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
using DevExpress.Persistent.Base.General;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlagensystem")]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("user_r2d2")]
    public class fiTechnikeinheit : BaseObject//,ITreeNode
    {
    //Systembezeichung leiet sich von der Technikeinh
        private System.String _systembezeichnung;
        private System.String _bezeichnung;
        private System.String _beschreibung;
        private System.Boolean _aktiv;
        private boAnlagenArt _basisanlage;
        private fiLinkKeyHtKomponente _defaultLinkKey;
        private boAnlagenArt _category;
        private boMandant _mandant;
        
        public fiTechnikeinheit(Session session)
            : base(session)
        {
        }
        #region Methoden
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            this.Aktiv = true;
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "Basisanlage":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (newValue != null)
                        {
                            //steht schon was in der Bezeichnung?
                            var curBezeichnung = string.Empty;
                            curBezeichnung = this.SystemBezeichnung;
                            if (this.SystemBezeichnung != string.Empty)
                            {
                                this.SystemBezeichnung = String.Format("{0} - {1}",((boAnlagenArt)newValue).Bezeichnung,curBezeichnung);
                            }
                            else
                            {
                                this.SystemBezeichnung = string.Format("{0}", ((boAnlagenArt)newValue).Bezeichnung);
                            }
                        }
                        else
                        {
                            this.SystemBezeichnung = string.Empty;
                        }
                    }
                    break;
            }
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            this.Category = this.Basisanlage;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if(this.Category == null)
            {
                this.Category = this.Basisanlage;
            }
        }

        #endregion

        #region ICategorizedItem
        /*
        #region iTreeNode
        #region ITReeNode
        IBindingList ITreeNode.Children
        {
            get
            {
                return lstAnlagenarten;
            }
        }
        String ITreeNode.Name
        {
            get
            {
                return Bezeichnung;
            }
        }

        ITreeNode ITreeNode.Parent
        {
            get
            {
                return null;
            }
        }
        #endregion

        #endregion
       */
      

        public boAnlagenArt Category
        {
            get
            {
                return _category;
            }
            set
            {
                SetPropertyValue("Category", ref _category, value);
            }
        }
        #endregion
    
        #region Properties

        [XafDisplayName("Mandant")]
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
        [XafDisplayName("DefaultLinkKey")]
        public fiLinkKeyHtKomponente DefaultLinkKey
        {
            get
            {
                return _defaultLinkKey;
            }
            set
            {
                SetPropertyValue("DefaultLinkKey", ref _defaultLinkKey, value);
            }
        }
        [XafDisplayName("Basisanlage")]
        [RuleRequiredField]
        [ImmediatePostData(true)]
        public boAnlagenArt Basisanlage
        {
            get
            {
                return _basisanlage;
            }
            set
            {
                SetPropertyValue("Basisanlage", ref _basisanlage, value);
            }
        }
        [XafDisplayName("Aktiv")]
        [CaptionsForBoolValues("ja","nein")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [ImmediatePostData(true)]
        public System.Boolean Aktiv
        {
            get
            {
                return _aktiv;
            }
            set
            {
                SetPropertyValue("Aktiv", ref _aktiv, value);
            }
        }

        [XafDisplayName("Systembezeichnung")]
        //ist ja eigentlich die Anlagenart
        public System.String SystemBezeichnung
        {
            get
            {
                return _systembezeichnung;
            }
            set
            {
                SetPropertyValue("SystemBezeichnung", ref _systembezeichnung, value);
            }
        }
        [XafDisplayName("Bezeichnung")]
        //ist ja eigentlich die Anlagenart
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

        //Welche Anlagenarten ghören dazu?
        [XafDisplayName("Anlagenarten")]
        [Association("fiTechnikeinheit-boAnlagenArt")]
        public XPCollection<boAnlagenArt> lstAnlagenarten
        {
            get
            {
                return GetCollection<boAnlagenArt>("lstAnlagenarten");
            }
        }

        #endregion

    }
}