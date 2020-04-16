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
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.DMS.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Liegenschaftsdokument")]
    [ImageName("inbox_table_16")]
    public class boLGAttachment : boAttachment
    {
        private boLiegenschaft _liegenschaft;
        public boLGAttachment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //die Bibkiothek zuordnen
            boAttachmentBibliothek chosenLibary;
            chosenLibary = this.Session.FindObject<boAttachmentBibliothek>(new BinaryOperator("Key", "LgDoc", BinaryOperatorType.Equal));
            if(chosenLibary != null)
            {
                this.Bibliothek = chosenLibary;
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if(!this.Session.IsObjectToDelete(this))
            {
                switch(propertyName)
                {
                    case "Liegenschaft":
                        if (newValue != null)
                        {
                            boLiegenschaft chosenLg = (boLiegenschaft)newValue;
                            this.Liegenschaft = this.Session.GetObjectByKey<boLiegenschaft>(chosenLg.Oid);
                            this.Betreff = chosenLg.Bezeichnung;
                            this.Mandant = this.Session.GetObjectByKey<boMandant>(this.Liegenschaft.Mandant.Oid);
                        }
                        else
                        {
                            this.Liegenschaft = null;
                            this.Betreff = null;
                        }
                        break;
                }
            }
        }

        [XafDisplayName("Liegenschaft")]
       [Association("boLiegenschaft-boLGAttachment")]
       public boLiegenschaft Liegenschaft
        {
            get
            {
                return _liegenschaft;
            }
            set
            {
                SetPropertyValue("Liegenschaft", ref _liegenschaft, value);
            }
        }
    }
}