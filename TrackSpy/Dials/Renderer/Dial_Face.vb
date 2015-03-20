Imports TrackSpyLib.Dials.Object
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System
Namespace Dials.Renderer
    Public Class Dial_Face
        Inherits  Dials.Object.Dial_Base
#Region "Fields"
        Private _BackImg As Bitmap = Nothing
#End Region
#Region ""

        Public Property BackImg() As Bitmap
            Get
                Return _BackImg
            End Get
            Set(ByVal value As Bitmap)
                _BackImg = value
            End Set
        End Property
#End Region
#Region "CONSTRUCTOR"
        Public Sub New()
        End Sub
        Public Sub New(ByVal Filename As String)
            If Not My.Computer.FileSystem.FileExists(Filename) Then
                Throw New IO.IOException("Missing File")
            End If
            _BackImg = New Bitmap(Filename)
            _BackImg = _BackImg.Clone(New Rectangle(0, 0, _BackImg.Width, _BackImg.Height), PixelFormat.Format32bppPArgb)
            Me.Width = -1
            Me.Height = -1
        End Sub
        Public Sub New(ByVal Filename As String, ByVal Width As Single, ByVal Height As Single)
            If Not My.Computer.FileSystem.FileExists(Filename) Then
                Throw New IO.IOException("Missing File")
            End If
            _BackImg = New Bitmap(Filename)
            _BackImg = _BackImg.Clone(New Rectangle(0, 0, _BackImg.Width, _BackImg.Height), PixelFormat.Format32bppPArgb)

            Me.Width = Width
            Me.Height = Height
        End Sub
        Public Sub New(ByVal Img As Bitmap, ByVal Width As Single, ByVal Height As Single)
            'Me.X = X
            ' Me.Y = Y
            _BackImg = Img
            Me.Width = Width
            Me.Height = Height
        End Sub
        Public Sub New(ByVal Img As Bitmap)
            'Me.X = X
            ' Me.Y = Y
            _BackImg = Img
            Me.Width = -1
            Me.Height = -1
        End Sub
#End Region
#Region "Method"
        ''' <summary>
        ''' Draws the Ruler
        ''' </summary>
        ''' <param name="e"></param>
        Public Sub Render(ByVal e As Graphics, ByVal cx As Single, ByVal cy As Single)
            Dim Titem As New Bitmap(Me.Width, Me.Width, 0, PixelFormat.Format32bppPArgb, Nothing)
            Dim g As Graphics = Graphics.FromImage(Titem)

            g.SmoothingMode = SmoothingMode.AntiAlias
            If Me.Width = -1 And Me.Height = -1 Then
                g.DrawImage(Me._BackImg, New Point(0, 0))
            Else
                g.DrawImage(Me._BackImg, New RectangleF(0, 0, Me.Width, Me.Height))
            End If
            e.DrawImage(Titem, CInt(cx - (Me.Width * 0.5)), CInt(cy - (Me.Height * 0.5)))
            Titem.Dispose()
            g.Dispose()
            Titem = Nothing
            g = Nothing
        End Sub
#End Region
        Public Sub Dispose()
            _BackImg.Dispose()
            _BackImg = Nothing
        End Sub
        Protected Overrides Sub Finalize()

            MyBase.Finalize()

        End Sub
    End Class
End Namespace