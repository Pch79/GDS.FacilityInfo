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
    [XafDefaultProperty("Matchkey")]

    public class fiZugangLiegenschaft : fiZugang
    {
        private boLiegenschaft _liegenschaft;
        private Boolean _hauptZugang;
        private boAdresse _zugangAdresse;
        private boKontakt _kontakt;
        public fiZugangLiegenschaft(Session session)
            : base(session)
        {
        }
        
        protected override void OnSaved()
        {
            base.OnSaved();

        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //gibt es einen Kontakt aber keine Adresse ?
            /*
            if (this.Kontakt != null)


            {
                if (this.ZugangAdresse == null)
                {
                    if (this.Kontakt != null)
                    {
                        if (this.Kontakt.Adresse != null)
                        {
                            boAdresse workingAdress = this.Session.GetObjectByKey<boAdresse>(this.Kontakt.Adresse.Oid);

                            if (workingAdress != null)
                            {
                                this.ZugangAdresse = workingAdress;
                                this.Save();
                                this.Session.CommitTransaction();
                            }
                        }
                    }
                }
            }

            //wurde die Kategorie Kontakt gewählt aber es ist keine Adresse da??
            */
   
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
                            this.Status = curKategorie.DefaultStatus;


                                switch (curKategorie.Bezeichnung)
                            {
                                case "Hausverwaltung":
                                    if (this.Liegenschaft != null)
                                    {
                                        this.ZugangAdresse = (this.Liegenschaft.Hausverwalter != null) ?       this.Liegenschaft.Hausverwalter.Adresse : null;
                                       // RaisePropertyChangedEvent("ZugangAdresse");
                                    }
                                    break;
                                case "Hausbetreuung":
                                    if (this.Liegenschaft != null)
                                    {
                                        this.ZugangAdresse = (this.Liegenschaft.Hausbetreuer != null) ? this.Liegenschaft.Hausbetreuer.Adresse : null;
                                        //RaisePropertyChangedEvent("ZugangAdresse");
                                    }
                                    break;

                                //wenn Kontakt gewähl wird die Liegenschaft
                                case "Kontakt":
                                    if (this.Liegenschaft != null)
                                    {
                                        this.ZugangAdresse = (this.Liegenschaft.Liegenschaftsadresse != null) ? this.Liegenschaft.Liegenschaftsadresse : null;
                                       // RaisePropertyChangedEvent("ZugangAdresse");
                                    }
                                    break;

                                case "Sonstiges":
                                    //this.ZugangAdresse = this.ZugangAdresse = this.Liegenschaft.Liegenschaftsadresse;
                                    break;

                                default:
                                    if (this.Liegenschaft != null)
                                    {
                                        this.ZugangAdresse = (this.Liegenschaft.Liegenschaftsadresse != null) ? this.Liegenschaft.Liegenschaftsadresse : null;
                                        //RaisePropertyChangedEvent("ZugangAdresse");
                                    }
                                    break;
                            }
                           // RefreshAvailableContacts();
                        }
                        else
                        {
                            this.ZugangAdresse = null;
                        }
                        
                     

                    }
                    break;
                case "ZugangAdresse":
                    if(this.Kontakt != null)
                    {
                        this.Kontakt = null;
                    }
                    RefreshAvailableContacts();
                    break;
                  
            }
            
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


        [XafDisplayName("Matchkey")]
        public String Matchkey
        {
            get
            {

                //wenn ein Kontakt da ist muss der Matchkey anders aus sehen
                var retVal = string.Empty;
                var kategorie1 = string.Empty;
                var kategorie2 = string.Empty;
                var wert = string.Empty;
                var schluesselNummer = string.Empty;
                var kontakt = string.Empty;
                var ort = string.Empty;
                var lg = string.Empty;

                lg = (this.Liegenschaft != null) ? this.Liegenschaft.Bezeichnung : string.Empty;
                kategorie1 = (this.ZugangKategorie != null) ? this.ZugangKategorie.Bezeichnung : string.Empty;
                kategorie2 = (this.SubKategorie != null) ? this.SubKategorie.Bezeichnung : string.Empty;
                wert = (this.Wert != null) ? this.Wert : string.Empty;
                schluesselNummer = (this.KeyCodeIntern != null) ? this.KeyCodeIntern : string.Empty;
                ort = (this.Ort != null) ? this.Ort : string.Empty;
                //wenn ein Kontakt eingetragen ist brauch ich den Wert und den Oret nicht

                kontakt = (this.Kontakt != null) ? String.Format("{0}, Tel:{1}, Mobil:{2}", this.Kontakt.Matchkey, this.Kontakt.Telefon1, this.Kontakt.Mobil) : null;
                if (kontakt == null)
                {
                    if (schluesselNummer != string.Empty)
                    {
                        retVal = string.Format("{0}-{1}", lg, schluesselNummer);
                    }
                    else
                    {

                        retVal = string.Format("{0}-{1}-{2}", lg, ort, wert);
                    }
                }
                else
                {
                    retVal = string.Format("{0}-{1}", lg, kontakt);
                }
                return retVal;
            }
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

        [XafDisplayName("Adresse")]
       // [Association("boAdresse-fiZugangLiegenschaft")]
        [ImmediatePostData(true)]

        public boAdresse ZugangAdresse
        {
            get
            {
                return _zugangAdresse;
            }
            set
            {
                SetPropertyValue("ZugangAdresse", ref _zugangAdresse, value);
            }
        }


        [XafDisplayName("Kontakt")]
   
        [ImmediatePostData(true)]
        [DataSourceProperty("lstAvailableContacts")]
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

        private XPCollection<boKontakt> _lstAvailableContacts;
        [Browsable(false)]

        public XPCollection<boKontakt> lstAvailableContacts
        {
            get
            {
                if (_lstAvailableContacts == null)
                {
                    _lstAvailableContacts = new XPCollection<boKontakt>(this.Session);
                    RefreshAvailableContacts();
                }

                return _lstAvailableContacts;
            }
        }

        

        public void RefreshAvailableContacts()
        {
            if (_lstAvailableContacts == null)
            {
                _lstAvailableContacts = new XPCollection<boKontakt>(this.Session);
                 RefreshAvailableContacts();
                return;
            }


            //if(this.ZugangKategorie.Bezeichnung =="Hausverwalter")
                
                if (this._zugangAdresse != null)
                {

                    _lstAvailableContacts.Criteria = new BinaryOperator("Adresse.Oid", this.ZugangAdresse.Oid, BinaryOperatorType.Equal);
                }
                else
                {
                    _lstAvailableContacts.Criteria = new BinaryOperator(1, 1, BinaryOperatorType.Equal);

                }
           



        }
        


    }
}