﻿Imports System.Windows.Forms

Public Class updateAvailable

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub updateAvailable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call updateAvailable_LoadManager()
    End Sub

    Friend Sub updateAvailable_LoadManager()
        'Get information about current version:
        currentVersion.Text = "Version: " + Application.ProductVersion.ToString()

        'Get information about latest release:

    End Sub
End Class
