Namespace PlugIn
    Public Interface IPlugin
        ReadOnly Property Name() As String
        Sub PerformAction(ByVal context As IPluginContext)
        Sub Config(ByVal context As Layout.SettingItem())
    End Interface
End Namespace