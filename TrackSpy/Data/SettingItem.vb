Imports System.Xml.Serialization
Namespace Layout
    Public Class SettingItem
#Region "Fields"
        Private _Name As String
        Private _Value As String
#End Region
#Region "Propety"
        <XmlAttribute("Name")> _
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property
        <XmlAttribute("Value")> _
        Public Property Value() As String
            Get
                Return _Value
            End Get
            Set(ByVal value As String)
                _Value = value
            End Set
        End Property
#End Region
#Region "Constructor"
        Public Sub New(ByVal Name As String, ByVal Value As String)
            Me.Name = Name
            Me.Value = Value
        End Sub
        Public Sub New()

        End Sub
#End Region
    End Class
End Namespace