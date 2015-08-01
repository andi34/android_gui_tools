Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If RadioButton1.Checked = True Then
            Shell("adb.exe wait-for-device")
            Shell("adb.exe reboot")
        ElseIf RadioButton2.Checked = True Then
            Shell("adb.exe wait-for-device")
            Shell("adb.exe reboot recovery")
        ElseIf RadioButton3.Checked = True Then
            Shell("adb.exe wait-for-device")
            Shell("adb.exe reboot download")
        End If

    End Sub


    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If CheckBoxShell.Checked = False Then
            Shell("""adb.exe"" " & TextBoxShell.Text)
        ElseIf CheckBoxShell.Checked = True Then
            Shell("""adb.exe shell"" " & TextBoxShell.Text)
        End If

    End Sub


    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Shell("adb.exe remount")
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Shell("adb.exe start-server")
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Shell("adb.exe kill-server")
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Shell("adb.exe kill-server")
        Shell("adb.exe start-server")
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If CheckBox2.Checked = True Then
            Shell("adb shell mkdir " & TextBox5.Text & "/" & "fb2png")
            Shell("adb shell mkdir " & TextBox5.Text & "/" & "fb2png/tool")
            Shell("adb shell rm -rf " & TextBox5.Text & "/" & "fb2png/screens")
            Shell("adb shell mkdir " & TextBox5.Text & "/" & "fb2png/screens")
            Shell("adb push fb2png " & TextBox5.Text & "/" & "fb2png/tool/")
            Shell("adb shell chmod 755 " & TextBox5.Text & "/" & "fb2png/tool/fb2png")
            Shell("adb shell " & TextBox5.Text & "/" & "fb2png/tool/fb2png" & " " & TextBox5.Text & "/" & "fb2png/screens/fbdump.png")
            Shell("adb pull " & TextBox5.Text & "/" & "fb2png/screens/fbdump.png screenshots")
            Shell("adb shell rm " & TextBox5.Text & "/" & "fb2png/screens/fbdump.png")
        ElseIf CheckBox2.Checked = False Then
            Shell("adb.exe shell mkdir /sdcard/fb2png")
            Shell("adb.exe shell mkdir /sdcard/fb2png/tool")
            Shell("adb.exe shell rm -rf /sdcard/fb2png/screens")
            Shell("adb.exe shell mkdir /sdcard/fb2png/screens")
            Shell("adb.exe push fb2png /sdcard/fb2png/tool/")
            Shell("adb.exe shell chmod 755 /sdcard/fb2png/tool/fb2png")
            Shell("adb.exe shell /sdcard/fb2png/tool/fb2png /sdcard/fb2png/screens/fbdump.png")
            Shell("adb.exe pull /sdcard/fb2png/screens/fbdump.png screenshots")
            Shell("adb.exe shell rm /sdcard/fb2png/screens/fbdump.png")
        End If
    End Sub
End Class
