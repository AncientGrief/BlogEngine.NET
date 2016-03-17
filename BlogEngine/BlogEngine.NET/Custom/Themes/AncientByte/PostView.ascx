<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="BlogEngine.Core.Web.Controls.PostViewBase" %>
<%@ Import Namespace="BlogEngine.Core" %>
<article class="post" id="post<%=Index %>">
    <header class="post-header">
        <h2 class="post-title">
            <a href="<%=Post.RelativeOrAbsoluteLink %>"><%=Server.HtmlEncode(Post.Title) %></a>
        </h2>
        <div class="post-info clearfix">
            <span class="post-date"><i class="glyphicon glyphicon-calendar"></i><%=Post.DateCreated.ToString("d. MMMM yyyy") %></span> by 
            <span class="post-author"><i class="glyphicon glyphicon-user"></i><a href="<%=Utils.AbsoluteWebRoot + "author/" + Utils.RemoveIllegalCharacters(Post.Author + BlogConfig.FileExtension) %>"><%=Post.AuthorProfile != null ? Utils.RemoveIllegalCharacters(Post.AuthorProfile.DisplayName) : Utils.RemoveIllegalCharacters(Post.Author) %></a></span>
            
            <% 
                //string categories = CategoryLinks(", ");
                string tags = TagLinks(", ");
                if (!string.IsNullOrEmpty(tags)) { %>
            ,<span class="post-tags"><i class="fa fa-tags"></i><%=tags %></span>
            <% } %>
            <a rel="nofollow" class="post-comment-link" href="<%=Post.RelativeOrAbsoluteLink %>#comment"><i class="fa fa-comment"></i>(<%=Post.ApprovedComments.Count %>)</a>
        </div>
    </header>
    <section class="post-body text">
        <asp:PlaceHolder ID="BodyContent" runat="server" />
    </section>

    <% if (Location == BlogEngine.Core.ServingLocation.SinglePost)
       {%>
    <footer class="post-footer">
        <!--<div class="post-rating">
            <%=Rating %>
        </div>-->
    </footer>

    <%} %>
    <div class="adminLinks">
       <%=AdminLinks %>
    </div>
</article>
