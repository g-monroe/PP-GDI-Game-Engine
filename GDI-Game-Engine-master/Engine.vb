Imports System.Timers
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports System.Media
Imports System.Threading
Imports System.Runtime.InteropServices

Public Class EngineVisual
    Inherits PictureBox
    Public GameWindow As PictureBox = Me
    Public Property Start As Boolean = False

#Region "Initialize"
    Sub New()

        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.ResizeRedraw Or
                 ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.UserPaint, True)

    End Sub

    Public Sub IntiliazeGraphics(g As Graphics)
        g.SmoothingMode = Quality
        g.Clear(BackColor)
    End Sub

#End Region

#Region "Draw"

#Region "Notes"
    '⌫⚍⚍⚍⚍⚍⚍⌧ Graphics Info ⌧⚍⚍⚍⚍⚍⚍⌦
    '▓ Notes taken Oct 30, 2016 by Kevin.        ▓
    '▓                                           ▓
    '▓ Since Brushes, Pens, Bitmaps, Images, etc ▓
    '▓ don't get disposed when they are done     ▓
    '▓ being use; I have created some functions  ▓
    '▓ to help over clean and simplify my code.  ▓
    '▓ There are still somethings to add here &  ▓
    '▓ there.                                    ▓
    '▓                                           ▓
    '▓⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍▓
#End Region

#Region "CleanGraphics"
#Region "-] Draw"
    Private Sub DrawImage(g As Graphics, Image As Image, Rectangle As Rectangle)
        If Image IsNot Nothing Then
            Using Bitmap As New Bitmap(Image)
                g.DrawImage(Image, Rectangle)
            End Using
        End If
    End Sub
    Private Sub DrawRectangle(g As Graphics, Color As Color, Rectangle As Rectangle)
        Using Pen As New Pen(Color)
            g.DrawRectangle(Pen, Rectangle)
        End Using
    End Sub
    Private Sub DrawEllipse(g As Graphics, Color As Color, Rectangle As Rectangle)
        Using Pen As New Pen(Color)
            g.DrawEllipse(Pen, Rectangle)
        End Using
    End Sub
    Private Sub DrawLine(g As Graphics, Color As Color, Start As Point, Finish As Point)
        Using Pen As New Pen(Color)
            g.DrawLine(Pen, Start, Finish)
        End Using
    End Sub
    Private Sub DrawString(g As Graphics, Text As String, FontSet As Font, Color As Color, Rectangle As Rectangle, Format As StringFormat)
        If Not Text = String.Empty Then
            Using Brush As New SolidBrush(Color)
                Using Font As Font = FontSet
                    g.DrawString(Text, Font, Brush, Rectangle)

                End Using
            End Using
        End If
    End Sub
    Private Sub DrawPolygon(g As Graphics, Color As Color, Points As PointF())
        Using Pen As New Pen(Color)
            g.DrawPolygon(Pen, Points)
        End Using
    End Sub
#End Region
#Region "-] Fill"
    Private Sub FillRectangle(g As Graphics, Color As Color, Rectangle As Rectangle)
        Using Brush As New SolidBrush(Color)
            g.FillRectangle(Brush, Rectangle)
        End Using
    End Sub
    Private Sub FillEllipse(g As Graphics, Color As Color, Rectangle As Rectangle)
        Using Brush As New SolidBrush(Color)
            g.FillEllipse(Brush, Rectangle)
        End Using
    End Sub
    Private Sub FillPolygon(g As Graphics, Color As Color, Points As PointF())
        Using Brush As New SolidBrush(Color)
            g.FillPolygon(Brush, Points)
        End Using
    End Sub
#End Region
#Region "-] Brushes"
#Region "-#-] Gradient"
    Private Sub FillGradientRectangle(g As Graphics, Rectangle As Rectangle, FirstColor As Color, SecondColor As Color, Angle As Single, GammaCorrection As Boolean)
        Using Brush As New LinearGradientBrush(Rectangle, FirstColor, SecondColor, Angle)
            Brush.GammaCorrection = GammaCorrection
            g.FillRectangle(Brush, Rectangle)
        End Using
    End Sub
    Private Sub FillGradientEllipse(g As Graphics, Rectangle As Rectangle, FirstColor As Color, SecondColor As Color, Angle As Single, GammaCorrection As Boolean)
        Using Brush As New LinearGradientBrush(Rectangle, FirstColor, SecondColor, Angle)
            Brush.GammaCorrection = GammaCorrection
            g.FillEllipse(Brush, Rectangle)
        End Using
    End Sub
    'NOTE: Fill polygon would have way to many valuables(points) would be faster just to manually add it in.
