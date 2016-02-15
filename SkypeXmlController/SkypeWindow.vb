Imports System.Runtime.InteropServices

Public Class SkypeWindow

    Protected skypeWindowHandle As IntPtr = IntPtr.Zero
    Protected formContainer As Form

    Private Const WM_SYSCOMMAND As Integer = 274
    Private Const SC_MAXIMIZE As Integer = 61488
    Private Const SC_RESTORE As Long = &HF120&
    Private Const WM_SETTEXT As Integer = &HC

    Declare Auto Function SetParent Lib "user32.dll" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As Integer
    Declare Auto Function GetParent Lib "user32.dll" (ByVal hWndChild As IntPtr) As IntPtr
    Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Declare Auto Function FindWindow Lib "user32.dll" (ByVal strClassName As String, ByVal strWindowName As String) As Integer
    Declare Auto Function SetActiveWindow Lib "user32.dll" (ByVal hwnd As Integer) As Integer

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="form"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function catchSkypeWindow(ByRef form As Form) As Boolean

        Dim handle As String = getSkypeWindowHandle()

        Me.formContainer = form

        Dim result As Integer

        result = SendMessage(handle, WM_SYSCOMMAND, SC_RESTORE, 0)
        Tool.log("Restore skype window with handle #" & handle & ". Result is :" & result)

        result = SetParent(handle, formContainer.Handle)
        Tool.log("Catch skype window with handle #" & handle & ". Result is :" & result)

        Threading.Thread.Sleep(1000)

        result = SendMessage(handle, WM_SYSCOMMAND, SC_MAXIMIZE, 0)
        Tool.log("Maximize skype window with handle #" & handle & ". Result is :" & result)


        Return True
    End Function

    ''' <summary>
    ''' Resize skype window
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function resizeSkypeWindow() As Boolean
        Dim handle As String = getSkypeWindowHandle()

        Dim result As Integer

        result = SendMessage(handle, WM_SYSCOMMAND, SC_RESTORE, 0)
        Tool.log("Restore skype window with handle #" & handle & ". Result is :" & result)

        result = SendMessage(handle, WM_SYSCOMMAND, SC_MAXIMIZE, 0)
        Tool.log("resize skype window with handle #" & handle & ". Result is :" & result)

        Return True
    End Function

    ''' <summary>
    ''' Release skype window
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function releaseSkypeWindow() As Boolean
        Dim handle As String = getSkypeWindowHandle()

        Dim result As Integer

        result = SendMessage(handle, WM_SYSCOMMAND, SC_RESTORE, 0)
        Tool.log("Restore skype window with handle #" & handle & ". Result is :" & result)

        result = SetParent(handle, IntPtr.Zero)
        Tool.log("release skype window with handle #" & handle & ". Result is :" & result)

        skypeWindowHandle = IntPtr.Zero

        Return True
    End Function

    ''' <summary>
    ''' Return skype window handle
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function getSkypeWindowHandle() As System.IntPtr

        If skypeWindowHandle = IntPtr.Zero Then
            Dim p As System.Diagnostics.Process
            For Each p In System.Diagnostics.Process.GetProcesses
                If p.ProcessName = "Skype" Then
                    Dim handle As IntPtr = p.MainWindowHandle
                    If handle = 0 Then
                        Throw New System.Exception("Skype main window handle is null")
                    End If
                    skypeWindowHandle = handle
                End If
            Next
            If skypeWindowHandle = IntPtr.Zero Then
                Throw New System.Exception("Unable to find skype window")
            End If
        End If

        Return skypeWindowHandle

    End Function

    Public Sub maximize()
        
        Me.formContainer.WindowState = FormWindowState.Maximized
        Me.resizeSkypeWindow()
        Me.sendFullscreenCmd()
        Me.formContainer.Show()
        Me.formContainer.BringToFront()
        Me.formContainer.Focus()

        SetActiveWindow(Me.formContainer.Handle)

    End Sub

    Public Sub minimize()
        Me.formContainer.Hide()
        Tool.log("Minimize skype window")
    End Sub

    Public Sub sendFullscreenCmd()

    End Sub


End Class
