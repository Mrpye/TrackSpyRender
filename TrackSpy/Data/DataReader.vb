Imports System

Namespace Data
    Public Class DataReader
#Region "Fields"
#Region "Data"
        Private OBD_EngineRPM() As Integer
        Private OBD_VehicleSpeed() As Integer
        Private OBD_ThrottlePosition() As Integer
        Private OBD_BarometricPressure() As Integer
        Private OBD_AmbientAirTemperature() As Integer
        Private OBD_ActualEnginePercentTorque() As Integer
        Private OBD_EngineReferenceTorque() As Integer
        Private OBD_IntakeAirTemperature() As Integer

        Private GPS_Longitude() As Integer
        Private GPS_Latitude() As Integer
        Private GPS_Altitude() As Integer
        Private GPS_Course() As Integer
        Private GPS_Speed() As Integer

        Private SENS_XAcceleration() As Integer
        Private SENS_YAcceleration() As Integer
        Private SENS_ZAcceleration() As Integer
        Private SENS_XGyroscope() As Integer
        Private SENS_YGyroscope() As Integer
        Private SENS_ZGyroscope() As Integer

        
#End Region
#Region "Max Min Values"
        Private OBD_EngineRPM_Max As Integer
        Private OBD_VehicleSpeed_Max As Integer
        Private OBD_ThrottlePosition_Max As Integer
        Private OBD_BarometricPressure_Max As Integer
        Private OBD_AmbientAirTemperature_Max As Integer
        Private OBD_ActualEnginePercentTorque_Max As Integer
        Private OBD_EngineReferenceTorque_Max As Integer
        Private OBD_IntakeAirTemperature_Max As Integer

        Public GPS_Longitude_Max As Integer
        Public GPS_Latitude_Max As Integer
        Public GPS_Altitude_Max As Integer
        Public GPS_Course_Max As Integer
        Public GPS_Speed_Max As Integer

        Public SENS_XAcceleration_Max As Integer
        Public SENS_YAcceleration_Max As Integer
        Public SENS_ZAcceleration_Max As Integer
        Public SENS_XGyroscope_Max As Integer
        Public SENS_YGyroscope_Max As Integer
        Public SENS_ZGyroscope_Max As Integer

        Public SENS_XAcceleration_Min As Integer
        Public SENS_YAcceleration_Min As Integer
        Public SENS_ZAcceleration_Min As Integer
        Public SENS_XGyroscope_Min As Integer
        Public SENS_YGyroscope_Min As Integer
        Public SENS_ZGyroscope_Min As Integer
#End Region
#Region "Reasults"

        Public OBD_EngineRPM_Val As Integer
        Public OBD_VehicleSpeed_Val As Integer
        Public OBD_ThrottlePosition_Val As Integer
        Public OBD_BarometricPressure_Val As Integer
        Public OBD_AmbientAirTemperature_Val As Integer
        Public OBD_ActualEnginePercentTorque_Val As Integer
        Public OBD_EngineReferenceTorque_Val As Integer
        Public OBD_IntakeAirTemperature_Val As Integer

        Public GPS_Longitude_Val As Integer
        Public GPS_Latitude_Val As Integer
        Public GPS_Altitude_Val As Integer
        Public GPS_Course_Val As Integer
        Public GPS_Speed_Val As Integer

        Public SENS_XAcceleration_Val As Integer
        Public SENS_YAcceleration_Val As Integer
        Public SENS_ZAcceleration_Val As Integer
        Public SENS_XGyroscope_Val As Integer
        Public SENS_YGyroscope_Val As Integer
        Public SENS_ZGyroscope_Val As Integer
#End Region

        Public ConnStr As String = ""
        Public ArraySteps As Integer = 0
        Public count As Integer = 0
        Public CurrSegment As Integer = 0

#End Region


