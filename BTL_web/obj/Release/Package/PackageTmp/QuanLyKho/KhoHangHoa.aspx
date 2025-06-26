<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KhoHangHoa.aspx.cs" Inherits="BTL_web.KhoHangHoa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kho Hàng Hóa</title>
    <style>
        body {
    font-family: Arial, sans-serif;
    background-color: #f5f6fa;
    margin: 0;
    padding: 0;
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
    .container {
            width: 90%;
            margin: auto;
            padding: 20px;
            background: white;
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            margin-top: 20px;
        }

        h2 {
            text-align: center;
            color: #2c3e50;
        }

        .search-box {
    display: flex;
    justify-content: center;
    gap: 10px;
    margin-bottom: 15px;
}

.search-box input, .search-box select, .search-box button {
    padding: 8px;
    border-radius: 5px;
    border: 1px solid #ccc;
}

.search-box button {
    background: #3498db;
    color: white;
    border: none;
    cursor: pointer;
    transition: background 0.3s ease;
}

.search-box button:hover {
    background: #2980b9;
}

.gridview-container table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 15px;
}

.gridview-container th {
    background: #2c3e50;
    color: white;
    text-align: center;
}

.gridview-container td {
    padding: 10px;
    border: 1px solid #ddd;
    text-align: center;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
            <a href="" class="<%= currentPage.Contains("ThemMoi.aspx") ? "active" : "" %>">Báo cáo</a>
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
        </div>
        <div class="container">
    <h2>Kho Hàng Hóa</h2>

    <div class="search-box">
        <asp:TextBox ID="txtSearch" runat="server" placeholder="Nhập tên hàng..."></asp:TextBox>
        <asp:DropDownList ID="ddlKho" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlKho_SelectedIndexChanged">
            <asp:ListItem Text="Tất cả hàng" Value="0"></asp:ListItem>
            <asp:ListItem Text="Hàng Thừa" Value="1"></asp:ListItem>
            <asp:ListItem Text="Hàng Thiếu" Value="2"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn-search" OnClick="btnSearch_Click" />
    </div>

    <div class="gridview-container">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
            CssClass="table"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="MaHang" HeaderText="Mã Hàng" />
                <asp:BoundField DataField="TenHang" HeaderText="Tên Hàng" />
                <asp:BoundField DataField="DonVi" HeaderText="Đơn Vị" />
                <asp:BoundField DataField="SoLuong" HeaderText="Số Lượng" />
                <asp:BoundField DataField="DonGia" HeaderText="Đơn Giá" DataFormatString="{0:N0} VND"/>
                <asp:BoundField DataField="TenKho" HeaderText="Kho" />
                <asp:BoundField DataField="DuTruToiThieu" HeaderText="Dự Trữ Tối Thiểu" />
                <asp:BoundField DataField="DuTruToiDa" HeaderText="Dự Trữ Tối Đa" />
            </Columns>
        </asp:GridView>
    </div>
</div>
    </form>
</body>
</html>
