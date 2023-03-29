<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO.master" AutoEventWireup="true" CodeFile="ScoComplain.aspx.cs" Inherits="SCO_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            padding-right: 10px;
            padding-bottom: 10px;
            width: 800px;
        }
    </style>

    <div class="row">
        <div class="col-lg-2 col-md-2 col-sm-2">
            <b>Complain Type :</b>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10">
            <asp:DropDownList ID="Ddl_ComplainType" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Font-Bold="true" ForeColor="#CC3300" Font-Size="XX-Small" ControlToValidate="Ddl_ComplainType"
                CssClass="required" Display="dynamic" ErrorMessage="Please Select Complain Type" SetFocusOnError="true"
                InitialValue="0"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row">
        <div style="background-color: red; color: white; font-weight: bold; padding: 10px;">Please mention any Reference No. like Farmer Id, A/c No., Voter Id no. Aadhar No. while lodging your complain. Ensure to describe your complain in detail to avoid rejection.   </div>
        <div class="col-lg-2 col-md-2 col-sm-2"><b>Problem : </b></div>
        <div class="col-lg-10 col-md-10 col-sm-10">
            <asp:TextBox ID="Txt_Problem" runat="server" TextMode="MultiLine" Width="575px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorProblem" Display="Dynamic" Font-Bold="true"
                ForeColor="#CC3300" SetFocusOnError="true" runat="server" Font-Size="XX-Small"
                ErrorMessage="Please Fill Problem Box" ControlToValidate="Txt_Problem"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-2 col-md-2 col-sm-2">
            <b>Attach File :</b>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10">
            <asp:FileUpload ID="FileUpload1" runat="server" />

            <%--            <asp:RequiredFieldValidator ID="RequiredFieldValidatorupload" Display="Dynamic" Font-Bold="true"
                ForeColor="#CC3300" SetFocusOnError="true" ErrorMessage="Upload Is Required" ControlToValidate="FileUpload1"
                runat="server" Font-Size="XX-Small" />--%>
        </div>
    </div>
    <br />
    <div class="row">

        <div class="col-lg-12 col-md-12 col-sm-12">
            <%--<asp:LinkButton > <span class="badge">Submit</span></asp:LinkButton>--%>
            <asp:Button ID="Btn_Submit" CssClass="btn btn-success" runat="server"
                OnClick="Btn_Submit_Click" Text="Submit" /> 
        </div>
    </div>
    <div>
        <cc1:ModalPopupExtender ID="mpe1" runat="server" BehaviorID="popup" TargetControlID="Button1"
            PopupControlID="poppanel" CancelControlID="btn_close" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
        <div class="col-lg-1 col-offset-6 centered">
            <asp:Panel ID="poppanel" runat="server" CssClass="modalPopup" align="center" Style="width: 300px; background-color: #F0FFF0"
                ScrollBars="Auto">
                <div>
                    Your Complain No. is<b>
                        <asp:Label ID="LblComplain" runat="server" Text="Label"></asp:Label></b>.Please Note it down.
                   
                </div>
                <div>
                    <asp:Button ID="btn_close" runat="server" Text="close" OnClientClick="javascript:ResetButton();" />&nbsp;&nbsp;
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

