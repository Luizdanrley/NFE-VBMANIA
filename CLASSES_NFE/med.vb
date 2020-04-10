''' <summary>
''' Informar apenas quando se tratar de medicamentos, permite múltiplas ocorrências (ilimitado)
''' </summary>
Public Class med
    ''' <summary>
    ''' Número do Lote do medicamento
    ''' </summary>
    Private _nLote As String
    Public Property nLote() As String
        Get
            Return _nLote
        End Get
        Set(ByVal value As String)
            _nLote = value
        End Set
    End Property
    ''' <summary>
    ''' Quantidade de produto no Lote do medicamento
    ''' </summary>
    Private _qLote As Decimal
    <Formato("N0", "pt-BR")> _
    Public Property qLote() As Decimal
        Get
            Return _qLote
        End Get
        Set(ByVal value As Decimal)
            _qLote = value
        End Set
    End Property
    ''' <summary>
    ''' Data de fabricação
    ''' </summary>
    Private _dFab As DateTime
    Public Property dFab() As DateTime
        Get
            Return _dFab
        End Get
        Set(ByVal value As DateTime)
            _dFab = value
        End Set
    End Property
    ''' <summary>
    ''' Data de validade
    ''' </summary>
    Private _dVal As DateTime
    Public Property dVal() As DateTime
        Get
            Return _dVal
        End Get
        Set(ByVal value As DateTime)
            _dVal = value
        End Set
    End Property
    ''' <summary>
    ''' Preço máximo consumidor
    ''' </summary>
    Private _vPMC As Decimal
    Public Property vPMC() As Decimal
        Get
            Return _vPMC
        End Get
        Set(ByVal value As Decimal)
            _vPMC = value
        End Set
    End Property
End Class