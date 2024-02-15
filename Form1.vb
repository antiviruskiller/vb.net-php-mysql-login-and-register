Imports System.Drawing
Imports System.Windows.Forms
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json.Linq
Public Class Form1
    Private Sub DisplayOnlineUsers()
       Dim request As HttpWebRequest = WebRequest.Create("https://haxcore.net/games/get_online_users.php")
        request.Method = "GET"

        Try
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()

            ' Parse JSON response
            Dim onlineUsersArray As JArray = JArray.Parse(responseFromServer)

            ' Display online users
            For Each user As JToken In onlineUsersArray
                TextBox1.AppendText(user.ToString() & Environment.NewLine)
            Next

            reader.Close()
            dataStream.Close()
            response.Close()
        Catch ex As WebException
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        DisplayOnlineUsers()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim request As Net.HttpWebRequest = Net.WebRequest.Create("https://haxcore.net/games/logout.php")
        Dim response As Net.HttpWebResponse = request.GetResponse()

        ' Optionally, handle response (e.g., display a message)
        ' Dim reader As New IO.StreamReader(response.GetResponseStream())
        ' Dim responseText As String = reader.ReadToEnd()
        ' MessageBox.Show(responseText)
    End Sub
End Class
