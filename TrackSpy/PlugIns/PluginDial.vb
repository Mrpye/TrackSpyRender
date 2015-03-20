Imports TrackSpy.PlugIn
Imports TrackSpy
Imports TrackSpy.Data
Imports System.Drawing
Imports System.Xml.Serialization
Imports System
Namespace plug_Ins
    Public Class PluginDial
        Inherits PlugIn.BasePlugin
        Implements IPlugin
#Region "Fields"
        Private _Dial As Dials.Renderer.Dial_Type1 = Nothing
        Private _NoOfDivisions As Single = 10
        Private _NoOfSubDivisions As Single = 1
        Private _RedLineMPH As Single = 80
        Private _Value As Single = 0


        Private _MinValue As Single = 0
        Private _MaxValue As Single = 130
        Private _FromAngle As Single = 135.0F
        Private _ToAngle As Single = 405.0F
        Private _CurrentValue As Single = 0
        Private _BackImg As String = ""
#End Region

#Region "Property"
        Public Property BackImg() As String
            Get
                Return _BackImg
            End Get
            Set(ByVal value As String)
                _BackImg = value
            End Set
        End Property
        Public Property RedLineMPH() As Single
            Get
                Return _RedLineMPH
            End Get
            Set(ByVal value As Single)
                _RedLineMPH = value
            End Set
        End Property
        Public Property NoOfDivisions() As Single
            Get
                Return _NoOfDivisions
            End Get
            Set(ByVal value As Single)
                _NoOfDivisions = value
            End Set
        End Property
        Public Property NoOfSubDivisions() As Single
            Get
                Return _NoOfSubDivisions
            End Get
            Set(ByVal value As Single)
                _NoOfSubDivisions = value
            End Set
        End Property
        Public Property MinValue() As Single
            Get
                Return _MinValue
            End Get
            Set(ByVal value As Single)
                _MinValue = value
            End Set
        End Property
        Public Property MaxValue() As Single
            Get
                Return _MaxValue
            End Get
            Set(ByVal value As Single)
                _MaxValue = value
            End Set
        End Property
        Public Property FromAngle() As Single
            Get
                Return _FromAngle
            End Get
            Set(ByVal value As Single)
                _FromAngle = value
            End Set
        End Property
        Public Property ToAngle() As Single
            Get
                Return _ToAngle
            End Get
            Set(ByVal value As Single)
                _ToAngle = value
            End Set
        End Property
        Public Property Value() As Single
            Get
                Return _CurrentValue
            End Get
            Set(ByVal value As Single)
                _CurrentValue = value
            End Set
        End Property
#End Region


        Protected Overrides ReadOnly Property Name() As String
            Get
                Return "DialRender"
            End Get
        End Property
        Public Overrides Sub PerformAction(ByVal context As PlugIn.IPluginContext)
            'Set up
            'Map data
            MyBase.PerformAction(context)


            _Dial = New Dials.Renderer.Dial_Type1(CSng(context.Layout.GetWidth), CSng(context.Layout.GetHeight))

            'Assign value
            _Dial.Font = New Font("Arial", 12)
            _Dial.ForeColor = Color.White
            _Dial.FromAngle = Me.FromAngle
            '_Dial.Height = Me.Height
            _Dial.MaxValue = Me.MaxValue
            _Dial.MinValue = Me.MinValue
            _Dial.NoOfDivisions = Me.NoOfDivisions
            _Dial.NoOfSubDivisions = Me.NoOfSubDivisions
            _Dial.RedLineMPH = Me.RedLineMPH
            _Dial.ToAngle = Me.ToAngle
            _Dial.Value = Me.Value
            '_Dial.Width = Me.Width
            _Dial.BackImg = Me.FixPath(Me.BackImg)

            _Dial.Render(context.g, context.Layout.GetX(),context.Layout.GetY(), , , False)

            _Dial = Nothing
            'Render
            'context.g.DrawString(Me.Text, New Drawing.Font("Arial", 12), Drawing.Brushes.Red, context.Layout.X, context.Layout.Y)
            'context.g.DrawString(Me.Age, New Drawing.Font("Arial", 12), Drawing.Brushes.Red, context.Layout.X, context.Layout.Y + 10)
            ' oItem.Dial.Render(max, Value)
            'G.DrawImage(oItem.Dial.GetRenderedImg(), New Point(oItem.Dial.X, oItem.Dial.Y))
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
            Dim mlSerializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(PluginDial))
            Dim writer As IO.StringWriter = New IO.StringWriter()
            mlSerializer.Serialize(writer, Me)
            My.Computer.FileSystem.WriteAllText(filename, writer.ToString, False, System.Text.Encoding.Unicode)
        End Sub
        Public Overrides Sub Config(ByVal Settings() As Layout.SettingItem)
            MyBase.Config(Settings)
        End Sub

    End Class

End Namespace