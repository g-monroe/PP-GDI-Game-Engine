Imports System.Drawing.Drawing2D
Imports VisualEngine.Engine.Globals
Namespace Engine
    Public Class Render
#Region "CleanGraphics"
#Region "-] Draw"
        Public Shared Sub DrawImage(g As Graphics, Image As Image, Rectangle As Rectangle)
            If Image IsNot Nothing Then
                'Using Bitmap As New Bitmap(Image)
                g.DrawImage(Image, Rectangle)
                'End Using
            End If
        End Sub
        Public Shared Sub DrawRectangle(g As Graphics, Color As Color, Rectangle As Rectangle)
            Using Pen As New Pen(Color)
                g.DrawRectangle(Pen, Rectangle)
            End Using
        End Sub
        Public Shared Sub DrawEllipse(g As Graphics, Color As Color, Rectangle As Rectangle)
            Using Pen As New Pen(Color)
                g.DrawEllipse(Pen, Rectangle)
            End Using
        End Sub
        Public Shared Sub DrawLine(g As Graphics, Color As Color, Start As Point, Finish As Point)
            Using Pen As New Pen(Color)
                g.DrawLine(Pen, Start, Finish)
            End Using
        End Sub
        Public Shared Sub DrawString(g As Graphics, Text As String, FontSet As Font, Color As Color, Rectangle As Rectangle, Format As StringFormat)
            If Not Text = String.Empty Then
                Using Brush As New SolidBrush(Color)
                    Using Font As Font = FontSet
                        g.DrawString(Text, Font, Brush, Rectangle)

                    End Using
                End Using
            End If
        End Sub
        Public Shared Sub DrawPolygon(g As Graphics, Color As Color, Points As PointF())
            Using Pen As New Pen(Color)
                g.DrawPolygon(Pen, Points)
            End Using
        End Sub
#End Region
#Region "-] Fill"
        Public Shared Sub FillRectangle(g As Graphics, Color As Color, Rectangle As Rectangle)
            Using Brush As New SolidBrush(Color)
                g.FillRectangle(Brush, Rectangle)
            End Using
        End Sub
        Public Shared Sub FillEllipse(g As Graphics, Color As Color, Rectangle As Rectangle)
            Using Brush As New SolidBrush(Color)
                g.FillEllipse(Brush, Rectangle)
            End Using
        End Sub
        Public Shared Sub FillPolygon(g As Graphics, Color As Color, Points As PointF())
            Using Brush As New SolidBrush(Color)
                g.FillPolygon(Brush, Points)
            End Using
        End Sub
#End Region
#Region "-] Brushes"
#Region "-#-] Gradient"
        Public Shared Sub FillGradientRectangle(g As Graphics, Rectangle As Rectangle, FirstColor As Color, SecondColor As Color, Angle As Single, GammaCorrection As Boolean)
            Using Brush As New LinearGradientBrush(Rectangle, FirstColor, SecondColor, Angle)
                Brush.GammaCorrection = GammaCorrection
                g.FillRectangle(Brush, Rectangle)
            End Using
        End Sub
        Public Shared Sub FillGradientEllipse(g As Graphics, Rectangle As Rectangle, FirstColor As Color, SecondColor As Color, Angle As Single, GammaCorrection As Boolean)
            Using Brush As New LinearGradientBrush(Rectangle, FirstColor, SecondColor, Angle)
                Brush.GammaCorrection = GammaCorrection
                g.FillEllipse(Brush, Rectangle)
            End Using
        End Sub
        'NOTE: Fill polygon would have way to many valuables(points) would be faster just to manually add it in.
#End Region
#Region "-#-] Texture"
        Public Shared Sub FillTexture(g As Graphics, Rectangle As Rectangle, Image As Bitmap)
            Dim Brush As New TextureBrush(Image)
            g.FillRectangle(Brush, Rectangle)
            Brush.Dispose()
        End Sub
        'NOTE: Fill polygon would have way to many valuables(points) would be faster just to manually add it in.
#End Region
#End Region
#End Region
        Sub New(Game As Game)
        End Sub
