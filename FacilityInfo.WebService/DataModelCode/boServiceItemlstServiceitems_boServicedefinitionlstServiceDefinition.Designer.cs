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

    public partial class boServiceItemlstServiceitems_boServicedefinitionlstServiceDefinition : XPBaseObject
    {
        boServicedefinition flstServiceDefinition;
        [Indexed(@"lstServiceitems", Name = @"ilstServiceDefinitionlstServiceitems_boServiceItemlstServiceitems_boServicedefinitionlstServiceDefinition", Unique = true)]
        [Association(@"boServiceItemlstServiceitems_boServicedefinitionlstServiceDefinitionReferencesboServicedefinition")]
        public boServicedefinition lstServiceDefinition
        {
            get { return flstServiceDefinition; }
            set { SetPropertyValue<boServicedefinition>("lstServiceDefinition", ref flstServiceDefinition, value); }
        }
        boServiceItem flstServiceitems;
        [Association(@"boServiceItemlstServiceitems_boServicedefinitionlstServiceDefinitionReferencesboServiceItem")]
        public boServiceItem lstServiceitems
        {
            get { return flstServiceitems; }
            set { SetPropertyValue<boServiceItem>("lstServiceitems", ref flstServiceitems, value); }
        }
        Guid fOID;
        [Key(true)]
        public Guid OID
        {
            get { return fOID; }
            set { SetPropertyValue<Guid>("OID", ref fOID, value); }
        }
    }

}
