using FacilityInfo.Liegenschaft.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.Management.Interfaces
{
    public interface IRealEstateService
    {
        void AddFunctionalUnit(boLiegenschaft workingRealEstate);
    }
}
