using System;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using FacilityInfo.Action.BusinessObjects;
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Management.BusinessObjects.ActionHandling
{
    [DefaultClassOptions]
    [XafDisplayName("Maßnahem (Funktionseinheit)")]
    [ImageName("helmet_mine_16")]
    [XafDefaultProperty("MatchKey")]
    public class FunctionUnitAction : actionActionBase
    {


        private LgHaustechnikKomponente _functionUnit;
        public FunctionUnitAction(Session session) : base(session)
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
                    case "Status":
                        if (((enmBearbeitungsStatus)newValue) == enmBearbeitungsStatus.erledigt)
                        {
                            this.DatumReal = DateTime.Now;
                            this.Save();
                            Session.CommitTransaction();
                            if (this.TurnusValue > 0 && this.Turnus != enmTurnus.none)
                            {

                                actionActionBase retValBase = (actionActionBase)this;
                                createNewAction(retValBase);

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
        [Association("LgHaustechnikKomponente-FunctionUnitAction")]
        public LgHaustechnikKomponente FunctionUnit
        {
            get { return _functionUnit; }
            set { SetPropertyValue("FunctionUnit", ref _functionUnit, value); }
        }
    }

}