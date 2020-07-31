''Author: S M Hasibur Rahman
''Date: 31th July 2020
''Description: This Program will be able to open edit and save afda text file

Option Strict On
Imports System.IO ''importing library
Public Class txtEditorForm
    ''' <summary>
    ''' executes when "new" under "file" menu clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mnuFileNew_Click(sender As Object, e As EventArgs) Handles mnuFileNew.Click
        txtTextArea.Text = String.Empty
        lblStatus.Text = "New file started"



    End Sub


    ''' <summary>
    ''' executes when "Open" under "file" menu clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mnuFileOpen_Click(sender As Object, e As EventArgs) Handles mnuFileOpen.Click, tsbOpen.Click
        Dim inputStream As StreamReader

        If openDialog.ShowDialog() = DialogResult.OK Then
            inputStream = New StreamReader(openDialog.FileName)
            txtTextArea.Text = inputStream.ReadToEnd()
            inputStream.Close()

            lblStatus.Text = "Loaded file" & openDialog.FileName
        End If
    End Sub



    ''' <summary>
    ''' executes when "Save AS" under "file" menu clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mnuFileSaveAs_Click(sender As Object, e As EventArgs) Handles mnuFileSaveAs.Click
        Dim outputStream As StreamWriter

        If saveDialog.ShowDialog() = DialogResult.OK Then
            outputStream = New StreamWriter(saveDialog.FileName)
            outputStream.Write(txtTextArea.Text)
            outputStream.Close()

            lblStatus.Text = "Saved file" + saveDialog.FileName



        End If

    End Sub


    ''' <summary>
    ''' executes when "save" under "file" menu clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mnuFileSave_Click(sender As Object, e As EventArgs) Handles mnuFileSave.Click

        If openDialog.FileName = "" Then ''if file is unknown 
            Dim outputStream As StreamWriter

            If saveDialog.ShowDialog() = DialogResult.OK Then
                outputStream = New StreamWriter(saveDialog.FileName)
                outputStream.Write(txtTextArea.Text)
                outputStream.Close()

                lblStatus.Text = "Saved file" + saveDialog.FileName



            End If

        Else
            SaveFile(openDialog.FileName, txtTextArea.Text)


        End If


        lblStatus.Text = "Saved file" + saveDialog.FileName
    End Sub


    ''' <summary>
    ''' save file function
    ''' </summary>
    ''' <param name="filename"></param>
    ''' <param name="texttowrite"></param>
    Private Sub SaveFile(ByVal filename As String, ByVal texttowrite As String)
        Dim FS As New FileStream(filename, FileMode.Open, FileAccess.Write, FileShare.ReadWrite)
        Dim sw As New StreamWriter(FS)
        sw.Write(texttowrite)
        sw.Close()
        FS.Close()
    End Sub

    ''' <summary>
    ''' executes when "about" under "Help" menu clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mnuHelpAbout_Click(sender As Object, e As EventArgs) Handles mnuHelpAbout.Click
        Dim aboutModal As New aboutForm

        aboutModal.ShowDialog()
    End Sub


    ''' <summary>
    ''' executes when "copy" under "Edit" menu clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mnuEditCopy_Click(sender As Object, e As EventArgs) Handles mnuEditCopy.Click
        My.Computer.Clipboard.SetText(txtTextArea.SelectedText)
    End Sub

    ''' <summary>
    ''' executes when "cut" under "Edit" menu clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mnuEditCut_Click(sender As Object, e As EventArgs) Handles mnuEditCut.Click
        My.Computer.Clipboard.SetText(txtTextArea.SelectedText)
        txtTextArea.SelectedText = ""
    End Sub
    ''' <summary>
    ''' executes when "Paste" under "Edit" menu clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub mnuEditPaste_Click(sender As Object, e As EventArgs) Handles mnuEditPaste.Click
        txtTextArea.SelectedText = My.Computer.Clipboard.GetText
        My.Computer.Clipboard.GetText()
    End Sub




    Private Sub txtEditorForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim dialog As DialogResult
        If txtTextArea.Text <> "" Then


            If openDialog.FileName <> "" Then




                Dim inputStream As StreamReader
                inputStream = New StreamReader(openDialog.FileName)


                If String.Compare(txtTextArea.Text.ToString, inputStream.ReadToEnd().ToString, True) = 0 Then ''comparing the the text area text with the file
                    inputStream.Close()
                    Application.Exit()



                Else
                    inputStream.Close()
                    dialog = MessageBox.Show("Do you want to save the file?", "Exit", MessageBoxButtons.YesNo)

                    If dialog = DialogResult.No Then
                        Application.Exit()

                    Else

                        SaveFile(openDialog.FileName, txtTextArea.Text)


                        lblStatus.Text = "Saved file" + saveDialog.FileName



                    End If

                End If


            Else

                dialog = MessageBox.Show("Do you want to save the file?", "Exit", MessageBoxButtons.YesNo)

                If dialog = DialogResult.Yes Then

                    Dim outputStream As StreamWriter

                    If saveDialog.ShowDialog() = DialogResult.OK Then
                        outputStream = New StreamWriter(saveDialog.FileName)
                        outputStream.Write(txtTextArea.Text)
                        outputStream.Close()

                        lblStatus.Text = "Saved file" + saveDialog.FileName

                    End If


                End If

            End If


        Else


            Application.Exit()


        End If






    End Sub
End Class
