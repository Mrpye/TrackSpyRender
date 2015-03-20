Imports TrackSpy.PlugIn
Imports TrackSpy.Exceptions
Imports System
Namespace PlugIn
    Public Class BasePlugin
        Implements IPlugin
#Region "Fields"
        Private _X As Single
        Private _Y As Single
        Private _Width As Single
        Private _Height As Single
#End Region
#Region "Properties"
        Public Property X() As Single
            Get
                Return _X
            End Get
            Set(ByVal value As Single)
                _X = value
            End Set
        End Property
        Public Property Y() As Single
            Get
                Return _Y
            End Get
            Set(ByVal value As Single)
                _Y = value
            End Set
        End Property
        Public Property Width() As Single
            Get
                Return _Width
            End Get
            Set(ByVal value As Single)
                _Width = value
            End Set
        End Property
        Public Property Height() As Single
            Get
                Return _Height
            End Get
            Set(ByVal value As Single)
                _Height = value
            End Set
        End Property
#End Region

        Public Event DataMap(ByVal Name As String, ByVal Value As Object)
        Protected Overridable ReadOnly Property Name() As String Implements IPlugin.Name
            Get
                Return "BasePlugin"
            End Get
        End Property
        Public Overridable Sub Config(ByVal Settings As Layout.SettingItem()) Implements IPlugin.Config
            Try
                '============
                'Map the data
                '============
                For Each DataMapItem As TrackSpy.Layout.SettingItem In Settings
                    '===========================================
                    'Map the value to the property of the plugin
                    '===========================================
                    onDataMap(DataMapItem.Name, DataMapItem.Value)
                Next DataMapItem
            Catch ex As Exception
                Throw New DataMappingException(ex.Message)
            End Try

        End Sub
        Protected Overridable Function FixPath(ByVal Path As String) As String
            Dim TPath As String = Path
            TPath = TPath.ToUpper.Replace("[Current_Path]".ToUpper, My.Application.Info.DirectoryPath)
            Return TPath
        End Function
        Public Overridable Sub PerformAction(ByVal context As IPluginContext) Implements IPlugin.PerformAction

            'If context.DataConnector Is Nothing Then Throw New DataReaderException("No Data Reader Context Set")
            If context.g Is Nothing Then Throw New MissingGraphicsException("No Graphic Contect Set")

            Try
                '============
                'Map the data
                '============
                For Each DataMapItem As TrackSpy.Data.DataMappingItem In context.Layout.DataMapping
                    '===========================================
                    'Map the value to the property of the plugin
                    '===========================================
                    ' Dim Value As Object = Helper.ReflectionHelper.GetFieldValue(context.DataConnector, DataMapItem.MapFrom)
                    ' onDataMap(DataMapItem.MapTo, Value)
                Next DataMapItem

            Catch ex As Exception
                Throw New DataMappingException(ex.Message)
            End Try

        End Sub
        Protected Overridable Sub onDataMap(ByVal Name As String, ByVal Value As Object)
            RaiseEvent DataMap(Name, Value)
        End Sub
    End Class
End Namespace