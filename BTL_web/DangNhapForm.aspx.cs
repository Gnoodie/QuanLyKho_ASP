using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_web
{
    public partial class DangNhapForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Gọi hàm kiểm tra đăng nhập và lấy thông tin
            string message = KiemTraDangNhap(email, password, out string chucVu, out int maNhanVien);

            if (!string.IsNullOrEmpty(message))
            {

                Session["UserEmail"] = email;
                Session["UserRole"] = chucVu;
                Session["UserID"] = maNhanVien;

                Response.Write($"<script>alert('{message}');</script>");

                if (chucVu == "Quản lý")
                {
                    Response.Redirect("QuanLyKho/TrangQuanLy.aspx");
                }
                else
                {
                    Response.Redirect("ThuKho/TrangThuKho.aspx"); // Trang mặc định cho nhân viên
                }
            }
            else
            {
                Response.Write("<script>alert('Email hoặc mật khẩu không đúng!');</script>");
            }
        }

        private string KiemTraDangNhap(string email, string password, out string chucVu, out int maNhanVien)
        {
            string connStr = "Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;";
            string message = "";
            chucVu = "";
            maNhanVien = 0;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaNhanVien, ChucVu FROM NhanVien WHERE Email = @Email AND MatKhau = @MatKhau";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@MatKhau", password); // Nên mã hóa mật khẩu

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Nếu tìm thấy tài khoản
                            {
                                maNhanVien = reader.GetInt32(0);
                                chucVu = reader.GetString(1);
                                message = $"Đăng nhập thành công!\\nMã nhân viên: {maNhanVien}\\nChức vụ: {chucVu}";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    message = $"Lỗi kết nối CSDL: {ex.Message}";
                }
            }

            return message;
        }
    }
}