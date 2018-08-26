<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim Layer1 As VisualEngine.Engine.Layer = New VisualEngine.Engine.Layer()
        Dim Colider1 As VisualEngine.Engine.Colider = New VisualEngine.Engine.Colider()
        Dim Colider2 As VisualEngine.Engine.Colider = New VisualEngine.Engine.Colider()
        Dim Entity1 As VisualEngine.Engine.Entity = New VisualEngine.Engine.Entity()
        Dim Layer2 As VisualEngine.Engine.Layer = New VisualEngine.Engine.Layer()
        Dim Entity2 As VisualEngine.Engine.Entity = New VisualEngine.Engine.Entity()
        Dim Entity3 As VisualEngine.Engine.Entity = New VisualEngine.Engine.Entity()
        Me.Game1 = New VisualEngine.Engine.Game()
        CType(Me.Game1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Game1
        '
        Me.Game1.ClockSpeed = 17
        Me.Game1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Game1.FrameRateCap = 60
        Colider1.EntityIndex = 0
        Colider1.GravityColider = True
        Colider1.Name = "Unknown"
        Colider1.Rectangle = New System.Drawing.Rectangle(0, 30, 32, 2)
        Colider1.Relative = True
        Colider1.Toggle = True
        Colider2.EntityIndex = 1
        Colider2.GravityColider = False
        Colider2.Name = "Unknown"
        Colider2.Rectangle = New System.Drawing.Rectangle(0, 0, 32, 32)
        Colider2.Relative = True
        Colider2.Toggle = True
        Layer1.ColiderList.Add(Colider1)
        Layer1.ColiderList.Add(Colider2)
        Layer1.ColiderList.Add(Colider1)
        Layer1.ColiderList.Add(Colider2)
        Entity1.Background = True
        Entity1.BufferedGraphic = True
        Entity1.Color = System.Drawing.Color.Empty
        Entity1.Falling = False
        Entity1.Fixed = True
        Entity1.Gravity = False
        Entity1.Index = 0
        Entity1.Location = New System.Drawing.Point(0, 0)
        Entity1.Name = "Unknown"
        Entity1.ParentFrameIndex = 0
        Entity1.Player = False
        Entity1.Rectangle = New System.Drawing.Rectangle(0, 0, 0, 0)
        Entity1.Size = New System.Drawing.Size(32, 32)
        Entity1.Text = Nothing
        Entity1.TextFont = Nothing
        Entity1.Texture = Global.VisualEngine.My.Resources.Resources.Untitled
        Entity1.Tile = New System.Drawing.Rectangle(0, 0, 0, 0)
        Entity1.TileMapIndex = 0
        Entity1.TimeFall = 0
        Entity1.Toggled = True
        Entity1.Type = VisualEngine.Engine.Entity.Kind.Texture
        Entity1.UpdateFrames = True
        Entity1.UpdateTile = False
        Entity1.VelocityDown = New Decimal(New Integer() {0, 0, 0, 0})
        Entity1.VelocityLeft = New Decimal(New Integer() {0, 0, 0, 0})
        Entity1.VelocityRight = New Decimal(New Integer() {0, 0, 0, 0})
        Entity1.VelocityUp = New Decimal(New Integer() {0, 0, 0, 0})
        Layer1.EntityList.Add(Entity1)
        Layer1.Game = Me.Game1
        Layer1.gxUpdate = False
        Layer1.Index = -1
        Layer2.ColiderList.Add(Colider1)
        Layer2.ColiderList.Add(Colider2)
        Layer2.ColiderList.Add(Colider1)
        Layer2.ColiderList.Add(Colider2)
        Entity2.Background = False
        Entity2.BufferedGraphic = False
        Entity2.Color = System.Drawing.Color.Empty
        Entity2.Falling = False
        Entity2.Fixed = False
        Entity2.Gravity = True
        Entity2.Index = 0
        Entity2.Location = New System.Drawing.Point(0, 0)
        Entity2.Name = "Unknown"
        Entity2.ParentFrameIndex = 0
        Entity2.Player = True
        Entity2.Rectangle = New System.Drawing.Rectangle(0, 0, 0, 0)
        Entity2.Size = New System.Drawing.Size(32, 32)
        Entity2.Text = Nothing
        Entity2.TextFont = Nothing
        Entity2.Texture = Global.VisualEngine.My.Resources.Resources.pica2
        Entity2.Tile = New System.Drawing.Rectangle(0, 0, 0, 0)
        Entity2.TileMapIndex = 0
        Entity2.TimeFall = 0
        Entity2.Toggled = True
        Entity2.Type = VisualEngine.Engine.Entity.Kind.Texture
        Entity2.UpdateFrames = True
        Entity2.UpdateTile = False
        Entity2.VelocityDown = New Decimal(New Integer() {0, 0, 0, 0})
        Entity2.VelocityLeft = New Decimal(New Integer() {0, 0, 0, 0})
        Entity2.VelocityRight = New Decimal(New Integer() {0, 0, 0, 0})
        Entity2.VelocityUp = New Decimal(New Integer() {0, 0, 0, 0})
        Entity3.Background = False
        Entity3.BufferedGraphic = True
        Entity3.Color = System.Drawing.Color.Empty
        Entity3.Falling = False
        Entity3.Fixed = False
        Entity3.Gravity = False
        Entity3.Index = 1
        Entity3.Location = New System.Drawing.Point(0, 200)
        Entity3.Name = "Unknown"
        Entity3.ParentFrameIndex = 0
        Entity3.Player = False
        Entity3.Rectangle = New System.Drawing.Rectangle(0, 0, 0, 0)
        Entity3.Size = New System.Drawing.Size(32, 32)
        Entity3.Text = Nothing
        Entity3.TextFont = Nothing
        Entity3.Texture = Global.VisualEngine.My.Resources.Resources.cGrass
        Entity3.Tile = New System.Drawing.Rectangle(0, 0, 0, 0)
        Entity3.TileMapIndex = 0
        Entity3.TimeFall = 0
        Entity3.Toggled = True
        Entity3.Type = VisualEngine.Engine.Entity.Kind.Texture
        Entity3.UpdateFrames = True
        Entity3.UpdateTile = False
        Entity3.VelocityDown = New Decimal(New Integer() {0, 0, 0, 0})
        Entity3.VelocityLeft = New Decimal(New Integer() {0, 0, 0, 0})
        Entity3.VelocityRight = New Decimal(New Integer() {0, 0, 0, 0})
        Entity3.VelocityUp = New Decimal(New Integer() {0, 0, 0, 0})
        Layer2.EntityList.Add(Entity2)
        Layer2.EntityList.Add(Entity3)
        Layer2.Game = Me.Game1
        Layer2.gxUpdate = False
        Layer2.Index = 0
        Me.Game1.LayerList.Add(Layer1)
        Me.Game1.LayerList.Add(Layer2)
        Me.Game1.Location = New System.Drawing.Point(0, 0)
        Me.Game1.Name = "Game1"
        Me.Game1.ShowDebug = False
        Me.Game1.Size = New System.Drawing.Size(464, 321)
        Me.Game1.Start = False
        Me.Game1.TabIndex = 0
        Me.Game1.TabStop = False
        Me.Game1.VSync = True
        Me.Game1.Window = Me.Game1
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 321)
        Me.Controls.Add(Me.Game1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.Game1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Game1 As Engine.Game
End Class
