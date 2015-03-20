Imports TrackSpy.Dials
Imports System.Drawing
Imports System.Reflection
Imports TrackSpy.PlugIn
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports TrackSpy.plug_Ins
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Microsoft.VisualBasic.Conversion
Imports System.Diagnostics

Namespace Renderers
    Public Class RenderVideo
#Region "Fields"
        Private TrackData As Data.DataReader = Nothing
        Private Layout As Layout.LayoutManager = Nothing
        Private PlugInsAsembily As Dictionary(Of String, Assembly)
        Private PlugIns As Dictionary(Of String, IPlugin)
        Private oTrackGpsList As List(Of Track_Gps_Data)
        Private TransX As Single
        Private TransY As Single
        Private trackMap As Bitmap
#End Region
#Region "Events"
        Public Event StaringFrame(ByVal FrameCount As Integer)
        Public Event RenderingFrame(ByVal No As Integer)
        Public Event Finished()
#End Region
        'Private Function ValidateMappingNames(ByVal dm As Data.DataMappingItem()) As String
        '    Dim DataMapping As String() = {"OBD_EngineRPM_Val", "OBD_VehicleSpeed_Val", "OBD_ThrottlePosition_Val", "OBD_BarometricPressure_Val", "OBD_AmbientAirTemperature_Val", "OBD_ActualEnginePercentTorque_Val", "OBD_EngineReferenceTorque_Val", "OBD_IntakeAirTemperature_Val", "GPS_Longitude_Val", "GPS_Latitude_Val", "GPS_Altitude_Val", "GPS_Course_Val", "GPS_Speed_Val", "SENS_XAcceleration_Val", "SENS_YAcceleration_Val", "SENS_ZAcceleration_Val", "SENS_XGyroscope_Val", "SENS_YGyroscope_Val", "SENS_ZGyroscope_Val", _
        '                                   "OBD_EngineRPM_Max", "OBD_VehicleSpeed_Max", "OBD_ThrottlePosition_Max", "OBD_BarometricPressure_Max", "OBD_AmbientAirTemperature_Max", "OBD_ActualEnginePercentTorque_Max", "OBD_EngineReferenceTorque_Max", "OBD_IntakeAirTemperature_Max", "GPS_Longitude_Max", "GPS_Latitude_Max", "GPS_Altitude_Max", "GPS_Course_Max", "GPS_Speed_Max", "SENS_XAcceleration_Max", "SENS_YAcceleration_Max", "SENS_ZAcceleration_Max", "SENS_XGyroscope_Max", "SENS_YGyroscope_Max", "SENS_ZGyroscope_Max", _
        '                                   "OBD_EngineRPM_Min", "OBD_VehicleSpeed_Min", "OBD_ThrottlePosition_Min", "OBD_BarometricPressure_Min", "OBD_AmbientAirTemperature_Min", "OBD_ActualEnginePercentTorque_Min", "OBD_EngineReferenceTorque_Min", "OBD_IntakeAirTemperature_Min", "GPS_Longitude_Min", "GPS_Latitude_Min", "GPS_Altitude_Min", "GPS_Course_Min", "GPS_Speed_Min", "SENS_XAcceleration_Min", "SENS_YAcceleration_Min", "SENS_ZAcceleration_Min", "SENS_XGyroscope_Min", "SENS_YGyroscope_Min", "SENS_ZGyroscope_Min"}
        '    For Each tobj In dm
        '        If Not DataMapping.Contains(tobj.MapFrom) Then
        '            Return "Cannot Find Mapping " & tobj.MapFrom
        '        End If
        '    Next tobj
        '    Return ""
        'End Function
        'Private Sub LoadTrackData()
        '    Dim fileContents As String

        '    fileContents = My.Computer.FileSystem.ReadAllText("C:\Users\andy.pye\Desktop\VB Projects\TrackSpyRender\TestData\track.json")
        '    Dim json = JValue.Parse(fileContents)

        '    json = json.Root.First

        '    oTrackGpsList = New List(Of Track_Gps_Data)
        '    'Load gps data
        '    For Each obj In json.Root

        '        oTrackGpsList.Add(New Track_Gps_Data(obj.Item("lng"), obj.Item("lat"), obj.Item("track_id"), obj.Item("id")))

        '    Next
        'End Sub
        'Private Sub BuildTrack()
        '    Dim gp As New Drawing2D.GraphicsPath
        '    'Dim gp2 As New Drawing2D.GraphicsPath
        '    Dim smallx As Single = 50000000000000
        '    Dim smally As Single = 50000000000000
        '    Dim g As Graphics
        '    Dim Translatex As Single = 0
        '    Dim Translatey As Single = 0
        '    Dim m As New Drawing2D.Matrix()
        '    'Dim bp As Bitmap

        '    Dim lng As Double = 0
        '    Dim lat As Double = 0


        '    If oTrackGpsList IsNot Nothing Then



        '        'Dim oTrackGpsList As List(Of Track_Gps_Data) = oTrackGps.SearchByTrackId(TTrack.id)
        '        'Create array to store points
        '        Dim PointArray(oTrackGpsList.Count - 1) As PointF
        '        For i = 0 To oTrackGpsList.Count - 1
        '            Dim longd As Double = oTrackGpsList(i).lng * 100000
        '            Dim latd As Double = oTrackGpsList(i).lat * 100000
        '            If latd < smally Then
        '                smally = latd
        '            End If
        '            If longd < smallx Then
        '                smallx = longd
        '            End If
        '            PointArray(i) = New PointF(longd, latd)
        '        Next i
        '        'Add lines to graphic path
        '        gp.AddLines(PointArray)

        '        'Add the start point
        '        'gp.AddEllipse(New RectangleF(TTrack.start_line_lng * 100000, TTrack.start_line_lat * 100000, 100, 100))
        '        '=============
        '        'Set the scale
        '        '=============
        '        ' m.Scale(MaxScale, MaxScale)
        '        'gp2.AddEllipse(New RectangleF(Lng2 * 100000 - 10, lat2 * 100000 - 10, 20, 20))

        '        '==============================
        '        'Calc transform to fit in image
        '        '==============================
        '        If gp.GetBounds.X < 0 Then
        '            Translatex = Math.Abs(gp.GetBounds.X)
        '        Else
        '            Translatex = -smallx
        '        End If
        '        If gp.GetBounds.Y < 0 Then
        '            Translatey = Math.Abs(gp.GetBounds.Y)
        '        Else
        '            Translatey = -smally
        '        End If
        '        TransX = Translatex
        '        TransY = Translatey
        '        m.Translate(Translatex, Translatey)

        '        ' gp2.Transform(m)

        '        gp.Transform(m)

        '        trackMap = New Bitmap(CInt(gp.GetBounds.Width), CInt(gp.GetBounds.Height))
        '        g = Graphics.FromImage(trackMap)

        '        g.FillPath(Brushes.OrangeRed, gp)
        '        g.DrawPath(Pens.Orange, gp)
        '        g.Dispose()
        '        ' g.DrawPath(Pens.Red, gp2)
        '        'g.FillPath(Brushes.Red, gp2)
        '        ' e.DrawImage(bp, New RectangleF(0, 0, 300, 300), New RectangleF(0, 0, bp.Width, bp.Height), GraphicsUnit.Pixel)
        '    End If
        'End Sub
        'Private Sub RenderMap(ByVal e As Graphics, ByVal Lng2 As Single, ByVal lat2 As Single)
        '    Dim m As New Drawing2D.Matrix()
        '    Dim gp As New Drawing2D.GraphicsPath
        '    Dim g As Graphics

        '    m.Translate(TransX, TransY)

        '    'g = Graphics.FromImage(trackMap)

        '    gp.AddEllipse(New RectangleF((Lng2 * 100000) - 20, (lat2 * 100000) - 20, 40, 40))
        '    gp.Transform(m)


        '    Dim TMap As New Bitmap(trackMap.Width, trackMap.Height)
        '    g = Graphics.FromImage(TMap)
        '    g.DrawImage(trackMap, New RectangleF(0, 0, trackMap.Width, trackMap.Height))

        '    g.DrawPath(Pens.Red, gp)
        '    g.FillPath(Brushes.Red, gp)



        '    e.DrawImage(TMap, New RectangleF(0, 0, 300, 300), New RectangleF(0, 0, trackMap.Width, trackMap.Height), GraphicsUnit.Pixel)

        '    gp.Dispose()
        '    g.Dispose()

        'End Sub
        Public Function ReadData(ByVal url As String) As String
            Dim Res As String = ""
            Dim inStream As StreamReader
            Dim webRequest As Net.WebRequest
            Dim webresponse As Net.WebResponse
            webRequest = Net.WebRequest.Create(url)
            webresponse = webRequest.GetResponse()
            inStream = New StreamReader(webresponse.GetResponseStream())
            Res = inStream.ReadToEnd()
            Return Res
        End Function
        Private Function SecToTime(ByVal oseconds As Single) As String
            Dim Seconds as integer = Int(Math.Abs(oseconds))
            Dim remainder As Single = oseconds - Seconds
            'Dim Hours = Seconds / 3600
            Dim Hours = Int(Seconds / 3600)
            Dim Minutes = Int(Seconds / 60)
            Seconds = Int(Seconds Mod 60)

            Dim sHours As String = Hours.ToString.PadLeft(2, "0"c)
            Dim sMin As String = Minutes.ToString.PadLeft(2, "0"c)
            Dim sSec As String = Seconds.ToString.PadLeft(2, "0"c)

            Dim a As String() = Math.Round(remainder, 2).ToString.Split(".")
            If a.Length > 1 Then
                Return sHours & ":" & sMin & ":" & sSec & "." & a(1)
            Else
                Return sHours & ":" & sMin & ":" & sSec & ".0"
            End If

        End Function
        Public Sub DownloadVideo(ByVal filename As String, ByVal Temp As String)

            My.Computer.Network.DownloadFile("https://s3-us-west-2.amazonaws.com/pistonspy/" & filename, Temp)

        End Sub
        Public Sub ExtractFrames(ByVal ffmpg As String, ByVal TempFile As String)
            Dim Path As String = IO.Path.GetFullPath(TempFile).Replace(IO.Path.GetFileName(TempFile), "")
            Dim process As Process = New Process()
            Dim ffPath As String = IO.Path.Combine(ffmpg, "ffmpeg.exe")
            process.StartInfo.FileName = ffPath
            process.StartInfo.Arguments = "-i """ & TempFile & """ -vf fps=25 """ & Path & "\ffmpeg_%03d.png"""
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            process.Start()
            process.WaitForExit()
        End Sub
        Public Sub BuildVideo(ByVal ffmpg As String, ByVal TempFile As String)
            Dim Path As String = IO.Path.GetFullPath(TempFile).Replace(IO.Path.GetFileName(TempFile), "")
            If My.Computer.FileSystem.FileExists(Path & "\new.mp4") Then
                My.Computer.FileSystem.DeleteFile(Path & "\new.mp4")
            End If
            'ffmpeg -i  "C:\Users\andy.pye\Desktop\VB Projects\TrackSpyRender\TestData\test\ffmpeg_%03d.png" -vf "fps=25,format=yuv420p" "C:\Users\andy.pye\Desktop\VB Projects\TrackSpyRender\TestData\test\new.mp4"
            Dim process As Process = New Process()
            Dim ffPath As String = IO.Path.Combine(ffmpg, "ffmpeg.exe")
            process.StartInfo.FileName = ffPath
            'process.StartInfo.FileName = "C:\ffmpeg-20150312-git-3bedc99-win32-static\bin\ffmpeg.exe"
            process.StartInfo.Arguments = "-i """ & Path & "\ffmpeg_%03d.png"" -vf fps=25,format=yuv420p """ & Path & "\new.mp4"""
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            process.Start()
            process.WaitForExit()
        End Sub
        Public Sub Render(ByVal Layout As String, ByVal RecordingID As Integer, ByVal TempFolder As String, ByVal ffmpg As String, Optional ByVal Preview As Boolean = False, Optional ByRef PreviewBMP As Bitmap = Nothing)
            '================
            'Declare varibles
            '================
            Dim Fps As Single = 25
            Dim FrameTime As Single = 1 / Fps
            Dim StartData As Single = 0
            Dim EndData As Single = 0

            Dim CurrSpeedVal As Single = 0
            Dim CurrRPMVal As Single = 0
            Dim CurrTempVal As Single = 0
            Dim CurrlngVal As Single = 0
            Dim CurrlatVal As Single = 0

            Dim LastSpeedVal As Single = 0
            Dim LastRPMVal As Single = 0
            Dim LastTempVal As Single = 0

            Dim LastLngVal As Single = 0
            Dim LastLatVal As Single = 0

            Dim fxVal As Single = 0
            Dim fyVal As Single = 0

            Dim CurrfxVal As Single = 0
            Dim CurrfyVal As Single = 0
            Dim LastfxVal As Single = 0
            Dim LastfyVal As Single = 0

            Dim Speedval As Single = 0
            Dim Tempval As Single = 0
            Dim RPMval As Single = 0

            Dim Lngval As Single = 0
            Dim Latval As Single = 0

            Dim lap_count As Single = 0
            Dim last_lap As String
            Dim best_lap As String
            Dim total_lap_run As Single = 0
            Dim sub_time As Single = 0

            Dim TrackID As Integer
            Dim VideoFile As String
            Dim g As Graphics = Nothing

            '==================
            'Check input values
            '==================
            If TempFolder = "" Then
                Throw New IO.IOException("Missing Temp Folder")
            End If

            If Not My.Computer.FileSystem.FileExists(Layout) Then
                Throw New IO.IOException("Missing Layout File")
            End If

            If Not RecordingID > 0 Then
                Throw New IO.IOException("Invalid Recording ID")
            End If


            Try
                ''=======================
                ''Load the layout manager
                ''=======================
                ' Me.Layout = TrackSpyLib.Layout.LayoutManager.LoadFromFile("C:\Users\andy.pye\Desktop\VB Projects\TrackSpyRender\TestData\testconfig1.xml")
                Me.Layout = TrackSpy.Layout.LayoutManager.LoadFromFile(Layout)
                If Me.Layout Is Nothing Then Throw New IO.IOException("Unable to load " & Layout)
                ''Validate the mapping
                'For Each Tobj In Me.Layout.Layouts
                '    Dim Result As String = ValidateMappingNames(Me.Layout.Layouts(0).DataMapping)
                '    If Result <> "" Then
                '        Throw New Exceptions.DataMappingException(Result)
                '    End If
                'Next

            Catch ex As Exception
                Throw New IO.IOException(ex.Message)
            End Try

            '============================
            'First load the recording job
            '============================
            Dim RecordingJob As String = ReadData("https://www.trackspy.com/recordings/ajax_get_render_job_data/" & RecordingID & "")
            If RecordingJob <> "" Then
                Dim JJob = JValue.Parse(RecordingJob)
                JJob = JJob.Root.First
                TrackID = CInt(JJob.Item("track_id"))
               
                VideoFile = CStr(JJob.Item("fname"))
            Else
                Throw New Exceptions.DataReaderException("No job found!" & RecordingID)
            End If

            'Download video
            Dim TempFile As String = IO.Path.Combine(TempFolder & "\" & RecordingID, "video.mp4")
            If Not My.Computer.FileSystem.FileExists(TempFile) Then
                DownloadVideo(VideoFile, IO.Path.Combine(TempFolder & "\" & RecordingID, "video.mp4"))
            End If

            If Not My.Computer.FileSystem.FileExists(TempFile) Then
                Throw New Exceptions.DataReaderException("Cannot download Video File!!")
            End If

            If Not My.Computer.FileSystem.FileExists(TempFolder & "\" & RecordingID & "\ffmpeg_001.png") Then
                Me.ExtractFrames(ffmpg, TempFile)
            End If
            ' 

            'Load the recordings
            'Dim RecordingData As String = ReadData("https://www.trackspy.com/recordings/ajax_getdata/67/0")

            '==============
            'Load the track
            '==============
            'fileContents = My.Computer.FileSystem.ReadAllText("C:\Users\andy.pye\Desktop\VB Projects\TrackSpyRender\TestData\data.json")
            Dim RecordingData As String = ReadData("https://www.trackspy.com/recordings/ajax_getfulldata/" & RecordingID & "")
            If RecordingJob = "" Then
                Throw New Exceptions.DataReaderException("Missing Recording Data!" & RecordingID)
            End If


            '=================
            'Load the plugings
            '=================
            'Speed
            Dim Speed As PluginDial = New plug_Ins.PluginDial()
            'Rpm
            Dim Rpm As PluginDial = New plug_Ins.PluginDial()
            'Temp
            Dim Temp As PluginDial = New plug_Ins.PluginDial()
            'Map
            Dim Map As PluginMap = New plug_Ins.PluginMap()
            'Map
            Dim Img As PluginImg = New plug_Ins.PluginImg()
            'FORCE
            Dim FORCE As PluginForce = New plug_Ins.PluginForce()
            'TEXT
            Dim LapTimer As PluginText = New plug_Ins.PluginText()

            If TrackID > 0 Then
                Dim TrackData As String = ReadData("https://www.trackspy.com/tracks/ajax_getdata/" & TrackID & "/0")
                Map.LoadTrackData(TrackData)
            End If


            Dim currframe As Integer = -1
            ' Dim LoadFrame As Integer = currframe + 1
            Dim json = JValue.Parse(RecordingData)
            If json IsNot Nothing Then

                json = json.Root.First
                Dim Display As Integer = 0

                Do
                    currframe = currframe + 1
                    Display = Display + 1
                    If Display > 25 Then
                        Display = 0
                        RaiseEvent RenderingFrame(currframe)
                    End If

                    'If currframe = 50 Then Stop
                    Dim LoadFrame As Integer = currframe + 1
                    Dim filename As String = TempFolder & "\" & RecordingID & "\ffmpeg_" & LoadFrame.ToString("000").ToString & ".png"
                    If Not My.Computer.FileSystem.FileExists(filename) Then
                        Exit Do
                    End If



                    StartData = CSng(json.Item("start"))
                    EndData = CSng(json.Item("end"))

                    CurrSpeedVal = CSng(json.Item("speed"))
                    CurrTempVal = CSng(json.Item("engine_coolant_temp"))
                    CurrRPMVal = CSng(json.Item("rpm"))
                    CurrfxVal = CSng(json.Item("x_acceleration"))
                    CurrfyVal = CSng(json.Item("y_acceleration"))
                    CurrlatVal = CSng(json.Item("latitude"))
                    CurrlngVal = CSng(json.Item("longitude"))

                    lap_count = CSng(json.Item("lap_count"))
                    last_lap = CStr(json.Item("last_lap"))
                    best_lap = CStr(json.Item("best_lap"))
                    total_lap_run = CSng(json.Item("total_lap_run"))
                    sub_time = CSng(json.Item("total_lap_run"))


                    If LastLngVal = 0 Then LastLngVal = CurrlngVal
                    If LastLatVal = 0 Then LastLatVal = CurrlatVal

                    Dim a As Single = (currframe * FrameTime)
                    If EndData > (currframe * FrameTime) Then
                        Speedval = CSng(Interpol(LastSpeedVal, CurrSpeedVal, StartData, EndData, (currframe * FrameTime)))
                        Tempval = CSng(Interpol(LastTempVal, CurrTempVal, StartData, EndData, (currframe * FrameTime)))
                        RPMval = CSng(Interpol(LastRPMVal, CurrRPMVal, StartData, EndData, (currframe * FrameTime)))
                        Lngval = CSng(Interpol(LastLngVal, CurrlngVal, StartData, EndData, (currframe * FrameTime)))
                        Latval = CSng(Interpol(LastLatVal, CurrlatVal, StartData, EndData, (currframe * FrameTime)))
                        fxVal = CSng(Interpol(LastfxVal, CurrfxVal, StartData, EndData, (currframe * FrameTime)))
                        fyVal = CSng(Interpol(LastfyVal, CurrfyVal, StartData, EndData, (currframe * FrameTime)))
                    Else
                        LastSpeedVal = CurrSpeedVal
                        LastTempVal = CurrTempVal
                        LastRPMVal = CurrRPMVal
                        LastLngVal = CurrlngVal
                        LastLatVal = CurrlatVal

                        LastfxVal = CurrfxVal
                        LastfyVal = CurrfyVal


                        json = json.Next
                        If json IsNot Nothing Then
                            Exit Do
                        End If

                        StartData = CSng(json.Item("start"))
                        EndData = CSng(json.Item("end"))

                        CurrSpeedVal = CSng(json.Item("speed"))
                        CurrTempVal = CSng(json.Item("engine_coolant_temp"))
                        CurrRPMVal = CSng(json.Item("rpm"))
                        CurrlatVal = CSng(json.Item("latitude"))
                        CurrlngVal = CSng(json.Item("longitude"))
                        CurrfxVal = CSng(json.Item("x_acceleration"))
                        CurrfyVal = CSng(json.Item("y_acceleration"))

                        Speedval = CSng(Interpol(LastSpeedVal, CurrSpeedVal, StartData, EndData, (currframe * FrameTime)))
                        Tempval = CSng(Interpol(LastTempVal, CurrTempVal, StartData, EndData, (currframe * FrameTime)))
                        RPMval = CSng(Interpol(LastRPMVal, CurrRPMVal, StartData, EndData, (currframe * FrameTime)))
                        Lngval = CSng(Interpol(LastLngVal, CurrlngVal, StartData, EndData, (currframe * FrameTime)))
                        Latval = CSng(Interpol(LastLatVal, CurrlatVal, StartData, EndData, (currframe * FrameTime)))
                        fxVal = CSng(Interpol(LastfxVal, CurrfxVal, StartData, EndData, (currframe * FrameTime)))
                        fyVal = CSng(Interpol(LastfyVal, CurrfyVal, StartData, EndData, (currframe * FrameTime)))
                    End If

                    '====================
                    'Load the video frame
                    '====================
                    Try


                        Dim buffer As Byte() = File.ReadAllBytes(filename)
                        Dim stream As MemoryStream = New MemoryStream(buffer)
                        Dim myBitmap As Bitmap = CType(Bitmap.FromStream(stream), Bitmap)
                        ' Dim myBitmap2 = myBitmap
                        Dim myBitmap2 = New Bitmap(myBitmap.Width, myBitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
                        g = Graphics.FromImage(myBitmap2)
                        g.DrawImage(myBitmap, 0, 0)


                        Dim dc As New PlugIn.PluginContext()
                        'dc.DataConnector = TrackData
                        dc.g = g


                        For Each oItem As TrackSpy.Layout.LayoutItem In Me.Layout.Layouts
                            oItem.SetScreenSize(New Size(myBitmap2.Width, myBitmap2.Height))
                            If oItem.Name.ToUpper = "SPEED" Then
                                dc.ScreenSize = New Size(myBitmap2.Width, myBitmap2.Height)
                                dc.Layout = oItem
                                Speed.Config(oItem.PlugInConfig)
                                Speed.Value = Speedval
                                Speed.PerformAction(dc)
                            ElseIf oItem.Name.ToUpper = "FORCE" Then
                                dc.ScreenSize = New Size(myBitmap2.Width, myBitmap2.Height)
                                dc.Layout = oItem
                                FORCE.Config(oItem.PlugInConfig)
                                FORCE.Value = fxVal
                                FORCE.Value2 = fyVal
                                FORCE.PerformAction(dc)
                            ElseIf oItem.Name.ToUpper = "RPM" Then
                                dc.ScreenSize = New Size(myBitmap2.Width, myBitmap2.Height)
                                dc.Layout = oItem
                                Rpm.Config(oItem.PlugInConfig)
                                Rpm.Value = RPMval
                                Rpm.PerformAction(dc)
                            ElseIf oItem.Name.ToUpper = "TEMP" Then
                                dc.ScreenSize = New Size(myBitmap2.Width, myBitmap2.Height)
                                dc.Layout = oItem
                                Temp.Config(oItem.PlugInConfig)
                                Temp.Value = Tempval
                                Temp.PerformAction(dc)
                            ElseIf oItem.Name.ToUpper = "MAP" Then
                                If TrackID > 0 Then
                                    dc.ScreenSize = New Size(myBitmap2.Width, myBitmap2.Height)
                                    dc.Layout = oItem
                                    Map.Config(oItem.PlugInConfig)
                                    Map.Lng = Lngval
                                    Map.Lat = Latval
                                    Map.PerformAction(dc)
                                End If
                            ElseIf oItem.Name.ToUpper = "IMG" Then
                                dc.ScreenSize = New Size(myBitmap2.Width, myBitmap2.Height)
                                dc.Layout = oItem
                                Img.Config(oItem.PlugInConfig)
                                Img.PerformAction(dc)
                            ElseIf oItem.Name.ToUpper = "TEXT" Then
                                dc.ScreenSize = New Size(myBitmap2.Width, myBitmap2.Height)
                                dc.Layout = oItem
                                LapTimer.Lap = CStr(lap_count)
                                LapTimer.Last = CStr(last_lap)
                                If sub_time > -1 Then
                                    LapTimer.Current = SecToTime(sub_time - (currframe * FrameTime))
                                Else
                                    LapTimer.Current = SecToTime((currframe * FrameTime))
                                End If
                                LapTimer.Best = best_lap
                                LapTimer.Config(oItem.PlugInConfig)
                                LapTimer.PerformAction(dc)
                            End If
                        Next
                        dc = Nothing


                        'Save
                        If Preview = False Then
                            myBitmap2.Save(filename)
                        Else
                            If PreviewBMP IsNot Nothing Then
                                PreviewBMP = myBitmap2
                                Exit Do
                            End If
                        End If

                        'myBitmap2.Dispose()
                        'myBitmap.Dispose()
                        stream.Dispose()
                    Catch ex As Exception


                    End Try


                    'Application.DoEvents()
                Loop
            End If
            Speed = Nothing
            Rpm = Nothing
            Temp = Nothing

            If Preview = False Then
                Me.BuildVideo(ffmpg, TempFile)
            End If

            RaiseEvent Finished()
            '======================= 
            '            Dim dc As New PlugIn.PluginContext()
            '            dc.DataConnector = TrackData
            '            dc.g = G
            '            dc.ScreenSize = New Size(reader.Width, reader.Height)
            '            dc.Layout = oItem
            '            plugin = PlugIns.Item(oItem.Name)
            '            ' plugin = CType(myType, IPlugin)
            '            '        'Write our frame back
            '            plugin.PerformAction(dc)
            '            plugin = Nothing
            '            dc = Nothing

            ''Try
            ''==========================================
            '' create instance of video reader AND WRITE
            ''==========================================
            'reader = New VideoFileReader()
            'WRITER = New VideoFileWriter()

            ''================
            '' open video file
            ''================
            'reader.Open(InputVideo)

            ''===================
            ''OPEN THE WRITE FILE
            ''===================
            'WRITER.Open(OutputVideo, reader.Width, reader.Height, reader.FrameRate, VideoCodec.MPEG4)

            ''=======================
            ''This opend the database
            ''=======================
            'TrackData = New TrackSpyLib.Data.DataReader(Database, reader.FrameCount)


            ''========================
            ''Now Presload the plugins
            ''========================
            'For Each oItem As TrackSpyLib.Layout.LayoutItem In Me.Layout.Layouts
            '    Dim Path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, oItem.PlugInName)
            '    'Dim Path As String = IO.Path.Combine("C:\Users\andrew.pye\Desktop\VbProject\TrackSpyRender\DialRender\bin\Debug", oItem.PlugInName)
            '    '=====================
            '    'Set the screen height
            '    '=====================
            '    oItem.SetScreenSize(New Size(reader.Width, reader.Height))
            '    If My.Computer.FileSystem.FileExists(Path) Then
            '        '==================
            '        'Save our assembily
            '        '==================
            '        If PlugInsAsembily.ContainsKey(oItem.PlugInName) = False Then
            '            asm = Assembly.LoadFrom(Path)
            '            If asm IsNot Nothing Then
            '                PlugInsAsembily.Add(oItem.PlugInName, asm)
            '            Else
            '                Throw New Exceptions.PluginException("Cannot Load PlugIns Asembily")
            '            End If
            '        End If
            '        '==================
            '        'Now create a class
            '        '==================
            '        If PlugIns.ContainsKey(oItem.Name) = False Then
            '            asm = PlugInsAsembily.Item(oItem.PlugInName)
            '            If asm IsNot Nothing Then
            '                myType = asm.GetType(asm.GetName.Name + ".Plugin")
            '                Dim implementsIPlugin As Boolean = GetType(IPlugin).IsAssignableFrom(myType)
            '                If implementsIPlugin Then
            '                    plugin = CType(Activator.CreateInstance(myType), IPlugin)
            '                    plugin.Config(oItem.PlugInConfig)
            '                    'Add to the list
            '                    PlugIns.Add(oItem.Name, plugin)
            '                End If
            '            End If
            '        End If
            '    Else
            '        Throw New Exceptions.PluginException("Plug in " & oItem.PlugInName & " Does not exist")
            '    End If

            'Next oItem

            ''==============================
            ''Raise event Strating Rendering
            ''==============================
            'RaiseEvent StaringFrame(reader.FrameCount)

            ''=========
            ''Main Loop
            ''=========
            'For i As Integer = 0 To 1000 'reader.FrameCount - 1
            '    '==================
            '    'Increment the data
            '    '==================
            '    If TrackData.Read() = True Then
            '        '    '==========================
            '        '    'Rendering First Frame
            '        '    '==========================
            '        RaiseEvent RenderingFrame(i)

            '        Dim videoFrame As Bitmap = reader.ReadVideoFrame()

            '        '    'videoFrame.RotateFlip(RotateFlipType.Rotate90FlipNone)
            '        '    ' process the frame somehow
            '        '    ' ...
            '        Dim G As Graphics = Graphics.FromImage(videoFrame)

            '        '    '==============
            '        '    'Draw the layout
            '        '    '==============
            '        For Each oItem As TrackSpyLib.Layout.LayoutItem In Me.Layout.Layouts
            '            '        '=======================
            '            '        'Set up the data context
            '            '        '======================= 
            '            Dim dc As New PlugIn.PluginContext()
            '            dc.DataConnector = TrackData
            '            dc.g = G
            '            dc.ScreenSize = New Size(reader.Width, reader.Height)
            '            dc.Layout = oItem
            '            plugin = PlugIns.Item(oItem.Name)
            '            ' plugin = CType(myType, IPlugin)
            '            '        'Write our frame back
            '            plugin.PerformAction(dc)
            '            plugin = Nothing
            '            dc = Nothing
            '        Next oItem

            '        '    '====================
            '        '    '====================
            '        WRITER.WriteVideoFrame(videoFrame)

            '        G.Dispose()
            '        videoFrame.Dispose()
            '        videoFrame = Nothing
            '        G = Nothing
            '    End If
            'Next

            '    reader.Close()
            '    WRITER.Close()
            '    ' Catch ex As Exception
            '    ' Throw New Exception(ex.Message)
            '    'Finally
            '    '=================
            '    'Clean up the data
            '    '=================
            '    If reader IsNot Nothing Then
            '        If reader.IsOpen Then
            '            reader.Close()
            '        End If
            '        reader.Dispose()
            '    End If
            '    If WRITER IsNot Nothing Then
            '        If WRITER.IsOpen Then
            '            WRITER.Close()
            '        End If
            '        WRITER.Dispose()
            '    End If
            '    asm = Nothing
            '    myType = Nothing
            '    plugin = Nothing
            '    ' End Try

            '    RaiseEvent Finished()

        End Sub
        Private Function Interpol(ByVal A1 As Single, ByVal B1 As Single, ByVal A2 As Single, ByVal B2 As Single, ByVal Value As Single) As Single
            If A1 = B1 Then
                Return A1
            End If
            Return ((B1 - A1) / (B2 - A2) * (Value - A2)) + A1
        End Function
        Private Function GetValue(ByVal PropertyName As String) As Single
            Try
                Dim Pi As FieldInfo = TrackData.GetType.GetField(PropertyName)
                Return CSng(Pi.GetValue(TrackData))
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace