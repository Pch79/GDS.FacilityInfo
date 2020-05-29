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
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Messung.BusinessObjects;
using FacilityInfo.Parameter.BusinessObjects;
using FacilityInfo.Management.Helpers;
using System.Drawing;
using FacilityInfo.Management.BusinessObjects.ServiceHandling;

namespace FacilityInfo.Anlagen.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlagenart")]
    [ImageName("gear_in_16")]
    [XafDefaultProperty("Bezeichnung")]
    
    public class boAnlagenArt : BaseObject //,ITreeNode
    {
        private System.String _bezeichnung;
        
        private System.String _kuerzel;
        private System.String _beschreibung;
        private boAnlagenKategorie _anlagenkategorie;
        private System.String _ansprechcode;
        private System.Boolean _aktiv;
        
    
        
        public boAnlagenArt(Session session)
            : base(session)
        {

        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
       
            this.Aktiv = true;
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            //das Kürzel setzen
            if(this.Ansprechcode != null)
            {
                this.Kuerzel = this.Ansprechcode.Trim();
            }
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "Icon":
                
                    
                    break;
            }
        }

        private byte[] resizeIcon(byte[] source)
        {
            byte[] retVal = null;
            if (source != null)
            {
                Image workingImage = PictureHelper.ImageFromByteArray(source);
               retVal= PictureHelper.ResizePicByHeight(workingImage, 32);
            }
            return retVal;
            
            
        }


        [XafDisplayName("Icon")]
        public byte[] Icon
        {
            get
            {
                return GetPropertyValue<byte[]>("Icon");
            }
            set
            {

               
                SetPropertyValue<byte[]>("Icon",resizeIcon(value));
            }
        }

        [XafDisplayName("Baugruppen")]
        [Association("boAnlagenart-anlageBauGruppe")]
        public XPCollection<anlageBauGruppe> lstBauGruppe
        {
            get
            {
                return GetCollection<anlageBauGruppe>("lstBauGruppe");
            }
        }
        

        [XafDisplayName("Messtypen")]
        [Association("boMesstyp-boAnlagenArt")]
        public XPCollection<boMesstyp> lstMesstypen
        {
            get
            {
                return GetCollection<boMesstyp>("lstMesstypen");
            }
        }

        [XafDisplayName("Aktiv")]
        [CaptionsForBoolValues("ja", "nein")]
        [ImagesForBoolValues("Action_Grant", "Action_Deny")]
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

        [XafDisplayName("Service-Pakete")]
        [Association("serviceServicePackage-boAnlagenArt")]
        public XPCollection<serviceServicePackage> lstServicePackage
        {
            get
            {
                return GetCollection<serviceServicePackage>("lstServicePackage");
            }
        }

        
        
        [XafDisplayName("Technikeinheiten")]
        [Association("fiTechnikeinheit-boAnlagenArt")]
        public XPCollection<fiTechnikeinheit> lstTechnikeinheit
        {
            get
            {
                return GetCollection<fiTechnikeinheit>("lstTechnikeinheit");
            }
        }

        [XafDisplayName("Ansprechcode")]
        [RuleRequiredField]
        [Size(4)]
        public System.String Ansprechcode
        {
            get
            {
                return _ansprechcode;
            }
            set
            {
                //immer auf 4 auffüllen
                var resultValue = string.Empty;
                resultValue = value;
                if (resultValue != null)
                {
                    resultValue = resultValue.PadRight(4, ' ');
                }
                SetPropertyValue("Ansprechcode", ref _ansprechcode, resultValue);
            }
        }
        [XafDisplayName("Matchkey")]
        [ReadOnly(true)]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var gruppe = string.Empty;
                var bezeichnung = string.Empty;
                gruppe = (this.AnlagenKategorie != null) ? this.AnlagenKategorie.Bezeichnung : "N/A";
                if (this.Bezeichnung != null)
                {
                    bezeichnung = this.Bezeichnung;
                }
                else
                {
                    bezeichnung = "N/A";
                }

                retVal = string.Format("{0} - {1}", gruppe, bezeichnung);
                return retVal;
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


       


       
        [XafDisplayName("Bezeichnung")]
        [RuleRequiredField]
        public System.String Bezeichnung
        { get
            {
                return _bezeichnung;
            }
            set
            {
                SetPropertyValue("Bezeichnung", ref _bezeichnung, value);
            }
        }

        [XafDisplayName("Kuerzel")]
        [ReadOnly(true)]
        public System.String Kuerzel
        {
            get
            {
                //Kürzel ist der getrimte Ansprechcode
                
                return _kuerzel;
            }
            set
            {
                //der getrimmmte Ansprechcode
               
                
                SetPropertyValue("Kuerzel", ref _kuerzel, value);
            }
        }
        [XafDisplayName("Anlagenkategorie")]
        [Association("boAnlagenKategorie-boAnlagenart")]
        [RuleRequiredField]
        public boAnlagenKategorie AnlagenKategorie
        {
            get
            {
                return _anlagenkategorie;
            }
            set
            {
                SetPropertyValue("AnlagenKategorie", ref _anlagenkategorie, value);
            }
        }

      

    
        [XafDisplayName("Servicepunkte")]
        [Association("boAnlagenArt-anlagenServicePunkt")]
        public XPCollection<anlagenServicePunkt> lstServicePunkte
        {
            get
            {
                return GetCollection<anlagenServicePunkt>("lstServicePunkte");
            }
        }
        
        [XafDisplayName("Parameter")]
        [Association("boAnlagenArt-parameterAnlagenArtParam")]
        public XPCollection<parameterAnlagenArtParam> lstParameterItem
        {
            get
            {
                return GetCollection<parameterAnlagenArtParam>("lstParameterItem");
            }
        }


        [Association("boAnlagenArt-ServiceSpecification")]
        public XPCollection<ServiceSpecification> lstServiceSpecification
        {
            get { return GetCollection<ServiceSpecification>("lstServiceSpecification"); }
        }


        //Liste der Anlagen
        [XafDisplayName("Anlagen")]
        [Association("boAnlage-boAnlagenArt")]
        public XPCollection<boAnlage> lstAnlagen
        {
            get
            {
                return GetCollection<boAnlage>("lstAnlagen");
            }
        }
    }
}