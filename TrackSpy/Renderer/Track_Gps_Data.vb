Public Class Track_Gps_Data
#Region "Fields"
    Private _id As Integer
    Private _track_id As Integer
    Private _lat As Single
    Private _lng As Single
#End Region
#Region "Properties"
    Public Property lng() As Single
        Get
            Return _lng
        End Get
        Set(ByVal value As Single)
            _lng = value
        End Set
    End Property
    Public Property lat() As Single
        Get
            Return _lat
        End Get
        Set(ByVal value As Single)
            _lat = value
        End Set
    End Property
    Public Property track_id() As Integer
        Get
            Return _track_id
        End Get
        Set(ByVal value As Integer)
            _track_id = value
        End Set
    End Property
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property
#End Region
    Public Sub New(ByVal lng As Single, ByVal lat As Single, ByVal trackid As Integer, ByVal id As Integer)
        Me._id = id
        Me._lng = lng
        Me._lat = lat
        Me._track_id = trackid
        Me._id = id
    End Sub


End Class