#Region "methods"
#Region "Data Reader"
        Private Sub Read_SENS_Data()
            Try
                'Dim TSENS_XAcceleration As New List(Of Integer)
                'Dim TSENS_YAcceleration As New List(Of Integer)
                'Dim TSENS_ZAcceleration As New List(Of Integer)
                'Dim TSENS_XGyroscope As New List(Of Integer)
                'Dim TSENS_YGyroscope As New List(Of Integer)
                'Dim TSENS_ZGyroscope As New List(Of Integer)

                'Dim Tval As Integer = 0

                'Dim cnn As New SQLiteConnection(ConnStr)
                'cnn.Open()
                'Dim mycommand As New SQLiteCommand(cnn)
                'mycommand.CommandText = "select * from SENSORS"
                'Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
                'Do While reader.Read
                '    Tval = reader.GetInt32(reader.GetOrdinal("XAcceleration"))
                '    TSENS_XAcceleration.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("YAcceleration"))
                '    TSENS_YAcceleration.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("ZAcceleration"))
                '    TSENS_ZAcceleration.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("XGyroscope"))
                '    TSENS_XGyroscope.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("YGyroscope"))
                '    TSENS_YGyroscope.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("ZGyroscope"))
                '    TSENS_ZGyroscope.Add(Tval)

                'Loop
                'reader.Close()
                'cnn.Close()

                'Me.SENS_XAcceleration = TSENS_XAcceleration.ToArray
                'Me.SENS_YAcceleration = TSENS_YAcceleration.ToArray
                'Me.SENS_ZAcceleration = TSENS_ZAcceleration.ToArray
                'Me.SENS_XGyroscope = TSENS_XGyroscope.ToArray
                'Me.SENS_YGyroscope = TSENS_YGyroscope.ToArray
                'Me.SENS_ZGyroscope = TSENS_ZGyroscope.ToArray

                ''Get the max
                'Me.SENS_XAcceleration_Max = SENS_XAcceleration.Max
                'Me.SENS_YAcceleration_Max = SENS_XAcceleration.Max
                'Me.SENS_ZAcceleration_Max = SENS_XAcceleration.Max
                'Me.SENS_XGyroscope_Max = SENS_XAcceleration.Max
                'Me.SENS_YGyroscope_Max = SENS_XAcceleration.Max
                'Me.SENS_ZGyroscope_Max = SENS_XAcceleration.Max


                'Me.SENS_XAcceleration_Min = SENS_XAcceleration.Min
                'Me.SENS_YAcceleration_Min = SENS_XAcceleration.Min
                'Me.SENS_ZAcceleration_Min = SENS_XAcceleration.Min
                'Me.SENS_XGyroscope_Min = SENS_XAcceleration.Min
                'Me.SENS_YGyroscope_Min = SENS_XAcceleration.Min
                'Me.SENS_ZGyroscope_Min = SENS_XAcceleration.Min
            Catch e As Exception
           
                Throw New Exception(e.Message)
            End Try
        End Sub
        Private Sub Read_GPS_Data()
            Try
                'Dim TGPS_Longitude As New List(Of Integer)
                'Dim TGPS_Latitude As New List(Of Integer)
                'Dim TGPS_Altitude As New List(Of Integer)
                'Dim TGPS_Course As New List(Of Integer)
                'Dim TGPS_Speed As New List(Of Integer)

                'Dim Tval As Integer = 0

                'Dim cnn As New SQLiteConnection(ConnStr)
                'cnn.Open()
                'Dim mycommand As New SQLiteCommand(cnn)
                'mycommand.CommandText = "select * from GPS"
                'Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
                'Do While reader.Read
                '    Tval = reader.GetInt32(reader.GetOrdinal("Longitude"))
                '    TGPS_Longitude.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("Latitude"))
                '    TGPS_Latitude.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("Altitude"))
                '    TGPS_Altitude.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("Course"))
                '    TGPS_Course.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("Speed"))
                '    TGPS_Speed.Add(Tval)
                'Loop
                'reader.Close()
                'cnn.Close()

                'Me.GPS_Longitude = TGPS_Longitude.ToArray
                'Me.GPS_Latitude = TGPS_Latitude.ToArray
                'Me.GPS_Altitude = TGPS_Altitude.ToArray
                'Me.GPS_Course = TGPS_Course.ToArray
                'Me.GPS_Speed = TGPS_Speed.ToArray
                ''Get the max
                'Me.GPS_Longitude_Max = GPS_Longitude.Max
                'Me.GPS_Latitude_Max = GPS_Latitude.Max
                'Me.GPS_Altitude_Max = GPS_Altitude.Max
                'Me.GPS_Course_Max = GPS_Course.Max
                'Me.GPS_Speed_Max = GPS_Speed.Max
            Catch e As Exception
                Throw New Exception(e.Message)
            End Try
        End Sub
        Private Sub Read_OBD_Data()
            Try
                'Dim TOBD_EngineRPM As New List(Of Integer)
                'Dim TOBD_VehicleSpeed As New List(Of Integer)
                'Dim TOBD_ThrottlePosition As New List(Of Integer)
                'Dim TOBD_BarometricPressure As New List(Of Integer)
                'Dim TOBD_AmbientAirTemperature As New List(Of Integer)
                'Dim TOBD_ActualEnginePercentTorque As New List(Of Integer)
                'Dim TOBD_EngineReferenceTorque As New List(Of Integer)
                'Dim TOBD_IntakeAirTemperature As New List(Of Integer)

                'Dim Tval As Integer = 0

                'Dim cnn As New SQLiteConnection(ConnStr)
                'cnn.Open()
                'Dim mycommand As New SQLiteCommand(cnn)
                'mycommand.CommandText = "select * from OBD"
                'Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
                'Do While reader.Read
                '    Tval = reader.GetInt32(reader.GetOrdinal("EngineRPM"))
                '    TOBD_EngineRPM.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("VehicleSpeed"))
                '    TOBD_VehicleSpeed.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("ThrottlePosition"))
                '    TOBD_ThrottlePosition.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("BarometricPressure"))
                '    TOBD_BarometricPressure.Add(Tval)
                '    Tval = reader.GetInt32(reader.GetOrdinal("AmbientAirTemperature"))
                '    TOBD_AmbientAirTemperature.Add(Tval)
                '    'Tval = reader.GetInt32(reader.GetOrdinal("ActualEnginePercentTorque"))
                '    TOBD_ActualEnginePercentTorque.Add(0)
                '    'Tval = reader.GetInt32(reader.GetOrdinal("EngineReferenceTorque"))
                '    TOBD_EngineReferenceTorque.Add(0)
                '    ' Tval = reader.GetInt32(reader.GetOrdinal("IntakeAirTemperature"))
                '    TOBD_IntakeAirTemperature.Add(0)
                'Loop
                'reader.Close()
                'cnn.Close()

                'Me.OBD_EngineRPM = TOBD_EngineRPM.ToArray
                'Me.OBD_VehicleSpeed = TOBD_VehicleSpeed.ToArray
                'Me.OBD_ThrottlePosition = TOBD_ThrottlePosition.ToArray
                'Me.OBD_BarometricPressure = TOBD_BarometricPressure.ToArray
                'Me.OBD_AmbientAirTemperature = TOBD_AmbientAirTemperature.ToArray
                'Me.OBD_ActualEnginePercentTorque = TOBD_ActualEnginePercentTorque.ToArray
                'Me.OBD_EngineReferenceTorque = TOBD_EngineReferenceTorque.ToArray
                'Me.OBD_IntakeAirTemperature = TOBD_IntakeAirTemperature.ToArray

                ''Get the max
                'Me.OBD_EngineRPM_Max = OBD_EngineRPM.Max
                'Me.OBD_VehicleSpeed_Max = OBD_VehicleSpeed.Max
                'Me.OBD_ThrottlePosition_Max = OBD_ThrottlePosition.Max
                'Me.OBD_BarometricPressure_Max = OBD_BarometricPressure.Max
                'Me.OBD_AmbientAirTemperature_Max = OBD_AmbientAirTemperature.Max
                'Me.OBD_ActualEnginePercentTorque_Max = OBD_ActualEnginePercentTorque.Max
                'Me.OBD_EngineReferenceTorque_Max = OBD_EngineReferenceTorque.Max
                'Me.OBD_IntakeAirTemperature_Max = OBD_IntakeAirTemperature.Max
            Catch e As Exception
                Throw New Exception(e.Message)
            End Try
        End Sub
