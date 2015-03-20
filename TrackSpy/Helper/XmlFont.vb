Imports System.Drawing
Namespace Helper
    Public Class XmlFont
        Public FontFamily As String
        Public GraphicsUnit As GraphicsUnit
        Public Size As Single
        Public Style As FontStyle

        Public Sub New()
        End Sub
        Public Sub New(ByVal f As Font)
            FontFamily = f.FontFamily.Name
            GraphicsUnit = f.Unit
            Size = f.Size
            Style = f.Style
        End Sub

        Public Function ToFont() As Font
            Return New Font(FontFamily, Size, Style, GraphicsUnit)
        End Function
    End Class
End Namespace