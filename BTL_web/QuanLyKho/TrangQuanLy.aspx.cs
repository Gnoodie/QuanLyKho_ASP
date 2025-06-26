using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_web
{
    public partial class TrangQuanLy : System.Web.UI.Page
    {
        string connStr = "Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadThongTinNhanVien();
            }
        }

        private void LoadThongTinNhanVien()
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("/DangNhapForm.aspx"); // Nếu chưa đăng nhập, chuyển hướng về trang đăng nhập
                return;
            }

            int maNhanVien = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaNhanVien, Ten, GioiTinh, NgaySinh, Email, DiaChi, ChucVu FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Nếu tìm thấy nhân viên
                            {
                                lblHoTen.Text = reader["Ten"].ToString();
                                lblGioiTinh.Text = reader["GioiTinh"].ToString();
                                lblNgaySinh.Text = Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy");
                                lblEmail.Text = reader["Email"].ToString();
                                lblDiaChi.Text = reader["DiaChi"].ToString();
                                lblChucVu.Text = reader["ChucVu"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Lỗi kết nối CSDL: {ex.Message}');</script>");
                }
            }
        }

        protected void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            int maNhanVien = Convert.ToInt32(Session["UserID"]);
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            DateTime ngaySinh;

            // Kiểm tra ngày sinh hợp lệ
            bool isValidDate = DateTime.TryParseExact(txtNgaySinh.Text, "yyyy-MM-dd",
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out ngaySinh);

            if (!isValidDate)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Ngày sinh không hợp lệ!');", true);
                return;
            }

            // ✅ Lấy giá trị giới tính từ DropDownList
            string gioiTinh = ddlGioiTinh.SelectedValue;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE NhanVien 
                             SET Ten = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, 
                                 Email = @Email, DiaChi = @DiaChi 
                             WHERE MaNhanVien = @MaNhanVien";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                        cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh); // Dữ liệu từ DropDownList
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                        cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Cập nhật thành công!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Không có bản ghi nào được cập nhật!');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối CSDL: " + ex.Message + "');", true);
                }
            }

            // Gọi lại hàm LoadThongTinNhanVien để cập nhật lại thông tin trên giao diện
            LoadThongTinNhanVien();
        }

        protected void btnLuuMatKhau_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các TextBox
            string matKhauCu = txtMatKhauCu.Text.Trim();
            string matKhauMoi = txtMatKhauMoi.Text.Trim();
            string xacNhanMK = txtXacNhanMK.Text.Trim();

            // Kiểm tra mật khẩu mới và xác nhận mật khẩu
            if (matKhauMoi != xacNhanMK)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Mật khẩu mới và xác nhận không khớp!');", true);
                return;
            }

            // Lấy thông tin nhân viên từ Session
            if (Session["UserID"] == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng đăng nhập lại!');", true);
                return;
            }

            int maNhanVien = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Kiểm tra mật khẩu cũ có đúng không
                string queryCheck = "SELECT MatKhau FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    object currentPassword = cmdCheck.ExecuteScalar();

                    if (currentPassword == null || currentPassword.ToString() != matKhauCu)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Mật khẩu cũ không chính xác!');", true);
                        return;
                    }
                }

                // Cập nhật mật khẩu mới
                string queryUpdate = "UPDATE NhanVien SET MatKhau = @MatKhauMoi WHERE MaNhanVien = @MaNhanVien";
                using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
                    cmdUpdate.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmdUpdate.ExecuteNonQuery();
                }

                // Hiển thị thông báo thành công
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Mật khẩu đã được cập nhật thành công!');", true);
            }
        }





    }
}