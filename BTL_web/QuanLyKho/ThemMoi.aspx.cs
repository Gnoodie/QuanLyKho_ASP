using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_web
{
    public partial class ThemMoi : System.Web.UI.Page
    {
        private string connectionString = "Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadKho();
            }
        }

        private void LoadKho()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaKho, TenKho FROM Kho";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlKho.Items.Clear();
                ddlKho.Items.Insert(0, new ListItem("-- Chọn kho --", ""));

                foreach (DataRow row in dt.Rows)
                {
                    string displayText = row["MaKho"].ToString() + " - " + row["TenKho"].ToString();
                    ddlKho.Items.Add(new ListItem(displayText, row["MaKho"].ToString()));
                }
            }
        }

        protected void btnThemHang_Click(object sender, EventArgs e)
        {
            string tenHang = txtTenHang.Text.Trim();
            string donVi = txtDonVi.Text.Trim();
            decimal donGia;
            int maKho;
            int duTruToiDa;
            int duTruToiThieu;

            if (string.IsNullOrEmpty(tenHang) || string.IsNullOrEmpty(donVi) ||
                !decimal.TryParse(txtDonGia.Text, out donGia) ||
                !int.TryParse(ddlKho.SelectedValue, out maKho) ||
                !int.TryParse(txtDuTruToiDa.Text, out duTruToiDa) ||
                !int.TryParse(txtDuTruToiThieu.Text, out duTruToiThieu))
            {
                Response.Write("<script>alert('Vui lòng nhập đầy đủ và đúng thông tin');</script>");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string insertHangHoa = "INSERT INTO HangHoa (TenHang, DonVi, SoLuong, DonGia, MaKho, DuTruToiDa, DuTruToiThieu) VALUES (@TenHang, @DonVi, 0, @DonGia, @MaKho, @DuTruToiDa, @DuTruToiThieu)";

                using (SqlCommand cmd = new SqlCommand(insertHangHoa, conn))
                {
                    cmd.Parameters.AddWithValue("@TenHang", tenHang);
                    cmd.Parameters.AddWithValue("@DonVi", donVi);
                    cmd.Parameters.AddWithValue("@DonGia", donGia);
                    cmd.Parameters.AddWithValue("@MaKho", maKho);
                    cmd.Parameters.AddWithValue("@DuTruToiDa", duTruToiDa);
                    cmd.Parameters.AddWithValue("@DuTruToiThieu", duTruToiThieu);

                    cmd.ExecuteNonQuery();
                }
            }

            Response.Write("<script>alert('Thêm hàng thành công!');</script>");
        }
    }
}
