Imports TrackSpy.PlugIn
Imports System.Drawing
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports TrackSpy.Exceptions
Imports System
Namespace plug_Ins


    Public Class PluginText
        Inherits TrackSpy.PlugIn.BasePlugin
        Implements IPlugin
#Region "Fields"

        Private _CurrentFont As String
        Private _CurrentFontSize As String
        Private _Current As String
        Private _LapFont As String
        Private _LapFontSize As String
        Private _Lap As String


        Private _BestFont As String
        Private _BestFontSize As String
        Private _Best As String
        Private _LastFont As String
        Private _LastFontSize As String
        Private _Last As String

#End Region

#Region "Property"
        Public Property LapFont() As String
            Get
                Return _LapFont
            End Get
            Set(ByVal value As String)
                _LapFont = value

            End Set
        End Property
        Public Property LapFontSize() As String
            Get
                Return _LapFontSize
            End Get
            Set(ByVal value As String)
                _LapFontSize = value

            End Set
        End Property
        Public Property Lap() As String
            Get
                Return _Lap
            End Get
            Set(ByVal value As String)
                _Lap = value

            End Set
        End Property
        Public Property CurrentFont() As String
            Get
                Return _CurrentFont
            End Get
            Set(ByVal value As String)
                _CurrentFont = value

            End Set
        End Property
        Public Property CurrentFontSize() As String
            Get
                Return _CurrentFontSize
            End Get
            Set(ByVal value As String)
                _CurrentFontSize = value

            End Set
        End Property
        Public Property Current() As String
            Get
                Return _Current
            End Get
            Set(ByVal value As String)
                _Current = value

            End Set
        End Property
        Public Property BestFont() As String
            Get
                Return _BestFont
            End Get
            Set(ByVal value As String)
                _BestFont = value

            End Set
        End Property
        Public Property BestFontSize() As String
            Get
                Return _BestFontSize
            End Get
            Set(ByVal value As String)
                _BestFontSize = value

            End Set
        End Property
        Public Property Best() As String
            Get
                Return _Best
            End Get
            Set(ByVal value As String)
                _Best = value

            End Set
        End Property
        Public Property LastFont() As String
            Get
                Return _LastFont
            End Get
            Set(ByVal value As String)
                _LastFont = value

            End Set
        End Property
        Public Property LastFontSize() As String
            Get
                Return _LastFontSize
            End Get
            Set(ByVal value As String)
                _LastFontSize = value

            End Set
        End Property
        Public Property Last() As String
            Get
                Return _Last
            End Get
            Set(ByVal value As String)
                _Last = value

            End Set
        End Property
#End Region


        Protected Overrides ReadOnly Property Name() As String
            Get
                Return "ImgRender"
            End Get
        End Property
        Public Overrides Sub PerformAction(ByVal context As TrackSpy.PlugIn.IPluginContext)
            ' context.g.DrawString(Me.Text, New Font(Me.Font, Me.FontSize), Brushes.White, context.Layout.GetX, context.Layout.GetY)

            Dim lapsize = context.g.MeasureString("Lap: " & Me.Lap, New Font(Me.LapFont, Me.LapFontSize))
            Dim currentsize = context.g.MeasureString("Current: " & Me.Current, New Font(Me.CurrentFont, Me.CurrentFontSize))
            Dim lastsize = context.g.MeasureString("Last: " & Me.Last, New Font(Me.LastFont, Me.LapFontSize))
            Dim bestsize = context.g.MeasureString("Best: " & Me.Best, New Font(Me.BestFont, Me.LapFontSize))

            Dim MaxWidth = Math.Max(lapsize.Width, currentsize.Width)
            MaxWidth = Math.Max(MaxWidth, lastsize.Width)
            MaxWidth = Math.Max(MaxWidth, bestsize.Width)

            Dim MaxHeight = lapsize.Height + currentsize.Height + lastsize.Height + bestsize.Height

            MaxWidth = Math.Max(MaxWidth, context.Layout.GetWidth)
         
            context.g.FillRectangle(New SolidBrush(Color.FromArgb(150, 0, 0, 0)), New RectangleF(context.Layout.GetX, context.Layout.GetY, MaxWidth, MaxHeight))
            'context.g.FillRectangle(New SolidBrush(Color.Black), New RectangleF(context.Layout.GetX, context.Layout.GetY, MaxWidth, MaxHeight))

            context.g.DrawString("Lap: " & Me.Lap, New Font(Me.LapFont, Me.LapFontSize), Brushes.White, context.Layout.GetX, context.Layout.GetY)
            context.g.DrawString("Current: " & Me.Current, New Font(Me.CurrentFont, Me.CurrentFontSize), Brushes.White, context.Layout.GetX, context.Layout.GetY + lapsize.Height)
            context.g.DrawString("Last: " & Me.Last, New Font(Me.LastFont, Me.LastFontSize), Brushes.White, context.Layout.GetX, context.Layout.GetY + lapsize.Height + currentsize.Height)
            context.g.DrawString("Best: " & Me.Best, New Font(Me.BestFont, Me.BestFontSize), Brushes.White, context.Layout.GetX, context.Layout.GetY + lapsize.Height + currentsize.Height + lastsize.Height)

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