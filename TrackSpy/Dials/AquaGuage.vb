Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System
Imports System.Data
Imports System.Text
Imports System.Drawing.Drawing2D
Imports System.Xml.Serialization
Imports System.Drawing.Imaging

Namespace Dials
    ''' <summary>
    ''' Aqua Gauge Control - A Windows User Control.
    ''' Author  : Ambalavanar Thirugnanam
    ''' Date    : 24th August 2007
    ''' email   : ambalavanar.thiru@gmail.com
    ''' This is control is for free. You can use for any commercial or non-commercial purposes.
    ''' [Please do no remove this header when using this control in your application.]
    ''' </summary>
    Partial Public Class AquaGauge
#Region "Private Attributes"
        Private _backImg As String = ""
        Private m_minValue As Single = 0
        Private m_maxValue As Single = 130
        Private threshold As Single = 70
        Private currentValue As Single = 0
        Private m_recommendedValue As Single = 1
        Private m_noOfDivisions As Integer = 10
        Private m_noOfSubDivisions As Integer = 3
        Private m_dialText As String = "mph"
        Private m_dialColor As Color = Color.Lavender
        Private glossinessAlpha As Single = 25
        Private oldWidth As Integer, oldHeight As Integer
        Private _x As Integer = 0
        Private _y As Integer = 0
        Private _width As Integer = 300
        Private _height As Integer = 300
        Private fromAngle As Single = 135.0F
        Private toAngle As Single = 405.0F
        Private m_enableTransparentBackground As Boolean
        Private requiresRedraw As Boolean
        Private backgroundImg As Image
        Private rectImg As Rectangle
        Private Font As Font = New Font("Arial", 8)
        Private _ForeColor As Color = Color.Black
        Private _BackColor As Color = Color.Black
        Private RenderedDial As Bitmap
#End Region

        Public Sub New()
            'x = 5
            'y = 5
            '_width = 100 '_width - 10
            '_width = 100 'Me.height - 10
            Me.m_noOfDivisions = 10
            Me.m_noOfSubDivisions = 3
            Me.requiresRedraw = True
        End Sub
