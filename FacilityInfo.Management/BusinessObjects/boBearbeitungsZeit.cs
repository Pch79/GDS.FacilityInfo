using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Bearbeitungszeiten")]
    public class boBearbeitungsZeit : BaseObject
    {
        private enmZustand _zustand;
        private System.Int32 _bearbeitungszeit;
        private enmTurnus _zeitbezug;
        //müsste eigentlich dann bei den Fi-Objekten stehen
        private boFIObjekt _fiobjekt;

        

        public boBearbeitungsZeit(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public DateTime getZielDate(DateTime startDate)
        {
            DateTime retVal = DateTime.MinValue;
            switch (this.Zeitbezug)
            {
                case enmTurnus.Jahre:
                    retVal = startDate.AddYears(this.Bearbeitungszeit);
                    break;
                case enmTurnus.Monate:
                    retVal = startDate.AddMonths(this.Bearbeitungszeit);
                    break;
                case enmTurnus.none:
                    retVal = DateTime.MinValue;
                    break;

                case enmTurnus.Stunden:
                    retVal = startDate.AddHours(this.Bearbeitungszeit);
                    break;
                case enmTurnus.Tage:
                    retVal = startDate.AddDays(this.Bearbeitungszeit);
                    break;
            } 


            return retVal;
        }


        #region Properties
        [XafDisplayName("Zustand")]
        public enmZustand Zustand
        {
            get
            {
                return _zustand;
            }
            set
            {
                SetPropertyValue("Zustand", ref _zustand, value);
            }
        }
        [XafDisplayName("Bearbeitungszeit")]
        public System.Int32 Bearbeitungszeit
        {
            get
            {
                return _bearbeitungszeit;
            }
            set
            {
                SetPropertyValue("Bearbeitungszeit", ref _bearbeitungszeit, value);
            }
        }
        [XafDisplayName("Zeitbezug")]
        public enmTurnus Zeitbezug
        {
            get
            {
                return _zeitbezug;
            }
            set
            {
                SetPropertyValue("Zeitbezug", ref _zeitbezug, value);
            }
        }
        [XafDisplayName("FI-Objekt")]
        [Association("boFIObjekt-boBearbeitungsZeit")]
        public boFIObjekt FiObjekt
        {
            get
            {
                return _fiobjekt;
            }
            set
            {
                SetPropertyValue("FiObjekt", ref _fiobjekt, value);
            }
        }

        #endregion
    }
}