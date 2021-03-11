﻿using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Hersteller.BusinessObjects;
using FacilityInfo.Management.EnumStore;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.Helpers;
using System.Drawing;
using FacilityInfo.Management.Interfaces;
using FacilityInfo.Management.Services;
using BinaryOperatorType = DevExpress.Data.Filtering.BinaryOperatorType;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using FacilityInfo.Management.BusinessObjects.ArticleHandling;

namespace FacilityInfo.Management.BusinessObjects.TechnicalInstallation
{
    [DefaultClassOptions]
    [XafDisplayName("Technische Anlage")]
    [ImageName("centos_16")]
    [XafDefaultProperty("Designation")]
    public class TechnicalInstallation : BaseObject, ITreeNode
    {
        private string _installationCode;
        //private System.String _ansprechcode;
        private boAdresse _billingAddress;
        private string _serialNumber;
        private Int32 _constructionYear;
        private DateTime _installationDate;
        private boHersteller _manufacturer;
        private TCondition _condition;
        private boLiegenschaft _realEstate;
        private boMandant _client;
        private TechnicalInstallation _parentInstallation;
        private string _description;
        private boAdresse _installationAddress;
        private string _installationNumber;
        private string _designation;

        private FunctionalUnit _functionalUnit;
        private TOperatingState _operstionState;
        private IClientService _clientService;
        private TechnicalAssembly _technicalAssembly;
        public TechnicalInstallation(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this._clientService = new ClientService();

            this.OperationState = TOperatingState.active;

            this.Client = this._clientService.getClientByUserName(SecuritySystem.CurrentUserName, this.Session);
            
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.Session.IsObjectToDelete(this))
            {
                //wenn es sich 
                switch (propertyName)
                {
                    case "RealEstate":

                        boLiegenschaft workingLg = null;
                        if (newValue != null)
                        {
                            workingLg = ((boLiegenschaft)newValue);
                        }
                        if (this.BillingAddress == null)
                        {
                            if (newValue != null)
                            {

                                setRechnungsAdresse(((boLiegenschaft)newValue).Liegenschaftsadresse);
                            }
                        }

                        if (this.InstallationAddress == null)
                        {
                            if (newValue != null)
                            {
                                setAnlagenAdresse(((boLiegenschaft)newValue).Liegenschaftsadresse);
                            }
                        }

                        //TODO: hier auch den Mandanten noch prüfen

                        if (workingLg != null)

                        {
                            this.Client = Session.GetObjectByKey<boMandant>(workingLg.Mandant.Oid);
                            this.Save();
                        }
                        break;

                           
                    case "MainImage":
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

                    case "FunctionalUnit":
                        if (newValue != null)
                        {
                            this.RealEstate = this.Session.GetObjectByKey<boLiegenschaft>(this.FunctionalUnit.RealEstate.Oid);
                        }
                        break;
                    case "ParentInstallation":
                        if (newValue != null)
                        {
                            TechnicalInstallation parent = (TechnicalInstallation)newValue;
                            if (parent.FunctionalUnit != null)
                            {
                                this.FunctionalUnit = this.Session.GetObjectByKey<FunctionalUnit>(parent.FunctionalUnit.Oid);
                            }
                            if(parent.RealEstate != null)
                            {
                                this.RealEstate = this.Session.GetObjectByKey<boLiegenschaft>(parent.RealEstate.Oid);
                            }
                        }

                        break;
                }
            }
        }


        private void setRechnungsAdresse(boAdresse curAdresse)
        {
            this.BillingAddress = curAdresse;
        }

        private void setAnlagenAdresse(boAdresse curAdresse)
        {
            this.InstallationAddress = curAdresse;
        }

        private XPCollection<TechnicalAssembly> _availableAssemblies;
        [Browsable(false)]
        public XPCollection<TechnicalAssembly> AvailableAssemblies
        {
            get
            {
                if (_availableAssemblies == null)
                {
                    _availableAssemblies = new XPCollection<TechnicalAssembly>(this.Session);

                    RefreshAvailableAssemblies();
                }
                return _availableAssemblies;
            }
        }

