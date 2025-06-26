<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QLPhieu.aspx.cs" Inherits="BTL_web.QLPhieu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản Lý Phiếu</title>
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
            color: #1d5eb8;
            font-size: 24px;
        }
        .form-group {
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 10px;
            margin-bottom: 10px;
        }
        .form-group label {
            font-size: 14px;
            font-weight: bold;
        }
        .form-group input {
            padding: 5px;
            width: 150px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .search-btn {
            padding: 5px 15px;
            background: #1d5eb8;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .search-btn:hover {
            background: #144e9e;
        }
        .grid-container {
            margin-top: 20px;
        }
        .grid-container table {
            width: 100%;
            border-collapse: collapse;
        }
        .grid-container th, .grid-container td {
            padding: 10px;
            border: 1px solid #ddd;
            text-align: center;
        }
        .grid-container th {
            background-color: #1d5eb8;
            color: white;
        }
        .edit-btn {
            padding: 5px 10px;
            background: #ffc107;
            color: black;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-right: 5px;
        }

        .edit-btn:hover {
            background: #e0a800;
        }

        .delete-btn {
            padding: 5px 10px;
            background: #dc3545;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .delete-btn:hover {
            background: #c82333;
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
            <h2>Quản Lý Phiếu</h2>
            <div class="form-group">
                <label for="TextBox1">Loại phiếu</label>
                <asp:DropDownList ID="ddlLoaiPhieu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLoaiPhieu_SelectedIndexChanged">
                    <asp:ListItem Text="Tất cả" Value="" />
                    <asp:ListItem Text="Nhập" Value="Nhập" />
                    <asp:ListItem Text="Xuất" Value="Xuất" />
                </asp:DropDownList>

                <label for="TextBox2">Trạng thái</label>
                <asp:DropDownList ID="ddlTrangThai" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTrangThai_SelectedIndexChanged">
                    <asp:ListItem Text="Tất cả" Value="" />
                    <asp:ListItem Text="Hoàn Thành" Value="1" />
                    <asp:ListItem Text="Chưa Hoàn Thành" Value="0" />
                </asp:DropDownList>

                <label for="TextBox3">Ngày</label>
                <asp:TextBox ID="TextBox3" runat="server" TextMode="Date"></asp:TextBox>

                <asp:Button ID="btnSearch" runat="server" Text="Tìm Kiếm" CssClass="search-btn" OnClick="btnSearch_Click" />

            </div>
            
            <div class="grid-container">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grid-container" OnRowCommand="GridView1_RowCommand">
    <Columns>
        <asp:BoundField DataField="MaPhieu" HeaderText="Mã Phiếu" />
        <asp:BoundField DataField="TenKho" HeaderText="Tên Kho" />
        <asp:BoundField DataField="Ngay" HeaderText="Ngày" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="TenHang" HeaderText="Tên Hàng" />
        <asp:BoundField DataField="DonVi" HeaderText="Đơn Vị" />
        <asp:BoundField DataField="SoLuong" HeaderText="Số Lượng" />
        <asp:BoundField DataField="DonGia" HeaderText="Đơn Giá" DataFormatString="{0:N0}" />
        <asp:BoundField DataField="ThanhTien" HeaderText="Thành Tiền" DataFormatString="{0:N0}" />
        <asp:BoundField DataField="LoaiPhieu" HeaderText="Loại Phiếu" />
        <asp:BoundField DataField="TenDoiTac" HeaderText="Tên Đối Tác" />
        <asp:BoundField DataField="TrangThai" HeaderText="Trạng Thái" />
        <asp:TemplateField HeaderText="Thao Tác">
            <ItemTemplate>
                <asp:Button ID="btnEdit" runat="server" Text="Sửa" CssClass="edit-btn"
    CommandName="EditPhieu" CommandArgument="<%# Container.DataItemIndex %>" />
                <asp:Button ID="btnDelete" runat="server" Text="Xóa" CssClass="delete-btn"
                    CommandName="DeletePhieu" CommandArgument="<%# Container.DataItemIndex %>"
                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


            </div>
        </div>
    </form>
</body>
</html>