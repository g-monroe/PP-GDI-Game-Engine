Imports System.ComponentModel
Imports VisualEngine.Engine
Namespace Engine
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
        Public Property Background As Boolean = False
        Public Property BufferedGraphic As Boolean = True
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
        Public lstParentFrame As New Engine.Frames.ParentFrames
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public Property FrameParentList As Frames.ParentFrames
            Get
                Return lstParentFrame
            End Get
            Set(ByVal value As Frames.ParentFrames)
                lstParentFrame = value
            End Set
        End Property
        Public lstChildFrame As New Frames.ChildFrames
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public Property FrameChildList As Frames.ChildFrames
            Get
                Return lstChildFrame
            End Get
            Set(ByVal value As Frames.ChildFrames)
                lstChildFrame = value
            End Set
        End Property

    End Class
#End Region

#End Region

End Namespace
