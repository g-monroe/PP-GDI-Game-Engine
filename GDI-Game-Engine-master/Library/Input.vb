Imports System.Runtime.InteropServices

Public Class Input
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
End Class
