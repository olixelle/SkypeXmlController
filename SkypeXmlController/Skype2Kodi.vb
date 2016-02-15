Public Class Skype2Kodi


    Protected baseDirectory As String

    Protected skype As Skype

    Public Sub New(ByRef eSkype As Skype, ByVal eBaseDirectory As String)

        Me.skype = eSkype

        Me.baseDirectory = eBaseDirectory

        Me.updateAll()

    End Sub

    Public Sub updateAll()
        Me.generateFriendsFile()
        Me.generateProfileFile()
        Me.generateMissedCallFile()
    End Sub

    ''' <summary>
    ''' Create xml file with all contacts
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub generateFriendsFile()

        Dim StartTime As DateTime = DateTime.Now

        Dim filePath As String = IO.Path.Combine(Me.baseDirectory, "friends.xml")
        Tool.WaitReady(filePath)
        Dim xmlDoc As Xml.XmlWriter = Xml.XmlWriter.Create(filePath)
        xmlDoc.WriteStartDocument()
        xmlDoc.WriteStartElement("friends")

        Dim buffer As String = ""
        Dim f As skypeFriend

        For Each f In Me.skype.getFriends

            xmlDoc.WriteStartElement("friend")

            xmlDoc.WriteElementString("handle", f.handle)
            xmlDoc.WriteElementString("name", f.label)
            xmlDoc.WriteElementString("avatar", f.avatar)

            xmlDoc.WriteEndElement()
        Next

        'write file
        xmlDoc.WriteEndElement()
        xmlDoc.WriteEndDocument()
        xmlDoc.Close()

        Dim EndTime As DateTime = DateTime.Now
        Dim TotalTime As TimeSpan = EndTime - StartTime
        Tool.log("Friends file generated in " & TotalTime.ToString("ss") & " seconds")
    End Sub

    ''' <summary>
    ''' Create xml file with current user profile information
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub generateProfileFile()

        Dim filePath As String = IO.Path.Combine(Me.baseDirectory, "profile.xml")
        Dim xmlDoc As Xml.XmlWriter = Xml.XmlWriter.Create(filePath)
        xmlDoc.WriteStartDocument()
        xmlDoc.WriteStartElement("profile")

        Dim user As user = Me.skype.getCurrentUser()

        xmlDoc.WriteElementString("handle", user.handle)
        xmlDoc.WriteElementString("name", user.name)
        xmlDoc.WriteElementString("mood", user.mood)
        xmlDoc.WriteElementString("avatar", user.avatar)

        'write file
        xmlDoc.WriteEndElement()
        xmlDoc.WriteEndDocument()
        xmlDoc.Close()

        Tool.log("Profile file generated")
    End Sub

    ''' <summary>
    ''' Create xml file with missed calls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub generateMissedCallFile()
        Dim filePath As String = IO.Path.Combine(Me.baseDirectory, "missed_calls.xml")
        Dim xmlDoc As Xml.XmlWriter = Xml.XmlWriter.Create(filePath)
        xmlDoc.WriteStartDocument()
        xmlDoc.WriteStartElement("missed_calls")

        Dim item As missedCall
        For Each item In Me.skype.getMissedCalls
            xmlDoc.WriteStartElement("call")
            xmlDoc.WriteElementString("date", item.date)
            xmlDoc.WriteElementString("handle", item.friendHandle)
            xmlDoc.WriteElementString("name", item.friendName)
            xmlDoc.WriteEndElement()
        Next

        xmlDoc.WriteEndElement()
        xmlDoc.WriteEndDocument()
        xmlDoc.Close()

        Tool.log("Missed call file generated")
    End Sub


    

    Public Sub generateCallFile(status As String, partnerHandle As String, partnerName As String, friendAvatar As String)
        Dim filePath As String = IO.Path.Combine(Me.baseDirectory, "call.xml")

        Dim xmlDoc As Xml.XmlWriter = Xml.XmlWriter.Create(filePath)
        xmlDoc.WriteStartDocument()
        xmlDoc.WriteStartElement("call")

        xmlDoc.WriteElementString("status", status)
        xmlDoc.WriteElementString("handle", partnerHandle)
        xmlDoc.WriteElementString("name", partnerName)
        xmlDoc.WriteElementString("avatar", friendAvatar)

        xmlDoc.WriteEndElement()
        xmlDoc.WriteEndDocument()
        xmlDoc.Close()

        Tool.log("Call file generated for status " & status & " and friend " & partnerHandle)
    End Sub

End Class
