Namespace Dials.Object
    Public Class Dial_Base
#Region "Fields"
        'Private _X As Integer
        'Private _Y As Integer
        Private _Width As Integer
        Private _Height As Integer
#End Region

#Region "Properties"
        'Public Property X() As Integer
        '    Get
        '        Return _X
        '    End Get
        '    Set(ByVal value As Integer)
        '        _X = value
        '    End Set
        'End Property
        'Public Property Y() As Integer
        '    Get
        '        Return _Y
        '    End Get
        '    Set(ByVal value As Integer)
        '        _Y = value
        '    End Set
        'End Property
        Public Property Width() As Integer
            Get
                Return _Width
            End Get
            Set(ByVal value As Integer)
                _Width = value
            End Set
        End Property
        Public Property Height() As Integer
            Get
                Return _Height
            End Get
            Set(ByVal value As Integer)
                _Height = value
            End Set
        End Property
#End Region

#Region "CONSTRUCTOR"
        Public Sub New()
        End Sub
        Public Sub New(ByVal X As Single, ByVal Y As Single, ByVal Width As Integer, ByVal Height As Integer)
            'Me._X = X
            ' Me._Y = Y
            Me._Width =Cint(Width)
            Me._Height = Cint(Height)
        End Sub
#End Region

    End Class
End Namespace