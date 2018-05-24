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
using DevExpress.Xpo.Metadata;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Checkliste")]
    [ImageName("document_valid")]
    [Serializable]
    [XafDefaultProperty("Matchkey")]

    
    public class boCheckliste : BaseObject
    {
        
       
        private System.String _bezeichnung;
        private System.Boolean _aktiv;
        private boFIObjekt _objekttyp;




        
        
        public boCheckliste(Session session)
            : base(session)
        {
        }

        
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Aktiv = true;
            
        }
        #region Properties

      
        [XafDisplayName("Objekttyp")]
        [ImmediatePostData(true)]
        [Association("boFIObjekt-boCheckliste")]
        [RuleRequiredField]
        public boFIObjekt Objekttyp
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
        
        
        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
               
                var name = string.Empty;



                name = (this.Objekttyp != null)?this.Objekttyp.Bezeichnung:"N/A";
                

                retVal = string.Format("{0} - {1}", this.Bezeichnung, name);
                return retVal;
            }
        }


        [XafDisplayName("Prüfpunkte")]
        [Association("boCheckliste-boCheckItem"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boCheckItem> CheckItems
        {
           get
            {
                return GetCollection<boCheckItem>("CheckItems");
            }
        }

       

        [XafDisplayName("Bezeichnung")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        [XafDisplayName("Aktiv")]
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
        #endregion

    }
}