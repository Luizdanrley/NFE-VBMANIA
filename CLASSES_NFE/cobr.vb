﻿''' <summary>
''' TAG de grupo de Cobrança
''' </summary>
Public Class cobr
    ''' <summary>
    ''' TAG de grupo da Fatura
    ''' </summary>
    '''
    Public Sub New()
        _Dup = New List(Of dup)
    End Sub

    Private _Dup As List(Of dup)
    Public Property Dup() As List(Of dup)
        Get
            Return _Dup
        End Get
        Set(ByVal value As List(Of dup))
            _Dup = value
        End Set
    End Property


End Class