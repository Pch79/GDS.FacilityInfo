﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;

namespace FacilityInfo.Management
{
    public static class clsStatic
    {
        public static System.String loggedOnMandantOid;

        public static String loggedOnHausVerwalterOid;
        public static bool adminLoggedOn;
        public static String AppHomeDirectory;
        public static String AppName;
    }
}
