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
using FacilityInfo.Anlagen.BusinessObjects;

namespace FacilityInfo.Service.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Komponentenservice")]
    [ImageName("setting_tools_16")]
    public class serviceKomponentenService : serviceServiceBase

    {
        private LgHaustechnikKomponente _haustechnikKomponente;
        // was wird gemacht?
        // 

        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public serviceKomponentenService(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        #region Properties
        [XafDisplayName("haustechnikkomponente")]
        [Association("LgHaustechnikKomponente-serviceKomponentenService")]
        public LgHaustechnikKomponente HaustechnikKomponente
        {
            get
            {
                return _haustechnikKomponente;
            }
            set
            {
                SetPropertyValue("HaustechnikKomponente", ref _haustechnikKomponente, value);
            }
        }
#endregion
    }
}