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
using FacilityInfo.Hersteller.BusinessObjects;

namespace FacilityInfo.DMS.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Herstellerdokument")]
    public class fiHerstellerAttachment : boAttachment
    {
        private boHersteller _hersteller;
        public fiHerstellerAttachment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //prodDoc
            boAttachmentBibliothek chosenLibary = this.Session.FindObject<boAttachmentBibliothek>(new BinaryOperator("Key", "manufacturerDoc", BinaryOperatorType.Equal));
            if (chosenLibary != null)
            {
                this.Bibliothek = chosenLibary;
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case "Hersteller":


                    if(newValue != null)
                    {
                        boHersteller selectedHersteller = (boHersteller)newValue;
                        this.Hersteller = this.Session.GetObjectByKey<boHersteller>(selectedHersteller.Oid);
                        this.Betreff = selectedHersteller.Bezeichnung;
                       
                    }
                    else
                    {
                        this.Hersteller = null;
                        this.Betreff = null;
                    }
                    break;
            }
        }
        #region Properties
        [XafDisplayName("Hersteller")]
        [Association("boHersteller-fiHerstellerAttachment")]
        public boHersteller Hersteller
        {
            get
            {
                return _hersteller;
            }
            set
            {
                SetPropertyValue("Hersteller", ref _hersteller, value);
            }
        }
        #endregion
    }
}