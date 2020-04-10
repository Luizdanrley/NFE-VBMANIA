Public Class ImprimirDanfeRetrato
    Private Str_ChavedeAcesso As String
    Private WithEvents Prn_VisualizarDanfe As New System.Drawing.Printing.PrintDocument
    Private Visualizador As PrintPreviewDialog

    Public Sub New(ByVal ChavedeAcessoNFe As String)
        Str_ChavedeAcesso = ChavedeAcessoNFe
    End Sub
    Public Sub VisualizarImpressao()
        Visualizador = New PrintPreviewDialog
        Prn_VisualizarDanfe.DefaultPageSettings.Landscape = True
        Visualizador.Document = Prn_VisualizarDanfe
        Visualizador.ShowDialog()
    End Sub
    Private Sub Prn_VisualizarDanfe_PrintPage(ByVal sender As Object, _
       ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles Prn_VisualizarDanfe.PrintPage
        DesenharRetangulos(e)
    End Sub

    Public Sub DesenharRetangulos(ByVal Gra_Saida As System.Drawing.Printing.PrintPageEventArgs)
        'CONVERT A MEDIDA EM MILIMETROS
        Dim pen As New Pen(Brushes.Black, 0.3)
        Gra_Saida.Graphics.PageUnit = GraphicsUnit.Millimeter
        Gra_Saida.Graphics.DrawRectangle(pen, 1, 1, 20, 35)
        Gra_Saida.Graphics.DrawRectangle(pen, 1, 36, 10, 165)
        Gra_Saida.Graphics.DrawRectangle(pen, 11, 36, 10, 100)
        Gra_Saida.Graphics.DrawRectangle(pen, 11, 136, 10, 65)
        Gra_Saida.Graphics.FillRectangle(Brushes.Aqua, 11, 136, 10, 65)

    End Sub

    'REFERENCIA DA ROTINA RETIRADA DO EXEMPLO
    'http://stackoverflow.com/questions/1049328/how-to-create-a-rounded-rectangle-at-runtime-in-windows-forms-with-vb-net-c
    Private Sub DrawRoundedRectangle(ByVal g As Drawing.Graphics, ByVal r As Rectangle, ByVal d As Integer, ByVal p As Pen)
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
