'CLASSE PARA IMPRESSÃO DO DANFE SEM O USO DE GERADORES
'DE RELATÓRIOS E BIBLIOTECAS DE CÓDIGO DE BARRAS DE TERCEIROS

' NOME: DANIEL COUTINHO DE MELO
'EMAIL: DANIELCPAETE@GMAIL.COM
' DATA: 24-02-2010 PORTO ALEGRE - RS
Imports System.Drawing.Printing
Imports System.Collections.Generic

Public Class ImprimirDanfe
    Private ControlaImpressao As Integer = 0
    Private ContaProdutos As Integer = 0
    Private TotalFolha As Integer = 1

    'FONTES DA DANFE
    Private Font12_B As New Font("Times New Roman", 12, FontStyle.Bold)
    Private Font12 As New Font("Times New Roman", 12, FontStyle.Regular)
    Private Font6 As New Font("Times New Roman", 6, FontStyle.Regular)
    Private Font6_Courier As New Font("Courier New", 6, FontStyle.Regular)
    Private Font6_B As New Font("Times New Roman", 6, FontStyle.Bold)
    Private Font5 As New Font("Times New Roman", 5, FontStyle.Regular)
    Private Font5_B As New Font("Courier New", 5, FontStyle.Bold)
    Private Font12_S As New Font("Times New Roman", 12, FontStyle.Underline)
    Private Font8 As New Font("Times New Roman", 8, FontStyle.Regular)
    Private Font7 As New Font("Times New Roman", 7, FontStyle.Regular)
    Private Font10 As New Font("Times New Roman", 10, FontStyle.Regular)
    Private Font10_B As New Font("Times New Roman", 10, FontStyle.Bold)
    Private Font10_S As New Font("Times New Roman", 10, FontStyle.Underline)
    Private Font8_B As New Font("Times New Roman", 8, FontStyle.Bold)


    'DADOS DO EMITENTE
    Public Structure DEmitente
        Dim NOME As String
        Dim ENDERECO_COMPLETO As String
        Dim TELEFONE As String
        Dim CEP As String
        Dim MUNICIPIO As String
        Dim UF As String
        Dim IE As String
        Dim IESUBS As String
        Dim CNPJ As String
    End Structure

    'DADOS DO DESTINATARIO
    Public Structure DDestinatario
        Dim NOME As String
        Dim CEP As String
        Dim ENDERECO As String
        Dim BAIRRO As String
        Dim MUNICIPIO As String
        Dim UF As String
        Dim TELEFONE As String
        Dim IE As String
        Dim CNPJ As String
    End Structure

    'DADOS DAS DATAS DA NOTA EMISSAO DATA SAIDA E HORA SAIDA
    Public Structure DDataeHora
        Dim DATA_EMISSAO As String
        Dim DATA_ENTRADA_SAIDA As String
        Dim HORA_ENTRADA_SAIDA As String
    End Structure

    'DADOS DOS VALORES TOTAIS 
    Public Structure DValores
        Dim BASE_CALCULO_ICMS As String
        Dim VALOR_ICMS As String
        Dim BASE_CALCULO_ICMS_SUBS As String
        Dim VALOR_ICMS_SUBS As String
        Dim VALOR_TOTAL_PRODUTOS As String
        Dim VALOR_FRETE As String
        Dim VALOR_SEGURO As String
        Dim DESCONTO As String
        Dim OUTRAS_DESPESAS As String
        Dim VALOR_IPI As String
        Dim VALOR_TOTAL_NOTA As String
    End Structure

    'TRANSPORTADOR VOLUMES E TRANSPORTADO
    'TIPO DE PAGAMENTO DE FRETE
    Public Enum TipoTransportadora
        EMITENDE = 0
        DESTINATARIO = 1
    End Enum

    Public Enum TipoNota
        ENTRADA = 0
        SAIDA = 1
    End Enum

    'DADOS DA TRANSPORTADORA E VOLUME
    Public Structure DTransportadora
        Dim NOME As String
        Dim CEP As String
        Dim ENDERECO As String
        Dim MUNICIPIO As String
        Dim PLACA_VEICULO As String
        Dim UF_PLACA As String
        Dim TIPO_PAGAMENTO As TipoTransportadora
        Dim CODIGO_ANTT As String
        Dim UF As String
        Dim IE As String
        Dim CNPJ As String
        Dim QUANTIDADE As String
        Dim ESPECIE As String
        Dim MARCA As String
        Dim NUMERO As String
        Dim PESOBRUTO As String
        Dim PESOLIQUIDO As String
    End Structure

    'DADOS DO ISSQN
    Public Structure DISSQN
        Dim IM As String
        Dim VALOR_TOTAL_SERVICOS As String
        Dim BASE_CALCULO_ISSQN As String
        Dim VALOR_ISSQN As String
    End Structure

    'DADOS DO ISSQN
    Public Structure DINFOCOMPLEMENTAR
        Dim LINHA1 As String
        Dim LINHA2 As String
        Dim LINHA3 As String
        Dim LINHA4 As String
        Dim LINHA5 As String
        Dim LINHA6 As String
    End Structure

    'VARIAVEIS QUE VÃO ARMAZENAR OS DADOS DA NOTA
    Private V_DEmitente As DEmitente
    Private V_DDestinatario As DDestinatario
    Private V_DDataeHora As DDataeHora
    Private V_DValores As DValores
    Private V_DTransportadora As DTransportadora
    Private V_DISSQN As DISSQN
    Private S_PROTOCOLO As String
    Private S_NATUREZA As String
    Private S_TIPONOTA As String = "1"
    Private S_INFOCOMP As DINFOCOMPLEMENTAR

    'MATRIZ DE CLASSES DE PRODUTOS DA DANFE
    'CADA PRODUTO DA DANFE É UMA CLASSE
    '----------------------------------------------------------
    Private V_PRODUTOS As IList(Of ProdutoDanfe)
    Public Property AddProdutosDanfe() As IList(Of ProdutoDanfe)
        Get
            Return V_PRODUTOS
        End Get
        Set(ByVal value As IList(Of ProdutoDanfe))
            V_PRODUTOS = value
        End Set
    End Property
    '----------------------------------------------------------

    Public WriteOnly Property ProtocoloAutorizacao() As String
        Set(ByVal Valor As String)
            S_PROTOCOLO = Valor
        End Set
    End Property

    Public WriteOnly Property NaturezaOperacao() As String
        Set(ByVal Valor As String)
            S_NATUREZA = Valor
        End Set
    End Property
    Public WriteOnly Property TipoNotaFiscal() As TipoNota
        Set(ByVal Valor As TipoNota)
            S_TIPONOTA = Valor
        End Set
    End Property
    Public WriteOnly Property Identificacao_Emitente() As DEmitente
        Set(ByVal Valor As DEmitente)
            V_DEmitente = Valor
        End Set
    End Property
    Public WriteOnly Property Identificacao_Destinatario() As DDestinatario
        Set(ByVal Valor As DDestinatario)
            V_DDestinatario = Valor
        End Set
    End Property
    Public WriteOnly Property Data_Hora() As DDataeHora
        Set(ByVal Valor As DDataeHora)
            V_DDataeHora = Valor
        End Set
    End Property
    Public WriteOnly Property Valores_Nota() As DValores
        Set(ByVal Valor As DValores)
            V_DValores = Valor
        End Set
    End Property
    Public WriteOnly Property Identificacao_Transportadora() As DTransportadora
        Set(ByVal Valor As DTransportadora)
            V_DTransportadora = Valor
        End Set
    End Property
    Public WriteOnly Property Valores_ISSQN() As DISSQN
        Set(ByVal Valor As DISSQN)
            V_DISSQN = Valor
        End Set
    End Property
    Public WriteOnly Property InformacoesComplementares() As DINFOCOMPLEMENTAR
        Set(ByVal Valor As DINFOCOMPLEMENTAR)
            S_INFOCOMP = Valor
        End Set
    End Property

    'SEMPRE QUE UMA CHAMADA A DANFE É FEITA A CHAVE DE ACESSO DEVE SER INFORMADA
    Private Str_ChavedeAcesso As String
    Public Sub New(ByVal ChavedeAcessoNFe As String)
        Str_ChavedeAcesso = ChavedeAcessoNFe

        'CRIA UMA NOVA LISTA GENERICA DE CLASSES DE PRODUTOS
        V_PRODUTOS = New List(Of ProdutoDanfe)()
    End Sub

    Public Sub VisualizarImpressao()

        Dim Prv_Visualizador As New PrintPreviewDialog
        Dim Doc_VisualizarDanfe As New PrintDocument
        Dim Prn_Dialogo As New PrintDialog

        'ASSOCIA OS EVENTOS DE IMPRESSÃO COM MINHAS FUNÇÕES
        AddHandler Doc_VisualizarDanfe.BeginPrint, AddressOf InicioImpressao
        AddHandler Doc_VisualizarDanfe.PrintPage, AddressOf ImprimirDanfe

        Prv_Visualizador.FormBorderStyle = FormBorderStyle.Fixed3D

        Doc_VisualizarDanfe.DefaultPageSettings.Landscape = True
        Doc_VisualizarDanfe.OriginAtMargins = True

        'AS MARGENS PRECISAM SER AJUSTADAS PARA QUE A IMPRESSÃO INICIAL TENHA 5 MILIMETROS
        Doc_VisualizarDanfe.DefaultPageSettings.Margins = New Printing.Margins(11, 11, 11, 11)

        Prv_Visualizador.Document = Doc_VisualizarDanfe

        Prv_Visualizador.ShowDialog()
        Prv_Visualizador.Close()
        Prv_Visualizador.Dispose()

    End Sub
    Private Sub InicioImpressao()
        'ZERA VARIAVEIS DE CONTROLE DE IMPRESSÃO
        ControlaImpressao = 0
        ContaProdutos = 0
        TotalFolha = 1
    End Sub

    Private Sub ImprimirDanfe(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        'VERIFICA SE É PRIMEIRA FOLHA OU DEMAIS PARA FORMATAR A IMPRESSÃO
        If ControlaImpressao = 0 Then
            InserirCabecalho(e)
            DesenharRetangulos(e)
            InserirTextos(e)
            Preencher_Dados_Danfe(e)
            Imprimir_Produtos(e)
        Else
            InserirCabecalho(e)
            Imprimir_Produtos(e)
            CamposdemaisPaginas(e)
        End If
    End Sub

    'GERA CABEÇALHO E GUIA DO CLIENTE
    Private Sub InserirCabecalho(ByVal Gra_Saida As System.Drawing.Printing.PrintPageEventArgs)

        Dim pen As New Pen(Brushes.Black, 0.1)
        Gra_Saida.Graphics.PageUnit = GraphicsUnit.Millimeter

        'RETANGULOS ENTRADA E SAÍDA E FRETE POR CONTA
        Gra_Saida.Graphics.DrawRectangle(Pen, 157, 14, 5, 5)

        'CONJUNTO DE LINHAS LATERAIS ESQUERDAS
        Gra_Saida.Graphics.DrawLine(Pen, 2, 201, 2, 2)
        Gra_Saida.Graphics.DrawLine(Pen, 23, 201, 23, 2)
        Gra_Saida.Graphics.DrawLine(Pen, 12, 201, 12, 36)
        Gra_Saida.Graphics.DrawLine(Pen, 2, 36, 23, 36)
        Gra_Saida.Graphics.DrawLine(Pen, 12, 137, 23, 137)

        'CONJUNTO DE LINHAS HORIZONTAIS DADOS EMPRESA
        Gra_Saida.Graphics.DrawLine(Pen, 25, 33, 289, 33)
        Gra_Saida.Graphics.DrawLine(Pen, 25, 40, 289, 40)
        Gra_Saida.Graphics.DrawLine(Pen, 25, 47, 289, 47)

        'CONJUNTO DE LINHAS VERTICAIS DADOS DA NOTA
        Gra_Saida.Graphics.DrawLine(pen, 130, 2, 130, 33)
        Gra_Saida.Graphics.DrawLine(pen, 166, 2, 166, 40)

        'INSCRIÇÃO ESTADUAL / INSCR. SUBST / CNPJ
        Gra_Saida.Graphics.DrawLine(pen, 112, 40, 112, 47)
        Gra_Saida.Graphics.DrawLine(pen, 201, 40, 201, 47)

        'CONJUNTO DE LINHAS HORIZONTAIS CODIGO DE BARRAS
        Gra_Saida.Graphics.DrawLine(pen, 166, 12, 289, 12)
        Gra_Saida.Graphics.DrawLine(pen, 166, 19, 289, 19)

        'IDENTIFICAÇÃO DO DOCUMENTO
        Gra_Saida.Graphics.DrawString("NOTA", Font12, Brushes.Black, 6.3, 2)
        Gra_Saida.Graphics.DrawString("FISCAL", Font12, Brushes.Black, 5.3, 6)
        Gra_Saida.Graphics.DrawString("Nº", Font12_B, Brushes.Black, 10.3, 11)
        Gra_Saida.Graphics.DrawString("SÉRIE 1", Font12, Brushes.Black, 5, 24)

        Gra_Saida.Graphics.DrawImage(Image.FromFile(My.Application.Info.DirectoryPath & "\logo.jpg"), 25, 2, 35, 8)
        Gra_Saida.Graphics.DrawString("Identificação do Emitente", Font10_B, Brushes.Black, 76, 2)
        Gra_Saida.Graphics.DrawString("DANFE", Font12_B, Brushes.Black, 140, 2)
        Gra_Saida.Graphics.DrawString("DOCUMENTO AUXILIAR DA", Font7, Brushes.Black, 131.2, 7)
        Gra_Saida.Graphics.DrawString("NOTA FISCAL ELETRÔNICA", Font7, Brushes.Black, 131.5, 10)
        Gra_Saida.Graphics.DrawString("0 - ENTRADA", Font7, Brushes.Black, 134.5, 13.5)
        Gra_Saida.Graphics.DrawString("1 - SAÍDA", Font7, Brushes.Black, 134.5, 17)
        Gra_Saida.Graphics.DrawString("N.º", Font10_B, Brushes.Black, 131.5, 21)
        Gra_Saida.Graphics.DrawString("SÉRIE 1", Font10_B, Brushes.Black, 131.5, 25)

        'CALCULA TOTAL DE FOLHAS
        Dim Resto As Integer
        If V_PRODUTOS.Count > 3 Then
            Resto = (V_PRODUTOS.Count - 3) Mod 8
            If Resto > 0 Then
                TotalFolha = 2 + ((V_PRODUTOS.Count - 3) - Resto) / 8
            Else
                TotalFolha = 1 + ((V_PRODUTOS.Count - 3) - Resto) / 8
            End If
        End If

        Gra_Saida.Graphics.DrawString("FOLHA " & ControlaImpressao + 1 & "/" & TotalFolha, Font10_B, Brushes.Black, 131.5, 29)

        'PRIMEIRA LINHA
        Dim CodigoBarra As CodigodeBarra
        CodigoBarra = New CodigodeBarra(CodigodeBarra.BCEncoding.Code128C)
        Gra_Saida.Graphics.DrawImage(CodigoBarra.DrawBarCode(Str_ChavedeAcesso), 184, 1, 100, 10)

        Gra_Saida.Graphics.DrawString("NATUREZA DA OPERAÇÃO", Font6, Brushes.Black, 25, 33.2)
        Gra_Saida.Graphics.DrawString("PROTOCOLO DE AUTORIZAÇÃO DE USO", Font6, Brushes.Black, 167, 33.2)
        Gra_Saida.Graphics.DrawString("CHAVE DE ACESSO", Font6, Brushes.Black, 167, 12.2)
        Gra_Saida.Graphics.DrawString("Consulta de autenticidade no portal nacional da NF-e", Font12, Brushes.Black, 167, 21)
        Gra_Saida.Graphics.DrawString("www.nfe.fazenda.gov.br/portal", Font12_S, Brushes.Black, 167, 26)
        Gra_Saida.Graphics.DrawString("ou no site da Sefaz Autorizadora", Font12, Brushes.Black, 220.9, 26)

        'SEGUNDA LINHA
        Gra_Saida.Graphics.DrawString("INSCRIÇÃO ESTADUAL", Font6, Brushes.Black, 25, 40.2)
        Gra_Saida.Graphics.DrawString("INSCR. ESTADUAL DO SUBST. TRIBUT.", Font6, Brushes.Black, 113, 40.2)
        Gra_Saida.Graphics.DrawString("CNPJ", Font6, Brushes.Black, 202, 40.2)

        Dim NChave As String = ""
        Dim ContarS As Integer = 0
        For x As Int16 = 0 To Str_ChavedeAcesso.Length - 1
            ContarS = ContarS + 1
            NChave = NChave & Str_ChavedeAcesso.Substring(x, 1)
            If ContarS = 4 Then
                ContarS = 0
                NChave = NChave & " "
            End If
        Next
        Gra_Saida.Graphics.DrawString(NChave, Font8_B, Brushes.Black, 167, 15)

        'NATUREZA, PROTOCOLO E TIPO DE NOTA
        Gra_Saida.Graphics.DrawString(S_TIPONOTA, Font10, Brushes.Black, 158, 14.5)
        Gra_Saida.Graphics.DrawString(S_NATUREZA, Font10, Brushes.Black, 25, 36.2)
        Gra_Saida.Graphics.DrawString(S_PROTOCOLO, Font10, Brushes.Black, 167, 36.2)

        'DADOS EMITENTE
        Gra_Saida.Graphics.DrawString(V_DEmitente.NOME, Font8_B, Brushes.Black, 65, 8)
        Gra_Saida.Graphics.DrawString(V_DEmitente.ENDERECO_COMPLETO, Font8_B, Brushes.Black, 75, 18)
        Gra_Saida.Graphics.DrawString(V_DEmitente.MUNICIPIO & ", " & V_DEmitente.UF, Font8_B, Brushes.Black, 82, 22)
        Gra_Saida.Graphics.DrawString("Fone: " & V_DEmitente.TELEFONE & " Cep " & V_DEmitente.CEP, Font8_B, Brushes.Black, 74.8, 26)

        'INICIO DO PREENCHIMENTO NOTA EMITENTE
        Gra_Saida.Graphics.DrawString(V_DEmitente.IE, Font10, Brushes.Black, 25, 43.2)
        Gra_Saida.Graphics.DrawString(V_DEmitente.IESUBS, Font10, Brushes.Black, 113, 43.2)
        Gra_Saida.Graphics.DrawString(V_DEmitente.CNPJ, Font10, Brushes.Black, 202, 43.2)

        'INSERIR BLOCOS DE CAMPOS
        Dim Formato_Vertical As System.Drawing.StringFormat
        Dim Posição_Texto As System.Drawing.Point
        Dim Rotacionador_de_Texto As System.Drawing.Drawing2D.Matrix = Gra_Saida.Graphics.Transform()

        Formato_Vertical = New StringFormat(StringFormatFlags.DirectionVertical)
        Posição_Texto = New Point(27.5, 65)

        Rotacionador_de_Texto.RotateAt(180, Posição_Texto)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto

        'GUIA DO CLIENTE
        Posição_Texto = New Point(47.5, -71)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("RECEBEMOS DE", Font6, Brushes.Black, Posição_Texto, Formato_Vertical)

        Posição_Texto = New Point(47.5, -53)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("TESTE DE EMPRESA", Font6_B, Brushes.Black, Posição_Texto, Formato_Vertical)

        Posição_Texto = New Point(47.5, -6)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString(", OS PRODUTOS OU SERVIÇOS CONSTANTES NA NOTA FISCAL ELETRÔNICA INDICADA AO LADO", Font6, Brushes.Black, Posição_Texto, Formato_Vertical)

        Posição_Texto = New Point(41, -71)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("DATA E HORA", Font6, Brushes.Black, Posição_Texto, Formato_Vertical)

        Posição_Texto = New Point(41, -6)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("IDENTIFICAÇÃO DO RECEBEDOR", Font6, Brushes.Black, Posição_Texto, Formato_Vertical)

        'VOLTA TEXTO PARA POSIÇÃO ORIGINAL
        Gra_Saida.Graphics.ResetTransform()

    End Sub

    'PREENCHE DADOS QUANDO É PRIMEIRA PAGINA
    Private Sub Preencher_Dados_Danfe(ByVal Gra_Saida As System.Drawing.Printing.PrintPageEventArgs)
        Gra_Saida.Graphics.PageUnit = GraphicsUnit.Millimeter

        'DADOS DO DESTINATARIO
        Gra_Saida.Graphics.DrawString(V_DDestinatario.NOME, Font10, Brushes.Black, 31, 50.2)
        Gra_Saida.Graphics.DrawString(V_DDestinatario.CNPJ, Font10, Brushes.Black, 187, 50.2)
        Gra_Saida.Graphics.DrawString(V_DDestinatario.ENDERECO, Font10, Brushes.Black, 31, 57.2)
        Gra_Saida.Graphics.DrawString(V_DDestinatario.BAIRRO, Font10, Brushes.Black, 156, 57.2)
        Gra_Saida.Graphics.DrawString(V_DDestinatario.CEP, Font10, Brushes.Black, 210, 57.2)
        Gra_Saida.Graphics.DrawString(V_DDestinatario.MUNICIPIO, Font10, Brushes.Black, 31, 64.2)
        Gra_Saida.Graphics.DrawString(V_DDestinatario.TELEFONE, Font10, Brushes.Black, 126, 64.2)
        Gra_Saida.Graphics.DrawString(V_DDestinatario.UF, Font10, Brushes.Black, 175, 64.2)
        Gra_Saida.Graphics.DrawString(V_DDestinatario.IE, Font10, Brushes.Black, 187, 64.2)

        'DATAS E HORAS
        Gra_Saida.Graphics.DrawString(V_DDataeHora.DATA_EMISSAO, Font10, Brushes.Black, 247, 50.2)
        Gra_Saida.Graphics.DrawString(V_DDataeHora.DATA_ENTRADA_SAIDA, Font10, Brushes.Black, 247, 57.2)
        Gra_Saida.Graphics.DrawString(V_DDataeHora.HORA_ENTRADA_SAIDA, Font10, Brushes.Black, 247, 64.2)

        'VALORES DA NOTA
        Dim Alinhamento As New StringFormat()
        Alinhamento.Alignment = StringAlignment.Far

        Gra_Saida.Graphics.DrawString(V_DValores.BASE_CALCULO_ICMS, Font10, Brushes.Black, New RectangleF(31, 78.2, 51, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_ICMS, Font10, Brushes.Black, New RectangleF(83, 78.2, 51, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.BASE_CALCULO_ICMS_SUBS, Font10, Brushes.Black, New RectangleF(135, 78.2, 51, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_ICMS_SUBS, Font10, Brushes.Black, New RectangleF(188, 78.2, 51, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_TOTAL_PRODUTOS, Font10, Brushes.Black, New RectangleF(242, 78.2, 46, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_FRETE, Font10, Brushes.Black, New RectangleF(31, 85.2, 43, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_SEGURO, Font10, Brushes.Black, New RectangleF(76, 85.2, 40, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.DESCONTO, Font10, Brushes.Black, New RectangleF(118, 85.2, 40, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.OUTRAS_DESPESAS, Font10, Brushes.Black, New RectangleF(160, 85.2, 40, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_IPI, Font10, Brushes.Black, New RectangleF(202, 85.2, 40, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_TOTAL_NOTA, Font10, Brushes.Black, New RectangleF(244, 85.2, 44, Font10.Height), Alinhamento)

        'DADOS TRANSPORTADORA
        Gra_Saida.Graphics.DrawString(V_DTransportadora.NOME, Font10, Brushes.Black, 31, 92.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.TIPO_PAGAMENTO, Font10, Brushes.Black, 162, 92.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.CODIGO_ANTT, Font10, Brushes.Black, 168, 81.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.PLACA_VEICULO, Font10, Brushes.Black, 196, 92.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.UF_PLACA, Font10, Brushes.Black, 232, 92.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.CNPJ, Font10, Brushes.Black, 244, 92.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.ENDERECO, Font10, Brushes.Black, 31, 99.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.MUNICIPIO, Font10, Brushes.Black, 144, 99.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.UF, Font10, Brushes.Black, 232, 99.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.IE, Font10, Brushes.Black, 244, 99.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.QUANTIDADE, Font10, Brushes.Black, 31, 106.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.ESPECIE, Font10, Brushes.Black, 65, 106.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.MARCA, Font10, Brushes.Black, 101, 106.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.NUMERO, Font10, Brushes.Black, 144, 106.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.PESOBRUTO, Font10, Brushes.Black, 197, 106.2)
        Gra_Saida.Graphics.DrawString(V_DTransportadora.PESOLIQUIDO, Font10, Brushes.Black, 245, 106.2)

        'DADOS ISSQN
        Gra_Saida.Graphics.DrawString(V_DISSQN.IM, Font10, Brushes.Black, 31, 166.2)
        Gra_Saida.Graphics.DrawString(V_DISSQN.VALOR_TOTAL_SERVICOS, Font10, Brushes.Black, 97, 166.2)
        Gra_Saida.Graphics.DrawString(V_DISSQN.BASE_CALCULO_ISSQN, Font10, Brushes.Black, 160, 166.2)
        Gra_Saida.Graphics.DrawString(V_DISSQN.VALOR_ISSQN, Font10, Brushes.Black, 226, 166.2)

        Gra_Saida.Graphics.DrawString(S_INFOCOMP.LINHA1, Font6, Brushes.Black, 31, 173.2)
        Gra_Saida.Graphics.DrawString(S_INFOCOMP.LINHA2, Font6, Brushes.Black, 31, 175.2)
        Gra_Saida.Graphics.DrawString(S_INFOCOMP.LINHA3, Font6, Brushes.Black, 31, 177.2)
        Gra_Saida.Graphics.DrawString(S_INFOCOMP.LINHA4, Font6, Brushes.Black, 31, 179.2)
        Gra_Saida.Graphics.DrawString(S_INFOCOMP.LINHA5, Font6, Brushes.Black, 31, 181.2)

    End Sub

    'DEMAIS PAGINAS SÃO PREENCHIDAS APENAS COM OS CAMPOS DE PRODUTOS
    Private Sub CamposdemaisPaginas(ByVal Gra_Saida As System.Drawing.Printing.PrintPageEventArgs)

        Gra_Saida.Graphics.PageUnit = GraphicsUnit.Millimeter
        Dim pen As New Pen(Brushes.Black, 0.1)

        'LINHA PRODUTOS 5 CENTIMETROS
        Gra_Saida.Graphics.DrawLine(pen, 31, 52, 289, 52)
        'CODIGO PRODUTO
        Gra_Saida.Graphics.DrawLine(pen, 48, 48, 48, 163)
        'DESCRIÇÃO DOS PRODUTOS
        Gra_Saida.Graphics.DrawLine(pen, 136, 48, 136, 163)
        'NCM/SH
        Gra_Saida.Graphics.DrawLine(pen, 146, 48, 146, 163)
        'CST
        Gra_Saida.Graphics.DrawLine(pen, 151, 48, 151, 163)
        'CFOP
        Gra_Saida.Graphics.DrawLine(pen, 157, 48, 157, 163)
        'UNIDADE
        Gra_Saida.Graphics.DrawLine(pen, 163, 48, 163, 163)
        'QUANTIDADE
        Gra_Saida.Graphics.DrawLine(pen, 171, 48, 171, 163)
        'VALOR UNITARIO
        Gra_Saida.Graphics.DrawLine(pen, 187, 48, 187, 163)
        'VALOR TOTAL
        Gra_Saida.Graphics.DrawLine(pen, 201, 48, 201, 163)
        'B CALC ICMS
        Gra_Saida.Graphics.DrawLine(pen, 214, 48, 214, 163)
        'VALOR ICMS
        Gra_Saida.Graphics.DrawLine(pen, 226, 48, 226, 163)
        'BASE DE CALCULO ICMS
        Gra_Saida.Graphics.DrawLine(pen, 244, 48, 244, 163)
        'VALOR ICMS ST
        Gra_Saida.Graphics.DrawLine(pen, 259, 48, 259, 163)
        'VALOR IPI
        Gra_Saida.Graphics.DrawLine(pen, 269, 48, 269, 163)
        'BASE DE CALCULO 
        Gra_Saida.Graphics.DrawLine(pen, 279, 48, 279, 163)

        Gra_Saida.Graphics.DrawLine(pen, 31, 163, 289, 163)
        Gra_Saida.Graphics.DrawLine(pen, 31, 170, 289, 170)
        Gra_Saida.Graphics.DrawLine(pen, 31, 177, 289, 177)

        'BASE DE CALCULO DO ICMS / VALOR ICMS / BASE SUBST. ICMS ST / VALOR SUBST ICMS / VALOR TOTAL DOS PRODUTOS 
        Gra_Saida.Graphics.DrawLine(pen, 82, 163, 82, 170)
        Gra_Saida.Graphics.DrawLine(pen, 134, 163, 134, 170)
        Gra_Saida.Graphics.DrawLine(pen, 187, 163, 187, 170)
        Gra_Saida.Graphics.DrawLine(pen, 241, 163, 241, 170)

        'VALOR FRETE / VALOR SEGURO / DESCONTO / OUTRAS DESPESAS ACESSORAIS / VALOR TOTAL DO IPI / VALOR TOTAL DA NOTA 
        Gra_Saida.Graphics.DrawLine(pen, 75, 170, 75, 177)
        Gra_Saida.Graphics.DrawLine(pen, 117, 170, 117, 177)
        Gra_Saida.Graphics.DrawLine(pen, 159, 170, 159, 177)
        Gra_Saida.Graphics.DrawLine(pen, 201, 170, 201, 177)
        Gra_Saida.Graphics.DrawLine(pen, 243, 170, 243, 177)

        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 25, 163, 5, 14)

        'GERA TEXTO VERTICAL
        Dim Formato_Vertical As System.Drawing.StringFormat
        Dim Posição_Texto As System.Drawing.Point
        Dim Rotacionador_de_Texto As System.Drawing.Drawing2D.Matrix = Gra_Saida.Graphics.Transform()

        Formato_Vertical = New StringFormat(StringFormatFlags.DirectionVertical)
        Posição_Texto = New Point(27.5, 65)

        Rotacionador_de_Texto.RotateAt(180, Posição_Texto)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto

        Posição_Texto = New Point(27.5, -44)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("CÁLCULO", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)
        Posição_Texto = New Point(25.5, -44)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("IMPOSTO", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)

        'VOLTA TEXTO PARA POSIÇÃO ORIGINAL
        Gra_Saida.Graphics.ResetTransform()

        'QUADRO DE PRODUTOS
        Gra_Saida.Graphics.DrawString("CÓD PROD", Font5, Brushes.Black, 34, 48.2)
        Gra_Saida.Graphics.DrawString("DESCRIÇÃO DOS PRODUTOS/SERVIÇOS", Font5, Brushes.Black, 78, 48.2)
        Gra_Saida.Graphics.DrawString("NCM/SH", Font5, Brushes.Black, 137, 48.2)
        Gra_Saida.Graphics.DrawString("CST", Font5, Brushes.Black, 146.5, 48.2)
        Gra_Saida.Graphics.DrawString("CFOP", Font5, Brushes.Black, 151.5, 48.2)
        Gra_Saida.Graphics.DrawString("UNID", Font5, Brushes.Black, 157.5, 48.2)
        Gra_Saida.Graphics.DrawString("QUANT.", Font5, Brushes.Black, 163.5, 48.2)
        Gra_Saida.Graphics.DrawString("VALOR UNITÁRIO", Font5, Brushes.Black, 171.5, 48.2)
        Gra_Saida.Graphics.DrawString("VALOR TOTAL", Font5, Brushes.Black, 187.5, 48.2)
        Gra_Saida.Graphics.DrawString("B. CALC. ICMS", Font5, Brushes.Black, 201.1, 48.2)
        Gra_Saida.Graphics.DrawString("VALOR ICMS", Font5, Brushes.Black, 214.5, 48.2)
        Gra_Saida.Graphics.DrawString("B. CALC. ICMS ST", Font5, Brushes.Black, 227.3, 48.2)
        Gra_Saida.Graphics.DrawString("VALOR ICMS ST", Font5, Brushes.Black, 244.5, 48.2)
        Gra_Saida.Graphics.DrawString("VALOR IPI", Font5, Brushes.Black, 259.5, 48.2)
        Gra_Saida.Graphics.DrawString("ALÍQUOTA", Font5, Brushes.Black, 269.2, 48.2)
        Gra_Saida.Graphics.DrawString("ICMS", Font5, Brushes.Black, 271.5, 49.8)
        Gra_Saida.Graphics.DrawString("ALÍQUOTA", Font5, Brushes.Black, 279.5, 48.2)
        Gra_Saida.Graphics.DrawString("IPI", Font5, Brushes.Black, 283, 49.8)

        'SEXTA LINHA
        Gra_Saida.Graphics.DrawString("BASE DE CÁLCULO DO ICMS", Font6, Brushes.Black, 31, 163.2)
        Gra_Saida.Graphics.DrawString("VALOR DO ICMS", Font6, Brushes.Black, 83, 163.2)
        Gra_Saida.Graphics.DrawString("BASE DE CÁLCULO DO ICMS ST", Font6, Brushes.Black, 135, 163.2)
        Gra_Saida.Graphics.DrawString("VALOR DO ICMS SUBSTITUIÇÃO", Font6, Brushes.Black, 188, 163.2)
        Gra_Saida.Graphics.DrawString("VALOR TOTAL DOS PRODUTOS", Font6, Brushes.Black, 242, 163.2)

        'SEXTA LINHA
        Gra_Saida.Graphics.DrawString("VALOR DO FRETE", Font6, Brushes.Black, 31, 170.2)
        Gra_Saida.Graphics.DrawString("VALOR SEGURO", Font6, Brushes.Black, 76, 170.2)
        Gra_Saida.Graphics.DrawString("DESCONTO", Font6, Brushes.Black, 118, 170.2)
        Gra_Saida.Graphics.DrawString("OUTRAS DESPESAS", Font6, Brushes.Black, 160, 170.2)
        Gra_Saida.Graphics.DrawString("VALOR DO IPI", Font6, Brushes.Black, 202, 170.2)
        Gra_Saida.Graphics.DrawString("VALOR TOTAL DA NOTA", Font6, Brushes.Black, 244, 170.2)

        'VALORES DA NOTA
        Dim Alinhamento As New StringFormat()
        Alinhamento.Alignment = StringAlignment.Far

        Gra_Saida.Graphics.DrawString(V_DValores.BASE_CALCULO_ICMS, Font10, Brushes.Black, New RectangleF(31, 163.2, 51, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_ICMS, Font10, Brushes.Black, New RectangleF(83, 163.2, 51, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.BASE_CALCULO_ICMS_SUBS, Font10, Brushes.Black, New RectangleF(135, 163.2, 51, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_ICMS_SUBS, Font10, Brushes.Black, New RectangleF(188, 163.2, 51, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_TOTAL_PRODUTOS, Font10, Brushes.Black, New RectangleF(242, 163.2, 46, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_FRETE, Font10, Brushes.Black, New RectangleF(31, 170.2, 43, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_SEGURO, Font10, Brushes.Black, New RectangleF(76, 170.2, 40, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.DESCONTO, Font10, Brushes.Black, New RectangleF(118, 170.2, 40, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.OUTRAS_DESPESAS, Font10, Brushes.Black, New RectangleF(160, 170.2, 40, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_IPI, Font10, Brushes.Black, New RectangleF(202, 170.2, 40, Font10.Height), Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores.VALOR_TOTAL_NOTA, Font10, Brushes.Black, New RectangleF(244, 170.2, 44, Font10.Height), Alinhamento)

    End Sub

    'IMPRIME PRODUTOS NA DANFE
    Private Sub Imprimir_Produtos(ByVal Gra_Saida As System.Drawing.Printing.PrintPageEventArgs)
        Dim AlturaLinha As Single
        Dim LinhaSepara As Boolean = False
        Dim pen As New Pen(Brushes.Black, 0.1)

        'VALORES DA NOTA
        Dim Alinhamento As New StringFormat()
        Alinhamento.Alignment = StringAlignment.Far

        Dim LimitePagina As Integer

        'VERIFICA SE É PRIMEIRA PAGINA E LIMITA NUMERO DE PRODUTOS
        If ControlaImpressao = 0 Then
            LimitePagina = 3
            AlturaLinha = 115
        Else
            AlturaLinha = 52
            LimitePagina = 8
        End If

        Dim Conte As Integer = 1
        Dim ConteImp As Integer = 0
        LinhaSepara = False

        'PERCORRE TODA A MATRIZ DE PRODUTOS E IMPRIME OS MESMOS
        For Each PDanfe As ProdutoDanfe In V_PRODUTOS
            'VERIFICA SE PRODUTO NÃO FOI IMPRESSO
            If ContaProdutos < Conte Then

                ContaProdutos += 1
                ConteImp += 1

                'INSERE LINHA PARA DELIMITAR PRODUTOS
                If LinhaSepara = False Then
                    LinhaSepara = True
                Else
                    'PARA USAR LINHAS PONTILHADAS HABILITAR FUNÇÃO ABAIXO
                    'pen.DashStyle = Drawing2D.DashStyle.Dot
                    Gra_Saida.Graphics.DrawLine(pen, 31, AlturaLinha, 289, AlturaLinha)
                End If

                Gra_Saida.Graphics.DrawString(PDanfe.DCodigoProd, Font6, Brushes.Black, 31, AlturaLinha)
                Gra_Saida.Graphics.DrawString(PDanfe.DDescricao, Font6, Brushes.Black, 48, AlturaLinha)
                Gra_Saida.Graphics.DrawString(PDanfe.DNCM, Font6, Brushes.Black, 136.5, AlturaLinha)
                Gra_Saida.Graphics.DrawString(PDanfe.DCST, Font6, Brushes.Black, 146.5, AlturaLinha)
                Gra_Saida.Graphics.DrawString(PDanfe.DCFOP, Font6, Brushes.Black, 151.5, AlturaLinha)
                Gra_Saida.Graphics.DrawString(PDanfe.DUNID, Font6, Brushes.Black, 157.5, AlturaLinha)
                Gra_Saida.Graphics.DrawString(PDanfe.DQT, Font6, Brushes.Black, New RectangleF(163, AlturaLinha, 8, Font6.Height), Alinhamento)
                Gra_Saida.Graphics.DrawString(PDanfe.DVALORUNI, Font6, Brushes.Black, New RectangleF(170, AlturaLinha, 17, Font6.Height), Alinhamento)
                Gra_Saida.Graphics.DrawString(PDanfe.DVALORTOTAL, Font6, Brushes.Black, New RectangleF(186, AlturaLinha, 15, Font6.Height), Alinhamento)
                Gra_Saida.Graphics.DrawString(PDanfe.DBCALC_ICMS, Font6, Brushes.Black, New RectangleF(200, AlturaLinha, 14, Font6.Height), Alinhamento)
                Gra_Saida.Graphics.DrawString(PDanfe.DVALORICMS, Font6, Brushes.Black, New RectangleF(214, AlturaLinha, 12, Font6.Height), Alinhamento)
                Gra_Saida.Graphics.DrawString(PDanfe.DBCALC_ICMS_ST, Font6, Brushes.Black, New RectangleF(228, AlturaLinha, 16, Font6.Height), Alinhamento)
                Gra_Saida.Graphics.DrawString(PDanfe.DVALORICMS_ST, Font6, Brushes.Black, New RectangleF(244, AlturaLinha, 15, Font6.Height), Alinhamento)
                Gra_Saida.Graphics.DrawString(PDanfe.DVALORIPI, Font6, Brushes.Black, New RectangleF(259, AlturaLinha, 10, Font6.Height), Alinhamento)
                Gra_Saida.Graphics.DrawString(PDanfe.DALIQUOTAICMS, Font6, Brushes.Black, New RectangleF(269, AlturaLinha, 10, Font6.Height), Alinhamento)
                Gra_Saida.Graphics.DrawString(PDanfe.DALIQUOTAIPI, Font6, Brushes.Black, New RectangleF(279, AlturaLinha, 10, Font6.Height), Alinhamento)

                AlturaLinha += 2.5
                Gra_Saida.Graphics.DrawString(PDanfe.DLINHA1, Font6, Brushes.Black, 48, AlturaLinha)
                AlturaLinha += 2.5
                Gra_Saida.Graphics.DrawString(PDanfe.DLINHA2, Font6, Brushes.Black, 48, AlturaLinha)
                AlturaLinha += 2.5
                Gra_Saida.Graphics.DrawString(PDanfe.DLINHA3, Font6, Brushes.Black, 48, AlturaLinha)
                AlturaLinha += 2.5
                Gra_Saida.Graphics.DrawString(PDanfe.DLINHA4, Font6, Brushes.Black, 48, AlturaLinha)
                AlturaLinha += 4

                'SE POSSUI MAIS PAGINAS CHAMA A MESMA E SAI DA MATRIZ
                If ConteImp = LimitePagina Then
                    Gra_Saida.HasMorePages = True
                    Exit For
                Else
                    Gra_Saida.HasMorePages = False
                End If

            End If
            Conte = Conte + 1
        Next
        ControlaImpressao += 1
    End Sub

    'INSERIR TEXTOS NOS CAMPOS DA DANFE
    Private Sub InserirTextos(ByVal Gra_Saida As System.Drawing.Printing.PrintPageEventArgs)
        Gra_Saida.Graphics.PageUnit = GraphicsUnit.Millimeter

        'TERCEIRA LINHA
        Gra_Saida.Graphics.DrawString("NOME/RAZÃO SOCIAL", Font6, Brushes.Black, 31, 47.2)
        Gra_Saida.Graphics.DrawString("CNPJ/CPF", Font6, Brushes.Black, 187, 47.2)
        Gra_Saida.Graphics.DrawString("DATA DA EMISSÃO", Font6, Brushes.Black, 247, 47.2)

        'QUARTA LINHA
        Gra_Saida.Graphics.DrawString("ENDEREÇO", Font6, Brushes.Black, 31, 54.2)
        Gra_Saida.Graphics.DrawString("BAIRRO/DESTRITO", Font6, Brushes.Black, 156, 54.2)
        Gra_Saida.Graphics.DrawString("CEP", Font6, Brushes.Black, 210, 54.2)
        Gra_Saida.Graphics.DrawString("DATA DA ENTRADA/SAÍDA", Font6, Brushes.Black, 247, 54.2)

        'QUINTA LINHA
        Gra_Saida.Graphics.DrawString("MUNICIPIO", Font6, Brushes.Black, 31, 61.2)
        Gra_Saida.Graphics.DrawString("FONE/FAX", Font6, Brushes.Black, 126, 61.2)
        Gra_Saida.Graphics.DrawString("UF", Font6, Brushes.Black, 175, 61.2)
        Gra_Saida.Graphics.DrawString("INSCRIÇÃO ESTADUAL", Font6, Brushes.Black, 187, 61.2)
        Gra_Saida.Graphics.DrawString("HORA DA ENTRADA/SAÍDA", Font6, Brushes.Black, 247, 61.2)

        'SEXTA LINHA
        Gra_Saida.Graphics.DrawString("BASE DE CÁLCULO DO ICMS", Font6, Brushes.Black, 31, 75.2)
        Gra_Saida.Graphics.DrawString("VALOR DO ICMS", Font6, Brushes.Black, 83, 75.2)
        Gra_Saida.Graphics.DrawString("BASE DE CÁLCULO DO ICMS ST", Font6, Brushes.Black, 135, 75.2)
        Gra_Saida.Graphics.DrawString("VALOR DO ICMS SUBSTITUIÇÃO", Font6, Brushes.Black, 188, 75.2)
        Gra_Saida.Graphics.DrawString("VALOR TOTAL DOS PRODUTOS", Font6, Brushes.Black, 242, 75.2)

        'SEXTA LINHA
        Gra_Saida.Graphics.DrawString("VALOR DO FRETE", Font6, Brushes.Black, 31, 82.2)
        Gra_Saida.Graphics.DrawString("VALOR SEGURO", Font6, Brushes.Black, 76, 82.2)
        Gra_Saida.Graphics.DrawString("DESCONTO", Font6, Brushes.Black, 118, 82.2)
        Gra_Saida.Graphics.DrawString("OUTRAS DESPESAS", Font6, Brushes.Black, 160, 82.2)
        Gra_Saida.Graphics.DrawString("VALOR DO IPI", Font6, Brushes.Black, 202, 82.2)
        Gra_Saida.Graphics.DrawString("VALOR TOTAL DA NOTA", Font6, Brushes.Black, 244, 82.2)

        'SETIMA LINHA
        Gra_Saida.Graphics.DrawString("NOME/RAZÃO SOCIAL", Font6, Brushes.Black, 31, 89.2)
        Gra_Saida.Graphics.DrawString("FRETE POR CONTA", Font6, Brushes.Black, 144, 89.2)
        Gra_Saida.Graphics.DrawString("CÓDIGO ANTT", Font6, Brushes.Black, 168, 89.2)
        Gra_Saida.Graphics.DrawString("PLACA DO VEÍCULO", Font6, Brushes.Black, 196, 89.2)
        Gra_Saida.Graphics.DrawString("UF", Font6, Brushes.Black, 232, 89.2)
        Gra_Saida.Graphics.DrawString("CNPJ/CPF", Font6, Brushes.Black, 244, 89.2)

        'SUBLINHAS DO FRETE
        Gra_Saida.Graphics.DrawString("0-EMITENTE", Font5, Brushes.Black, 145, 91.6)
        Gra_Saida.Graphics.DrawString("1-DESTINATARIO", Font5, Brushes.Black, 145, 93.2)

        'OITAVA LINHA
        Gra_Saida.Graphics.DrawString("ENDEREÇO", Font6, Brushes.Black, 31, 96.2)
        Gra_Saida.Graphics.DrawString("MUNICÍPIO", Font6, Brushes.Black, 144, 96.2)
        Gra_Saida.Graphics.DrawString("UF", Font6, Brushes.Black, 232, 96.2)
        Gra_Saida.Graphics.DrawString("INSCRIÇÃO ESTADUAL", Font6, Brushes.Black, 244, 96.2)

        'NONA LINHA
        Gra_Saida.Graphics.DrawString("QUANTIDADE", Font6, Brushes.Black, 31, 103.2)
        Gra_Saida.Graphics.DrawString("ESPÉCIE", Font6, Brushes.Black, 65, 103.2)
        Gra_Saida.Graphics.DrawString("MARCA", Font6, Brushes.Black, 101, 103.2)
        Gra_Saida.Graphics.DrawString("NÚMERO", Font6, Brushes.Black, 144, 103.2)
        Gra_Saida.Graphics.DrawString("PESO BRUTO", Font6, Brushes.Black, 197, 103.2)
        Gra_Saida.Graphics.DrawString("PESO LÍQUIDO", Font6, Brushes.Black, 245, 103.2)

        'QUADRO DE PRODUTOS
        Gra_Saida.Graphics.DrawString("CÓD PROD", Font5, Brushes.Black, 34, 111.2)
        Gra_Saida.Graphics.DrawString("DESCRIÇÃO DOS PRODUTOS/SERVIÇOS", Font5, Brushes.Black, 78, 111.2)
        Gra_Saida.Graphics.DrawString("NCM/SH", Font5, Brushes.Black, 137, 111.2)
        Gra_Saida.Graphics.DrawString("CST", Font5, Brushes.Black, 146.5, 111.2)
        Gra_Saida.Graphics.DrawString("CFOP", Font5, Brushes.Black, 151.5, 111.2)
        Gra_Saida.Graphics.DrawString("UNID", Font5, Brushes.Black, 157.5, 111.2)
        Gra_Saida.Graphics.DrawString("QUANT.", Font5, Brushes.Black, 163.5, 111.2)
        Gra_Saida.Graphics.DrawString("VALOR UNITÁRIO", Font5, Brushes.Black, 171.5, 111.2)
        Gra_Saida.Graphics.DrawString("VALOR TOTAL", Font5, Brushes.Black, 187.5, 111.2)
        Gra_Saida.Graphics.DrawString("B. CALC. ICMS", Font5, Brushes.Black, 201.1, 111.2)
        Gra_Saida.Graphics.DrawString("VALOR ICMS", Font5, Brushes.Black, 214.5, 111.2)
        Gra_Saida.Graphics.DrawString("B. CALC. ICMS ST", Font5, Brushes.Black, 227.3, 111.2)
        Gra_Saida.Graphics.DrawString("VALOR ICMS ST", Font5, Brushes.Black, 244.5, 111.2)
        Gra_Saida.Graphics.DrawString("VALOR IPI", Font5, Brushes.Black, 259.5, 111.2)
        Gra_Saida.Graphics.DrawString("ALÍQUOTA", Font5, Brushes.Black, 269.2, 111.2)
        Gra_Saida.Graphics.DrawString("ICMS", Font5, Brushes.Black, 271.5, 112.9)
        Gra_Saida.Graphics.DrawString("ALÍQUOTA", Font5, Brushes.Black, 279.5, 111.2)
        Gra_Saida.Graphics.DrawString("IPI", Font5, Brushes.Black, 283, 112.9)

        'DECIMA LINHA
        Gra_Saida.Graphics.DrawString("INSCRIÇÃO MUNICIPAL", Font6, Brushes.Black, 31, 163.2)
        Gra_Saida.Graphics.DrawString("VALOR TOTAL DOS SERVIÇOS", Font6, Brushes.Black, 97, 163.2)
        Gra_Saida.Graphics.DrawString("BASE DE CALCULO ISSQN", Font6, Brushes.Black, 160, 163.2)
        Gra_Saida.Graphics.DrawString("VALOR DO ISSQN", Font6, Brushes.Black, 226, 163.2)

        'DECIMA PRIMEIRA LINHA
        Gra_Saida.Graphics.DrawString("INFORMAÇÕES COMPLEMENTARES", Font6, Brushes.Black, 31, 170.2)
        Gra_Saida.Graphics.DrawString("RESERVADO AO FISCO", Font6, Brushes.Black, 224, 170.2)

        'INSERIR BLOCOS DE CAMPOS
        Dim Formato_Vertical As System.Drawing.StringFormat
        Dim Posição_Texto As System.Drawing.Point
        Dim Rotacionador_de_Texto As System.Drawing.Drawing2D.Matrix = Gra_Saida.Graphics.Transform()

        Formato_Vertical = New StringFormat(StringFormatFlags.DirectionVertical)
        Posição_Texto = New Point(27.5, 65)

        Rotacionador_de_Texto.RotateAt(180, Posição_Texto)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("DESTINATÁRIO/", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)
        Posição_Texto = New Point(25.5, 66)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("REMETENTE", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)

        Posição_Texto = New Point(27.5, 55)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("FATURA", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)

        Posição_Texto = New Point(27.5, 43.5)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("CÁLCULO", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)
        Posição_Texto = New Point(25.5, 43.5)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("IMPOSTO", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)

        Posição_Texto = New Point(27.5, 18.5)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("TRANSPORTADOR/VOLUMES", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)
        Posição_Texto = New Point(25.5, 22)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("TRANSPORTADOS", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)

        Posição_Texto = New Point(27.5, -23)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("DADOS DOS PRODUTOS/SERVIÇOS", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)

        Posição_Texto = New Point(27.5, -41)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("CÁLCULO", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)
        Posição_Texto = New Point(25.5, -40)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("ISSQN", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)

        Posição_Texto = New Point(27.5, -66)
        Gra_Saida.Graphics.Transform = Rotacionador_de_Texto
        Gra_Saida.Graphics.DrawString("DADOS ADICIONAIS", Font5_B, Brushes.Black, Posição_Texto, Formato_Vertical)

        'VOLTA TEXTO PARA POSIÇÃO ORIGINAL
        Gra_Saida.Graphics.ResetTransform()

    End Sub

    'DESENHAR LINHAS DA DANFE
    Public Sub DesenharRetangulos(ByVal Gra_Saida As System.Drawing.Printing.PrintPageEventArgs)

        'CONVERT A MEDIDA EM MILIMETROS
        Dim pen As New Pen(Brushes.Black, 0.1)
        Gra_Saida.Graphics.PageUnit = GraphicsUnit.Millimeter

        Gra_Saida.Graphics.DrawRectangle(pen, 161, 92, 4, 4)

        'CONJUNTO DE LINHAS HORIZONTAIS DADOS CLIENTE E TRANSPORTADORA
        Gra_Saida.Graphics.DrawLine(pen, 31, 54, 289, 54)
        Gra_Saida.Graphics.DrawLine(pen, 31, 61, 289, 61)
        Gra_Saida.Graphics.DrawLine(pen, 31, 68, 289, 68)
        Gra_Saida.Graphics.DrawLine(pen, 31, 75, 289, 75)
        Gra_Saida.Graphics.DrawLine(pen, 31, 82, 289, 82)
        Gra_Saida.Graphics.DrawLine(pen, 31, 89, 289, 89)
        Gra_Saida.Graphics.DrawLine(pen, 31, 96, 289, 96)
        Gra_Saida.Graphics.DrawLine(pen, 31, 103, 289, 103)
        Gra_Saida.Graphics.DrawLine(pen, 31, 110, 289, 110)

        'CONJUNTO DE LINHAS HORIZONTAIS DADOS PRODUTOS
        Gra_Saida.Graphics.DrawLine(pen, 31, 163, 289, 163)
        Gra_Saida.Graphics.DrawLine(pen, 31, 170, 289, 170)
        'Gra_Saida.Graphics.DrawLine(pen, 31, 177, 289, 177)

        'NOME / CNPJ-CPF / DATA DE EMISSÃO
        Gra_Saida.Graphics.DrawLine(pen, 186, 47, 186, 54)
        Gra_Saida.Graphics.DrawLine(pen, 246, 47, 246, 54)

        'ENDEREÇO / BAIRRO / CEP / DATA ENTRADA - SAÍDA
        Gra_Saida.Graphics.DrawLine(pen, 155, 54, 155, 61)
        Gra_Saida.Graphics.DrawLine(pen, 209, 54, 209, 61)
        Gra_Saida.Graphics.DrawLine(pen, 246, 54, 246, 61)

        'MUNICIPIO / FONE / UF / INSCRIÇÃO ESTADUAL / HORA ENTRADA E SAÍDA
        Gra_Saida.Graphics.DrawLine(pen, 125, 61, 125, 68)
        Gra_Saida.Graphics.DrawLine(pen, 174, 61, 174, 68)
        Gra_Saida.Graphics.DrawLine(pen, 186, 61, 186, 68)
        Gra_Saida.Graphics.DrawLine(pen, 246, 61, 246, 68)

        'BASE DE CALCULO DO ICMS / VALOR ICMS / BASE SUBST. ICMS ST / VALOR SUBST ICMS / VALOR TOTAL DOS PRODUTOS 
        Gra_Saida.Graphics.DrawLine(pen, 82, 75, 82, 82)
        Gra_Saida.Graphics.DrawLine(pen, 134, 75, 134, 82)
        Gra_Saida.Graphics.DrawLine(pen, 187, 75, 187, 82)
        Gra_Saida.Graphics.DrawLine(pen, 241, 75, 241, 82)

        'VALOR FRETE / VALOR SEGURO / DESCONTO / OUTRAS DESPESAS ACESSORAIS / VALOR TOTAL DO IPI / VALOR TOTAL DA NOTA 
        Gra_Saida.Graphics.DrawLine(pen, 75, 82, 75, 89)
        Gra_Saida.Graphics.DrawLine(pen, 117, 82, 117, 89)
        Gra_Saida.Graphics.DrawLine(pen, 159, 82, 159, 89)
        Gra_Saida.Graphics.DrawLine(pen, 201, 82, 201, 89)
        Gra_Saida.Graphics.DrawLine(pen, 243, 82, 243, 89)

        'NOME RAZÃO SOCIAL / FRETE POR CONTA / CÓDIGO ANTT / PLACA VEÍCULO / UF / CNPJ - CPF
        Gra_Saida.Graphics.DrawLine(pen, 143, 89, 143, 96)
        Gra_Saida.Graphics.DrawLine(pen, 167, 89, 167, 96)
        Gra_Saida.Graphics.DrawLine(pen, 195, 89, 195, 96)
        Gra_Saida.Graphics.DrawLine(pen, 231, 89, 231, 96)
        Gra_Saida.Graphics.DrawLine(pen, 243, 89, 243, 96)

        'ENDEREÇO / MUNICIPIO / UF / INCRIÇÃO ESTADUAL
        Gra_Saida.Graphics.DrawLine(pen, 143, 96, 143, 103)
        Gra_Saida.Graphics.DrawLine(pen, 231, 96, 231, 103)
        Gra_Saida.Graphics.DrawLine(pen, 243, 96, 243, 103)

        'QUANTIDADE / ESPECIE / MARCA / NUMERO / PESO BRUTO / PESO LIQUIDO
        Gra_Saida.Graphics.DrawLine(pen, 64, 103, 64, 110)
        Gra_Saida.Graphics.DrawLine(pen, 100, 103, 100, 110)
        Gra_Saida.Graphics.DrawLine(pen, 143, 103, 143, 110)
        Gra_Saida.Graphics.DrawLine(pen, 196, 103, 196, 110)
        Gra_Saida.Graphics.DrawLine(pen, 244, 103, 244, 110)

        'LINHA PRODUTOS 5 CENTIMETROS
        Gra_Saida.Graphics.DrawLine(pen, 31, 115, 289, 115)

        'CODIGO PRODUTO
        Gra_Saida.Graphics.DrawLine(pen, 48, 110, 48, 163)
        'DESCRIÇÃO DOS PRODUTOS
        Gra_Saida.Graphics.DrawLine(pen, 136, 110, 136, 163)
        'NCM/SH
        Gra_Saida.Graphics.DrawLine(pen, 146, 110, 146, 163)
        'CST
        Gra_Saida.Graphics.DrawLine(pen, 151, 110, 151, 163)
        'CFOP
        Gra_Saida.Graphics.DrawLine(pen, 157, 110, 157, 163)
        'UNIDADE
        Gra_Saida.Graphics.DrawLine(pen, 163, 110, 163, 163)
        'QUANTIDADE
        Gra_Saida.Graphics.DrawLine(pen, 171, 110, 171, 163)
        'VALOR UNITARIO
        Gra_Saida.Graphics.DrawLine(pen, 187, 110, 187, 163)
        'VALOR TOTAL
        Gra_Saida.Graphics.DrawLine(pen, 201, 110, 201, 163)
        'B CALC ICMS
        Gra_Saida.Graphics.DrawLine(pen, 214, 110, 214, 163)
        'VALOR ICMS
        Gra_Saida.Graphics.DrawLine(pen, 226, 110, 226, 163)
        'BASE DE CALCULO ICMS
        Gra_Saida.Graphics.DrawLine(pen, 244, 110, 244, 163)
        'VALOR ICMS ST
        Gra_Saida.Graphics.DrawLine(pen, 259, 110, 259, 163)
        'VALOR IPI
        Gra_Saida.Graphics.DrawLine(pen, 269, 110, 269, 163)
        'BASE DE CALCULO 
        Gra_Saida.Graphics.DrawLine(pen, 279, 110, 279, 163)

        'CALCULO ISSQN
        Gra_Saida.Graphics.DrawLine(pen, 96, 163, 96, 170)
        Gra_Saida.Graphics.DrawLine(pen, 159, 163, 159, 170)
        Gra_Saida.Graphics.DrawLine(pen, 225, 163, 225, 170)

        'FISCO
        Gra_Saida.Graphics.DrawLine(pen, 223, 170, 223, 201)

        'DESENHAR RETANGULOS COM PREECHIMENTO BLOCOS DE CAMPOS
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 25, 49, 5, 17)
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 25, 68, 5, 7)
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 25, 77, 5, 10)
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 25, 89, 5, 23)
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 25, 114, 5, 46)
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 25, 162, 5, 10)
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 25, 174, 5, 27)
    End Sub

    'FUNÇÃO PARA CAMPOS ARREDONDADOS
    Private Sub DrawRoundedRectangle(ByVal g As Drawing.Graphics, ByVal r As Rectangle, ByVal d As Integer, ByVal p As Pen)

        'EXEMPLO DE USO
        'Dim gr As Graphics = CreateGraphics()
        'Dim rectangle As New Rectangle(120, 20, 100, 20)
        'gr.SmoothingMode = SmoothingMode.AntiAlias
        'Dim pen As New Pen(Brushes.Black, 1)
        'DrawRoundedRectangle(gr, rectangle, 5, Pens.Black)

        g.DrawArc(p, r.X, r.Y, d, d, 180, 90)
        g.DrawLine(p, CInt(r.X + d / 2), r.Y, CInt(r.X + r.Width - d / 2), r.Y)
        g.DrawArc(p, r.X + r.Width - d, r.Y, d, d, 270, 90)
        g.DrawLine(p, r.X, CInt(r.Y + d / 2), r.X, CInt(r.Y + r.Height - d / 2))
        g.DrawLine(p, CInt(r.X + r.Width), CInt(r.Y + d / 2), CInt(r.X + r.Width), CInt(r.Y + r.Height - d / 2))
        g.DrawLine(p, CInt(r.X + d / 2), CInt(r.Y + r.Height), CInt(r.X + r.Width - d / 2), CInt(r.Y + r.Height))
        g.DrawArc(p, r.X, r.Y + r.Height - d, d, d, 90, 90)
        g.DrawArc(p, r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90)
    End Sub

End Class

'CLASSE DE PRODUTOS DA DANFE
Public Class ProdutoDanfe
    Private S_CODPROD As String
    Private S_DESCRICAO As String
    Private S_NCM As String
    Private S_CST As String
    Private S_CFOP As String
    Private S_UNID As String
    Private S_QUANT As String
    Private S_VALORUN As String
    Private S_VALORTOTAL As String
    Private S_BASE_ICMS As String
    Private S_VALOR_ICMS As String
    Private S_BASE_ICMS_ST As String
    Private S_VALOR_ICMS_ST As String
    Private S_VALOR_IPI As String
    Private S_ALIQUOTA_IPI As String
    Private S_ALIQUOTA_ICMS As String
    Private S_LINHA1 As String
    Private S_LINHA2 As String
    Private S_LINHA3 As String
    Private S_LINHA4 As String

    Public Property DCodigoProd() As String
        Get
            Return S_CODPROD
        End Get
        Set(ByVal value As String)
            S_CODPROD = value
        End Set
    End Property
    Public Property DDescricao() As String
        Get
            Return S_DESCRICAO
        End Get
        Set(ByVal value As String)
            S_DESCRICAO = value
        End Set
    End Property
    Public Property DNCM() As String
        Get
            Return S_NCM
        End Get
        Set(ByVal value As String)
            S_NCM = value
        End Set
    End Property
    Public Property DCST() As String
        Get
            Return S_CST
        End Get
        Set(ByVal value As String)
            S_CST = value
        End Set
    End Property
    Public Property DCFOP() As String
        Get
            Return S_CFOP
        End Get
        Set(ByVal value As String)
            S_CFOP = value
        End Set
    End Property
    Public Property DUNID() As String
        Get
            Return S_UNID
        End Get
        Set(ByVal value As String)
            S_UNID = value
        End Set
    End Property
    Public Property DQT() As String
        Get
            Return S_QUANT
        End Get
        Set(ByVal value As String)
            S_QUANT = value
        End Set
    End Property
    Public Property DVALORUNI() As String
        Get
            Return S_VALORUN
        End Get
        Set(ByVal value As String)
            S_VALORUN = value
        End Set
    End Property
    Public Property DVALORTOTAL() As String
        Get
            Return S_VALORTOTAL
        End Get
        Set(ByVal value As String)
            S_VALORTOTAL = value
        End Set
    End Property
    Public Property DBCALC_ICMS() As String
        Get
            Return S_BASE_ICMS
        End Get
        Set(ByVal value As String)
            S_BASE_ICMS = value
        End Set
    End Property
    Public Property DVALORICMS() As String
        Get
            Return S_VALOR_ICMS
        End Get
        Set(ByVal value As String)
            S_VALOR_ICMS = value
        End Set
    End Property
    Public Property DBCALC_ICMS_ST() As String
        Get
            Return S_BASE_ICMS_ST
        End Get
        Set(ByVal value As String)
            S_BASE_ICMS_ST = value
        End Set
    End Property
    Public Property DVALORICMS_ST() As String
        Get
            Return S_VALOR_ICMS_ST
        End Get
        Set(ByVal value As String)
            S_VALOR_ICMS_ST = value
        End Set
    End Property
    Public Property DVALORIPI() As String
        Get
            Return S_VALOR_IPI
        End Get
        Set(ByVal value As String)
            S_VALOR_IPI = value
        End Set
    End Property
    Public Property DALIQUOTAICMS() As String
        Get
            Return S_ALIQUOTA_ICMS
        End Get
        Set(ByVal value As String)
            S_ALIQUOTA_ICMS = value
        End Set
    End Property
    Public Property DALIQUOTAIPI() As String
        Get
            Return S_ALIQUOTA_IPI
        End Get
        Set(ByVal value As String)
            S_ALIQUOTA_IPI = value
        End Set
    End Property
    Public Property DLINHA1() As String
        Get
            Return S_LINHA1
        End Get
        Set(ByVal value As String)
            S_LINHA1 = value
        End Set
    End Property
    Public Property DLINHA2() As String
        Get
            Return S_LINHA2
        End Get
        Set(ByVal value As String)
            S_LINHA2 = value
        End Set
    End Property
    Public Property DLINHA3() As String
        Get
            Return S_LINHA3
        End Get
        Set(ByVal value As String)
            S_LINHA3 = value
        End Set
    End Property
    Public Property DLINHA4() As String
        Get
            Return S_LINHA4
        End Get
        Set(ByVal value As String)
            S_LINHA4 = value
        End Set
    End Property
End Class
