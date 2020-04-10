''CLASSE PARA IMPRESSÃO DO DANFE SEM O USO DE GERADORES
'DE RELATÓRIOS E BIBLIOTECAS DE CÓDIGO DE BARRAS DE TERCEIROS

' NOME: DANIEL COUTINHO DE MELO
'EMAIL: DANIELCPAETE@GMAIL.COM
' DATA: 24-02-2010 PORTO ALEGRE - RS
Imports System.Drawing.Printing
Imports System.Collections.Generic

Public Class Nfe_ImprimirDanfeRetrato
    Private ControlaImpressao As Integer = 0
    Private ContaProdutos As Integer = 0
    Private TotalFolha As Integer = 1
    Private ContaProdutos1 As Integer = 0

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

    Private FontArial7 As New Font("Arial", 7, FontStyle.Bold)
    Private FontArial6 As New Font("Arial", 6, FontStyle.Regular)
    Private FontArial8 As New Font("Arial", 8, FontStyle.Bold)

    'DADOS DO EMITENTE
    Public Structure DEmitente_Retrato
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
    Public Structure DFaturamento_Retrato
        Dim Faturamento As String
    End Structure
    'DADOS DO DESTINATARIO
    Public Structure DDestinatario_Retrato
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
    Public Structure DDataeHora_Retrato
        Dim DATA_EMISSAO As String
        Dim DATA_ENTRADA_SAIDA As String
        Dim HORA_ENTRADA_SAIDA As String
    End Structure
    ' Nr da Nota Fiscal Chave de Acesso e Protocolo
    Public Structure DDadosNfe_Retrato
        Dim IMP_DV As String
        Dim NUMERO_COPIAS As String
        Dim NUMERO_NFE As String
        Dim CHAVEACESSO_NFE As String
        Dim PROTOCOLO_NFE As String
        Dim DHRECBTO_NFE As String
        Dim TIPONOTA_NFE As String
        Dim NATUREZA_NFE As String
    End Structure

    'DADOS DOS VALORES TOTAIS 
    Public Structure DValores_Retrato
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
    Public Enum TipoTransportadora_Retrato
        EMITENTE = 0
        DESTINATARIO = 1
    End Enum
    Public Enum TipoNota_Retrato
        ENTRADA = 0
        SAIDA = 1
    End Enum
    'DADOS DA TRANSPORTADORA E VOLUME
    Public Structure DTransportadora_Retrato
        Dim NOME As String
        Dim CEP As String
        Dim ENDERECO As String
        Dim MUNICIPIO As String
        Dim PLACA_VEICULO As String
        Dim UF_PLACA As String
        Dim TIPO_PAGAMENTO As TipoTransportadora_Retrato
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
    Public Structure DISSQN_Retrato
        Dim IM As String
        Dim VALOR_TOTAL_SERVICOS As String
        Dim BASE_CALCULO_ISSQN As String
        Dim VALOR_ISSQN As String
    End Structure

    'DADOS DO ISSQN
    Public Structure DINFOCOMPLEMENTAR_Retrato
        Dim DADOSADIC As String
    End Structure

    'VARIAVEIS QUE VÃO ARMAZENAR OS DADOS DA NOTA
    Private V_DEmitente_Retrato As DEmitente_Retrato
    Private V_DDestinatario_Retrato As DDestinatario_Retrato
    Private V_DDataeHora_Retrato As DDataeHora_Retrato
    Private V_DValores_Retrato As DValores_Retrato
    Private V_DFaturamento_Retrato As DFaturamento_Retrato
    Private V_DTransportadora_Retrato As DTransportadora_Retrato
    Private V_DISSQN_Retrato As DISSQN_Retrato
    Private I_DDadosNfe_Retrato As DDadosNfe_Retrato
    Private S_INFOCOMP_Retrato As DINFOCOMPLEMENTAR_Retrato

    'MATRIZ DE CLASSES DE PRODUTOS DA DANFE
    'CADA PRODUTO DA DANFE É UMA CLASSE
    '----------------------------------------------------------
    Private V_PRODUTOS_Retrato As IList(Of ProdutoDanfe_Retrato)
    Public Property AddProdutosDanfe() As IList(Of ProdutoDanfe_Retrato)
        Get
            Return V_PRODUTOS_Retrato
        End Get
        Set(ByVal value As IList(Of ProdutoDanfe_Retrato))
            V_PRODUTOS_Retrato = value
        End Set
    End Property
    Public WriteOnly Property Dados_Nfe() As DDadosNfe_Retrato
        Set(ByVal Valor As DDadosNfe_Retrato)
            I_DDadosNfe_Retrato = Valor
        End Set
    End Property
    Public WriteOnly Property Identificacao_Emitente() As DEmitente_Retrato
        Set(ByVal Valor As DEmitente_Retrato)
            V_DEmitente_Retrato = Valor
        End Set
    End Property
    Public WriteOnly Property Identificacao_Destinatario() As DDestinatario_Retrato
        Set(ByVal Valor As DDestinatario_Retrato)
            V_DDestinatario_Retrato = Valor
        End Set
    End Property
    Public WriteOnly Property Data_Hora() As DDataeHora_Retrato
        Set(ByVal Valor As DDataeHora_Retrato)
            V_DDataeHora_Retrato = Valor
        End Set
    End Property
    Public WriteOnly Property Valores_Nota() As DValores_Retrato
        Set(ByVal Valor As DValores_Retrato)
            V_DValores_Retrato = Valor
        End Set
    End Property
    Public WriteOnly Property Faturamento() As DFaturamento_Retrato
        Set(ByVal Valor As DFaturamento_Retrato)
            V_DFaturamento_Retrato = Valor
        End Set
    End Property
    Public WriteOnly Property Identificacao_Transportadora() As DTransportadora_Retrato
        Set(ByVal Valor As DTransportadora_Retrato)
            V_DTransportadora_Retrato = Valor
        End Set
    End Property
    Public WriteOnly Property Valores_ISSQN() As DISSQN_Retrato
        Set(ByVal Valor As DISSQN_Retrato)
            V_DISSQN_Retrato = Valor
        End Set
    End Property
    Public WriteOnly Property InformacoesComplementares() As DINFOCOMPLEMENTAR_Retrato
        Set(ByVal Valor As DINFOCOMPLEMENTAR_Retrato)
            S_INFOCOMP_Retrato = Valor
        End Set
    End Property

    'SEMPRE QUE UMA CHAMADA A DANFE É FEITA A CHAVE DE ACESSO DEVE SER INFORMADA
    Public Sub New()
        'CRIA UMA NOVA LISTA GENERICA DE CLASSES DE PRODUTOS
        V_PRODUTOS_Retrato = New List(Of ProdutoDanfe_Retrato)()
    End Sub
    Public Sub VisualizarImpressao()
        Dim Prv_Visualizador As New PrintPreviewDialog
        Dim Doc_VisualizarDanfe As New PrintDocument
        'Dim Prn_Dialogo As New PrintDialog

        'ASSOCIA OS EVENTOS DE IMPRESSÃO COM MINHAS FUNÇÕES
        AddHandler Doc_VisualizarDanfe.BeginPrint, AddressOf InicioImpressao
        AddHandler Doc_VisualizarDanfe.PrintPage, AddressOf ImprimirDanfe

        Doc_VisualizarDanfe.DefaultPageSettings.Landscape = False
        Doc_VisualizarDanfe.DefaultPageSettings.PrinterSettings.Copies = I_DDadosNfe_Retrato.NUMERO_COPIAS

        'Doc_VisualizarDanfe.OriginAtMargins = False

        'AS MARGENS PRECISAM SER AJUSTADAS PARA QUE A IMPRESSÃO INICIAL TENHA 5 MILIMETROS
        'Doc_VisualizarDanfe.DefaultPageSettings.Margins = New Printing.Margins(11, 11, 11, 11)

        If I_DDadosNfe_Retrato.IMP_DV = "SIM" Then
            Prv_Visualizador.Document = Doc_VisualizarDanfe

            Prv_Visualizador.FormBorderStyle = 1
            Prv_Visualizador.PrintPreviewControl.Zoom = 1.0
            Prv_Visualizador.ClientSize = New System.Drawing.Size(950, 750)
            Prv_Visualizador.AutoSizeMode = 1
            Prv_Visualizador.ShowDialog()
            Prv_Visualizador.Close()
            Prv_Visualizador.Dispose()
        Else
            Doc_VisualizarDanfe.Print()
        End If
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
            Preencher_Dados_Danfe(e)
            Imprimir_Produtos(e)
        Else
            InserirCabecalho(e)
            Imprimir_Produtos(e)
        End If
    End Sub

    'GERA CABEÇALHO E GUIA DO CLIENTE
    Private Sub InserirCabecalho(ByVal Gra_Saida As System.Drawing.Printing.PrintPageEventArgs)

        Dim pen As New Pen(Brushes.Black, 0.5)
 
        'CALCULA TOTAL DE FOLHAS
        Dim Resto As Integer
        If V_PRODUTOS_Retrato.Count > 3 Then
            Resto = (V_PRODUTOS_Retrato.Count - 3) Mod 8
            If Resto > 0 Then
                TotalFolha = 2 + ((V_PRODUTOS_Retrato.Count - 3) - Resto) / 8
            Else
                TotalFolha = 1 + ((V_PRODUTOS_Retrato.Count - 3) - Resto) / 8
            End If
        End If

        Dim LimitePagina As Integer
        Dim AlturaLinha As Integer

        LimitePagina = 1000
        AlturaLinha = 525

        Dim Conte As Integer = 1
        Dim ContaProd As Integer = 0
        ContaProdutos1 = 0
        For Each PDanfe As ProdutoDanfe_Retrato In V_PRODUTOS_Retrato
            ContaProd += 1
            If ContaProdutos1 < Conte Then
                AlturaLinha += 10
                Dim ContaPreen As Integer = 0
                Dim SLinhaLote As String = ""
                For Each LinhaAdi In PDanfe.LinhaProd
                    ContaPreen += 1
                    SLinhaLote &= " " & LinhaAdi.ToString
                    If ContaPreen = 3 Then
                        ContaPreen = 0
                        SLinhaLote = ""
                        AlturaLinha += 10
                    End If
                Next
                If SLinhaLote <> "" Then
                    AlturaLinha += 10
                End If

                If AlturaLinha > LimitePagina And ContaProd < V_PRODUTOS_Retrato.Count Then
                    Conte = Conte + 1
                    AlturaLinha = 260
                    LimitePagina = 940
                End If
            End If
        Next

        'PROTOCOLO DO CLIENTE
        Gra_Saida.Graphics.DrawRectangle(pen, 10, 10, 785, 60)
        Gra_Saida.Graphics.DrawLine(pen, 10, 30, 640, 30)
        Gra_Saida.Graphics.DrawLine(pen, 140, 30, 140, 70)
        Gra_Saida.Graphics.DrawLine(pen, 640, 10, 640, 70)

        Dim Prot_Texto(3) As String
        Prot_Texto(1) = "RECEBEMOS DE " & V_DEmitente_Retrato.NOME & " OS PRODUTOS CONSTANTES NA NOTA FISCAL INDICADA AO LADO"
        Prot_Texto(2) = "DATA E HORA"
        Prot_Texto(3) = "IDENTIFICAÇÃO DO RECEBEDOR"

        Gra_Saida.Graphics.DrawString(Prot_Texto(1), FontArial6, Brushes.Black, 10, 13, New StringFormat)
        Gra_Saida.Graphics.DrawString(Prot_Texto(2), FontArial6, Brushes.Black, 10, 32, New StringFormat)
        Gra_Saida.Graphics.DrawString(Prot_Texto(3), FontArial6, Brushes.Black, 142, 32, New StringFormat)

        Gra_Saida.Graphics.DrawString("NF-e", FontArial8, Brushes.Black, 652, 13, New StringFormat)
        Gra_Saida.Graphics.DrawString("Nº   " & Int32.Parse(I_DDadosNfe_Retrato.NUMERO_NFE).ToString("000000000"), FontArial8, Brushes.Black, 652, 31, New StringFormat)
        Gra_Saida.Graphics.DrawString("SÉRIE 1 - FOLHA " & ControlaImpressao + 1 & "/" & Conte, FontArial6, Brushes.Black, 652, 50, New StringFormat)

        'Logo Emitente
        Gra_Saida.Graphics.DrawRectangle(pen, 10, 72, 315, 120)
        Gra_Saida.Graphics.DrawImage(Image.FromFile("Logo.jpg"), 15, 75, 300, 110)

        ' Gra_Saida.Graphics.DrawString(V_DEmitente_Retrato.NOME, FontArial8, Brushes.Black, 12, 130, New StringFormat)
        ' Gra_Saida.Graphics.DrawString(V_DEmitente_Retrato.ENDERECO_COMPLETO, FontArial6, Brushes.Black, 12, 145, New StringFormat)
        ' Gra_Saida.Graphics.DrawString(V_DEmitente_Retrato.MUNICIPIO & ", " & V_DEmitente_Retrato.UF & " " & V_DEmitente_Retrato.CEP, FontArial6, Brushes.Black, 12, 155)
        ' Gra_Saida.Graphics.DrawString("FONE: " & V_DEmitente_Retrato.TELEFONE, Font8_B, Brushes.Black, 12, 165)

        Gra_Saida.Graphics.DrawString("DANFE", Font12_B, Brushes.Black, 350, 75, New StringFormat)
        Gra_Saida.Graphics.DrawString("Documento Auxiliar da", FontArial6, Brushes.Black, 330, 95, New StringFormat)
        Gra_Saida.Graphics.DrawString("Nota Fiscal Eletrônica", FontArial6, Brushes.Black, 330, 105, New StringFormat)
        Gra_Saida.Graphics.DrawString("0 - ENTRADA", FontArial8, Brushes.Black, 330, 120, New StringFormat)
        Gra_Saida.Graphics.DrawString("1 - SAÍDA", FontArial8, Brushes.Black, 330, 135, New StringFormat)
        Gra_Saida.Graphics.DrawString("N.º   " & Int32.Parse(I_DDadosNfe_Retrato.NUMERO_NFE).ToString("000000000"), FontArial8, Brushes.Black, 330, 150, New StringFormat)
        Gra_Saida.Graphics.DrawString("SÉRIE 1 - FOLHA " & ControlaImpressao + 1 & "/" & Conte, FontArial7, Brushes.Black, 330, 165, New StringFormat)
        Gra_Saida.Graphics.DrawRectangle(pen, 420, 127, 20, 20)
        Gra_Saida.Graphics.DrawString(I_DDadosNfe_Retrato.TIPONOTA_NFE, Font10, Brushes.Black, 423, 129, New StringFormat)

        ' Dados Nfe
        Gra_Saida.Graphics.DrawRectangle(pen, 445, 72, 350, 185)
        Gra_Saida.Graphics.DrawLine(pen, 445, 134, 795, 134)
        Gra_Saida.Graphics.DrawLine(pen, 445, 160, 795, 160)
        Gra_Saida.Graphics.DrawLine(pen, 445, 220, 795, 220)
        Dim CodigoBarra As CodigodeBarra
        CodigoBarra = New CodigodeBarra(CodigodeBarra.BCEncoding.Code128C)
        Dim NChave As String = ""
        Dim ContarS As Integer = 0
        For x As Int16 = 0 To I_DDadosNfe_Retrato.CHAVEACESSO_NFE.Length - 1
            ContarS = ContarS + 1
            NChave = NChave & I_DDadosNfe_Retrato.CHAVEACESSO_NFE.Substring(x, 1)
            If ContarS = 4 Then
                ContarS = 0
                NChave = NChave & " "
            End If
        Next
        Gra_Saida.Graphics.DrawImage(CodigoBarra.DrawBarCode(I_DDadosNfe_Retrato.CHAVEACESSO_NFE), 460, 76, 280, 40)
        Gra_Saida.Graphics.DrawString("CHAVE DE ACESSO", FontArial6, Brushes.Black, 447, 135, New StringFormat)
        Gra_Saida.Graphics.DrawString(NChave, FontArial8, Brushes.Black, 470, 147, New StringFormat)

        Gra_Saida.Graphics.DrawString("Consulta de autenticidade no portal nacional da NF-e", Font8_B, Brushes.Black, 450, 165, New StringFormat)
        Gra_Saida.Graphics.DrawString("www.nfe.fazenda.gov.br/portal", Font8_B, Brushes.Black, 450, 175, New StringFormat)
        Gra_Saida.Graphics.DrawString("ou no site da Sefaz Autorizadora", Font8_B, Brushes.Black, 450, 185, New StringFormat)
        Gra_Saida.Graphics.DrawString("PROTOCOLO DE AUTORIZAÇÃO DE USO", FontArial6, Brushes.Black, 447, 221, New StringFormat)
        Gra_Saida.Graphics.DrawString(I_DDadosNfe_Retrato.PROTOCOLO_NFE & "  " & I_DDadosNfe_Retrato.DHRECBTO_NFE, FontArial8, Brushes.Black, 530, 233, New StringFormat)

        Gra_Saida.Graphics.DrawRectangle(pen, 10, 193, 435, 64)
        Gra_Saida.Graphics.DrawLine(pen, 10, 213, 445, 213)
        Gra_Saida.Graphics.DrawLine(pen, 10, 233, 445, 233)
        Gra_Saida.Graphics.DrawLine(pen, 120, 213, 120, 233)
        Gra_Saida.Graphics.DrawLine(pen, 260, 213, 260, 233)
        Gra_Saida.Graphics.DrawString("NATUREZA DE OPERACAO", Font5_B, Brushes.Black, 12, 193, New StringFormat)
        Gra_Saida.Graphics.DrawString(I_DDadosNfe_Retrato.NATUREZA_NFE, FontArial7, Brushes.Black, 12, 200, New StringFormat)
        Gra_Saida.Graphics.DrawString("INSCRIÇÃO ESTADUAL", Font5_B, Brushes.Black, 12, 213, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DEmitente_Retrato.IE, FontArial7, Brushes.Black, 12, 220, New StringFormat)
        Gra_Saida.Graphics.DrawString("INSCR EST DO SUBST TRIBUTARIO", Font5_B, Brushes.Black, 120, 213, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DEmitente_Retrato.IESUBS, FontArial7, Brushes.Black, 260, 220, New StringFormat)
        Gra_Saida.Graphics.DrawString("CNPJ", Font5_B, Brushes.Black, 260, 213, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DEmitente_Retrato.CNPJ, FontArial7, Brushes.Black, 260, 220, New StringFormat)
        
    End Sub

    'PREENCHE DADOS QUANDO É PRIMEIRA PAGINA
    Private Sub Preencher_Dados_Danfe(ByVal Gra_Saida As System.Drawing.Printing.PrintPageEventArgs)
        Dim pen As New Pen(Brushes.Black, 0.5)
        ' REMETENTE
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 10, 260, 785, 12)
        Gra_Saida.Graphics.DrawString("DESTINATARIO/REMETENTE", FontArial7, Brushes.Black, 12, 260, New StringFormat)
        Gra_Saida.Graphics.DrawRectangle(pen, 10, 270, 640, 60)
        Gra_Saida.Graphics.DrawRectangle(pen, 650, 270, 145, 60)

        Gra_Saida.Graphics.DrawLine(pen, 10, 290, 795, 290)
        Gra_Saida.Graphics.DrawLine(pen, 10, 310, 795, 310)
        Gra_Saida.Graphics.DrawLine(pen, 470, 270, 470, 290)
        Gra_Saida.Graphics.DrawLine(pen, 370, 290, 370, 310)
        Gra_Saida.Graphics.DrawLine(pen, 560, 290, 560, 310)

        Gra_Saida.Graphics.DrawLine(pen, 260, 310, 260, 330)
        Gra_Saida.Graphics.DrawLine(pen, 440, 310, 440, 330)
        Gra_Saida.Graphics.DrawLine(pen, 480, 310, 480, 330)

        Gra_Saida.Graphics.DrawString("NOME/RAZÃO SOCIAL", Font5_B, Brushes.Black, 12, 270, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DDestinatario_Retrato.NOME, FontArial7, Brushes.Black, 12, 277, New StringFormat)
        Gra_Saida.Graphics.DrawString("CNPJ/CPF", Font5_B, Brushes.Black, 472, 270, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DDestinatario_Retrato.CNPJ, FontArial7, Brushes.Black, 472, 277, New StringFormat)
        Gra_Saida.Graphics.DrawString("DATA DE EMISSÃO", Font5_B, Brushes.Black, 650, 270, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DDataeHora_Retrato.DATA_EMISSAO, FontArial7, Brushes.Black, 650, 277)

        Gra_Saida.Graphics.DrawString("ENDEREÇO", Font5_B, Brushes.Black, 12, 290, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DDestinatario_Retrato.ENDERECO, FontArial7, Brushes.Black, 12, 297, New StringFormat)
        Gra_Saida.Graphics.DrawString("BAIRRO/DISTRITO", Font5_B, Brushes.Black, 372, 290, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DDestinatario_Retrato.BAIRRO, FontArial7, Brushes.Black, 372, 297, New StringFormat)
        Gra_Saida.Graphics.DrawString("CEP", Font5_B, Brushes.Black, 562, 290, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DDestinatario_Retrato.CEP, FontArial7, Brushes.Black, 562, 297, New StringFormat)
        Gra_Saida.Graphics.DrawString("DATA SAIDA/ENTRADA", Font5_B, Brushes.Black, 650, 290, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DDataeHora_Retrato.DATA_ENTRADA_SAIDA, FontArial7, Brushes.Black, 650, 297)

        Gra_Saida.Graphics.DrawString("MUNICIPIO", Font5_B, Brushes.Black, 12, 310, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DDestinatario_Retrato.MUNICIPIO, FontArial7, Brushes.Black, 12, 317, New StringFormat)
        Gra_Saida.Graphics.DrawString("FONE/FAX", Font5_B, Brushes.Black, 262, 310, New StringFormat)
        'Gra_Saida.Graphics.DrawString(V_DDestinatario_Retrato.TELEFONE, FontArial7, Brushes.Black, 262, 317, New StringFormat)
        Gra_Saida.Graphics.DrawString("UF", Font5_B, Brushes.Black, 442, 310, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DDestinatario_Retrato.UF, FontArial7, Brushes.Black, 442, 317, New StringFormat)
        Gra_Saida.Graphics.DrawString("INSCRIÇÃO ESTADUAL", Font5_B, Brushes.Black, 482, 310, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DDestinatario_Retrato.IE, FontArial7, Brushes.Black, 482, 317, New StringFormat)
        Gra_Saida.Graphics.DrawString("HORA SAIDA/ENTRADA", Font5_B, Brushes.Black, 650, 310, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DDataeHora_Retrato.HORA_ENTRADA_SAIDA, FontArial7, Brushes.Black, 650, 317, New StringFormat)

        'FATURA
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 10, 332, 785, 12)
        Gra_Saida.Graphics.DrawString("FATURA", FontArial7, Brushes.Black, 12, 332, New StringFormat)
        Gra_Saida.Graphics.DrawRectangle(pen, 10, 344, 785, 40)
        Gra_Saida.Graphics.DrawString(V_DFaturamento_Retrato.Faturamento, FontArial7, Brushes.Black, 12, 350, New StringFormat)

        ' VALORES
        Dim Alinhamento As New StringFormat()
        Alinhamento.Alignment = StringAlignment.Far
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 10, 387, 785, 12)
        Gra_Saida.Graphics.DrawString("CÁLCULO DO IMPOSTO", FontArial7, Brushes.Black, 12, 386, New StringFormat)
        Gra_Saida.Graphics.DrawRectangle(pen, 10, 397, 785, 56)
        Gra_Saida.Graphics.DrawLine(pen, 10, 425, 795, 425)
        Gra_Saida.Graphics.DrawLine(pen, 157, 397, 157, 425)
        Gra_Saida.Graphics.DrawLine(pen, 314, 397, 314, 425)
        Gra_Saida.Graphics.DrawLine(pen, 471, 397, 471, 425)
        Gra_Saida.Graphics.DrawLine(pen, 628, 397, 628, 425)
        Gra_Saida.Graphics.DrawString("BASE DE CÁLCULO DE ICMS", Font5_B, Brushes.Black, 12, 397, New StringFormat)
        Gra_Saida.Graphics.DrawString("VALOR ICMS", Font5_B, Brushes.Black, 159, 397, New StringFormat)
        Gra_Saida.Graphics.DrawString("BASE CÁLCULO ICMS ST", Font5_B, Brushes.Black, 316, 397, New StringFormat)
        Gra_Saida.Graphics.DrawString("VALOR ICMS ST", Font5_B, Brushes.Black, 473, 397, New StringFormat)
        Gra_Saida.Graphics.DrawString("VALOR TOTAL DOS PRODUTOS", Font5_B, Brushes.Black, 630, 397, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DValores_Retrato.BASE_CALCULO_ICMS, FontArial8, Brushes.Black, 155, 404, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores_Retrato.VALOR_ICMS, FontArial8, Brushes.Black, 312, 404, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores_Retrato.BASE_CALCULO_ICMS_SUBS, FontArial8, Brushes.Black, 469, 404, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores_Retrato.VALOR_ICMS_SUBS, FontArial8, Brushes.Black, 626, 404, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores_Retrato.VALOR_TOTAL_PRODUTOS, FontArial8, Brushes.Black, 783, 404, Alinhamento)

        Gra_Saida.Graphics.DrawLine(pen, 114, 425, 114, 453)
        Gra_Saida.Graphics.DrawLine(pen, 228, 425, 228, 453)
        Gra_Saida.Graphics.DrawLine(pen, 342, 425, 342, 453)
        Gra_Saida.Graphics.DrawLine(pen, 471, 425, 471, 453)
        Gra_Saida.Graphics.DrawLine(pen, 628, 425, 628, 453)
        Gra_Saida.Graphics.DrawString("VALOR DO FRETE", Font5_B, Brushes.Black, 12, 425, New StringFormat)
        Gra_Saida.Graphics.DrawString("VALOR DO SEGURO", Font5_B, Brushes.Black, 116, 425, New StringFormat)
        Gra_Saida.Graphics.DrawString("DESCONTO", Font5_B, Brushes.Black, 230, 425, New StringFormat)
        Gra_Saida.Graphics.DrawString("OUTRAS DESPESAS", Font5_B, Brushes.Black, 344, 425, New StringFormat)
        Gra_Saida.Graphics.DrawString("VALOR DO IPI", Font5_B, Brushes.Black, 473, 425, New StringFormat)
        Gra_Saida.Graphics.DrawString("VALOR TOTAL DA NOTA", Font5_B, Brushes.Black, 630, 425, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DValores_Retrato.VALOR_FRETE, FontArial8, Brushes.Black, 112, 432, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores_Retrato.VALOR_SEGURO, FontArial8, Brushes.Black, 226, 432, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores_Retrato.DESCONTO, FontArial8, Brushes.Black, 340, 432, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores_Retrato.OUTRAS_DESPESAS, FontArial8, Brushes.Black, 469, 432, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores_Retrato.VALOR_IPI, FontArial8, Brushes.Black, 626, 432, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DValores_Retrato.VALOR_TOTAL_NOTA, FontArial8, Brushes.Black, 783, 432, Alinhamento)

        'TRANSPORTADORA
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 10, 454, 785, 12)
        Gra_Saida.Graphics.DrawString("TRANSPORTADOR/VOLUMES TRANSPORTADOS", FontArial7, Brushes.Black, 12, 454, New StringFormat)
        Gra_Saida.Graphics.DrawRectangle(pen, 10, 464, 785, 60)
        ' 1º linha 
        Gra_Saida.Graphics.DrawLine(pen, 10, 484, 795, 484)
        Gra_Saida.Graphics.DrawRectangle(pen, 400, 466, 14, 14) 'frete

        Gra_Saida.Graphics.DrawLine(pen, 320, 464, 320, 484)
        Gra_Saida.Graphics.DrawLine(pen, 417, 464, 417, 484)
        Gra_Saida.Graphics.DrawLine(pen, 500, 464, 500, 484)
        Gra_Saida.Graphics.DrawLine(pen, 620, 464, 620, 484)
        Gra_Saida.Graphics.DrawLine(pen, 650, 464, 650, 484)
        Gra_Saida.Graphics.DrawString("RAZÃO SOCIAL", Font5_B, Brushes.Black, 12, 464, New StringFormat)
        Gra_Saida.Graphics.DrawString("FRETE POR CONTA", Font5_B, Brushes.Black, 322, 464, New StringFormat)
        Gra_Saida.Graphics.DrawString("0-Emitente", Font5_B, Brushes.Black, 322, 470, New StringFormat)
        Gra_Saida.Graphics.DrawString("1-Destinatario", Font5_B, Brushes.Black, 322, 476, New StringFormat)
        Gra_Saida.Graphics.DrawString("CODIGO ANTT", Font5_B, Brushes.Black, 419, 464, New StringFormat)
        Gra_Saida.Graphics.DrawString("PLACA VEICULO", Font5_B, Brushes.Black, 502, 464, New StringFormat)
        Gra_Saida.Graphics.DrawString("UF", Font5_B, Brushes.Black, 622, 464, New StringFormat)
        Gra_Saida.Graphics.DrawString("CNPJ/CPF", Font5_B, Brushes.Black, 652, 464, New StringFormat)

        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.NOME, FontArial7, Brushes.Black, 12, 471, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.TIPO_PAGAMENTO, FontArial7, Brushes.Black, 403, 468, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.CODIGO_ANTT, FontArial7, Brushes.Black, 419, 471, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.PLACA_VEICULO, FontArial7, Brushes.Black, 502, 471, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.UF_PLACA, FontArial7, Brushes.Black, 622, 471, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.CNPJ, FontArial7, Brushes.Black, 652, 471, New StringFormat)

        ' 2º linha 
        Gra_Saida.Graphics.DrawLine(pen, 10, 504, 795, 504)
        Gra_Saida.Graphics.DrawLine(pen, 320, 484, 320, 504)
        Gra_Saida.Graphics.DrawLine(pen, 620, 484, 620, 504)
        Gra_Saida.Graphics.DrawLine(pen, 650, 484, 650, 504)

        Gra_Saida.Graphics.DrawString("ENDEREÇO", Font5_B, Brushes.Black, 12, 484, New StringFormat)
        Gra_Saida.Graphics.DrawString("MUNICIPIO", Font5_B, Brushes.Black, 322, 484, New StringFormat)
        Gra_Saida.Graphics.DrawString("UF", Font5_B, Brushes.Black, 622, 484, New StringFormat)
        Gra_Saida.Graphics.DrawString("INSCRIÇÃO ESTADUAL", Font5_B, Brushes.Black, 652, 484, New StringFormat)

        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.ENDERECO, FontArial7, Brushes.Black, 12, 491, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.MUNICIPIO, FontArial7, Brushes.Black, 322, 491, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.UF, FontArial7, Brushes.Black, 622, 491, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.IE, FontArial7, Brushes.Black, 652, 491, New StringFormat)

        ' 3º linha 
        Gra_Saida.Graphics.DrawLine(pen, 100, 504, 100, 524)
        Gra_Saida.Graphics.DrawLine(pen, 210, 504, 210, 524)
        Gra_Saida.Graphics.DrawLine(pen, 320, 504, 320, 524)
        Gra_Saida.Graphics.DrawLine(pen, 500, 504, 500, 524)
        Gra_Saida.Graphics.DrawLine(pen, 650, 504, 650, 524)

        Gra_Saida.Graphics.DrawString("QUANTIDADE", Font5_B, Brushes.Black, 12, 504, New StringFormat)
        Gra_Saida.Graphics.DrawString("ESPÉCIE", Font5_B, Brushes.Black, 102, 504, New StringFormat)
        Gra_Saida.Graphics.DrawString("MARCA", Font5_B, Brushes.Black, 212, 504, New StringFormat)
        Gra_Saida.Graphics.DrawString("NUMERAÇÃO", Font5_B, Brushes.Black, 322, 504, New StringFormat)
        Gra_Saida.Graphics.DrawString("PESO BRUTO", Font5_B, Brushes.Black, 502, 504, New StringFormat)
        Gra_Saida.Graphics.DrawString("PESO LÍQUIDO", Font5_B, Brushes.Black, 652, 504, New StringFormat)

        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.QUANTIDADE, FontArial7, Brushes.Black, 90, 511, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.ESPECIE, FontArial7, Brushes.Black, 102, 511, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.MARCA, FontArial7, Brushes.Black, 212, 511, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.NUMERO, FontArial7, Brushes.Black, 322, 511, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.PESOBRUTO, FontArial7, Brushes.Black, 640, 511, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DTransportadora_Retrato.PESOLIQUIDO, FontArial7, Brushes.Black, 780, 511, Alinhamento)

        'CÁLCULO DO ISSQN
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 10, 974, 785, 12)
        Gra_Saida.Graphics.DrawString("CÁLCULO DO ISSQN", FontArial7, Brushes.Black, 12, 974, New StringFormat)
        Gra_Saida.Graphics.DrawRectangle(pen, 10, 984, 785, 20)
        Gra_Saida.Graphics.DrawLine(pen, 194, 984, 194, 1004)
        Gra_Saida.Graphics.DrawLine(pen, 388, 984, 388, 1004)
        Gra_Saida.Graphics.DrawLine(pen, 582, 984, 582, 1004)
        Gra_Saida.Graphics.DrawString("INSCRIÇÃO MUNICIPAL", Font5_B, Brushes.Black, 12, 984, New StringFormat)
        Gra_Saida.Graphics.DrawString("VALOR TOTAL DOS SERVIÇOS", Font5_B, Brushes.Black, 196, 984, New StringFormat)
        Gra_Saida.Graphics.DrawString("BASE DE CALCULO DO ISSQN", Font5_B, Brushes.Black, 390, 984, New StringFormat)
        Gra_Saida.Graphics.DrawString("VALOR DO ISSQN", Font5_B, Brushes.Black, 584, 984, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DISSQN_Retrato.IM, FontArial7, Brushes.Black, 12, 991, New StringFormat)
        Gra_Saida.Graphics.DrawString(V_DISSQN_Retrato.VALOR_TOTAL_SERVICOS, FontArial7, Brushes.Black, 383, 991, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DISSQN_Retrato.BASE_CALCULO_ISSQN, FontArial7, Brushes.Black, 577, 991, Alinhamento)
        Gra_Saida.Graphics.DrawString(V_DISSQN_Retrato.VALOR_ISSQN, FontArial7, Brushes.Black, 780, 991, Alinhamento)

        'DADOS ADICIONAIS
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 10, 1005, 785, 12)
        Gra_Saida.Graphics.DrawString("DADOS ADICIONAIS", FontArial7, Brushes.Black, 12, 1005, New StringFormat)
        Gra_Saida.Graphics.DrawRectangle(pen, 10, 1015, 500, 120)
        Gra_Saida.Graphics.DrawRectangle(pen, 510, 1015, 285, 120)
        Gra_Saida.Graphics.DrawString("INFORMAÇÕES COMPLEMENTARES", Font5_B, Brushes.Black, 12, 1015, New StringFormat)
        Gra_Saida.Graphics.DrawString("RESERVADO AO FISCO", Font5_B, Brushes.Black, 512, 1015, New StringFormat)
        Gra_Saida.Graphics.DrawString(S_INFOCOMP_Retrato.DADOSADIC, Font6, Brushes.Black, New RectangleF(12, 1023, 508, 117), New StringFormat)
    End Sub

    'IMPRIME PRODUTOS NA DANFE
    Private Sub Imprimir_Produtos(ByVal Gra_Saida As System.Drawing.Printing.PrintPageEventArgs)
        Dim AlturaLinha As Single
        Dim LinhaSepara As Boolean = False
        Dim pen As New Pen(Brushes.Black, 0.5)

        'VALORES DA NOTA
        Dim AlinhamentoFar As New StringFormat()
        AlinhamentoFar.Alignment = StringAlignment.Far
        Dim AlinhamentoCenter As New StringFormat()
        AlinhamentoCenter.Alignment = StringAlignment.Center

        Dim LimitePagina As Integer = 0
        Dim RetHeight As Integer = 0
        Dim Y2Line As Integer = 0

        'VERIFICA SE É PRIMEIRA PAGINA E LIMITA NUMERO DE PRODUTOS
        If ControlaImpressao = 0 Then
            LimitePagina = 30
            AlturaLinha = 525
            RetHeight = 439
            Y2Line = 974

        Else
            LimitePagina = 50
            AlturaLinha = 260
            RetHeight = 845
            Y2Line = 1115
        End If
        Gra_Saida.Graphics.FillRectangle(Brushes.LightGray, 10, AlturaLinha, 785, 12)
        Gra_Saida.Graphics.DrawString("DADOS DOS PRODUTOS / SERVIÇOS", FontArial7, Brushes.Black, 12, AlturaLinha, New StringFormat)
        AlturaLinha += 10
        Gra_Saida.Graphics.DrawRectangle(pen, 10, AlturaLinha, 785, RetHeight)
        'Verticais
        Gra_Saida.Graphics.DrawLine(pen, 120, AlturaLinha, 120, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 270, AlturaLinha, 270, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 310, AlturaLinha, 310, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 330, AlturaLinha, 330, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 370, AlturaLinha, 370, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 390, AlturaLinha, 390, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 435, AlturaLinha, 435, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 495, AlturaLinha, 495, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 555, AlturaLinha, 555, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 595, AlturaLinha, 595, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 650, AlturaLinha, 650, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 700, AlturaLinha, 700, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 745, AlturaLinha, 745, Y2Line)
        Gra_Saida.Graphics.DrawLine(pen, 770, AlturaLinha, 770, Y2Line)
        AlturaLinha += 5
        Gra_Saida.Graphics.DrawString("CÓD PROD", Font5_B, Brushes.Black, 11, AlturaLinha, New StringFormat)
        Gra_Saida.Graphics.DrawString("DESCRIÇÃO DOS PRODUTOS/SERVIÇOS", Font5_B, Brushes.Black, 121, AlturaLinha, New StringFormat)
        Gra_Saida.Graphics.DrawString("NCM", Font5_B, Brushes.Black, 290, AlturaLinha, AlinhamentoCenter)
        Gra_Saida.Graphics.DrawString("CST", Font5_B, Brushes.Black, 321, AlturaLinha, AlinhamentoCenter)
        Gra_Saida.Graphics.DrawString("CFOP", Font5_B, Brushes.Black, 351, AlturaLinha, AlinhamentoCenter)
        Gra_Saida.Graphics.DrawString("UN", Font5_B, Brushes.Black, 381, AlturaLinha, AlinhamentoCenter)
        Gra_Saida.Graphics.DrawString("QTDE", Font5_B, Brushes.Black, 434, AlturaLinha, AlinhamentoFar)
        Gra_Saida.Graphics.DrawString("VLR UNITÁRIO", Font5_B, Brushes.Black, 494, AlturaLinha, AlinhamentoFar)
        Gra_Saida.Graphics.DrawString("VLR TOTAL", Font5_B, Brushes.Black, 554, AlturaLinha, AlinhamentoFar)
        Gra_Saida.Graphics.DrawString("DESC", Font5_B, Brushes.Black, 594, AlturaLinha, AlinhamentoFar)
        Gra_Saida.Graphics.DrawString("BC ICMS", Font5_B, Brushes.Black, 649, AlturaLinha, AlinhamentoFar)
        Gra_Saida.Graphics.DrawString("VLR ICMS", Font5_B, Brushes.Black, 699, AlturaLinha, AlinhamentoFar)
        Gra_Saida.Graphics.DrawString("VLR IPI", Font5_B, Brushes.Black, 744, AlturaLinha, AlinhamentoFar)
        Gra_Saida.Graphics.DrawString("ALIQ " & vbCrLf & "ICMS", Font5_B, Brushes.Black, 769, AlturaLinha - 2, AlinhamentoFar)
        Gra_Saida.Graphics.DrawString("ALIQ " & vbCrLf & "IPI", Font5_B, Brushes.Black, 793, AlturaLinha - 2, AlinhamentoFar)
        AlturaLinha += 14
        Gra_Saida.Graphics.DrawLine(pen, 10, AlturaLinha, 795, AlturaLinha)
        AlturaLinha += 5

        Dim Conte As Integer = 1
        Dim ConteImp As Integer = 0
        LinhaSepara = False
        Dim Alinhamento As New StringFormat()
        Alinhamento.Alignment = StringAlignment.Far

        'PERCORRE TODA A MATRIZ DE PRODUTOS E IMPRIME OS MESMOS
        For Each PDanfe As ProdutoDanfe_Retrato In V_PRODUTOS_Retrato
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
                    If ControlaImpressao = 0 Then
                        ' Gra_Saida.Graphics.DrawLine(pen, 31, AlturaLinha, 289, AlturaLinha)
                    Else
                        ' Gra_Saida.Graphics.DrawLine(pen, 23, AlturaLinha, 289, AlturaLinha)
                    End If
                End If

                Gra_Saida.Graphics.DrawString(PDanfe.DCodigoProd, Font6, Brushes.Black, 11, AlturaLinha, New StringFormat)
                Gra_Saida.Graphics.DrawString(PDanfe.DNCM, Font6, Brushes.Black, 290, AlturaLinha, AlinhamentoCenter)
                Gra_Saida.Graphics.DrawString(PDanfe.DCST, Font6, Brushes.Black, 321, AlturaLinha, AlinhamentoCenter)
                Gra_Saida.Graphics.DrawString(PDanfe.DCFOP, Font6, Brushes.Black, 351, AlturaLinha, AlinhamentoCenter)
                Gra_Saida.Graphics.DrawString(PDanfe.DUNID, Font6, Brushes.Black, 381, AlturaLinha, AlinhamentoCenter)

                Gra_Saida.Graphics.DrawString(PDanfe.DQT, Font6, Brushes.Black, 434, AlturaLinha, AlinhamentoFar)
                Gra_Saida.Graphics.DrawString(PDanfe.DVALORUNI, Font6, Brushes.Black, 494, AlturaLinha, AlinhamentoFar)
                Gra_Saida.Graphics.DrawString(PDanfe.DVALORTOTAL, Font6, Brushes.Black, 554, AlturaLinha, AlinhamentoFar)
                Gra_Saida.Graphics.DrawString(PDanfe.DVALORDESC, Font6, Brushes.Black, 594, AlturaLinha, AlinhamentoFar)
                Gra_Saida.Graphics.DrawString(PDanfe.DBCALC_ICMS, Font6, Brushes.Black, 649, AlturaLinha, AlinhamentoFar)
                Gra_Saida.Graphics.DrawString(PDanfe.DVALORICMS, Font6, Brushes.Black, 699, AlturaLinha, AlinhamentoFar)

                Gra_Saida.Graphics.DrawString(PDanfe.DVALORIPI, Font6, Brushes.Black, 744, AlturaLinha, AlinhamentoFar)
                Gra_Saida.Graphics.DrawString(PDanfe.DALIQUOTAICMS, Font6, Brushes.Black, 769, AlturaLinha, AlinhamentoFar)
                Gra_Saida.Graphics.DrawString(PDanfe.DALIQUOTAIPI, Font6, Brushes.Black, 793, AlturaLinha, AlinhamentoFar)

                If PDanfe.DDescricao.Length > 40 Then
                    Alinhamento.Alignment = StringAlignment.Near
                    Gra_Saida.Graphics.DrawString(PDanfe.DDescricao, Font6, Brushes.Black, New RectangleF(121, AlturaLinha, 150, 20), Alinhamento)
                    AlturaLinha += 10
                Else
                    Gra_Saida.Graphics.DrawString(PDanfe.DDescricao, Font6, Brushes.Black, 121, AlturaLinha, New StringFormat)
                End If

                AlturaLinha += 10
                Dim ContaPreen As Integer = 0
                Dim SLinhaLote As String = ""
                For Each LinhaAdi In PDanfe.LinhaProd
                    ContaPreen += 1
                    SLinhaLote &= " " & LinhaAdi.ToString
                    If ContaPreen = 3 Then
                        If SLinhaLote.Length > 40 Then
                            Alinhamento.Alignment = StringAlignment.Near
                            Gra_Saida.Graphics.DrawString(SLinhaLote, Font5, Brushes.Black, New RectangleF(121, AlturaLinha, 150, 20), Alinhamento)
                            AlturaLinha += 10
                        Else
                            Gra_Saida.Graphics.DrawString(SLinhaLote, Font5, Brushes.Black, 121, AlturaLinha, New StringFormat)
                        End If
                        ContaPreen = 0
                        SLinhaLote = ""
                        AlturaLinha += 10
                    End If
                Next
                If SLinhaLote <> "" Then
                    If SLinhaLote.Length > 40 Then
                        Alinhamento.Alignment = StringAlignment.Near
                        Gra_Saida.Graphics.DrawString(SLinhaLote, Font5, Brushes.Black, New RectangleF(121, AlturaLinha, 150, 20), Alinhamento)
                        AlturaLinha += 10
                    Else
                        Gra_Saida.Graphics.DrawString(SLinhaLote, Font5, Brushes.Black, 121, AlturaLinha, New StringFormat)
                    End If
                    AlturaLinha += 10
                End If

                'pen.DashStyle = Drawing2D.DashStyle.DashDotDot
                Gra_Saida.Graphics.DrawLine(pen, 10, AlturaLinha, 795, AlturaLinha)
                AlturaLinha += 4

                'SE POSSUI MAIS PAGINAS CHAMA A MESMA E SAI DA MATRIZ
                If AlturaLinha > Y2Line Then
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

End Class
'CLASSE DE PRODUTOS DA DANFE
Public Class ProdutoDanfe_Retrato
    Private S_CODPROD As String
    Private S_DESCRICAO As String
    Private S_NCM As String
    Private S_CST As String
    Private S_CFOP As String
    Private S_UNID As String
    Private S_QUANT As String
    Private S_VALORUN As String
    Private S_VALORTOTAL As String
    Private S_VALORDESC As String
    Private S_BASE_ICMS As String
    Private S_VALOR_ICMS As String
    Private S_BASE_ICMS_ST As String
    Private S_VALOR_ICMS_ST As String
    Private S_VALOR_IPI As String
    Private S_ALIQUOTA_IPI As String
    Private S_ALIQUOTA_ICMS As String
    Public Sub New()
        _LinhaProd = New List(Of String)
    End Sub
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
    Public Property DVALORDESC() As String
        Get
            Return S_VALORDESC
        End Get
        Set(ByVal value As String)
            S_VALORDESC = value
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
    Private _LinhaProd As List(Of String)
    Public Property LinhaProd() As List(Of String)
        Get
            Return _LinhaProd
        End Get
        Set(ByVal value As List(Of String))
            _LinhaProd = value
        End Set
    End Property
End Class
