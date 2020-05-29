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
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Building.BusinessObjects;

using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Management.EnumStore;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using FacilityInfo.Management.Klassen;
using FacilityInfo.Fremdsystem.BusinessObjects;
using FacilityInfo.Management.BusinessObjects.WorkItemHandling;

namespace FacilityInfo.Anlagen.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Funktionseinheit")]
    [XafDefaultProperty("Systembezeichnung")]
    [ImageName("control_panel_16")]
    public class LgHaustechnikKomponente : BaseObject//,ITreeNode, ITreeNodeImageProvider
    {
        private System.String _bezeichnung;
        private String _bezeichnungIntern;
        private boLiegenschaft _liegenschaft;
        private System.String _beschreibung;
        private String _notiz;
        private fiTechnikeinheit _technikeinheit;

        //Standort
        private fiGebaeude _gebaeude;
        private fiEbene _ebene;
        private fiRaum _raum;

        //einfache Standortimplementierung

        private fiGebaeude _building;
        private fiRaumart _roomType;
        private String _roomDesignation;

        private String _floorDesignation;
        private fiEbenenart _floorType;

        //Kopplung an KWP
        private String _fremdsystemId;
        private KwpWartungsAnlage _kwpAnlage;
        //hier beim KWP die Anlagenart also das Feld Brennstoffart
        private fiLinkKeyHtKomponente _linkKeyFremdsystem;
        //hier den Vertragsstatus noch berücksichtigen
        private enmVertragsStatus _vertragsStatus;

        //Das anlagensystem ist die Anlagenart aus dem KWP

        //kann auch noch einen Ansprechpartner anfügen
        //private System.String _notfallnummer;

        public LgHaustechnikKomponente(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected override void OnDeleting()
        {
            base.OnDeleting();
            //hier die Anlagen gleich mitlöschen
            if(this.lstAnlagen != null)
            {
                this.Session.Delete(this.lstAnlagen);
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            if (this.LinkKeyFremdsystem == null)
            {
                if (this.Technikeinheit != null)
                {
                    if (this.Technikeinheit.DefaultLinkKey != null)
                    {
                        this.LinkKeyFremdsystem = this.Session.GetObjectByKey<fiLinkKeyHtKomponente>(this.Technikeinheit.DefaultLinkKey.Oid);
                        this.Save();
                    }
                }
            }
        }

        protected override void OnDeleted()
        {
            base.OnDeleted();
            //alle angehängten Anlagena uch löschen
            this.Session.Delete(this.lstAnlagen);
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
          base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                
                
                case "Technikeinheit":
                    if (!this.Session.IsObjectToDelete(this) || !this.Session.IsNewObject(this))
                    {
                        if (newValue != null)
                        {
                            //steht schon was in der Bezeichnung?
                            if (this.lstAnlagen != null)
                            {
                                this.Session.Delete(lstAnlagen);
                            }

                            //gleich auch den LinkKey setzen
                            if (((fiTechnikeinheit)newValue).DefaultLinkKey != null)
                            {

                                this.LinkKeyFremdsystem = this.Session.GetObjectByKey<fiLinkKeyHtKomponente>((((fiTechnikeinheit)newValue)).DefaultLinkKey.Oid);
                            }
                            else
                            {
                                this.LinkKeyFremdsystem = null;
                            }

                            //die Bezeichnung standardisieren   
                            //wenn ich eine LG-Nummer habe dann diese mit angeben
                            //Hier würd eigentlich der Acode reichen sind hja Fachleute
                            var acode = string.Empty;

                                acode = ((fiTechnikeinheit)newValue).Basisanlage.Ansprechcode;
                            var lgNumber = string.Empty;
                            if (this.Liegenschaft != null)
                            {
                                lgNumber = (this.Liegenschaft.Liegenschaftsnummer != null)?this.Liegenschaft.Liegenschaftsnummer:"n.A";
                            }
                            else
                            {
                                lgNumber = "n.A";
                            }

                            this.BezeichnungIntern = string.Format("{0}-{1}", acode, lgNumber);



                            //this.Bezeichnung = ((fiTechnikeinheit)newValue).Basisanlage.Ansprechcode;
                        }
                        else
                        {
                            this.LinkKeyFremdsystem = null;
                        }

                        setDefaultBezeichnung();
                    }

                   
                    break;


                    // Standort aus der HT-Komponente ziehen
                    //Statt Bezeichnung den Standort eintragen
                    
                    
                case "Bezeichnung":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (newValue != null)
                        {
                            //die bezeichnung in die Anlagen übernehmen
                            if(this.lstAnlagen != null)
                            {
                                foreach(boAnlage item in this.lstAnlagen)
                                {
                                    item.Bezeichnung = newValue.ToString();
                                    
                                }
                            }
                        }
                    }
                    break;
                //Gebäude 
                case "Gebaeude":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (newValue != null)
                        {
                            //die bezeichnung in die Anlagen übernehmen
                            if (this.lstAnlagen != null)
                            {
                                foreach (boAnlage item in this.lstAnlagen)
                                {
                                    item.Gebaeude = this.Session.GetObjectByKey<fiGebaeude>(((fiGebaeude)newValue).Oid);

                                }
                            }
                           
                        }
                        setDefaultBezeichnung();
                    }
                    break;
                //Ebenenart und Ebnenname
                case "FloorType":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (newValue != null)
                        {
                            //die bezeichnung in die Anlagen übernehmen
                            if (this.lstAnlagen != null)
                            {
                                foreach (boAnlage item in this.lstAnlagen)
                                {
                                    item.FloorType = this.Session.GetObjectByKey<fiEbenenart>(((fiEbenenart)newValue).Oid);

                                }
                            }
                        }
                    }
                    break;
                case "FloorDesignation":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (newValue != null)
                        {
                            //die bezeichnung in die Anlagen übernehmen
                            if (this.lstAnlagen != null)
                            {
                                foreach (boAnlage item in this.lstAnlagen)
                                {
                                    item.FloorDesignation = newValue.ToString();

                                }
                            }
                        }
                    }

                    break;

                //Raumart und Raumname

                case "RoomType":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (newValue != null)
                        {
                            //die bezeichnung in die Anlagen übernehmen
                            if (this.lstAnlagen != null)
                            {
                                foreach (boAnlage item in this.lstAnlagen)
                                {
                                    item.RoomType = this.Session.GetObjectByKey<fiRaumart>(((fiRaumart)newValue).Oid);

                                }
                            }
                        }
                    }
                    break;
                case "RoomDesignation":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (newValue != null)
                        {
                            //die bezeichnung in die Anlagen übernehmen
                            if (this.lstAnlagen != null)
                            {
                                foreach (boAnlage item in this.lstAnlagen)
                                {
                                    item.RoomDesignation = this.RoomDesignation.ToString();

                                }
                            }
                        }
                    }
                    break;
            }
        }

        public void setDefaultBezeichnung()
        {
                var system = string.Empty;
                var building = string.Empty;
            var lgNumber = string.Empty;
            var retVal = string.Empty;

            system = this.Technikeinheit.Basisanlage.Ansprechcode;
            lgNumber = this.Liegenschaft.Liegenschaftsnummer;
                 
                building = (this.Gebaeude != null) ? this.Gebaeude.Bezeichnung : string.Empty;
            if(building==string.Empty)
            {
                retVal = String.Format("{0}-{1}",
                system, lgNumber) ;
               
            }
            else
            {
                retVal = string.Format("{0}-{1}-{2}", system,lgNumber, building);
            }
            this.BezeichnungIntern = retVal;
            this.Save();
        }


        #region Properties

        #region einfache Standortimplementierung
        [XafDisplayName("Gebäude")]
        [DataSourceCriteria("[Liegenschaft.Oid]= '@this.Liegenschaft.Oid'")]
        public fiGebaeude Building
        {
            get { return _building; }
            set { SetPropertyValue("Building", ref _building, value); }
        }
        [XafDisplayName("Raumbezeichnung")]
        public String RoomDesignation
        {
            get { return _roomDesignation; }
            set { SetPropertyValue("RoomDesignation", ref _roomDesignation, value); }
        }

        [XafDisplayName("Raumart")]
        public fiRaumart RoomType
        {
            get { return _roomType; }
            set { SetPropertyValue("RoomType", ref _roomType, value); }
        }

        [XafDisplayName("Ebenenbezeichnung")]
        public String FloorDesignation
        {
            get { return _floorDesignation; }
            set { SetPropertyValue("FloorDesignation", ref _floorDesignation, value); }
        }

        [XafDisplayName("Ebenenart")]
        public fiEbenenart FloorType
        {
            get { return _floorType; }
            set { SetPropertyValue("FloorType", ref _floorType, value); }
        }
        #endregion


        [XafDisplayName("BezeichnungIntern")]
            public String BezeichnungIntern
        {
            get { return _bezeichnungIntern; }
            set { SetPropertyValue("BezeichnungIntern", ref _bezeichnungIntern, value); }
        }
        

            [XafDisplayName("KWP-Anlage")]
            public KwpWartungsAnlage KwpAnlage
        {
            get { return _kwpAnlage;}
            set { SetPropertyValue("KwpAnlage", ref _kwpAnlage, value); }
        }
                [XafDisplayName("Husverwalter")]
        public boHausverwalter Hausverwalter
        {
            get
            {
                boHausverwalter retVal;
                if (this.Liegenschaft != null)
                {

                    retVal = (this.Liegenschaft.Hausverwalter != null) ? retVal = this.Liegenschaft.Hausverwalter : null;
                }
                else
                {
                    retVal = null;
                }

                
                return retVal;
            }
        }
        [XafDisplayName("LinkKey (Fremdsystem)")]
        public fiLinkKeyHtKomponente LinkKeyFremdsystem
        {
            get
            {
                return _linkKeyFremdsystem;
            }
            set
            {
                SetPropertyValue("LinkKeyFremdsystem", ref _linkKeyFremdsystem, value);
            }
        }

        [NonPersistent]
        public boAnlagenArt BasisAnlage
        {
            get
            {
                boAnlagenArt retVal = null;
                if(this.Technikeinheit != null)
                {
                    if(this.Technikeinheit.Basisanlage != null)
                    {
                        return this.Technikeinheit.Basisanlage;
                    }
                    else
                    {
                        retVal = null;
                    }
                }
                else
                {
                    retVal = null;
                }
                return retVal;//Technikeinheit.Basisanlage;
            }
        }
        [XafDisplayName("FremdsystemId")]
        public String FremdsystemId
        {
            get
            {
                return _fremdsystemId;
            }
            set
            {
                SetPropertyValue("FremdsystemId", ref _fremdsystemId, value);
            }
        }
        
        [XafDisplayName("Vertragsstatus")]
        public enmVertragsStatus VertragsStatus
        {
            get
            {
                return _vertragsStatus;
            }
            set
            {
                SetPropertyValue("VertragsStatus", ref _vertragsStatus, value);
            }
        }
        [XafDisplayName("Notiz")]
        [Size(-1)]
        public String Notiz
        {
          get {
                return _notiz;
          }
          set {
                SetPropertyValue("Notiz", ref _notiz, value);
          }
        }

        [XafDisplayName("Gebäude")]
        //[DataSourceProperty("lstBuildings")]
        [DataSourceCriteria("[Liegenschaft.Oid]= '@this.Liegenschaft.Oid'")]
        [ImmediatePostData(true)]

        public fiGebaeude Gebaeude
        {
            get
            {
                return _gebaeude;
            }
            set
            {
                SetPropertyValue("Gebaeude", ref _gebaeude, value);
            }
        }
        [XafDisplayName("Ebene")]
        [DataSourceCriteria("[Gebaeude.Oid]= '@this.Gebaeude.Oid'")]
 
        [ImmediatePostData(true)]
        public fiEbene Ebene
        {
            get
            {
                return _ebene;
            }
            set
            {
                SetPropertyValue("Ebene", ref _ebene, value);
            }
        }
        [XafDisplayName("Raum")]
        [DataSourceCriteria("Gebaeude.Oid = '@this.Gebaeude.Oid'")]
        [ImmediatePostData(true)]
        public fiRaum Raum
        {
            get
            {
                return _raum;
            }
            set
            {
                SetPropertyValue("Raum", ref _raum, value);
            }
        }

        #region Filterproperties
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public List<fiGebaeude> lstBuildings
        {
            get
            {
                return lstAvailableBuildings();
            }
        }

        public List<fiGebaeude> lstAvailableBuildings()
        {
            List<fiGebaeude> lstRetVal = new List<fiGebaeude>();
            lstRetVal.AddRange(this.Liegenschaft.lstGebaeude);
            return lstRetVal;
        }

        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public List<fiEbene> lstFloors
        {
            get
            {
                return lstAvailableEbenen();
            }
        }

        public List<fiEbene> lstAvailableEbenen()
        {
            List<fiEbene> lstRetVal = new List<fiEbene>();
            if (this.Gebaeude != null)
            {
                lstRetVal.AddRange(this.Gebaeude.lstEbenen);
            }
            return lstRetVal;
        }
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public List<fiRaum> lstRooms
        {
            get
            {
                return lstAvailableRooms();
            }
        }

        public List<fiRaum> lstAvailableRooms()
        {
            List<fiRaum> lstRetVal = new List<fiRaum>();
            if (this.Ebene != null)
            {
                lstRetVal.AddRange(this.Ebene.lstRaeume);
            }
            return lstRetVal;
        }

        #endregion


        [XafDisplayName("Technikeinheit")]
        [ImmediatePostData(true)]
       
        public fiTechnikeinheit Technikeinheit
        {
            get
            {
                return _technikeinheit;
            }
            set
            {
                SetPropertyValue("Technikeinheit", ref _technikeinheit, value);
            }
        }

        [XafDisplayName("Systembezeichnung")]
        public System.String Systembezeichnung
        {
        get 
        {
                var retVal = string.Empty;
                if(this.Technikeinheit != null)
                {
                    retVal = this.Technikeinheit.SystemBezeichnung;
                }
                return retVal;
        }
        }
    

        [XafDisplayName("Bezeichnung")]
    
        public System.String Bezeichnung
        {
            get
            {
                return _bezeichnung;
            }
            set
            {
                SetPropertyValue("Bezeichnung", ref _bezeichnung, value);
            }
        }
        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-LGHaustechnikKomponente")]
        [ImmediatePostData(true)]
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
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public System.String Beschreibung
        {
            get
            {
                return _beschreibung;
            }
            set
            {
                SetPropertyValue("Beschreibung", ref _beschreibung, value);
            }
        }

        //Maßnahmen
        [XafDisplayName("Maßnahmen")]
        [Association("LgHaustechnikKomponente-FunctionUnitWorkItem")]
        public XPCollection<FunctionUnitWorkItem> lstFunctionUnitWorkItems
        {
            get { return GetCollection<FunctionUnitWorkItem>("lstFunctionUnitWorkItems"); }
        }

        [XafDisplayName("Anlagen")]
        [Association("LgHaustechnikKomponente-boAnlage")]
        [ImmediatePostData(true)]
        public XPCollection<boAnlage> lstAnlagen
        {
            get
            {
                return GetCollection<boAnlage>("lstAnlagen");
            }
        }

     
        public XPCollection<boAnlage> lstHauptAnlagen
        {
            get
            {
                XPCollection<boAnlage> lstResult = new XPCollection<boAnlage>(this.Session,new GroupOperator(new BinaryOperator("HaustechnikKomponente.Oid",this.Oid,BinaryOperatorType.Equal),new NullOperator("ParentAnlage")));
          
                return lstResult;
                
            }
        }

        //die Maßnahmen der Anlagen anzeigen
        [XafDisplayName("Maßnahmen")]
        public List<EquipmentWorkItem> lstEquipmentWorkItems
        {
            get
            {
                List<EquipmentWorkItem> lstRetVal = new List<EquipmentWorkItem>();
                if(this.lstAnlagen != null)
                {
                    boAnlage workingAnlage;
                    for(int i=0;i<this.lstAnlagen.Count;i++)
                    {
                        workingAnlage = (boAnlage)this.lstAnlagen[i];
                        if(workingAnlage.lstEquipmentWorkItems!= null)
                        {
                            lstRetVal.AddRange(workingAnlage.lstEquipmentWorkItems);
                        }

                    }
                }
                return lstRetVal;
               // XPCollection<actionActionAnlage> lstRetVal = new XPCollection<actionActionAnlage>(this.Session,new BinaryOperator(""))
            }
        }
        #endregion
    }
}