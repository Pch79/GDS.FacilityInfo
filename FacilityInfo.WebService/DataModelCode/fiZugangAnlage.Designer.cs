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

    public partial class fiZugangAnlage : fiZugang
    {
        boAnlage fAnlage;
        [Association(@"fiZugangAnlageReferencesboAnlage")]
        public boAnlage Anlage
        {
            get { return fAnlage; }
            set { SetPropertyValue<boAnlage>("Anlage", ref fAnlage, value); }
        }
    }

}
