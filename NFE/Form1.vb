Imports System.Drawing.Drawing2D

Public Class Form1
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim gr As Graphics = CreateGraphics()
        'Dim rectangle As New Rectangle(120, 20, 100, 20)
        'gr.SmoothingMode = SmoothingMode.AntiAlias
        'Dim pen As New Pen(Brushes.Black, 1)
        'DrawRoundedRectangle(gr, rectangle, 5, Pens.Black)
        Dim a As New ImprimirDanfe("dada")
        a.VisualizarImpressao()
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
