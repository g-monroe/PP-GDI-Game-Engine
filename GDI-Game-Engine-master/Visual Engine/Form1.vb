Imports System.ComponentModel

Public Class Form1
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        EngineVisual1.StopThreads()
        EngineVisual1.Focus()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EngineVisual1.RunThreads()
    End Sub
End Class
