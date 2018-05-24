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

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Prüfergebnis")]
    //[ImageName("")]
    
    public class boCheckItemValue : BaseObject
    {
        private System.String _bezeichnung;
        
        public boCheckItemValue(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
       
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties

        [XafDisplayName("Prüfpunkte")]
        [Association("boCheckItem-boCheckItemValue"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boCheckItem> lstCheckItems
        {
            get
            {
                return GetCollection<boCheckItem>("lstCheckItems");
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


       
        #endregion
    }
}