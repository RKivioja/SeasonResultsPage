<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tulospalvelu.aspx.cs" Inherits="Tulospalvelu.Tulospalvelu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Resultstyle.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery('.tabs .tab-links a').on('click', function (e) {
                var currentAttrValue = jQuery(this).attr('href');

                // Show/Hide Tabs
                jQuery('.tabs ' + currentAttrValue).show().siblings().hide();

                // Change/remove current tab to active
                jQuery(this).parent('li').addClass('active').siblings().removeClass('active');

                e.preventDefault();
            });
        });
    </script>
</head>

<body>
   <h1>Veikkausliigan tulokset kaudelta 2015</h1>
   <div class="tabs">
      <ul class="tab-links">
         <li class="active"><a href="#tab1">Kaikki tulokset</a></li>
         <li><a href="#tab2">Haku</a></li>
      </ul>
 
      <div class="tab-content">
         <div id="tab1" class="tab active">
            <div id="divAllMatches">
               <p id="LabelAllMatches">Kaikki pelatut ottelut:</p>
               <asp:Table ID="TableAllMatches" runat="server"></asp:Table>
            </div>
         </div>
 
         <div id="tab2" class="tab">
            <p>Etsi otteluita päivämäärän perusteella:</p>
            <form runat="server">
            <div id="begindate">
               <label>Alkaen:</label>
               <asp:TextBox ID="TextBoxStartDay" runat="server"/>
               <label>(pp.kk.vvvv)</label>
            </div>

            <div id="enddate">   
               <label>Päättyen:</label>
               <asp:TextBox ID="TextBoxEndDay" runat="server"/>
               <label>(pp.kk.vvvv)</label>
            </div>

            <asp:ScriptManager runat="server" ID="ScriptManagerSearch">
            </asp:ScriptManager>
            
            <asp:updatepanel ID="UpdatepanelSearch" runat="server">
               <ContentTemplate>
                  <asp:Button ID="ButtonSearch" Text="Hae" OnClick="ButtonSearch_Click" runat="server"/>
                  <asp:Table ID="TableResults" runat="server"></asp:Table>
               </ContentTemplate>
            </asp:updatepanel>

            </form>

            
         </div>
    </div>
</div>
</body>
</html>
