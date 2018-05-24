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

namespace FacilityInfo.Messung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("MeessitemEntry")]
    [XafDefaultProperty("Messitem")]
    [ImageName("elements")]
    public class boMessitemEntry : BaseObject
    {
        private boMessItem _messitem;
        private boMesstyp _messtyp;
        private System.String _bezeichnung;
         
        public boMessitemEntry(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties

      //  [XafDisplayName("Matchkey")]
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
        [XafDisplayName("Messtyp")]
        [Association("boMesstyp-boMessitemEntry")]
        public boMesstyp Messtyp
        {
            get
            {
                return _messtyp;
            }
            set
            {
                SetPropertyValue("Messtyp", ref _messtyp, value);
            }
        }


        [XafDisplayName("Messitem")]

        public boMessItem Messitem
        {
            get
            {
                return _messitem;
            }
            set
            {
                SetPropertyValue("Messitem", ref _messitem, value);
            }
        }
        [XafDisplayName("Wertebereiche")]
        [Association("boMessitemEntry-boWertebereich"),DevExpress.Xpo.Aggregated]
        public XPCollection<boWertebereich> lstWertebereiche
        {
            get
            {
                return GetCollection<boWertebereich>("lstWertebereiche");
            }
        }
        #endregion
    }
}