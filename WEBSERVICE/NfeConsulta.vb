﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.4927
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by wsdl, Version=2.0.50727.42.
'

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Web.Services.WebServiceBindingAttribute(Name:="NfeConsultaSoap12", [Namespace]:="http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta")>  _
Partial Public Class NfeConsulta
    Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
    
    Private nfeConsultaNFOperationCompleted As System.Threading.SendOrPostCallback
    
    '''<remarks/>
    Public Sub New()
        MyBase.New
        Me.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12
        Me.Url = "https://homologacao.nfe.sefaz.rs.gov.br/ws/nfeconsulta/nfeconsulta.asmx"
    End Sub
    
    '''<remarks/>
    Public Event nfeConsultaNFCompleted As nfeConsultaNFCompletedEventHandler
    
    '''<remarks/>
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta/nfeConsultaNF", RequestNamespace:="http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta", ResponseNamespace:="http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
    Public Function nfeConsultaNF(ByVal nfeCabecMsg As String, ByVal nfeDadosMsg As String) As String
        Dim results() As Object = Me.Invoke("nfeConsultaNF", New Object() {nfeCabecMsg, nfeDadosMsg})
        Return CType(results(0),String)
    End Function
    
    '''<remarks/>
    Public Function BeginnfeConsultaNF(ByVal nfeCabecMsg As String, ByVal nfeDadosMsg As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
        Return Me.BeginInvoke("nfeConsultaNF", New Object() {nfeCabecMsg, nfeDadosMsg}, callback, asyncState)
    End Function
    
    '''<remarks/>
    Public Function EndnfeConsultaNF(ByVal asyncResult As System.IAsyncResult) As String
        Dim results() As Object = Me.EndInvoke(asyncResult)
        Return CType(results(0),String)
    End Function
    
    '''<remarks/>
    Public Overloads Sub nfeConsultaNFAsync(ByVal nfeCabecMsg As String, ByVal nfeDadosMsg As String)
        Me.nfeConsultaNFAsync(nfeCabecMsg, nfeDadosMsg, Nothing)
    End Sub
    
    '''<remarks/>
    Public Overloads Sub nfeConsultaNFAsync(ByVal nfeCabecMsg As String, ByVal nfeDadosMsg As String, ByVal userState As Object)
        If (Me.nfeConsultaNFOperationCompleted Is Nothing) Then
            Me.nfeConsultaNFOperationCompleted = AddressOf Me.OnnfeConsultaNFOperationCompleted
        End If
        Me.InvokeAsync("nfeConsultaNF", New Object() {nfeCabecMsg, nfeDadosMsg}, Me.nfeConsultaNFOperationCompleted, userState)
    End Sub
    
    Private Sub OnnfeConsultaNFOperationCompleted(ByVal arg As Object)
        If (Not (Me.nfeConsultaNFCompletedEvent) Is Nothing) Then
            Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
            RaiseEvent nfeConsultaNFCompleted(Me, New nfeConsultaNFCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
        End If
    End Sub
    
    '''<remarks/>
    Public Shadows Sub CancelAsync(ByVal userState As Object)
        MyBase.CancelAsync(userState)
    End Sub
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")>  _
Public Delegate Sub nfeConsultaNFCompletedEventHandler(ByVal sender As Object, ByVal e As nfeConsultaNFCompletedEventArgs)

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code")>  _
Partial Public Class nfeConsultaNFCompletedEventArgs
    Inherits System.ComponentModel.AsyncCompletedEventArgs
    
    Private results() As Object
    
    Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
        MyBase.New(exception, cancelled, userState)
        Me.results = results
    End Sub
    
    '''<remarks/>
    Public ReadOnly Property Result() As String
        Get
            Me.RaiseExceptionIfNecessary
            Return CType(Me.results(0),String)
        End Get
    End Property
End Class
