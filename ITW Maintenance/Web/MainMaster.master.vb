Imports ITWMaintenance.Library.Security
Imports ITWMaintenance.Library.Menu

Partial Class MainMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Security
        Response.Expires = 0
        Response.ExpiresAbsolute = Now()
        Response.CacheControl = "no-cache"

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

        If Not Page.IsPostBack() Then
            Me.UserLabel.Text = DirectCast(HttpContext.Current.Session("CslaPrincipal"), ITWPrincipal).Identity.Name
        End If

        ' Build the menu
        Dim UserIdentity = DirectCast(DirectCast(HttpContext.Current.Session("CslaPrincipal"), ITWPrincipal).Identity, ITWIdentity)

        Dim Level1MenuList As ReadOnlyMenuItemList = ReadOnlyMenuItemList.GetMenuItemList(0, UserIdentity.SecurityLevel, UserIdentity.IntakeFacility)

        ' 2/2/2015 Matt - The menu is now built based on the user's security level, which means
        ' there could be times where no menu items are returned. In that case, redirect the user
        ' over to the unauthorized page (and don't try to select a menu item).
        If Level1MenuList.Count = 0 AndAlso Not LCase(HttpContext.Current.Request.FilePath).Contains("unauthorized.aspx") Then
            Response.Redirect("~/Unauthorized.aspx")
        End If

        If Level1MenuList.Count > 0 Then
            MenuItemBulletedList.DataSource = Level1MenuList
            MenuItemBulletedList.DataBind()

            ' Default the first tab if no tab has been chosen yet
            If String.IsNullOrEmpty(Session("CurrentTab")) Then
                Session("CurrentTab") = Level1MenuList.Item(0).Description
            End If

            For Each menuTab As ListItem In MenuItemBulletedList.Items
                If menuTab.Text = Session("CurrentTab") Then
                    menuTab.Attributes("class") = "current"
                Else
                    menuTab.Attributes("class") = ""
                End If
            Next

            ' Build the submenu
            Dim Level2MenuList As ReadOnlyMenuItemList = ReadOnlyMenuItemList.GetMenuItemList(Level1MenuList.GetMenuItemByDescription(Session("CurrentTab")).ID, UserIdentity.SecurityLevel, UserIdentity.IntakeFacility)

            If IsNothing(Level2MenuList) OrElse Level2MenuList.Count = 0 Then
                NavBar.CssClass = "NavBar"
                SubLinksNavBarDiv.Visible = False
            Else
                MainBody.Attributes("class") = "SubLinksBody !important"
                NavBar.CssClass = "SubLinksNavBar !important"
                SubLinksNavBarDiv.Visible = True

                SubMenuItemBulletedList.DataSource = Level2MenuList
                SubMenuItemBulletedList.DataBind()

                ' Default the first sub tab if no tab has been chosen yet
                If String.IsNullOrEmpty(Session("CurrentSubLink")) Then
                    Session("CurrentSubLink") = Level2MenuList.Item(0).Description
                End If

                For Each subLink As ListItem In SubMenuItemBulletedList.Items
                    If subLink.Text = Session("CurrentSubLink") Then
                        subLink.Attributes("class") = "current"
                    Else
                        subLink.Attributes("class") = ""
                    End If
                Next
            End If
        End If
    End Sub

    Protected Sub LogOutButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LogOutButton.Click
        LogOff()
    End Sub

    Private Sub LogOff()
        ITWMaintenance.Library.Security.ITWPrincipal.Logout()
        Session("CslaPrincipal") = Csla.ApplicationContext.User
        Session.Abandon()
        FormsAuthentication.SignOut()
        FormsAuthentication.RedirectToLoginPage()
    End Sub

    Protected Sub MenuItemBulletedList_Click(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.BulletedListEventArgs) Handles MenuItemBulletedList.Click
        Session("CurrentTab") = MenuItemBulletedList.Items(e.Index).Text
        Session("CurrentSubLink") = ""
        Response.Redirect(MenuItemBulletedList.Items(e.Index).Value)
    End Sub

    Protected Sub SubMenuItemBulletedList_Click(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.BulletedListEventArgs) Handles SubMenuItemBulletedList.Click
        Session("CurrentSubLink") = SubMenuItemBulletedList.Items(e.Index).Text
        Response.Redirect(SubMenuItemBulletedList.Items(e.Index).Value)
    End Sub

End Class

