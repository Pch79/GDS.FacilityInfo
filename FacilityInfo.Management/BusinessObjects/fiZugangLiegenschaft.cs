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
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Adresse.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Zugang (Liegenschaft)")]
    [ImageName("key_16")]

    public class fiZugangLiegenschaft : fiZugang
    {
        private boLiegenschaft _liegenschaft;
        private Boolean _hauptZugang;
        public fiZugangLiegenschaft(Session session)
            : base(session)
        {
        }

        protected override void OnSaved()
        {
            base.OnSaved();

        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            //wenn sich die Liegneschaft ändert prüfen ob es 
            switch(propertyName)
            {
                case "Liegenschaft":
                    if (newValue != null)
                    {
                        boLiegenschaft curLiegenschaft = (boLiegenschaft)newValue;
                        if(curLiegenschaft.lstZugangLiegenschaft != null)
                        {
                            //ist schon ein Hauptzugang definiert??
                            var cnt = curLiegenschaft.lstZugangLiegenschaft.Where(t => t.HauptZugang == true).Count();
                            if(cnt == 0)
                            {
                                this.HauptZugang = true;
                            }

                        }
                        else
                        {
                            this.HauptZugang = true;
                        }
                    }
                    break;
                case "HauptZugang":
                    //wenn auf ja geändert wird prüfen ob schon ein anderer mit ja da ist, wenn dem so sei dann diesen auch ändern
                    if((bool)newValue == true)
                    {
                        if (this.Liegenschaft != null)
                        {
                            var cnt = this.Liegenschaft.lstZugangLiegenschaft.Where(t => t.HauptZugang == true && t.Oid != this.Oid).Count();
                            if (cnt > 0)
                            {
                                fiZugangLiegenschaft oldHauptZugang = this.Liegenschaft.lstZugangLiegenschaft.FirstOrDefault(t => t.HauptZugang == true && t.Oid != this.Oid);
                                oldHauptZugang.HauptZugang = false;
                                oldHauptZugang.Save();
                                
                            }
                        }
                    }

               

                    break;

                case "ZugangKategorie":

                    //wenn es sich um Hausbetreuer oder Hausverwalter handelt nur die Kontakte der jreweiligen Adresse einbelnden
                    if (newValue != null)
                    {
                        fiZugangKategorie curKategorie = (fiZugangKategorie)newValue;

                        //fiZugangKategorie curKategorie = (fiZugangKategorie)(newValue);
                        if (curKategorie != null)
                        {
                           // this.Status = curKategorie.DefaultStatus;


                                switch (curKategorie.Bezeichnung)
                            {
                                case "Hausverwaltung":
                                    if (this.Liegenschaft != null)
                                    {
                                        this.ZugangAdresse = (this.Liegenschaft.Hausverwalter != null) ?       this.Liegenschaft.Hausverwalter.Adresse : null;
                                        RaisePropertyChangedEvent("ZugangAdresse");
                                    }
                                    break;
                                case "Hausbetreuung":
                                    if (this.Liegenschaft != null)
                                    {
                                        this.ZugangAdresse = (this.Liegenschaft.Hausbetreuer != null) ? this.Liegenschaft.Hausbetreuer.Adresse : null;
                                        RaisePropertyChangedEvent("ZugangAdresse");
                                    }
                                    break;

                                default:
                                    this.ZugangAdresse = null;
                                    RaisePropertyChangedEvent("ZugangAdresse");
                                    break;
                            }
                        }
                        else
                        {
                            this.ZugangAdresse = null;
                        }

                        this.Save();
                        if(!IsLoading)
                        {
                            this.Session.CommitTransaction();
                            RaisePropertyChangedEvent("ZugangKategorie");
                        }

                    }
                    break;
                  
            }
            if(!IsLoading)
            {
                saveAndLoad();
            }
        }

        private void saveAndLoad()
        {
            if (this.Session.IsObjectToSave(this))
            {
                this.Save();
                this.Session.CommitTransaction();
            }
            this.Session.Reload(this);

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            this.HauptZugang = false;
            //Die Defaultkategorie >setzen (Sonstige)
            fiZugangKategorie defaultkategorie = this.Session.FindObject<fiZugangKategorie>(new BinaryOperator("Bezeichnung", "Sonstiges", BinaryOperatorType.Equal));
            this.ZugangKategorie = defaultkategorie;
        }
        [XafDisplayName("Husverwalter")]
        public boHausverwalter Hausverwalter
        {
            get
            {
                boHausverwalter retVal;
                retVal = (this.Liegenschaft != null) ? retVal = this.Liegenschaft.Hausverwalter : null;
                return retVal;
            }
        }

        [Association("boLiegenschaft-boZugangLiegenschaft")]
        [XafDisplayName("Liegenschaft")]
        [RuleRequiredField]
        [ImmediatePostData]

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
        [XafDisplayName("Hauptzugang")]
        [ImagesForBoolValues("Action_Grant", "Action_Deny")]
        [CaptionsForBoolValues("ja", "nein")]
        public Boolean HauptZugang
        {
            get
            {
                return _hauptZugang;
            }
            set
            {
                SetPropertyValue("HauptZugang", ref _hauptZugang, value);
            }
        }
    }
}