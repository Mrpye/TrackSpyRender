Imports System.ComponentModel
Imports System
Namespace Dials.Object
    Public Class Round_Dial_Base
        Inherits Dial_Base
#Region "Fields"
        Private _MinValue As Single = 0
        Private _MaxValue As Single = 130
        Private _FromAngle As Single = 135.0F
        Private _ToAngle As Single = 405.0F
        Private _CurrentValue As Single = 0
#End Region
#Region "Properties"
        <DefaultValue(0)> _
        <Description("Mininum value on the scale")> _
        Public Property MinValue() As Single
            Get
                Return _MinValue
            End Get
            Set(ByVal value As Single)
                _MinValue = value
            End Set
        End Property
        <DefaultValue(100)> _
        <Description("Maximum value on the scale")> _
        Public Property MaxValue() As Single
            Get
                Return _MaxValue
            End Get
            Set(ByVal value As Single)
                _MaxValue = value
            End Set
        End Property
        Public Property FromAngle() As Single
            Get
                Return _FromAngle
            End Get
            Set(ByVal value As Single)
                _FromAngle = value
            End Set
        End Property
        Public Property ToAngle() As Single
            Get
                Return _ToAngle
            End Get
            Set(ByVal value As Single)
                _ToAngle = value
            End Set
        End Property
        <DefaultValue(0)> _
        <Description("Value where the pointer will point to.")> _
        Public Property Value() As Single
            Get
                Return _CurrentValue
            End Get
            Set(ByVal value As Single)
                _CurrentValue = value
            End Set
        End Property
#End Region
#Region "Methods"
        ''' <summary>
        ''' Converts the given degree to radian.
        ''' </summary>
        ''' <param name="theta"></param>
        ''' <returns></returns>
        Friend Function GetRadian(ByVal theta As Single) As Single
            Return theta * CSng(Math.PI) / 180.0F
        End Function
#End Region
    End Class
End Namespace
