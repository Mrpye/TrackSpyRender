Imports TrackSpy.PlugIn
Imports TrackSpy
Imports TrackSpy.Data
Imports System.Drawing
Imports System
Imports TrackSpy.Exceptions
Imports System.Xml.Serialization
Imports System.Xml

Namespace plug_Ins
    Public Class PluginForce
        Inherits TrackSpy.PlugIn.BasePlugin
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

        Private _ImagePath As String
        Private _trackMap As Bitmap

        Private _Value2 As Single
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
        Public Property Value2() As Single
            Get
                Return _Value2
            End Get
            Set(ByVal value As Single)
                _Value2 = value
            End Set
        End Property
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

        Public Sub LoadImg()
            If My.Computer.FileSystem.FileExists(_ImagePath) Then
                _trackMap = New Bitmap(_ImagePath)
            Else
                Throw New PluginException("Missing image" & _ImagePath)
            End If
        End Sub
        Protected Overrides ReadOnly Property Name() As String
            Get
                Return "DialForce"
            End Get
        End Property
        Public Overrides Sub PerformAction(ByVal context As TrackSpy.PlugIn.IPluginContext)
            'Set up
            'Map data
            MyBase.PerformAction(context)


            '_Dial = New Dials.Renderer.Dial_Type1(CSng(context.Layout.GetWidth), CSng(context.Layout.GetHeight))

            ''Assign value
            '_Dial.Font = New Drawing.Font("Arial", 12)
            '_Dial.ForeColor = Drawing.Color.White
            '_Dial.FromAngle = Me.FromAngle
            ''_Dial.Height = Me.Height
            '_Dial.MaxValue = Me.MaxValue
            '_Dial.MinValue = Me.MinValue
            '_Dial.NoOfDivisions = Me.NoOfDivisions
            '_Dial.NoOfSubDivisions = Me.NoOfSubDivisions
            '_Dial.RedLineMPH = Me.RedLineMPH
            '_Dial.ToAngle = Me.ToAngle
            '_Dial.Value = Me.Value
            ''_Dial.Width = Me.Width
            '_Dial.BackImg = Me.FixPath(Me.BackImg)

            '_Dial.Render(context.g, context.Layout.GetX(), context.Layout.GetY(), , , False)

            If _trackMap IsNot Nothing Then

                context.g.DrawImage(_trackMap, New RectangleF(context.Layout.GetX(), context.Layout.GetY(), context.Layout.GetWidth(), context.Layout.GetHeight()), New RectangleF(0, 0, _trackMap.Width, _trackMap.Height), GraphicsUnit.Pixel)
            End If

            'context.g.DrawImage(context.g, New RectangleF(context.Layout.GetX(), context.Layout.GetY(), context.Layout.GetWidth(), context.Layout.GetHeight()), New RectangleF(0, 0, _trackMap.Width, _trackMap.Height), GraphicsUnit.Pixel)



            context.g.FillEllipse(Brushes.Red, New RectangleF(context.Layout.GetX() - 5 + (context.Layout.GetWidth() * 0.5) + Me.Value, context.Layout.GetY() - 5 + (context.Layout.GetHeight() * 0.5) + Me.Value2, 10, 10))

            ' _Dial = Nothing
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