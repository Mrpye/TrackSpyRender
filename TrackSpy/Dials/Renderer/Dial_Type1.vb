Imports TrackSpyLib.Dials.Object
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports TrackSpy.Dials.Object
Imports System
Namespace Dials.Renderer
    Public Class Dial_Type1
        Inherits Round_Dial_Base

#Region "Fields"
        Private Back As Dial_Face = Nothing
        Private cal As Dial_Calibration = Nothing
        Private needle As Dial_Needle = Nothing
        Private Treash As Dial_Threshold = Nothing
        ' Private digit As Digit = Nothing
        Private _ForeColor As Color = Color.White
        'Private _BackColor As Color = Color.Black
        Private _Font As Font = New Font("Arial", 8)
        Private _NoOfDivisions As Single = 10
        Private _NoOfSubDivisions As Single = 1
        Private _RedLineMPH As Single = 80
        Private _BackImg As String = ""
#End Region
#Region ""
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
        Public Property Font() As Font
            Get
                Return _Font
            End Get
            Set(ByVal value As Font)
                _Font = value
            End Set
        End Property
        'Public Property BackColor() As Color
        '    Get
        '        Return _BackColor
        '    End Get
        '    Set(ByVal value As Color)
        '        _BackColor = value
        '    End Set
        'End Property
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
            'If Not My.Computer.FileSystem.FileExists(Filename) Then
            '    Throw New IO.IOException("Missing File")
            'End If
            Me.Width = Width
            Me.Height = Height

            'Me.Back = New Dial_Face(Filename, Width, Height)
            Me.cal = New Dial_Calibration(Width, Height)
            Me.needle = New Dial_Needle(Width, Height)
            Me.Treash = New Dial_Threshold(Width, Height)

        End Sub
        Public Sub New(ByVal Filename As String, ByVal Width As Single, ByVal Height As Single)
            If Not My.Computer.FileSystem.FileExists(Filename) Then
                Throw New IO.IOException("Missing File")
            End If
            Me.Width = Width
            Me.Height = Height

            Me.Back = New Dial_Face(Filename, Width, Height)
            Me.cal = New Dial_Calibration(Width, Height)
            Me.needle = New Dial_Needle(Width, Height)
            Me.Treash = New Dial_Threshold(Width, Height)

        End Sub
        Public Sub New(ByVal Img As Bitmap, ByVal Width As Single, ByVal Height As Single)
            'Me.X = X
            ' Me.Y = Y
            Me.Width = Width
            Me.Height = Height

            Me.Back = New Dial_Face(Img, Width, Height)
            Me.cal = New Dial_Calibration(Width, Height)
            Me.needle = New Dial_Needle(Width, Height)
            Me.Treash = New Dial_Threshold(Width, Height)

        End Sub

#End Region

#Region "Method"
        Public Sub Render(ByVal e As Graphics, ByVal cx As Integer, ByVal cy As Integer, Optional ByVal RenderBack As Boolean = True, Optional ByVal RenderFront As Boolean = True, Optional ByVal OriginCenter As Boolean = True)

            If BackImg <> "" And Me.Back Is Nothing Then
                Me.Back = New Dial_Face(BackImg, Width, Height)
            End If

            Dim img As New Bitmap(Me.Width, Me.Width, 0, PixelFormat.Format32bppPArgb, Nothing)
            ' img = img.Clone(New Rectangle(0, 0, img.Width, img.Height), PixelFormat.Format32bppPArgb)


            Dim g As Graphics = Graphics.FromImage(img)

            e.SmoothingMode = SmoothingMode.AntiAlias

            Me.cal.MinValue = Me.MinValue
            Me.cal.MaxValue = Me.MaxValue
            Me.cal.NoOfSubDivisions = Me.NoOfSubDivisions
            Me.cal.NoOfDivisions = Me.NoOfDivisions
            Me.cal.ForeColor = Me.ForeColor

            needle.MinValue = Me.MinValue
            needle.MaxValue = Me.MaxValue
            needle.Value = Me.Value

            Treash.MinValue = Me.MinValue
            Treash.MaxValue = Me.MaxValue
            Treash.Color = Color.Red
            Treash.Value = Me.RedLineMPH

            If Me.Back IsNot Nothing Then
                If RenderBack = True Then Me.Back.Render(g, Me.Width * 0.5, Me.Height * 0.5)
            End If

            If RenderBack = True Then Treash.Render(g, Me.Width * 0.5, Me.Height * 0.5)
            If RenderBack = True Then cal.Render(g, Me.Width * 0.5, Me.Height * 0.5)
            If RenderFront = True Then needle.Render(g, Me.Width * 0.5, Me.Height * 0.5)

            If OriginCenter = True Then
                e.DrawImage(img, CInt(cx - (Me.Width * 0.5)), CInt(cy - (Me.Height * 0.5)))
            Else
                e.DrawImage(img, CInt(cx), CInt(cy))
            End If

            Me.Back.Dispose()
            Me.Back = Nothing
            img.Dispose()
            g.Dispose()
            img = Nothing
            g = Nothing
        End Sub
#End Region


    End Class
End Namespace