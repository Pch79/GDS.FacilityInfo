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

namespace FacilityInfo.Import.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Importobjekt")]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("document_num_16")]
    public class impotImportObjekt : BaseObject
    {
        private String _bezeichnung;
        private String _kuerzel;
        private Type _objektTyp;
        public impotImportObjekt(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties

        [XafDisplayName("Objekttyp")]
        [ImmediatePostData(true)]
        [ValueConverter(typeof(TypeToStringConverter))]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [RuleRequiredField]
        [RuleUniqueValue]
        public Type ObjektTyp
        {
            get
            {
                return _objektTyp;
            }
            set
            {
                SetPropertyValue("ObjektTyp", ref _objektTyp, value);

            }
        }
        [XafDisplayName("Bezeichnung")]
        public String Bezeichnung
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
        [XafDisplayName("Kuerzel")]
        public String Kuerzel
        {
            get
            {
                return _kuerzel;
            }
            set
            {
                SetPropertyValue("Kuerzel", ref _kuerzel, value);
            }
        }
        #endregion

    }
}