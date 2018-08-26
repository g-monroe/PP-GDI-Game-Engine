Imports System.ComponentModel
Imports VisualEngine.Engine
Public Class Form1
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Game1.StopThreads()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Game1.RunThreads()

    End Sub
End Class
