Namespace Engine
    Public Class Globals
#Region "Entity"
        Public Shared Sub MoveEntities(e As Entity, curLayer As Layer)
            If e.Toggled Then
                Dim tempE As Entity = e
                'tempE.Location = New Point(e.Location.X - (e.VelocityLeft + e.Size.Width), e.Location.Y)
                If Not IsColiding(tempE, curLayer) Then
                    e.Location = New Point(e.Location.X - (e.VelocityLeft), e.Location.Y)
                Else
                    e.Location = New Point(e.Location.X - (e.VelocityLeft), e.Location.Y)
                End If
                'tempE.Location = New Point(e.Location.X + e.VelocityRight, e.Location.Y)
                If Not IsColiding(tempE, curLayer) Then
                    e.Location = New Point(e.Location.X + e.VelocityRight, e.Location.Y)
                Else
                    e.Location = New Point(e.Location.X + (e.VelocityLeft), e.Location.Y)
                End If
                e.Location = New Point(e.Location.X, e.Location.Y - e.VelocityUp)
                If e.Location.Y > curLayer.Game.Window.Size.Height Then
                    e.Location = New Point(e.Location.X, 0)
                Else
                    e.Location = New Point(e.Location.X, e.Location.Y + e.VelocityDown)
                End If
            End If
        End Sub
#End Region
#Region "Collision"
        Public Shared Function IsColiding(firstE As Entity, curLayer As Layer) As Boolean
            Dim i As Integer = 0
            For Each secondE As Entity In curLayer.EntityList
                Dim FECL As New Coliders
                Dim SECL As New Coliders
                If Not firstE.Index = secondE.Index Then
