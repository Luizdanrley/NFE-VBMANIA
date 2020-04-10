''' <summary>
''' TAG de grupo de Valores Totais referentes ao ICMS
''' </summary>
Public Class ICMSTot
    ''' <summary>
    ''' Base de Cálculo do ICMS
    ''' </summary>
    Private _vBC As Double
    Public Property vBC() As Double
        Get
            Return _vBC
        End Get
        Set(ByVal value As Double)
            _vBC = value
        End Set
    End Property
    ''' <summary>
    ''' Valor Total do ICMS
    ''' </summary>
    Private _vICMS As Double
    Public Property vICMS() As Double
        Get
            Return _vICMS
        End Get
        Set(ByVal value As Double)
            _vICMS = value
        End Set
    End Property
    ''' <summary>
    ''' Base de Cálculo do ICMS ST
    ''' </summary>
    Private _vBCST As Double
    Public Property vBCST() As Double
        Get
            Return _vBCST
        End Get
        Set(ByVal value As Double)
            _vBCST = value
        End Set
    End Property
    ''' <summary>
    ''' Valor Total do ICMS ST
    ''' </summary>
    Private _vST As Double
    Public Property vST() As Double
        Get
            Return _vST
        End Get
        Set(ByVal value As Double)
            _vST = value
        End Set
    End Property
    ''' <summary>
    ''' Valor Total dos produtos e serviços
    ''' </summary>
    Private _vProd As Double
    Public Property vProd() As Double
        Get
            Return _vProd
        End Get
        Set(ByVal value As Double)
            _vProd = value
        End Set
    End Property
    ''' <summary>
    ''' Valor Total do Frete
    ''' </summary>
    Private _vFrete As Double
    Public Property vFrete() As Double
        Get
            Return _vFrete
        End Get
        Set(ByVal value As Double)
            _vFrete = value
        End Set
    End Property
    ''' <summary>
    ''' Valor Total do Seguro
    ''' </summary>
    Private _vSeg As Double
    Public Property vSeg() As Double
        Get
            Return _vSeg
        End Get
        Set(ByVal value As Double)
            _vSeg = value
        End Set
    End Property
    ''' <summary>
    ''' Valor Total do Desconto
    ''' </summary>
    Private _vDesc As Double
    Public Property vDesc() As Double
        Get
            Return _vDesc
        End Get
        Set(ByVal value As Double)
            _vDesc = value
        End Set
    End Property
    ''' <summary>
    ''' Valor Total do II
    ''' </summary>
    Private _vII As Double
    Public Property vII() As Double
        Get
            Return _vII
        End Get
        Set(ByVal value As Double)
            _vII = value
        End Set
    End Property
    ''' <summary>
    ''' Valor Total do IPI
    ''' </summary>
    Private _vIPI As Double
    Public Property vIPI() As Double
        Get
            Return _vIPI
        End Get
        Set(ByVal value As Double)
            _vIPI = value
        End Set
    End Property
    ''' <summary>
    ''' Valor Total do PIS
    ''' </summary>
    Private _vPIS As Double
    Public Property vPIS() As Double
        Get
            Return _vPIS
        End Get
        Set(ByVal value As Double)
            _vPIS = value
        End Set
    End Property
    ''' <summary>
    ''' Valor total do COFINS
    ''' </summary>
    Private _vCOFINS As Double
    Public Property vCOFINS() As Double
        Get
            Return _vCOFINS
        End Get
        Set(ByVal value As Double)
            _vCOFINS = value
        End Set
    End Property
    ''' <summary>
    ''' Outras Despesas acessórias
    ''' </summary>
    Private _vOutro As Double
    Public Property vOutro() As Double
        Get
            Return _vOutro
        End Get
        Set(ByVal value As Double)
            _vOutro = value
        End Set
    End Property
    ''' <summary>
    ''' Valor total da NFE
    ''' </summary>
    Private _vNF As Double
    Public Property vNF() As Double
        Get
            Return _vNF
        End Get
        Set(ByVal value As Double)
            _vNF = value
        End Set
    End Property
End Class