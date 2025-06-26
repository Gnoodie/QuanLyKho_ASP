<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs" Inherits="BTL_web.Confirm" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Xác nhận thông tin</title>
    <style type="text/css">
        .container {
            width: 60%;
            margin: 5% auto;
            background-color: #EAF4FF;
            padding: 2%;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.2);
            text-align: center;
        }
        h2 {
            color: #004080;
            font-size: 2em;
            margin-bottom: 15px;
        }
        .info {
            text-align: left;
            font-size: 1.2em;
            color: #004080;
            margin: 10px 0;
            padding: 8px;
            border-bottom: 1px solid #004080;
        }
        .info strong {
            width: 150px;
            display: inline-block;
        }
        .submit-button {
            margin-top: 20px;
            padding: 10px 20px;
            font-size: 1.1em;
            background-color: #004080;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .submit-button:hover {
            background-color: #002D5E;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Xác nhận thông tin nhập kho</h2>
            <p class="info"><strong>Mã kho:</strong> <asp:Label ID="lblMaKho" runat="server" /></p>
            <p class="info"><strong>Tên kho:</strong> <asp:Label ID="lblTenKho" runat="server" /></p>
            <p class="info"><strong>Mã đối tác:</strong> <asp:Label ID="lblMaDoiTac" runat="server" /></p>
            <p class="info"><strong>Tên đối tác:</strong> <asp:Label ID="lblTenDoiTac" runat="server" /></p>
            <p class="info"><strong>Loại phiếu:</strong> <asp:Label ID="lblLoaiPhieu" runat="server" /></p>
            <p class="info"><strong>Ngày:</strong> <asp:Label ID="lblNgay" runat="server" /></p>
            <p class="info"><strong>Mã hàng:</strong> <asp:Label ID="lblMaHang" runat="server" /></p>
            <p class="info"><strong>Tên hàng:</strong> <asp:Label ID="lblTenHang" runat="server" /></p>
            <p class="info"><strong>Đơn vị:</strong> <asp:Label ID="lblDonVi" runat="server" /></p>
            <p class="info"><strong>Số lượng:</strong> <asp:Label ID="lblSoLuong" runat="server" /></p>
            <p class="info"><strong>Đơn giá:</strong> <asp:Label ID="lblDonGia" runat="server" /></p>
            <p class="info"><strong>Thành tiền:</strong> <asp:Label ID="lblThanhTien" runat="server" /></p>
            
            <asp:Button ID="btnXacNhan" runat="server" Text="Xác nhận nhập kho" CssClass="submit-button" OnClick="btnXacNhan_Click" />
        </div>
    </form>
</body>
</html>
