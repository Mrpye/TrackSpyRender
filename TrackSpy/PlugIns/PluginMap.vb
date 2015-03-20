Imports TrackSpy.PlugIn
Imports System.Drawing
Imports Newtonsoft.Json.Linq
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.IO
Namespace plug_Ins


    Public Class PluginMap
        Inherits TrackSpy.PlugIn.BasePlugin
        Implements IPlugin
#Region "Fields"
        Private _Lng As Single
        Private _Lat As Single
        Private _trackMap As Bitmap
        Private oTrackGpsList As List(Of Track_Gps_Data)
        Private TransX As Single
        Private TransY As Single
#End Region

#Region "Property"
        Public Property Lng() As Single
            Get
                Return _Lng
            End Get
            Set(ByVal value As Single)
                _Lng = value
            End Set
        End Property
        Public Property Lat() As Single
            Get
                Return _Lat
            End Get
            Set(ByVal value As Single)
                _Lat = value
            End Set
        End Property
#End Region


        Protected Overrides ReadOnly Property Name() As String
            Get
                Return "MapRender"
            End Get
        End Property
        Public Sub LoadTrackData(ByVal path As String)
            'Dim fileContents As String

            'fileContents = My.Computer.FileSystem.ReadAllText("C:\Users\andy.pye\Desktop\VB Projects\TrackSpyRender\TestData\track.json")
            'fileContents = My.Computer.FileSystem.ReadAllText(path)
            Dim json  = JValue.Parse(path)

            json = json.Root.First

            oTrackGpsList = New List(Of Track_Gps_Data)
            'Load gps data
            For Each obj as JObject In json.Root
                oTrackGpsList.Add(New Track_Gps_Data(obj.Item("lng"), obj.Item("lat"), obj.Item("track_id"), obj.Item("id")))
            Next
            BuildTrack()
        End Sub
        Private Sub BuildTrack()
            Dim gp As New Drawing2D.GraphicsPath
            Dim smallx As Single = 50000000000000
            Dim smally As Single = 50000000000000
            Dim g As Graphics
            Dim Translatex As Single = 0
            Dim Translatey As Single = 0
            Dim m As New Drawing2D.Matrix()

            Dim lng As Double = 0
            Dim lat As Double = 0


            If oTrackGpsList IsNot Nothing Then
                'Create array to store points
                Dim PointArray(oTrackGpsList.Count - 1) As PointF
                For i as integer = 0 To oTrackGpsList.Count - 1
                    Dim longd As Double = oTrackGpsList(i).lng * 100000
                    Dim latd As Double = oTrackGpsList(i).lat * 100000
                    If latd < smally Then
                        smally = latd
                    End If
                    If longd < smallx Then
                        smallx = longd
                    End If
                    PointArray(i) = New PointF(longd, latd)
                Next i
                'Add lines to graphic path
                gp.AddLines(PointArray)

                '==============================
                'Calc transform to fit in image
                '==============================
                If gp.GetBounds.X < 0 Then
                    Translatex = Math.Abs(gp.GetBounds.X)
                Else
                    Translatex = -smallx
                End If
                If gp.GetBounds.Y < 0 Then
                    Translatey = Math.Abs(gp.GetBounds.Y)
                Else
                    Translatey = -smally
                End If
                TransX = Translatex
                TransY = Translatey
                m.Translate(Translatex, Translatey)

                gp.Transform(m)


                _trackMap = New Bitmap(CInt(gp.GetBounds.Width), CInt(gp.GetBounds.Height))
                g = Graphics.FromImage(_trackMap)
                'gp = ReflectPath(gp)
                g.FillPath(New SolidBrush(Color.OrangeRed), gp)
                g.DrawPath(New Pen(Color.OrangeRed, 20), gp)
                g.Dispose()

            End If
        End Sub
        Public Overrides Sub PerformAction(ByVal context As TrackSpy.PlugIn.IPluginContext)
            'Set up
            'Map data
            MyBase.PerformAction(context)

            Dim m As New Drawing2D.Matrix()
            Dim gp As New Drawing2D.GraphicsPath
            Dim g As Graphics

            m.Translate(TransX, TransY)

            'g = Graphics.FromImage(trackMap)

            gp.AddEllipse(New RectangleF((Me.Lng * 100000) - 30, (Me.Lat * 100000) - 30, 60, 60))
            gp.Transform(m)


            Dim TMap As New Bitmap(_trackMap.Width, _trackMap.Height)
            g = Graphics.FromImage(TMap)


            g.DrawImage(_trackMap, New RectangleF(0, 0, _trackMap.Width, _trackMap.Height))

            g.DrawPath(Pens.Red, gp)
            g.FillPath(Brushes.Red, gp)

            TMap.RotateFlip(RotateFlipType.RotateNoneFlipY)

            context.g.DrawImage(TMap, New RectangleF(context.Layout.GetX(), context.Layout.GetY(), context.Layout.GetWidth(), context.Layout.GetHeight()), New RectangleF(0, 0, _trackMap.Width, _trackMap.Height), GraphicsUnit.Pixel)

            gp.Dispose()
            g.Dispose()

        End Sub
        Private Function ReflectPath(ByVal gp As Drawing2D.GraphicsPath) As Drawing2D.GraphicsPath
            Dim Xtranslation As Single = 2 * gp.GetBounds.Right
            Dim Ytranslation As Single = 2 * gp.GetBounds.Bottom
            Dim mtx As New Drawing2D.Matrix(-1, -1, 1, 1, Xtranslation, Ytranslation)
            Dim pth As New Drawing2D.GraphicsPath(gp.PathPoints, gp.PathTypes)
            Using mirror As New Drawing2D.GraphicsPath(gp.PathPoints, gp.PathTypes)
                mirror.Transform(mtx)
                mirror.Reverse()
                pth.AddPath(mirror, True)
                pth.CloseFigure()
            End Using
            Return pth
        End Function
        Private Function ReflectPathold(ByVal gp As Drawing2D.GraphicsPath) As Drawing2D.GraphicsPath

            Using mirror As New Drawing2D.GraphicsPath(gp.PathPoints, gp.PathTypes)
                Dim mtx As New Drawing2D.Matrix(-1, 0, 0, 1, gp.GetBounds.Width * 2.1F, 0) 'flip and translate X
                mirror.Transform(mtx)
                gp.AddPath(mirror, False)
            End Using
            Return gp
        End Function
        Private Sub DialRender_DataMap(ByVal Name As String, ByVal Value As Object) Handles Me.DataMap
            'Map the data to the property
            ' Try
            TrackSpy.Helper.ReflectionHelper.SetPropertyValue(Me, Name, Value)
            'Catch ex As Exception
            ' Throw New Exceptions.DataMappingException(ex.Message)
            'End Try
        End Sub
        Public Sub Save(ByVal filename As String)
            Dim mlSerializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(PluginMap))
            Dim writer As IO.StringWriter = New IO.StringWriter()
            mlSerializer.Serialize(writer, Me)
            My.Computer.FileSystem.WriteAllText(filename, writer.ToString, False, System.Text.Encoding.Unicode)
        End Sub
        Public Overrides Sub Config(ByVal Settings() As Layout.SettingItem)
            MyBase.Config(Settings)
        End Sub

    End Class


End Namespace