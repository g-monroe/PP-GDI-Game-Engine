Imports System.ComponentModel

Namespace Engine
    Public Class Frames
        Public Property Layer As Layer
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
#Region "Movement"
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

#End Region
#End Region
    End Class
End Namespace

