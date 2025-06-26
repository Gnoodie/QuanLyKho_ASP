using System;
using System.Web;
using System.Web.UI;

namespace BTL_web
{
    public partial class DangXuat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Xóa tất cả session khi đăng xuất
            Session.Clear();  // Xóa tất cả biến session
            Session.Abandon(); // Hủy bỏ session hiện tại

            // Chuyển hướng về trang đăng nhập
            Response.Redirect("DangNhapForm.aspx");
        }
    }
}
