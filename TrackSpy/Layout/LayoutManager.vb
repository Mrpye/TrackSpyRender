Imports System.Xml.Serialization
Imports System
Imports System.Collections
Imports System.Collections.Generic
Namespace Layout
    Public Class LayoutManager
        Private _Layouts As New List(Of LayoutItem)
        Public Property Layouts() As List(Of LayoutItem)
            Get
                Return _Layouts
            End Get
            Set(ByVal value As List(Of LayoutItem))
                _Layouts = value
            End Set
        End Property

        Public Sub Save(ByVal filename As String)
            Dim mlSerializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(LayoutManager))
            Dim writer As IO.StringWriter = New IO.StringWriter()
            mlSerializer.Serialize(writer, Me)
            My.Computer.FileSystem.WriteAllText(filename, writer.ToString, False, System.Text.Encoding.Unicode)
        End Sub
        Public Function SaveToString() As String
            Dim mlSerializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(LayoutManager))
            Dim writer As IO.StringWriter = New IO.StringWriter()
            mlSerializer.Serialize(writer, Me)
            Return writer.ToString
        End Function
        Public Shared Function LoadFromString(ByVal Data As String) As LayoutManager
            If String.IsNullOrEmpty(Data) Then
                Return Nothing
            End If
            Try
                Dim string_reader As IO.StringReader
                Dim Tstr As String = Data
                Dim xml_serializer As New XmlSerializer(GetType(LayoutManager))
                ' Make a StringReader holding the serialization.
                string_reader = New IO.StringReader(Tstr)
                ' Create the new Person object from the serialization
                Dim TObj As Object = xml_serializer.Deserialize(string_reader)
                Return TObj
            Catch ex As Exception
                Return Nothing
            End Try

        End Function
        Public Shared Function LoadFromFile(ByVal filename As String) As LayoutManager
            Dim string_reader As IO.StringReader
            Dim Tstr As String = My.Computer.FileSystem.ReadAllText(filename)
            Dim xml_serializer As New XmlSerializer(GetType(LayoutManager))
            ' Make a StringReader holding the serialization.
            string_reader = New IO.StringReader(Tstr)
            ' Create the new Person object from the serialization.
            Dim TObj As Object = xml_serializer.Deserialize(string_reader)
            Return TObj
        End Function
    End Class
End Namespace