#Disable Warning BC42025
                    For Each Collision As Colider In curLayer.CollisionList
                        If Collision.EntityIndex = firstE.Index And Collision.Toggle And Not Collision.GravityColider Then
                            FECL.Add(Collision)
                        End If
                        If Collision.EntityIndex = secondE.Index And Collision.Toggle Then
                            SECL.Add(Collision)
                        End If
                    Next
                    For Each FEC As Colider In FECL
                        For Each SEC As Colider In SECL
                            If FEC.Relative And SEC.Relative Then
                                Dim FECR As New Rectangle(FEC.Rectangle.X + firstE.Location.X, FEC.Rectangle.Y + firstE.Location.Y, FEC.Rectangle.Width, FEC.Rectangle.Height)
                                Dim SECR As New Rectangle(SEC.Rectangle.X + secondE.Location.X, SEC.Rectangle.Y + secondE.Location.Y, SEC.Rectangle.Width, SEC.Rectangle.Height)
                                If FECR.IntersectsWith(SECR) Then
                                    i += 1
                                    Exit For
                                End If
                            ElseIf FEC.Relative Then
                                Dim FECR As New Rectangle(FEC.Rectangle.X + firstE.Location.X, FEC.Rectangle.Y + firstE.Location.Y, FEC.Rectangle.Width, FEC.Rectangle.Height)
                                If FECR.IntersectsWith(SEC.Rectangle) Then
                                    i += 1
                                    Exit For
                                End If
                            ElseIf SEC.Relative Then
                                Dim SECR As New Rectangle(SEC.Rectangle.X + secondE.Location.X, SEC.Rectangle.Y + secondE.Location.Y, SEC.Rectangle.Width, SEC.Rectangle.Height)
                                If FEC.Rectangle.IntersectsWith(SECR) Then
                                    i += 1
                                    Exit For
                                End If
                            Else
                                If FEC.Rectangle.IntersectsWith(SEC.Rectangle) Then
                                    i += 1
                                    Exit For
                                End If
                            End If
                        Next
                    Next
                End If
            Next
            If i = 0 Then
                Return False
            Else
                Return True
            End If
        End Function
        Public Shared Function GravityIsColiding(firstE As Entity, curLayer As Layer) As Boolean
            Dim i As Integer = 0
            For Each secondE As Entity In curLayer.EntityList
                Dim FECL As New Coliders
                Dim SECL As New Coliders
                If Not firstE.Index = secondE.Index Then
                    For Each Collision As Colider In Layer.CollisionList
                        If Collision.EntityIndex = firstE.Index And Collision.Toggle And Collision.GravityColider Then
                            FECL.Add(Collision)
                        End If
                        If Collision.EntityIndex = secondE.Index And Collision.Toggle Then
                            SECL.Add(Collision)
                        End If
                    Next
                    For Each FEC As Colider In FECL
                        For Each SEC As Colider In SECL
                            If FEC.Relative And SEC.Relative Then
                                Dim FECR As New Rectangle(FEC.Rectangle.X + firstE.Location.X, FEC.Rectangle.Y + firstE.Location.Y, FEC.Rectangle.Width, FEC.Rectangle.Height)
                                Dim SECR As New Rectangle(SEC.Rectangle.X + secondE.Location.X, SEC.Rectangle.Y + secondE.Location.Y, SEC.Rectangle.Width, SEC.Rectangle.Height)
                                If FECR.IntersectsWith(SECR) Then
                                    i += 1
                                    Exit For
                                End If
                            ElseIf FEC.Relative Then
                                Dim FECR As New Rectangle(FEC.Rectangle.X + firstE.Location.X, FEC.Rectangle.Y + firstE.Location.Y, FEC.Rectangle.Width, FEC.Rectangle.Height)
                                If FECR.IntersectsWith(SEC.Rectangle) Then
                                    i += 1
                                    Exit For
                                End If
                            ElseIf SEC.Relative Then
                                Dim SECR As New Rectangle(SEC.Rectangle.X + secondE.Location.X, SEC.Rectangle.Y + secondE.Location.Y, SEC.Rectangle.Width, SEC.Rectangle.Height)
                                If FEC.Rectangle.IntersectsWith(SECR) Then
                                    i += 1
                                    Exit For
                                End If
                            Else
                                If FEC.Rectangle.IntersectsWith(SEC.Rectangle) Then
                                    i += 1
                                    Exit For
                                End If
                            End If
                        Next
                    Next
                End If
            Next
            If i = 0 Then
                Return False
            Else
                Return True
            End If
        End Function
        Public Shared Function ProIsColiding(firstE As Entity, secondE As Entity) As Boolean
            Dim FECL As New Coliders
            Dim SECL As New Coliders
            If Not firstE.Index = secondE.Index Then
                For Each Collision As Colider In Layer.CollisionList
                    If Collision.EntityIndex = firstE.Index And Collision.Toggle Then
                        FECL.Add(Collision)
                    End If
                    If Collision.EntityIndex = secondE.Index And Collision.Toggle Then
                        SECL.Add(Collision)
                    End If
                Next
                For Each FEC As Colider In FECL
                    For Each SEC As Colider In SECL
                        If FEC.Relative And SEC.Relative Then
                            Dim FECR As New Rectangle(FEC.Rectangle.X + firstE.Location.X, FEC.Rectangle.Y + firstE.Location.Y, FEC.Rectangle.Width, FEC.Rectangle.Height)
                            Dim SECR As New Rectangle(SEC.Rectangle.X + secondE.Location.X, SEC.Rectangle.Y + secondE.Location.Y, SEC.Rectangle.Width, SEC.Rectangle.Height)
                            If FECR.IntersectsWith(SECR) Then
                                Return True
                                Exit For
                            End If
                        ElseIf FEC.Relative Then
                            Dim FECR As New Rectangle(FEC.Rectangle.X + firstE.Location.X, FEC.Rectangle.Y + firstE.Location.Y, FEC.Rectangle.Width, FEC.Rectangle.Height)
                            If FECR.IntersectsWith(SEC.Rectangle) Then
                                Return True
                                Exit For
                            End If
                        ElseIf SEC.Relative Then
                            Dim SECR As New Rectangle(SEC.Rectangle.X + secondE.Location.X, SEC.Rectangle.Y + secondE.Location.Y, SEC.Rectangle.Width, SEC.Rectangle.Height)
                            If FEC.Rectangle.IntersectsWith(SECR) Then
                                Return True
                                Exit For
                            End If
                        Else
                            If FEC.Rectangle.IntersectsWith(SEC.Rectangle) Then
                                Return True
                                Exit For
                            End If
                        End If
                    Next
                Next
            End If
            Return False
        End Function
