Imports TrackSpy.PlugIn
Imports System.Drawing
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System
Imports TrackSpy.Exceptions
Namespace plug_Ins


    Public Class PluginImg
        Inherits TrackSpy.PlugIn.BasePlugin
        Implements IPlugin
#Region "Fields"
        Private _ImagePath As String
        Private _trackMap As Bitmap

#End Region

#Region "Property"
        Public Property ImagePath() As String
            Get
                Return _ImagePath
            End Get
            Set(ByVal value As String)
                _ImagePath = value
                Me.LoadImg()
            End Set
        End Property

#End Region


        Protected Overrides ReadOnly Property Name() As String
            Get
                Return "ImgRender"
            End Get
        End Property
        Public Sub LoadImg()
            If My.Computer.FileSystem.FileExists(_ImagePath) Then
                _trackMap = New Bitmap(_ImagePath)
            Else
                Throw New PluginException("Missing image" & _ImagePath)
            End If
        End Sub
        Public Overrides Sub PerformAction(ByVal context As TrackSpy.PlugIn.IPluginContext)



            If _trackMap IsNot Nothing Then
                context.g.DrawImage(_trackMap, New RectangleF(context.Layout.GetX(), context.Layout.GetY(), context.Layout.GetWidth(), context.Layout.GetHeight()), New RectangleF(0, 0, _trackMap.Width, _trackMap.Height), GraphicsUnit.Pixel)
            End If

        End Sub
        Private Sub DialRender_DataMap(ByVal Name As String, ByVal Value As Object) Handles Me.DataMap
            'Map the data to the property
            ' Try
            TrackSpy.Helper.ReflectionHelper.SetPropertyValue(Me, Name, Value)
            'Catch ex As Exception
            ' Throw New Exceptions.DataMappingException(ex.Message)
            'End Try
        End Sub
        Public Sub Save(ByVal filename As String)
            Dim mlSerializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(PluginImg))
            Dim writer As IO.StringWriter = New IO.StringWriter()
            mlSerializer.Serialize(writer, Me)
            My.Computer.FileSystem.WriteAllText(filename, writer.ToString, False, System.Text.Encoding.Unicode)
        End Sub
        Public Overrides Sub Config(ByVal Settings() As Layout.SettingItem)
            MyBase.Config(Settings)
        End Sub

    End Class


End Namespace