Imports System.Security.Cryptography
Imports System.Security.Permissions
Imports System.IO
Imports System.Threading
Imports System.Web.Services.Protocols
Imports System.Security.Cryptography.X509Certificates
Imports System.Xml

Public Class C_WebService
    'VARIAVEL QUE IRA RECEBER O RETORNO DO ENVIO DO LOTE
    Public Structure RetEnvio
        Dim verAplic As String
        Dim cStat As String
        Dim xMotivo As String
        Dim cUF As String
        Dim nRec As String
        Dim dhRecbto As String
        Dim tMed As String
        Dim XmlRecibo As String
    End Structure

    'VARIAVEL COM AS INFORMACOES DA NFE PARA USAR NA CONSULTA RETORNO
    Public Structure RetRecibo
        Dim Id As String
        Dim tpAmb As String
        Dim verAplic As String
        Dim chNFe As String
        Dim dhRecbto As String
        Dim digVal As String
        Dim cStat As String
        Dim xMotivo As String
        Dim nProt As String
        Dim XmlRecibo As String
    End Structure

    'VARIAVEL QUE VAI RECEBER O RETORNO DA CONSULTA DE ENVIO
    Public Structure RetRetorno
        Dim tpAmb As String
        Dim verAplic As String
        Dim cStat As String
        Dim xMotivo As String
        Dim cUF As String
        Dim nRec As String
        Dim dhRecbto As String
        Dim tMed As String
        Dim L_Retornos As List(Of RetRecibo) 'LISTA DE NOTAS PARA QUANDO MAIS DE UMA NOTA FOI ENVIADA NO LOTE
    End Structure

    Public Function EnviaLote(ByVal EndArquivoXml As String) As RetEnvio
        Dim strRetorno As String
        Dim xmldoc = New XmlDocument()
        xmldoc.Load(EndArquivoXml) 'Carrega o arquivo XML 
        Dim CERT As X509Certificate2
        CERT = SelecionarCertificado("")
        Try
            Dim wsMsg As NfeRecepcao
            Dim wsCabecMsg As String
            Dim Notas As String
            'CABEÇALHO USADA PARA ENVIO DE LOTE
            wsCabecMsg = "<?xml version=""1.0"" encoding=""UTF-8"" ?><cabecMsg xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""1.02""><versaoDados>1.10</versaoDados></cabecMsg>"
            wsMsg = New NfeRecepcao()

            wsMsg.Timeout = 100000
            wsMsg.ClientCertificates.Add(CERT)
            wsMsg.SoapVersion = SoapProtocolVersion.Soap12
            Notas = xmldoc.OuterXml

            strRetorno = wsMsg.nfeRecepcaoLote(wsCabecMsg, Notas)

            'DESMEMBRA RETORNO XML
            Dim StrRetNota As RetEnvio = Nothing
            Dim XmlText As XmlDocument = New XmlDocument
            XmlText.LoadXml(strRetorno)

            Dim nodelist As XmlNodeList = XmlText.DocumentElement.ChildNodes

            For Each outerNode As XmlNode In nodelist
                For Each InnerNode As XmlNode In outerNode.ChildNodes
                    If (InnerNode.Name = "verAplic" Or outerNode.Name = "verAplic") Then
                        StrRetNota.verAplic = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "cStat" Or outerNode.Name = "cStat") Then
                        StrRetNota.cStat = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "xMotivo" Or outerNode.Name = "xMotivo") Then
                        StrRetNota.xMotivo = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "cUF" Or outerNode.Name = "cUF") Then
                        StrRetNota.cUF = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "nRec" Or outerNode.Name = "nRec") Then
                        StrRetNota.nRec = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "dhRecbto" Or outerNode.Name = "dhRecbto") Then
                        StrRetNota.dhRecbto = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "tMed" Or outerNode.Name = "tMed") Then
                        StrRetNota.tMed = InnerNode.InnerText
                    End If
                Next
            Next

            Return StrRetNota
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "")
            Return Nothing
        End Try
    End Function
    Public Function ConsultaRecLote(ByVal NAutorizacao As String) As RetRetorno
        Dim strRetorno As String
        Dim CERT As X509Certificate2
        'BUSCA CERTIFICADO SE DEIXAR EM BRANCO ABRE JANELA DE SELEÇÃO DO WINDOWS
        CERT = SelecionarCertificado("")
        Try
            Dim wsMsg As NfeRetRecepcao
            Dim wsCabecMsg As String
            Dim dados As String
            'CABEÇALHO USADA PARA CONSULTA DE LOTE ENVIADO
            wsCabecMsg = "<?xml version=""1.0"" encoding=""UTF-8"" ?><cabecMsg xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""1.02""><versaoDados>1.10</versaoDados></cabecMsg>"

            'CRIA UMA INSTANCIA DA CONEXÃO COM O WEBSERVICE
            wsMsg = New NfeRetRecepcao()

            'DEFINE TEMPO MAXIMO DE ESPERA POR RETORNO
            wsMsg.Timeout = 100000

            'ASSOCIA CERTIFICADO A CONEXAO WEBSERVICE
            wsMsg.ClientCertificates.Add(CERT)

            'DEFINE PROTOCOLO USADO NA CONEXÃO
            wsMsg.SoapVersion = SoapProtocolVersion.Soap12

            'DADOS DA CONSULTA UNIDCO CAMPO VARIAVEL É O NAutorizacao PASSADO COMO PARAMETRO PARA A FUNCÃO
            dados = "<?xml version=""1.0"" encoding=""utf-8""?><consReciNFe xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""1.10""><tpAmb>1</tpAmb><nRec>" & NAutorizacao & "</nRec></consReciNFe>"

            'ENVIA CONSULTA PARA SEFAZ E OBTEM RETORNO EM FORMATO STRING
            strRetorno = wsMsg.nfeRetRecepcao(wsCabecMsg, dados)

            'DESMEMBRA RETORNO XML
            '-----------------------------------------------------------------------------------
            'VARIAVER QUE VAI RECEBER O RETORNO
            Dim Retornos As New RetRetorno
            'CRIA LISTA PARA RECEBER RETORNOS
            Retornos.L_Retornos = New List(Of RetRecibo)
            'VARIAVEL QUE VAI RECEBER A LISTA ANTES DE SER COLOCADA NO RETORNO
            Dim VarRecibo As New RetRecibo

            'CRIA UM NOVO DOCUMENTO XML
            Dim XmlText As XmlDocument = New XmlDocument
            'ASSOCIA O NOVO XML COM A VARIAVEL DE RETORNO DA SEFAZ
            XmlText.LoadXml(strRetorno)
            ''MsgBox(strRetorno)
            Dim nodelist As XmlNodeList = XmlText.DocumentElement.ChildNodes
            'PERCORRE TODOS OS NOS DO XML E PROCURA A TAG DE RETORNO infProt
            For Each outerNode As XmlNode In nodelist
                'PARA CADA NO VERIFICA SE O MESMO POSSUI FILHOS E VARRE OS MESMOS
                If (outerNode.Name = "tpAmb") Then
                    Retornos.tpAmb = outerNode.InnerText
                End If
                If (outerNode.Name = "verAplic") Then
                    Retornos.verAplic = outerNode.InnerText
                End If
                If (outerNode.Name = "nRec") Then
                    Retornos.nRec = outerNode.InnerText
                End If
                If (outerNode.Name = "cStat") Then
                    Retornos.cStat = outerNode.InnerText
                End If
                If (outerNode.Name = "xMotivo") Then
                    Retornos.xMotivo = outerNode.InnerText
                End If
                If (outerNode.Name = "cUF") Then
                    Retornos.cUF = outerNode.InnerText
                End If
                For Each InnerNode As XmlNode In outerNode.ChildNodes
                    VarRecibo.XmlRecibo = InnerNode.OuterXml
                    'SE O NOME DO NO FOR infProt ENTRA NO MESMO
                    If InnerNode.Name = "infProt" Then
                        'PEGA A VARIAVEL ID QUE É UM ATRIBUTO E NAO UM ITEM
                        VarRecibo.Id = InnerNode.Attributes.ItemOf("Id").InnerText
                        'PERCORRE CAMPOS DO RETORNO PARA CAPTURAR AS INFORMAÇÕES
                        For Each InnerNode2 As XmlNode In InnerNode.ChildNodes
                            If (InnerNode2.Name = "tpAmb") Then
                                VarRecibo.tpAmb = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "verAplic") Then
                                VarRecibo.verAplic = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "chNFe") Then
                                VarRecibo.chNFe = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "dhRecbto") Then
                                VarRecibo.dhRecbto = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "digVal") Then
                                VarRecibo.digVal = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "nProt") Then
                                VarRecibo.nProt = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "cStat") Then
                                VarRecibo.cStat = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "xMotivo") Then
                                VarRecibo.xMotivo = InnerNode2.InnerText
                            End If
                        Next
                        'ADICIONA A NOTA NA LISTA DE RETORNO
                        Retornos.L_Retornos.Add(VarRecibo)
                    End If
                Next
            Next

            Return Retornos

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "")
            Return Nothing
        End Try
    End Function

    Public Function CancelaNFe(ByVal NotaCancelada As String) As RetEnvio
        Dim strRetorno As String
        Dim xmldoc = New XmlDocument()
        Dim CERT As X509Certificate2
        CERT = SelecionarCertificado("")
        Try
            Dim wsMsg As NfeCancelamento
            Dim wsCabecMsg As String
            Dim dados As String
            'CABEÇALHO USADA PARA CONSULTA DE LOTE ENVIADO
            wsCabecMsg = "<?xml version=""1.0"" encoding=""UTF-8""?><cabecMsg xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""1.02""><versaoDados>1.07</versaoDados></cabecMsg>"
            wsMsg = New NfeCancelamento()

            wsMsg.Timeout = 100000
            wsMsg.ClientCertificates.Add(CERT)
            wsMsg.SoapVersion = SoapProtocolVersion.Soap12
            dados = NotaCancelada
            strRetorno = wsMsg.nfeCancelamentoNF(wsCabecMsg, dados)

            'DESMEMBRA RETORNO XML
            Dim StrRetNota As RetEnvio = Nothing
            Dim XmlText As XmlDocument = New XmlDocument
            XmlText.LoadXml(strRetorno)

            Dim nodelist As XmlNodeList = XmlText.DocumentElement.ChildNodes

            For Each outerNode As XmlNode In nodelist
                For Each InnerNode As XmlNode In outerNode.ChildNodes
                    If (InnerNode.Name = "verAplic" Or outerNode.Name = "verAplic") Then
                        StrRetNota.verAplic = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "cStat" Or outerNode.Name = "cStat") Then
                        StrRetNota.cStat = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "xMotivo" Or outerNode.Name = "xMotivo") Then
                        StrRetNota.xMotivo = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "cUF" Or outerNode.Name = "cUF") Then
                        StrRetNota.cUF = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "nRec" Or outerNode.Name = "nRec") Then
                        StrRetNota.nRec = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "dhRecbto" Or outerNode.Name = "dhRecbto") Then
                        StrRetNota.dhRecbto = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "tMed" Or outerNode.Name = "tMed") Then
                        StrRetNota.tMed = InnerNode.InnerText
                    End If
                Next
            Next

            Return StrRetNota

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "")
            Return Nothing
        End Try
    End Function

    Public Function ConsultaNFe(ByVal NotaConsulta As String) As RetEnvio
        Dim strRetorno As String
        Dim xmldoc = New XmlDocument()
        Dim CERT As X509Certificate2
        CERT = SelecionarCertificado("")
        Try
            Dim wsMsg As NfeConsulta
            Dim wsCabecMsg As String
            Dim dados As String
            'CABEÇALHO USADA PARA CONSULTA DE LOTE ENVIADO
            wsCabecMsg = "<?xml version=""1.0"" encoding=""UTF-8""?><cabecMsg xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""1.02""><versaoDados>1.07</versaoDados></cabecMsg>"
            wsMsg = New NfeConsulta

            wsMsg.Timeout = 100000
            wsMsg.ClientCertificates.Add(CERT)
            wsMsg.SoapVersion = SoapProtocolVersion.Soap12
            dados = NotaConsulta
            strRetorno = wsMsg.nfeConsultaNF(wsCabecMsg, dados)

            'DESMEMBRA RETORNO XML
            Dim StrRetNota As RetEnvio = Nothing
            Dim XmlText As XmlDocument = New XmlDocument
            XmlText.LoadXml(strRetorno)

            Dim nodelist As XmlNodeList = XmlText.DocumentElement.ChildNodes

            For Each outerNode As XmlNode In nodelist
                StrRetNota.XmlRecibo = outerNode.OuterXml

                For Each InnerNode As XmlNode In outerNode.ChildNodes
                    If (InnerNode.Name = "verAplic" Or outerNode.Name = "verAplic") Then
                        StrRetNota.verAplic = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "cStat" Or outerNode.Name = "cStat") Then
                        StrRetNota.cStat = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "xMotivo" Or outerNode.Name = "xMotivo") Then
                        StrRetNota.xMotivo = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "cUF" Or outerNode.Name = "cUF") Then
                        StrRetNota.cUF = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "nProt" Or outerNode.Name = "nRec") Then
                        StrRetNota.nRec = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "dhRecbto" Or outerNode.Name = "dhRecbto") Then
                        StrRetNota.dhRecbto = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "tMed" Or outerNode.Name = "tMed") Then
                        StrRetNota.tMed = InnerNode.InnerText
                    End If
                Next
            Next

            Return StrRetNota

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "")
            Return Nothing
        End Try
    End Function


    Public Function EnviaLote2(ByVal EndArquivoXml As String) As RetEnvio
        Dim strRetorno As XmlElement
        Dim xmldoc = New XmlDocument()

        xmldoc.Load(EndArquivoXml) 'Carrega o arquivo XML 
        Dim CERT As X509Certificate2
        CERT = SelecionarCertificado("")
        Try
            Dim wsMsg As NfeRecepcao2
            Dim cab As New nfeCabecMsg

            'UF E VERSÃO DO CABEÇALHO
            cab.cUF = 43
            cab.versaoDados = "2.00"

            Dim Notas As String
            'CABEÇALHO USADA PARA ENVIO DE LOTE
            wsMsg = New NfeRecepcao2
            wsMsg.nfeCabecMsgValue = cab

            wsMsg.Timeout = 100000
            wsMsg.ClientCertificates.Add(CERT)
            wsMsg.SoapVersion = SoapProtocolVersion.Soap12
            Notas = xmldoc.OuterXml

            'RETORNO DA SEFAZ
            strRetorno = wsMsg.nfeRecepcaoLote2(xmldoc)


            'DESMEMBRA RETORNO XML
            Dim StrRetNota As RetEnvio = Nothing
            Dim XmlText As XmlDocument = New XmlDocument

            For Each outerNode As XmlNode In strRetorno
                For Each InnerNode As XmlNode In outerNode.ChildNodes
                    If (InnerNode.Name = "verAplic" Or outerNode.Name = "verAplic") Then
                        StrRetNota.verAplic = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "cStat" Or outerNode.Name = "cStat") Then
                        StrRetNota.cStat = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "xMotivo" Or outerNode.Name = "xMotivo") Then
                        StrRetNota.xMotivo = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "cUF" Or outerNode.Name = "cUF") Then
                        StrRetNota.cUF = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "nRec" Or outerNode.Name = "nRec") Then
                        StrRetNota.nRec = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "dhRecbto" Or outerNode.Name = "dhRecbto") Then
                        StrRetNota.dhRecbto = InnerNode.InnerText
                    End If
                    If (InnerNode.Name = "tMed" Or outerNode.Name = "tMed") Then
                        StrRetNota.tMed = InnerNode.InnerText
                    End If
                Next
            Next

            Return StrRetNota
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "")
            Return Nothing
        End Try
    End Function
    Public Function ConsultaRecLote2(ByVal NAutorizacao As String) As RetRetorno
        Dim strRetorno As XmlElement
        Dim CERT As X509Certificate2
        'BUSCA CERTIFICADO SE DEIXAR EM BRANCO ABRE JANELA DE SELEÇÃO DO WINDOWS
        CERT = SelecionarCertificado("")
        Try
            Dim wsMsg As NfeRetRecepcao2
            Dim cab As New nfeCabecMsg

            'UF E VERSÃO DO CABEÇALHO
            cab.cUF = 43
            cab.versaoDados = "2.00"

            'CRIA UMA INSTANCIA DA CONEXÃO COM O WEBSERVICE
            wsMsg = New NfeRetRecepcao2()

            'ASSOCIA CABEÇALHO NFE
            wsMsg.nfeCabecMsgValue = cab

            'DEFINE TEMPO MAXIMO DE ESPERA POR RETORNO
            wsMsg.Timeout = 100000

            'ASSOCIA CERTIFICADO A CONEXAO WEBSERVICE
            wsMsg.ClientCertificates.Add(CERT)

            'DEFINE PROTOCOLO USADO NA CONEXÃO
            wsMsg.SoapVersion = SoapProtocolVersion.Soap12

            'CRIA UM NOVO DOCUMENTO XML
            Dim dados As XmlDocument = New XmlDocument

            'ASSOCIA O NOVO XML COM A VARIAVEL DE RETORNO DA SEFAZ
            dados.LoadXml("<?xml version=""1.0"" encoding=""utf-8""?><consReciNFe xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""1.10""><tpAmb>1</tpAmb><nRec>" & NAutorizacao & "</nRec></consReciNFe>")


            'ENVIA CONSULTA PARA SEFAZ E OBTEM RETORNO EM FORMATO STRING
            strRetorno = wsMsg.nfeRetRecepcao2(dados)

            'DESMEMBRA RETORNO XML
            '-----------------------------------------------------------------------------------
            'VARIAVER QUE VAI RECEBER O RETORNO
            Dim Retornos As New RetRetorno
            'CRIA LISTA PARA RECEBER RETORNOS
            Retornos.L_Retornos = New List(Of RetRecibo)
            'VARIAVEL QUE VAI RECEBER A LISTA ANTES DE SER COLOCADA NO RETORNO
            Dim VarRecibo As New RetRecibo

            'PERCORRE TODOS OS NOS DO XML E PROCURA A TAG DE RETORNO infProt
            For Each outerNode As XmlNode In strRetorno
                'PARA CADA NO VERIFICA SE O MESMO POSSUI FILHOS E VARRE OS MESMOS
                If (outerNode.Name = "tpAmb") Then
                    Retornos.tpAmb = outerNode.InnerText
                End If
                If (outerNode.Name = "verAplic") Then
                    Retornos.verAplic = outerNode.InnerText
                End If
                If (outerNode.Name = "nRec") Then
                    Retornos.nRec = outerNode.InnerText
                End If
                If (outerNode.Name = "cStat") Then
                    Retornos.cStat = outerNode.InnerText
                End If
                If (outerNode.Name = "xMotivo") Then
                    Retornos.xMotivo = outerNode.InnerText
                End If
                If (outerNode.Name = "cUF") Then
                    Retornos.cUF = outerNode.InnerText
                End If
                For Each InnerNode As XmlNode In outerNode.ChildNodes
                    'SE O NOME DO NO FOR infProt ENTRA NO MESMO
                    If InnerNode.Name = "infProt" Then
                        'PEGA A VARIAVEL ID QUE É UM ATRIBUTO E NAO UM ITEM
                        VarRecibo.Id = InnerNode.Attributes.ItemOf("Id").InnerText
                        'PERCORRE CAMPOS DO RETORNO PARA CAPTURAR AS INFORMAÇÕES
                        For Each InnerNode2 As XmlNode In InnerNode.ChildNodes
                            If (InnerNode2.Name = "tpAmb") Then
                                VarRecibo.tpAmb = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "verAplic") Then
                                VarRecibo.verAplic = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "chNFe") Then
                                VarRecibo.chNFe = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "dhRecbto") Then
                                VarRecibo.dhRecbto = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "digVal") Then
                                VarRecibo.digVal = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "cStat") Then
                                VarRecibo.cStat = InnerNode2.InnerText
                            End If
                            If (InnerNode2.Name = "xMotivo") Then
                                VarRecibo.xMotivo = InnerNode2.InnerText
                            End If
                        Next
                        'ADICIONA A NOTA NA LISTA DE RETORNO
                        Retornos.L_Retornos.Add(VarRecibo)
                    End If
                Next
            Next

            Return Retornos

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "")
            Return Nothing
        End Try
    End Function

End Class