#Region "Public Properties"
        '<XmlElement(GetType(XmlFont))> _
        'Public Property Font() As Font
        '    Get
        '        Return _Font
        '    End Get
        '    Set(ByVal value As Font)
        '        _Font = value
        '    End Set
        'End Property
        Public Property backImg() As String
            Get
                Return _backImg
            End Get
            Set(ByVal value As String)
                _backImg = value
            End Set
        End Property
        <XmlElement(GetType(Helper.XmlColor))> _
        Public Property BackColor() As Color
            Get
                Return _BackColor
            End Get
            Set(ByVal value As Color)
                _BackColor = value
            End Set
        End Property
        <XmlElement(GetType(Helper.XmlColor))> _
        Public Property ForeColor() As Color
            Get
                Return _ForeColor
            End Get
            Set(ByVal value As Color)
                _ForeColor = value
            End Set
        End Property
        Public Property X() As Integer
            Get
                Return _x
            End Get
            Set(ByVal value As Integer)
                _x = value
            End Set
        End Property
        Public Property Y() As Integer
            Get
                Return _y
            End Get
            Set(ByVal value As Integer)
                _y = value
            End Set
        End Property

        Public Property Width() As Integer
            Get
                Return _width
            End Get
            Set(ByVal value As Integer)
                _width = value
            End Set
        End Property
        Public Property Height() As Integer
            Get
                Return _height
            End Get
            Set(ByVal value As Integer)
                _height = value
            End Set
        End Property


        ''' <summary>
        ''' Mininum value on the scale
        ''' </summary>
        <DefaultValue(0)> _
        <Description("Mininum value on the scale")> _
        Public Property MinValue() As Single
            Get
                Return m_minValue
            End Get
            Set(ByVal value As Single)
                If value < m_maxValue Then
                    m_minValue = value
                    If currentValue < m_minValue Then
                        currentValue = m_minValue
                    End If
                    If m_recommendedValue < m_minValue Then
                        m_recommendedValue = m_minValue
                    End If
                    requiresRedraw = True
                    'Me.Invalidate()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Maximum value on the scale
        ''' </summary>
        <DefaultValue(100)> _
        <Description("Maximum value on the scale")> _
        Public Property MaxValue() As Single
            Get
                Return m_maxValue
            End Get
            Set(ByVal value As Single)
                If value > m_minValue Then
                    m_maxValue = value
                    If currentValue > m_maxValue Then
                        currentValue = m_maxValue
                    End If
                    If m_recommendedValue > m_maxValue Then
                        m_recommendedValue = m_maxValue
                    End If
                    requiresRedraw = True
                    'Me.Invalidate()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets the Threshold area from the Recommended Value. (1-99%)
        ''' </summary>
        <DefaultValue(25)> _
        <Description("Gets or Sets the Threshold area from the Recommended Value. (1-99%)")> _
        Public Property ThresholdPercent() As Single
            Get
                Return threshold
            End Get
            Set(ByVal value As Single)
                If value > 0 AndAlso value < 100 Then
                    threshold = value
                    requiresRedraw = True
                    'Me.Invalidate()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Threshold value from which green area will be marked.
        ''' </summary>
        <DefaultValue(25)> _
        <Description("Threshold value from which green area will be marked.")> _
        Public Property RecommendedValue() As Single
            Get
                Return m_recommendedValue
            End Get
            Set(ByVal value As Single)
                If value > m_minValue AndAlso value < m_maxValue Then
                    m_recommendedValue = value
                    requiresRedraw = True
                    'Me.Invalidate()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value where the pointer will point to.
        ''' </summary>
        <DefaultValue(0)> _
        <Description("Value where the pointer will point to.")> _
        Public Property Value() As Single
            Get
                Return currentValue
            End Get
            Set(ByVal value As Single)
                If value >= m_minValue AndAlso value <= m_maxValue Then
                    currentValue = value
                    'Me.Refresh()
                End If
            End Set
        End Property

        <Description("Background color of the dial"), XmlElement(GetType(Helper.XmlColor))> _
        Public Property DialColor() As Color
            Get
                Return m_dialColor
            End Get
            Set(ByVal value As Color)
                m_dialColor = value
                requiresRedraw = True
                'Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Glossiness strength. Range: 0-100
        ''' </summary>
        <DefaultValue(72)> _
        <Description("Glossiness strength. Range: 0-100")> _
        Public Property Glossiness() As Single
            Get
                Return (glossinessAlpha * 100) / 220
            End Get
            Set(ByVal value As Single)
                Dim val As Single = value
                If val > 100 Then
                    value = 100
                End If
                If val < 0 Then
                    value = 0
                End If
                glossinessAlpha = (value * 220) / 100
                ' Me.Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Get or Sets the number of Divisions in the dial scale.
        ''' </summary>
        <DefaultValue(10)> _
        <Description("Get or Sets the number of Divisions in the dial scale.")> _
        Public Property NoOfDivisions() As Integer
            Get
                Return Me.m_noOfDivisions
            End Get
            Set(ByVal value As Integer)
                If value > 1 AndAlso value < 25 Then
                    Me.m_noOfDivisions = value
                    requiresRedraw = True
                    'Me.Invalidate()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets the number of Sub Divisions in the scale per Division.
        ''' </summary>
        <DefaultValue(3)> _
        <Description("Gets or Sets the number of Sub Divisions in the scale per Division.")> _
        Public Property NoOfSubDivisions() As Integer
            Get
                Return Me.m_noOfSubDivisions
            End Get
            Set(ByVal value As Integer)
                If value > 0 AndAlso value <= 10 Then
                    Me.m_noOfSubDivisions = value
                    requiresRedraw = True
                    'Me.Invalidate()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets the Text to be displayed in the dial
        ''' </summary>
        <Description("Gets or Sets the Text to be displayed in the dial")> _
        Public Property DialText() As String
            Get
                Return Me.m_dialText
            End Get
            Set(ByVal value As String)
                Me.m_dialText = value
                requiresRedraw = True
                'Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Enables or Disables Transparent Background color.
        ''' Note: Enabling this will reduce the performance and may make the control flicker.
        ''' </summary>
        <DefaultValue(False)> _
        <Description("Enables or Disables Transparent Background color. Note: Enabling this will reduce the performance and may make the control flicker.")> _
        Public Property EnableTransparentBackground() As Boolean
            Get
                Return Me.m_enableTransparentBackground
            End Get
            Set(ByVal value As Boolean)
                Me.m_enableTransparentBackground = value
                'Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, Not m_enableTransparentBackground)
                requiresRedraw = True
                ' Me.Refresh()
            End Set
        End Property
#End Region

#Region "Overriden Control methods"
        Public Function GetRenderedImg() As Bitmap
            Return RenderedDial
        End Function
        ''' <summary>
        ''' Draws the pointer.
        ''' </summary>
        Public Sub Render(ByVal Width As Integer, ByVal height As Integer, ByVal Max As Single, ByVal Value As Single)
            Me.Width = Width
            Me.Height = height
            Me.MaxValue = Max
            Me.Value = Value
            Me.BackColor = Color.AliceBlue
            RenderedDial = New Bitmap(Me.Width, Me.Width, 0, PixelFormat.Format32bppPArgb, Nothing)
            Dim e As Graphics = Graphics.FromImage(RenderedDial)
            e.SmoothingMode = SmoothingMode.AntiAlias
            'Width = Me.Width - x * 2
            'height = Me.Height - y * 2
            'RenderBackGround(e)
            'DrawPointer(e, ((Width) \ 2) + x, ((height) \ 2) + y)
            'Width = Me.Width - x * 2
            'height = Me.Height - y * 2
            RenderBackGround(e)
            DrawPointer(e, ((Width) \ 2), ((height) \ 2))
        End Sub
        Public Sub Render(ByVal Max As Single, ByVal Value As Single)
            Me.MaxValue = Max
            Me.Value = Value
            RenderedDial = New Bitmap(Me.Width, Me.Width, 0, PixelFormat.Format32bppPArgb, Nothing)
            Dim e As Graphics = Graphics.FromImage(RenderedDial)
            e.SmoothingMode = SmoothingMode.AntiAlias
            'Width = Me.Width - X * 2
            'Height = Me.Height - Y * 2
            RenderBackGround(e)
            DrawPointer(e, ((Width) \ 2), ((Height) \ 2))
        End Sub
        ''' <summary>
        ''' Draws the dial background.
        ''' </summary>
        ''' <param name="e"></param>
        Private Sub RenderBackGround(ByVal e As Graphics)
            If Not m_enableTransparentBackground Then
                'MyBase.OnPaintBackground(e)
            End If

            e.SmoothingMode = SmoothingMode.HighQuality
            e.FillRectangle(New SolidBrush(Color.Transparent), New Rectangle(0, 0, Width, Height))
            'If backgroundImg Is Nothing Then
            backgroundImg = New Bitmap(Me.Width, Me.Width, 0, PixelFormat.Format32bppPArgb, Nothing)
            Dim g As Graphics = Graphics.FromImage(backgroundImg)
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            'Width = Me.Width - x * 2
            'Height = Me.Height - y * 2
            rectImg = New Rectangle(0, 0, Width, Height)

            'Draw background color
            Dim backGroundBrush As Brush = New SolidBrush(Color.FromArgb(120, m_dialColor))
            'If m_enableTransparentBackground AndAlso Me.Parent IsNot Nothing Then
            'g.FillEllipse(new SolidBrush(me.BackColor), -gg, -gg, this.Width+gg*2, this.Height+gg*2);
            ' Dim gg As Single = width \ 60
            'End If

            If String.IsNullOrEmpty(backImg) Then
                g.FillEllipse(backGroundBrush, 0, 0, Width, Height)
                'Draw Rim
                Dim outlineBrush As New SolidBrush(Color.FromArgb(100, Color.SlateGray))
                Dim outline As New Pen(outlineBrush, CSng(Width * 0.03))
                g.DrawEllipse(outline, rectImg)
                Dim darkRim As New Pen(Color.SlateGray)
                g.DrawEllipse(darkRim, 0, 0, Width, Height)
            Else
                Dim TBit As New Bitmap(backImg)
                g.DrawImage(TBit, New Rectangle(0, 0, Width, Height))
            End If

            'Draw Callibration
            DrawCalibration(g, rectImg, ((Width) \ 2), ((Height) \ 2))

            'Draw Colored Rim
            'If String.IsNullOrEmpty(backImg) Then
            Dim colorPen As New Pen(Color.FromArgb(190, Color.Gainsboro), Me.Width \ 40)
            Dim blackPen As New Pen(Color.FromArgb(250, Color.Black), Me.Width \ 200)
            Dim gap As Integer = CInt(Math.Truncate(Me.Width * 0.03F))
            Dim rectg As New Rectangle(rectImg.X + gap, rectImg.Y + gap, rectImg.Width - gap * 2, rectImg.Height - gap * 2)
            g.DrawArc(colorPen, rectg, 135, 270)
            'End If

            'Draw Threshold
            colorPen = New Pen(Color.FromArgb(200, Color.LawnGreen), Me.Width \ 50)
            rectg = New Rectangle(rectImg.X + gap, rectImg.Y + gap, rectImg.Width - gap * 2, rectImg.Height - gap * 2)

            Dim val As Single = MaxValue - MinValue
            val = (100 * (Me.m_recommendedValue - MinValue)) / val
            val = ((toAngle - fromAngle) * val) / 100
            val += fromAngle
            Dim stAngle As Single = val - ((270 * threshold) / 200)
            If stAngle <= 135 Then
                stAngle = 135
            End If
            Dim sweepAngle As Single = ((270 * threshold) / 100)
            If stAngle + sweepAngle > 405 Then
                sweepAngle = 405 - stAngle
            End If
            g.DrawArc(colorPen, rectg, stAngle, sweepAngle)

            'Draw Digital Value
            Dim digiRect As New RectangleF(CSng(Me.Width) / 2.0F - CSng(Me.Width) / 5.0F, CSng(Me.Height) / 1.2F, CSng(Me.Width) / 2.5F, CSng(Me.Height) / 9.0F)
            Dim digiFRect As New RectangleF(Me.Width \ 2 - Me.Width \ 7, CInt(Math.Truncate(Me.Height / 1.18)), Me.Width \ 4, Me.Height \ 12)
            g.FillRectangle(New SolidBrush(Color.FromArgb(30, Color.Gray)), digiRect)

            DisplayNumber(g, Me.currentValue, digiFRect)

            Dim textSize As SizeF = g.MeasureString(Me.m_dialText, Me.Font)
            Dim digiFRectText As New RectangleF(Me.Width \ 2 - textSize.Width / 2, CInt(Math.Truncate(Me.Height / 1.5)), textSize.Width, textSize.Height)
            g.DrawString(m_dialText, Me.Font, New SolidBrush(Me.ForeColor), digiFRectText)
            requiresRedraw = False
            ' End If
            e.DrawImage(backgroundImg, rectImg)
        End Sub

        'Protected Overrides ReadOnly Property CreateParams() As CreateParams
        '    Get
        '        Dim cp As CreateParams = MyBase.CreateParams
        '        cp.ExStyle = cp.ExStyle Or &H20
        '        Return cp
        '    End Get
        'End Property
