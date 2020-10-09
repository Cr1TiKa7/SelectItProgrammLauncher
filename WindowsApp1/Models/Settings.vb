Imports System.IO
Imports Newtonsoft.Json

Namespace Models

    Public Class Settings
        Public Property BackgroundImage As String
        Public Property Processes As List(Of ProcessInfo) = New List(Of ProcessInfo)

        ''' <summary>
        ''' Liest die settings datei aus
        ''' </summary>
        ''' <param name="filePath">Pfad der datei</param>
        ''' <returns></returns>
        Public Shared Function Read(filePath As String)
            If File.Exists(filePath) Then
                Using streamReader As New StreamReader(filePath)
                    Return JsonConvert.DeserializeObject(Of Settings)(streamReader.ReadToEnd())
                End Using
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Schreibt die settings in eine Datei
        ''' </summary>
        ''' <param name="filePath">Pfad der Datei</param>
        ''' <param name="settings">Das Settings Objekt was gespeichert werden soll</param>
        Public Shared Sub Write(filePath As String, settings As Settings)
            Using streamWriter As New StreamWriter(filePath)
                streamWriter.Write(JsonConvert.SerializeObject(settings))
            End Using
        End Sub
    End Class

End Namespace