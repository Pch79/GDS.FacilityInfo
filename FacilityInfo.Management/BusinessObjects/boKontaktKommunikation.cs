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

namespace FacilityInfo.Adresse.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Kommunikation (Kontakt)")]
    [Serializable]
    public class boKontaktKommunikation : boKommunikationItem
    {
        private boKontakt _kontakt;
        public boKontaktKommunikation(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }


        [XafDisplayName("Kontakt")]
        [Association("boKontakt-boKontaktKommunikation")]
        public boKontakt Kontakt
        {
            get
            {
                return _kontakt;
            }
            set
            {
                SetPropertyValue("Kontakt", ref _kontakt, value);
            }
        }
    }
}