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

namespace FacilityInfo.DMS.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class boANAttachment : boAttachment
    {
        private boAnlage _anlage;
        public boANAttachment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            //in den parentkey die Anlage schreiben
            //this.Parentkey = this.Anlage.Oid.ToString();
            // in den Objektkey die Liegenschaft
           // this.Objektkey = this.Anlage.Liegenschaft.Oid.ToString();

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
                            this.Parentkey = selectedAnlage.Oid.ToString();
                            this.Objektkey = selectedAnlage.Liegenschaft.Oid.ToString();

                        }
                        break;
                }

            }
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