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
    public partial class QLPhieu : System.Web.UI.Page
    {
        string connectionString = "Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadChiTietPhieu();
            }
        }

        private void LoadChiTietPhieu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT 
            p.MaPhieu,
            k.TenKho,
            p.Ngay,   
            h.TenHang,       
            h.DonVi,        
            h.DonGia,        
            cp.SoLuong,
            (cp.SoLuong * h.DonGia) AS ThanhTien,  
            p.LoaiPhieu,      
            dt.TenDoiTac,
            CASE 
                WHEN cp.TrangThai = 0 THEN N'Chưa Hoàn Thành'
                WHEN cp.TrangThai = 1 THEN N'Hoàn Thành'
            END AS TrangThai
        FROM ChiTietPhieu cp
        JOIN Phieu p ON cp.MaPhieu = p.MaPhieu
        JOIN Kho k ON p.MaKho = k.MaKho
        JOIN HangHoa h ON cp.MaHang = h.MaHang
        JOIN DoiTac dt ON p.MaDoiTac = dt.MaDoiTac
        WHERE (@LoaiPhieu = '' OR p.LoaiPhieu = @LoaiPhieu)
        AND (@TrangThai = '' OR cp.TrangThai = @TrangThai)
        AND cp.TrangThai != -1";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LoaiPhieu", ddlLoaiPhieu.SelectedValue);
                cmd.Parameters.AddWithValue("@TrangThai", ddlTrangThai.SelectedValue);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        protected void ddlLoaiPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChiTietPhieu();
        }

        protected void ddlTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChiTietPhieu();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string ngayChon = TextBox3.Text.Trim(); // Lấy ngày từ TextBox3 và loại bỏ khoảng trắng

            string query = @"
        SELECT 
            p.MaPhieu, p.LoaiPhieu, p.Ngay, 
            k.TenKho, dt.TenDoiTac, 
            hh.TenHang, hh.DonVi, hh.DonGia, 
            ct.SoLuong, ct.ThanhTien, 
            CASE 
                WHEN ct.TrangThai = 0 THEN N'Chưa Hoàn Thành' 
                WHEN ct.TrangThai = 1 THEN N'Hoàn Thành' 
            END AS TrangThai
        FROM Phieu p
        INNER JOIN Kho k ON p.MaKho = k.MaKho
        INNER JOIN DoiTac dt ON p.MaDoiTac = dt.MaDoiTac
        INNER JOIN ChiTietPhieu ct ON p.MaPhieu = ct.MaPhieu
        INNER JOIN HangHoa hh ON ct.MaHang = hh.MaHang
        WHERE ct.TrangThai != -1";

            // Nếu có ngày, thêm điều kiện lọc theo ngày
            if (!string.IsNullOrEmpty(ngayChon))
            {
                query += " AND CONVERT(date, p.Ngay) = @Ngay";
            }

            using (SqlConnection conn = new SqlConnection("Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;"))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(ngayChon))
                    {
                        cmd.Parameters.AddWithValue("@Ngay", DateTime.Parse(ngayChon)); // Chuyển đổi ngày sang DateTime
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }







        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeletePhieu")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];
                string maPhieu = row.Cells[0].Text; // Lấy MaPhieu từ cột đầu tiên

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE ChiTietPhieu SET TrangThai = -1 WHERE MaPhieu = @MaPhieu";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                LoadChiTietPhieu(); // Cập nhật lại danh sách sau khi xóa
            }
            if (e.CommandName == "EditPhieu")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];

                // Lấy MaPhieu từ cột đầu tiên của GridView
                string maPhieu = row.Cells[0].Text;

                // Chuyển hướng với MaPhieu truyền qua URL
                Response.Redirect("SuaPhieu.aspx?MaPhieu=" + maPhieu);
            }



        }
    }
}