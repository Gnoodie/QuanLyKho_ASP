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
    public partial class KiemKe : System.Web.UI.Page
    {
        private readonly string connectionString = "Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtFromDate.Text, txtToDate.Text);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            LoadData();
        }

        private void LoadData(string fromDate = "", string toDate = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT  
                    CASE WHEN p.LoaiPhieu = N'Nhập' THEN N'Phiếu Nhập' 
                         WHEN p.LoaiPhieu = N'Xuất' THEN N'Phiếu Xuất' 
                         ELSE N'Khác' END AS LoaiPhieu, 
                    hh.TenHang, cp.SoLuong,  cp.ThanhTien, p.Ngay
                FROM ChiTietPhieu cp
                JOIN Phieu p ON cp.MaPhieu = p.MaPhieu
                JOIN HangHoa hh ON cp.MaHang = hh.MaHang  
                WHERE (@fromDate = '' OR p.Ngay >= @fromDate)
                  AND (@toDate = '' OR p.Ngay <= @toDate)
                ORDER BY p.Ngay DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }

        protected void btnTinhTong_Click(object sender, EventArgs e)
        {
            decimal tongTienNhap = 0;
            decimal tongTienXuat = 0;

            foreach (GridViewRow row in GridView1.Rows)
            {
                string loaiPhieu = row.Cells[0].Text; // Cột Loại Phiếu
                decimal thanhTien;

                // Chuyển đổi giá trị cột Thành Tiền sang số
                if (decimal.TryParse(row.Cells[3].Text, out thanhTien))
                {
                    if (loaiPhieu.Contains("Nhập")) // Nếu là phiếu nhập
                    {
                        tongTienNhap += thanhTien;
                    }
                    else if (loaiPhieu.Contains("Xuất")) // Nếu là phiếu xuất
                    {
                        tongTienXuat += thanhTien;
                    }
                }
            }

            // Cập nhật dữ liệu lên label
            lblTongTienNhap.Text = "Tổng tiền đã chi: " + tongTienNhap.ToString("N0") + " VND";
            lblTongTienXuat.Text = "Tổng tiền đã thu: " + tongTienXuat.ToString("N0") + " VND";
        }


    }
}