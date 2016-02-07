Imports System.IO

Public Class Kodi2Skype

    Protected actionsDirectory As String

    Public Const kMethodChangeStatus = "change_status"
    Public Const kMethodCallFriend = "call_friend"
    Public Const kMethodCallAccept = "call_accept"
    Public Const kMethodCallEnd = "call_end"

    Protected skype As Skype

    Protected currentCall As SKYPE4COMLib.Call

    Protected WithEvents watchfolder As FileSystemWatcher

    Public Sub New(ByRef eSkype As Skype, ByVal eActionDirectory As String)
        Me.actionsDirectory = eActionDirectory
        Me.skype = eSkype

        Me.watchfolder = New FileSystemWatcher(Me.actionsDirectory)
        watchfolder.EnableRaisingEvents = True

    End Sub

    ''' <summary>
    ''' New file in kodi2skype directory
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub watchfolder_Created(sender As Object, e As FileSystemEventArgs) Handles watchfolder.Created
        tool.WaitReady(e.FullPath)
        Dim action As action = action.createActionFromXml(e.FullPath)
        Me.executeAction(action)
        IO.File.Delete(e.FullPath)
    End Sub

    Public Sub createActionFile(method As String, Optional param As String = "")
        Dim filePath As String = IO.Path.Combine(Me.actionsDirectory, "action.xml")
        action.createActionFile(filePath, method, param)
    End Sub


    ''' <summary>
    ''' Execute the action
    ''' </summary>
    ''' <param name="action"></param>
    ''' <remarks></remarks>
    Public Sub executeAction(action As action)
        Debug.WriteLine("Execute action " & action.method & " with param " & action.param)
        Select Case action.method
            Case kMethodChangeStatus
            Case kMethodCallFriend
                Me.skype.CallFriend(action.param)
            Case kMethodCallAccept
                Me.skype.acceptCall()
            Case kMethodCallEnd
                Me.skype.endCall()
        End Select
    End Sub


End Class
