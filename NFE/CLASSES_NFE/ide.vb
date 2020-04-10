Public Class ide
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Código da UF do emitente do Documento Fiscal. Utilizar a Tabela do IBGE de código de unidades da
    ''' federação (Anexo IV - Tabela de UF, Município e País)
    ''' </summary>
    Private _cUF As Integer
    Public Property cUF() As Integer
        Get
            Return _cUF
        End Get
        Set(ByVal value As Integer)
            _cUF = value
        End Set
    End Property
    ''' <summary>
    ''' Código numérico que compõe a Chave de Acesso. Número aleatório gerado pelo emitente para
    ''' cada NF-e para evitar acessos indevidos da NF-e.
    ''' </summary>
    Private _cNF As String
    Public Property cNF() As String
        Get
            Return _cNF
        End Get
        Set(ByVal value As String)
            _cNF = value
        End Set
    End Property
    ''' <summary>
    ''' Informar a natureza da operação de que decorrer a saída ou a entrada, tais como: venda, compra,
    ''' transferência, devolução, importação, consignação, remessa (para fins de demonstração, de
    ''' industrialização ou outra), conforme previsto na alínea 'i', inciso I, art. 19
    ''' do CONVÊNIO S/Nº, de 15 de dezembro de 1970
    ''' </summary>
    Private _natOp As String
    Public Property natOp() As String
        Get
            Return _natOp
        End Get
        Set(ByVal value As String)
            _natOp = value
        End Set
    End Property
    ''' <summary>
    ''' 0 – pagamento à vista;
    ''' 1 – pagamento à prazo;
    ''' 2 - outros.
    ''' </summary>
    Private _indPag As String
    Public Property indPag() As String
        Get
            Return _indPag
        End Get
        Set(ByVal value As String)
            _indPag = value
        End Set
    End Property
    ''' <summary>
    ''' Utilizar o código 55 para identificação da NF-e, emitida em substituição ao modelo 1 ou 1A.
    ''' </summary>
    Private _mod As String
    Public Property [mod]() As String
        Get
            Return _mod
        End Get
        Set(ByVal value As String)
            _mod = value
        End Set
    End Property
    ''' <summary>
    ''' Série do Documento Fiscal, informar 0 (zero) para série única.
    ''' </summary>
    Private _serie As String
    Public Property serie() As String
        Get
            Return _serie
        End Get
        Set(ByVal value As String)
            _serie = value
        End Set
    End Property
    ''' <summary>
    ''' Número do Documento Fiscal.
    ''' </summary>
    Private _nNF As String
    Public Property nNF() As String
        Get
            Return _nNF
        End Get
        Set(ByVal value As String)
            _nNF = value
        End Set
    End Property
    ''' <summary>
    ''' Data de emissão do documento fiscal, Formato “AAAA-MM-DD”
    ''' </summary>
    Private _dEmi As DateTime
    Public Property dEmi() As DateTime
        Get
            Return _dEmi
        End Get
        Set(ByVal value As DateTime)
            _dEmi = value
        End Set
    End Property
    ''' <summary>
    ''' Data de saída ou da entrada da mercadoria/produto, Formato “AAAA-MM-DD”
    ''' </summary>
    Private _dSaiEnt As DateTime
    Public Property dSaiEnt() As DateTime
        Get
            Return _dSaiEnt
        End Get
        Set(ByVal value As DateTime)
            _dSaiEnt = value
        End Set
    End Property
    ''' <summary>
    ''' 0-entrada / 1-saída
    ''' </summary>
    Private _tpNF As Integer
    Public Property tpNF() As Integer
        Get
            Return _tpNF
        End Get
        Set(ByVal value As Integer)
            _tpNF = value
        End Set
    End Property
    ''' <summary>
    ''' Informar o município de ocorrência do fato gerador do ICMS. Utilizar a
    ''' Tabela do IBGE (Anexo IV - Tabela de UF, Município e País).
    ''' </summary>
    Private _cMunFG As Integer
    Public Property cMunFG() As Integer
        Get
            Return _cMunFG
        End Get
        Set(ByVal value As Integer)
            _cMunFG = value
        End Set
    End Property
    ''' <summary>
    ''' Grupo com as informações das NF/NF-e referenciadas.
    ''' </summary>

    Private _tpImp As Integer
    Public Property tpImp() As Integer
        Get
            Return _tpImp
        End Get
        Set(ByVal value As Integer)
            _tpImp = value
        End Set
    End Property
    ''' <summary>
    ''' 1 – Normal – emissão normal;
    ''' 2 – Contingência FS – emissão em contingência com impressão do DANFE em Formulário de Segurança;
    ''' 3 – Contingência SCAN – emissão em contingência no Sistema de Contingência do Ambiente Nacional – SCAN;
    ''' 4 – Contingência DPEC - emissão em contingência com envio da Declaração Prévia de Emissão em Contingência – DPEC;
    ''' 5 – Contingência FS-DA - emissão em contingência com impressão do DANFE em Formulário de Segurança para Impressão de
    ''' Documento Auxiliar de Documento Fiscal Eletrônico (FS-DA).
    ''' </summary>
    Private _tpEmis As String
    Public Property tpEmis() As String
        Get
            Return _tpEmis
        End Get
        Set(ByVal value As String)
            _tpEmis = value
        End Set
    End Property
    ''' <summary>
    ''' Informar o DV da Chave de Acesso da NF-e, o DV será calculado com a aplicação do algoritmo módulo 11
    ''' (base 2,9) da Chave de Acesso. (vide item 5 do Manual de Integração)
    ''' </summary>
    Private _cDV As String
    Public Property cDV() As String
        Get
            Return _cDV
        End Get
        Set(ByVal value As String)
            _cDV = value
        End Set
    End Property
    ''' <summary>
    ''' 1-Produção/ 2-Homologação
    ''' </summary>
    Private _tpAmb As Integer
    Public Property tpAmb() As Integer
        Get
            Return _tpAmb
        End Get
        Set(ByVal value As Integer)
            _tpAmb = value
        End Set
    End Property
    ''' <summary>
    ''' 1- NF-e normal/ 2-NF-e complementar / 3 – NF-e de ajuste
    ''' </summary>
    Private _finNFe As String
    Public Property finNFe() As String
        Get
            Return _finNFe
        End Get
        Set(ByVal value As String)
            _finNFe = value
        End Set
    End Property
    ''' <summary>
    ''' Identificador do processo de emissão da NF-e: 0 - emissão de NF-e com aplicativo do contribuinte;
    ''' 1 - emissão de NF-e avulsa pelo Fisco;
    ''' 2 - emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco;
    ''' 3- emissão NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.
    ''' </summary>
    Private _procEmi As String
    Public Property procEmi() As String
        Get
            Return _procEmi
        End Get
        Set(ByVal value As String)
            _procEmi = value
        End Set
    End Property
    ''' <summary>
    ''' Identificador da versão do processo de emissão (informar a versão do aplicativo emissor de NF-e).
    ''' </summary>
    Private _verProc As String
    Public Property verProc() As String
        Get
            Return _verProc
        End Get
        Set(ByVal value As String)
            _verProc = value
        End Set
    End Property

    Private _NFRef As NFref
    Public Property NFRef() As NFref
        Get
            Return _NFRef
        End Get
        Set(ByVal value As NFref)
            _NFRef = value
        End Set
    End Property
End Class