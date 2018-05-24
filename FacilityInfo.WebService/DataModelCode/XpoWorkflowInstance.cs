using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace FacilityInfo.WebService.FacilityInfo_Data
{

    public partial class XpoWorkflowInstance
    {
        public XpoWorkflowInstance(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
