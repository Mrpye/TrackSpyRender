Imports System.Drawing
Namespace PlugIn
    Public Interface IPluginContext
        Property Layout() As Layout.LayoutItem
        Property DataConnector() As Data.DataReader
        Property g() As Graphics
    End Interface
End Namespace