﻿namespace FacilityInfo.Management.BusinessObjects.TechnicalInstallation
{
 using DevExpress.Xpo;
 using DevExpress.ExpressApp.DC;
 using DevExpress.Persistent.Base;
 using FacilityInfo.Artikelverwaltung.BusinessObjects;
 using FacilityInfo.Management.BusinessObjects.TechnicalInstallation;
 using FacilityInfo.Management.BusinessObjects.ArticleHandling;
 using DevExpress.Data.Filtering;
 using FacilityInfo.Core.BusinessObjects;
 using FacilityInfo.Management.BusinessObjects;
 using DevExpress.Persistent.BaseImpl.PermissionPolicy;
 using DevExpress.ExpressApp;
    using FacilityInfo.Management.Helpers;
    using System.Drawing;
    using FacilityInfo.Hersteller.BusinessObjects;

    [DefaultClassOptions]
    [XafDisplayName("Baugruppe")]
    [XafDefaultProperty("Bezeichnung")]
    public class TechnicalAssembly : artikelArtikelBase
    {
        private boHersteller _manufacturer;
        private System.String _designation;
        private System.String _modelName;
        private fiHerstellerProduktgruppe _productGroup;

        public TechnicalAssembly(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            artikelArtikelKatalog chosenKatalog = null;
            boMandant chosenMandant = getCurrentMandant();
            if (chosenMandant != null)
            {
                chosenKatalog = this.Session.FindObject<artikelArtikelKatalog>(new GroupOperator(new BinaryOperator("Mandant.Oid", chosenMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("Bezeichnung", "Anlagenartikel", BinaryOperatorType.Equal)));
                if (chosenKatalog != null)
                {
                    this.ArtikelKatalog = chosenKatalog;
                }
            }

        }

        private boMandant getCurrentMandant()
        {
            boMitarbeiter curMitarbeiter = null;
            boMandant curMandant = null;
            PermissionPolicyUser curUser = (PermissionPolicyUser)SecuritySystem.CurrentUser;
            //gibt es einen Mitarbeiter dazu?
            curMitarbeiter = this.Session.FindObject<boMitarbeiter>(new BinaryOperator("Systembenutzer.Oid", curUser.Oid, BinaryOperatorType.Equal));

            if (curMitarbeiter != null)
            {
                if (curMitarbeiter.Mandant != null)
                {
                    curMandant = this.Session.GetObjectByKey<boMandant>(curMitarbeiter.Mandant.Oid);
                }
            }
            return curMandant;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (this.MainImage != null)
            {
                if (this.MainImageThumb == null)
                {
                    setMainThumbnail();
                }
                if (this.MainImageWeb == null)
                {
                    setMainImageWeb();
                }
            }
            if (this.KurzText == null || this.KurzText == string.Empty)
            {
                this.KurzText = this.Designation;
                this.Save();
            }
        }

      

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            //wenn sich die Gruppe ändert müssen die Felder neu geschrieben werden
            switch (propertyName)
            {
                case "Produktbild":
                    if (!this.IsLoading)
                    {
                        if (newValue != null)
                        {
                            setMainThumbnail();
                            //hier kan ich auch gleich die Web-Implementierung erstellen
                            setMainImageWeb();
                        }
                        if (newValue == null)
                        {
                            this.MainImageThumb = null;
                            this.MainImageWeb = null;
                        }
                    }
                    break;
            }
        }

        [XafDisplayName("Anlagen")]
        [Association("TechnicalInstallation-TechnicalAssembly")]
        public XPCollection<TechnicalInstallation> lstTechnicalInstallations
        {
            get { return GetCollection<TechnicalInstallation>("lstTechnicalInstallations"); }
        }

        [XafDisplayName("Hersteller")]
        [Association("fiHersteller-TechnicalAssembly")]
        public boHersteller Manufacturer
        {
            get
            {
                return _manufacturer;
            }
            set
            {
                SetPropertyValue("Manufacturer", ref _manufacturer, value);
            }
        }

        [XafDisplayName("Produktgruppe")]
        [ImmediatePostData]
        public fiHerstellerProduktgruppe ProductGroup
        {
            get
            {
                return _productGroup;
            }
            set
            {
                SetPropertyValue("ProductGroup", ref _productGroup, value);
            }
        }

        [XafDisplayName("Bezeichnung")]
        public string Designation
        {
            get
            {
                return _designation;
            }
            set
            {
                SetPropertyValue("Desigantion", ref _designation, value);
            }
        }

        [XafDisplayName("Modellbezeichnung")]
        public System.String ModelName
        {
            get
            {
                return _modelName;
            }
            set
            {
                SetPropertyValue("ModelName", ref _modelName, value);
            }
        }

        [XafDisplayName("Produktbild")]
        [Delayed]
        public byte[] MainImage
        {
            get
            {
                return GetDelayedPropertyValue<byte[]>("MainImage");
            }
            set
            {
                SetDelayedPropertyValue<byte[]>("MainImage", value);
            }
        }
        
       

        [XafDisplayName("Component-Parts")]
        [Association("TechnicalAssembly-ComponentPart")]
        public XPCollection<ComponentPart> lstComponentParts
        {
            get
            {
                return GetCollection<ComponentPart>("lstComponentParts");
            }
        }

       
    


       
    }
}