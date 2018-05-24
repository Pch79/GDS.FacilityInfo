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
using DevExpress.Persistent.Base.General;

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
        private boAnlagenGruppe _anlagengruppe;
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
        /*

        #region ITReeNode
        
        IBindingList ITreeNode.Children
        {
            get
            {
                return lstAnlagen;
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
        */



        [XafDisplayName("Komponenten")]
        [Association("boAnlagenart-AnKomponente")]
        public XPCollection<AnKomponente> lstKomponenten
        {
            get
            {
                return GetCollection<AnKomponente>("lstKomponenten");
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
                gruppe = (this.AnlagenGruppe != null) ? this.AnlagenGruppe.Bezeichnung : "N/A";
                bezeichnung = (this.Bezeichnung != null) ? this.Bezeichnung : "N/A";
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


        [XafDisplayName("Symbol")]
        [ImageEditor(DetailViewImageEditorFixedHeight = 180, DetailViewImageEditorFixedWidth = 180, ListViewImageEditorCustomHeight =30,ImageSizeMode =ImageSizeMode.Zoom)]
        public byte[] Symbol
        {
            get
            {
                return GetPropertyValue<byte[]>("Symbol");
            }
            set
            {
                SetPropertyValue<byte[]>("Symbol", value);
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
        [XafDisplayName("Anlagengruppe")]
        [Association("boAnlagengruppe-boAnlagenart")]
        [RuleRequiredField]
        public boAnlagenGruppe AnlagenGruppe
        {
            get
            {
                return _anlagengruppe;
            }
            set
            {
                SetPropertyValue("AnlagenGruppe", ref _anlagengruppe, value);
            }
        }

        
        [XafDisplayName("Datenfelder")]
        [Association("boAnlagenArt-boANDatenItem"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boANDatenItem> lstDatenFelder
        {
            get
            {
                return GetCollection<boANDatenItem>("lstDatenFelder");
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
        [Association("boAnlagenArt-parameterParameterItem")]
        public XPCollection<parameterParameterItem> lstParameterItem
        {
            get
            {
                return GetCollection<parameterParameterItem>("lstParameterItem");
            }
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