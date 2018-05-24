using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.Management.Klassen
{
    public abstract class Category : BaseObject,ITreeNode
    {
        private string name;
        protected abstract ITreeNode Parent { get; }
        protected abstract IBindingList Children { get; }
        public Category(Session session) : base(session) { }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetPropertyValue("Name", ref name, value);
            }
        }
        #region ITreeNode
        IBindingList ITreeNode.Children
        {
            get
            {
                return Children;
            }
        }
        string ITreeNode.Name
        {
            get
            {
                return Name;
            }
        }
        ITreeNode ITreeNode.Parent
        {
            get
            {
                return Parent;
            }
        }
        #endregion
    }
}
