Imports TrackSpyLib.Dials.Object
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports TrackSpy.Dials.Object
Imports System
Namespace Dials.Renderer
    Public Class Dial_Threshold
        Inherits Round_Dial_Base
#Region "Fields"
        Private _Color As Color = Color.White

#End Region
#Region ""
        'Public Property RedLineMPH() As Single
        '    Get
        '        Return _RedLineMPH
        '    End Get
        '    Set(ByVal value As Single)
        '        _RedLineMPH = value
        '    End Set
        'End Property
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
#Region "Method"
        Public Sub Render(ByVal e As Graphics, ByVal cx As Single, ByVal cy As Single)
            Dim Titem As New Bitmap(Me.Width, Me.Height)
            Dim g As Graphics = Graphics.FromImage(Titem)
            'g = e
            'g.Clear(Drawing.Color.White)
            g.SmoothingMode = SmoothingMode.AntiAlias
            'Draw Threshold
            Dim rectImg As Rectangle = New Rectangle(0, 0, Me.Width, Me.Height)
            Dim colorPen As Pen = New Pen(Color.FromArgb(200, Me.Color), Me.Width \ 50)
            Dim gap As Integer = CInt(Math.Truncate(Me.Width * 0.02F))
            Dim rectg As Rectangle = New Rectangle(rectImg.X + gap, rectImg.Y + gap, rectImg.Width - gap * 2, rectImg.Height - gap * 2)

            Dim diff As Single = Me.MaxValue - Me.Value
            Dim startAngle As Single = CalcAngle(Me.Value)
            Dim sweepAngle As Single = ((270 * diff) / 100)

            If startAngle + sweepAngle > 405 Then
                sweepAngle = 405 - startAngle
            End If

            rectg.X = rectg.X
            'rectg.Y = ((rectg.Y - rectg.Height * 0.5) + cy) - 2

            g.DrawArc(colorPen, rectg, startAngle, sweepAngle)

            e.DrawImage(Titem, CInt(cx - (Me.Width * 0.5)), CInt(cy - (Me.Height * 0.5)))
            Titem.Dispose()
            g.Dispose()
        End Sub
        Private Function CalcAngle(ByVal Value As Single) As Single
            Dim val As Single = MaxValue - MinValue
            val = (100 * (Value - MinValue)) / val
            val = ((ToAngle - FromAngle) * val) / 100
            val += FromAngle
            Return val
        End Function
#End Region
    End Class
End Namespace