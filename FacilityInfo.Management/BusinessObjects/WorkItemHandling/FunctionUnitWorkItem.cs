using System;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Management.BusinessObjects.WorkItemHandling
{
    [DefaultClassOptions]
    [XafDisplayName("Maßnahem (Funktionseinheit)")]
    [ImageName("helmet_mine_16")]
    [XafDefaultProperty("MatchKey")]
    public class FunctionUnitWorkItem : WorkItem
    {


        private LgHaustechnikKomponente _functionUnit;
        public FunctionUnitWorkItem(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.IsLoading)
            {
                switch (propertyName)
                {
                    /*
                    case "FunctionUnit":
                        if (newValue != null)
                        {

                            this.Liegenschaft = this.Session.GetObjectByKey<boLiegenschaft>(((LgHaustechnikKomponente)newValue).Liegenschaft.Oid);
                        }
                        else
                        {
                            this.Liegenschaft = null;
                        }
                        break;
                        */
                    case "Status":
                        if (((enmBearbeitungsStatus)newValue) == enmBearbeitungsStatus.erledigt)
                        {
                            this.DatumReal = DateTime.Now;
                            this.Save();
                            Session.CommitTransaction();
                            if (this.TurnusValue > 0 && this.Turnus != enmTurnus.none)
                            {

                                WorkItem retValBase = (WorkItem)this;
                                //createNewAction(retValBase);

                            }
                        }
                        break;


                }
            }
        }


        [XafDisplayName("Matchkey")]
        public String MatchKey
        {
            get
            {
                var retVal = string.Empty;
                var bezeichnung = string.Empty;
                var functionUnit = string.Empty;
                bezeichnung = (this.Bezeichnung != null) ? this.Bezeichnung : "n.a.";
                functionUnit = (this.FunctionUnit != null) ? this.FunctionUnit.Bezeichnung : "n.a.";
                retVal = String.Format("{0} - {1}", functionUnit, bezeichnung);
                return retVal;
            }
        }
        [XafDisplayName("Funktionseinheit")]
        [Association("LgHaustechnikKomponente-FunctionUnitWorkItem")]
        public LgHaustechnikKomponente FunctionUnit
        {
            get { return _functionUnit; }
            set { SetPropertyValue("FunctionUnit", ref _functionUnit, value); }
        }
    }

}