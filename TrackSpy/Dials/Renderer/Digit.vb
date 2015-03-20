Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports TrackSpy.Dials.Object
Imports System
Namespace Dials.Renderer


    Public Class Digit
        Inherits Dials.Object.Dial_Base
#Region "Fields"
        Private _Color As Color = Color.Lavender
        Private _CurrentValue As Single = 0

#End Region
#Region "Properties"
        Public Property Value() As Single
            Get
                Return _CurrentValue
            End Get
            Set(ByVal value As Single)
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
        Public Sub Render(ByVal e As Graphics, ByVal cx As Single, ByVal cy As Single)
            Try
                Dim Timg As New Bitmap(Me.Width, Me.Height)
                Dim g As Graphics = Graphics.FromImage(Timg)
                g.Clear(Drawing.Color.White)
                g.SmoothingMode = SmoothingMode.AntiAlias
                Dim num As String = Me.Value.ToString("000.00")
                Dim drect As Rectangle = New Rectangle(0, 0, 15, 15)
                num.PadLeft(3, "0"c)
                Dim shift As Single = 0
                If Me.Value < 0 Then
                    shift -= Width \ 17
                End If
                Dim drawDPS As Boolean = False
                Dim chars As Char() = num.ToCharArray()
                For i As Integer = 0 To chars.Length - 1
                    Dim c As Char = chars(i)
                    If i < chars.Length - 1 AndAlso chars(i + 1) = "."c Then
                        drawDPS = True
                    Else
                        drawDPS = False
                    End If
                    If c <> "."c Then
                        If c = "-"c Then
                            DrawDigit(g, -1, New PointF(drect.X + shift, drect.Y), drawDPS, drect.Height)
                        Else
                            DrawDigit(g, Int16.Parse(c.ToString()), New PointF(drect.X + shift, drect.Y), drawDPS, drect.Height)
                        End If
                        shift += 15 * Me.Width \ 250
                    Else
                        shift += 2 * Me.Width \ 250
                    End If
                Next


                e.DrawImage(Timg, cx, cy)
                Timg.Dispose()
                g.Dispose()
            Catch generatedExceptionName As Exception
            End Try
        End Sub

        ''' <summary>
        ''' Draws a digit in 7-Segement format.
        ''' </summary>
        ''' <param name="g"></param>
        ''' <param name="number"></param>
        ''' <param name="position"></param>
        ''' <param name="dp"></param>
        ''' <param name="height"></param>
        Friend Sub DrawDigit(ByVal g As Graphics, ByVal number As Integer, ByVal position As PointF, ByVal dp As Boolean, ByVal height As Single)
            Dim width As Single
            width = 10.0F * height / 13

            Dim outline As New Pen(Color.FromArgb(40, Me.Color))
            Dim fillPen As New Pen(Color.Black)

            '#Region "Form Polygon Points"
            'Segment A
            Dim segmentA As PointF() = New PointF(4) {}
            segmentA(0) = InlineAssignHelper(segmentA(4), New PointF(position.X + GetX(2.8F, width), position.Y + GetY(1.0F, height)))
            segmentA(1) = New PointF(position.X + GetX(10, width), position.Y + GetY(1.0F, height))
            segmentA(2) = New PointF(position.X + GetX(8.8F, width), position.Y + GetY(2.0F, height))
            segmentA(3) = New PointF(position.X + GetX(3.8F, width), position.Y + GetY(2.0F, height))

            'Segment B
            Dim segmentB As PointF() = New PointF(4) {}
            segmentB(0) = InlineAssignHelper(segmentB(4), New PointF(position.X + GetX(10, width), position.Y + GetY(1.4F, height)))
            segmentB(1) = New PointF(position.X + GetX(9.3F, width), position.Y + GetY(6.8F, height))
            segmentB(2) = New PointF(position.X + GetX(8.4F, width), position.Y + GetY(6.4F, height))
            segmentB(3) = New PointF(position.X + GetX(9.0F, width), position.Y + GetY(2.2F, height))

            'Segment C
            Dim segmentC As PointF() = New PointF(4) {}
            segmentC(0) = InlineAssignHelper(segmentC(4), New PointF(position.X + GetX(9.2F, width), position.Y + GetY(7.2F, height)))
            segmentC(1) = New PointF(position.X + GetX(8.7F, width), position.Y + GetY(12.7F, height))
            segmentC(2) = New PointF(position.X + GetX(7.6F, width), position.Y + GetY(11.9F, height))
            segmentC(3) = New PointF(position.X + GetX(8.2F, width), position.Y + GetY(7.7F, height))

            'Segment D
            Dim segmentD As PointF() = New PointF(4) {}
            segmentD(0) = InlineAssignHelper(segmentD(4), New PointF(position.X + GetX(7.4F, width), position.Y + GetY(12.1F, height)))
            segmentD(1) = New PointF(position.X + GetX(8.4F, width), position.Y + GetY(13.0F, height))
            segmentD(2) = New PointF(position.X + GetX(1.3F, width), position.Y + GetY(13.0F, height))
            segmentD(3) = New PointF(position.X + GetX(2.2F, width), position.Y + GetY(12.1F, height))

            'Segment E
            Dim segmentE As PointF() = New PointF(4) {}
            segmentE(0) = InlineAssignHelper(segmentE(4), New PointF(position.X + GetX(2.2F, width), position.Y + GetY(11.8F, height)))
            segmentE(1) = New PointF(position.X + GetX(1.0F, width), position.Y + GetY(12.7F, height))
            segmentE(2) = New PointF(position.X + GetX(1.7F, width), position.Y + GetY(7.2F, height))
            segmentE(3) = New PointF(position.X + GetX(2.8F, width), position.Y + GetY(7.7F, height))

            'Segment F
            Dim segmentF As PointF() = New PointF(4) {}
            segmentF(0) = InlineAssignHelper(segmentF(4), New PointF(position.X + GetX(3.0F, width), position.Y + GetY(6.4F, height)))
            segmentF(1) = New PointF(position.X + GetX(1.8F, width), position.Y + GetY(6.8F, height))
            segmentF(2) = New PointF(position.X + GetX(2.6F, width), position.Y + GetY(1.3F, height))
            segmentF(3) = New PointF(position.X + GetX(3.6F, width), position.Y + GetY(2.2F, height))

            'Segment G
            Dim segmentG As PointF() = New PointF(6) {}
            segmentG(0) = InlineAssignHelper(segmentG(6), New PointF(position.X + GetX(2.0F, width), position.Y + GetY(7.0F, height)))
            segmentG(1) = New PointF(position.X + GetX(3.1F, width), position.Y + GetY(6.5F, height))
            segmentG(2) = New PointF(position.X + GetX(8.3F, width), position.Y + GetY(6.5F, height))
            segmentG(3) = New PointF(position.X + GetX(9.0F, width), position.Y + GetY(7.0F, height))
            segmentG(4) = New PointF(position.X + GetX(8.2F, width), position.Y + GetY(7.5F, height))
            segmentG(5) = New PointF(position.X + GetX(2.9F, width), position.Y + GetY(7.5F, height))

            'Segment DP
            '#End Region

            '#Region "Draw Segments Outline"
            g.FillPolygon(outline.Brush, segmentA)
            g.FillPolygon(outline.Brush, segmentB)
            g.FillPolygon(outline.Brush, segmentC)
            g.FillPolygon(outline.Brush, segmentD)
            g.FillPolygon(outline.Brush, segmentE)
            g.FillPolygon(outline.Brush, segmentF)
            g.FillPolygon(outline.Brush, segmentG)
            '#End Region

            '#Region "Fill Segments"
            'Fill SegmentA
            If IsNumberAvailable(number, 0, 2, 3, 5, 6, _
             7, 8, 9) Then
                g.FillPolygon(fillPen.Brush, segmentA)
            End If

            'Fill SegmentB
            If IsNumberAvailable(number, 0, 1, 2, 3, 4, _
             7, 8, 9) Then
                g.FillPolygon(fillPen.Brush, segmentB)
            End If

            'Fill SegmentC
            If IsNumberAvailable(number, 0, 1, 3, 4, 5, _
             6, 7, 8, 9) Then
                g.FillPolygon(fillPen.Brush, segmentC)
            End If

            'Fill SegmentD
            If IsNumberAvailable(number, 0, 2, 3, 5, 6, _
             8, 9) Then
                g.FillPolygon(fillPen.Brush, segmentD)
            End If

            'Fill SegmentE
            If IsNumberAvailable(number, 0, 2, 6, 8) Then
                g.FillPolygon(fillPen.Brush, segmentE)
            End If

            'Fill SegmentF
            If IsNumberAvailable(number, 0, 4, 5, 6, 7, _
             8, 9) Then
                g.FillPolygon(fillPen.Brush, segmentF)
            End If

            'Fill SegmentG
            If IsNumberAvailable(number, 2, 3, 4, 5, 6, _
             8, 9, -1) Then
                g.FillPolygon(fillPen.Brush, segmentG)
            End If
            '#End Region

            'Draw decimal point
            If dp Then
                g.FillEllipse(fillPen.Brush, New RectangleF(position.X + GetX(10.0F, width), position.Y + GetY(12.0F, height), width / 7, width / 7))
            End If
        End Sub
        ''' <summary>
        ''' Returns true if a given number is available in the given list.
        ''' </summary>
        ''' <param name="number"></param>
        ''' <param name="listOfNumbers"></param>
        ''' <returns></returns>
        Friend Function IsNumberAvailable(ByVal number As Integer, ByVal ParamArray listOfNumbers As Integer()) As Boolean
            If listOfNumbers.Length > 0 Then
                For Each i As Integer In listOfNumbers
                    If i = number Then
                        Return True
                    End If
                Next
            End If
            Return False
        End Function
        ''' <summary>
        ''' Gets Relative X for the given width to draw digit
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="width"></param>
        ''' <returns></returns>
        Friend Function GetX(ByVal x As Single, ByVal width As Single) As Single
            Return x * width / 12
        End Function

        ''' <summary>
        ''' Gets relative Y for the given height to draw digit
        ''' </summary>
        ''' <param name="y"></param>
        ''' <param name="height"></param>
        ''' <returns></returns>
        Friend Function GetY(ByVal y As Single, ByVal height As Single) As Single
            Return y * height / 15
        End Function
        Friend Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
            target = value
            Return value
        End Function
#End Region
    End Class
End Namespace