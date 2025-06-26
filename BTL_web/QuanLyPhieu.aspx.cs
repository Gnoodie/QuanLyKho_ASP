using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_web
{
    public partial class QuanLyPhieu : System.Web.UI.Page
    {
        string connStr = "Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPhieu();
            }
        }

        private void LoadPhieu()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                SELECT p.MaPhieu, k.TenKho, p.Ngay, p.LoaiPhieu, dt.TenDoiTac, 
                       (CASE WHEN ct.TrangThai = 0 THEN N'Chưa Xong' 
                             WHEN ct.TrangThai = 1 THEN N'Hoàn Thành' 
                             ELSE N'Đã Hủy' END) AS TrangThai
                FROM Phieu p
                JOIN Kho k ON p.MaKho = k.MaKho
                JOIN DoiTac dt ON p.MaDoiTac = dt.MaDoiTac
                LEFT JOIN ChiTietPhieu ct ON p.MaPhieu = ct.MaPhieu";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvPhieu.DataSource = dt;
                    gvPhieu.DataBind();
                }
            }
        }

        protected void gvPhieu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditPhieu")
            {
                Response.Redirect("SuaPhieu.aspx?MaPhieu=" + e.CommandArgument);
            }
            else if (e.CommandName == "DeletePhieu")
            {
                int maPhieu = Convert.ToInt32(e.CommandArgument);
                DeletePhieu(maPhieu);
            }
        }

        private void DeletePhieu(int maPhieu)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE ChiTietPhieu SET TrangThai = -1 WHERE MaPhieu = @MaPhieu";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            LoadPhieu();  // Cập nhật lại danh sách sau khi xóa
        }
    }
}