using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using FacilityInfo.Anlagen.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects.ServiceHandling
{
    [DefaultClassOptions]
   [XafDisplayName("ServicePosition")]
    [ImageName("gearTool_16")]
    [XafDefaultProperty("MatchKey")]
    public class ServicePosition : BaseObject
    {
        private String _posText;
  
     
        private Int32 _positionsNummer;

        private String _arbeitsAnweisung;
        private ServiceSpecification _serviceSpecification;
        private Int32 _sortIndex;
        public ServicePosition(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [XafDisplayName("ServiceSpecification")]
        [Association("ServiceSpecification-ServicePosition")]
        public ServiceSpecification ServiceSpecification
        {
            get { return _serviceSpecification; }
            set { SetPropertyValue("ServiceSpecification", ref _serviceSpecification, value); }
        }

        [XafDisplayName("Arbeitsanweisung")]
        [Size(-1)]
        public String ArbeitsAnweisung
        {
            get { return _arbeitsAnweisung; }
            set { SetPropertyValue("ArbeitsAnweisung", ref _arbeitsAnweisung, value); }
        }
        [XafDisplayName("Nummer")]
        public Int32 PositionsNummer
        {
            get { return _positionsNummer; }
            set { SetPropertyValue("PositionsNummer", ref _positionsNummer, value); }
        }
        [XafDisplayName("SortIndex")]
        public Int32 SortIndex
        {
            get { return _sortIndex; }
            set { SetPropertyValue("SortIndex", ref _sortIndex, value); }
        }
       
        [XafDisplayName("Positionstext")]

        public String PosText
        {
            get { return _posText; }
            set { SetPropertyValue("PosText", ref _posText, value); }
        }

        /*
        JosEphAzoo 
            Wedda mude
            Wedda muss ins Bett
        */
    }
}