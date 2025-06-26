<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThucHienPhieu.aspx.cs" Inherits="BTL_web.ThucHienPhieu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thực Hiện Phiếu</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
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
            margin-top: 40px;
            max-width: 900px;
        }

        .table {
            background-color: white;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
        }

            .table thead {
                background-color: #007bff;
                color: white;
            }

        .btn-thuchien {
            background-color: #28a745;
            color: white;
            padding: 6px 12px;
            border: none;
            border-radius: 4px;
            transition: background-color 0.3s;
        }

            .btn-thuchien:hover {
                background-color: #218838;
            }
    </style>
</head>
<body>
    <div class="navbar">
        <div class="logo">Trang Thủ Kho</div>
        <ul class="menu">
            <%
                string currentPage = Request.Url.AbsolutePath;
            %>

            <li>
                <a href="TrangThuKho.aspx" class="<%= currentPage.Contains("TrangThuKho.aspx") ? "active" : "" %>">Thông Tin Cá Nhân</a>
            </li>
            <li>
                <a href="ThucHienPhieu.aspx" class="<%= currentPage.Contains("ThucHienPhieu.aspx") ? "active" : "" %>">Xem Phiếu</a>
            </li>
            <li>
                <a href="XemKho.aspx" class="<%= currentPage.Contains("XemKho.aspx") ? "active" : "" %>">Xem Kho</a>
            </li>
            <li>
                <a href="/DangXuat.aspx">Đăng Xuất</a>
            </li>
        </ul>
    </div>

    <div class="container">
        <h2 class="text-center text-primary">Danh Sách Phiếu Cần Thực Hiện</h2>
        <form id="form1" runat="server">
            <asp:GridView ID="gvPhieu" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                OnRowCommand="gvPhieu_RowCommand">
                <Columns>
                    <asp:BoundField DataField="MaPhieu" HeaderText="Mã Phiếu" />
                    <asp:BoundField DataField="LoaiPhieu" HeaderText="Loại Phiếu" />
                    <asp:BoundField DataField="MaHang" HeaderText="Mã Hàng" />
                    <asp:BoundField DataField="TenHang" HeaderText="Tên Hàng" />
                    <asp:BoundField DataField="SoLuong" HeaderText="Số Lượng" />
                    <asp:TemplateField HeaderText="Trạng Thái">
                        <ItemTemplate>
                            <%# Convert.ToInt32(Eval("TrangThai")) == 0 ? "<span class='badge bg-danger'>Chưa Hoàn Thành</span>" : "<span class='badge bg-success'>Hoàn Thành</span>" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnThucHien" runat="server" CssClass='<%# Convert.ToInt32(Eval("TrangThai")) == 1 ? "btn btn-disabled" : "btn btn-thuchien" %>'
                                CommandName="ThucHien" CommandArgument="<%# Container.DataItemIndex %>"
                                Text="Thực Hiện" Enabled='<%# Convert.ToInt32(Eval("TrangThai")) == 0 %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </form>
    </div>
</body>
</html>
