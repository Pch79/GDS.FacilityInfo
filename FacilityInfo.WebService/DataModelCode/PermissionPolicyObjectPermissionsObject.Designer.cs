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

    public partial class PermissionPolicyObjectPermissionsObject : XPCustomObject
    {
        Guid fOid;
        [Key(true)]
        public Guid Oid
        {
            get { return fOid; }
            set { SetPropertyValue<Guid>("Oid", ref fOid, value); }
        }
        string fCriteria;
        [Size(SizeAttribute.Unlimited)]
        public string Criteria
        {
            get { return fCriteria; }
            set { SetPropertyValue<string>("Criteria", ref fCriteria, value); }
        }
        int fReadState;
        public int ReadState
        {
            get { return fReadState; }
            set { SetPropertyValue<int>("ReadState", ref fReadState, value); }
        }
        int fWriteState;
        public int WriteState
        {
            get { return fWriteState; }
            set { SetPropertyValue<int>("WriteState", ref fWriteState, value); }
        }
        int fDeleteState;
        public int DeleteState
        {
            get { return fDeleteState; }
            set { SetPropertyValue<int>("DeleteState", ref fDeleteState, value); }
        }
        int fNavigateState;
        public int NavigateState
        {
            get { return fNavigateState; }
            set { SetPropertyValue<int>("NavigateState", ref fNavigateState, value); }
        }
        PermissionPolicyTypePermissionsObject fTypePermissionObject;
        [Association(@"PermissionPolicyObjectPermissionsObjectReferencesPermissionPolicyTypePermissionsObject")]
        public PermissionPolicyTypePermissionsObject TypePermissionObject
        {
            get { return fTypePermissionObject; }
            set { SetPropertyValue<PermissionPolicyTypePermissionsObject>("TypePermissionObject", ref fTypePermissionObject, value); }
        }
    }

}
