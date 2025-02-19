﻿Imports System.IO
Imports System.Net
Imports System.Reflection

Public NotInheritable Class startsplash
    Public NewtonSoftJson As Object
    Dim unzipDLL As Boolean
    Private Sub startsplash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Bar1.Visible = False
        Label2.Visible = False

        If Not Directory.Exists("c:\Tfff1") Then
            Directory.CreateDirectory("c:\Tfff1")
        End If

        If Not Directory.Exists("c:\Tfff1\Simple_MC") Then
            Directory.CreateDirectory("c:\Tfff1\Simple_MC")
        End If

        If Not File.Exists("C:\Tfff1\Simple_MC\Backup_path.txt") Then
            File.Create("C:\Tfff1\Simple_MC\Backup_path.txt")
        End If

        If Not Directory.Exists("C:\Tfff1\Simple_MC\Mod_Collections") Then
            Directory.CreateDirectory("C:\Tfff1\Simple_MC\Mod_Collections")
        End If

        If File.Exists("C:\Tfff1\Simple_MC\Mod_Collections\collections.txt") Then
            File.Delete("C:\Tfff1\Simple_MC\Mod_Collections\collections.txt")
        End If

        If Not Directory.Exists("C:\Tfff1\Simple_MC\Mod_Collections_Export") Then
            My.Computer.FileSystem.CreateDirectory("C:\Tfff1\Simple_MC\Mod_Collections_Export")
        Else
            My.Computer.FileSystem.DeleteDirectory("C:\Tfff1\Simple_MC\Mod_Collections_Export", FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.CreateDirectory("C:\Tfff1\Simple_MC\Mod_Collections_Export")
        End If

        Try
            My.Computer.FileSystem.DeleteDirectory("C:\Tfff1\Simple_MC\Mod_Info_Finder\Mod_Data\", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch ex As Exception

        End Try

        'Scans Mod_Collections for folders - Overwrites collections.txt with the results.
        Call Collection_FolderScan(Me, "C:\Tfff1\Simple_MC\Mod_Collections", False, "C:\Tfff1\Simple_MC\Mod_Collections\collections.txt", True)


        '    If (Not System.IO.File.Exists("c:\Tfff1\Simple_MC\WorldBackup.exe")) Then
        '        My.Computer.Network.DownloadFile(
        '"http//drive.google.com / uc?export=download&id=0BzZlcNkakawtem5xZGdZRzlfR0E",
        '"C:\Tfff1\Simple_MC\WorldBackup.exe")
        '    Else

        'End If

        My.Settings.appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        My.Settings.Save()

        If Not My.Computer.FileSystem.DirectoryExists("C:\Tfff1\Simple_MC\Mod_Info_Finder") Then
            unzipDLL = True
            Bar1.Visible = True
            Label2.Visible = True
            Dim dllclient As WebClient = New WebClient
            AddHandler dllclient.DownloadFileCompleted, AddressOf Dll_DownloadCompleted
            AddHandler dllclient.DownloadProgressChanged, AddressOf client_ProgressChanged
            dllclient.DownloadFileAsync(New Uri("https://github.com/tfff1OFFICIAL/Simple-Mod-Installer-Master/raw/master/External%20Tools/Mod_Info_Finder/Mod_Info_Finder.zip"), "C:\Tfff1\Mod_Info_Finder.zip")
        Else
            unzipDLL = False
            Call Dll_DownloadCompleted(Me, Nothing)
        End If

    End Sub

    Private Sub client_ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())
        Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())
        Dim percentage As Double = bytesIn / totalBytes * 100
        Bar1.Value = Integer.Parse(Math.Truncate(percentage).ToString())
    End Sub

    'Private Sub client2_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
    Private Sub client2_DownloadCompleted()
        If unzipDLL = True Then
            My.Computer.FileSystem.CreateDirectory("C:\Tfff1\Simple_MC\Octokit")
            My.Computer.FileSystem.CreateDirectory("C:\Tfff1\Simple_MC\Temp")
            System.IO.Compression.ZipFile.ExtractToDirectory("C:\Tfff1\Simple_MC\Octokit.nupkg", "C:\Tfff1\Simple_MC\Temp")
            IO.File.Delete("C:\Tfff1\Simple_MC\Octokit.nupkg")
            unzipDLL = False
            System.IO.File.Copy("C:\Tfff1\Simple_MC\Temp\lib\net45\Octokit.dll", "C:\Tfff1\Simple_MC\Octokit\Octokit.dll", True)
            System.IO.File.Copy("C:\Tfff1\Simple_MC\Temp\lib\net45\Octokit.pdb", "C:\Tfff1\Simple_MC\Octokit\Octokit.pdb", True)
            System.IO.File.Copy("C:\Tfff1\Simple_MC\Temp\lib\net45\Octokit.XML", "C:\Tfff1\Simple_MC\Octokit\Octokit.XML", True)
            My.Computer.FileSystem.DeleteDirectory("C:\Tfff1\Simple_MC\Temp", FileIO.DeleteDirectoryOption.DeleteAllContents)
            'loadOctokit()
        End If

        If (Not System.IO.File.Exists("c:\Tfff1\Simple_MC\WorldBackup.exe")) And My.Computer.Network.IsAvailable Then
            Bar1.Visible = True
            Label2.Visible = True
            Dim client As WebClient = New WebClient
            AddHandler client.DownloadProgressChanged, AddressOf client_ProgressChanged
            AddHandler client.DownloadFileCompleted, AddressOf client_DownloadCompleted
            client.DownloadFileAsync(New Uri("http://drive.google.com/uc?export=download&id=0BzZlcNkakawtem5xZGdZRzlfR0E"), "C:\Tfff1\Simple_MC\WorldBackup.exe")
        End If
    End Sub

    Private Sub Dll_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        If unzipDLL = True Then
            System.IO.Compression.ZipFile.ExtractToDirectory("C:\Tfff1\Mod_Info_Finder.zip", "C:\Tfff1\Simple_MC")
            IO.File.Delete("C:\Tfff1\Mod_Info_Finder.zip")
            unzipDLL = False
        End If

        ''Check for octokit: - used for version checking.
        'If Not My.Computer.FileSystem.FileExists("C:\Tfff1\Simple_MC\Octokit\Octokit.dll") Then
        '    unzipDLL = True
        '    Bar1.Visible = True
        '    Label2.Visible = True
        '    Dim client2 As WebClient = New WebClient
        '    'AddHandler client2.DownloadProgressChanged, AddressOf client_ProgressChanged
        '    'AddHandler client2.DownloadFileCompleted, AddressOf client2_DownloadCompleted
        '    client2.DownloadFile(New Uri("https://www.nuget.org/api/v2/package/Octokit/0.20.0"), "C:\Tfff1\Simple_MC\Octokit.nupkg")
        'End If
        Call client2_DownloadCompleted()
    End Sub

    Private Sub client_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        Bar1.Value = 100
        MessageBox.Show("Additional Features Downloaded & Installed Successfully!")
    End Sub

    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).
End Class
