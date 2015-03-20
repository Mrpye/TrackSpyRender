Imports System.Drawing

Namespace PlugIn
    Public Class PluginContext
        Implements IPluginContext
        Private _DataConnector As Data.DataReader
        Private _DataMapping As Data.DataMappingItem()
        Private _g As System.Drawing.Graphics
        Private _Layout As Layout.LayoutItem
        Public _Screen As Size
        Public Property ScreenSize() As Size
            Get
                Return _Screen
            End Get
            Set(ByVal value As Size)
                _Screen = value
            End Set
        End Property
        Public Property DataConnector() As Data.DataReader Implements IPluginContext.DataConnector
            Get
                Return _DataConnector
            End Get
            Set(ByVal value As Data.DataReader)
                _DataConnector = value
            End Set
        End Property

        Public Property g() As System.Drawing.Graphics Implements IPluginContext.g
            Get
                Return _g
            End Get
            Set(ByVal value As System.Drawing.Graphics)
                _g = value
            End Set
        End Property
        Public Property Layout() As Layout.LayoutItem Implements IPluginContext.Layout
            Get
                Return _Layout
            End Get
            Set(ByVal value As Layout.LayoutItem)
                _Layout = value
            End Set
        End Property

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            _DataConnector = Nothing
            _DataMapping = Nothing
            If _g IsNot Nothing Then
                _g.Dispose()
                _g = Nothing
            End If
            _Layout = Nothing
            _Screen = Nothing
        End Sub
    End Class
End Namespace