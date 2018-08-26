Imports System.ComponentModel

Namespace Engine
    Public Class Resource
#Region "TileMap"
        Public Class _TileMaps
            Inherits List(Of TileMap)
        End Class
        Public Class TileMap

            'Properties
            Protected Property UniqueId As Guid
            Public Property Index As Integer
            Public Property Map As Image
            Public Property Name As String = "Unknown"

            'Events
            Sub New()
                UniqueId = Guid.NewGuid()
            End Sub

            Public Overrides Function ToString() As String
                Return Name
            End Function

        End Class


        Public Shared Function GrabTile(tile As TileMap, Region As Rectangle) As Image
            ' Create a new bitmap to hold source tiles bitmap
            Dim fr_bm As New Bitmap(tile.Map)
            ' Create a new blank bitmap to hold
            ' only one 32 by 32 tile
            Dim to_bm As New Bitmap(Region.Size.Width, Region.Size.Height)
            ' Create the drawing object
            Using gr As Graphics = Graphics.FromImage(to_bm)
                Dim to_rect As New Rectangle(0, 0, Region.Size.Width, Region.Size.Height)
                gr.DrawImage(fr_bm, to_rect, Region, GraphicsUnit.Pixel)
                Return to_bm
            End Using
        End Function
#End Region
    End Class
End Namespace
