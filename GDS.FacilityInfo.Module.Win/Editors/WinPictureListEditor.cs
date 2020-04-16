using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win.Controls;
using DevExpress.Utils.Menu;
using FacilityInfo.Management.DomainComponents;

namespace GDS.FacilityInfo.Module.Win.Editors
{

    [ListEditor(typeof(IpictureItem))]
    public class WinPictureListEditor :ListEditor, IDXPopupMenuHolder, IControlOrderProvider
    {
        private ActionsDXPopupMenu popupMenu;
        private System.Windows.Forms.ListView control;
        private System.Windows.Forms.ImageList images;
        private Object controlDataSource;
        private void dataSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            Refresh();
        }
        private void control_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OnProcessSelectedItem();
            }
        }
        private void control_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnProcessSelectedItem();
            }
        }
        private void control_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            OnSelectionChanged();
        }
        private void control_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectionChanged();
            OnFocusedObjectChanged();
        }
        private System.Windows.Forms.ListViewItem FindByTag(object tag)
        {
            IpictureItem itemToSearch = (IpictureItem)tag;
            if (control != null && itemToSearch != null)
            {
                foreach (System.Windows.Forms.ListViewItem item in control.Items)
                {
                    if (((IpictureItem)item.Tag).id == itemToSearch.id)
                        return item;
                }
            }
            return null;
        }
        protected override object CreateControlsCore()
        {
            control = new System.Windows.Forms.ListView();
            control.Sorting = SortOrder.Ascending;
            images = new System.Windows.Forms.ImageList();
            images.ImageSize = new System.Drawing.Size(100, 100);

            images.ColorDepth = ColorDepth.Depth32Bit;
            control.LargeImageList = images;
            control.HideSelection = false;
            control.SelectedIndexChanged += new EventHandler(control_SelectedIndexChanged);
            control.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(control_ItemSelectionChanged);
            control.MouseDoubleClick += new MouseEventHandler(control_MouseDoubleClick);
            control.KeyDown += new System.Windows.Forms.KeyEventHandler(control_KeyDown);
            Refresh();
            return control;
        }
        protected override void AssignDataSourceToControl(Object dataSource)
        {
            if (dataSource is DevExpress.Xpo.XPServerCollectionSource)
            {
                throw new Exception("The WinCustomListEditor doesn't support Server Mode and so cannot use an XPServerCollectionSource object as the data source.");
            }
            if (controlDataSource != dataSource)
            {
                IBindingList oldBindable = controlDataSource as IBindingList;
                if (oldBindable != null)
                {
                    oldBindable.ListChanged -= new ListChangedEventHandler(dataSource_ListChanged);
                }
                controlDataSource = dataSource;
                IBindingList bindable = controlDataSource as IBindingList;
                if (bindable != null)
                {
                    bindable.ListChanged += new ListChangedEventHandler(dataSource_ListChanged);
                }
                Refresh();
            }
        }
        public WinPictureListEditor(IModelListView info)
            : base(info)
        {
            popupMenu = new ActionsDXPopupMenu();
        }
        public override void Dispose()
        {
            controlDataSource = null;
            if (popupMenu != null)
            {
                popupMenu.Dispose();
                popupMenu = null;
            }
            base.Dispose();
        }
        public override void Refresh()
        {
            if (control == null)
                return;
            object focused = FocusedObject;
            control.SelectedItems.Clear();
            try
            {
                control.BeginUpdate();
                images.Images.Clear();
                control.Items.Clear();
                if (ListHelper.GetList(controlDataSource) != null)
                {
                    // images.Images.Add(ImageLoader.Instance.GetImageInfo("NoImage").Image);
                    foreach (IpictureItem item in ListHelper.GetList(controlDataSource))
                    {
                        int imageIndex = 0;
                        if (item.bild != null)
                        {
                            images.Images.Add(item.bild);
                            imageIndex = images.Images.Count - 1;
                        }
                        System.Windows.Forms.ListViewItem lItem =
                            new System.Windows.Forms.ListViewItem(item.bildtitel, imageIndex);
                        lItem.Tag = item;
                        lItem.ToolTipText = item.beschreibung;
                        control.Items.Add(lItem);
                    }
                }
            }
            finally
            {
                control.EndUpdate();
            }

            FocusedObject = focused;
            if (FocusedObject == null && control.Items.Count > 1)
            {
                FocusedObject = control.Items[0].Tag;
            }
        }
        public override IList GetSelectedObjects()
        {
            if (control == null)
                return new object[0] { };

            object[] result = new object[control.SelectedItems.Count];
            for (int i = 0; i < control.SelectedItems.Count; i++)
            {
                result[i] = control.SelectedItems[i].Tag;
            }
            return result;
        }
        public override void SaveModel()
        {
        }
        public override SelectionType SelectionType
        {
            get { return SelectionType.Full; }
        }
        public override object FocusedObject
        {
            get
            {
                return (control != null) && (control.FocusedItem != null) ? control.FocusedItem.Tag : null;
            }
            set
            {
                System.Windows.Forms.ListViewItem item = FindByTag(value);
                if (item != null)
                {
                    control.SelectedItems.Clear();

                    item.Focused = true;
                    item.Selected = true;
                }
            }
        }
        public override IContextMenuTemplate ContextMenuTemplate
        {
            get { return popupMenu; }
        }
        #region IDXPopupMenuHolder Members

        public bool CanShowPopupMenu(System.Drawing.Point position)
        {
            Point clientPosition = control.PointToClient(position);
            return clientPosition != Point.Empty;
        }
        public void SetMenuManager(IDXMenuManager manager) { }
        public Control PopupSite
        {
            get { return control; }
        }

        #endregion

        #region IControlOrderProvider Members
        public int GetIndexByObject(Object obj)
        {
            int index = -1;
            IpictureItem itemToSearch = (IpictureItem)obj;
            if (control != null)
            {
                for (int i = 0; i < control.Items.Count; i++)
                {
                    if (((IpictureItem)control.Items[i].Tag).id == itemToSearch.id)
                        return i;
                }
            }
            return index;
        }

        public Object GetObjectByIndex(int index)
        {
            if (control != null && control.Items.Count > index)
            {
                return control.Items[index].Tag;
            }
            return null;
        }
        public IList GetOrderedObjects()
        {
            List<Object> list = new List<Object>();
            if (control != null)
            {
                for (int i = 0; i < control.Items.Count; i++)
                {
                    list.Add(control.Items[i].Tag);
                }
            }
            return list;
        }

        #endregion
    }

}
