
Imports System
Namespace Exceptions
    Public Class DataMappingException
        Inherits Exception
        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub
    End Class
End Namespace