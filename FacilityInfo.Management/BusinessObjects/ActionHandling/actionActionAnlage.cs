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
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Action.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlagenmaßnahme")]
    [ImageName("helmet_mine_16")]
    [XafDefaultProperty("MatchKey")]
    public class actionActionAnlage : actionActionBase
    {
        private boAnlage _anlage;
        public actionActionAnlage(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if(!this.IsLoading)
            {
                switch (propertyName)
                {
                    case "Anlage":
                        if(newValue != null)
                        {

                            this.Liegenschaft = this.Session.GetObjectByKey<boLiegenschaft>(((boAnlage)newValue).Liegenschaft.Oid);
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

        public override void createNewAction(actionActionBase curBaseAction)
        {
            actionActionAnlage retVal = new actionActionAnlage(this.Session);
            actionActionBase retValBase = (actionActionBase)retVal;
            retVal.Anlage = Session.GetObjectByKey<boAnlage>(this.Anlage.Oid);
           
            retVal.Save();

            base.createNewAction(retValBase);


        }
        #region Properties
        [XafDisplayName("Matchkey")]
        public String MatchKey
        {
            get
            {
                var retVal = string.Empty;
                var bezeichnung = string.Empty;
                var anlage = string.Empty;
                bezeichnung = (this.Bezeichnung !=null)?this.Bezeichnung:"n.a.";
                anlage  = (this.Anlage != null)?this.Anlage.AnlagenNummer:"n.a.";
                retVal = String.Format("{0} - {1}", anlage, bezeichnung);
                return retVal;
            }
        }
        [XafDisplayName("Anlage")]
        [Association("boAnlage-actionActionAnlage")]
        public boAnlage Anlage
        {
            get { return _anlage; }
            set { SetPropertyValue("Anlage", ref _anlage, value); }
        }
        #endregion
    }
}