        private void RefreshAvailableAssemblies()
        {
            if (_availableAssemblies == null)
            {
                return;
            }
            if (this.Manufacturer != null)
            {
                _availableAssemblies.Criteria = new BinaryOperator("Manufacturer.Oid", this.Manufacturer.Oid, BinaryOperatorType.Equal);
            }
            else
            {
                _availableAssemblies.Criteria = new BinaryOperator(1, 1, BinaryOperatorType.Equal);
            }
        }

        public void makeMainThumbnail()
        {
            setMainThumbnail();
            setMainImageWeb();
        }
        public void setMainThumbnail()
        {
            if (this.Mainimage != null)
            {
                this.MainImageThumb = PictureHelper.getThumbnailByteArray(this.Mainimage);
                this.Save();
                this.Session.CommitTransaction();
            }
        }

        public void setMainImageWeb()
        {
            if (this.Mainimage != null)
            {
                Image workingImage = PictureHelper.ImageFromByteArray(this.Mainimage);
                this.MainImageWeb = PictureHelper.ResizePicByWidth(workingImage, 400);
                this.Save();
                this.Session.CommitTransaction();
            }
        }

        [XafDisplayName("Komponenten")]
        public XPCollection<ComponentPart> ComponentParts
        {
            get
            {
                { return GetCollection<ComponentPart>("ComponentParts"); }
            }
        }

        [XafDisplayName("TechnicalAsembly")]
        [DataSourceProperty("AvailableAssemblies")]
        [Association("TechnicalInstallation-TechnicalAssembly")]
        public TechnicalAssembly TechnicalAssembly
        {
            get { return this._technicalAssembly; }
            set { SetPropertyValue("TechnicalAssembly",ref _technicalAssembly,value);}
        }

        [XafDisplayName("Wartungszone")]
        // [Delayed(true)]
        public boWartungszone MaintenanceZone
        {
            get
            {
                boWartungszone retVal = null;

                if (this.InstallationAddress != null && this.RealEstate != null)
                {
                    if (this.InstallationAddress.getWartungszone(this.RealEstate.Mandant) != null)
                    {
                        retVal = this.InstallationAddress.getWartungszone(this.RealEstate.Mandant);
                    }
                    else
                    {
                        retVal = null;
                    }
                }
                else
                {
                    retVal = null;
                }

                return retVal;
            }

        }

        [XafDisplayName("Anlagencode")]
        public string InstallationCode
        {
            get { return this._installationCode; }
            set { SetPropertyValue("InstallationCode", ref _installationCode, value); }
        }

        [XafDisplayName("Rechnungsadresse")]
        public boAdresse BillingAddress
        {
            get { return this._billingAddress; }
            set { SetPropertyValue<boAdresse>("BillingAddress", ref _billingAddress, value); }
        }

        [Association("TechnicalInstallation-FunctionalUnit")]
        public FunctionalUnit FunctionalUnit
        {
            get { return this._functionalUnit; }
            set { SetPropertyValue("FunctionalUnit", ref _functionalUnit, value); }
        }

        [XafDisplayName("Anlagenstatus")]
        public TOperatingState OperationState
        {
            get
            {
                return _operstionState;

            }
            set
            {
                SetPropertyValue("OperationState", ref _operstionState, value);
            }
        }
        [XafDisplayName("Seriennummer")]
        public string SerialNumber
        {
            get
            {
                return _serialNumber;
            }
            set
            {
                SetPropertyValue("SerialNumber", ref _serialNumber, value);
            }
        }
        [XafDisplayName("Mandant")]
        public boMandant Client
        {
            get
            {
                return _client;
            }

            set
            {
                SetPropertyValue("Client", ref _client, value);
            }
        }

