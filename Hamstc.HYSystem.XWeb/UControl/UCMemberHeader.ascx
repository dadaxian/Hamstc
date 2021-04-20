<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCMemberHeader.ascx.cs" Inherits="Hamstc.HYSystem.XWeb.UControl.UCMemberHeader" %>

<%-- 头部--%>
<div class="heading dark affix dock-top">
    <nav class="nav">
        <a href="javascript:history.go(-1)" style="background:url(./Res/CSS/imgs/arr-left-white.png) center center no-repeat;"></a>
    </nav>
    <div class="title text-center"><strong><asp:Literal ID="litTitle" runat="server" Text="默认标题"></asp:Literal></strong></div>
    <nav class="nav">
        <a> </a>
    </nav>
</div>