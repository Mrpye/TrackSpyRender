Imports System.Reflection
Imports System
Namespace Helper
    Public Class ReflectionHelper
        Public Shared Function GetFieldValue(ByVal DataContext As Data.DataReader, ByVal PropertyName As String) As Object
            Try
                Dim Pi As FieldInfo = DataContext.GetType.GetField(PropertyName)
                Return Pi.GetValue(DataContext)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Shared Sub SetFieldValue(ByVal Obj As Object, ByVal PropertyName As String, ByVal Value As Object)
            Try
                Dim Pi As FieldInfo = Obj.GetType.GetField(PropertyName)
                Pi.SetValue(Obj, Value)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub
        Public Shared Sub SetPropertyValue(ByVal Obj As Object, ByVal PropertyName As String, ByVal Value As Object)
            Try
                Dim Pi As PropertyInfo = Obj.GetType.GetProperty(PropertyName)
                Pi.SetValue(Obj, Convert.ChangeType(Value, Pi.PropertyType), Nothing)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub
        Public Shared Function GetPropertyValue(ByVal DataContext As Data.DataReader, ByVal PropertyName As String) As Object
            Try
                Dim Pi As PropertyInfo = DataContext.GetType.GetProperty(PropertyName)
                Return Pi.GetValue(DataContext, Nothing)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace