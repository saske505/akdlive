<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<script runat="server">
    void Application_Start(object sender, EventArgs e)
    {
        AKDLIVE.RouteConfig.RegisterRoutes(RouteTable.Routes);
    }
</script>
