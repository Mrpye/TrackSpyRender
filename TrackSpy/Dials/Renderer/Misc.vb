Imports System.Drawing
Imports TrackSpy.Dials.Object
Imports System
Namespace Dials.Renderer


    Public Class Misc
        Inherits Dials.Object.Dial_Base
#Region "Fields"
        Private _Color As Color = Color.Lavender
        Private _CurrentValue As String
        Private _Font As Font = New Font("Arial", 8)
#End Region
#Region "Properties"
        Public Property Font() As Font
            Get
                Return _Font
            End Get
            Set(ByVal value As Font)
                _Font = value
            End Set
        End Property
        Public Property Value() As String
            Get
                Return _CurrentValue
            End Get
            Set(ByVal value As String)
                _CurrentValue = value
            End Set
        End Property
        Public Property Color() As Color
            Get
                Return _Color
            End Get
            Set(ByVal value As Color)
                _Color = value
            End Set
        End Property
#End Region
#Region "CONSTRUCTOR"
        Public Sub New()
        End Sub
        Public Sub New(ByVal Width As Single, ByVal Height As Single)
            'Me.X = X
            ' Me.Y = Y
            Me.Width = Width
            Me.Height = Height
        End Sub
#End Region
#Region "Methods"
        ''' <summary>
        ''' Displays the given number in the 7-Segement format.
        ''' </summary>
        ''' <param name="g"></param>
        Private Sub Render(ByVal g As Graphics)
            Try
                Dim textSize As SizeF = g.MeasureString(Me.Value, Me.Font)
                Dim digiFRectText As New RectangleF(Me.Width \ 2 - textSize.Width / 2, CInt(Math.Truncate(Me.Height / 1.5)), textSize.Width, textSize.Height)
                g.DrawString(Me.Value, Me.Font, New SolidBrush(Me.Color), digiFRectText)
            Catch generatedExceptionName As Exception
            End Try
        End Sub

#End Region
    End Class
End Namespace