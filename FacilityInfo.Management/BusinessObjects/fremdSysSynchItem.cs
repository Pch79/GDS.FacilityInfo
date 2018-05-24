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
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Fremdsystem.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Synchronisations-Item")]
    [ImageName("edit_diff_16")]
    public class fremdSysSynchItem : BaseObject
    {
        private String _bezeichnung;
        private String _beschreibung;
        private String _fremdsystemTabelle;
        private String _aufrufParameter;
        private Type _objektTyp;
        private fremdSysFremdsystem _fremdsystem;
        private enmFunktion _funktion;
        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public fremdSysSynchItem(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Funktion")]
        public enmFunktion Funktion
        {
            get
            {
                return _funktion;
            }
            set
            {
                SetPropertyValue("Funktion", ref _funktion, value);
            }
        }
        [XafDisplayName("Fremdsystem")]
        [Association("fremdSysFremdsystem-fremdSysFremdsystem")]
        public fremdSysFremdsystem Fremdsystem
        {
          get {
                return _fremdsystem;
          }
          set {
                SetPropertyValue("Fremdsystem", ref _fremdsystem, value);
          }
        }
        [XafDisplayName("Bezeichnung")]
        public String Bezeichnung
        {
        get {
                return _bezeichnung;
        }
        set {
                SetPropertyValue("Bezeichnung", ref _bezeichnung, value);
        }
        }
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public String Beschreibung
        {
        get {
                return _beschreibung;
        }
        set {
                SetPropertyValue("Beschreibung", ref _beschreibung, value);
        }
        }
                [XafDisplayName("Tabelle (Fremdsystem)")]
                public String FremdsystemTabelle
                {
                get {
                return _fremdsystemTabelle;
                }
                set {
                SetPropertyValue("FremdsystemTabelle", ref _fremdsystemTabelle, value);
                }
                }
        [XafDisplayName("Aufrufparameter")]
        public String AufrufParameter
        {
          get {
                return _aufrufParameter;
          }
          set {
                SetPropertyValue("AufrufParameter", ref _aufrufParameter, value);
          }
        }




        [XafDisplayName("Objekttyp")]
        [ImmediatePostData(true)]
        [ValueConverter(typeof(TypeToStringConverter))]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [RuleRequiredField]
        
        public Type Objekttyp
        {
            get
            {
                return _objektTyp;
            }
            set
            {
                SetPropertyValue("Objekttyp", ref _objektTyp, value);
            }
        }
        #endregion
    }
}