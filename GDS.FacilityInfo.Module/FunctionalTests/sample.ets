;#DropDB GDS.FacilityInfoEasyTest

#Application GDS.FacilityInfo.Win
#Application GDS.FacilityInfo.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef GDS.FacilityInfo.Win

*Action About...
 
*Action OK

#EndIf

#IfDef GDS.FacilityInfo.Web
*Action Navigation(My Details)
#EndIf
