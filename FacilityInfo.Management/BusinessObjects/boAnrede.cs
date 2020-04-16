using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using FacilityInfo.Management.EnumStore;
using System;
using System.Linq;

namespace FacilityInfo.Adresse.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anrede")]
    [XafDefaultProperty("Text")]
    [ImageName("tag")]
    public class boAnrede : BaseObject
    {
        private enmGeschlecht _geschlecht;
        private System.String _text;
        private System.String _briefanrede;
        private System.String _fremdsystemId;
        public boAnrede(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
        #region Properties
        
        [XafDisplayName("FremdsystemId")]
        public System.String FremdsystemId
        {
            get
            {
                return _fremdsystemId;
            }
            set
            {
                SetPropertyValue("FremdsystemId", ref _fremdsystemId, value);
            }
        }
        [XafDisplayName("Briefanrede")]
        public System.String Breifanrede
        {
            get
            {
                return _briefanrede;
            }
            set
            {
                SetPropertyValue("Briefanrede", ref _briefanrede, value);
            }
        }
        [XafDisplayName("Geschlecht")]
        public enmGeschlecht Geschlecht
        {
            get
            {
                return _geschlecht;
            }
            set
            {
                SetPropertyValue("Geschlecht", ref _geschlecht, value);
            }
        }
        
        [XafDisplayName("Text")]
        public System.String Text
        {
            get
            {
                return _text;
            }
            set
            {
                SetPropertyValue("Text", ref _text, value);
            }
        }
        #endregion
        
    }
}