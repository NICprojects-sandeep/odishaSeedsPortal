Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

''' <summary>
''' Error messages
''' </summary>
''' <remarks></remarks>
Public Class Messages
#Region "Private Const variables"
#Region "Common Messages"
    Public Const mSaveMessage As String = "Record Saved Successfully"
    Public Const DbError As String = "Transaction Error"
    Public Const mDeleteVR As String = "Data Can not be inserted,Try Again !!"
    Public Const mDuplicateVoterId As String = "Given Voter ID Card Number Already Exist!!!"
    Public Const mDuplicateAadhar As String = "Given Aadhar Number Already Exist!!!"
    Public Const mDuplicateMobile As String = "Given Mobile Number Already Exist!!!"
    Public Const mDuplicateAccount As String = "Given Account Number Already Exist!!!"
    Public Const mInvalidAadhar As String = "Given Aadhar Number is Invalid!!!"
#End Region
#End Region
#Region "Alert Common messages"
    Public Shared Function message(ByVal errmsgID As Integer) As String
        Dim retval As String = ""
        If errmsgID = 1 Then
            retval = mSaveMessage
        End If
        If errmsgID = 9 Then
            retval = DbError
        End If
        If errmsgID = 17 Then
            retval = mDeleteVR
        End If
        If errmsgID = 23 Then
            retval = mDuplicateVoterId
        End If
        If errmsgID = 24 Then
            retval = mDuplicateAadhar
        End If
        If errmsgID = 25 Then
            retval = mDuplicateMobile
        End If
        If errmsgID = 26 Then
            retval = mDuplicateAccount
        End If
        If errmsgID = 27 Then
            retval = mInvalidAadhar
        End If
        Return retval
    End Function
#End Region
End Class


