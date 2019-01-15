Imports ITWMaintenance.Library.Security

Partial Class Evals_EvalMainMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Duplicate this here since this runs before the MainMaster page
        If IsNothing(HttpContext.Current.Session("CslaPrincipal")) Then
            LogOff()
        Else
            Try
                If Not DirectCast(HttpContext.Current.Session("CslaPrincipal"), ITWPrincipal).Identity.IsAuthenticated Then
                    LogOff()
                End If
            Catch ex As Exception
                LogOff()
            End Try
        End If
    End Sub

    Private Sub LogOff()
        ITWMaintenance.Library.Security.ITWPrincipal.Logout()
        Session("CslaPrincipal") = Csla.ApplicationContext.User
        Session.Abandon()
        FormsAuthentication.SignOut()
        FormsAuthentication.RedirectToLoginPage()
    End Sub

End Class

