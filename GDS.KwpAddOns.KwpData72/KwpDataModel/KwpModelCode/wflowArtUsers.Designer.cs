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
namespace GDS.KwpAddOns.KwpData72.KwpDataModel.BNWINS_Demo_new
{

    public partial class wflowArtUsers : XPLiteObject
    {
        int fArtUserId;
        [Key(true)]
        public int ArtUserId
        {
            get { return fArtUserId; }
            set { SetPropertyValue<int>("ArtUserId", ref fArtUserId, value); }
        }
        wflowArten fArtId;
        [Association(@"wflowArtUsersReferenceswflowArten")]
        public wflowArten ArtId
        {
            get { return fArtId; }
            set { SetPropertyValue<wflowArten>("ArtId", ref fArtId, value); }
        }
        string fBenutzerID;
        [Indexed(Name = @"IX_wflowArtUsersBenutzerID")]
        [Size(8)]
        public string BenutzerID
        {
            get { return fBenutzerID; }
            set { SetPropertyValue<string>("BenutzerID", ref fBenutzerID, value); }
        }
    }

}
