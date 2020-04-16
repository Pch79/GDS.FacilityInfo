using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using FacilityInfo.Management.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDS.FacilityInfo.Module.Win.Editors
{
    [PropertyEditor(typeof(byte[]), false)]
    public class Win_ZoomImageEditor : ImagePropertyEditor
    {
        private Form zoomForm = null;

        public Win_ZoomImageEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        private void ZoomImage(object sender, EventArgs e)
        {
            object current = CurrentObject;
            if (PropertyValue != null)
            {
                Image image = PictureHelper.ImageFromByteArray((byte[])PropertyValue);
                //TODO hier das Main Image anzeigen
                if (image != null)
                {

                    if (zoomForm != null)
                    {
                        // Hide image
                        zoomForm.Close();
                        zoomForm = null;
                    }
                    else
                    {
                        zoomForm = new Form()
                        {
                            FormBorderStyle = FormBorderStyle.Sizable,
                            ShowInTaskbar = false,
                            TopMost = true,
                            ShowIcon = false,
                            MinimizeBox = false,
                            Text = "Click anywhere on image to close",
                            BackgroundImage = image,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            BackColor = Color.White,
                            // ClientSize = image.Size,
                            ClientSize = new Size(1024,768),
                            
                            StartPosition = FormStartPosition.CenterScreen
                        };
                       // zoomForm.AutoSize = true;                    
                        zoomForm.FormClosed += ZoomImage;
                        zoomForm.Click += ZoomImage;
                        zoomForm.Show();
                    }
                }
            }
        }

        protected override object CreateControlCore()
        {
            object control = base.CreateControlCore();
            if (control is XafPictureEdit)
            {
                ((XafPictureEdit)control).Click += ZoomImage;
            }
            return control;
        }
    }
}
