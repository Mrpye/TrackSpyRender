Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports TrackSpy.Dials.Object
Imports System
Namespace Dials.Renderer
    Public Class Dial_Needle
        Inherits Dials.Object.Round_Dial_Base
#Region "Fields"

        Private _DialColor As Color = Color.Lavender
        Private _BackColor As Color = Color.Lavender
        Private _GlossinessAlpha As Single = 25
#End Region
#Region "Properties"
        Public Property BackColor() As Color
            Get
                Return _BackColor
            End Get
            Set(ByVal value As Color)
                _BackColor = value
            End Set
        End Property
        Public Property DialColor() As Color
            Get
                Return _DialColor
            End Get
            Set(ByVal value As Color)
                _DialColor = value
            End Set
        End Property
        Public Property GlossinessAlpha() As Single
            Get
                Return _GlossinessAlpha
            End Get
            Set(ByVal value As Single)
                _GlossinessAlpha = value
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
        Public Sub Render2(ByVal e As Graphics, ByVal cx As Integer, ByVal cy As Integer)
            Dim g As Graphics = Nothing
            Dim radius As Single = Me.Width \ 2 - (Me.Width * 0.12F)
            Dim val As Single = MaxValue - MinValue


            ' Dim img As Image = New Bitmap(Me.Width, Me.Height)
            'Dim g As Graphics = Graphics.FromImage(img)
            g = e
            'g.Clear(Color.White)
            g.SmoothingMode = SmoothingMode.AntiAlias

            val = (100 * (Me.Value - MinValue)) / val
            val = ((ToAngle - FromAngle) * val) / 100
            val += FromAngle

            Dim angle As Single = GetRadian(val)
            Dim gradientAngle As Single = angle

            Dim pts As PointF() = New PointF(4) {}

            pts(0).X = CSng(cx + radius * Math.Cos(angle))
            pts(0).Y = CSng(cy + radius * Math.Sin(angle))

            pts(4).X = CSng(cx + radius * Math.Cos(angle - 0.02))
            pts(4).Y = CSng(cy + radius * Math.Sin(angle - 0.02))

            angle = GetRadian((val + 20))
            pts(1).X = CSng(cx + (Me.Width * 0.09F) * Math.Cos(angle))
            pts(1).Y = CSng(cy + (Me.Width * 0.09F) * Math.Sin(angle))

            pts(2).X = cx
            pts(2).Y = cy

            angle = GetRadian((val - 20))
            pts(3).X = CSng(cx + (Me.Width * 0.09F) * Math.Cos(angle))
            pts(3).Y = CSng(cy + (Me.Width * 0.09F) * Math.Sin(angle))

            Dim pointer As Brush = New SolidBrush(Color.Black)

            g.TranslateTransform(((Width) \ 2) + 0, ((Height) \ 2) + 0)
            g.FillPolygon(pointer, pts)
            g.ResetTransform()

            '((Width) \ 2) + 0, ((Height) \ 2) + 0
            Dim shinePts As PointF() = New PointF(2) {}
            angle = GetRadian(val)
            shinePts(0).X = CSng(cx + radius * Math.Cos(angle))
            shinePts(0).Y = CSng(cy + radius * Math.Sin(angle))

            angle = GetRadian(val + 20)
            shinePts(1).X = CSng(cx + (Me.Width * 0.09F) * Math.Cos(angle))
            shinePts(1).Y = CSng(cy + (Me.Width * 0.09F) * Math.Sin(angle))

            shinePts(2).X = cx
            shinePts(2).Y = cy

            Dim gpointer As New LinearGradientBrush(shinePts(0), shinePts(2), Color.SlateGray, Color.Black)
            g.FillPolygon(gpointer, shinePts)

            Dim rect As New Rectangle(0, 0, Width, Height)
            DrawCenterPoint(g, rect, ((Width) \ 2) + 0, ((Height) \ 2) + 0)

            DrawGloss(g)

            'e.DrawImage(img, CInt(cx - (Me.Width * 0.5)), CInt(cy - (Me.Height * 0.5)))
            'img.Dispose()
            g.Dispose()


        End Sub
        Public Sub Render(ByVal e As Graphics, ByVal cx As Integer, ByVal cy As Integer)

            Dim radius As Single = Me.Width \ 2 - (Me.Width * 0.12F)
            Dim val As Single = MaxValue - MinValue


            Dim img As Image = New Bitmap(Me.Width, Me.Height)
            Dim g As Graphics = Graphics.FromImage(img)
            'g.Clear(Color.White)
            g.SmoothingMode = SmoothingMode.AntiAlias

            val = (100 * (Me.Value - MinValue)) / val
            val = ((ToAngle - FromAngle) * val) / 100
            val += FromAngle

            Dim angle As Single = GetRadian(val)
            Dim gradientAngle As Single = angle

            Dim pts As PointF() = New PointF(4) {}

            pts(0).X = CSng(cx + radius * Math.Cos(angle))
            pts(0).Y = CSng(cy + radius * Math.Sin(angle))

            pts(4).X = CSng(cx + radius * Math.Cos(angle - 0.02))
            pts(4).Y = CSng(cy + radius * Math.Sin(angle - 0.02))

            angle = GetRadian((val + 20))
            pts(1).X = CSng(cx + (Me.Width * 0.09F) * Math.Cos(angle))
            pts(1).Y = CSng(cy + (Me.Width * 0.09F) * Math.Sin(angle))

            pts(2).X = cx
            pts(2).Y = cy

            angle = GetRadian((val - 20))
            pts(3).X = CSng(cx + (Me.Width * 0.09F) * Math.Cos(angle))
            pts(3).Y = CSng(cy + (Me.Width * 0.09F) * Math.Sin(angle))

            Dim pointer As Brush = New SolidBrush(Color.Red)
            g.FillPolygon(pointer, pts)

            Dim shinePts As PointF() = New PointF(2) {}
            angle = GetRadian(val)
            shinePts(0).X = CSng(cx + radius * Math.Cos(angle))
            shinePts(0).Y = CSng(cy + radius * Math.Sin(angle))

            angle = GetRadian(val + 20)
            shinePts(1).X = CSng(cx + (Me.Width * 0.09F) * Math.Cos(angle))
            shinePts(1).Y = CSng(cy + (Me.Width * 0.09F) * Math.Sin(angle))

            shinePts(2).X = cx
            shinePts(2).Y = cy

            Dim gpointer As New LinearGradientBrush(shinePts(0), shinePts(2), Color.SlateGray, Color.Red)
            g.FillPolygon(gpointer, shinePts)

            Dim rect As New Rectangle(0, 0, Width, Height)
            DrawCenterPoint(g, rect, ((Width) \ 2) + 0, ((Height) \ 2) + 0)

            DrawGloss(g)

            e.DrawImage(img, CInt(cx - (Me.Width * 0.5)), CInt(cy - (Me.Height * 0.5)))
            img.Dispose()
            g.Dispose()


        End Sub
        ''' <summary>
        ''' Draws the center point.
        ''' </summary>
        ''' <param name="g"></param>
        ''' <param name="rect"></param>
        ''' <param name="cX"></param>
        ''' <param name="cY"></param>
        Friend Sub DrawCenterPoint(ByVal g As Graphics, ByVal rect As Rectangle, ByVal cX As Integer, ByVal cY As Integer)
            Dim shift As Single = Width \ 5
            Dim rectangle As New RectangleF(cX - (shift / 2), cY - (shift / 2), shift, shift)
            Dim brush As New LinearGradientBrush(rect, Color.Black, Color.FromArgb(100, Me._DialColor), LinearGradientMode.Vertical)
            g.FillEllipse(brush, rectangle)

            shift = Width \ 7
            rectangle = New RectangleF(cX - (shift / 2), cY - (shift / 2), shift, shift)
            brush = New LinearGradientBrush(rect, Color.SlateGray, Color.Black, LinearGradientMode.ForwardDiagonal)
            g.FillEllipse(brush, rectangle)
        End Sub
        ''' <summary>
        ''' Draws the glossiness.
        ''' </summary>
        ''' <param name="g"></param>
        Friend Sub DrawGloss(ByVal g As Graphics)
            Dim glossRect As New RectangleF(0 + CSng(Width * 0.1), 0 + CSng(Height * 0.07), CSng(Width * 0.8), CSng(Height * 0.7))
            Dim gradientBrush As New LinearGradientBrush(glossRect, Color.FromArgb(CInt(Math.Truncate(Me.GlossinessAlpha)), Color.White), Color.Transparent, LinearGradientMode.Vertical)
            g.FillEllipse(gradientBrush, glossRect)

            'TODO: Gradient from bottom
            glossRect = New RectangleF(0 + CSng(Width * 0.25), 0 + CSng(Height * 0.77), CSng(Width * 0.5), CSng(Height * 0.2))
            Dim gloss As Integer = CInt(Math.Truncate(GlossinessAlpha / 3))
            gradientBrush = New LinearGradientBrush(glossRect, Color.Transparent, Color.FromArgb(gloss, Me.BackColor), LinearGradientMode.Vertical)
            g.FillEllipse(gradientBrush, glossRect)
        End Sub
#End Region
    End Class
End Namespace