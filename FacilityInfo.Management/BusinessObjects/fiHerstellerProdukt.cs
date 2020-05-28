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
using FacilityInfo.DMS.BusinessObjects;

using FacilityInfo.Parameter.BusinessObjects;
using FacilityInfo.Anlagen.BusinessObjects;
using System.Drawing;
using FacilityInfo.Management.Helpers;
using FacilityInfo.Wartung.BusinessObjects;
using FacilityInfo.Artikelverwaltung.BusinessObjects;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Management.BusinessObjects.ArticleHandling;

namespace FacilityInfo.Hersteller.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlagenkomponenten")]
    [XafDefaultProperty("Bezeichnung")]
    public class fiHerstellerProdukt : artikelArtikelBase
    {
        private boHersteller _hersteller;
        private System.String _typbezeichnung;
        private System.String _modellbezeichnung;
        private System.String _produktbeschreibung;
        private fiHerstellerProduktgruppe _produktgruppe;
        private System.String _seriennummer;

        public fiHerstellerProdukt(Session session)
            : base(session)
        {
        }

        #region Methoden
        protected override void OnLoaded()
        {
            base.OnLoaded();

            if (this.Produktgruppe != null)
            {
                generateParamList(this.Produktgruppe);
            }
            if (this.Produktbild != null)
            {
                if (this.MainImageThumb == null)
                {
                    setMainThumbnail();
                }
                if (this.MainImageWeb == null)
                {
                    setMainImageWeb();
                }
            }
            if(this.KurzText == null || this.KurzText == string.Empty)
            {
                this.KurzText = this.Bezeichnung;
                this.Save();
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            //wenn sich die Gruppe ändert müssen die Felder neu geschrieben werden
            switch (propertyName)
            {
                case "Produktgruppe":

                    //die Felder des tps auslesn und hzuordnen
                    if (newValue != null)
                    {
                        if (this.lstProduktParameter != null)
                        {
                            this.Session.Delete(this.lstProduktParameter);
                            this.Save();
                            this.Session.CommitTransaction();
                        }

                        generateParamList((fiHerstellerProduktgruppe)newValue);

                    }
                    else
                    {
                        if (this.lstProduktParameter != null)
                        {
                            this.Session.Delete(this.lstProduktParameter);
                            this.Save();
                            this.Session.CommitTransaction();
                        }
                    }
                    break;
                case "Produktbild":
                    if (!this.IsLoading)
                    {
                        if (newValue != null)
                        {
                            setMainThumbnail();
                            //hier kan ich auch gleich die Web-Implementierung erstellen
                            setMainImageWeb();
                        }
                        if (newValue == null)
                        {
                            this.MainImageThumb = null;
                            this.MainImageWeb = null;
                        }
                    }
                    break;
            }
        }

        public void makeMainThumbnail()
        {
            setMainThumbnail();
            setMainImageWeb();
        }
        public void setMainThumbnail()
        {
            if (this.Produktbild != null)
            {
                this.MainImageThumb = PictureHelper.getThumbnailByteArray(this.Produktbild);
                this.Save();
                this.Session.CommitTransaction();
            }
        }

        public void setMainImageWeb()
        {
            if (this.Produktbild != null)
            {
                Image workingImage = PictureHelper.ImageFromByteArray(this.Produktbild);
                this.MainImageWeb = PictureHelper.ResizePicByWidth(workingImage, 300);
                this.Save();
                this.Session.CommitTransaction();
            }
        }

        public void generateParamList(fiHerstellerProduktgruppe curGruppe)
        {
            if (curGruppe.lstParameterProduktGruppe != null && curGruppe.lstParameterProduktGruppe.Count > 0)
            {
                for (int i = 0; i < curGruppe.lstParameterProduktGruppe.Count; i++)
                {
                    parameterProduktGruppeParam paramToAdd = this.Session.GetObjectByKey<parameterProduktGruppeParam>(curGruppe.lstParameterProduktGruppe[i].Oid);
                    if (this.lstProduktParameter.Where(t => t.ParameterItem.ParamKey == paramToAdd.ParameterDefinition.ParamKey).Count() <= 0)
                    {
                        parameterHerstellerProduktParameter curParam = new parameterHerstellerProduktParameter(this.Session);
                        curParam.ParameterItem = paramToAdd.ParameterDefinition;
                        curParam.DefaultValue = paramToAdd.SollWert;
                        curParam.Save();
                        this.lstProduktParameter.Add(curParam);
                        this.Save();
                        this.Session.CommitTransaction();
                    }
                }
            }
        }
        #endregion

        #region Properties


        [XafDisplayName("Seriennummer")]
        public System.String Seriennummer
        {

            get
            {
                return _seriennummer;
            }
            set
            {
                SetPropertyValue("Seriennummer", ref _seriennummer, value);
            }
        }


        [XafDisplayName("Modellbezeichnung")]
        public System.String Modellbezeichnung
        {
            get
            {
                return _modellbezeichnung;
            }
            set
            {
                SetPropertyValue("Modellbezeichnung", ref _modellbezeichnung, value);
            }
        }

        [XafDisplayName("Produktgruppe")]
        [ImmediatePostData]
        public fiHerstellerProduktgruppe Produktgruppe
        {
            get
            {
                return _produktgruppe;
            }
            set
            {
                SetPropertyValue("Produktgruppe", ref _produktgruppe, value);
            }
        }
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public System.String Beschreibung
        {
            get
            {
                return _produktbeschreibung;
            }
            set
            {
                SetPropertyValue("Beschreibung", ref _produktbeschreibung, value);
            }
        }
        [XafDisplayName("Bezeichnung")]
        public System.String Bezeichnung
        {
            get
            {
                return _typbezeichnung;
            }
            set
            {
                SetPropertyValue("Bezeichnung", ref _typbezeichnung, value);
            }
        }


        [XafDisplayName("Hersteller")]
        [Association("fiHersteller-fiHerstellerProdukt")]
        public boHersteller Hersteller
        {
            get
            {
                return _hersteller;
            }
            set
            {
                SetPropertyValue("Hersteller", ref _hersteller, value);
            }
        }

        [XafDisplayName("Produktbild")]
      
        [Delayed]
        public byte[] Produktbild
        {
            get
            {
                return GetDelayedPropertyValue<byte[]>("Produktbild");
            }
            set
            {
                SetDelayedPropertyValue<byte[]>("Produktbild", value);
            }
        }

        //Thumbnail und Image Web einbauen
        [XafDisplayName("Vorschaubild")]
        public byte[] MainImageThumb
        {
            get
            {
                return GetPropertyValue<byte[]>("MainImageThumb");
            }
            set { SetPropertyValue<byte[]>("MainImageThumb", value); }


        }
        [XafDisplayName("Titelbild (Web)")]
        public byte[] MainImageWeb
        {
            get
            {

                return GetPropertyValue<byte[]>("MainImageWeb");
            }
            set
            {
                SetPropertyValue<byte[]>("MainImageWeb", value);
            }
        }

        [XafDisplayName("Produktdokumente")]
        [Association("fiHerstellerProdukt-fiHerstellerProduktAttachment"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<fiHerstellerProduktAttachment> lstProduktDokumente
        {
            get
            {
                return GetCollection<fiHerstellerProduktAttachment>("lstProduktDokumente");
            }
        }

        [XafDisplayName("Bauteile")]
        [Association("fiHerstellerProdukt-fiBauteil")]
        public XPCollection<fiBauteil> lstBauteile
        {
            get
            {
                return GetCollection<fiBauteil>("lstBauteile");
            }
        }


        [XafDisplayName("Component-Parts")]
        [Association("fiHerstellerProdukt-ComponentPart")]
        public XPCollection<ComponentPart> lstComponentParts
        {
            get
            {
                return GetCollection<ComponentPart>("lstComponentParts");
            }
        }


        //Baugruppen
        [XafDisplayName("Baugruppen")]
        [Association("anlageAnlagenBaugruppe-fiHerstellerProdukt")]
        public XPCollection<anlageAnlagenbaugruppe> lstAnlagenBaugruppe
        {
            get { return GetCollection<anlageAnlagenbaugruppe>("lstAnlagenBaugruppe"); }
        }
     

        [Association("fiHerstellerProdukt-parameterHerstellerProduktParameter"),DevExpress.Xpo.Aggregated]
        [XafDisplayName("Produktparameter")]
        public XPCollection<parameterHerstellerProduktParameter> lstProduktParameter
        {
            get
            {
                return GetCollection<parameterHerstellerProduktParameter>("lstProduktParameter");
            }
        }

        //Wartungspaln einbauen
        //Wartungsplan
        //hat Wartungspunkte und er hat mehrere Bauteile

        [XafDisplayName("Wartungspläne")]
        [Association("fiHerstellerProdukt-wartungWartungsPlanProdukt")]
        public XPCollection<wartungWartungsPlanProdukt> lstWartungsPlan
        {
            get { return GetCollection<wartungWartungsPlanProdukt>("lstWartungsPlan"); }
        }
        #endregion

        #region Methoden
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            artikelArtikelKatalog chosenKatalog = null;
            boMandant chosenMandant = getCurrentMandant();
           if(chosenMandant!=null)
            {
                chosenKatalog = this.Session.FindObject<artikelArtikelKatalog>(new GroupOperator(new BinaryOperator("Mandant.Oid", chosenMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("Bezeichnung", "Anlagenartikel", BinaryOperatorType.Equal)));
                if(chosenKatalog != null)
                {
                    this.ArtikelKatalog = chosenKatalog;
                }
            }

           
        }
        private boMandant getCurrentMandant()
        {
            boMitarbeiter curMitarbeiter = null;
            boMandant curMandant = null;
            PermissionPolicyUser curUser = (PermissionPolicyUser)SecuritySystem.CurrentUser;
            //gibt es einen Mitarbeiter dazu?
            curMitarbeiter = this.Session.FindObject<boMitarbeiter>(new BinaryOperator("Systembenutzer.Oid", curUser.Oid, BinaryOperatorType.Equal));

            if (curMitarbeiter != null)
            {
                if (curMitarbeiter.Mandant != null)
                {


                    curMandant = this.Session.GetObjectByKey<boMandant>(curMitarbeiter.Mandant.Oid);

                }



            }
            return curMandant;
        }


    #endregion
}
}