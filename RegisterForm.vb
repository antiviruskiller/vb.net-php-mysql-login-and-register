Imports System.Net
Imports System.IO

Public Class RegisterForm

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Check if username and password fields are empty
        If String.IsNullOrEmpty(UsernameTextBox.Text) OrElse String.IsNullOrEmpty(PasswordTextBox.Text) Then
            MessageBox.Show("Username and password cannot be empty.")
            Return
        End If

        Try
            Dim request As HttpWebRequest = WebRequest.Create("https://haxcore.net/games/register.php")
            request.Method = "POST"
            Dim postData = "username=" & UsernameTextBox.Text & "&password=" & PasswordTextBox.Text
            Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
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
                If responseFromServer = "New record created successfully" Then
                    ' Display a new form or perform any other action upon successful record creation
                    Dim newForm As New Form()
                    newForm.Show()
                End If
                reader.Close()
            End If

            dataStreamResponse.Close()
            response.Close()
        Catch ex As WebException
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        LoginForm.Show()
    End Sub
End Class