#End Region

#Region "Methods"
        Private Function ValidateMappingNames(ByVal dm As Data.DataMappingItem()) As String
            Dim DataMapping As String() = {"OBD_EngineRPM_Val", "OBD_VehicleSpeed_Val", "OBD_ThrottlePosition_Val", "OBD_BarometricPressure_Val", "OBD_AmbientAirTemperature_Val", "OBD_ActualEnginePercentTorque_Val", "OBD_EngineReferenceTorque_Val", "OBD_IntakeAirTemperature_Val", "GPS_Longitude_Val", "GPS_Latitude_Val", "GPS_Altitude_Val", "GPS_Course_Val", "GPS_Speed_Val", "SENS_XAcceleration_Val", "SENS_YAcceleration_Val", "SENS_ZAcceleration_Val", "SENS_XGyroscope_Val", "SENS_YGyroscope_Val", "SENS_ZGyroscope_Val"}

            For Each tobj as DataMappingItem In dm


                'If DataMapping.Contains(tobj.MapFrom) Then
               '     Return "Cannot Find Mapping " & tobj.MapFrom
               ' End If
            Next tobj
            Return ""
        End Function
        Private Function Interpol(ByVal A1 As Single, ByVal B1 As Single, ByVal A2 As Single, ByVal B2 As Single, ByVal Value As Single) As Single
            If A1 = B1 Then
                Return A1
            End If
            Return ((B1 - A1) / (B2 - A2) * (Value - A2)) + A1
        End Function
        Public Sub Reset()
            Me.count = 0
            Me.CurrSegment = 0
            ' Me.ArraySteps = 0
        End Sub
        Public Function Read() As Boolean

            Try
                '===============================
                'See if to increment the secment
                '===============================
                If Me.count >= Me.ArraySteps Then
                    Me.CurrSegment = Me.CurrSegment + 1
                    Me.count = 0
                End If

                If Me.CurrSegment > Me.OBD_EngineRPM.Length - 1 Then
                    Return False
                End If

                '=====================
                'Get the Segnment data
                '=====================
                Me.OBD_EngineRPM_Val = CInt(Interpol(Me.OBD_EngineRPM(CurrSegment), Me.OBD_EngineRPM(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.OBD_VehicleSpeed_Val = CInt(Interpol(Me.OBD_VehicleSpeed(CurrSegment), Me.OBD_VehicleSpeed(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.OBD_ThrottlePosition_Val = CInt(Interpol(Me.OBD_ThrottlePosition(CurrSegment), Me.OBD_ThrottlePosition(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.OBD_BarometricPressure_Val = CInt(Interpol(Me.OBD_BarometricPressure(CurrSegment), Me.OBD_BarometricPressure(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.OBD_AmbientAirTemperature_Val = CInt(Interpol(Me.OBD_AmbientAirTemperature(CurrSegment), Me.OBD_AmbientAirTemperature(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.OBD_ActualEnginePercentTorque_Val = CInt(Interpol(Me.OBD_ActualEnginePercentTorque(CurrSegment), Me.OBD_ActualEnginePercentTorque(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.OBD_EngineReferenceTorque_Val = CInt(Interpol(Me.OBD_EngineReferenceTorque(CurrSegment), Me.OBD_EngineReferenceTorque(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.OBD_IntakeAirTemperature_Val = CInt(Interpol(Me.OBD_IntakeAirTemperature(CurrSegment), Me.OBD_IntakeAirTemperature(CurrSegment + 1), 0, Me.ArraySteps, count))

                Me.GPS_Longitude_Val = CInt(Interpol(Me.GPS_Longitude(CurrSegment), Me.GPS_Longitude(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.GPS_Latitude_Val = CInt(Interpol(Me.GPS_Latitude(CurrSegment), Me.GPS_Latitude(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.GPS_Altitude_Val = CInt(Interpol(Me.GPS_Altitude(CurrSegment), Me.GPS_Altitude(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.GPS_Course_Val = CInt(Interpol(Me.GPS_Course(CurrSegment), Me.GPS_Course(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.GPS_Speed_Val = CInt(Interpol(Me.GPS_Speed(CurrSegment), Me.GPS_Speed(CurrSegment + 1), 0, Me.ArraySteps, count))


                Me.SENS_XAcceleration_Val = CInt(Interpol(Me.SENS_XAcceleration(CurrSegment), Me.SENS_XAcceleration(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.SENS_YAcceleration_Val = CInt(Interpol(Me.SENS_YAcceleration(CurrSegment), Me.SENS_YAcceleration(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.SENS_ZAcceleration_Val = CInt(Interpol(Me.SENS_ZAcceleration(CurrSegment), Me.SENS_ZAcceleration(CurrSegment + 1), 0, Me.ArraySteps, count))

                Me.SENS_XGyroscope_Val = CInt(Interpol(Me.SENS_XGyroscope(CurrSegment), Me.SENS_XGyroscope(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.SENS_YGyroscope_Val = CInt(Interpol(Me.SENS_YGyroscope(CurrSegment), Me.SENS_YGyroscope(CurrSegment + 1), 0, Me.ArraySteps, count))
                Me.SENS_ZGyroscope_Val = CInt(Interpol(Me.SENS_ZGyroscope(CurrSegment), Me.SENS_ZGyroscope(CurrSegment + 1), 0, Me.ArraySteps, count))

                '==============
                'Inc the couner
                '==============
                Me.count = Me.count + 1

                '===========
                'All is good
                '===========
                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try


        End Function
#End Region
#End Region

#Region "Constructor"
        Public Sub New(ByVal DatabasePath As String, ByVal FrameCount As Integer)
            If FrameCount = 0 Then Throw New Exception("0 Frames")
            If Not My.Computer.FileSystem.FileExists(DatabasePath) Then
                Throw New IO.IOException("Missing Database File")
            End If
            Me.ConnStr = "data source=" & DatabasePath
            Try
                '========================================
                'FIRST WE READ THE DATA FROM THE DATABASE
                '========================================
                Read_SENS_Data()
                Read_GPS_Data()
                Read_OBD_Data()

                '==================================================
                'Calc the numbers of frames for each sensor segment
                '==================================================
                Me.ArraySteps = CInt(FrameCount / OBD_EngineRPM.Length)

                '=====
                'Reset
                '=====
                Me.Reset()

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub
#End Region

    End Class
End Namespace