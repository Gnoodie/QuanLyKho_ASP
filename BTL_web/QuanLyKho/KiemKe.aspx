<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KiemKe.aspx.cs" Inherits="BTL_web.KiemKe" %>

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
            width: 80%;
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
            gap: 15px;
            align-items: center;
            margin-bottom: 15px;
        }

        .search-box input, .search-box button {
            padding: 10px;
            border-radius: 5px;
            border: 1px solid #ccc;
            font-size: 16px;
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

        .table-container {
            overflow-x: auto;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
            background: white;
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            overflow: hidden;
        }

        .table th, .table td {
            padding: 12px;
            border: 1px solid #ddd;
            text-align: center;
        }

        .table th {
            background: #2c3e50;
            color: white;
        }

        .table tr:hover {
            background: #f1f1f1;
        }
        .summary {
    text-align: center;
    margin-top: 20px;
    font-size: 18px;
    font-weight: bold;
}

.calculate-button {
    margin-top: 10px;
    padding: 10px 15px;
    background-color: #27ae60;
    color: white;
    border: none;
    cursor: pointer;
    border-radius: 5px;
    font-size: 16px;
}

.calculate-button:hover {
    background-color: #219150;
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
            <a href="" class="<%= currentPage.Contains("BaoCao") ? "active" : "" %>">Báo cáo</a>
            <div class="dropdown-menu">
                <a href="KhoHangHoa.aspx" class="<%= currentPage.Contains("KhoHangHoa.aspx") ? "active" : "" %>">Tồn kho</a>
                <a href="KiemKe.aspx" class="<%= currentPage.Contains("KiemKe.aspx") ? "active" : "" %>">Nhập Xuất</a>
            </div>
        </li>
        <li>
            <a href="DangXuat.aspx">Đăng Xuất</a>
        </li>
    </ul>
</div>
        </div>

        <div class="container">
            <h2>Kiểm Kê Hàng Hóa</h2>
            <div class="search-box">
                <asp:Label ID="lblFromDate" runat="server" Text="Từ ngày:"></asp:Label>
                <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date"></asp:TextBox>

                <asp:Label ID="lblToDate" runat="server" Text="Đến ngày:"></asp:Label>
                <asp:TextBox ID="txtToDate" runat="server" TextMode="Date"></asp:TextBox>

                <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Đặt lại" OnClick="btnReset_Click" />
            </div>

            <div class="table-container">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table">
                    <Columns>
                        <asp:BoundField DataField="LoaiPhieu" HeaderText="Loại Phiếu" />
                        <asp:BoundField DataField="TenHang" HeaderText="Tên Hàng" />
                        <asp:BoundField DataField="SoLuong" HeaderText="Số Lượng" />
                        <asp:BoundField DataField="ThanhTien" HeaderText="Thành Tiền" />
                        <asp:BoundField DataField="Ngay" HeaderText="Ngày" DataFormatString="{0:yyyy-MM-dd}" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="summary">
    <asp:Label ID="lblTongTienNhap" runat="server" Text="Tổng tiền đã chi: 0"></asp:Label><br />
    <asp:Label ID="lblTongTienXuat" runat="server" Text="Tổng tiền đã thu: 0"></asp:Label><br />
    <asp:Button ID="btnTinhTong" runat="server" Text="Tính Tổng" OnClick="btnTinhTong_Click" CssClass="calculate-button"/>
</div>

        </div>
    </form>
</body>
</html>
