using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using FacilityInfo.Artikelverwaltung.BusinessObjects;
using FacilityInfo.Hersteller.BusinessObjects;
using FacilityInfo.Management.BusinessObjects.TechnicalInstallation;

namespace FacilityInfo.Management.BusinessObjects.ArticleHandling
{
    


    [DefaultClassOptions]
    [XafDisplayName("Component-Part")]
    [ImageName("box_16")]
    [XafDefaultProperty("KurzText")]
    public class ComponentPart : artikelArtikelBase
    {
    
        private boHersteller _hersteller;
        private System.String _bauteilkennung;

        private string _bauteilOid;
        private fiBauteilkatalog _bauteil;

        public ComponentPart(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        [Action(AutoCommit =false,Caption ="Bauteil-Import",ConfirmationMessage ="Import done",SelectionDependencyType =MethodActionSelectionDependencyType.RequireSingleObject)]
        public void ImportBauteilExecute()
        {
            XPCollection<fiBauteil> lstBauteile = new XPCollection<fiBauteil>(this.Session);
            foreach(var item in lstBauteile)
            {
                ComponentPart workingPart = this.Session.FindObject<ComponentPart>(new BinaryOperator("BauteilOid", item.Oid, BinaryOperatorType.Equal));
                if(workingPart != null)
                {
                    workingPart.Bild = item.Bild;
                    workingPart.Hersteller = this.Session.GetObjectByKey<boHersteller>(item.Hersteller.Oid);
                    workingPart.HerstellerNummer = item.herstellernummer;
                    workingPart.Bauteil = this.Session.GetObjectByKey<fiBauteilkatalog>(item.Bauteilkatalog.Oid);
                    workingPart.KurzText = item.Bauteilkennung;
                    workingPart.Bauteilkennung = item.Bauteilkennung;
                    workingPart.Langtext = item.Beschreibung;
                    workingPart.BauteilOid = item.Oid.ToString();
                    workingPart.Save();
                    if(item.lstHerstellerprodukte != null && item.lstHerstellerprodukte.Count>0)
                    {
                        foreach(var componentItem in item.lstHerstellerprodukte)
                        {
                            fiHerstellerProdukt curComponent = this.Session.GetObjectByKey<fiHerstellerProdukt>(componentItem.Oid);
                            workingPart.lstHerstellerprodukte.Add(curComponent);
                            workingPart.Save();
                        }
                    }

                }
                else
                {
                    workingPart = new ComponentPart(this.Session);
                    workingPart.Bild = item.Bild;
                    workingPart.Hersteller = this.Session.GetObjectByKey<boHersteller>(item.Hersteller.Oid);
                    workingPart.HerstellerNummer = item.herstellernummer;
                    workingPart.Bauteil = this.Session.GetObjectByKey<fiBauteilkatalog>(item.Bauteilkatalog.Oid);
                    workingPart.KurzText = item.Bauteilkennung;
                    workingPart.Bauteilkennung = item.Bauteilkennung;
                    workingPart.Langtext = item.Beschreibung;
                    workingPart.BauteilOid = item.Oid.ToString();
                    workingPart.Save();
                }
            }
            this.Session.CommitTransaction();
        }

        [XafDisplayName("Bauteil")]
        public fiBauteilkatalog Bauteil
        {
            get
            {
                return _bauteil;
            }
            set
            {
                SetPropertyValue("Bauteil", ref _bauteil, value);
            }
        }

        [XafDisplayName("Produkte")]
        [Association("fiHerstellerProdukt-ComponentPart")]
        public XPCollection<fiHerstellerProdukt> lstHerstellerprodukte
        {
            get
            {
                return GetCollection<fiHerstellerProdukt>("lstHerstellerprodukte");
            }
        }

        [Association("TechnicalAssembly-ComponentPart")]
        public XPCollection<TechnicalAssembly> lstTechnicalAssemblys
        {
            get { return GetCollection<TechnicalAssembly>("lstTechnicalAssemblys"); }
        }

        [XafDisplayName("Bauteil-Oid")]
        public string BauteilOid
        {
            get { return this._bauteilOid; }
            set { SetPropertyValue("BauteilOid", ref _bauteilOid, value); }
        }

        [XafDisplayName("Hersteller")]
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

        [XafDisplayName("Bauteilkennung")]
        public System.String Bauteilkennung
        {
            get
            {
                return _bauteilkennung;
            }
            set
            {
                SetPropertyValue("Bauteilkennung", ref _bauteilkennung, value);
            }
        }
    }

}