#End Region

#Region "Private methods"
        ''' <summary>
        ''' Draws the Pointer.
        ''' </summary>
        ''' <param name="gr"></param>
        ''' <param name="cx"></param>
        ''' <param name="cy"></param>
        Private Sub DrawPointer(ByVal gr As Graphics, ByVal cx As Integer, ByVal cy As Integer)
            Dim radius As Single = Me.Width \ 2 - (Me.Width * 0.12F)
            Dim val As Single = MaxValue - MinValue

            Dim img As Image = New Bitmap(Me.Width, Me.Width, 0, PixelFormat.Format32bppPArgb, Nothing)
            Dim g As Graphics = Graphics.FromImage(img)
            g.SmoothingMode = SmoothingMode.AntiAlias

            val = (100 * (Me.currentValue - MinValue)) / val
            val = ((toAngle - fromAngle) * val) / 100
            val += fromAngle

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

            Dim gpointer As New LinearGradientBrush(shinePts(0), shinePts(2), Color.SlateGray, Color.Black)
            g.FillPolygon(gpointer, shinePts)

            Dim rect As New Rectangle(0, 0, Width, Height)
            DrawCenterPoint(g, rect, ((Width) \ 2) + 0, ((Height) \ 2) + 0)

            DrawGloss(g)

            gr.DrawImage(img, 0, 0)
        End Sub

        ''' <summary>
        ''' Draws the glossiness.
        ''' </summary>
        ''' <param name="g"></param>
        Private Sub DrawGloss(ByVal g As Graphics)
            Dim glossRect As New RectangleF(0 + CSng(Width * 0.1), 0 + CSng(Height * 0.07), CSng(Width * 0.8), CSng(Height * 0.7))
            Dim gradientBrush As New LinearGradientBrush(glossRect, Color.FromArgb(CInt(Math.Truncate(glossinessAlpha)), Color.White), Color.Transparent, LinearGradientMode.Vertical)
            g.FillEllipse(gradientBrush, glossRect)

            'TODO: Gradient from bottom
            glossRect = New RectangleF(0 + CSng(Width * 0.25), 0 + CSng(Height * 0.77), CSng(Width * 0.5), CSng(Height * 0.2))
            Dim gloss As Integer = CInt(Math.Truncate(glossinessAlpha / 3))
            gradientBrush = New LinearGradientBrush(glossRect, Color.Transparent, Color.FromArgb(gloss, Me.BackColor), LinearGradientMode.Vertical)
            g.FillEllipse(gradientBrush, glossRect)
        End Sub

        ''' <summary>
        ''' Draws the center point.
        ''' </summary>
        ''' <param name="g"></param>
        ''' <param name="rect"></param>
        ''' <param name="cX"></param>
        ''' <param name="cY"></param>
        Private Sub DrawCenterPoint(ByVal g As Graphics, ByVal rect As Rectangle, ByVal cX As Integer, ByVal cY As Integer)
            Dim shift As Single = Width \ 5
            Dim rectangle As New RectangleF(cX - (shift / 2), cY - (shift / 2), shift, shift)
            Dim brush As New LinearGradientBrush(rect, Color.Black, Color.FromArgb(100, Me.m_dialColor), LinearGradientMode.Vertical)
            g.FillEllipse(brush, rectangle)

            shift = Width \ 7
            rectangle = New RectangleF(cX - (shift / 2), cY - (shift / 2), shift, shift)
            brush = New LinearGradientBrush(rect, Color.SlateGray, Color.Black, LinearGradientMode.ForwardDiagonal)
            g.FillEllipse(brush, rectangle)
        End Sub

        ''' <summary>
        ''' Draws the Ruler
        ''' </summary>
        ''' <param name="g"></param>
        ''' <param name="rect"></param>
        ''' <param name="cX"></param>
        ''' <param name="cY"></param>
        Private Sub DrawCalibration(ByVal g As Graphics, ByVal rect As Rectangle, ByVal cX As Integer, ByVal cY As Integer)
            Dim noOfParts As Integer = Me.m_noOfDivisions + 1
            Dim noOfIntermediates As Integer = Me.m_noOfSubDivisions
            Dim currentAngle As Single = GetRadian(fromAngle)
            Dim gap As Integer = CInt(Math.Truncate(Me.Width * 0.01F))
            Dim shift As Single = Me.Width \ 25
            Dim rectangle As New Rectangle(rect.Left + gap, rect.Top + gap, rect.Width - gap, rect.Height - gap)

            Dim x As Single, y As Single, x1 As Single, y1 As Single, tx As Single, ty As Single, _
             radius As Single
            radius = rectangle.Width \ 2 - gap * 5
            Dim totalAngle As Single = toAngle - fromAngle
            Dim incr As Single = GetRadian(((totalAngle) / ((noOfParts - 1) * (noOfIntermediates + 1))))

            Dim thickPen As New Pen(Color.Black, Width \ 50)
            Dim thinPen As New Pen(Color.Black, Width \ 100)
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
        End Sub

        ''' <summary>
        ''' Converts the given degree to radian.
        ''' </summary>
        ''' <param name="theta"></param>
        ''' <returns></returns>
        Private Function GetRadian(ByVal theta As Single) As Single
            Return theta * CSng(Math.PI) / 180.0F
        End Function

        ''' <summary>
        ''' Displays the given number in the 7-Segement format.
        ''' </summary>
        ''' <param name="g"></param>
        ''' <param name="number"></param>
        ''' <param name="drect"></param>
        Private Sub DisplayNumber(ByVal g As Graphics, ByVal number As Single, ByVal drect As RectangleF)
            Try
                Dim num As String = number.ToString("000.00")
                num.PadLeft(3, "0"c)
                Dim shift As Single = 0
                If number < 0 Then
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
        Private Sub DrawDigit(ByVal g As Graphics, ByVal number As Integer, ByVal position As PointF, ByVal dp As Boolean, ByVal height As Single)
            Dim width As Single
            width = 10.0F * height / 13

            Dim outline As New Pen(Color.FromArgb(40, Me.m_dialColor))
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
        ''' Gets Relative X for the given width to draw digit
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="width"></param>
        ''' <returns></returns>
        Private Function GetX(ByVal x As Single, ByVal width As Single) As Single
            Return x * width / 12
        End Function

        ''' <summary>
        ''' Gets relative Y for the given height to draw digit
        ''' </summary>
        ''' <param name="y"></param>
        ''' <param name="height"></param>
        ''' <returns></returns>
        Private Function GetY(ByVal y As Single, ByVal height As Single) As Single
            Return y * height / 15
        End Function

        ''' <summary>
        ''' Returns true if a given number is available in the given list.
        ''' </summary>
        ''' <param name="number"></param>
        ''' <param name="listOfNumbers"></param>
        ''' <returns></returns>
        Private Function IsNumberAvailable(ByVal number As Integer, ByVal ParamArray listOfNumbers As Integer()) As Boolean
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
        ''' Restricts the size to make sure the height and width are always same.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub AquaGauge_Resize(ByVal sender As Object, ByVal e As EventArgs)
            If Me.Width < 136 Then
                Me.Width = 136
            End If
            If oldWidth <> Me.Width Then
                Me.Height = Me.Width
                oldHeight = Me.Width
            End If
            If oldHeight <> Me.Height Then
                Me.Width = Me.Height
                oldWidth = Me.Width
            End If
        End Sub
#End Region

        'Private Sub AquaGauge_Load(ByVal sender As Object, ByVal e As EventArgs)

        'End Sub
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace
