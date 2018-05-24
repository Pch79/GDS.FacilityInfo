using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Windows.Forms;

namespace GDS.FacilityInfo.Module.Win.Editors
{
    [PropertyEditor(typeof(string), "FolderBrowseEditor", false)]
    public class XafFolderBrowserDialog : DXPropertyEditor
    {
        public XafFolderBrowserDialog(Type objectType, IModelMemberViewItem model)
                : base(objectType, model) { }

        private void buttonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Verzeichnis auswählen...";
                if (dialog.ShowDialog() != DialogResult.Cancel)
                {
                    ((ButtonEdit)sender).Text = dialog.SelectedPath;
                }
            }
        }

        protected override object CreateControlCore()
        {
            return new ButtonEdit();
        }
        protected override RepositoryItem CreateRepositoryItem()
        {
            return new RepositoryItemButtonEdit();
        }
        protected override void SetupRepositoryItem(RepositoryItem item)
        {
            base.SetupRepositoryItem(item);
            ((RepositoryItemButtonEdit)item).ButtonClick += buttonEdit_ButtonClick;
        }
        protected override void SetRepositoryItemReadOnly(RepositoryItem item, bool readOnly)
        {
            base.SetRepositoryItemReadOnly(item, readOnly);
            ((RepositoryItemButtonEdit)item).Buttons[0].Enabled = !readOnly;
        }
    }
}