#End Region
#Region "-#-] Texture"
    Private Sub FillTexture(g As Graphics, Rectangle As Rectangle, Image As Bitmap)
        Dim Brush As New TextureBrush(Image)
        g.FillRectangle(Brush, Rectangle)
        Brush.Dispose()
    End Sub
    'NOTE: Fill polygon would have way to many valuables(points) would be faster just to manually add it in.
#End Region
#End Region
#End Region

#Region "Events"

    Private Sub DrawEntities(g As Graphics, e As Entity)
        For i As Integer = 0 To EntityList.Count - 1
            If e.Index = i And e.Toggled Then
                Select Case e.Type
                    Case Entity.Kind.Texture
                        DrawImage(g, e.Texture, New Rectangle(e.Location, e.Size))
                    Case Entity.Kind.Text
                        Using brush As New SolidBrush(e.Color)
                            g.DrawString(e.Text, e.TextFont, brush, New Rectangle(e.Location, e.Size), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                        End Using
                    Case Entity.Kind.Rectange
                        FillRectangle(g, e.Color, New Rectangle(e.Location, e.Size))
                    Case Entity.Kind.Circle
                        FillEllipse(g, e.Color, New Rectangle(e.Location, e.Size))
                    Case Entity.Kind.Tile
                        If e.UpdateTile = True Then
                            For Each tile As TileMap In TileMaps
                                If tile.Index = e.TileMapIndex Then
                                    e.Texture = GrabTile(tile, e.Tile)
                                End If
                            Next
                            e.UpdateTile = False
                        End If
                        DrawImage(g, e.Texture, New Rectangle(e.Location, e.Size))
                    Case Entity.Kind.TextureBrush
                        FillTexture(g, New Rectangle(e.Location, e.Size), e.Texture)
                End Select
            End If
        Next
    End Sub
    Private Sub DrawFPS(g As Graphics)
        g.DrawString("Framerate: " & FrameRate, New Font("Arial", 7, FontStyle.Regular), Brushes.Red, New Rectangle(0, 0, 70, 20))
    End Sub
    Private Sub DrawColiders(g As Graphics, e As Entity)
        If e.Toggled Then
            For Each Colide As Colider In CollisionList
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
    End Sub
#End Region

#Region "Core"
    Private g As Graphics
    Public Property Quality As SmoothingMode = SmoothingMode.AntiAlias

    Private Delegate Sub InvokeRender()
    Private Sub Render()
        Refresh()
    End Sub


    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        g = e.Graphics
        '⌦ Intiliaze Graphics
        IntiliazeGraphics(g)

        '⌦ Draw Entities
        For Each en As Entity In EntityList
            DrawEntities(g, en)
            If ShowDebug Then
                DrawColiders(g, en)
            End If
        Next
        If ShowDebug Then
            DrawFPS(g)
        End If
    End Sub
#End Region

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
    Public lstTileMaps As New _TileMaps
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property TileMaps As _TileMaps
        Get
            Return lstTileMaps
        End Get
        Set(ByVal value As _TileMaps)
            lstTileMaps = value
        End Set
    End Property

    Private Function GrabTile(tile As TileMap, Region As Rectangle) As Image
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
#End Region

#Region "Sound"
    Public Class Sounds
        Inherits List(Of Sound)
        Public Shadows Sub Add(Sound As Sound)
            MyBase.Add(Sound)
        End Sub
        Public Shadows Sub AddRange(Range As List(Of Sound))
            MyBase.AddRange(Range)
        End Sub
        Public Shadows Sub Clear()
            MyBase.Clear()
        End Sub
        Public Shadows Sub Remove(Sound As Sound)
            MyBase.Remove(Sound)
        End Sub
        Public Shadows Sub RemoveAt(Index As Integer)
            MyBase.RemoveAt(Index)
        End Sub
        Public Shadows Sub RemoveAll(Predicate As System.Predicate(Of Sound))
            MyBase.RemoveAll(Predicate)
        End Sub
        Public Shadows Sub RemoveRange(Index As Integer, Count As Integer)
            MyBase.RemoveRange(Index, Count)
        End Sub
    End Class
    Public Sub playSound(Index As Integer)
        For Each Sound As Sound In SoundList
            If Sound.Index = Index And Not Sound.playing Then
                Sound.playing = True

                Sound.Player.SoundLocation = Sound.Location
                Sound.Player.Load()

                Sound.Player.Play()
            End If
        Next
    End Sub
    Public Sub stopSound(Index As Integer)
        For Each itm As Sound In SoundList
            If itm.Index = Index Then
                itm.Player.Stop()
            End If
        Next
    End Sub
    Public Class Sound

        Public Player As New SoundPlayer
        Public Property Location As String
        Public Property Volume As Integer = 100
        Public Property Index As Integer
        Public Property playing As Boolean = False
        Public Property Name As String = "Unknown"
        Protected UniqueId As Guid
        Sub New()
            UniqueId = Guid.NewGuid()
        End Sub
        Public Overrides Function ToString() As String
            Return Name
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is Sound Then
                Return (DirectCast(obj, Sound).UniqueId = UniqueId)
            End If
            Return False
        End Function

    End Class
    Public _Sounds As New Sounds
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property SoundList As Sounds
        Get
            Return _Sounds
        End Get
        Set(ByVal value As Sounds)
            _Sounds = value
        End Set
    End Property
#End Region

#Region "Entity"
#Region "Core"
    Public Class Entities
        Inherits List(Of Entity)
    End Class

    Public Class Entity
        'Properties
        Enum Kind
            Texture = 0
            Text = 1
            Rectange = 2
            Circle = 3
            Tile = 4
            TextureBrush = 5
        End Enum
        Public Property Toggled As Boolean = True
        Public Property Type As Kind = Kind.Texture
        Public Property Texture As Image
        Public Property Text As String
        Public Property TextFont As Font
        Public Property Color As Color
        Public Property Rectangle As Rectangle

        Public Property VelocityUp As Decimal
        Public Property VelocityDown As Decimal
        Public Property VelocityLeft As Decimal
        Public Property VelocityRight As Decimal

        Public Property Falling As Boolean = False
        Public Property Gravity As Boolean = False
        Public Property TimeFall As Integer = 0

        Public Property Player As Boolean = False
        Public Property TileMapIndex As Integer
        Public Property Tile As Rectangle
        Public Property UpdateTile As Boolean = False

        Protected Property UniqueId As Guid
        Public Property Index As Integer

        Public Property ParentFrameIndex As Integer
        Public Property UpdateFrames As Boolean = True

        Public Property Location As Point = New Point(New Decimal(0.0), New Decimal(0.0))
        Public Property Size As Size = New Size(New Decimal(32.0), New Decimal(32.0))
        Public Property Fixed As Boolean = False

        Public Property Name As String = "Unknown"
        'Events
        Sub New()
            UniqueId = Guid.NewGuid()
        End Sub
        Public Overrides Function ToString() As String
            Return Name
        End Function

        'Frame Properties
        Public lstParentFrame As New ParentFrames
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public Property FrameParentList As ParentFrames
            Get
                Return lstParentFrame
            End Get
            Set(ByVal value As ParentFrames)
                lstParentFrame = value
            End Set
        End Property
        Public lstChildFrame As New ChildFrames
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public Property FrameChildList As ChildFrames
            Get
                Return lstChildFrame
            End Get
            Set(ByVal value As ChildFrames)
                lstChildFrame = value
            End Set
        End Property

    End Class

    'Engine-Level Property
    Public lstEntity As New Entities
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property EntityList As Entities
        Get
            Return lstEntity
        End Get
        Set(ByVal value As Entities)
            lstEntity = value
        End Set
    End Property

#End Region

#Region "Events"


    Public Sub MoveEntities(e As Entity)
        If e.Toggled Then
            Dim tempE As Entity = e
            'tempE.Location = New Point(e.Location.X - (e.VelocityLeft + e.Size.Width), e.Location.Y)
            If Not IsColiding(tempE) Then
                e.Location = New Point(e.Location.X - (e.VelocityLeft), e.Location.Y)
            Else
                e.Location = New Point(e.Location.X - (e.VelocityLeft), e.Location.Y)
            End If
            'tempE.Location = New Point(e.Location.X + e.VelocityRight, e.Location.Y)
            If Not IsColiding(tempE) Then
                e.Location = New Point(e.Location.X + e.VelocityRight, e.Location.Y)
            Else
                e.Location = New Point(e.Location.X + (e.VelocityLeft), e.Location.Y)
            End If
            e.Location = New Point(e.Location.X, e.Location.Y - e.VelocityUp)
            If e.Location.Y > GameWindow.Size.Height Then
                e.Location = New Point(e.Location.X, 0)
            Else
                e.Location = New Point(e.Location.X, e.Location.Y + e.VelocityDown)
            End If
        End If
    End Sub
#End Region
#End Region

#Region "Collision"
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
    Public CollisionList As New Coliders
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property ColiderList As Coliders
        Get
            Return CollisionList
        End Get
        Set(ByVal value As Coliders)
            CollisionList = value
        End Set
    End Property
#End Region

#Region "Events"
    Public Function IsColiding(firstE As Entity) As Boolean
        Dim i As Integer = 0
        For Each secondE As Entity In EntityList
            Dim FECL As New Coliders
            Dim SECL As New Coliders
            If Not firstE.Index = secondE.Index Then
                For Each Collision As Colider In CollisionList
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
    Public Function GravityIsColiding(firstE As Entity) As Boolean
        Dim i As Integer = 0
        For Each secondE As Entity In EntityList
            Dim FECL As New Coliders
            Dim SECL As New Coliders
            If Not firstE.Index = secondE.Index Then
                For Each Collision As Colider In CollisionList
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
    Public Function ProIsColiding(firstE As Entity, secondE As Entity) As Boolean
        Dim FECL As New Coliders
        Dim SECL As New Coliders
        If Not firstE.Index = secondE.Index Then
            For Each Collision As Colider In CollisionList
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

#End Region

#Region "Gravity"
    Public Sub Gravity(e As Entity)
        If e.Gravity Then
            If GravityIsColiding(e) Then
                e.Falling = False
                e.Location = New Point(e.Location.X, e.Location.Y - e.VelocityDown)
                e.VelocityDown = 0
                e.TimeFall = 0

            Else
                If e.Falling = False Then
                    e.TimeFall = ClockSpeed * 10
                End If
                e.Falling = True
            End If
            CalcGravity(e)
        End If
    End Sub
    Public Sub CalcGravity(e As Entity)
        'Physics Milimeters & seconds
        If e.Falling = True And e.VelocityDown <= 6 Then
            e.TimeFall += ClockSpeed
            e.VelocityDown = 1 * (e.TimeFall / 1000)
        End If
    End Sub
#End Region

#Region "Animation"
#Region "Core"
    Public Class ParentFrames
        Inherits List(Of ParentFrame)
    End Class
    Public Class ParentFrame

        'Properties
        Protected Property UniqueId As Guid
        Public Property ParentIndex As Integer
        Public Property ParentMaxInt As Integer
        Public Property ParentCurrInt As Integer
        Public Property Name As String = "Unknown"

        'Events
        Sub New()
            UniqueId = Guid.NewGuid()
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class

    Public Class ChildFrames
        Inherits List(Of ChildFrame)
    End Class
    Public Class ChildFrame

        'Properties
        Protected Property UniqueId As Guid
        Public Property ChildIndex As Integer
        Public Property ParentIndex As Integer
        Public Property Name As String = "Unknown"
        Public Property Type As Entity.Kind = Entity.Kind.Texture
        Public Property Texture As Image
        Public Property Text As String
        Public Property TextFont As Font
        Public Property Color As Color
        Public Property TileMapIndex As Integer
        Public Property Tile As Rectangle
        Public Property Rectangle As Rectangle

        'Events
        Sub New()
            UniqueId = Guid.NewGuid()
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class
#End Region
#Region "FrameMovement"
#Region "Core"
    Public Class Movements
        Inherits List(Of Movement)
    End Class
    Public Class Movement
        'Properties
        Protected Property UniqueId As Guid
        Public Property EntityIndex As Integer
        Public PathList As New Paths
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public Property Paths As Paths
            Get
                Return PathList
            End Get
            Set(ByVal value As Paths)
                PathList = value
            End Set
        End Property
        Public Property Toggled As Boolean = False
        Public Property CurrentPath As Path
        Public Property Name As String = "Unknown"

        'Events
        Sub New()
            UniqueId = Guid.NewGuid()
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class

    Public Class Paths
        Inherits List(Of Path)
    End Class
    Public Class Path
        Protected Property UniqueId As Guid
        Public Property Speed As Decimal = 1
        Public Property Location As Point = New Point(New Decimal(0.0), New Decimal(0.0))
        Public Property Index As Integer = 0
        Public Property Name As String = "Unknown"
        Sub New()
            UniqueId = Guid.NewGuid()
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
    Public MovementList As New Movements
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property EnitityMovement As Movements
        Get
            Return MovementList
        End Get
        Set(ByVal value As Movements)
            MovementList = value
        End Set
    End Property
#End Region
#End Region

#Region "Events"
    Private Delegate Sub InvokeFrameUpdate()
    Private Sub UpdateFrames(e As Entity)
        If e.UpdateFrames Then
            For Each pFrame As ParentFrame In e.FrameParentList

                If pFrame.ParentIndex = e.ParentFrameIndex Then

                    Dim nextFrame As Integer = pFrame.ParentCurrInt + 1

                    If nextFrame >= pFrame.ParentMaxInt Then
                        nextFrame = 0
                    End If

                    pFrame.ParentCurrInt = nextFrame

                    For Each cFrame As ChildFrame In e.FrameChildList
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
                                For Each tile As TileMap In TileMaps
                                    If tile.Index = e.TileMapIndex Then
                                        e.Type = Entity.Kind.Tile
                                        e.Rectangle = cFrame.Rectangle
                                        e.Color = cFrame.Color
                                        e.Tile = cFrame.Tile
                                        e.UpdateTile = True
                                        e.TileMapIndex = cFrame.TileMapIndex
                                        e.Texture = GrabTile(tile, cFrame.Tile)
                                    End If
                                Next
                            End If
                        End If
                    Next

                End If

            Next
        End If
    End Sub
    Private Delegate Sub InvokeMoveFrame()
    Private Sub MoveFrame(e As Entity)

        For Each Movement As Movement In MovementList
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
                                inDirectFrameMovement(e, Direct.Right, Movement.CurrentPath.Speed)
                            Else
                                e.Location = New Point(e.Location.X - Movement.CurrentPath.Speed, e.Location.Y)
                                inDirectFrameMovement(e, Direct.Left, Movement.CurrentPath.Speed)
                            End If
                        End If
                        If (e.Location.Y) = Movement.CurrentPath.Location.Y Then
                            YHit = True
                        Else
                            If e.Location.Y < Movement.CurrentPath.Location.Y Then
                                e.Location = New Point(e.Location.X, e.Location.Y + Movement.CurrentPath.Speed)
                                inDirectFrameMovement(e, Direct.Down, Movement.CurrentPath.Speed)
                            Else
                                e.Location = New Point(e.Location.X, e.Location.Y - Movement.CurrentPath.Speed)
                                inDirectFrameMovement(e, Direct.Up, Movement.CurrentPath.Speed)
                            End If
                        End If
                        If Xhit And YHit Then
                            Dim newpath As Integer = Movement.CurrentPath.Index + 1
                            For Each path As Path In Movement.Paths
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
    Private Sub inDirectFrameMovement(e As Entity, direction As Direct, MovementSpeed As Integer)
        For Each en As Entity In EntityList
            If Not en.Index = e.Index Then
                If ProIsColiding(e, en) Then
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

#End Region

#Region "Camera"

#End Region

#Region "Input"
#Region "KeyPresses"
    Private kdA As Boolean = False
    Private kdD As Boolean = False
#End Region
#Region "KeyDown"
#Region "Core"
    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Space
                Start = Not Start
            Case Keys.A
                kdA = True
            Case Keys.D
                kdD = True
            Case Keys.F1
                ShowDebug = Not ShowDebug

        End Select
    End Sub
    Public Sub KeysDown(e As Entity)
        If Keyboard.WasKeyDown(Keys.F1) Then
            ShowDebug = Not ShowDebug
        End If
        If Keyboard.IsKeyDown(Keys.A) Then
            kdA = True
        Else
            kdA = False
            e.VelocityLeft = 0
        End If
        If Keyboard.IsKeyDown(Keys.D) Then
            kdD = True
        Else
            kdD = False
            e.VelocityRight = 0
        End If
    End Sub
    Public Sub KeysUp()
        If Keyboard.WasKeyUp(Keys.A) Then
            kdA = False
        End If
        If Keyboard.WasKeyUp(Keys.D) Then
            kdD = False
        End If
    End Sub
    Public Class Keyboard
        <DllImport("User32.dll")>
        Private Shared Function GetAsyncKeyState(vKey As System.Int32) As Short
        End Function

        <DllImport("user32.dll")>
        Private Shared Function GetForegroundWindow() As IntPtr
        End Function

        Shared oldState As New List(Of Integer)()

        Public Shared Sub Update()
            Dim removeList As New List(Of Integer)()

            For Each key As Integer In oldState
                If Not IsKeyDown(key) Then
                    removeList.Add(key)
                End If
            Next

            For Each rKey As Integer In removeList
                oldState.Remove(rKey)
            Next
        End Sub

        Public Shared Function WasKeyDown(vKey As Keys) As Boolean
            If oldState.Contains(CInt(vKey)) Then
                Return False
            Else
                If IsKeyDown(vKey) Then
                    oldState.Add(CInt(vKey))
                    Return True
                End If
            End If

            Return False
        End Function

        Public Shared Function WasKeyUp(vKey As Keys) As Boolean
            If oldState.Contains(CInt(vKey)) Then
                If Not IsKeyDown(vKey) Then
                    oldState.Remove(CInt(vKey))
                    Return True
                End If
            End If

            Return False
        End Function

        Public Shared Function IsKeyDown(vKey As Keys) As Boolean
            If GetForegroundWindow() <> Process.GetCurrentProcess().MainWindowHandle Then
                Return False
            End If
            Return 0 <> (GetAsyncKeyState(CInt(vKey)) And &H8000)
        End Function
        Public Shared Function IsKeyDown(key As Integer) As Boolean
            If GetForegroundWindow() <> Process.GetCurrentProcess().MainWindowHandle Then
                Return False
            End If
            Return 0 <> (GetAsyncKeyState(key) And &H8000)
        End Function
    End Class
#End Region
#End Region
#End Region

#Region "Debug"
#Region "Core"
    Public Property ShowDebug As Boolean = False
    Private Delegate Sub InvokeDebug()
    Private Sub nDebug()
        fpsTick()
    End Sub
#End Region

#Region "FPS"
    Private FrameRate As Integer = 60
    Public Property FrameRateCap As Integer = 60
    Private ptlu As Double
    Private callCount As Integer
    Public Sub fpsTick()
        callCount += 1
        If (Environment.TickCount - ptlu) >= 1000 Then
            FrameRate = callCount
            callCount = 0
            ptlu = Environment.TickCount
        End If
    End Sub
#End Region

#End Region

#Region "Clock"

#Region "Notes"
    '⌫⚍⚍⚍⚍⚍⚍⚍⌧ Clock Info ⌧⚍⚍⚍⚍⚍⚍⚍⌦
    '▓ Notes taken Oct 30, 2016 by Kevin.        ▓
    '▓                                           ▓
    '▓  Researching different types of Timers to ▓
    '▓ see which timer can handle the refresh ➸ ▓
    '▓ rate. Found Timers.Timer which can't ➸   ▓
    '▓ handle manual refresh inside the Timer or ▓
    '▓ from a called sub or function. If you use ▓
    '▓ Timers.Timer you will have to use  ➸     ▓
    '▓ threading which will help with game perf. ▓
    '▓                                           ▓
    '▓⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍⚍▓
#End Region

    Private gameClock As Thread
    Public TotalTime As Double
    Public DeltaTime As Double
    Private oldtime As TimeSpan
    Public Property VSync As Boolean = True
    Public Property ClockSpeed As Integer = (1000 / FrameRateCap)
    Public Sub RunThreads()
        gameClock = New Thread(New ThreadStart(AddressOf GameLoop))
        gameClock.Start()
        Start = True
    End Sub
    Public Sub StopThreads()
        Start = False
        DisposeThreads()
    End Sub
    Public Sub DisposeThreads()
        If gameClock IsNot Nothing And Not Start Then
            gameClock.Abort()
        End If
    End Sub
    Friend Sub tick()
        Try
            'Redraw the control.
            If GameWindow.Image IsNot Nothing Then : GameWindow.Image.Dispose() : End If
            GameWindow.Invalidate()
            ' Update Delta
            CalcDeltaTime()
            TotalTime += DeltaTime
            For Each e As Entity In EntityList
                '⌦Animate
                UpdateFrames(e)
                '⌦Move Frame Entities
                MoveFrame(e)
                '⌦Move Player
                Keyboard.Update()
                KeysDown(e)
                If kdA Then
                    If e.Player And e.Toggled Then
                        e.VelocityLeft = 1
                    End If
                End If
                If kdD Then
                    If e.Player And e.Toggled Then
                        e.VelocityRight = 1
                    End If
                End If
                '⌦Gravity
                Gravity(e)

                '⌦MoveEntities
                MoveEntities(e)
            Next
            playSound(0)

            '⌦Debug
            nDebug()
        Catch e As Exception
            Debug.Print(e.ToString)
        End Try
        'Collect Garbage 
        GC.Collect()


    End Sub
    Private Sub CalcDeltaTime()
        Dim deltaTim As TimeSpan = (DateTime.Now.TimeOfDay - oldtime)
        DeltaTime = CDbl(deltaTim.TotalSeconds)
        oldtime = DateTime.Now.TimeOfDay
    End Sub
    Private Sub GameLoop()
        While Start
            Try
                oldtime = DateTime.Now.TimeOfDay
                ' Invoke the draw
                GameWindow.Invoke(New Action(AddressOf tick))


                ' Lock FPS
                Dim TimeSinceLastDraw As TimeSpan = (DateTime.Now.TimeOfDay - oldtime)
                If VSync Then
                    Dim sleep As Single = CType((ClockSpeed - TimeSinceLastDraw.TotalMilliseconds), Integer)
                    If (sleep > 0) Then
                        Thread.Sleep(CType(sleep, Integer))
                    End If
                End If

            Catch e As Exception
                Debug.Print(e.ToString)
            End Try
        End While
    End Sub
#Region "Old Clock"
    'Private WithEvents Clock As New Timers.Timer With {.Enabled = True, .Interval = ClockSpeed}
    'Private Sub Clock_Elapsed(sender As Object, e As ElapsedEventArgs) Handles Clock.Elapsed
    '    If Start Then
    '        '[Major Key]⌦This try here is to catch the errors when the form isn't created yet. 
    '        Try
    '            ' Dim Tpool As Threading.ThreadPool
    '            Dim keyThread As New Threading.Thread(AddressOf KeyPressDown)
    '            '⌦Input
    '            'TPool.QueueUserWorkItem(New System.Threading.WaitCallback(AddressOf KeyPressDown))
    '            'BeginInvoke(New InvokeKeyDown(AddressOf KeyPressDown))

    '            '⌦Animate
    '            Dim frameThread As New Threading.Thread(AddressOf UpdateFrames)
    '            frameThread.Start()
    '            'BeginInvoke(New InvokeFrameUpdate(AddressOf UpdateFrames))


    '            '⌦Move Entities
    '            Dim moveThread As New Threading.Thread(AddressOf MoveFrame)
    '            moveThread.Start()
    '            'TPool.QueueUserWorkItem(New System.Threading.WaitCallback(AddressOf MoveFrame))
    '            'BeginInvoke(New InvokeMoveFrame(AddressOf MoveFrame))

    '            '⌦Render
    '            ' TPool.QueueUserWorkItem(New System.Threading.WaitCallback(AddressOf Render))
    '            'BeginInvoke(New InvokeRender(AddressOf Render))

    '            '⌦Debug
    '            BeginInvoke(New InvokeDebug(AddressOf nDebug))

    '        Catch ex As Exception

    '        End Try
    '    Else
    '        ClockSpeed = (1000 / FrameRateCap)
    '        'Clock.Interval = ClockSpeed
    '    End If
    'End Sub

#End Region



#End Region
End Class
