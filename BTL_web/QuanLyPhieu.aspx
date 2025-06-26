<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuanLyPhieu.aspx.cs" Inherits="BTL_web.QuanLyPhieu" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Quản Lý Phiếu</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2 class="text-center">Quản Lý Phiếu</h2>
            <asp:GridView ID="gvPhieu" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" 
                OnRowCommand="gvPhieu_RowCommand">
                <Columns>
                    <asp:BoundField DataField="MaPhieu" HeaderText="Mã Phiếu" />
                    <asp:BoundField DataField="TenKho" HeaderText="Tên Kho" />
                    <asp:BoundField DataField="Ngay" HeaderText="Ngày" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="LoaiPhieu" HeaderText="Loại Phiếu" />
                    <asp:BoundField DataField="TenDoiTac" HeaderText="Tên Đối Tác" />
                    <asp:BoundField DataField="TrangThai" HeaderText="Trạng Thái" />

                    <asp:TemplateField HeaderText="Thao Tác">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary btn-sm"
                                Text="Sửa" CommandName="EditPhieu" CommandArgument='<%# Eval("MaPhieu") %>' />
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm"
                                Text="Xóa" CommandName="DeletePhieu" CommandArgument='<%# Eval("MaPhieu") %>'
                                OnClientClick="return confirm('Bạn có chắc chắn muốn xóa không?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>