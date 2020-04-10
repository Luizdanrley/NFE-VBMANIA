﻿Imports System.IO
Imports System.Xml
Imports System.Xml.Schema

Public Module ValidaXML
    Private _Erro As String
    Public Property Erro() As String
        Get
            Return _Erro
        End Get
        Set(ByVal value As String)
            _Erro = value
        End Set
    End Property

    Public Function ValidarXML(ByVal documento As XmlDocument) As String
        Dim xmlSaida As Stream = New MemoryStream()
        documento.Save(xmlSaida)

        xmlSaida.Flush()
        xmlSaida.Position = 0
        Dim retorno As String = ""

        If (documento IsNot Nothing) AndAlso (File.Exists("nfe_v1.10.xsd")) Then
            'Vai encontrar o arquivo apenas se ele estiver na mesma pasta do executável
            Dim cStreamReader As New StreamReader(xmlSaida)
            Dim cXmlTextReader As New XmlTextReader(cStreamReader)
            Dim reader As New XmlValidatingReader(cXmlTextReader)

            Dim schemaCollection As New XmlSchemaCollection()
            schemaCollection.Add("http://www.portalfiscal.inf.br/nfe", "nfe_v1.10.xsd")

            reader.Schemas.Add(schemaCollection)

            AddHandler reader.ValidationEventHandler, AddressOf reader_ValidationEventHandler

            Erro = ""
            Try
                While reader.Read()
                End While
            Catch ex As Exception
                Erro = ex.Message
            End Try

            reader.Close()

            If Erro <> "" Then
                retorno = "Resultado da validação " & vbCr & vbLf & vbCr & vbLf
                retorno += Erro
                retorno += vbCr & vbLf & "...Fim da validação"
            End If
        Else
            retorno = "Documento XML inválido ou arquivo do Schema não foi encontrado."
        End If

        Return retorno
    End Function

    Private Sub reader_ValidationEventHandler(ByVal sender As Object, ByVal e As ValidationEventArgs)
        Erro = (("Linha: " & e.Exception.LineNumber & " Coluna: ") + e.Exception.LinePosition & " Erro: ") + e.Exception.Message & vbCr & vbLf
    End Sub

End Module