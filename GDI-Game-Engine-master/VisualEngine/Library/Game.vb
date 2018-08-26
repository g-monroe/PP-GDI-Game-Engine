Imports System.ComponentModel
Imports System.Threading
Imports VisualEngine.Engine.Layers
Imports VisualEngine.Engine
Imports VisualEngine.Engine.Layer
Namespace Engine
    Public Class Game
        Inherits PictureBox
        Public g As Graphics

        'Buffered Graphics
        Private context As BufferedGraphicsContext
        Private grafx As BufferedGraphics
        Private bufferingMode As Byte
        Public Property Window As PictureBox = Me
        Public Property Start As Boolean = False
        'Engine-Level Property
        Public lstLayer As New Layers(Me)
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public Property LayerList As Layers
            Get
                Return lstLayer
            End Get
            Set(ByVal value As Layers)
                lstLayer = value
            End Set
        End Property

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or
                     ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or
                     ControlStyles.SupportsTransparentBackColor, True)
            InitializeBufferedGaphics(grafx, context, bufferingMode)
        End Sub
        Private gxUpdate As Boolean
        Sub InitializeBufferedGaphics(ByRef gx As BufferedGraphics, ByRef gxBuffer As BufferedGraphicsContext, ByRef gxByte As Byte)
            bufferingMode = 2
            context = BufferedGraphicsManager.Current
            context.MaximumBuffer = New Size(Me.Width + 1, Me.Height + 1)
            grafx = context.Allocate(Me.CreateGraphics(),
           New Rectangle(0, 0, Me.Width, Me.Height))

        End Sub
#Region "OnPaint"
        Private Sub Game_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

            grafx.Render(e.Graphics)
            Render.DrawLayers(LayerList, e.Graphics, grafx)
            If ShowDebug Then
                Render.DrawFPS(e.Graphics, Me)
                Render.DrawColiders(e.Graphics, Me)
            End If
        End Sub
        Protected Overrides Sub OnResize(e As EventArgs)
            ' Re-create the graphics buffer for a new window size.
            context.MaximumBuffer = New Size(Me.Width + 1, Me.Height + 1)
            If (grafx IsNot Nothing) Then
                grafx.Dispose()
                grafx = Nothing
            End If
            grafx = context.Allocate(Me.CreateGraphics(), New Rectangle(0, 0, Me.Width, Me.Height))

            ' Cause the background to be cleared and redraw.
            For Each layer As Layer In LayerList
                layer.gxUpdate = True
            Next
            Me.Refresh()
        End Sub
#End Region
#Region "Debug"
#Region "Core"
        Public Property ShowDebug As Boolean = True
        Private Delegate Sub InvokeDebug()
        Private Sub nDebug()
            fpsTick()
        End Sub
#End Region

#Region "FPS"
        Public Shared FrameRate As Integer = 60
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
            'Try
            'Redraw the control.
            If Window.Image IsNot Nothing Then : Window.Image.Dispose() : End If
            Window.Invalidate()
            ' Update Delta
            CalcDeltaTime()
            TotalTime += DeltaTime
            For Each curLayer As Layer In LayerList
                For Each e As Entity In curLayer.EntityList
                    '⌦Animate
                    Globals.UpdateFrames(e, curLayer)
                    '⌦Move Frame Entities
                    Globals.MoveFrame(e, curLayer)
                    '⌦Move Player
                    Input.Keyboard.Update()
                    Globals.KeysDown(e, Me)
                    If Globals.kdA Then
                        If e.Player And e.Toggled Then
                            e.VelocityLeft = 1
                        End If
                    End If
                    If Globals.kdD Then
                        If e.Player And e.Toggled Then
                            e.VelocityRight = 1
                        End If
                    End If
                    '⌦Gravity
                    Globals.Gravity(e, curLayer)

                    '⌦MoveEntities
                    Globals.MoveEntities(e, curLayer)

                Next
            Next



            '⌦Debug
            nDebug()
            '  Catch e As Exception
            '  Debug.Print(e.ToString)
            ' End Try
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
                    Window.Invoke(New Action(AddressOf tick))


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
End Namespace
