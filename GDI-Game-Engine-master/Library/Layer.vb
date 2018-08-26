Imports System.ComponentModel
Imports VisualEngine.Engine
Namespace Engine
    Public Class Layer
        Protected Property UniqueId As Guid
        Public Property Game As Game
        Public Property gxUpdate As Boolean = True
        Public Property Index As Integer = 0
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
        Public Shared CollisionList As New Coliders
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public Property ColiderList As Coliders
            Get
                Return CollisionList
            End Get
            Set(ByVal value As Coliders)
                CollisionList = value
            End Set
        End Property
        Public Shared lstTileMaps As New Resource._TileMaps
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public Shared Property TileMaps As Resource._TileMaps
            Get
                Return lstTileMaps
            End Get
            Set(ByVal value As Resource._TileMaps)
                lstTileMaps = value
            End Set
        End Property
        Public MovementList As New Frames.Movements
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public Property EnitityMovement As Frames.Movements
            Get
                Return MovementList
            End Get
            Set(ByVal value As Frames.Movements)
                MovementList = value
            End Set
        End Property

        Public Sub New()
            UniqueId = Guid.NewGuid()
        End Sub

        Public Sub New(game As Game)
            Me.New()
            Me.Game = game
        End Sub
    End Class

    Public Class Layers
        Inherits List(Of Layer)
        Public Property Game As Game
        Public Shadows Sub Add(Layer As Layer)
            Layer.Game = Game
            Layer.Index = Me.Count - 1
            MyBase.Add(Layer)
        End Sub

        Public Sub New(newgame As Game)
            MyBase.New()
            Game = newgame
        End Sub
    End Class
End Namespace
