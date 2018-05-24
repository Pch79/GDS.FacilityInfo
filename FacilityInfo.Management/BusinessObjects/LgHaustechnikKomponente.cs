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
using FacilityInfo.Service.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Management.EnumStore;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using FacilityInfo.Management.Klassen;

namespace FacilityInfo.Anlagen.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlagensystem")]
    [XafDefaultProperty("Systembezeichnung")]
    [ImageName("control_panel_16")]
    public class LgHaustechnikKomponente : BaseObject//,ITreeNode, ITreeNodeImageProvider
    {
        private System.String _bezeichnung;
        private boLiegenschaft _liegenschaft;
        private System.String _beschreibung;
        private String _notiz;
        private fiTechnikeinheit _technikeinheit;

        //Standort
        private fiGebaeude _gebaeude;
        private fiEbene _ebene;
        private fiRaum _raum;

        //Kopplung an KWP
        private String _fremdsystemId;
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


                        }
                        else
                        {
                            this.LinkKeyFremdsystem = null;
                        }
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
                    }
                    break;
                //Raum
                case "Ebene":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (newValue != null)
                        {
                            //die bezeichnung in die Anlagen übernehmen
                            if (this.lstAnlagen != null)
                            {
                                foreach (boAnlage item in this.lstAnlagen)
                                {
                                    item.Ebene = this.Session.GetObjectByKey<fiEbene>(((fiEbene)newValue).Oid);

                                }
                            }
                        }
                    }
                    break;

                //Raum
                case "Raum":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (newValue != null)
                        {
                            //die bezeichnung in die Anlagen übernehmen
                            if (this.lstAnlagen != null)
                            {
                                foreach (boAnlage item in this.lstAnlagen)
                                {
                                    item.Raum = this.Session.GetObjectByKey<fiRaum>(((fiRaum)newValue).Oid);

                                }
                            }
                        }
                    }
                    break;                   
            }
        }
        /*

        #region iTreeNode
        ITreeNode ITreeNode.Parent
        {
            get
            {
                return null;
            }
        }
        string ITreeNode.Name
        {
            get
            {
                return Systembezeichnung;
            }
        }

        IBindingList ITreeNode.Children
        {
            get
            {
                return lstHauptAnlagen;
            }
        }


        #endregion
        #region iTreeImageProvider
        public System.Drawing.Image GetImage(out string imageName)
        {
            if (lstAnlagen != null && lstAnlagen.Count > 0)
            {
                imageName = "control_panel_branding_16";

            }
            else
            {
                imageName = "control_panel_16";

            }
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }
        #endregion
        */
        #region Properties


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
        [DataSourceCriteria("Liegenschaft.Oid='@this.Liegenschaft.Oid'")]
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
        [DataSourceCriteria("Gebaeude.Oid='@this.Gebaeude.Oid'")]
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
        [DataSourceCriteria("Gebaeude.Oid='@this.Gebaeude.Oid'")]
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
        [XafDisplayName("Servces")]
        [Association("LgHaustechnikKomponente-serviceKomponentenService")]
        public XPCollection<serviceKomponentenService> lstServices
        {
            get
            {
                return GetCollection<serviceKomponentenService>("lstServices");
            }
        }
        #endregion
    }
}