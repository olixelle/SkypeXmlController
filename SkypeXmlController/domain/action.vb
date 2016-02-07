﻿Public Class action

    Public method As String
    Public param As String


    Public Shared Function createActionFromXml(path) As action
        Dim action As New action

        Dim doc As New Xml.XmlDocument
        doc.Load(path)

        action.method = doc.SelectSingleNode(".//method").InnerText
        action.param = doc.SelectSingleNode(".//param").InnerText

        Return action
    End Function

    ''' <summary>
    ''' Create an action file
    ''' </summary>
    ''' <param name="method"></param>
    ''' <param name="param"></param>
    ''' <remarks></remarks>
    Public Shared Sub createActionFile(filePath As String, method As String, Optional param As String = "")

        Dim xmlDoc As Xml.XmlWriter = Xml.XmlWriter.Create(filePath)
        xmlDoc.WriteStartDocument()
        xmlDoc.WriteStartElement("action")

        xmlDoc.WriteElementString("method", method)
        xmlDoc.WriteElementString("param", param)

        xmlDoc.WriteEndElement()
        xmlDoc.WriteEndDocument()
        xmlDoc.Close()
        xmlDoc.Dispose()

    End Sub

End Class
