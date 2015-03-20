Imports System.Reflection
Imports System
Namespace PlugIn
    Public Class PluginManager

        Public Sub LoadPlugin(ByVal Filename As String)
            Dim asm As Assembly = Assembly.LoadFrom(Filename)
            Dim myType As System.Type = asm.GetType(asm.GetName.Name + ".Plugin")
            Dim implementsIPlugin As Boolean = GetType(IPlugin).IsAssignableFrom(myType)
            If implementsIPlugin Then
               ' Console.WriteLine(asm.GetName.Name + " is a valid plugin!")
                Dim plugin As IPlugin = CType(Activator.CreateInstance(myType), IPlugin)
                'Console.WriteLine("{0}: {1}", plugin.Name,  plugin.)
                'lugin.PerformAction()
            End If
        End Sub
    End Class
End Namespace