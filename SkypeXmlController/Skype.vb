Public Class Skype

    Protected WithEvents oskype As SKYPE4COMLib.Skype

    Public imageDirectory As String

    Public Event evtCallPending(friendHandle As String, friendName As String)
    Public Event evtCallActive(friendHandle As String, friendName As String)
    Public Event evtCallFinished(friendHandle As String, friendName As String)

    Protected currentCall As SKYPE4COMLib.Call

    Protected skypePath As String = "C:\Program Files (x86)\Skype\Phone\Skype.exe"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="eImageDirectory">Image directory for avatars</param>
    ''' <remarks></remarks>
    Public Sub New(eImageDirectory)

        'init skype OBJ
        Me.oskype = New SKYPE4COMLib.Skype
        If Not Me.oskype.Client.IsRunning Then
            Me.oskype.Client.Start(True, True)
        End If
        Me.oskype.Attach(Me.oskype.Protocol, True)

        Me.imageDirectory = eImageDirectory
        If Not IO.Directory.Exists(Me.imageDirectory) Then
            IO.Directory.CreateDirectory(Me.imageDirectory)
        End If

    End Sub

    Public Function getFriends() As ArrayList
        Dim friends As New ArrayList

        For Each ofriend In Me.oskype.Friends
            If ofriend.OnLineStatus = Me.oskype.Convert.TextToUserStatus("ONLINE") Then
                Dim obj As New skypeFriend
                obj.label = ofriend.fullName
                obj.handle = ofriend.handle
                obj.avatar = Me.getFriendPicture(obj.handle)

                friends.Add(obj)
            End If
        Next

        Return friends
    End Function

    ''' <summary>
    ''' Return friend picture path
    ''' </summary>
    ''' <param name="friendHandle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getFriendPicture(friendHandle As String) As String


        Dim filePath As String = System.IO.Path.Combine(Me.imageDirectory, friendHandle.Replace(":", "_") & ".jpg")

        If Not IO.File.Exists(filePath) Then
            Dim cmd As New SKYPE4COMLib.Command
            cmd.Command = "GET USER " & friendHandle & " AVATAR 1 " & filePath
            Me.oskype.SendCommand(cmd)
            Debug.WriteLine("Download avatar to " & filePath)
        End If

        Return filePath
    End Function

    ''' <summary>
    ''' Return current user details
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getCurrentUser()
        Dim user As New user

        user.handle = Me.oskype.CurrentUserHandle
        user.name = Me.oskype.CurrentUserProfile.FullName
        user.mood = Me.oskype.CurrentUserProfile.MoodText
        user.avatar = Me.getFriendPicture(user.handle)

        Return user
    End Function

    Public Function getMissedCalls() As ArrayList
        Dim missedCalls As New ArrayList

        Dim item As Object
        For Each item In Me.oskype.MissedCalls
            Dim mc As New missedCall
            mc.date = item.Timestamp
            mc.friendHandle = item.PartnerHandle
            mc.friendName = item.PartnerDisplayName
            missedCalls.Add(mc)
        Next

        Return missedCalls
    End Function

    ''' <summary>
    ''' Raised when call status changes
    ''' </summary>
    ''' <param name="pCall"></param>
    ''' <param name="Status"></param>
    ''' <remarks></remarks>
    Private Sub oskype_CallStatus(pCall As SKYPE4COMLib.Call, Status As SKYPE4COMLib.TCallStatus) Handles oskype.CallStatus

        Debug.WriteLine("Call status event : " & Status)

        Select Case Status
            Case SKYPE4COMLib.TCallStatus.clsRouting, SKYPE4COMLib.TCallStatus.clsRinging
                RaiseEvent evtCallPending(pCall.PartnerHandle, pCall.PartnerDisplayName)
            Case SKYPE4COMLib.TCallStatus.clsInProgress '5
                RaiseEvent evtCallActive(pCall.PartnerHandle, pCall.PartnerDisplayName)
            Case SKYPE4COMLib.TCallStatus.clsEarlyMedia
            Case SKYPE4COMLib.TCallStatus.clsLocalHold
            Case SKYPE4COMLib.TCallStatus.clsOnHold
            Case SKYPE4COMLib.TCallStatus.clsRemoteHold
            Case SKYPE4COMLib.TCallStatus.clsTransferred
            Case SKYPE4COMLib.TCallStatus.clsTransferring
            Case SKYPE4COMLib.TCallStatus.clsUnknown
            Case SKYPE4COMLib.TCallStatus.clsUnplaced
                '???
            Case SKYPE4COMLib.TCallStatus.clsMissed, SKYPE4COMLib.TCallStatus.clsBusy, SKYPE4COMLib.TCallStatus.clsRefused, SKYPE4COMLib.TCallStatus.clsCancelled, SKYPE4COMLib.TCallStatus.clsFailed, SKYPE4COMLib.TCallStatus.clsFinished   '7
                'call stopped
                RaiseEvent evtCallFinished(pCall.PartnerHandle, pCall.PartnerDisplayName)
        End Select
    End Sub

    Public Sub CallFriend(ByVal friendHandle As String)
        friendHandle = "axelle.prevost"

        Dim shellCommand As String = """" & Me.skypePath & """ /callto:" & friendHandle
        Shell(shellCommand)

        'Dim i As Integer
        'Do While i < 50
        '    Me.currentCall = Me.oskype.PlaceCall(friendHandle)
        '    Debug.WriteLine("Call statut = " & Me.currentCall.Status & " : " & Me.currentCall.FailureReason)
        '    Threading.Thread.Sleep(500)
        '    i += 1
        'Loop
        'Debug.WriteLine("Call attempt - statut = " & Me.currentCall.Status)

    End Sub

    Public Sub endCall()
        Me.oskype.Client.ButtonPressed("NO")
        Me.oskype.Client.ButtonReleased("NO")
    End Sub

    Public Sub acceptCall()
        Me.oskype.Client.ButtonPressed("YES")
        Me.oskype.Client.ButtonReleased("YES")
    End Sub

    Private Sub oskype_Command(pCommand As SKYPE4COMLib.Command) Handles oskype.Command
        Debug.WriteLine("Skype command = " & pCommand.Command)
    End Sub

    Private Sub oskype_Error(pCommand As SKYPE4COMLib.Command, Number As Integer, Description As String) Handles oskype.Error
        Debug.WriteLine("Skype error = " & pCommand.Command & " : " & Description)
    End Sub
End Class
