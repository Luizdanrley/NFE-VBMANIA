﻿''' <summary>
''' H01
''' </summary>
Public Class det
    Private _prod As prod
    Private _imposto As imposto
    Public Sub New()
        _prod = New prod()
        _imposto = New imposto()
    End Sub

    Private _nItem As Integer
    Public Property nItem() As Integer
        Get
            Return _nItem
        End Get
        Set(ByVal value As Integer)
            _nItem = value
        End Set
    End Property
    Public ReadOnly Property Prod() As prod
        Get
            Return _prod
        End Get
    End Property

    Public ReadOnly Property Imposto() As imposto
        Get
            Return _imposto
        End Get
    End Property
End Class