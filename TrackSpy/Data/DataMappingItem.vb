Imports System.Xml.Serialization

Namespace Data
    Public Class DataMappingItem
#Region "Field"
        Private _Name As String
        Private _MapFrom As String
        Private _MapTo As String
#End Region
#Region "Properties"
        <XmlAttribute("Name")> _
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property
        <XmlAttribute("Map_From")> _
        Public Property MapFrom() As String
            Get
                Return _MapFrom
            End Get
            Set(ByVal value As String)
                _MapFrom = value
            End Set
        End Property
        <XmlAttribute("Map_To")> _
        Public Property MapTo() As String
            Get
                Return _MapTo
            End Get
            Set(ByVal value As String)
                _MapTo = value
            End Set
        End Property
#End Region
#Region "Constructor"
        Public Sub New()
        End Sub
        Public Sub New(ByVal Name As String, ByVal MapFrom As String, ByVal MapTo As String)
            Me._Name = Name
            Me._MapFrom = MapFrom
            Me._MapTo = MapTo
        End Sub
#End Region
    End Class
End Namespace