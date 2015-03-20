Imports Dials.Object
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System
Namespace Dials.Renderer
    Public Class Dial_Calibration
        Inherits Dials.Object.Round_Dial_Base
#Region "Fields"
        Private _ForeColor As Color = Color.White
        Private _BackColor As Color = Color.White
        Private _Font As Font = New Font("Arial", 8)
        Private _NoOfDivisions As Single = 10
        Private _NoOfSubDivisions As Single = 1
        'Private _RedLineMPH As Single = 100
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
        Public Property Font() As Font
            Get
                Return _Font
            End Get
            Set(ByVal value As Font)
                _Font = value
            End Set
        End Property
        Public Property BackColor() As Color
            Get
                Return _BackColor
            End Get
            Set(ByVal value As Color)
                _BackColor = value
            End Set
        End Property
        Public Property ForeColor() As Color
            Get
                Return _ForeColor
            End Get
            Set(ByVal value As Color)
                _ForeColor = value
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
        ''' <summary>
        ''' Draws the Ruler
        ''' </summary>
        ''' <param name="e"></param>
        ''' <param name="cX"></param>
        ''' <param name="cY"></param>
        Public Sub Render(ByVal e As Graphics, ByVal cX As Integer, ByVal cY As Integer)
            Dim Titem As New Bitmap(Me.Width, Me.Height)
            Dim g As Graphics = Graphics.FromImage(Titem)
            'g = e
            g.SmoothingMode = SmoothingMode.AntiAlias

            Dim noOfParts As Integer = Me.NoOfDivisions + 1
            Dim noOfIntermediates As Integer = Me.NoOfSubDivisions
            Dim currentAngle As Single = GetRadian(FromAngle)
            Dim gap As Integer = CInt(Math.Truncate(Me.Width * 0.01F))
            Dim shift As Single = Me.Width \ 25
            Dim rect As Rectangle = New Rectangle(0, 0, Me.Width, Me.Height)
            Dim rectangle As New Rectangle(rect.Left + gap, rect.Top + gap, rect.Width - gap, rect.Height - gap)

            Dim x As Single, y As Single, x1 As Single, y1 As Single, tx As Single, ty As Single, _
             radius As Single
            radius = rectangle.Width \ 2 - gap * 5
            Dim totalAngle As Single = ToAngle - FromAngle
            Dim incr As Single = GetRadian(((totalAngle) / ((noOfParts - 1) * (noOfIntermediates + 1))))

            Dim thickPen As New Pen(Me.ForeColor, 2)
            Dim thinPen As New Pen(Me.ForeColor, 1)
            Dim rulerValue As Single = MinValue
            For i As Integer = 0 To noOfParts
                'Draw Thick Line
                x = CSng(cX + radius * Math.Cos(currentAngle))
                y = CSng(cY + radius * Math.Sin(currentAngle))
                x1 = CSng(cX + (radius - Width \ 20) * Math.Cos(currentAngle))
                y1 = CSng(cY + (radius - Width \ 20) * Math.Sin(currentAngle))
                g.DrawLine(thickPen, x, y, x1, y1)

                'Draw Strings
                Dim format As New StringFormat()
                tx = CSng(cX + (radius - Width \ 10) * Math.Cos(currentAngle))
                ty = CSng(cY - shift + (radius - Width \ 10) * Math.Sin(currentAngle))
                Dim stringPen As Brush = New SolidBrush(Me.ForeColor)
                Dim strFormat As New StringFormat(StringFormatFlags.NoClip)
                strFormat.Alignment = StringAlignment.Center
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
                Dim f As New Font(Me.Font.FontFamily, CSng(Me.Width \ 23), Me.Font.Style)
                g.DrawString(rulerValue.ToString() & "", f, stringPen, New PointF(tx, ty), strFormat)
                rulerValue += CSng((MaxValue - MinValue) / (noOfParts - 1))
                rulerValue = CSng(Math.Round(rulerValue, 2))

                'currentAngle += incr;
                If i = noOfParts - 1 Then
                    Exit For
                End If
                For j As Integer = 0 To noOfIntermediates
                    'Draw thin lines 
                    currentAngle += incr
                    x = CSng(cX + radius * Math.Cos(currentAngle))
                    y = CSng(cY + radius * Math.Sin(currentAngle))
                    x1 = CSng(cX + (radius - Width \ 50) * Math.Cos(currentAngle))
                    y1 = CSng(cY + (radius - Width \ 50) * Math.Sin(currentAngle))
                    g.DrawLine(thinPen, x, y, x1, y1)
                Next
            Next

            e.DrawImage(Titem, CInt(cX - (Me.Width * 0.5)), CInt(cY - (Me.Height * 0.5)))
            Titem.Dispose()
            g.Dispose()
        End Sub
        'Public Sub RenderThreshold(ByVal g As Graphics)
        '    'Draw Threshold
        '    Dim rectImg As Rectangle = New Rectangle(0, 0, Me.Width, Me.Height)
        '    Dim colorPen As Pen = New Pen(Color.FromArgb(200, Color.Red), Me.Width \ 50)
        '    Dim gap As Integer = CInt(Math.Truncate(Me.Width * 0.01F))
        '    Dim rectg As Rectangle = New Rectangle(rectImg.X + gap, rectImg.Y + gap, rectImg.Width - gap * 2, rectImg.Height - gap * 2)

        '    Dim diff As Single = Me.MaxValue - Me.RedLineMPH
        '    Dim startAngle As Single = CalcAngle(Me.RedLineMPH)
        '    Dim sweepAngle As Single = ((270 * diff) / 100)

        '    If startAngle + sweepAngle > 405 Then
        '        sweepAngle = 405 - startAngle
        '    End If

        '    g.DrawArc(colorPen, rectg, startAngle, sweepAngle)

        'End Sub
        'Private Function CalcAngle(ByVal Value As Single) As Single
        '    Dim val As Single = MaxValue - MinValue
        '    val = (100 * (Value - MinValue)) / val
        '    val = ((ToAngle - FromAngle) * val) / 100
        '    val += FromAngle
        '    Return val
        'End Function
#End Region
    End Class
End Namespace