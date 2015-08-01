Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim startInfo As New ProcessStartInfo("cmd.exe")
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardInput = True
        startInfo.RedirectStandardOutput = True
        startInfo.CreateNoWindow = False
        Dim adbProcess As Process = Process.Start(startInfo)
        Dim adbInput As IO.StreamWriter = adbProcess.StandardInput
        Dim adbOutput As IO.StreamReader = adbProcess.StandardOutput

        ' Send the input
        adbInput.WriteLine("adb.exe wait-for-device")
        adbInput.WriteLine("adb.exe reboot")
        adbInput.WriteLine("adb.exe kill-server")
        adbInput.Close()

        ' read the output:
        While Not adbOutput.EndOfStream
            Console.WriteLine("|output:| {0}", adbOutput.ReadLine())
        End While
        adbOutput.Close()

        adbProcess.WaitForExit()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim startInfo As New ProcessStartInfo("cmd.exe")
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardInput = True
        startInfo.RedirectStandardOutput = True
        startInfo.CreateNoWindow = False
        Dim adbProcess As Process = Process.Start(startInfo)
        Dim adbInput As IO.StreamWriter = adbProcess.StandardInput
        Dim adbOutput As IO.StreamReader = adbProcess.StandardOutput

        ' Send the input
        adbInput.WriteLine("adb.exe wait-for-device")
        adbInput.WriteLine("adb.exe reboot recovery")
        adbInput.WriteLine("adb.exe kill-server")
        adbInput.Close()

        ' read the output:
        While Not adbOutput.EndOfStream
            Console.WriteLine("|output:| {0}", adbOutput.ReadLine())
        End While
        adbOutput.Close()

        adbProcess.WaitForExit()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim startInfo As New ProcessStartInfo("cmd.exe")
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardInput = True
        startInfo.RedirectStandardOutput = True
        startInfo.CreateNoWindow = False
        Dim adbProcess As Process = Process.Start(startInfo)
        Dim adbInput As IO.StreamWriter = adbProcess.StandardInput
        Dim adbOutput As IO.StreamReader = adbProcess.StandardOutput

        ' Send the input
        adbInput.WriteLine("adb.exe wait-for-device")
        adbInput.WriteLine("adb.exe reboot dwonload")
        adbInput.WriteLine("adb.exe kill-server")
        adbInput.Close()

        ' read the output:
        While Not adbOutput.EndOfStream
            Console.WriteLine("|output:| {0}", adbOutput.ReadLine())
        End While
        adbOutput.Close()

        adbProcess.WaitForExit()
    End Sub

End Class
