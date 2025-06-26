<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuaPhieu.aspx.cs" Inherits="BTL_web.SuaPhieu" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Sửa Phiếu</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .container {
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            width: 400px;
        }
        h2 {
            text-align: center;
            color: #333;
        }
        .form-group {
            display: flex;
            flex-direction: column;
            margin-bottom: 15px;
        }
        label {
            font-weight: bold;
            margin-bottom: 5px;
        }
        input, select {
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 14px;
        }
        .btn-container {
            text-align: center;
        }
        button {
            background-color: #007bff;
            color: white;
            padding: 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            width: 100%;
        }
        button:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Sửa Phiếu</h2>

            <div class="form-group">
                <label>Mã Phiếu:</label>
                <asp:TextBox ID="txtMaPhieu" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Mã Kho:</label>
                <asp:DropDownList ID="ddlMaKho" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMaKho_SelectedIndexChanged"></asp:DropDownList>
            </div>
            
            <div class="form-group">
                <label>Tên Hàng:</label>
                <asp:DropDownList ID="ddlTenHang" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTenHang_SelectedIndexChanged"></asp:DropDownList>
            </div>
            
            <div class="form-group">
                <label>Đơn Vị:</label>
                <asp:TextBox ID="txtDonVi" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Đơn Giá:</label>
                <asp:TextBox ID="txtDonGia" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Số Lượng:</label>
                <asp:TextBox ID="txtSoLuong" runat="server" AutoPostBack="true" OnTextChanged="txtSoLuong_TextChanged"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Thành Tiền:</label>
                <asp:TextBox ID="txtThanhTien" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Loại Phiếu:</label>
                <asp:DropDownList ID="ddlLoaiPhieu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLoaiPhieu_SelectedIndexChanged">
                    <asp:ListItem Text="Nhập" Value="Nhập"></asp:ListItem>
                    <asp:ListItem Text="Xuất" Value="Xuất"></asp:ListItem>
                </asp:DropDownList>
            </div>
            
            <div class="form-group">
                <label>Đối Tác:</label>
                <asp:DropDownList ID="ddlDoiTac" runat="server"></asp:DropDownList>
            </div>
            
            <div class="form-group">
                <label>Ngày:</label>
                <asp:TextBox ID="txtNgay" runat="server" TextMode="Date"></asp:TextBox>
            </div>
            
            <div class="btn-container">
                <asp:Button ID="btnCapNhat" runat="server" Text="Cập Nhật" OnClick="btnCapNhat_Click" />
            </div>
        </div>
    </form>
</body>
</html>
