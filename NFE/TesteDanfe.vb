Imports System.Drawing.Drawing2D
Imports System.Xml
Imports System.Security.Cryptography.X509Certificates
Imports System.Text

Public Class TesteDanfe
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim IDanfe As New ImprimirDanfe("35091201755555000152550010000011180000050121")

        Dim CEmitente As ImprimirDanfe.DEmitente = Nothing
        CEmitente.NOME = "TESTE DE EMPRESA"
        CEmitente.MUNICIPIO = "PORTO ALEGRE"
        CEmitente.UF = "RS"
        CEmitente.ENDERECO_COMPLETO = "RUA: TESTE 555, SANTANA"
        CEmitente.CNPJ = "0000.123.4564/66"
        CEmitente.CEP = "90620.170"
        CEmitente.IE = "1234567891"
        CEmitente.IESUBS = ""
        CEmitente.TELEFONE = "(51) 33333333"

        IDanfe.NaturezaOperacao = "VENDA DE MERCADORIA PARA TERCEIROS"
        IDanfe.ProtocoloAutorizacao = "123132345646"
        IDanfe.TipoNotaFiscal = ImprimirDanfe.TipoNota.SAIDA

        Dim CDestinatario As ImprimirDanfe.DDestinatario = Nothing
        CDestinatario.CEP = "90840440"
        CDestinatario.CNPJ = "001.234.560/23"
        CDestinatario.ENDERECO = "ORFANATROFIO 123"
        CDestinatario.BAIRRO = "CRISTAL"
        CDestinatario.IE = "ISENTO"
        CDestinatario.NOME = "DANIEL COUTINHO DE MELO"
        CDestinatario.TELEFONE = "(51)-93999992"
        CDestinatario.UF = "RS"
        CDestinatario.MUNICIPIO = "PORTO ALEGRE"

        Dim CDataHora As ImprimirDanfe.DDataeHora = Nothing
        CDataHora.DATA_EMISSAO = "01/02/2010"
        CDataHora.DATA_ENTRADA_SAIDA = ""
        CDataHora.HORA_ENTRADA_SAIDA = ""

        Dim CTransportadora As ImprimirDanfe.DTransportadora = Nothing
        CTransportadora.CEP = ""
        CTransportadora.CNPJ = "00417.145.0001/92"
        CTransportadora.CODIGO_ANTT = ""
        CTransportadora.ENDERECO = "TESTE DE ENDEREÇO 123132"
        CTransportadora.ESPECIE = "CDA"
        CTransportadora.IE = "1321123132"
        CTransportadora.MARCA = ""
        CTransportadora.MUNICIPIO = "PORTO ALEGRE"
        CTransportadora.NOME = "TESTE DE TRANSPORTADORA"
        CTransportadora.NUMERO = ""
        CTransportadora.PESOBRUTO = ""
        CTransportadora.PESOLIQUIDO = ""
        CTransportadora.PLACA_VEICULO = "HGD2345"
        CTransportadora.QUANTIDADE = "0"
        CTransportadora.TIPO_PAGAMENTO = ImprimirDanfe.TipoTransportadora.EMITENDE
        CTransportadora.UF = "RS"
        CTransportadora.UF_PLACA = "RS"

        Dim CValores As ImprimirDanfe.DValores = Nothing
        CValores.BASE_CALCULO_ICMS = "0,00"
        CValores.BASE_CALCULO_ICMS_SUBS = "0,00"
        CValores.DESCONTO = "0,00"
        CValores.OUTRAS_DESPESAS = "0,00"
        CValores.VALOR_FRETE = "0,00"
        CValores.VALOR_ICMS = "0,00"
        CValores.VALOR_ICMS_SUBS = "0,00"
        CValores.VALOR_IPI = "0,00"
        CValores.VALOR_SEGURO = "0,00"
        CValores.VALOR_TOTAL_NOTA = "0,00"
        CValores.VALOR_TOTAL_PRODUTOS = "0,00"

        Dim CISSQN As ImprimirDanfe.DISSQN = Nothing
        CISSQN.BASE_CALCULO_ISSQN = "0,00"
        CISSQN.IM = "0,00"
        CISSQN.VALOR_TOTAL_SERVICOS = "0,00"
        CISSQN.VALOR_ISSQN = "0,00"

        Dim CINFO As ImprimirDanfe.DINFOCOMPLEMENTAR = Nothing
        CINFO.LINHA1 = "linha1"
        CINFO.LINHA2 = "linha2"
        CINFO.LINHA3 = "linha3"
        CINFO.LINHA4 = "linha4"
        CINFO.LINHA5 = "linha5"

        Dim PROD As ProdutoDanfe

        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        PROD = New ProdutoDanfe
        PROD.DCodigoProd = "1"
        PROD.DDescricao = "TESTE DE DESCRICAO"
        PROD.DNCM = "96565831"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "1,000"
        PROD.DVALORUNI = "165,00"
        PROD.DVALORTOTAL = "165,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346541320 Cod. Sus: 96142315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0013324103 Qtd: 1,00"

        'ADICIONA PRODUTOS NA DANFE
        IDanfe.AddProdutosDanfe.Add(PROD)

        PROD = New ProdutoDanfe

        PROD.DCodigoProd = "2"
        PROD.DDescricao = "TESTE DE DESCRICAO LINHA 2"
        PROD.DNCM = "96565561"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "2,000"
        PROD.DVALORUNI = "100,00"
        PROD.DVALORTOTAL = "200,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346545698 Cod. Sus: 785632315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0045674103 Qtd: 2,00"
        IDanfe.AddProdutosDanfe.Add(PROD)

        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        PROD = New ProdutoDanfe
        PROD.DCodigoProd = "3"
        PROD.DDescricao = "TESTE DE DESCRICAO"
        PROD.DNCM = "96565831"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "1,000"
        PROD.DVALORUNI = "165,00"
        PROD.DVALORTOTAL = "165,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346541320 Cod. Sus: 96142315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0013324103 Qtd: 1,00"

        'ADICIONA PRODUTOS NA DANFE
        IDanfe.AddProdutosDanfe.Add(PROD)
        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        PROD = New ProdutoDanfe
        PROD.DCodigoProd = "4"
        PROD.DDescricao = "TESTE DE DESCRICAO"
        PROD.DNCM = "96565831"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "1,000"
        PROD.DVALORUNI = "165,00"
        PROD.DVALORTOTAL = "165,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346541320 Cod. Sus: 96142315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0013324103 Qtd: 1,00"

        'ADICIONA PRODUTOS NA DANFE
        IDanfe.AddProdutosDanfe.Add(PROD)
        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        PROD = New ProdutoDanfe
        PROD.DCodigoProd = "5"
        PROD.DDescricao = "TESTE DE DESCRICAO"
        PROD.DNCM = "96565831"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "1,000"
        PROD.DVALORUNI = "165,00"
        PROD.DVALORTOTAL = "165,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346541320 Cod. Sus: 96142315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0013324103 Qtd: 1,00"

        'ADICIONA PRODUTOS NA DANFE
        IDanfe.AddProdutosDanfe.Add(PROD)
        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        PROD = New ProdutoDanfe
        PROD.DCodigoProd = "6"
        PROD.DDescricao = "TESTE DE DESCRICAO"
        PROD.DNCM = "96565831"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "1,000"
        PROD.DVALORUNI = "165,00"
        PROD.DVALORTOTAL = "165,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346541320 Cod. Sus: 96142315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0013324103 Qtd: 1,00"

        'ADICIONA PRODUTOS NA DANFE
        IDanfe.AddProdutosDanfe.Add(PROD)
        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        PROD = New ProdutoDanfe
        PROD.DCodigoProd = "7"
        PROD.DDescricao = "TESTE DE DESCRICAO"
        PROD.DNCM = "96565831"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "1,000"
        PROD.DVALORUNI = "165,00"
        PROD.DVALORTOTAL = "165,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346541320 Cod. Sus: 96142315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0013324103 Qtd: 1,00"

        'ADICIONA PRODUTOS NA DANFE
        IDanfe.AddProdutosDanfe.Add(PROD)
        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        PROD = New ProdutoDanfe
        PROD.DCodigoProd = "8"
        PROD.DDescricao = "TESTE DE DESCRICAO"
        PROD.DNCM = "96565831"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "1,000"
        PROD.DVALORUNI = "165,00"
        PROD.DVALORTOTAL = "165,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346541320 Cod. Sus: 96142315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0013324103 Qtd: 1,00"

        'ADICIONA PRODUTOS NA DANFE
        IDanfe.AddProdutosDanfe.Add(PROD)
        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        PROD = New ProdutoDanfe
        PROD.DCodigoProd = "9"
        PROD.DDescricao = "TESTE DE DESCRICAO"
        PROD.DNCM = "96565831"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "1,000"
        PROD.DVALORUNI = "165,00"
        PROD.DVALORTOTAL = "165,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346541320 Cod. Sus: 96142315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0013324103 Qtd: 1,00"

        'ADICIONA PRODUTOS NA DANFE
        IDanfe.AddProdutosDanfe.Add(PROD)
        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        PROD = New ProdutoDanfe
        PROD.DCodigoProd = "10"
        PROD.DDescricao = "TESTE DE DESCRICAO"
        PROD.DNCM = "96565831"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "1,000"
        PROD.DVALORUNI = "165,00"
        PROD.DVALORTOTAL = "165,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346541320 Cod. Sus: 96142315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0013324103 Qtd: 1,00"

        'ADICIONA PRODUTOS NA DANFE
        IDanfe.AddProdutosDanfe.Add(PROD)
        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        PROD = New ProdutoDanfe
        PROD.DCodigoProd = "11"
        PROD.DDescricao = "TESTE DE DESCRICAO"
        PROD.DNCM = "96565831"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "1,000"
        PROD.DVALORUNI = "165,00"
        PROD.DVALORTOTAL = "165,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346541320 Cod. Sus: 96142315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0013324103 Qtd: 1,00"

        'ADICIONA PRODUTOS NA DANFE
        IDanfe.AddProdutosDanfe.Add(PROD)
        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        PROD = New ProdutoDanfe
        PROD.DCodigoProd = "12"
        PROD.DDescricao = "TESTE DE DESCRICAO"
        PROD.DNCM = "96565831"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "1,000"
        PROD.DVALORUNI = "165,00"
        PROD.DVALORTOTAL = "165,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346541320 Cod. Sus: 96142315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0013324103 Qtd: 1,00"

        'ADICIONA PRODUTOS NA DANFE
        IDanfe.AddProdutosDanfe.Add(PROD)
        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        PROD = New ProdutoDanfe
        PROD.DCodigoProd = "13"
        PROD.DDescricao = "TESTE DE DESCRICAO"
        PROD.DNCM = "96565831"
        PROD.DCST = "140"
        PROD.DCFOP = "6114"
        PROD.DUNID = "CDA"
        PROD.DQT = "1,000"
        PROD.DVALORUNI = "165,00"
        PROD.DVALORTOTAL = "165,00"
        PROD.DBCALC_ICMS = "0,00"
        PROD.DVALORICMS = "0,00"
        PROD.DBCALC_ICMS_ST = "0,00"
        PROD.DVALORICMS_ST = "0,00"
        PROD.DVALORIPI = "0,00"
        PROD.DALIQUOTAICMS = "0,00"
        PROD.DALIQUOTAIPI = "0,00"
        PROD.DLINHA1 = "Cod. Anvisa: 1346541320 Cod. Sus: 96142315"
        PROD.DLINHA2 = ""
        PROD.DLINHA3 = "Lote: 0013324103 Qtd: 1,00"

        'ADICIONA PRODUTOS NA DANFE
        IDanfe.AddProdutosDanfe.Add(PROD)

        IDanfe.Identificacao_Emitente = CEmitente
        IDanfe.Identificacao_Destinatario = CDestinatario
        IDanfe.Data_Hora = CDataHora
        IDanfe.Valores_Nota = CValores
        IDanfe.Identificacao_Transportadora = CTransportadora
        IDanfe.Valores_ISSQN = CISSQN
        IDanfe.InformacoesComplementares = CINFO
        IDanfe.VisualizarImpressao()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim teste As New NFe()

        teste.versao = "1.10"

        teste.infNFE.Ide.cUF = 41
        teste.infNFE.Ide.cNF = "000001430"
        teste.infNFE.Ide.natOp = "VENDAS DE MERCADORIA A PRAZO"
        teste.infNFE.Ide.indPag = "1"
        teste.infNFE.Ide.[mod] = "55"
        teste.infNFE.Ide.serie = "2"
        teste.infNFE.Ide.nNF = "1261"
        teste.infNFE.Ide.dEmi = DateTime.Now.AddDays(-1)
        teste.infNFE.Ide.dSaiEnt = DateTime.Now.AddDays(-1)
        teste.infNFE.Ide.tpNF = 1
        teste.infNFE.Ide.cMunFG = 4106902
        teste.infNFE.Ide.tpImp = 1
        teste.infNFE.Ide.tpEmis = "1"
        teste.infNFE.Ide.tpAmb = 2
        teste.infNFE.Ide.finNFe = "1"
        teste.infNFE.Ide.procEmi = "0"
        teste.infNFE.Ide.verProc = "1.10"



        teste.infNFE.Emit.CNPJ = "00417145000192"
        teste.infNFE.Emit.xNome = "TESTANDO A GERACAO DE XML"
        teste.infNFE.Emit.xFant = "TESTE TESTANDO A GERACAO DE XML"
        teste.infNFE.Emit.IE = "096123345"



        Dim _codUF As String = teste.infNFE.Ide.cUF.ToString()
        Dim _dEmi As String = teste.infNFE.Ide.dEmi.ToString("yyMM")
        Dim _cnpj As String = Funcoes.removeFormatacao(teste.infNFE.Emit.CNPJ)
        Dim _mod As String = teste.infNFE.Ide.[mod]

        Dim _serie As String = String.Format("{0:000}", Int32.Parse(teste.infNFE.Ide.serie))
        Dim _numNF As String = String.Format("{0:000000000}", Int32.Parse(teste.infNFE.Ide.nNF))

        Dim _codigo As String = String.Format("{0:000000000}", Int32.Parse(teste.infNFE.Ide.cNF))

        Dim chaveNF As String = _codUF + _dEmi + _cnpj + _mod + _serie + _numNF + _codigo

        Dim _dv As Integer = Funcoes.modulo11(chaveNF)

        teste.Id = chaveNF + _dv.ToString()
        teste.infNFE.Ide.cDV = _dv.ToString()
        'MessageBox.Show("ID: " + teste.Id.ToString());


        teste.infNFE.Emit.EnderEmit.xLgr = "INFORMAR UM ENDERECO"
        teste.infNFE.Emit.EnderEmit.nro = "77"
        teste.infNFE.Emit.EnderEmit.xBairro = "INFORMAR BAIRRO"
        teste.infNFE.Emit.EnderEmit.cMun = 4106902
        teste.infNFE.Emit.EnderEmit.xMun = "MARECHAL CANDIDO RONDON"
        teste.infNFE.Emit.EnderEmit.UF = "PR"
        teste.infNFE.Emit.EnderEmit.CEP = "85960000"
        teste.infNFE.Emit.EnderEmit.cPais = 1058
        teste.infNFE.Emit.EnderEmit.xPais = "BRASIL"
        teste.infNFE.Emit.EnderEmit.fone = "30336300"

        teste.infNFE.Dest.CPF = "00318385023"
        teste.infNFE.Dest.xNome = "INFORMAR UM NOME"
        teste.infNFE.Dest.EnderDest.xLgr = "INFORMAR UM ENDERECO"
        teste.infNFE.Dest.EnderDest.nro = "0"
        teste.infNFE.Dest.EnderDest.xBairro = "INFORMAR UM BAIRRO"
        teste.infNFE.Dest.EnderDest.cMun = 4106902
        teste.infNFE.Dest.EnderDest.xMun = "MARECHAL CANDIDO RONDON"
        teste.infNFE.Dest.EnderDest.UF = "PR"
        teste.infNFE.Dest.EnderDest.CEP = "85960000"
        teste.infNFE.Dest.EnderDest.cPais = 1058
        teste.infNFE.Dest.EnderDest.xPais = "BRASIL"
        teste.infNFE.Dest.IE = ""

        Dim detalhamento As New det()
        detalhamento.nItem = 1
        detalhamento.Prod.cProd = "1/1"
        detalhamento.Prod.cEAN = ""
        detalhamento.Prod.cEANTrib = ""
        detalhamento.Prod.xProd = "INFORMAR UM PRODUTO"
        detalhamento.Prod.CFOP = "5401"
        detalhamento.Prod.uCom = "PC"
        detalhamento.Prod.qCom = 1
        detalhamento.Prod.vUnCom = 550
        detalhamento.Prod.vProd = 550
        detalhamento.Prod.uTrib = "UN"
        detalhamento.Prod.qTrib = 1
        detalhamento.Prod.vUnTrib = 550

        'detalhamento.Prod.Med = new infNFE.det.prod.med();
        ' detalhamento.Prod.Med.nLote = "123656";
        ' detalhamento.Prod.Med.qLote = 10000;
        ' detalhamento.Prod.Med.dFab = DateTime.Now;
        ' detalhamento.Prod.Med.dVal = DateTime.Now;
        ' detalhamento.Prod.Med.vPMC = (decimal)3.5;


        detalhamento.Imposto.Icms = New ICMS()
        detalhamento.Imposto.Icms.Icms00 = New ICMS00()
        detalhamento.Imposto.Icms.Icms00.CST = "00"
        detalhamento.Imposto.Icms.Icms00.orig = "0"
        detalhamento.Imposto.Icms.Icms00.modBC = "0"
        detalhamento.Imposto.Icms.Icms00.vBC = 0
        detalhamento.Imposto.Icms.Icms00.pICMS = 0
        detalhamento.Imposto.Icms.Icms00.vICMS = 0

        detalhamento.Imposto.Pis = New PIS()
        detalhamento.Imposto.Pis.PisAliq = New PISAliq()
        detalhamento.Imposto.Pis.PisAliq.CST = "01"
        detalhamento.Imposto.Pis.PisAliq.vBC = 0
        detalhamento.Imposto.Pis.PisAliq.pPIS = 0
        detalhamento.Imposto.Pis.PisAliq.vPIS = 0


        detalhamento.Imposto.Cofins = New COFINS()
        detalhamento.Imposto.Cofins.CofinsAliq = New COFINSAliq()
        detalhamento.Imposto.Cofins.CofinsAliq.CST = "01"
        detalhamento.Imposto.Cofins.CofinsAliq.vBC = 0
        detalhamento.Imposto.Cofins.CofinsAliq.pCOFINS = 0
        detalhamento.Imposto.Cofins.CofinsAliq.vCOFINS = 0


        teste.infNFE.Total = New total()

        teste.infNFE.Det.Add(detalhamento)

        teste.infNFE.Total.IcmsTot = New ICMSTot()
        teste.infNFE.Total.IcmsTot.vBC = 0
        teste.infNFE.Total.IcmsTot.vBCST = 0
        teste.infNFE.Total.IcmsTot.vST = 0
        teste.infNFE.Total.IcmsTot.vProd = 25
        teste.infNFE.Total.IcmsTot.vFrete = 0
        teste.infNFE.Total.IcmsTot.vSeg = 0
        teste.infNFE.Total.IcmsTot.vDesc = 0
        teste.infNFE.Total.IcmsTot.vII = 0
        teste.infNFE.Total.IcmsTot.vIPI = 0
        teste.infNFE.Total.IcmsTot.vPIS = 0
        teste.infNFE.Total.IcmsTot.vCOFINS = 0
        teste.infNFE.Total.IcmsTot.vOutro = 0
        teste.infNFE.Total.IcmsTot.vNF = 25

        teste.infNFE.Transp = New transp()
        teste.infNFE.Transp.modFrete = "0"

        'Gera o XML
        Dim xmlGerado As XmlDocument = teste.GerarXML()
        'Salva uma cópia do XML não assinado - ATENÇÃO - se você está utilizando Windows Vista/7/Server, salvar na Unidade C pode não ser possível caso o VS2008 não esteja rodando como administrador
        xmlGerado.Save("c:\testeXMLNaoAssinado.xml")
        'Seleciona o certificado
        Dim certificado As X509Certificate2 = CertificadoDigital.SelecionarCertificado()
        'assina o xml
        Dim xmlAssinado As XmlDocument = CertificadoDigital.Assinar(xmlGerado, "infNFe", certificado)

        'Valida o XML assinado
        Dim resultado As String = ValidaXML.ValidarXML(xmlAssinado)

        If resultado.Trim().Length = 0 Then
            resultado = "Xml gerado com sucesso, nenhum erro encontrado"
        End If

        'Opcional - Função para gerar o Lote e deixar o arquivo pronto para ser enviado.
        'teste.GerarLoteNfe(ref xmlAssinado);

        'Importante:
        'Salvar através do TextWriter evita que o XML saia formatado no arquivo, desta forma o mesmo
        'pode ser rejeitado por alguns estados e/ou não validar nos programas teste
        Using xmltw As New XmlTextWriter("C:\testeXML.xml", New UTF8Encoding(False))
            xmlAssinado.WriteTo(xmltw)
            xmltw.Close()
        End Using

        textBox1.Text = resultado
    End Sub

End Class
