'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.portalfiscal.inf.br/nfe/wsdl/NfeRecepcao2"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://www.portalfiscal.inf.br/nfe/wsdl/NfeRecepcao2", IsNullable:=False)> _
Partial Public Class nfeCabecMsg
    Inherits System.Web.Services.Protocols.SoapHeader

    Private cUFField As String

    Private versaoDadosField As String

    Private anyAttrField() As System.Xml.XmlAttribute

    '''<remarks/>
    Public Property cUF() As String
        Get
            Return Me.cUFField
        End Get
        Set(ByVal value As String)
            Me.cUFField = value
        End Set
    End Property

    '''<remarks/>
    Public Property versaoDados() As String
        Get
            Return Me.versaoDadosField
        End Get
        Set(ByVal value As String)
            Me.versaoDadosField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAnyAttributeAttribute()> _
    Public Property AnyAttr() As System.Xml.XmlAttribute()
        Get
            Return Me.anyAttrField
        End Get
        Set(ByVal value As System.Xml.XmlAttribute())
            Me.anyAttrField = value
        End Set
    End Property
End Class
