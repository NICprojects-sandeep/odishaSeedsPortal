Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class FarmerService
    Private Shared eProvider As IFarmer
    Public Shared ReadOnly Property Provider() As IFarmer
        Get
            Initialize()
            Return eProvider
        End Get
    End Property
    Private Shared Sub Initialize()
        eProvider = New FarmerServiceProvider
        Dim mystr As String
        mystr = eProvider.Name
    End Sub
    Public Shared Function FillDistrict(ByVal ddlDist As DropDownList) As DropDownList
        Return Provider.FillDistrict(ddlDist)
    End Function
    Public Shared Function FillBlock(ByVal ddlBlock As DropDownList, ByVal intDist As String) As DropDownList
        Return Provider.FillBlock(ddlBlock, intDist)
    End Function
    Public Shared Function FillGP(ByVal ddlGP As DropDownList, ByVal intGP As Integer) As DropDownList
        Return Provider.FillGP(ddlGP, intGP)
    End Function
    Public Shared Function FillVillage(ByVal ddlGP As DropDownList, ByVal intGP As String) As DropDownList
        Return Provider.FillVillage(ddlGP, intGP)
    End Function
    Public Shared Function FillBankName(ByVal ddlBank As DropDownList) As DropDownList
        Return Provider.FillBankName(ddlBank)
    End Function
    Public Shared Function BranchFill(ByVal ddlBranch As DropDownList, ByVal id As Integer, ByVal distid As String) As DropDownList
        Return Provider.BranchFill(ddlBranch, id, distid)
    End Function
    Public Shared Function FillOdiaRelationforFR(ByVal ddlRelation As DropDownList) As DropDownList
        Return Provider.FillOdiaRelationforFR(ddlRelation)
    End Function
#Region "FARMER REGISTRATION"
    Public Shared Function FarmerReg(ByVal ObjFarmer As Farmer) As String
        Return Provider.FarmerReg(ObjFarmer)
    End Function
    Public Shared Function chkMobAccDuplicacy(ByVal objFarmer As Farmer) As String
        Return Provider.chkMobAccDuplicacy(objFarmer)
    End Function
    Public Shared Function GetFarmerDetails(ByVal objFarmer As Farmer) As DataTable
        Return Provider.GetFarmerDetails(objFarmer)
    End Function
#End Region
End Class
