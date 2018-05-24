using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using FacilityInfo.Liegenschaft.BusinessObjects;
using DevExpress.Xpo;

namespace GDS.FacilityInfo.Win.UserControls
{
    public partial class ucLiegenschaft_ListView : DevExpress.XtraEditors.XtraUserControl
    {
        public ucLiegenschaft_ListView()
        {
            InitializeComponent();
            Session fiSession = XpoHelper.GetNewSession();
            this.gridControl1.DataSource = new XPCollection<boLiegenschaft>(fiSession);

        }

        //die Bindung manuell erstellen
        
       
            /*
        private IObjectSpace objectSpace;
       
        void IComplexControl.Setup(IObjectSpace objectSpace, XafApplication application)
        {
            gridControl1.DataSource = objectSpace.GetObjects<boLiegenschaft>();
            this.objectSpace = objectSpace;
        }
        void IComplexControl.Refresh()
        {
           gridControl1.DataSource = objectSpace.GetObjects<boLiegenschaft>();

        }
        */
    }
}
