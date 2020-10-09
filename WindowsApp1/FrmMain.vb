Imports System.IO
Imports WindowsApp1.Models

Public Class FrmMain

    Private _settings As Settings

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Settings laden
        _settings = Settings.Read("settings.json")
        ' Wenn settings nicht existieren eine neue erstellen
        If (_settings Is Nothing) Then
            _settings = New Settings
            Settings.Write("settings.json", _settings)
        End If

        Dim x = 0
        Dim y = 0
        Dim maxItems = CType((Width - 100) / 50, Integer)

        ' Schaut ob das Hintegrundbild da ist
        If Not String.IsNullOrEmpty(_settings.BackgroundImage) AndAlso File.Exists(_settings.BackgroundImage) Then
            BackgroundImage = Image.FromFile(_settings.BackgroundImage)
        End If

        ' Überprüft alle prozesse und erstellt einen Button dafür
        For Each process In _settings.Processes
            Dim button As New Button
            ' Hier wird alle 50 Pixel ein Button gesetzt
            button.Location = New Point(x * 50 + 50, y * 50 + 50)
            ' Die ProzessInfo Klasse wird an den Tag des Buttons gehangen damit wir es später wieder nutzen können
            button.Tag = process
            ' Die größe des Buttons
            button.Size = New Size(40, 40)

            ' Überprüft ob ein Icon für den Button existiert und weißt es diesem dann zu
            If Not String.IsNullOrEmpty(process.Icon) AndAlso File.Exists(process.Icon) Then
                button.BackgroundImage = Image.FromFile(process.Icon)
                button.BackgroundImageLayout = ImageLayout.Zoom
            End If
            ' Fügt es zu der Liste der Controls für die Form damit der Button auch angezeigt wird
            Controls.Add(button)
            ' Sorgt dafür das beim Button Click das OnButtonClick Event gerufen wird
            AddHandler button.Click, AddressOf OnButtonClick
            ' Zählt x hoch damit der nächste button nicht an der selben position erstellt wird
            x += 1
            ' Wenn x gleich die Maximalen Buttons pro Zeile erreicht hat dann wird x wieder auf 0 gesetzt
            ' damit der Button wieder vorne erscheint und y wird hochgezählt damit er in der nächsten zeile erstellt wird
            If (x = maxItems) Then
                x = 0
                y += 1
            End If
        Next
    End Sub

    Private Sub OnButtonClick(sender As Object, e As EventArgs)
        ' Prüft ob das objekt ein Button ist
        If (TypeOf sender Is Button) Then
            ' Convertiert das Object zu einem Button
            Dim btn = CType(sender, Button)
            ' Convertiert den Tag des Buttons zu einem ProcessInfo
            Dim processInfo = CType(btn.Tag, ProcessInfo)
            ' Hier wird der Process dann gestartet mit den Infos aus dem geklickten Button
            Dim processStartInfo As New ProcessStartInfo
            processStartInfo.FileName = processInfo.Path
            processStartInfo.Arguments = processInfo.Arguments
            Process.Start(processStartInfo)
        End If
    End Sub
End Class