#End Region
#Region "Frame"
        Public Delegate Sub InvokeFrameUpdate()
        Public Shared Sub UpdateFrames(e As Entity, curlayer As Layer)
            If e.UpdateFrames Then
                For Each pFrame As Frames.ParentFrame In e.FrameParentList

                    If pFrame.ParentIndex = e.ParentFrameIndex Then

                        Dim nextFrame As Integer = pFrame.ParentCurrInt + 1

                        If nextFrame >= pFrame.ParentMaxInt Then
                            nextFrame = 0
                        End If

                        pFrame.ParentCurrInt = nextFrame

                        For Each cFrame As Frames.ChildFrame In e.FrameChildList
                            If cFrame.ChildIndex = pFrame.ParentCurrInt And cFrame.ParentIndex = pFrame.ParentIndex Then
                                If cFrame.Type = Entity.Kind.Texture Then
                                    e.Type = Entity.Kind.Texture
                                    e.Texture = cFrame.Texture
                                    Exit For
                                End If
                                If cFrame.Type = Entity.Kind.Text Then
                                    e.Type = Entity.Kind.Text
                                    e.Text = cFrame.Text
                                    e.TextFont = cFrame.TextFont
                                    e.Color = cFrame.Color
                                    Exit For
                                End If
                                If cFrame.Type = Entity.Kind.Rectange Then
                                    e.Type = Entity.Kind.Rectange
                                    e.Rectangle = cFrame.Rectangle
                                    e.Color = cFrame.Color
                                    Exit For
                                End If
                                If cFrame.Type = Entity.Kind.Circle Then
                                    e.Type = Entity.Kind.Circle
                                    e.Texture = cFrame.Texture
                                    e.Rectangle = cFrame.Rectangle
                                    e.Color = cFrame.Color
                                    Exit For
                                End If
                                If cFrame.Type = Entity.Kind.TextureBrush Then
                                    e.Type = Entity.Kind.TextureBrush
                                    e.Rectangle = cFrame.Rectangle
                                    e.Color = cFrame.Color
                                End If
                                If cFrame.Type = Entity.Kind.Tile Then
                                    For Each tile As Resource.TileMap In curlayer.TileMaps
                                        If tile.Index = e.TileMapIndex Then
                                            e.Type = Entity.Kind.Tile
                                            e.Rectangle = cFrame.Rectangle
                                            e.Color = cFrame.Color
                                            e.Tile = cFrame.Tile
                                            e.UpdateTile = True
                                            e.TileMapIndex = cFrame.TileMapIndex
                                            e.Texture = Resource.GrabTile(tile, cFrame.Tile)
                                        End If
                                    Next
                                End If
                            End If
                        Next
                    End If
                Next
            End If
        End Sub
        Public Delegate Sub InvokeMoveFrame()
        Public Shared Sub MoveFrame(e As Entity, curlayer As Layer)

            For Each Movement As Frames.Movement In curlayer.MovementList
                If Movement.CurrentPath IsNot Nothing Then
                    If Movement.Toggled Then
                        If Movement.EntityIndex = e.Index Then
                            Dim Xhit As Boolean = False
                            Dim YHit As Boolean = False
                            If (e.Location.X) = Movement.CurrentPath.Location.X Then
                                Xhit = True
                            Else
                                If e.Location.X < Movement.CurrentPath.Location.X Then
                                    e.Location = New Point(e.Location.X + Movement.CurrentPath.Speed, e.Location.Y)
                                    inDirectFrameMovement(e, Direct.Right, Movement.CurrentPath.Speed, curlayer)
                                Else
                                    e.Location = New Point(e.Location.X - Movement.CurrentPath.Speed, e.Location.Y)
                                    inDirectFrameMovement(e, Direct.Left, Movement.CurrentPath.Speed, curlayer)
                                End If
                            End If
                            If (e.Location.Y) = Movement.CurrentPath.Location.Y Then
                                YHit = True
                            Else
                                If e.Location.Y < Movement.CurrentPath.Location.Y Then
                                    e.Location = New Point(e.Location.X, e.Location.Y + Movement.CurrentPath.Speed)
                                    inDirectFrameMovement(e, Direct.Down, Movement.CurrentPath.Speed, curlayer)
                                Else
                                    e.Location = New Point(e.Location.X, e.Location.Y - Movement.CurrentPath.Speed)
                                    inDirectFrameMovement(e, Direct.Up, Movement.CurrentPath.Speed, curlayer)
                                End If
                            End If
                            If Xhit And YHit Then
                                Dim newpath As Integer = Movement.CurrentPath.Index + 1
                                For Each path As Frames.Path In Movement.Paths
                                    If path.Index = newpath And Not newpath > Movement.Paths.Count - 1 Then
                                        Movement.CurrentPath = path
                                        Exit For
                                    ElseIf newpath > Movement.Paths.Count - 1 Then
                                        Movement.CurrentPath = Movement.Paths.Item(0)
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                    End If
                Else
                    Debug.WriteLine(Movement.Paths.Item(0).Name)
                    Movement.CurrentPath = Movement.Paths.Item(0)
                End If
            Next
        End Sub
        Enum Direct
            Left
            Right
            Up
            Down
        End Enum
        Public Shared Sub inDirectFrameMovement(e As Entity, direction As Direct, MovementSpeed As Integer, curlayer As Layer)
            For Each en As Entity In curlayer.EntityList
                If Not en.Index = e.Index Then
                    If Globals.ProIsColiding(e, en) Then
                        Select Case direction
                            Case Direct.Left
                                en.Location = New Point(en.Location.X - MovementSpeed, en.Location.Y)
                            Case Direct.Right
                                en.Location = New Point(en.Location.X + MovementSpeed, en.Location.Y)
                            Case Direct.Down
                                en.Location = New Point(en.Location.X, en.Location.Y + MovementSpeed)
                            Case Direct.Up
                                en.Location = New Point(en.Location.X, en.Location.Y - MovementSpeed)
                        End Select
                    End If
                End If
            Next
        End Sub
