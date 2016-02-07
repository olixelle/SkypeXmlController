Public Class Form1

    Public WithEvents skype As Skype
    Public skypeWindow As SkypeWindow
    Public s2k As Skype2Kodi
    Public k2s As Kodi2Skype

    Protected baseDirectory As String
    Protected s2kDirectory As String
    Protected k2sDirectory As String

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SkypeWindow.releaseSkypeWindow()

    End Sub

    Public Sub initDirectories()

        Me.baseDirectory = IO.Path.Combine(Application.StartupPath, "var")
        If Not IO.Directory.Exists(Me.baseDirectory) Then
            IO.Directory.CreateDirectory(Me.baseDirectory)
        End If

        Me.s2kDirectory = IO.Path.Combine(Me.baseDirectory, "skype2kodi")
        If Not IO.Directory.Exists(Me.s2kDirectory) Then
            IO.Directory.CreateDirectory(Me.s2kDirectory)
        End If

        Me.k2sDirectory = IO.Path.Combine(Me.baseDirectory, "kodi2skype")
        If Not IO.Directory.Exists(Me.k2sDirectory) Then
            IO.Directory.CreateDirectory(Me.k2sDirectory)
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.initDirectories()

        Me.skype = New Skype(IO.Path.Combine(Me.baseDirectory, "img"))

        Me.skypeWindow = New SkypeWindow
        Me.s2k = New Skype2Kodi(skype, Me.s2kDirectory)
        Me.k2s = New Kodi2Skype(skype, Me.k2sDirectory)

        Me.skypeWindow.catchSkypeWindow(Me)

        Me.hydrateCallFriendMenu()

    End Sub

    ''' <summary>
    ''' Add call friend items in context menu
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub hydrateCallFriendMenu()
        Dim f As skypeFriend
        For Each f In Me.skype.getFriends
            Dim item As New ToolStripMenuItem()
            item.Text = f.label
            item.Tag = f.handle
            AddHandler item.Click, AddressOf Me.callContactHandler
            Me.CallContactToolStripMenuItem.DropDownItems.Add(item)
        Next
    End Sub

    Protected Sub callContactHandler(sender As Object, e As EventArgs)
        Me.k2s.createActionFile(Kodi2Skype.kMethodCallFriend, sender.tag)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)
        'Me.s2k.updateAll()
    End Sub


    Private Sub Form1_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        Me.skypeWindow.resizeSkypeWindow()
    End Sub

    Private Sub ReleaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToolStripMenuItem.Click
        Me.skypeWindow.releaseSkypeWindow()
    End Sub

    Private Sub CatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CatchToolStripMenuItem.Click
        Me.skypeWindow.catchSkypeWindow(Me)
    End Sub


    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

    End Sub

    Private Sub ShowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowToolStripMenuItem.Click
        Me.Show()
        Me.WindowState = FormWindowState.Maximized
        Me.skypeWindow.resizeSkypeWindow()
    End Sub

    Private Sub SimulateCallToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub AcceptCallToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AcceptCallToolStripMenuItem.Click
        Me.k2s.createActionFile(Kodi2Skype.kMethodCallAccept)
    End Sub

    Private Sub EndCallToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EndCallToolStripMenuItem.Click
        Me.k2s.createActionFile(Kodi2Skype.kMethodCallEnd)
    End Sub

    Private Sub skype_evtCallActive(friendHandle As String, friendName As String) Handles skype.evtCallActive
        Debug.WriteLine("Call is active with " & friendHandle & " / " & friendName)
        Me.skypeWindow.maximize()
    End Sub

    Private Sub skype_evtCallFinished(friendHandle As String, friendName As String) Handles skype.evtCallFinished
        Debug.WriteLine("Call finished with " & friendHandle & " / " & friendName)
        Me.skypeWindow.minimize()
    End Sub

    Private Sub skype_evtCallPending(friendHandle As String, friendName As String) Handles skype.evtCallPending
        Debug.WriteLine("Call is pending with " & friendHandle & " / " & friendName)
        Me.skypeWindow.maximize()
    End Sub
End Class
