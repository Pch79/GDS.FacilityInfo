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

namespace FacilityInfo.Import.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Dokumentobjekt")]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("document_inspect_16")]
    public class importDokumentObjekt : BaseObject
    {
        private String _bezeichnung;
        private importDokumentTyp _dokumentTyp;
        //welches Objekt
        private impotImportObjekt _importObjekt;
        private String _sheetName;
        //Positionierung anhand der zeilenindizes
        private Int32 _startIndexZeile;
        private Int32 _stopIndexZeile;
        private Int32 _startIndexSpalte;
        private Int32 _stopIndexSpalte;

        //Positionierung anhand der inhalte
        private String _startStringZeile;
        private String _stopStringZeile;
        private String _startStringSpalte;
        private String _stopStringSpalte;

        private bool _importAktiv;
        public importDokumentObjekt(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Sheetname")]
        public String SheetName
        {
            get
            {
                return _sheetName;
            }
            set
            {
                SetPropertyValue("SheetName", ref _sheetName, value);

            }
        }


        [XafDisplayName("Import Aktiv")]
        [ImagesForBoolValues("Action_Grant", "Action_Deny")]
        [CaptionsForBoolValues("ja", "nein")]
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
        [XafDisplayName("Eigenschaften")]
        [Association("importDokumentObjekt-importObjektEigenschaft")]
        public XPCollection<importObjektEigenschaft> lstObjektEigenschaft
        {
            get
            {
                return GetCollection<importObjektEigenschaft>("lstObjektEigenschaft");
            }

        }
        [XafDisplayName("Dokumentobjekt")]
        public impotImportObjekt ImportObjekt
        {
            get
            {
                return _importObjekt;
            }
            set
            {
                SetPropertyValue("ImportObjekt", ref _importObjekt, value);
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
        [XafDisplayName("Dokumenttyp")]
        [Association("importDokumentTyp-importDokumentObjekt")]
        public importDokumentTyp DokumentTyp
        {
            get
            {
                return _dokumentTyp;
            }
            set
            {
                SetPropertyValue("DokumentTyp", ref _dokumentTyp, value);
            }
        }

        //jetzt den Range definieren
        [XafDisplayName("Startindex (Zeile)")]
        public Int32 StartIndexZeile
        {
            get
            {
                return _startIndexZeile;
            }
            set
            {
                SetPropertyValue("StartIndexZeile", ref _startIndexZeile, value);
            }
        }
        [XafDisplayName("Stopindex (Zeile)")]
        public Int32 StopIndexZeile
        {
            get
            {
                return _stopIndexZeile;
            }
            set
            {
                SetPropertyValue("StartIndexZeile", ref _stopIndexZeile, value);
            }
        }
        [XafDisplayName("Startindex (Spalte)")]
        public Int32 StartIndexSpalte
        {
            get
            {
                return _startIndexSpalte;
            }
            set
            {
                SetPropertyValue("StartIndexSpalte", ref _startIndexSpalte, value);
            }
        }
        [XafDisplayName("Stopindex (Spalte)")]
        public Int32 StopIndexSpalte
        {
            get
            {
                return _stopIndexSpalte;
            }
            set
            {
                SetPropertyValue("StopIndexSpalte", ref _stopIndexSpalte, value);
            }
        }

        [XafDisplayName("Startstring (Zeile)")]
        public String StartStringZeile
        {
            get
            {
                return _startStringZeile;
            }
            set
            {
                SetPropertyValue("StartStringZeile", ref _startStringZeile, value);
            }
        }
        [XafDisplayName("Stopstring (Zeile)")]
        public String StopStringZeile
        {
            get
            {
                return _stopStringSpalte;
            }
            set
            {
                SetPropertyValue("StopStringZeile", ref _stopStringZeile, value);
            }
        }
        [XafDisplayName("Startstring (Spalte)")]
        public String StartStringSpalte
        {
            get
            {
                return _startStringSpalte;
            }
            set
            {
                SetPropertyValue("StartStringSpalte", ref _startStringSpalte, value);
            }
        }

        [XafDisplayName("Stopstring (Spalte)")]
        public String StopStringSpalte
        {
            get
            {
                return _stopStringSpalte;
            }
            set
            {
                SetPropertyValue("StopStringSpalte", ref _stopStringSpalte, value);
            }
        }


        #endregion

    }
}