#End Region
#Region "Resource"
        Public Shared Function GrabTile(tile As Resource.TileMap, Region As Rectangle, curlayer As Layer) As Image
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
#Region "Input"
#Region "KeyPresses"
        Public Shared kdA As Boolean = False
        Public Shared kdD As Boolean = False
#End Region
        Public Shared Sub KeysDown(e As Entity, game As Game)
            If Input.Keyboard.WasKeyDown(Keys.F1) Then
                game.ShowDebug = Not game.ShowDebug
            End If
            If Input.Keyboard.IsKeyDown(Keys.A) Then
                kdA = True
            Else
                kdA = False
                e.VelocityLeft = 0
            End If
            If Input.Keyboard.IsKeyDown(Keys.D) Then
                kdD = True
            Else
                kdD = False
                e.VelocityRight = 0
            End If
        End Sub
#End Region
#Region "Gravity"
        Public Shared Sub Gravity(e As Entity, curlayer As Layer)
            If e.Gravity Then
                If GravityIsColiding(e, curlayer) Then
                    e.Falling = False
                    e.Location = New Point(e.Location.X, e.Location.Y - e.VelocityDown)
                    e.VelocityDown = 0
                    e.TimeFall = 0

                Else
                    If e.Falling = False Then
                        e.TimeFall = curlayer.Game.ClockSpeed * 10
                    End If
                    e.Falling = True
                End If
                CalcGravity(e, curlayer)
            End If
        End Sub
        Public Shared Sub CalcGravity(e As Entity, curlayer As Layer)
            'Physics Milimeters & seconds
            If e.Falling = True And e.VelocityDown <= 6 Then
                e.TimeFall += curlayer.Game.ClockSpeed
                e.VelocityDown = 1 * (e.TimeFall / 1000)
            End If
        End Sub
#End Region
    End Class
End Namespace
