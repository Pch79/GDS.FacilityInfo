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
using FacilityInfo.DMS.BusinessObjects;
using FacilityInfo.Fremdsystem.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.DMS.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlage KWP-Vertrag")]
    public class docKwpVertragAttachment : boAttachment
    {
        private KwpWartungsVertrag _kwpVertrag;
        private KwpWartungsAnlage _kwpAnlage;
        public docKwpVertragAttachment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            boAttachmentBibliothek relatedLibary = this.Session.FindObject<boAttachmentBibliothek>(new BinaryOperator("Key", "KwpContract", BinaryOperatorType.Equal));
            if(relatedLibary != null)
            {
                this.Bibliothek = relatedLibary;
            }
          
        }


        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "KwpVertrag":
                    if(newValue !=  null)
                    {
                        KwpWartungsVertrag curVertrag = (KwpWartungsVertrag)newValue;
                        this.Betreff = curVertrag.VertragsNummer;
                        this.KwpAnlage = this.Session.GetObjectByKey<KwpWartungsAnlage>(curVertrag.WartungsAnlage.Oid);
                        //hier hab ich den Mandanten auch gleich
                        this.Mandant = this.Session.GetObjectByKey<boMandant>(curVertrag.Mandant.Oid);
                       
                    }
                    else
                    {
                        this.Betreff = null;
                        this.KwpAnlage = null;
                        this.Mandant = null;
                    }
                    break;

                

            }
        }


        [XafDisplayName("Wartungsvertrag (KWP)")]
        [Association("KwpWartungsVertrag-docKwpVertragAttachment")]
        public KwpWartungsVertrag KwpVertrag
        {
            get { return _kwpVertrag; }
            set { SetPropertyValue("KwpVertrag", ref _kwpVertrag, value); }
        }

        [XafDisplayName("Wartungsanlage (KWP)")]
        public KwpWartungsAnlage KwpAnlage
        {
            get
            {
                return _kwpAnlage;
            }

            set { SetPropertyValue("KwpAnlage", ref _kwpAnlage, value); }
        }
       
       
    }
}