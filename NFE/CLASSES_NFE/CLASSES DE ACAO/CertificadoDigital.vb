﻿Imports System.Security.Cryptography.Xml
Imports System.Xml
Imports System.Security.Cryptography.X509Certificates

Public Module CertificadoDigital
    ''' <summary>
    ''' Função retirada do UNINFE - http://www.unimake.com.br/uninfe/
    ''' Modificada por Giovanni - Try Ideas - www.tryideas.com.br para exemplo no Blog www.entendendo.net
    '''
    ''' O método assina digitalmente o arquivo XML passado por parâmetro e
    ''' grava o XML assinado com o mesmo nome, sobreponto o XML informado por parâmetro.
    ''' Disponibiliza também uma propriedade com uma string do xml assinado (this.vXmlStringAssinado)
    ''' </summary>
    ''' <param name="pArqXMLAssinar">Nome do arquivo XML a ser assinado</param>
    ''' <param name="pUri">URI (TAG) a ser assinada</param>
    ''' <param name="pCertificado">Certificado a ser utilizado na assinatura</param>
    ''' <remarks>
    ''' Podemos pegar como retorno do método as seguintes propriedades:
    '''
    ''' - Atualiza a propriedade this.vXMLStringAssinado com a string de
    ''' xml já assinada
    '''
    ''' - Grava o XML sobreponto o informado para o método com o conteúdo
    ''' já assinado
    '''
    ''' - Atualiza as propriedades this.vResultado e
    ''' this.vResultadoString com os seguintes valores:
    '''
    ''' 0 - Assinatura realizada com sucesso
    ''' 1 - Erro: Problema ao acessar o certificado digital - %exceção%
    ''' 2 - Problemas no certificado digital
    ''' 3 - XML mal formado + %exceção%
    ''' 4 - A tag de assinatura %pUri% não existe
    ''' 5 - A tag de assinatura %pUri% não é unica
    ''' 6 - Erro ao assinar o documento - %exceção%
    ''' 7 - Falha ao tentar abrir o arquivo XML - %exceção%
    ''' </remarks>
    Public Function Assinar(ByVal docXML As XmlDocument, ByVal pUri As String, ByVal pCertificado As X509Certificate2) As XmlDocument

        Try
            ' Load the certificate from the certificate store.
            Dim cert As X509Certificate2 = pCertificado

            ' Create a new XML document.
            Dim doc As New XmlDocument()

            ' Format the document to ignore white spaces.
            doc.PreserveWhitespace = False

            ' Load the passed XML file using it's name.
            doc = docXML

            ' Create a SignedXml object.
            Dim signedXml As New SignedXml(doc)

            ' Add the key to the SignedXml document.
            signedXml.SigningKey = cert.PrivateKey

            ' Create a reference to be signed.
            Dim reference As New Reference()
            ' pega o uri que deve ser assinada
            Dim _Uri As XmlAttributeCollection = doc.GetElementsByTagName(pUri).Item(0).Attributes
            For Each _atributo As XmlAttribute In _Uri
                If _atributo.Name = "Id" Then
                    reference.Uri = "#" & _atributo.InnerText
                End If
            Next


            ' Add an enveloped transformation to the reference.
            Dim env As New XmlDsigEnvelopedSignatureTransform()
            reference.AddTransform(env)

            Dim c14 As New XmlDsigC14NTransform()
            reference.AddTransform(c14)

            ' Add the reference to the SignedXml object.
            signedXml.AddReference(reference)

            ' Create a new KeyInfo object.
            Dim keyInfo As New KeyInfo()

            ' Load the certificate into a KeyInfoX509Data object
            ' and add it to the KeyInfo object.
            keyInfo.AddClause(New KeyInfoX509Data(cert))

            ' Add the KeyInfo object to the SignedXml object.
            signedXml.KeyInfo = keyInfo

            ' Compute the signature.
            signedXml.ComputeSignature()

            ' Get the XML representation of the signature and save
            ' it to an XmlElement object.
            Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()

            ' Append the element to the XML document.
            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, True))


            If TypeOf doc.FirstChild Is XmlDeclaration Then
                doc.RemoveChild(doc.FirstChild)
            End If

            Return doc
        Catch ex As Exception
            Throw New Exception("Erro ao efetuar assinatura digital, detalhes: " & ex.Message)
        End Try
    End Function


    Public Function SelecionarCertificado() As X509Certificate2
        Dim CertificadoDigital As New X509Certificate2()

        Dim store As New X509Store("MY", StoreLocation.CurrentUser)
        store.Open(OpenFlags.[ReadOnly] Or OpenFlags.OpenExistingOnly)
        Dim collection As X509Certificate2Collection = DirectCast(store.Certificates, X509Certificate2Collection)
        Dim collection1 As X509Certificate2Collection = DirectCast(collection.Find(X509FindType.FindByTimeValid, DateTime.Now, False), X509Certificate2Collection)
        Dim collection2 As X509Certificate2Collection = DirectCast(collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, False), X509Certificate2Collection)
        Dim scollection As X509Certificate2Collection = X509Certificate2UI.SelectFromCollection(collection2, "Certificados disponíveis", "Selecione o certificado digital", X509SelectionFlag.SingleSelection)

        If scollection.Count = 0 Then
            Throw New Exception("Nenhum certificado digital foi selecionado ou o certificado selecionado está com problemas.")
        End If

        Return scollection(0)
    End Function

End Module