        [XafDisplayName("Baujahr")]
        [ModelDefault("DisplayFormat", "0:D0")]
        public System.Int32 ConstructionYear
        {
            get
            {
                return _constructionYear;
            }
            set
            {
                SetPropertyValue("ConstructionYear", ref _constructionYear, value);
            }
        }

        [XafDisplayName("Einbaudatum")]
        public System.DateTime InstallationDate
        {
            get
            {
                return _installationDate;
            }
            set
            {
                SetPropertyValue("InstallationDate", ref _installationDate, value);
            }
        }

        [XafDisplayName("Hersteller")]
        [ImmediatePostData]
        public boHersteller Manufacturer
        {
            get
            {
                return _manufacturer;
            }
            set
            {
                SetPropertyValue("Manufacturer", ref _manufacturer, value);
                RefreshAvailableAssemblies();
            }
        }

        [XafDisplayName("Zustand")]
        public TCondition Condition
        {
            get
            {
                return _condition;
            }
            set
            {
                SetPropertyValue("Condition", ref _condition, value);
            }
        }

        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-TechnicalInstallation")]
        [RuleRequiredField]
        public boLiegenschaft RealEstate
        {
            get
            {
                return _realEstate;
            }
            set
            {
                SetPropertyValue("RealEstate", ref _realEstate, value);
            }
        }

        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                SetPropertyValue("Description", ref _description, value);
            }
        }

        [XafDisplayName("Anlagenadresse")]

        public boAdresse InstallationAddress
        {
            get
            {
                return _installationAddress;
            }
            set
            {
                SetPropertyValue("InstallationAddress", ref _installationAddress, value);
            }
        }
        [XafDisplayName("Anlagennummer")]

        public string InsatallationNumber
        {
            get
            {
                return _installationNumber;
            }
            set
            {
                SetPropertyValue("InstallationNumber", ref _installationNumber, value);
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
                SetPropertyValue("Designation", ref _designation, value);
            }
        }

        [XafDisplayName("Vorschaubild")]
        public byte[] MainImageThumb
        {
            get
            {
                return GetPropertyValue<byte[]>("MainImageThumb");
            }
            set { SetPropertyValue<byte[]>("MainImageThumb", value); }


        }
        [XafDisplayName("Titelbild (Web)")]
        public byte[] MainImageWeb
        {
            get
            {

                return GetPropertyValue<byte[]>("MainImageWeb");
            }
            set
            {
                SetPropertyValue<byte[]>("MainImageWeb", value);
            }
        }
        
        [Delayed(true)]
        public byte[] Mainimage
        {
            get
            {
                //return GetPropertyValue<byte[]>("Mainimage");
                return GetDelayedPropertyValue<byte[]>("Mainimage");
            }
            set
            {
                SetDelayedPropertyValue<byte[]>("Mainimage", value);
            }
        }

        [XafDisplayName("Hauptanlage")]
        //[DataSourceCriteria("Liegenschaft.Oid ='@this.Liegenschaft.Oid' AND Oid != '@this.Oid'")]
        [Association("ParentInstallation-ChildInstallations")]
        public TechnicalInstallation ParentInstallation
        {
            get
            {
                return _parentInstallation;
            }
            set
            {
                SetPropertyValue("ParentInstallation", ref _parentInstallation, value);
            }
        }

        [Association("ParentInstallation-ChildInstallations")]
        public XPCollection<TechnicalInstallation> ChildInstallations
        {
            get { return GetCollection<TechnicalInstallation>("ChildInstallations"); }
        }
       
        IBindingList ITreeNode.Children
        {
            get { return this.ChildInstallations; }
        }

        string ITreeNode.Name
        {
            get { return this.Designation; }
        }

        ITreeNode ITreeNode.Parent
        {
            get
            {
                return this.ParentInstallation;
            }
        }

        public System.Drawing.Image GetImage(out string imageName)
        {
            if (this.ChildInstallations != null && this.ChildInstallations.Count > 0)
            {
                imageName = "BO_Category";
            }
            else
            {
                imageName = "BO_Product";
            }
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }
        

       
    }
}