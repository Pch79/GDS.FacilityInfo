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
using FacilityInfo.GlobalObjects.DomainComponents;
using System.Drawing;
using FacilityInfo.GlobalObjects.Helpers;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Base.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;

namespace FacilityInfo.Bildverarbeitung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Liegenschaftsbild")]
    [ImageName("picture")]
    [Serializable]
    public class boLiegenschaftsBild : BaseObject,IpictureItem
    {
        #region Fields
        private System.String _beschreibung;
        private System.String _bildtitel;
        private fiBildtitel _titel;
       // private Image _bild;
        private System.String _id;
        private boLiegenschaft _liegenschaft;
        private System.Int32 _sortPosition;
        // private Image _originalBild;
        private BildKategorie _bildKategorie;


        [XafDisplayName("Bildkategorie")]
        public BildKategorie BildKategorie
        {
            get
            {
                return _bildKategorie;
            }
            set
            {
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
        [Size(SizeAttribute.Unlimited),ImageEditor]
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
             /*
        [XafDisplayName("Originalbild")]
        [Size(SizeAttribute.Unlimited),
  //  ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        public Image Originalbild
        {
            get
            {
                return _originalBild;
            }
            set
            {
                SetPropertyValue("Originalbild", ref _originalBild, value);
            }
        }
        */

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

        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-boLiegenschaftsBild")]
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
        [XafDisplayName("Bild")]
        [Size(SizeAttribute.Unlimited),ImageEditor]
        [ImmediatePostData(true)]
        public byte[] Bild
        {
            get
            {
                return GetPropertyValue<byte[]>("Bild");
            }
            set
            {
                SetPropertyValue<byte[]>("Bild", value);
            }
        }
        /*
        [XafDisplayName("Bild")]
        [Size(SizeAttribute.Unlimited),
    ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        public Image Bild
        {
            get
            {
                return _bild;
            }
            set
            {
              
                SetPropertyValue("Bild", ref _bild, value);

               
            }
        }
        */
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
        public boLiegenschaftsBild(Session session)
            : base(session)
        {
        }

       
        #region Methoden
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            //wenn sich das Bild ändert gliech das originalbild 
            
            switch(propertyName)
            {
                case "Bild":
                    /*
                   if (newValue != null)
                   {

                       Bitmap addedImage = PictureHelper.byteArrayToBitmap((byte[])newValue);


                       Bitmap copy = new Bitmap(addedImage.Width, addedImage.Height);

                       using (Graphics gr = Graphics.FromImage(copy))
                       {
                           Rectangle myRec = new Rectangle(0, 0, copy.Width, copy.Height);
                           gr.DrawImage(addedImage, myRec, myRec, GraphicsUnit.Pixel);
                       }
                       this.Originalbild = PictureHelper.imageToByteArray(copy); 
                       // this.Save();
                   }

                   else
                   {
                       this.Originalbild = null;
                       this.Bild = null;
                   }
                   */
                    break;
            }
        }

        private void setWatermark()
        {
            
            // und dann die Watermark setzen
            if (this.Liegenschaft.Mandant != null)
            {
                //das Wasserzeichen für den Mandanten suchen
                boWatermark curWatermark = this.Session.GetObjectByKey<boWatermark>(this.Liegenschaft.Mandant.Wasserzeichen.Oid);
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
                        try {
                            setWatermark();
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                }
            }
                       
                    }
        #endregion

        #region Properties
        #endregion

    }
}