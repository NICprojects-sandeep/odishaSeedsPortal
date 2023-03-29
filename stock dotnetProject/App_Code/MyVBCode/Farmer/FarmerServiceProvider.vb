Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports System.Web.HttpServerUtility

Public Class FarmerServiceProvider : Inherits IFarmer

    Public cmd As New SqlCommand
    ' Public Con As SqlConnection = New SqlConnection("Data Source=10.172.0.101;database=PDS;uid=sa;pwd=pass")
    Public Con As SqlConnection = New SqlConnection("Data Source=10.172.0.101;database=FARMERDB;uid=FARMER;pwd=faRmeR@DB@13")
#Region "Registration"
    Public Overrides Function FillDistrict(ByVal ddlDist As System.Web.UI.WebControls.DropDownList) As System.Web.UI.WebControls.DropDownList
        Dim dt As DataTable
        Try
            Dim sqlComm As SqlCommand = New SqlCommand("select distinct b.int_DistrictID,(vch_DistrictName)vch_DistrictName from PDS_BLOCKMASTER as a inner join PDS_DistrictMaster as b on NIC_BlockCode=@BlockCode and a.int_DistrictID=b.int_DistrictID", Con)
            sqlComm.Parameters.AddWithValue("@BlockCode", System.Web.HttpContext.Current.Session("BlockCode"))
            'Select int_DistrictID,(vch_DistrictName)vch_DistrictName From PDS_DistrictMaster vch_DistrictName where int_DistrictID='01'
            Dim da As New SqlDataAdapter(sqlComm)
            Dim ds As New DataSet
            da.Fill(ds, "FDATA")
            dt = ds.Tables(0)
            ddlDist.DataSource = dt
            ddlDist.DataTextField = "vch_DistrictName"
            ddlDist.DataValueField = "int_DistrictID"
            ddlDist.DataBind()
            'ddlDist.Items.Insert(0, New ListItem("--Select--", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Con.Close()
        End Try
        Return ddlDist
    End Function
    Public Overrides Function FillBlock(ByVal ddlBlock As System.Web.UI.WebControls.DropDownList, ByVal intDist As Integer) As System.Web.UI.WebControls.DropDownList
        Dim dt As DataTable
        Dim sqlComm As SqlCommand
        Try
            If intDist = 0 Then
                sqlComm = New SqlCommand("Select int_BlockID,Upper(vch_BlockName)vch_BlockName  From PDS_BlockMaster Order By vch_BlockName", Con)
            Else
                '260105'
                'NIC_BlockCode=" & intDist & "
                sqlComm = New SqlCommand("Select int_BlockID,Upper(vch_BlockName)vch_BlockName  From PDS_BlockMaster Where NIC_BlockCode=@BlockCode Order By vch_BlockName", Con)
                sqlComm.Parameters.AddWithValue("@BlockCode", System.Web.HttpContext.Current.Session("BlockCode"))
            End If

            Dim da As New SqlDataAdapter(sqlComm)
            Dim ds As New DataSet
            da.Fill(ds, "FDATA")
            dt = ds.Tables(0)
            ddlBlock.DataSource = dt
            ddlBlock.DataTextField = "vch_BlockName"
            ddlBlock.DataValueField = "int_BlockID"
            ddlBlock.DataBind()
            ddlBlock.Items.Insert(0, New ListItem("--Select--", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Con.Close()
        End Try
        Return ddlBlock
    End Function
    Public Overrides Function FillGP(ByVal ddlGP As System.Web.UI.WebControls.DropDownList, ByVal intBlock As Integer) As System.Web.UI.WebControls.DropDownList
        Dim dt As DataTable
        Try
            Dim sqlComm As SqlCommand = New SqlCommand("Select int_GPID,Upper(vch_GPName)vch_GPName From PDS_GPMaster Where int_BlockID=" & intBlock & " Order By vch_GPName", Con)
            Dim da As New SqlDataAdapter(sqlComm)
            Dim ds As New DataSet
            da.Fill(ds, "FDATA")
            dt = ds.Tables(0)
            ddlGP.DataSource = dt
            ddlGP.DataTextField = "vch_GPName"
            ddlGP.DataValueField = "int_GPID"
            ddlGP.DataBind()
            ddlGP.Items.Insert(0, New ListItem("--Select--", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Con.Close()
        End Try
        Return ddlGP
    End Function
    Public Overrides Function FillVillage(ByVal ddlVill As System.Web.UI.WebControls.DropDownList, ByVal intGP As String) As System.Web.UI.WebControls.DropDownList
        Dim dt As DataTable
        Try
            Dim sqlComm As SqlCommand = New SqlCommand("Select int_VillageID,Upper(vch_VillageName)vch_VillageName From PDS_VillageMaster Where int_GPID= '" & intGP & "' Order By vch_VillageName", Con)
            Dim da As New SqlDataAdapter(sqlComm)
            Dim ds As New DataSet
            da.Fill(ds, "FDATA")
            dt = ds.Tables(0)
            ddlVill.DataSource = dt
            ddlVill.DataTextField = "vch_VillageName"
            ddlVill.DataValueField = "int_VillageID"
            ddlVill.DataBind()
            ddlVill.Items.Insert(0, New ListItem("--Select--", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Con.Close()
        End Try
        Return ddlVill
    End Function
    Public Overrides Function FillBankName(ByVal ddlBank As DropDownList) As DropDownList
        Dim dt As DataTable
        Try
            Dim sqlComm As SqlCommand = New SqlCommand("SELECT INTID,VCHBANKNAME FROM M_PDS_FARMERBANK WHERE INTDELETEDFLAG=0 ORDER BY VCHBANKNAME", Con)
            Dim da As New SqlDataAdapter(sqlComm)
            Dim ds As New DataSet
            da.Fill(ds, "FDATA")
            dt = ds.Tables(0)
            ddlBank.DataSource = dt
            ddlBank.DataTextField = "VCHBANKNAME"
            ddlBank.DataValueField = "INTID"
            ddlBank.DataBind()
            ddlBank.Items.Insert(0, New ListItem("--Select--", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Con.Close()
        End Try
        Return ddlBank
    End Function
    Public Overrides Function BranchFill(ByVal ddlBranch As DropDownList, ByVal id As Integer, ByVal distid As String) As DropDownList
        Dim dt As DataTable
        Try
            Dim sqlComm As SqlCommand = New SqlCommand("SELECT INTBRANCHID,(VCHBRANCHNAME+','+VCHDISTRICT +' (' + isnull(IFSC_CODE,'-')  +')') VCHBRANCHNAME  FROM M_PDS_BANKBRANCH WHERE INTBANKID=" & id & " AND INTDELETEDFLAG=0 AND IFSC_CODE IS NOT NULL AND IFSC_CODE<>'' ORDER BY VCHBRANCHNAME", Con)
            Dim da As New SqlDataAdapter(sqlComm)
            Dim ds As New DataSet
            da.Fill(ds, "FDATA")
            dt = ds.Tables(0)
            ddlBranch.DataSource = dt
            ddlBranch.DataTextField = "VCHBRANCHNAME"
            ddlBranch.DataValueField = "INTBRANCHID"
            ddlBranch.DataBind()
            ddlBranch.Items.Insert(0, New ListItem("--Select--", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Con.Close()
        End Try
        Return ddlBranch
    End Function
    Public Overrides Function FillOdiaRelationforFR(ByVal ddlRelation As DropDownList) As DropDownList
        Dim dt As DataTable
        Try
            Dim sqlComm As SqlCommand = New SqlCommand("SELECT INTRELATIONID,VCHORIYAREL FROM M_PDS_RELATIONSHIP WHERE INTFRSTATUS=0 ORDER BY INTSEQUENCE", Con)
            Dim da As New SqlDataAdapter(sqlComm)
            Dim ds As New DataSet
            da.Fill(ds, "FDATA")
            dt = ds.Tables(0)
            ddlRelation.DataSource = dt
            ddlRelation.DataTextField = "VCHORIYAREL"
            ddlRelation.DataValueField = "INTRELATIONID"
            ddlRelation.DataBind()
            ddlRelation.Items.Insert(0, New ListItem("--Select--", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Con.Close()
        End Try
        Return ddlRelation
    End Function
#End Region
#Region "FARMER REGISTRATION"
    Public Overrides Function FarmerReg(ByVal ObjFarmer As Farmer) As String
        Dim strOut As String = ""
        cmd.CommandType = CommandType.Text
        cmd.Connection = Con
        Try
            Con.Open()
            cmd = New SqlCommand("USP_FARMER_REGISTRATION", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@P_CH_ACTION", ObjFarmer.Action)
            cmd.Parameters.AddWithValue("@P_INTFARMERID", ObjFarmer.Id)
            cmd.Parameters.AddWithValue("@P_XMLDATA", ObjFarmer.XMLDataFarmer)
            cmd.Parameters.AddWithValue("@P_XMLDATAFAMILY", ObjFarmer.XMLData)
            cmd.Parameters.AddWithValue("@P_VCHPACSCODE", ObjFarmer.Code)
            cmd.Parameters.Add("@P_VCH_OUTPUT", SqlDbType.VarChar, 50)
            cmd.Parameters("@P_VCH_OUTPUT").Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()
            strOut = cmd.Parameters("@P_VCH_OUTPUT").Value.ToString()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Con.Close()
        End Try
        Return strOut
    End Function
    Public Overrides Function chkMobAccDuplicacy(ByVal objFarmer As Farmer) As String
        Dim strMsg As String
        Try
            Dim sQuery As String = "EXECUTE USP_FARMER_CHKDUPLICACYMOBACC " & objFarmer.Action & ",'" & objFarmer.Code & "','" & objFarmer.Mobile & "','" & objFarmer.Address & "'," & objFarmer.AutoId & ""
            cmd = New SqlCommand(sQuery, Con)
            Con.Open()
            strMsg = cmd.ExecuteScalar().ToString()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Con.Close()
        End Try
        Return strMsg
    End Function
    Public Overrides Function GetFarmerDetails(ByVal objFarmer As Farmer) As DataTable
        Dim dt As DataTable
        Try
            Dim sqlComm As SqlCommand = New SqlCommand("EXECUTE USP_FARMER_VIEW '" & objFarmer.Action & "'," & objFarmer.Kms & "", Con)
            Dim da As New SqlDataAdapter(sqlComm)
            Dim ds As New DataSet
            da.Fill(ds, "FDATA")
            dt = ds.Tables(0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Con.Close()
        End Try
        Return dt
    End Function
#End Region
End Class
