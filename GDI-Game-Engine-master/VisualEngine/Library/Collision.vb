Imports System.ComponentModel
Imports VisualEngine.Engine
Namespace Engine

#Region "Core"
    Public Class Coliders
        Inherits List(Of Colider)

    End Class
        Public Class Colider
            Protected Property UniqueId As Guid
            Public Property Name As String = "Unknown"
            Public Property EntityIndex As Integer
            Public Property GravityColider As Boolean = False
            Public Property Relative As Boolean = True
            Public Property Toggle As Boolean = True
            Public Property Rectangle As New Rectangle(0, 0, 0, 0)
            Sub New()
                UniqueId = Guid.NewGuid()
            End Sub
            Public Overrides Function ToString() As String
                Return Name
            End Function


    End Class

#End Region



End Namespace
