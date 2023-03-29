Imports Microsoft.VisualBasic
Imports System.Configuration.Provider
Imports System.Data
Imports System.Data.SqlClient

Public MustInherit Class IFarmer : Inherits ProviderBase
#Region "MASTER"
    Public MustOverride Function FillDistrict(ByVal ddlDist As DropDownList) As DropDownList
    Public MustOverride Function FillBlock(ByVal ddlBlock As DropDownList, ByVal intDist As Integer) As DropDownList
    Public MustOverride Function FillGP(ByVal ddlGP As DropDownList, ByVal intGP As Integer) As DropDownList
    Public MustOverride Function FillVillage(ByVal ddlGP As DropDownList, ByVal intGP As String) As DropDownList
    Public MustOverride Function FillBankName(ByVal ddlBank As DropDownList) As DropDownList
    Public MustOverride Function BranchFill(ByVal ddlBranch As DropDownList, ByVal id As Integer, ByVal distid As String) As DropDownList
    Public MustOverride Function FillOdiaRelationforFR(ByVal ddlRelation As DropDownList) As DropDownList
#End Region
#Region "FARMER REGISTRATION"
    Public MustOverride Function FarmerReg(ByVal ObjFarmer As Farmer) As String
    Public MustOverride Function chkMobAccDuplicacy(ByVal objFarmer As Farmer) As String
    Public MustOverride Function GetFarmerDetails(ByVal objFarmer As Farmer) As DataTable
#End Region
End Class
