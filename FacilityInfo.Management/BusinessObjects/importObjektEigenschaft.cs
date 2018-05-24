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
using System.Reflection;
using FacilityInfo.Management.Klassen;

namespace FacilityInfo.Import.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Objekteigenschaft")]
   [XafDefaultProperty("PtopertyName")]
   [ImageName("document_quote_16")]
    public class importObjektEigenschaft : BaseObject
    {
        private String _propertyName;
        private String _fieldName;
        private Int32 _rowIndex;
        private Int32 _columnIndex;
        private importDokumentObjekt _dokumentObjekt;
       
        private Type _datenTyp;
        //Steuerung ob der Eintag aktiv ist oder nicht
        private bool _importAktiv;


        public importObjektEigenschaft(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
         
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "ObjektTyp":
                    Type selected = (Type)newValue;
                    
                    Console.WriteLine(selected.FullName);
                    Console.WriteLine(selected.GetTypeInfo().ToString());

                    break;
            }
        }
        #region Properties



        [XafDisplayName("Import Aktiv")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
        public Boolean ImportAktiv
        {
            get
            {
                return _importAktiv;
            }
            set
            {
                SetPropertyValue("ImportAktiv", ref _importAktiv, value);
            }
        }


        [XafDisplayName("Datentyp")]
        [ImmediatePostData(true)]
        [ValueConverter(typeof(TypeToStringConverter))]
        [TypeConverter(typeof(MyLocalizedClassInfoTypeConverter))]
 
        public Type DatenTyp
        {
            get
            {
                return _datenTyp;
                    
            }
            set
            {
                SetPropertyValue("DatenTyp", ref _datenTyp, value);

            }
        }


        [XafDisplayName("Dokumentobjekt")]
        [Association("importDokumentObjekt-importObjektEigenschaft")]
        public importDokumentObjekt DokumentObjekt
        {
            get
            {
                return _dokumentObjekt;
            }
            set
            {
                SetPropertyValue("DokumentObjekt", ref _dokumentObjekt, value);
            }
        }

        [XafDisplayName("Propertyname")]
        public String PropertyName
        {
            get
            {
                return _propertyName;
            }
            set
            {
                SetPropertyValue("PropertyName", ref _propertyName, value);
            }
        }

        [XafDisplayName("Feldname")]
        public String FieldName
        {
            get
            {
                return _fieldName;
            }
            set
            {
                SetPropertyValue("FieldName", ref _fieldName, value);
            }
        }

        [XafDisplayName("Zeilenindex")]
        public Int32 RowIndex
        {
            get
            {
                return _rowIndex;
                    
            }
            set
            {
                SetPropertyValue("RowIndex", ref _rowIndex, value);
            }
        }

        [XafDisplayName("Spaltenindex")]
        public Int32 ColumnIndex
        {
            get
            {
                return _columnIndex;

            }
            set
            {
                SetPropertyValue("ColumnIndex", ref _columnIndex, value);
            }
        }

        #endregion
    }
}