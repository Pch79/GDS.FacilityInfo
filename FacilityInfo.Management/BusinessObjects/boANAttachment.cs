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
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.DMS.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.DMS.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlagendokument")]
    [ImageName("inbox_images_16")]
    public class boANAttachment : boAttachment
    {
        private boAnlage _anlage;
        private boLiegenschaft _liegenschaft;
        public boANAttachment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            boAttachmentBibliothek chosenLibary = this.Session.FindObject<boAttachmentBibliothek>(new BinaryOperator("Key", "equipDoc", BinaryOperatorType.Equal));
            if (chosenLibary != null)
            {
                this.Bibliothek = chosenLibary;
            }
        }
      

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.Session.IsObjectToDelete(this))
            {

                switch (propertyName)
                {
                    case "Anlage":
                        if(newValue != null)
                        {
                            boAnlage selectedAnlage = this.Session.GetObjectByKey<boAnlage>(this.Anlage.Oid);

                            if (selectedAnlage.Liegenschaft != null)
                            {
                                this.Liegenschaft = this.Session.GetObjectByKey<boLiegenschaft>(selectedAnlage.Liegenschaft.Oid);
                                //den Mandanten auch gleich setzen
                                this.Mandant = this.Session.GetObjectByKey<boMandant>(selectedAnlage.Liegenschaft.Mandant.Oid);
                                this.Betreff = selectedAnlage.AnlagenNummer;
                            }
                            

                        }
                        else
                        {
                            this.Betreff = null;
                            this.Liegenschaft = null;
                            this.Mandant = null;
                        }
                        break;
                }

            }
        }


        [XafDisplayName("Liegenschaft")]
        public boLiegenschaft Liegenschaft
        { get
            { return _liegenschaft; }
            set { SetPropertyValue("Liegenschaft", ref _liegenschaft, value); }
        }
        [XafDisplayName("Anlage")]
        [Association("boAnlage-boANAttachment")]
       public boAnlage Anlage
        {
            get
            {
                return _anlage;
            }
            set
            {
                SetPropertyValue("Anlage", ref _anlage, value);
            }
        }

        
    }
}