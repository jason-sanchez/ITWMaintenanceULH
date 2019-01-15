Imports ITWMaintenance.Library.Security

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DirectCast(frmLogin.FindControl("UserName"), TextBox).Text > "" Then
            DirectCast(frmLogin.FindControl("Password"), TextBox).Focus()
        Else
            DirectCast(frmLogin.FindControl("UserName"), TextBox).Focus()
        End If
    End Sub

    Protected Sub frmLogin_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles frmLogin.Authenticate
        Try
            If ITWPrincipal.Login(Me.frmLogin.UserName, Me.frmLogin.Password) Then
                e.Authenticated = True
                HttpContext.Current.Session("CslaPrincipal") = Csla.ApplicationContext.User
            Else
                e.Authenticated = False
                HttpContext.Current.Session("CslaPrincipal") = Nothing
            End If
        Catch ex As LoginException
            e.Authenticated = False
            HttpContext.Current.Session("CslaPrincipal") = Nothing
            Me.frmLogin.FailureText = ex.Message
        End Try
    End Sub

    Protected Sub frmLogin_LoggedIn(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmLogin.LoggedIn
        'If _changePassword Then
        '    Response.Redirect("Maintenance/ChangePassword.aspx?expired=true")
        'End If

        ' Always redirect to the start page
        Response.Redirect("Evals/Level1Folders.aspx")
    End Sub

End Class
