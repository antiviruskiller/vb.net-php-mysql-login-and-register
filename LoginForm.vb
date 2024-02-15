Imports System.Net
Imports System.IO

Public Class LoginForm

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim request As HttpWebRequest = WebRequest.Create("https://haxcore.net/games/login.php")
        request.Method = "POST"
        Dim postData = "username=" & UsernameTextBox.Text & "&password=" & PasswordTextBox.Text
        Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)
        request.ContentType = "application/x-www-form-urlencoded"
        request.ContentLength = byteArray.Length

        Try
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

            Dim response As WebResponse = request.GetResponse()
            Dim statusDescription As String = CType(response, HttpWebResponse).StatusDescription
            Dim dataStreamResponse As Stream = response.GetResponseStream()

            If dataStreamResponse IsNot Nothing Then
                Dim reader As New StreamReader(dataStreamResponse)
                Dim responseFromServer As String = reader.ReadToEnd()
                MessageBox.Show(responseFromServer)

                If responseFromServer.Trim() = "Login successful" Then
                    MessageBox.Show("Login successful")
                    ' Open your main form or perform any other action upon successful login
                    Form1.Show()
                Else
                    MessageBox.Show("Invalid username or password")
                End If

                reader.Close()
            End If

            dataStreamResponse.Close()
            response.Close()
        Catch ex As WebException
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try
    End Sub
End Class
