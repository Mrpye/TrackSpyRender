Imports System.Xml.Serialization
Imports System.Drawing
Imports System.Data
Imports System
Namespace Layout
    Public Class LayoutItem
#Region "Field"
        Private _Name As String
        Private _x As String
        Private _y As String
        Private _width As String
        Private _height As String
        Private _PlugInName As String
        Private _PlugInConfigName As String
        Private _DataMapping As Data.DataMappingItem()
        Private _PlugInConfig As Layout.SettingItem()
        Private Dt As New DataTable
        Private _ScreenSize As Size
#End Region
#Region "Property"
        <XmlAttribute("Name")> _
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property
        <XmlAttribute("PlugIn_Name")> _
        Public Property PlugInName() As String
            Get
                Return _PlugInName
            End Get
            Set(ByVal value As String)
                _PlugInName = value
            End Set
        End Property
        <XmlAttribute("X")> _
        Public Property X() As String
            Get
                Return _x
            End Get
            Set(ByVal value As String)
                _x = value
            End Set
        End Property
        <XmlAttribute("Y")> _
        Public Property Y() As String
            Get
                Return _y
            End Get
            Set(ByVal value As String)
                _y = value
            End Set
        End Property
        <XmlAttribute("Width")> _
        Public Property Width() As String
            Get
                Return _width
            End Get
            Set(ByVal value As String)
                _width = value
            End Set
        End Property
        <XmlAttribute("Height")> _
        Public Property Height() As String
            Get
                Return _height
            End Get
            Set(ByVal value As String)
                _height = value
            End Set
        End Property
        Public Property DataMapping() As Data.DataMappingItem()
            Get
                Return _DataMapping
            End Get
            Set(ByVal value As Data.DataMappingItem())
                _DataMapping = value
            End Set
        End Property
        Public Property PlugInConfig() As Layout.SettingItem()
            Get
                Return _PlugInConfig
            End Get
            Set(ByVal value As Layout.SettingItem())
                _PlugInConfig = value
            End Set
        End Property
#End Region
#Region "Methods"
        Public Sub SetScreenSize(ByVal osize As Size)
            _ScreenSize = osize
        End Sub
        Private Function EvalSize(ByVal Value As String) As String
            Dim Tval As String = Value
            If _ScreenSize = Nothing Then
                Throw New Exception("Need to set Sceen Height")
            End If
            Tval = Tval.ToUpper.Replace("[SCREENWIDTH]", _ScreenSize.Width)
            Tval = Tval.ToUpper.Replace("[SCREENHEIGHT]", _ScreenSize.Height)
            Tval = Tval.ToUpper.Replace("[SCREENMIN]", Math.Min(_ScreenSize.Height, _ScreenSize.Height))
            Tval = Tval.ToUpper.Replace("[SCREENMAX]", Math.Max(_ScreenSize.Height, _ScreenSize.Height))
            Return Tval
        End Function
        Public Function GetX() As Single
            Dim result = Dt.Compute(EvalSize(_x), Nothing)
            Return result
        End Function
        Public Function GetY() As Single
            Dim result = Dt.Compute(EvalSize(_y), Nothing)
            Return result
        End Function
        Public Function GetWidth() As Single
            Dim result = Dt.Compute(EvalSize(_width), Nothing)
            Return result
        End Function
        Public Function GetHeight() As Single
            Dim result = Dt.Compute(EvalSize(_height), Nothing)
            Return result
        End Function
#End Region
#Region "Constructor"
        Public Sub New(ByVal X As Single, ByVal Y As Single, ByVal Width As Single, ByVal Height As Single, ByVal Plugin As String, ByVal PluginConfig As Layout.SettingItem(), ByVal DataMapping As Data.DataMappingItem())
            Me._x = X
            Me._y = Y
            Me._width = Width
            Me._height = Height
            Me._PlugInName = Plugin
            Me.PlugInConfig = PluginConfig
            Me._DataMapping = DataMapping
        End Sub
        Public Sub New()

        End Sub
#End Region
    End Class
End Namespace