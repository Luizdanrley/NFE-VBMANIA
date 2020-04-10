Imports System.Drawing.Drawing2D
Imports System.Xml
Imports System.Text
Imports System
Imports System.Security.Cryptography.X509Certificates
Imports NFE.C_WebService

Public Class TesteDanfe
    Dim caminhoNFE As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Leitura da nfe para impressão retrato
        Dim Ler As New LerXmlNFE
        Dim VNotaLida As New LerXmlNFE.NotaEletronica

        'define as propriedades do controle 
        'OpenFileDialog
        Me.ofd1.Multiselect = True
        Me.ofd1.Title = "Selecionar NFE"
        ofd1.InitialDirectory = "C:\"
        'filtra para exibir somente arquivos de imagens
        ofd1.Filter = "xml |*.xml"
        ofd1.CheckFileExists = True
        ofd1.CheckPathExists = True
        ofd1.FilterIndex = 2
        ofd1.RestoreDirectory = True
        ofd1.ReadOnlyChecked = True
        ofd1.ShowReadOnly = True

        Dim dr As DialogResult = Me.ofd1.ShowDialog()

        If dr = System.Windows.Forms.DialogResult.OK Then
            caminhoNFE = ofd1.FileName()
        Else
            Exit Sub
        End If




        VNotaLida = Ler.LerNFE(caminhoNFE)

        Dim IDanfe As New Nfe_ImprimirDanfeRetrato



        Dim CEmitente As Nfe_ImprimirDanfeRetrato.DEmitente_Retrato = Nothing
        CEmitente.NOME = VNotaLida.NotaEletronica.infNFE.Emit.xNome
        CEmitente.MUNICIPIO = VNotaLida.NotaEletronica.infNFE.Emit.EnderEmit.xMun
        CEmitente.UF = VNotaLida.NotaEletronica.infNFE.Emit.EnderEmit.UF
        CEmitente.ENDERECO_COMPLETO = VNotaLida.NotaEletronica.infNFE.Emit.EnderEmit.xLgr & " " & VNotaLida.NotaEletronica.infNFE.Emit.EnderEmit.nro
        CEmitente.CNPJ = VNotaLida.NotaEletronica.infNFE.Emit.CNPJ & VNotaLida.NotaEletronica.infNFE.Emit.CPF
        CEmitente.CEP = VNotaLida.NotaEletronica.infNFE.Emit.EnderEmit.CEP
        CEmitente.IE = VNotaLida.NotaEletronica.infNFE.Emit.IE
        CEmitente.IESUBS = VNotaLida.NotaEletronica.infNFE.Emit.IEST
        CEmitente.TELEFONE = VNotaLida.NotaEletronica.infNFE.Emit.EnderEmit.fone

        Dim CDadosNF As New Nfe_ImprimirDanfeRetrato.DDadosNfe_Retrato
        CDadosNF.IMP_DV = "SIM"
        CDadosNF.CHAVEACESSO_NFE = VNotaLida.NotaEletronica.Id
        CDadosNF.NUMERO_NFE = VNotaLida.NotaEletronica.infNFE.Ide.nNF
        CDadosNF.NATUREZA_NFE = VNotaLida.ProtocoloAutorizacao
        CDadosNF.PROTOCOLO_NFE = VNotaLida.ProtocoloAutorizacao
        If VNotaLida.NotaEletronica.infNFE.Ide.tpImp = 1 Then
            CDadosNF.TIPONOTA_NFE = Nfe_ImprimirDanfeRetrato.TipoNota_Retrato.SAIDA
        Else
            CDadosNF.TIPONOTA_NFE = Nfe_ImprimirDanfeRetrato.TipoNota_Retrato.ENTRADA
        End If

        Dim CDestinatario As Nfe_ImprimirDanfeRetrato.DDestinatario_Retrato = Nothing
        CDestinatario.CEP = VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.CEP
        CDestinatario.CNPJ = VNotaLida.NotaEletronica.infNFE.Dest.CNPJ & VNotaLida.NotaEletronica.infNFE.Dest.CPF
        CDestinatario.ENDERECO = VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.xLgr & " " & VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.nro
        CDestinatario.BAIRRO = VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.xBairro
        CDestinatario.IE = VNotaLida.NotaEletronica.infNFE.Dest.IE
        CDestinatario.NOME = VNotaLida.NotaEletronica.infNFE.Dest.xNome
        CDestinatario.TELEFONE = VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.fone
        CDestinatario.UF = VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.UF
        CDestinatario.MUNICIPIO = VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.xMun

        Dim CDataHora As Nfe_ImprimirDanfeRetrato.DDataeHora_Retrato = Nothing
        CDataHora.DATA_EMISSAO = VNotaLida.NotaEletronica.infNFE.Ide.dEmi
        CDataHora.DATA_ENTRADA_SAIDA = VNotaLida.NotaEletronica.infNFE.Ide.dSaiEnt
        CDataHora.HORA_ENTRADA_SAIDA = ""

        Dim CTransportadora As Nfe_ImprimirDanfeRetrato.DTransportadora_Retrato = Nothing
        CTransportadora.CEP = ""
        CTransportadora.CNPJ = ""
        CTransportadora.CODIGO_ANTT = ""
        CTransportadora.ENDERECO = ""
        CTransportadora.ESPECIE = ""
        CTransportadora.IE = ""
        CTransportadora.MARCA = ""
        CTransportadora.MUNICIPIO = ""
        CTransportadora.NOME = ""
        CTransportadora.NUMERO = ""
        CTransportadora.PESOBRUTO = ""
        CTransportadora.PESOLIQUIDO = ""
        CTransportadora.PLACA_VEICULO = ""
        CTransportadora.QUANTIDADE = ""
        If VNotaLida.NotaEletronica.infNFE.Transp.modFrete = 0 Then
            CTransportadora.TIPO_PAGAMENTO = Nfe_ImprimirDanfeRetrato.TipoTransportadora_Retrato.EMITENTE
        Else
            CTransportadora.TIPO_PAGAMENTO = Nfe_ImprimirDanfeRetrato.TipoTransportadora_Retrato.DESTINATARIO
        End If

        CTransportadora.UF = ""
        CTransportadora.UF_PLACA = ""

        Dim CValores As Nfe_ImprimirDanfeRetrato.DValores_Retrato = Nothing
        CValores.BASE_CALCULO_ICMS = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vBC), "#,##0.00")
        CValores.BASE_CALCULO_ICMS_SUBS = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vBCST), "#,##0.00")
        CValores.DESCONTO = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vDesc), "#,##0.00")
        CValores.OUTRAS_DESPESAS = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vOutro), "#,##0.00")
        CValores.VALOR_FRETE = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vFrete), "#,##0.00")
        CValores.VALOR_ICMS = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vICMS), "#,##0.00")
        CValores.VALOR_ICMS_SUBS = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vST), "#,##0.00")
        CValores.VALOR_IPI = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vIPI), "#,##0.00")
        CValores.VALOR_SEGURO = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vSeg), "#,##0.00")
        CValores.VALOR_TOTAL_NOTA = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vNF), "#,##0.00")
        CValores.VALOR_TOTAL_PRODUTOS = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vProd), "#,##0.00")

        Dim CISSQN As Nfe_ImprimirDanfeRetrato.DISSQN_Retrato = Nothing
        CISSQN.BASE_CALCULO_ISSQN = ""
        CISSQN.IM = ""
        CISSQN.VALOR_TOTAL_SERVICOS = ""
        CISSQN.VALOR_ISSQN = ""

        Dim CINFO As Nfe_ImprimirDanfeRetrato.DINFOCOMPLEMENTAR_Retrato = Nothing
        CINFO.DADOSADIC = VNotaLida.NotaEletronica.infNFE.InfAdic.infCpl

        Dim PROD As ProdutoDanfe_Retrato

        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        For Each VProduto In VNotaLida.NotaEletronica.infNFE.Det
            PROD = New ProdutoDanfe_Retrato

            PROD.DCodigoProd = VProduto.Prod.cProd
            PROD.DDescricao = VProduto.Prod.xProd
            PROD.DNCM = VProduto.Prod.NCM
            PROD.DCST = VProduto.Prod.uTrib
            PROD.DCFOP = VProduto.Prod.CFOP
            PROD.DUNID = VProduto.Prod.uCom
            PROD.DQT = Decimal.Parse(VProduto.Prod.qCom).ToString("0.##")
            PROD.DVALORUNI = Format(Convert.ToDouble(VProduto.Prod.vUnCom), "#,##0.00")
            PROD.DVALORTOTAL = Format(Convert.ToDouble(VProduto.Prod.vProd), "#,##0.00")
            If Not VProduto.Imposto.Icms.Icms00 Is Nothing Then
                PROD.DBCALC_ICMS = Format(Convert.ToDouble(VProduto.Imposto.Icms.Icms00.vBC), "#,##0.00")
                PROD.DVALORICMS = Format(Convert.ToDouble(VProduto.Imposto.Icms.Icms00.vICMS), "#,##0.00")
                PROD.DBCALC_ICMS_ST = "0,00"
                PROD.DVALORICMS_ST = "0,00"
                PROD.DALIQUOTAICMS = VProduto.Imposto.Icms.Icms00.pICMS
            Else
                PROD.DBCALC_ICMS = "0,00"
                PROD.DVALORICMS = "0,00"
                PROD.DBCALC_ICMS_ST = "0,00"
                PROD.DVALORICMS_ST = "0,00"
                PROD.DALIQUOTAICMS = "0,00"
            End If
            If Not VProduto.Imposto.Ipi Is Nothing Then
                PROD.DVALORIPI = Format(Convert.ToDouble(VProduto.Imposto.Ipi.IpiTrib.vIPI), "#,##0.00")
                PROD.DALIQUOTAIPI = Format(Convert.ToDouble(VProduto.Imposto.Ipi.IpiTrib.vIPI), "#,##0.00")
            Else
                PROD.DVALORIPI = "0,00"
                PROD.DALIQUOTAIPI = "0,00"
            End If

            For Each DetMed In VProduto.Prod.Med
                PROD.LinhaProd.Add("Lote: " & DetMed.nLote & " Qtd: " & Decimal.Parse(DetMed.qLote).ToString("#.##"))
            Next

            'ADICIONA PRODUTOS NA DANFE
            IDanfe.AddProdutosDanfe.Add(PROD)
        Next

        Dim Duplicatas As dup
        Dim TextoDuplicatas As String = ""
        Try
            For Each Duplicatas In VNotaLida.NotaEletronica.infNFE.Cobr.Dup
                TextoDuplicatas = TextoDuplicatas & "Venc: " & Duplicatas.dVenc.ToString("dd/MM/yyyy") & " Valor: " & Format(Convert.ToDouble(Duplicatas.vDup), "#,##0.00") & "  |  "
            Next
        Catch

        End Try
        Dim CFaturamento_Retrato As Nfe_ImprimirDanfeRetrato.DFaturamento_Retrato = Nothing
        CFaturamento_Retrato.Faturamento = TextoDuplicatas


        IDanfe.Faturamento = CFaturamento_Retrato
        IDanfe.Identificacao_Emitente = CEmitente
        IDanfe.Identificacao_Destinatario = CDestinatario
        IDanfe.Data_Hora = CDataHora
        IDanfe.Valores_Nota = CValores
        IDanfe.Identificacao_Transportadora = CTransportadora
        IDanfe.Valores_ISSQN = CISSQN
        IDanfe.InformacoesComplementares = CINFO
        IDanfe.Dados_Nfe = CDadosNF
        IDanfe.VisualizarImpressao()

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim teste As New NFe()
        Dim DtMed As med

        teste.versao = "1.10"

        teste.infNFE.Ide.cUF = 43
        teste.infNFE.Ide.cNF = "000001521"
        teste.infNFE.Ide.natOp = "VENDAS DE MERCADORIA PARA TERCEIROS"
        teste.infNFE.Ide.indPag = "1"
        teste.infNFE.Ide.[mod] = "55"
        teste.infNFE.Ide.serie = "1"
        teste.infNFE.Ide.nNF = "65620"
        teste.infNFE.Ide.dEmi = DateTime.Now.AddDays(-1)
        teste.infNFE.Ide.dSaiEnt = DateTime.Now.AddDays(-1)
        teste.infNFE.Ide.tpNF = 1
        teste.infNFE.Ide.cMunFG = 4314902
        teste.infNFE.Ide.tpImp = 1
        teste.infNFE.Ide.tpEmis = "1"
        teste.infNFE.Ide.tpAmb = 2
        teste.infNFE.Ide.finNFe = "1"
        teste.infNFE.Ide.procEmi = "0"
        teste.infNFE.Ide.verProc = "1.10"

        'NOTAS DE REFERENCIA ' DANIEL MELO 30-03-2010
        Dim NFrefS As New NFref
        NFrefS.RefNF.AAMM = "0109"
        NFrefS.RefNF.CNPJ = "000000000000000"
        NFrefS.RefNF.cUF = 43
        NFrefS.RefNF.mod = "01"
        NFrefS.RefNF.nNF = "12345"
        NFrefS.RefNF.serie = "0"
        teste.infNFE.Ide.NFRef.Add(NFrefS)

        NFrefS = New NFref
        NFrefS.RefNF.AAMM = "0109"
        NFrefS.RefNF.CNPJ = "000000000000000"
        NFrefS.RefNF.cUF = 43
        NFrefS.RefNF.mod = "01"
        NFrefS.RefNF.nNF = "12345"
        NFrefS.RefNF.serie = "0"
        teste.infNFE.Ide.NFRef.Add(NFrefS)

        'QUANDO NOTA FOR VERSÃO ELETRONICA
        NFrefS = New NFref
        NFrefS.refNFe = "00000" 'NUMERO DA NOTA ELETRONICA 40 DIGITOS
        teste.infNFE.Ide.NFRef.Add(NFrefS)

        teste.infNFE.Emit.CNPJ = "0403215662" 'CNPJ DA EMPRESA
        teste.infNFE.Emit.xNome = "SUA EMPRESA"
        teste.infNFE.Emit.xFant = "NOME FANTASIA"
        teste.infNFE.Emit.IE = "0962495824" 'IE DA EMPRESA

        Dim _codUF As String = teste.infNFE.Ide.cUF.ToString()
        Dim _dEmi As String = teste.infNFE.Ide.dEmi.ToString("yyMM")
        Dim _cnpj As String = funcoesNfe.removeFormatacao(teste.infNFE.Emit.CNPJ)
        Dim _mod As String = teste.infNFE.Ide.[mod]

        Dim _serie As String = String.Format("{0:000}", Int32.Parse(teste.infNFE.Ide.serie))
        Dim _numNF As String = String.Format("{0:000000000}", Int32.Parse(teste.infNFE.Ide.nNF))
        Dim _codigo As String = String.Format("{0:000000000}", Int32.Parse(teste.infNFE.Ide.cNF))
        Dim chaveNF As String = _codUF + _dEmi + _cnpj + _mod + _serie + _numNF + _codigo
        Dim _dv As Integer = funcoesNfe.modulo11(chaveNF)

        teste.Id = chaveNF + _dv.ToString()
        teste.infNFE.Ide.cDV = _dv.ToString()
        'MessageBox.Show("ID: " + teste.Id.ToString());


        teste.infNFE.Emit.EnderEmit.xLgr = "Rua Sao Luis"
        teste.infNFE.Emit.EnderEmit.nro = "5555"
        teste.infNFE.Emit.EnderEmit.xBairro = "Cristal"
        teste.infNFE.Emit.EnderEmit.cMun = 4314902
        teste.infNFE.Emit.EnderEmit.xMun = "Porto Alegre"
        teste.infNFE.Emit.EnderEmit.UF = "RS"
        teste.infNFE.Emit.EnderEmit.CEP = "90620170"
        teste.infNFE.Emit.EnderEmit.cPais = 1058
        teste.infNFE.Emit.EnderEmit.xPais = "BRASIL"
        teste.infNFE.Emit.EnderEmit.fone = "93280012"

        teste.infNFE.Dest.CNPJ = "000000000000000" 'CNPJ CLIENTE
        teste.infNFE.Dest.xNome = "NOME DO CLIENTE"
        teste.infNFE.Dest.EnderDest.xLgr = "Av Ipiranga"
        teste.infNFE.Dest.EnderDest.nro = "6690"
        teste.infNFE.Dest.EnderDest.xBairro = "Jardim Botanico"
        teste.infNFE.Dest.EnderDest.cMun = 4314902
        teste.infNFE.Dest.EnderDest.xMun = "Porto Alegre"
        teste.infNFE.Dest.EnderDest.UF = "RS"
        teste.infNFE.Dest.EnderDest.CEP = "90610000"
        teste.infNFE.Dest.EnderDest.cPais = 1058
        teste.infNFE.Dest.EnderDest.xPais = "BRASIL"
        teste.infNFE.Dest.IE = ""

        'DETALHAMENTO DE MEDICAMENTOS 
        Dim detalhamento As New det()
        detalhamento.nItem = 1
        detalhamento.Prod.cProd = "DRV22508X"
        detalhamento.Prod.cEAN = ""
        detalhamento.Prod.cEANTrib = ""
        detalhamento.Prod.xProd = "Sistema de stent coronario troca rapida driver 2,25mmx8mm"
        detalhamento.Prod.CFOP = "5102"
        detalhamento.Prod.uCom = "UN"
        detalhamento.Prod.qCom = 2
        detalhamento.Prod.vUnCom = 2500
        detalhamento.Prod.vProd = 5000
        detalhamento.Prod.uTrib = "UN"
        detalhamento.Prod.qTrib = 1
        detalhamento.Prod.vUnTrib = 2500

        DtMed = New med
        DtMed.nLote = "123656"
        DtMed.qLote = 1
        DtMed.dFab = DateTime.Now
        DtMed.dVal = DateTime.Now
        DtMed.vPMC = Decimal.Parse(2500)

        detalhamento.Prod.Med.Add(DtMed)

        DtMed = New med
        DtMed.nLote = "551234"
        DtMed.qLote = 1
        DtMed.dFab = DateTime.Now
        DtMed.dVal = DateTime.Now
        DtMed.vPMC = Decimal.Parse(2500)

        detalhamento.Prod.Med.Add(DtMed)

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

        teste.infNFE.Det.Add(detalhamento)

        detalhamento = New det()
        detalhamento.nItem = 2
        detalhamento.Prod.cProd = "DRV22508X"
        detalhamento.Prod.cEAN = ""
        detalhamento.Prod.cEANTrib = ""
        detalhamento.Prod.xProd = "Sistema de stent coronario troca rapida driver 2,25mmx8mm"
        detalhamento.Prod.CFOP = "5102"
        detalhamento.Prod.uCom = "UN"
        detalhamento.Prod.qCom = 2
        detalhamento.Prod.vUnCom = 2500
        detalhamento.Prod.vProd = 5000
        detalhamento.Prod.uTrib = "UN"
        detalhamento.Prod.qTrib = 1
        detalhamento.Prod.vUnTrib = 2500

        DtMed = New med
        DtMed.nLote = "123656"
        DtMed.qLote = 1
        DtMed.dFab = DateTime.Now
        DtMed.dVal = DateTime.Now
        DtMed.vPMC = Decimal.Parse(2500)

        detalhamento.Prod.Med.Add(DtMed)

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

        teste.infNFE.Det.Add(detalhamento)

        teste.infNFE.Total = New total()
        teste.infNFE.Total.IcmsTot = New ICMSTot()
        teste.infNFE.Total.IcmsTot.vBC = 0
        teste.infNFE.Total.IcmsTot.vBCST = 0
        teste.infNFE.Total.IcmsTot.vST = 0
        teste.infNFE.Total.IcmsTot.vProd = 5000
        teste.infNFE.Total.IcmsTot.vFrete = 0
        teste.infNFE.Total.IcmsTot.vSeg = 0
        teste.infNFE.Total.IcmsTot.vDesc = 0
        teste.infNFE.Total.IcmsTot.vII = 0
        teste.infNFE.Total.IcmsTot.vIPI = 0
        teste.infNFE.Total.IcmsTot.vPIS = 0
        teste.infNFE.Total.IcmsTot.vCOFINS = 0
        teste.infNFE.Total.IcmsTot.vOutro = 0
        teste.infNFE.Total.IcmsTot.vNF = 5000

        teste.infNFE.Transp = New transp()
        teste.infNFE.Transp.modFrete = "0"

        Dim cobranca As New cobr()

        Dim DUPLICATA As New dup
        DUPLICATA.dVenc = DateTime.Now.AddDays(+30)
        DUPLICATA.nDup = "6001"
        DUPLICATA.vDup = 2500

        cobranca.Dup.Add(DUPLICATA)


        DUPLICATA = New dup
        DUPLICATA.dVenc = DateTime.Now.AddDays(+60)
        DUPLICATA.nDup = "6001"
        DUPLICATA.vDup = 2500

        cobranca.Dup.Add(DUPLICATA)
        teste.infNFE.Cobr = cobranca

        'Gera o XML
        Dim xmlGerado As XmlDocument = teste.GerarXML()
        'Salva uma cópia do XML não assinado - ATENÇÃO - se você está utilizando Windows Vista/7/Server, salvar na Unidade C pode não ser possível caso o VS2008 não esteja rodando como administrador
        xmlGerado.Save("c:\testeXMLNaoAssinado.xml")
        'Seleciona o certificado
        Dim certificado As X509Certificate2 = CertificadoDigital.SelecionarCertificado("")

        'assina o xml
        Dim xmlAssinado As XmlDocument = CertificadoDigital.Assinar(xmlGerado, "infNFe", certificado)

        'Valida o XML assinado
        Dim resultado As String = ValidaXML.ValidarXML(xmlAssinado, "nfe_v1.10.xsd")

        If resultado.Trim().Length = 0 Then
            resultado = "Xml gerado com sucesso, nenhum erro encontrado"
        End If

        'Opcional - Função para gerar o Lote e deixar o arquivo pronto para ser enviado.
        teste.GerarLoteNfe(xmlAssinado)

        'Importante:
        'Salvar através do TextWriter evita que o XML saia formatado no arquivo, desta forma o mesmo
        'pode ser rejeitado por alguns estados e/ou não validar nos programas teste
        Using xmltw As New XmlTextWriter("C:\testeXML.xml", New UTF8Encoding(False))
            xmlAssinado.WriteTo(xmltw)
            xmltw.Close()
        End Using

        textBox1.Text = resultado
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim WebS As New C_WebService
        Dim StrRetorno As RetEnvio

        'ENVIA LOTE DE NOTAS
        StrRetorno = WebS.EnviaLote("c:\testeXML.xml")
        textBox1.Text = StrRetorno.cStat & " " & StrRetorno.nRec & " " & StrRetorno.xMotivo

        'CONSULTA RECEBIMENTO DE LOTE
        Dim RetonoConsulta As New RetRetorno
        RetonoConsulta = WebS.ConsultaRecLote(StrRetorno.nRec)

        For Each RetC In RetonoConsulta.L_Retornos
            MsgBox(RetC.Id)
            MsgBox(RetC.chNFe)
            MsgBox(RetC.cStat)
            MsgBox(RetC.tpAmb)
            MsgBox(RetC.xMotivo)
            MsgBox(RetC.digVal)
        Next
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim WebS As New C_WebService
        Dim StrRetorno As RetEnvio

        'ENVIA LOTE DE NOTAS VERSÃO 2.00
        StrRetorno = WebS.EnviaLote2("c:\testeXML.xml")
        MsgBox(StrRetorno.xMotivo)

        Dim RetonoConsulta As New RetRetorno
        RetonoConsulta = WebS.ConsultaRecLote2(StrRetorno.nRec)
        MsgBox(RetonoConsulta.xMotivo)
        For Each RetC In RetonoConsulta.L_Retornos
            MsgBox(RetC.Id)
            MsgBox(RetC.chNFe)
            MsgBox(RetC.cStat)
            MsgBox(RetC.tpAmb)
            MsgBox(RetC.xMotivo)
            MsgBox(RetC.digVal)
        Next
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim Ler As New LerXmlNFE


        Ler.LerNFE("E:\ARQUIVO XML.xml")
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click

        Dim Ler As New LerXmlNFE
        Dim VNotaLida As New LerXmlNFE.NotaEletronica

        VNotaLida = Ler.LerNFE("C:\43100700417145000192550010000003300419777075 - NF-e.xml")

        Dim IDanfe As New Nfe_ImprimirDanfePaisagem



        Dim CEmitente As Nfe_ImprimirDanfePaisagem.DEmitente = Nothing
        CEmitente.NOME = VNotaLida.NotaEletronica.infNFE.Emit.xNome
        CEmitente.MUNICIPIO = VNotaLida.NotaEletronica.infNFE.Emit.EnderEmit.xMun
        CEmitente.UF = VNotaLida.NotaEletronica.infNFE.Emit.EnderEmit.UF
        CEmitente.ENDERECO_COMPLETO = VNotaLida.NotaEletronica.infNFE.Emit.EnderEmit.xLgr & " " & VNotaLida.NotaEletronica.infNFE.Emit.EnderEmit.nro
        CEmitente.CNPJ = VNotaLida.NotaEletronica.infNFE.Emit.CNPJ & VNotaLida.NotaEletronica.infNFE.Emit.CPF
        CEmitente.CEP = VNotaLida.NotaEletronica.infNFE.Emit.EnderEmit.CEP
        CEmitente.IE = VNotaLida.NotaEletronica.infNFE.Emit.IE
        CEmitente.IESUBS = VNotaLida.NotaEletronica.infNFE.Emit.IEST
        CEmitente.TELEFONE = VNotaLida.NotaEletronica.infNFE.Emit.EnderEmit.fone

        Dim CDadosNF As New Nfe_ImprimirDanfePaisagem.DDadosNfe
        CDadosNF.IMP_DV = "SIM"
        CDadosNF.CHAVEACESSO_NFE = VNotaLida.NotaEletronica.Id
        CDadosNF.NUMERO_NFE = VNotaLida.NotaEletronica.infNFE.Ide.nNF
        CDadosNF.NATUREZA_NFE = VNotaLida.ProtocoloAutorizacao
        CDadosNF.PROTOCOLO_NFE = VNotaLida.ProtocoloAutorizacao
        If VNotaLida.NotaEletronica.infNFE.Ide.tpImp = 1 Then
            CDadosNF.TIPONOTA_NFE = Nfe_ImprimirDanfePaisagem.TipoNota.SAIDA
        Else
            CDadosNF.TIPONOTA_NFE = Nfe_ImprimirDanfePaisagem.TipoNota.ENTRADA
        End If

        Dim CDestinatario As Nfe_ImprimirDanfePaisagem.DDestinatario = Nothing
        CDestinatario.CEP = VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.CEP
        CDestinatario.CNPJ = VNotaLida.NotaEletronica.infNFE.Dest.CNPJ & VNotaLida.NotaEletronica.infNFE.Dest.CPF
        CDestinatario.ENDERECO = VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.xLgr & " " & VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.nro
        CDestinatario.BAIRRO = VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.xBairro
        CDestinatario.IE = VNotaLida.NotaEletronica.infNFE.Dest.IE
        CDestinatario.NOME = VNotaLida.NotaEletronica.infNFE.Dest.xNome
        CDestinatario.TELEFONE = VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.fone
        CDestinatario.UF = VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.UF
        CDestinatario.MUNICIPIO = VNotaLida.NotaEletronica.infNFE.Dest.EnderDest.xMun

        Dim CDataHora As Nfe_ImprimirDanfePaisagem.DDataeHora = Nothing
        CDataHora.DATA_EMISSAO = VNotaLida.NotaEletronica.infNFE.Ide.dEmi
        CDataHora.DATA_ENTRADA_SAIDA = VNotaLida.NotaEletronica.infNFE.Ide.dSaiEnt
        CDataHora.HORA_ENTRADA_SAIDA = ""

        Dim CTransportadora As Nfe_ImprimirDanfePaisagem.DTransportadora = Nothing
        CTransportadora.CEP = ""
        CTransportadora.CNPJ = ""
        CTransportadora.CODIGO_ANTT = ""
        CTransportadora.ENDERECO = ""
        CTransportadora.ESPECIE = ""
        CTransportadora.IE = ""
        CTransportadora.MARCA = ""
        CTransportadora.MUNICIPIO = ""
        CTransportadora.NOME = ""
        CTransportadora.NUMERO = ""
        CTransportadora.PESOBRUTO = ""
        CTransportadora.PESOLIQUIDO = ""
        CTransportadora.PLACA_VEICULO = ""
        CTransportadora.QUANTIDADE = ""
        If VNotaLida.NotaEletronica.infNFE.Transp.modFrete = 0 Then
            CTransportadora.TIPO_PAGAMENTO = Nfe_ImprimirDanfePaisagem.TipoTransportadora.EMITENTE
        Else
            CTransportadora.TIPO_PAGAMENTO = Nfe_ImprimirDanfePaisagem.TipoTransportadora.DESTINATARIO
        End If

        CTransportadora.UF = ""
        CTransportadora.UF_PLACA = ""

        Dim CValores As Nfe_ImprimirDanfePaisagem.DValores = Nothing
        CValores.BASE_CALCULO_ICMS = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vBC), "#,##0.00")
        CValores.BASE_CALCULO_ICMS_SUBS = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vBCST), "#,##0.00")
        CValores.DESCONTO = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vDesc), "#,##0.00")
        CValores.OUTRAS_DESPESAS = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vOutro), "#,##0.00")
        CValores.VALOR_FRETE = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vFrete), "#,##0.00")
        CValores.VALOR_ICMS = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vICMS), "#,##0.00")
        CValores.VALOR_ICMS_SUBS = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vST), "#,##0.00")
        CValores.VALOR_IPI = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vIPI), "#,##0.00")
        CValores.VALOR_SEGURO = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vSeg), "#,##0.00")
        CValores.VALOR_TOTAL_NOTA = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vNF), "#,##0.00")
        CValores.VALOR_TOTAL_PRODUTOS = Format(Convert.ToDouble(VNotaLida.NotaEletronica.infNFE.Total.IcmsTot.vProd), "#,##0.00")

        Dim CISSQN As Nfe_ImprimirDanfePaisagem.DISSQN = Nothing
        CISSQN.BASE_CALCULO_ISSQN = ""
        CISSQN.IM = ""
        CISSQN.VALOR_TOTAL_SERVICOS = ""
        CISSQN.VALOR_ISSQN = ""

        Dim CINFO As Nfe_ImprimirDanfePaisagem.DINFOCOMPLEMENTAR = Nothing
        CINFO.DADOSADIC = VNotaLida.NotaEletronica.infNFE.InfAdic.infCpl

        Dim PROD As ProdutoDanfe

        'PEGA CLASSE PARA PREENCHER OS PRODUTOS
        For Each VProduto In VNotaLida.NotaEletronica.infNFE.Det
            PROD = New ProdutoDanfe

            PROD.DCodigoProd = VProduto.Prod.cProd
            PROD.DDescricao = VProduto.Prod.xProd
            PROD.DNCM = VProduto.Prod.NCM
            PROD.DCST = VProduto.Prod.uTrib
            PROD.DCFOP = VProduto.Prod.CFOP
            PROD.DUNID = VProduto.Prod.uCom
            PROD.DQT = Decimal.Parse(VProduto.Prod.qCom).ToString("0.##")
            PROD.DVALORUNI = Format(Convert.ToDouble(VProduto.Prod.vUnCom), "#,##0.00")
            PROD.DVALORTOTAL = Format(Convert.ToDouble(VProduto.Prod.vProd), "#,##0.00")
            If Not VProduto.Imposto.Icms.Icms00 Is Nothing Then
                PROD.DBCALC_ICMS = Format(Convert.ToDouble(VProduto.Imposto.Icms.Icms00.vBC), "#,##0.00")
                PROD.DVALORICMS = Format(Convert.ToDouble(VProduto.Imposto.Icms.Icms00.vICMS), "#,##0.00")
                PROD.DBCALC_ICMS_ST = "0,00"
                PROD.DVALORICMS_ST = "0,00"
                PROD.DALIQUOTAICMS = VProduto.Imposto.Icms.Icms00.pICMS
            Else
                PROD.DBCALC_ICMS = "0,00"
                PROD.DVALORICMS = "0,00"
                PROD.DBCALC_ICMS_ST = "0,00"
                PROD.DVALORICMS_ST = "0,00"
                PROD.DALIQUOTAICMS = "0,00"
            End If
            If Not VProduto.Imposto.Ipi Is Nothing Then
                PROD.DVALORIPI = Format(Convert.ToDouble(VProduto.Imposto.Ipi.IpiTrib.vIPI), "#,##0.00")
                PROD.DALIQUOTAIPI = Format(Convert.ToDouble(VProduto.Imposto.Ipi.IpiTrib.vIPI), "#,##0.00")
            Else
                PROD.DVALORIPI = "0,00"
                PROD.DALIQUOTAIPI = "0,00"
            End If

            For Each DetMed In VProduto.Prod.Med
                PROD.LinhaProd.Add("Lote: " & DetMed.nLote & " Qtd: " & Decimal.Parse(DetMed.qLote).ToString("#.##"))
            Next

            'ADICIONA PRODUTOS NA DANFE
            IDanfe.AddProdutosDanfe.Add(PROD)
        Next
        Dim Duplicatas As dup
        Dim TextoDuplicatas As String = ""
        Try
            For Each Duplicatas In VNotaLida.NotaEletronica.infNFE.Cobr.Dup
                TextoDuplicatas = TextoDuplicatas & "Venc: " & Duplicatas.dVenc.ToString("dd/MM/yyyy") & " Valor: " & Format(Convert.ToDouble(Duplicatas.vDup), "#,##0.00") & "  |  "
            Next
        Catch

        End Try
        Dim CFaturamento As Nfe_ImprimirDanfePaisagem.DFaturamento = Nothing
        CFaturamento.Faturamento = TextoDuplicatas

        IDanfe.Faturamento = CFaturamento
        IDanfe.Identificacao_Emitente = CEmitente
        IDanfe.Identificacao_Destinatario = CDestinatario
        IDanfe.Data_Hora = CDataHora
        IDanfe.Valores_Nota = CValores
        IDanfe.Identificacao_Transportadora = CTransportadora
        IDanfe.Valores_ISSQN = CISSQN
        IDanfe.InformacoesComplementares = CINFO
        IDanfe.Dados_Nfe = CDadosNF
        IDanfe.VisualizarImpressao()
    End Sub

    Private Sub btnAssinaxml_Click(sender As Object, e As EventArgs) Handles btnAssinaxml.Click
        Dim Assina As New LerXmlNFE
    End Sub
End Class
