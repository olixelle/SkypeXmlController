Imports System.IO

Public Class Tool

    Public Shared Function WaitReady(fileName As String) As Boolean
        Dim i As Integer = 0
        Do While True

            Try
                Dim s As Stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)
                If (Not (s Is Nothing)) Then
                    s.Close()
                    Return True
                End If
            Catch ex As IOException
                'nothing
            End Try

            Threading.Thread.Sleep(500)
            i += 1
        Loop

        Return True
    End Function

    Public Shared Sub log(txt As String)
        Try
            Dim logPath As String = Path.Combine(Application.StartupPath, "log.txt")
            Dim objWriter As New System.IO.StreamWriter(logPath, True)
            txt = System.DateTime.Now.ToString("HH:mm:ss") & " - " & txt
            Debug.WriteLine(txt)
            objWriter.WriteLine(txt)
            objWriter.Close()
        Catch ex As Exception
            'nothing
        End Try

    End Sub

End Class
