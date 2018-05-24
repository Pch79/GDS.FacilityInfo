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

    public partial class boMessung : XPCustomObject
    {
        Guid fOid;
        [Key(true)]
        public Guid Oid
        {
            get { return fOid; }
            set { SetPropertyValue<Guid>("Oid", ref fOid, value); }
        }
        DateTime fMessdatum;
        public DateTime Messdatum
        {
            get { return fMessdatum; }
            set { SetPropertyValue<DateTime>("Messdatum", ref fMessdatum, value); }
        }
        PermissionPolicyUser fDurchfuehrender;
        [Association(@"boMessungReferencesPermissionPolicyUser")]
        public PermissionPolicyUser Durchfuehrender
        {
            get { return fDurchfuehrender; }
            set { SetPropertyValue<PermissionPolicyUser>("Durchfuehrender", ref fDurchfuehrender, value); }
        }
        boMesstyp fMesstyp;
        [Association(@"boMessungReferencesboMesstyp")]
        public boMesstyp Messtyp
        {
            get { return fMesstyp; }
            set { SetPropertyValue<boMesstyp>("Messtyp", ref fMesstyp, value); }
        }
        boAnlage fAnlage;
        [Association(@"boMessungReferencesboAnlage")]
        public boAnlage Anlage
        {
            get { return fAnlage; }
            set { SetPropertyValue<boAnlage>("Anlage", ref fAnlage, value); }
        }
        int fStatus;
        public int Status
        {
            get { return fStatus; }
            set { SetPropertyValue<int>("Status", ref fStatus, value); }
        }
        string fNotiz;
        [Size(SizeAttribute.Unlimited)]
        public string Notiz
        {
            get { return fNotiz; }
            set { SetPropertyValue<string>("Notiz", ref fNotiz, value); }
        }
        DateTime fPlandatum;
        public DateTime Plandatum
        {
            get { return fPlandatum; }
            set { SetPropertyValue<DateTime>("Plandatum", ref fPlandatum, value); }
        }
        [Association(@"boMessprobeReferencesboMessung")]
        public XPCollection<boMessprobe> boMessprobes { get { return GetCollection<boMessprobe>("boMessprobes"); } }
        [Association(@"fiMessungAttachmentReferencesboMessung")]
        public XPCollection<fiMessungAttachment> fiMessungAttachments { get { return GetCollection<fiMessungAttachment>("fiMessungAttachments"); } }
    }

}
