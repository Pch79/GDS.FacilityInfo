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
using FacilityInfo.Datenfeld.BusinessObjects;

namespace FacilityInfo.Hersteller.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Produktgruppe")]
    [XafDefaultProperty("Bezeichnung")]
    
    public class fiHerstellerProduktgruppe : BaseObject
    {
       
        private System.String _bezeichnung;
        private System.String _notiz;

        //Steuerungsvariablen
        private bool _valueAdded;
        private XPCollection<fiDatenfeldProduktgruppe> lstAddedObjects;

       
        public fiHerstellerProduktgruppe(Session session)
            : base(session)
        {
            this.lstDatenfeldProduktgruppe.CollectionChanged += LstDatenfeldProduktgruppe_CollectionChanged;
            //this.Session.ObjectSaved += Session_ObjectSaved;
        }

        private void Session_ObjectSaved(object sender, ObjectManipulationEventArgs e)
        {
            if (this._valueAdded)
            {
              //  updateHerstellerProdukt();
            }
        }

        private void LstDatenfeldProduktgruppe_CollectionChanged(object sender, XPCollectionChangedEventArgs e)
        {
           if(e.CollectionChangedType == XPCollectionChangedType.AfterAdd)
            {
                
                this._valueAdded = true;
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            //die Datenfelder bei allen zugeordenten Produkten nachziehen
            //die Collection 
            if (this._valueAdded)
            {
                updateHerstellerProdukt();
            }

        }

        private void updateHerstellerProdukt()
        {
            //eine Collectiona us allen Herstellerprodukten laden die zu der Gruppe gehören
            XPCollection<fiHerstellerProdukt> lstProdukte = new XPCollection<fiHerstellerProdukt>(this.Session, new BinaryOperator("Produktgruppe.Oid", this.Oid, BinaryOperatorType.Equal));
            if(lstProdukte != null)
            {
                foreach (fiHerstellerProdukt item in lstProdukte)
                {
                    //working Field

                    foreach (fiDatenfeldProduktgruppe addedItem in this.lstDatenfeldProduktgruppe)
                    {
                        fiDatenfeldHerstellerprodukt workingItem = this.Session.FindObject<fiDatenfeldHerstellerprodukt>(new BinaryOperator("DatenfeldProduktgruppe.Oid", addedItem.Oid, BinaryOperatorType.Equal));
                        if (workingItem == null)
                        {
                            //dann den EIntrag machen
                            workingItem = new fiDatenfeldHerstellerprodukt(this.Session);
                            workingItem.DatenfeldProduktgruppe = this.Session.GetObjectByKey<fiDatenfeldProduktgruppe>(addedItem.Oid);
                            workingItem.Herstellerprodukt = this.Session.GetObjectByKey<fiHerstellerProdukt>(item.Oid);
                            
                            workingItem.Save();
                        }
                    }
                    item.Save();
                       
                }
            }
            Session.CommitTransaction();


        }


        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "lstDatenfeldProduktgruppe":
                    if(!this.Session.IsObjectToDelete(this))
                    {
                       
                    }
                    break;
            }
        }

        #region Properties
        [XafDisplayName("Notiz")]
        [Size(-1)]
        public System.String Notiz
        {
            get
            {
                return _notiz;
            }
            set
            {
                SetPropertyValue("Notiz", ref _notiz, value);
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


        [XafDisplayName("Icon")]
        [ImageEditor]
        [Delayed]
        public byte[] Icon
        {
            get
            {
                return GetPropertyValue < byte[]>("Icon");
            }
            set
            {
                SetPropertyValue<byte[]>("Icon", value);
            }
        }

        [Association("fiHerstellerProduktgruppe-fiDatenfeldProduktgruppe")]
       
        public XPCollection<fiDatenfeldProduktgruppe> lstDatenfeldProduktgruppe
        {
        
            get
            {
                return GetCollection<fiDatenfeldProduktgruppe>("lstDatenfeldProduktgruppe");
            }
        }
        
        #endregion
    }
}