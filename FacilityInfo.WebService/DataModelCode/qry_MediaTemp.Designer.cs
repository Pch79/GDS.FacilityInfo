﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace FacilityInfo.WebService.FacilityInfo_Data
{

    [NonPersistent]
    public partial class qry_MediaTemp : XPLiteObject
    {
        byte[] fMediaData;
        [Size(SizeAttribute.Unlimited)]
        [MemberDesignTimeVisibility(true)]
        public byte[] MediaData
        {
            get { return fMediaData; }
            set { SetPropertyValue<byte[]>("MediaData", ref fMediaData, value); }
        }
        Guid fLiegenschaft;
        public Guid Liegenschaft
        {
            get { return fLiegenschaft; }
            set { SetPropertyValue<Guid>("Liegenschaft", ref fLiegenschaft, value); }
        }
    }

}
