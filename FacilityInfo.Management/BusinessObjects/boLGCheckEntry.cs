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
using FacilityInfo.Management.DomainComponents;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Prüfpunkt (Liegenschaft)")]
    [ImageName("attributes_display")]
    
    public class boLGCheckEntry : BaseObject, IcheckEntry
    {
        private boLGCheckListe _lgcheckliste;
        private boCheckItem _checkitem;
        private enmCheckResult _checkresult;
        private System.String _checkvalue;
        private PermissionPolicyUser _erfasser;
        private System.DateTime _erfassungsdatum;
        public boLGCheckEntry(Session session)
            : base(session)
        {
        }

        [XafDisplayName("LG-Checkliste")]
        [Association("boLgCheckliste-boLGCheckEntry")]
        public boLGCheckListe LGCheckliste
        {
            get
            {
                return _lgcheckliste;
            }
            set
            {
                SetPropertyValue("LGCheckliste", ref _lgcheckliste, value);
            }
        }
        public boCheckItem checkItem
        {
            get
            {
                return _checkitem;
            }

            set
            {
                SetPropertyValue("checkItem", ref _checkitem, value);
            }
        }

        public enmCheckResult checkResult
        {
            get
            {
                return _checkresult;
            }

            set
            {
                SetPropertyValue("checkResult", ref _checkresult, value);
            }
        }

        public string checkValue
        {
            get
            {
                return _checkvalue;
            }

            set
            {
                SetPropertyValue("checkValue", ref _checkvalue, value);
            }
        }

        public PermissionPolicyUser erfasser
        {
            get
            {
                return _erfasser;
            }

            set
            {
                SetPropertyValue("erfasser", ref _erfasser, value);
            }
        }

        public DateTime erfassungsdatum
        {
            get
            {
                return _erfassungsdatum;
            }

            set
            {
                SetPropertyValue("erfassungsdatum", ref _erfassungsdatum, value);
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if(!this.Session.IsNewObject(this) || !this.Session.IsObjectToDelete(this))
            switch(propertyName)
            {
                case "checkResult":
                        if(newValue != null)
                        {
                            enmBearbeitungsStatus curStatus = this.LGCheckliste.status;
                            this.erfasser = this.Session.GetObjectByKey<PermissionPolicyUser>(SecuritySystem.CurrentUserId);
                            this.erfassungsdatum = DateTime.Now;
                            if((enmBearbeitungsStatus)newValue != enmBearbeitungsStatus.neu)
                            {
                                var doneQuery = from boLGCheckEntry item in this.LGCheckliste.lstLGCheckEntries
                                                where item.checkResult != enmCheckResult.offen
                                                select item;
                                if(this.LGCheckliste.checkItemCount == doneQuery.Count())

                                {
                                    //this.LGCheckliste.status = enmMassnahmenStatus.erledigt;
                                }
                                else
                                {
                                  //  this.LGCheckliste.status = curStatus;
                                }
                            }
                        }
                        else
                        {
                            this.erfasser = null;
                            this.erfassungsdatum = DateTime.MinValue;
                        }
                    break;

            }
        }
    }
}