#Region "Events"
        Public Shared Sub DrawEntities(g As Graphics, e As Entity, curlayer As Layer)
            For i As Integer = 0 To curlayer.EntityList.Count - 1
                If e.Index = i And e.Toggled Then
                    Select Case e.Type
                        Case Entity.Kind.Texture
                            If e.Fixed Then
                                g.DrawImage(e.Texture, New Rectangle(0, 0, curlayer.Game.Window.Width, curlayer.Game.Window.Height))
                            Else
                                g.DrawImage(e.Texture, New Rectangle(e.Location, e.Size))
                            End If
                        Case Entity.Kind.Text
                            Using brush As New SolidBrush(e.Color)
                                g.DrawString(e.Text, e.TextFont, brush, New Rectangle(e.Location, e.Size), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                            End Using
                        Case Entity.Kind.Rectange
                            Render.FillRectangle(g, e.Color, New Rectangle(e.Location, e.Size))
                        Case Entity.Kind.Circle
                            Render.FillEllipse(g, e.Color, New Rectangle(e.Location, e.Size))
                        Case Entity.Kind.Tile
                            If e.UpdateTile = True Then
                                For Each tile As Resource.TileMap In curlayer.TileMaps
                                    If tile.Index = e.TileMapIndex Then
                                        e.Texture = Globals.GrabTile(tile, e.Tile, curlayer)
                                    End If
                                Next
                                e.UpdateTile = False
                            End If
                            Render.DrawImage(g, e.Texture, New Rectangle(e.Location, e.Size))
                        Case Entity.Kind.TextureBrush
                            Render.FillTexture(g, New Rectangle(e.Location, e.Size), e.Texture)
                    End Select
                End If
            Next
        End Sub
        Public Shared Sub gxDrawEntities(g As BufferedGraphics, e As Entity, curlayer As Layer)
            For i As Integer = 0 To curlayer.EntityList.Count - 1
                If e.Index = i And e.Toggled Then
                    Select Case e.Type
                        Case Entity.Kind.Texture
                            If e.Fixed Then
                                g.Graphics.DrawImage(e.Texture, New Rectangle(0, 0, curlayer.Game.Window.Width, curlayer.Game.Window.Height))
                            Else
                                g.Graphics.DrawImage(e.Texture, New Rectangle(e.Location, e.Size))
                            End If
                        Case Entity.Kind.Text
                            Using brush As New SolidBrush(e.Color)
                                g.Graphics.DrawString(e.Text, e.TextFont, brush, New Rectangle(e.Location, e.Size), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                            End Using
                        Case Entity.Kind.Rectange
                            Render.FillRectangle(g.Graphics, e.Color, New Rectangle(e.Location, e.Size))
                        Case Entity.Kind.Circle
                            Render.FillEllipse(g.Graphics, e.Color, New Rectangle(e.Location, e.Size))
                        Case Entity.Kind.Tile
                            If e.UpdateTile = True Then
                                For Each tile As Resource.TileMap In curlayer.TileMaps
                                    If tile.Index = e.TileMapIndex Then
                                        e.Texture = Globals.GrabTile(tile, e.Tile, curlayer)
                                    End If
                                Next
                                e.UpdateTile = False
                            End If
                            Render.DrawImage(g.Graphics, e.Texture, New Rectangle(e.Location, e.Size))
                        Case Entity.Kind.TextureBrush
                            Render.FillTexture(g.Graphics, New Rectangle(e.Location, e.Size), e.Texture)
                    End Select
                End If
            Next
        End Sub
        Public Shared Sub DrawFPS(g As Graphics, game As Game)
            g.DrawString("Framerate: " & game.FrameRate, New Font("Arial", 7, FontStyle.Regular), Brushes.Red, New Rectangle(0, 0, 70, 20))
        End Sub
        Public Shared Sub DrawColiders(g As Graphics, game As Game)
            For Each layer As Layer In game.LayerList
                For Each e As Entity In layer.EntityList
                    If e.Toggled Then
                        For Each Colide As Colider In Layer.CollisionList
                            If e.Index = Colide.EntityIndex Then
                                If Colide.Toggle Then
                                    If Colide.Relative Then
                                        DrawRectangle(g, Color.Red, New Rectangle(e.Location.X + Colide.Rectangle.X, e.Location.Y + Colide.Rectangle.Y, Colide.Rectangle.Width, Colide.Rectangle.Height))
                                    Else
                                        DrawRectangle(g, Color.Red, Colide.Rectangle)
                                    End If
                                Else
                                    If Colide.Relative Then
                                        DrawRectangle(g, Color.Pink, New Rectangle(e.Location.X + Colide.Rectangle.X, e.Location.Y + Colide.Rectangle.Y, Colide.Rectangle.Width, Colide.Rectangle.Height))
                                    Else
                                        DrawRectangle(g, Color.Pink, Colide.Rectangle)
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
            Next
        End Sub
#End Region
        Public Shared Sub DrawBuffer(ByVal g As BufferedGraphics, EntityList As Entities, curlayer As Layer)
            For Each en As Entity In EntityList
                If en.BufferedGraphic Then
                    gxDrawEntities(g, en, curlayer)
                End If
            Next
            curlayer.gxUpdate = False
        End Sub
        Public Shared Sub DrawLayers(Layers As Layers, g As Graphics, gfx As BufferedGraphics)
            For i As Integer = 0 To Layers.Count
                For Each curLayer As Layer In Layers
                    If curLayer.gxUpdate = True Then
                        DrawBuffer(gfx, curLayer.EntityList, curLayer)
                    End If
                    For Each e As Entity In curLayer.EntityList
                        If e.BufferedGraphic = False Then
                            DrawEntities(g, e, curLayer)
                        End If
                    Next

                Next
            Next
        End Sub
    End Class
End Namespace
