Imports System.Data.SQLite
Imports AForge.Video.FFMPEG
Imports TrackSpyLib.Dials
Imports System.Drawing
Imports System.Reflection
Imports TrackSpyLib.PlugIn

Namespace Renderers
    Public Class RenderVideoV2
#Region "Fields"
        Private TrackData As Data.DataReader = Nothing
        Private Layout As Layout.LayoutManager = Nothing
        Private PlugInsAsembily As Dictionary(Of String, Assembly)
        Private PlugIns As Dictionary(Of String, IPlugin)
#End Region
#Region "Events"
        Public Event StaringFrame(ByVal FrameCount As Integer)
        Public Event RenderingFrame(ByVal No As Integer)
        Public Event Finished()
#End Region
        Private Function ValidateMappingNames(ByVal dm As Data.DataMappingItem()) As String
            Dim DataMapping As String() = {"OBD_EngineRPM_Val", "OBD_VehicleSpeed_Val", "OBD_ThrottlePosition_Val", "OBD_BarometricPressure_Val", "OBD_AmbientAirTemperature_Val", "OBD_ActualEnginePercentTorque_Val", "OBD_EngineReferenceTorque_Val", "OBD_IntakeAirTemperature_Val", "GPS_Longitude_Val", "GPS_Latitude_Val", "GPS_Altitude_Val", "GPS_Course_Val", "GPS_Speed_Val", "SENS_XAcceleration_Val", "SENS_YAcceleration_Val", "SENS_ZAcceleration_Val", "SENS_XGyroscope_Val", "SENS_YGyroscope_Val", "SENS_ZGyroscope_Val", _
                                           "OBD_EngineRPM_Max", "OBD_VehicleSpeed_Max", "OBD_ThrottlePosition_Max", "OBD_BarometricPressure_Max", "OBD_AmbientAirTemperature_Max", "OBD_ActualEnginePercentTorque_Max", "OBD_EngineReferenceTorque_Max", "OBD_IntakeAirTemperature_Max", "GPS_Longitude_Max", "GPS_Latitude_Max", "GPS_Altitude_Max", "GPS_Course_Max", "GPS_Speed_Max", "SENS_XAcceleration_Max", "SENS_YAcceleration_Max", "SENS_ZAcceleration_Max", "SENS_XGyroscope_Max", "SENS_YGyroscope_Max", "SENS_ZGyroscope_Max", _
                                           "OBD_EngineRPM_Min", "OBD_VehicleSpeed_Min", "OBD_ThrottlePosition_Min", "OBD_BarometricPressure_Min", "OBD_AmbientAirTemperature_Min", "OBD_ActualEnginePercentTorque_Min", "OBD_EngineReferenceTorque_Min", "OBD_IntakeAirTemperature_Min", "GPS_Longitude_Min", "GPS_Latitude_Min", "GPS_Altitude_Min", "GPS_Course_Min", "GPS_Speed_Min", "SENS_XAcceleration_Min", "SENS_YAcceleration_Min", "SENS_ZAcceleration_Min", "SENS_XGyroscope_Min", "SENS_YGyroscope_Min", "SENS_ZGyroscope_Min"}
            For Each tobj In dm
                If Not DataMapping.Contains(tobj.MapFrom) Then
                    Return "Cannot Find Mapping " & tobj.MapFrom
                End If
            Next tobj
            Return ""
        End Function
        Public Sub Render(ByVal Database As String, ByVal Layout As String, ByVal InputVideo As String, ByVal OutputVideo As String)
            '================
            'Declare varibles
            '================
            Dim reader As VideoFileReader = Nothing
            Dim WRITER As VideoFileWriter = Nothing
            Dim asm As Assembly = Nothing
            Dim myType As System.Type = Nothing
            Dim plugin As IPlugin = Nothing
            PlugInsAsembily = New Dictionary(Of String, Assembly)
            PlugIns = New Dictionary(Of String, IPlugin)

            '==================
            'Check input values
            '==================
            If Not My.Computer.FileSystem.FileExists(Database) Then
                Throw New IO.IOException("Missing Database File")
            End If

            If Not My.Computer.FileSystem.FileExists(InputVideo) Then
                Throw New IO.IOException("Missing Video File")
            End If

            If Not My.Computer.FileSystem.FileExists(Layout) Then
                Throw New IO.IOException("Missing Layout File")
            End If

            If My.Computer.FileSystem.FileExists(OutputVideo) Then
                My.Computer.FileSystem.DeleteFile(OutputVideo)
            End If

            ' Try
            '=======================
            'Load the layout manager
            '=======================
            Me.Layout = TrackSpyLib.Layout.LayoutManager.LoadFromFile(Layout)
            If Me.Layout Is Nothing Then Throw New IO.IOException("Unable to load " & Layout)
            'Validate the mapping
            For Each Tobj In Me.Layout.Layouts
                Dim Result As String = ValidateMappingNames(Me.Layout.Layouts(0).DataMapping)
                If Result <> "" Then
                    Throw New Exceptions.DataMappingException(Result)
                End If
            Next

            ' Catch ex As Exception
            'Throw New IO.IOException(ex.Message)
            ' End Try


            'Try
            '==========================================
            ' create instance of video reader AND WRITE
            '==========================================
            reader = New VideoFileReader()
            WRITER = New VideoFileWriter()

            '================
            ' open video file
            '================
            reader.Open(InputVideo)

            '===================
            'OPEN THE WRITE FILE
            '===================
            WRITER.Open(OutputVideo, reader.Width, reader.Height, reader.FrameRate, VideoCodec.MPEG4)

            '=======================
            'This opend the database
            '=======================
            TrackData = New TrackSpyLib.Data.DataReader(Database, reader.FrameCount)


            '========================
            'Now Presload the plugins
            '========================
            For Each oItem As TrackSpyLib.Layout.LayoutItem In Me.Layout.Layouts
                Dim Path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, oItem.PlugInName)
                'Dim Path As String = IO.Path.Combine("C:\Users\andrew.pye\Desktop\VbProject\TrackSpyRender\DialRender\bin\Debug", oItem.PlugInName)
                '=====================
                'Set the screen height
                '=====================
                oItem.SetScreenSize(New Size(reader.Width, reader.Height))
                If My.Computer.FileSystem.FileExists(Path) Then
                    '==================
                    'Save our assembily
                    '==================
                    If PlugInsAsembily.ContainsKey(oItem.PlugInName) = False Then
                        asm = Assembly.LoadFrom(Path)
                        If asm IsNot Nothing Then
                            PlugInsAsembily.Add(oItem.PlugInName, asm)
                        Else
                            Throw New Exceptions.PluginException("Cannot Load PlugIns Asembily")
                        End If
                    End If
                    '==================
                    'Now create a class
                    '==================
                    If PlugIns.ContainsKey(oItem.Name) = False Then
                        asm = PlugInsAsembily.Item(oItem.PlugInName)
                        If asm IsNot Nothing Then
                            myType = asm.GetType(asm.GetName.Name + ".Plugin")
                            Dim implementsIPlugin As Boolean = GetType(IPlugin).IsAssignableFrom(myType)
                            If implementsIPlugin Then
                                plugin = CType(Activator.CreateInstance(myType), IPlugin)
                                plugin.Config(oItem.PlugInConfig)
                                'Add to the list
                                PlugIns.Add(oItem.Name, plugin)
                            End If
                        End If
                    End If
                Else
                    Throw New Exceptions.PluginException("Plug in " & oItem.PlugInName & " Does not exist")
                End If

            Next oItem

            '==============================
            'Raise event Strating Rendering
            '==============================
            RaiseEvent StaringFrame(reader.FrameCount)

            '=========
            'Main Loop
            '=========
            For i As Integer = 0 To 1000 'reader.FrameCount - 1
                '==================
                'Increment the data
                '==================
                If TrackData.Read() = True Then
                    '    '==========================
                    '    'Rendering First Frame
                    '    '==========================
                    RaiseEvent RenderingFrame(i)

                    Dim videoFrame As Bitmap = reader.ReadVideoFrame()

                    '    'videoFrame.RotateFlip(RotateFlipType.Rotate90FlipNone)
                    '    ' process the frame somehow
                    '    ' ...
                    Dim G As Graphics = Graphics.FromImage(videoFrame)

                    '    '==============
                    '    'Draw the layout
                    '    '==============
                    For Each oItem As TrackSpyLib.Layout.LayoutItem In Me.Layout.Layouts
                        '        '=======================
                        '        'Set up the data context
                        '        '======================= 
                        Dim dc As New PlugIn.PluginContext()
                        dc.DataConnector = TrackData
                        dc.g = G
                        dc.ScreenSize = New Size(reader.Width, reader.Height)
                        dc.Layout = oItem
                        plugin = PlugIns.Item(oItem.Name)
                        ' plugin = CType(myType, IPlugin)
                        '        'Write our frame back
                        plugin.PerformAction(dc)
                        plugin = Nothing
                        dc = Nothing
                    Next oItem

                    '    '====================
                    '    '====================
                    WRITER.WriteVideoFrame(videoFrame)

                    G.Dispose()
                    videoFrame.Dispose()
                    videoFrame = Nothing
                    G = Nothing
                End If
            Next

            reader.Close()
            WRITER.Close()
            ' Catch ex As Exception
            ' Throw New Exception(ex.Message)
            'Finally
            '=================
            'Clean up the data
            '=================
            If reader IsNot Nothing Then
                If reader.IsOpen Then
                    reader.Close()
                End If
                reader.Dispose()
            End If
            If WRITER IsNot Nothing Then
                If WRITER.IsOpen Then
                    WRITER.Close()
                End If
                WRITER.Dispose()
            End If
            asm = Nothing
            myType = Nothing
            plugin = Nothing
            ' End Try

            RaiseEvent Finished()

        End Sub
        Private Function GetValue(ByVal PropertyName As String) As Single
            Try
                Dim Pi As FieldInfo = TrackData.GetType.GetField(PropertyName)
                Return Pi.GetValue(TrackData)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace