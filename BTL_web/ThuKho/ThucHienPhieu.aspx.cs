using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_web
{
    public partial class ThucHienPhieu : System.Web.UI.Page
    {
        private string connStr = "Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhSachPhieu();
            }
        }

        private void LoadDanhSachPhieu()
        {
            if (Session["MaKho"] == null)
            {
                Response.Redirect("DangNhapForm.aspx"); // Điều hướng nếu chưa đăng nhập
                return;
            }

            int maKho = Convert.ToInt32(Session["MaKho"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"
    SELECT p.MaPhieu, p.LoaiPhieu, ctp.MaHang, h.TenHang, ctp.SoLuong, ctp.TrangThai
    FROM Phieu p
    JOIN ChiTietPhieu ctp ON p.MaPhieu = ctp.MaPhieu
    JOIN HangHoa h ON ctp.MaHang = h.MaHang
    WHERE p.MaKho = @MaKho AND ctp.TrangThai <> -1";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKho", maKho);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        gvPhieu.DataSource = dt;
                        gvPhieu.DataBind();
                    }
                }
            }
        }

        protected void gvPhieu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ThucHien")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvPhieu.Rows[rowIndex];

                int maPhieu = Convert.ToInt32(row.Cells[0].Text);
                string loaiPhieu = row.Cells[1].Text;
                int maHang = Convert.ToInt32(row.Cells[2].Text);
                int soLuong = Convert.ToInt32(row.Cells[4].Text);

                CapNhatSoLuongHangHoa(maPhieu, maHang, soLuong, loaiPhieu);
                LoadDanhSachPhieu();
            }
        }

        private void CapNhatSoLuongHangHoa(int maPhieu, int maHang, int soLuong, string loaiPhieu)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string updateKhoQuery = loaiPhieu == "Nhập" ?
                        "UPDATE HangHoa SET SoLuong = SoLuong + @SoLuong WHERE MaHang = @MaHang" :
                        "UPDATE HangHoa SET SoLuong = SoLuong - @SoLuong WHERE MaHang = @MaHang";

                    using (SqlCommand cmdKho = new SqlCommand(updateKhoQuery, conn, transaction))
                    {
                        cmdKho.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdKho.Parameters.AddWithValue("@MaHang", maHang);
                        cmdKho.ExecuteNonQuery();
                    }

                    string updateTrangThaiQuery = "UPDATE ChiTietPhieu SET TrangThai = 1 WHERE MaPhieu = @MaPhieu AND MaHang = @MaHang";
                    using (SqlCommand cmdTrangThai = new SqlCommand(updateTrangThaiQuery, conn, transaction))
                    {
                        cmdTrangThai.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        cmdTrangThai.Parameters.AddWithValue("@MaHang", maHang);
                        cmdTrangThai.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Thực hiện phiếu thành công!');", true);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi: " + ex.Message + "');", true);
                }
            }
        }

    }
}