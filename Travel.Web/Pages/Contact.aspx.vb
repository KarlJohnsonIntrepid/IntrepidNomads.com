Imports System.Net.Mail

Public Class Contact
    Inherits BasePage

    Private Sub Home_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.MyMaster, blog1).HideSideBar()
    End Sub

    Private Sub Countries_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Title = "Contact the Intrepid Nomads - Budget BackPacking As A Couple"
        Me.Description = "Contact the Intrepid Nomads, we woould love to here from you"
    End Sub

    Private Sub btnContact_Click(sender As Object, e As EventArgs) Handles btnContact.Click

        Try
            ''Create the message

            Dim msg As MailMessage = New MailMessage
            msg.To.Add("nomads@intrepidnomads.com")

            Dim address As New MailAddress(txtEmail.Text)
            msg.From = address

            msg.Subject = txtName.Text & " : " & txtSubject.Text
            msg.Body = txtMessage.Text

            'Configure an SmtpClient to send the mail.
            Dim client As New SmtpClient("relay.hostinguk.net")
            client.EnableSsl = False

            'Send the msg
            client.Send(msg)

            lblMessageSent.Text = "Message Sent"

            txtEmail.Text = ""
            txtMessage.Text = ""
            txtName.Text = ""
            txtSubject.Text = ""

        Catch ex As Exception
            lblMessageSent.Text = "Message failed to send please try again"
        End Try


    End Sub
End Class