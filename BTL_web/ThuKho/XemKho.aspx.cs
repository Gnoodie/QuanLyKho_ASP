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
    public partial class XemKho : System.Web.UI.Page
    {
        private readonly string connectionString = "Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadTenKho();
            }
        }

        private void LoadTenKho()
        {
            int maKho = Convert.ToInt32(Session["MaKho"]);
            string tenKho = "Kho Hàng Hóa"; // Mặc định nếu không tìm thấy

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT TenKho FROM Kho WHERE MaKho = @MaKho";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKho", maKho);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        tenKho = result.ToString();
                    }
                }
            }

            lblTenKho.Text = tenKho; // Hiển thị tên kho trên giao diện
        }

        private void LoadData(string searchTerm = "", int filterType = 0)
        {
            // Lấy MaKho từ Session
            int maKho = Convert.ToInt32(Session["MaKho"]);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT h.MaHang, h.TenHang, h.DonVi, h.SoLuong, h.DonGia, h.DuTruToiThieu, h.DuTruToiDa, k.TenKho
                FROM HangHoa h
                JOIN Kho k ON h.MaKho = k.MaKho
                WHERE h.MaKho = @MaKho AND (h.TenHang LIKE @searchTerm OR k.TenKho LIKE @searchTerm)";

                // Bộ lọc dựa trên số lượng tồn kho
                if (filterType == 1)
                {
                    query += " AND h.SoLuong >= h.DuTruToiDa"; // Hàng tồn nhiều hơn mức tối đa
                }
                else if (filterType == 2)
                {
                    query += " AND h.SoLuong <= h.DuTruToiThieu"; // Hàng tồn ít hơn mức tối thiểu
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKho", maKho);
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData(txtSearch.Text.Trim(), int.Parse(ddlKho.SelectedValue));
        }

        protected void ddlKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            int filterType = int.Parse(ddlKho.SelectedValue);
            LoadData(txtSearch.Text.Trim(), filterType);
        }
    }
}
