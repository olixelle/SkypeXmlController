Imports System.Configuration

Public Class Skype

    Protected WithEvents oskype As SKYPE4COMLib.Skype

    Public imageDirectory As String

    Public Event evtCallIncoming(friendHandle As String, friendName As String, friendAvatar As String)
    Public Event evtCallOutgoing(friendHandle As String, friendName As String, friendAvatar As String)
    Public Event evtCallActive(friendHandle As String, friendName As String, friendAvatar As String)
    Public Event evtCallFinished(friendHandle As String, friendName As String, friendAvatar As String)

    Protected currentCall As SKYPE4COMLib.Call

    Protected skypePath As String

    Protected isOutgoingCall = False

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="eImageDirectory">Image directory for avatars</param>
    ''' <remarks></remarks>
    Public Sub New(eImageDirectory As String, eSkypePath As String)

        Me.skypePath = eSkypePath
        Tool.log("Skype path is " & Me.skypePath)

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
            If ofriend.OnLineStatus <> Me.oskype.Convert.TextToUserStatus("OFFLINE") Then
                Dim obj As New skypeFriend
                obj.handle = ofriend.handle
                obj.label = ofriend.fullName
                If (obj.label = "") Then
                    obj.label = obj.handle
                End If
                obj.avatar = Me.getFriendPicture(obj.handle)

                If obj.handle <> "echo123" Then
                    friends.Add(obj)
                End If
            End If
        Next
        friends.Sort()
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
            Tool.log("Download avatar to " & filePath)
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

        Tool.log("Call status event : " & Status)

        Select Case Status
            Case SKYPE4COMLib.TCallStatus.clsRinging, SKYPE4COMLib.TCallStatus.clsRouting
                If Me.isOutgoingCall Then
                    RaiseEvent evtCallOutgoing(pCall.PartnerHandle, pCall.PartnerDisplayName, Me.getFriendPicture(pCall.PartnerHandle))
                Else
                    RaiseEvent evtCallIncoming(pCall.PartnerHandle, pCall.PartnerDisplayName, Me.getFriendPicture(pCall.PartnerHandle))
                End If
            Case SKYPE4COMLib.TCallStatus.clsInProgress '5
                RaiseEvent evtCallActive(pCall.PartnerHandle, pCall.PartnerDisplayName, Me.getFriendPicture(pCall.PartnerHandle))
            Case SKYPE4COMLib.TCallStatus.clsEarlyMedia
            Case SKYPE4COMLib.TCallStatus.clsLocalHold
            Case SKYPE4COMLib.TCallStatus.clsOnHold
            Case SKYPE4COMLib.TCallStatus.clsRemoteHold
            Case SKYPE4COMLib.TCallStatus.clsTransferred
            Case SKYPE4COMLib.TCallStatus.clsTransferring
            Case SKYPE4COMLib.TCallStatus.clsUnknown
            Case SKYPE4COMLib.TCallStatus.clsUnplaced
                'nothing
            Case SKYPE4COMLib.TCallStatus.clsCancelled  '11
                RaiseEvent evtCallFinished(pCall.PartnerHandle, pCall.PartnerDisplayName, Me.getFriendPicture(pCall.PartnerHandle))
                Me.isOutgoingCall = False
            Case SKYPE4COMLib.TCallStatus.clsBusy
                RaiseEvent evtCallFinished(pCall.PartnerHandle, pCall.PartnerDisplayName, Me.getFriendPicture(pCall.PartnerHandle))
                Me.isOutgoingCall = False
            Case SKYPE4COMLib.TCallStatus.clsRefused    '9
                RaiseEvent evtCallFinished(pCall.PartnerHandle, pCall.PartnerDisplayName, Me.getFriendPicture(pCall.PartnerHandle))
                Me.isOutgoingCall = False
            Case SKYPE4COMLib.TCallStatus.clsFailed
                RaiseEvent evtCallFinished(pCall.PartnerHandle, pCall.PartnerDisplayName, Me.getFriendPicture(pCall.PartnerHandle))
                Me.isOutgoingCall = False
            Case SKYPE4COMLib.TCallStatus.clsMissed
                RaiseEvent evtCallFinished(pCall.PartnerHandle, pCall.PartnerDisplayName, Me.getFriendPicture(pCall.PartnerHandle))
                Me.isOutgoingCall = False
            Case SKYPE4COMLib.TCallStatus.clsFinished   '7
                RaiseEvent evtCallFinished(pCall.PartnerHandle, pCall.PartnerDisplayName, Me.getFriendPicture(pCall.PartnerHandle))
                Me.isOutgoingCall = False
        End Select
    End Sub

    Public Sub CallFriend(ByVal friendHandle As String)
        'friendHandle = "axelle.prevost"

        Dim shellCommand As String = """" & Me.skypePath & """ /callto:" & friendHandle
        Tool.log("Execute shell cmd : " & shellCommand)
        Shell(shellCommand)

        isOutgoingCall = True

        'Dim i As Integer
        'Do While i < 50
        '    Me.currentCall = Me.oskype.PlaceCall(friendHandle)
        '    Tool.log("Call statut = " & Me.currentCall.Status & " : " & Me.currentCall.FailureReason)
        '    Threading.Thread.Sleep(500)
        '    i += 1
        'Loop
        'Tool.log("Call attempt - statut = " & Me.currentCall.Status)

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
        'Tool.log("Skype command = " & pCommand.Command)
    End Sub

    Private Sub oskype_Error(pCommand As SKYPE4COMLib.Command, Number As Integer, Description As String) Handles oskype.Error
        Tool.log("Skype error = " & pCommand.Command & " : " & Description)
    End Sub
End Class
