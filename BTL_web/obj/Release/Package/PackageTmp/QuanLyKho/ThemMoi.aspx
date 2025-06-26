<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThemMoi.aspx.cs" Inherits="BTL_web.ThemMoi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
        .container {
               width: 65%;
                margin: auto;
                background-color: #EAF4FF;
                padding: 2% 2% 1% 2%;
                border-radius: 10px;
                text-align: center;
        }
        .form-group {
               display: flex;
                justify-content: space-evenly;
                align-items: center;
                margin-bottom: 1.5%;
        }
        .form-group label {
                width: 11%;
                text-align: left;
                font-weight: bold;
                font-size: 1.2em;
                color: #004080;
        }
        .form-group input {
                width: 60%;
                padding: 1.1%;
                border: 1px solid #ccc;
                border-radius: 5px;
                box-sizing: border-box;
        }
        .form-group select {
        width: 60%;
        padding: 1.1%;
        border: 1px solid #ccc;
        border-radius: 5px;
        box-sizing: border-box;
}
        .radio-group {
                text-align: center;
                margin: 2.5%;
                font-size: 1.2em;
                color: #004080;
        }
        .radio-group label {        
            margin-right: 3%; /* Khoảng cách giữa "Loại Phiếu" và radio button */   
            font-weight: bold;
        }

        .radio-group input[type="radio"] {
            margin-left:3%; /* Tăng khoảng cách giữa các radio button */
        }

        .submit-button {
            display: block;
            margin: 3% auto;
            padding: 1.5% 3%;   
            font-size: 1.1em;
            background-color: #004080;
            color: white;
            border: none;
            cursor: pointer;
            border-radius: 5px;
        }
        h2 {
            margin-top: 0;
            text-align: center;
            color: #004080;
            font-size: 2.5em    ;
        }
        .navbar {
    background: #2c3e50;
    color: white;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 15px 25px;
    position: sticky;
    top: 0;
    z-index: 100;
    box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
}

.navbar .menu {
    list-style: none;
    display: flex;
    margin: 0;
    padding: 0;
}

.navbar .menu li {
    position: relative;
}

.navbar .menu li a {
    text-decoration: none;
    color: white;
    padding: 12px 18px;
    display: block;
    font-size: 16px;
    transition: all 0.3s ease;
    border-radius: 5px;
}

.navbar .menu li a:hover, .navbar .menu li .active {
    background: #3498db;
}

/* Dropdown menu */
.dropdown-menu {
    display: none;
    position: absolute;
    background: #34495e;
    min-width: 180px;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
    border-radius: 5px;
    top: 40px;
    left: 0;
    z-index: 200;
}

.dropdown-menu a {
    display: block;
    color: white;
    padding: 10px 15px;
    text-decoration: none;
    transition: background 0.3s ease;
}

.dropdown-menu a:hover, .dropdown-menu a.active {
    background: #1abc9c;
}

.navbar .menu li:hover .dropdown-menu {
    display: block;
}
        body {
    font-family: Arial, sans-serif;
    background-color: #f5f6fa;
    margin: 0;
    padding: 0;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
                       <div class="navbar">
    <div class="logo">Trang Quản Lý</div>
    <ul class="menu">
        <%
            string currentPage = Request.Url.AbsolutePath;
        %>

        <li>
            <a href="TrangQuanLy.aspx" class="<%= currentPage.Contains("TrangQuanLy.aspx") ? "active" : "" %>">Thông Tin Cá Nhân</a>
        </li>

        <li>
            <a href="TaoPhieu.aspx" class="<%= currentPage.Contains("TaoPhieu.aspx") ? "active" : "" %>">Tạo Phiếu</a>
        </li>

        <li>
            <a href="ThemMoi.aspx" class="<%= currentPage.Contains("ThemMoi.aspx") ? "active" : "" %>">Thêm mới</a>
        </li>
        <li>
    <a href="QLPhieu.aspx" class="<%= currentPage.Contains("QLPhieu.aspx") ? "active" : "" %>">Quản lý phiếu</a>
</li>
        <li>
            <a href="" class="<%= currentPage.Contains("BaoCao.aspx") ? "active" : "" %>">Báo cáo</a>
            <div class="dropdown-menu">
                <a href="KhoHangHoa.aspx" class="<%= currentPage.Contains("KhoHangHoa.aspx") ? "active" : "" %>">Tồn kho</a>
                <a href="KiemKe.aspx" class="<%= currentPage.Contains("TaoPhieuNhap.aspx") ? "active" : "" %>">Nhập Xuất</a>
            </div>
        </li>
        <li>
            <a href="/DangXuat.aspx">Đăng Xuất</a>
        </li>
    </ul>
</div>
        <div class="container">
    <h2>Phiếu Nhập Hàng</h2>
    <div class="form-group">
        <label>Kho</label>
        <asp:DropDownList ID="ddlKho" runat="server" Width="60%"></asp:DropDownList>
    </div>
    <div class="form-group">
        <label>Tên Hàng</label>
        <asp:TextBox ID="txtTenHang" runat="server" Width="60%"></asp:TextBox>
    </div>
    <div class="form-group">
        <label>Đơn vị</label>
        <asp:TextBox ID="txtDonVi" runat="server" Width="60%"></asp:TextBox>
    </div>
    <div class="form-group">
        <label>Đơn giá</label>
        <asp:TextBox ID="txtDonGia" runat="server" Width="60%"></asp:TextBox>
    </div>
    <div class="form-group">
        <label>Dự trữ tối thiểu</label>
        <asp:TextBox ID="txtDuTruToiThieu" runat="server" Width="60%"></asp:TextBox>
    </div>
    <div class="form-group">
        <label>Dự trữ tối đa</label>
        <asp:TextBox ID="txtDuTruToiDa" runat="server" Width="60%"></asp:TextBox>
    </div>
    <asp:Button ID="btnThemHang" runat="server" Text="Thêm Hàng" CssClass="submit-button" OnClick="btnThemHang_Click" />
</div>

    </form>
</body>
</html>
