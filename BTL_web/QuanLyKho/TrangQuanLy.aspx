<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrangQuanLy.aspx.cs" Inherits="BTL_web.TrangQuanLy" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Trang Quản Lý</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" />
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

        .main-content {
            padding: 40px;
            max-width: 800px;
            margin: auto;
        }

        .info-container {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 15px;
        }

        .info-box {
            background: white;
            padding: 15px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            display: flex;
            align-items: center;
            justify-content: space-between;
            transition: transform 0.2s ease-in-out;
        }

            .info-box:hover {
                transform: translateY(-5px);
            }

        .label {
            font-weight: bold;
            color: #333;
        }

        .button-container {
            margin-top: 20px;
            text-align: center;
        }

        .btn {
            padding: 10px 20px;
            font-size: 16px;
            color: white;
            background: #3498db;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: 0.3s;
            margin: 5px;
        }

            .btn:hover {
                background: #2980b9;
            }
        /* Ẩn dialog mặc định */
        .dialog {
            display: none;
            position: fixed;
            z-index: 9999;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
        }

        /* Hộp thoại nội dung */
        .dialog-content {
            background: white;
            width: 40%;
            margin: 10% auto;
            padding: 20px;
            border-radius: 10px;
            position: relative;
        }

        /* Nút đóng */
        .close {
            position: absolute;
            top: 10px;
            right: 15px;
            font-size: 20px;
            cursor: pointer;
        }

        /* Ô nhập liệu */
        .input-box {
            width: 100%;
            padding: 8px;
            margin-top: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        /* Nút */
        .btn-dialog {
            background: #28a745;
            color: white;
            padding: 10px 15px;
            border: none;
            cursor: pointer;
            margin-top: 10px;
        }

        .dialog-footer {
            display: flex;
            justify-content: space-between;
            padding-top: 10px;
        }

        .btn-cancel {
            margin-left: auto;
            background-color: #ccc;
            color: black;
            border: none;
            padding: 8px 15px;
            cursor: pointer;
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
                    <a href="" class="<%= currentPage.Contains("BaoCao") ? "active" : "" %>">Báo cáo</a>
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

        <div class="main-content">
            <h2>Thông Tin Nhân Viên</h2>
            <div class="info-container">
                <div class="info-box">
                    <span class="label">Họ Tên:</span>
                    <asp:Label ID="lblHoTen" runat="server"></asp:Label>
                </div>
                <div class="info-box">
                    <span class="label">Ngày sinh:</span>
                    <asp:Label ID="lblNgaySinh" runat="server"></asp:Label>
                </div>
                <div class="info-box">
                    <span class="label">Giới tính:</span>
                    <asp:Label ID="lblGioiTinh" runat="server"></asp:Label>
                </div>
                <div class="info-box">
                    <span class="label">Email:</span>
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </div>
                <div class="info-box">
                    <span class="label">Địa chỉ:</span>
                    <asp:Label ID="lblDiaChi" runat="server"></asp:Label>
                </div>
                <div class="info-box">
                    <span class="label">Chức vụ:</span>
                    <asp:Label ID="lblChucVu" runat="server"></asp:Label>
                </div>
            </div>
            <div class="button-container">
                <asp:Button ID="btnThayDoiThongTin" runat="server" Text="Thay Đổi Thông Tin" CssClass="btn" OnClientClick="moDialog(); return false;" />
                <asp:Button ID="btnDoiMatKhau" runat="server" Text="Đổi Mật Khẩu" CssClass="btn" OnClientClick="moDialogDoiMatKhau(); return false;" />
            </div>
        </div>
        <!-- Dialog (ẩn mặc định) -->
        <div id="dialogThayDoiThongTin" class="dialog">
            <div class="dialog-content">
                <span class="close" onclick="dongDialog()">&times;</span>
                <h2>Thay Đổi Thông Tin Cá Nhân</h2>

                <div class="form-group">
                    <label for="txtHoTen">Họ và Tên:</label>
                    <asp:TextBox ID="txtHoTen" runat="server" CssClass="input-box"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtNgaySinh">Ngày Sinh:</label>
                    <asp:TextBox ID="txtNgaySinh" runat="server" CssClass="input-box" TextMode="Date"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Giới Tính:</label><br />
                    <asp:DropDownList ID="ddlGioiTinh" runat="server">
                        <asp:ListItem Text="Nam" Value="Nam"></asp:ListItem>
                        <asp:ListItem Text="Nữ" Value="Nữ"></asp:ListItem>
                    </asp:DropDownList>

                </div>

                <div class="form-group">
                    <label for="txtEmail">Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="input-box" TextMode="Email"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtDiaChi">Địa Chỉ:</label>
                    <asp:TextBox ID="txtDiaChi" runat="server" CssClass="input-box"></asp:TextBox>
                </div>

                <div class="dialog-footer">
                    <asp:Button ID="btnLuuThongTin" runat="server" Text="Lưu Thay Đổi" CssClass="btn-dialog" OnClick="btnLuuThongTin_Click" />
                    <button class="btn btn-cancel" onclick="dongDialog()">Hủy</button>
                </div>
            </div>
        </div>
        <!-- Dialog -->
        <!-- Dialog Đổi Mật Khẩu (ẩn mặc định) -->
        <div id="dialogDoiMatKhau" class="dialog">
            <div class="dialog-content">
                <span class="close" onclick="dongDialogDoiMatKhau()">&times;</span>
                <h2>Đổi Mật Khẩu</h2>

                <div class="form-group">
                    <label for="txtMatKhauCu">Mật khẩu cũ:</label>
                    <asp:TextBox ID="txtMatKhauCu" runat="server" CssClass="input-box" TextMode="Password"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtMatKhauMoi">Mật khẩu mới:</label>
                    <asp:TextBox ID="txtMatKhauMoi" runat="server" CssClass="input-box" TextMode="Password"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtXacNhanMK">Xác nhận mật khẩu mới:</label>
                    <asp:TextBox ID="txtXacNhanMK" runat="server" CssClass="input-box" TextMode="Password"></asp:TextBox>
                </div>

                <div class="dialog-footer">
                    <asp:Button ID="btnLuuMatKhau" runat="server" Text="Lưu" CssClass="btn-dialog" OnClick="btnLuuMatKhau_Click" />
                    <button class="btn btn-cancel" onclick="dongDialogDoiMatKhau()">Hủy</button>
                </div>
            </div>
        </div>


    </form>



</body>
</html>
<script>
    function moDialog() {
        // Lấy dữ liệu từ các Label có sẵn trên trang
        var hoTen = document.getElementById('<%= lblHoTen.ClientID %>').innerText;
        var ngaySinh = document.getElementById('<%= lblNgaySinh.ClientID %>').innerText;
        var gioiTinh = document.getElementById('<%= lblGioiTinh.ClientID %>').innerText;
        var email = document.getElementById('<%= lblEmail.ClientID %>').innerText;
        var diaChi = document.getElementById('<%= lblDiaChi.ClientID %>').innerText;

        // Gán dữ liệu vào các input của dialog
        document.getElementById('<%= txtHoTen.ClientID %>').value = hoTen;

        // Chuyển đổi ngày sinh về định dạng YYYY-MM-DD để hiển thị đúng trong input date
        var parts = ngaySinh.split("/");
        if (parts.length === 3) {
            var formattedDate = parts[2] + "-" + parts[1] + "-" + parts[0];
            document.getElementById('<%= txtNgaySinh.ClientID %>').value = formattedDate;
        }

        document.getElementById('<%= txtEmail.ClientID %>').value = email;
        document.getElementById('<%= txtDiaChi.ClientID %>').value = diaChi;

        // ✅ Gán giá trị cho DropDownList giới tính
        var ddlGioiTinh = document.getElementById('<%= ddlGioiTinh.ClientID %>');
        for (var i = 0; i < ddlGioiTinh.options.length; i++) {
            if (ddlGioiTinh.options[i].value.trim() === gioiTinh.trim()) {
                ddlGioiTinh.selectedIndex = i;
                break;
            }
        }

        // Hiển thị dialog
        document.getElementById("dialogThayDoiThongTin").style.display = "block";
    }

    function dongDialog() {
        document.getElementById("dialogThayDoiThongTin").style.display = "none";
    }
    function moDialogDoiMatKhau() {
        document.getElementById("dialogDoiMatKhau").style.display = "block";
    }

    function dongDialogDoiMatKhau() {
        document.getElementById("dialogDoiMatKhau").style.display = "none";
    }


</script>


