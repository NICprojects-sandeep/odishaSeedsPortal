Imports Microsoft.VisualBasic

Public Class Farmer
    Private vchId As String
    Private chrAction As String
    Private intAutoId As Integer
    Private strMob, strAdd As String
    Private strCode As String
    Private strXML, strXMLFarmer As String
    Private intKms As Integer

    Public Property AutoId() As Integer
        Get
            Return intAutoId
        End Get
        Set(ByVal value As Integer)
            intAutoId = value
        End Set
    End Property
    Public Property Mobile() As String
        Get
            Return strMob
        End Get
        Set(ByVal value As String)
            strMob = value
        End Set
    End Property
    Public Property Address() As String
        Get
            Return strAdd
        End Get
        Set(ByVal value As String)
            strAdd = value
        End Set
    End Property
    Public Property Action() As String
        Get
            Return chrAction
        End Get
        Set(ByVal value As String)
            chrAction = value
        End Set
    End Property
    Public Property Code() As String
        Get
            Return strCode
        End Get
        Set(ByVal value As String)
            strCode = value
        End Set
    End Property
    Public Property Id() As String
        Get
            Return vchId
        End Get
        Set(ByVal value As String)
            vchId = value
        End Set
    End Property
    Public Property XMLData() As String
        Get
            Return strXML
        End Get
        Set(ByVal value As String)
            strXML = value
        End Set
    End Property
    Public Property XMLDataFarmer() As String
        Get
            Return strXMLFarmer
        End Get
        Set(ByVal value As String)
            strXMLFarmer = value
        End Set
    End Property
    Public Property Kms() As Integer
        Get
            Return intKms
        End Get
        Set(ByVal value As Integer)
            intKms = value
        End Set
    End Property
End Class
