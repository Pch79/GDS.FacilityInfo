using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace FacilityInfo.WebService.FacilityInfo_Data
{

    public partial class boCheckEntry
    {
        public boCheckEntry(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
