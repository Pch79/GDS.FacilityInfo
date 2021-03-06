﻿using System;
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

namespace FacilityInfo.Building.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Ebenenart")]
    [XafDefaultProperty("Bezeichnung")]
    [RuleObjectExists(DefaultContexts.Save,"Bezeichnung = '@Bezeichnung' AND Kuerzel = '@Kuerzel'",InvertResult =true)]
    public class fiEbenenart : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _beschreibung;
        private System.String _kuerzel;
        private Int32 _defaultSortPosition;
        public fiEbenenart(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        [XafDisplayName("Sortposition (default)")]
        public Int32 DefaultSortPosition
        {
            get { return _defaultSortPosition; }
            set { SetPropertyValue("DefaultSortPosition", ref _defaultSortPosition, value); }
        }
        [XafDisplayName("Kürzel")]
        public System.String Kuerzel
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
        /*
        [ImageEditor(DetailViewImageEditorFixedHeight =120,DetailViewImageEditorMode =ImageEditorMode.PictureEdit,ListViewImageEditorCustomHeight =40,ListViewImageEditorMode=ImageEditorMode.PictureEdit,ImageSizeMode =ImageSizeMode.StretchImage)]
        */
        [XafDisplayName("Symbol")]
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

    }
}