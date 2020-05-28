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

using System.Drawing;
using FacilityInfo.Anlagen.BusinessObjects;

using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Base.BusinessObjects;
using FacilityInfo.Management.DomainComponents;
using FacilityInfo.Management.Helpers;

namespace FacilityInfo.Bildverarbeitung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlagenbild")]
    [ImageName("picture")]
    [Serializable]
    public class boAnlagenBild : BaseObject, IpictureItem
    {
        private System.String _beschreibung;
        private fiBildtitel _titel;
        private System.String _bildtitel;
        //private Image _bild;
        //private System.String _id;
        private boAnlage _anlage;
        private System.Int32 _sortPosition;
        private BildKategorie _bildKategorie;
        public boAnlagenBild(Session session)
            : base(session)
        {
        }

        #region Methoden
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if (!this.IsDeleted)
            {
                if (!this.Session.IsObjectToDelete(this))
                {

                    //bevor ich irgendwas mit QWasserzeichen mache erst mal eine Kopie des Bildes abspeicechern
                    saveImageCopy();

                    //das Bild mit Wasserzeichen versehen
                    if (this.Originalbild != null)
                    {
                        try
                        {


                            setWatermark();
                        }
                        catch(Exception ex)
                        { }
                    }
                }
            }
        }
              private void setWatermark()
        {

            // und dann die Watermark setzen
            if (this.Anlage.Mandant != null)
            {
                //das Wasserzeichen für den Mandanten suchen
                boWatermark curWatermark = this.Session.GetObjectByKey<boWatermark>(this.Anlage.Mandant.Wasserzeichen.Oid);
                if (curWatermark != null)
                {
                    //die Einstellungen auslesen
                    //1. Wo wird Positioniert
                    //das Bild an sich
                    System.Drawing.Image curWaterMarkImage = PictureHelper.byteArrayToImage(curWatermark.Wasserzeichen);
                    System.Drawing.Image curImage = PictureHelper.byteArrayToImage(this.Originalbild);
                    this.Bild = PictureHelper.imageToByteArray(PictureHelper.SetWaterMark(curImage, curWaterMarkImage, curWatermark.Vertical, curWatermark.Horizontal, curWatermark.Breite, curWatermark.Hoehe));
                }

            }
        }

        private void saveImageCopy()
        {
            Bitmap addedImage = PictureHelper.byteArrayToBitmap(this.Bild);


            Bitmap copy = new Bitmap(addedImage.Width, addedImage.Height);

            using (Graphics gr = Graphics.FromImage(copy))
            {
                Rectangle myRec = new Rectangle(0, 0, copy.Width, copy.Height);
                gr.DrawImage(addedImage, myRec, myRec, GraphicsUnit.Pixel);
            }
            this.Originalbild = PictureHelper.imageToByteArray(copy);
            this.Save();

        }
    

        #endregion
        #region Properties
        [XafDisplayName("Bildkategorie")]
        public BildKategorie BildKategorie
        {
          get {
                return _bildKategorie;
          }
          set {
                SetPropertyValue("BildKategorie", ref _bildKategorie, value);
          }
        }
        [XafDisplayName("Titel")]
        public fiBildtitel Titel
        {
            get
            {
                return _titel;
            }
            set
            {
                SetPropertyValue("Titel", ref _titel, value);
            }
        }

        [XafDisplayName("Originalbild")]
        [Size(SizeAttribute.Unlimited), ImageEditor]
        public byte[] Originalbild
        {
            get
            {
                return GetPropertyValue<byte[]>("Originalbild");
            }
            set
            {
                SetPropertyValue<byte[]>("Originalbild", value);
            }
        }

        [XafDisplayName("Bild")]
        [Size(SizeAttribute.Unlimited),ImageEditor]
        public byte[] Bild
        {
            get
            {
                return GetPropertyValue<byte[]>("Bild");
            }
            set
            {
                SetPropertyValue("Bild", value);
            }
        }


        [XafDisplayName("Anzeigeposition")]
        public System.Int32 SortPosition
        {
            get
            {
                return _sortPosition;
            }
            set
            {
                SetPropertyValue("SortPosition", ref _sortPosition, value);
            }
        }
        [XafDisplayName("Anlage")]
        [Association("boAnlage-boAnlagenBild")]
        public boAnlage Anlage
        {
            get
            {
                return _anlage;
            }
            set
            {
                SetPropertyValue("Anlage", ref _anlage, value);
            }
        }
        [XafDisplayName("Bildtitel")]
        public System.String Bildtitel
        {
            get
            {
                return _bildtitel;
            }
            set
            {
                SetPropertyValue("Bildtitel", ref _bildtitel, value);
            }
        }
        [XafDisplayName("Beschreibung")]
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
        
        string IpictureItem.bildtitel
        {
            get
            {
                return Bildtitel;
            }
        }

        string IpictureItem.beschreibung
        {
            get
            {
                return Beschreibung;
            }
        }

        string IpictureItem.id
        {
            get
            {
                return Oid.ToString();
            }
        }

        Image IpictureItem.bild
        {
            get
            {
                return PictureHelper.byteArrayToImage(Bild);
            }
        }
        #endregion
        
    }
}