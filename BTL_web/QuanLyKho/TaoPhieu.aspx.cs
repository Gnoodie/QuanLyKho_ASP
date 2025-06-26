using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_web
{
    public partial class TaoPhieu : System.Web.UI.Page
    {
        private string connectionString = "Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadKho();
                LoadDoiTac();
            }
        }

        // 🏢 Load danh sách Kho
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
                    string displayText = $"{row["MaKho"]} - {row["TenKho"]}";
                    ddlKho.Items.Add(new ListItem(displayText, row["MaKho"].ToString()));
                }
            }
        }

        // 📦 Load danh sách hàng hóa khi chọn Kho
        protected void ddlKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTenHang();
        }

        private void LoadTenHang()
        {
            if (string.IsNullOrEmpty(ddlKho.SelectedValue)) return; // Nếu chưa chọn kho, không làm gì cả

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT HH.MaHang, HH.TenHang, HH.DonVi, HH.DonGia 
            FROM HangHoa HH
            JOIN Kho KH ON HH.MaKho = KH.MaKho
            WHERE KH.MaKho = @MaKho";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKho", ddlKho.SelectedValue);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlTenHang.Items.Clear();
                ddlTenHang.Items.Insert(0, new ListItem("-- Chọn hàng hóa --", ""));

                foreach (DataRow row in dt.Rows)
                {
                    string displayText = $"{row["MaHang"]} - {row["TenHang"]}";
                    string value = $"{row["MaHang"]}|{row["DonVi"]}|{row["DonGia"]}";
                    ddlTenHang.Items.Add(new ListItem(displayText, value));
                }
            }
        }


        // 🏷 Khi chọn hàng hóa => Hiển thị đơn vị và đơn giá
        protected void ddlTenHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTenHang.SelectedIndex > 0)
            {
                string[] parts = ddlTenHang.SelectedValue.Split('|');
                if (parts.Length == 3)
                {
                    TextBox4.Text = parts[1]; // Đơn vị
                    TextBox6.Text = parts[2]; // Đơn giá
                }
            }
            else
            {
                TextBox4.Text = "";
                TextBox6.Text = "";
            }
        }

        // 🧮 Tính thành tiền khi nhập số lượng
        protected void TinhThanhTien(object sender, EventArgs e)
        {
            if (decimal.TryParse(TextBox5.Text, out decimal soLuong) && decimal.TryParse(TextBox6.Text, out decimal donGia))
            {
                decimal thanhTien = soLuong * donGia;
                TextBox7.Text = thanhTien.ToString("N0");
            }
            else
            {
                TextBox7.Text = "";
            }
        }

        // 🏢 Cập nhật danh sách đối tác khi chọn phiếu nhập/xuất
        private void UpdateDoiTac()
        {
            string loaiDoiTac = rdbNhap.Checked ? "Nhà Cung Cấp" : "Đại Lý";
            LoadDoiTac(loaiDoiTac);
        }

        protected void rdbNhap_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDoiTac();
        }

        protected void rdbXuat_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDoiTac();
        }

        // 📄 Load danh sách đối tác
        private void LoadDoiTac()
        {
            LoadDoiTac("Nhà Cung Cấp"); // Mặc định là Nhà Cung Cấp
        }

        private void LoadDoiTac(string loaiDoiTac)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaDoiTac, TenDoiTac FROM DoiTac WHERE LoaiDoiTac = @LoaiDoiTac";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LoaiDoiTac", loaiDoiTac);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlDoiTac.Items.Clear();
                ddlDoiTac.Items.Insert(0, new ListItem("-- Chọn đối tác --", ""));

                foreach (DataRow row in dt.Rows)
                {
                    string displayText = $"{row["MaDoiTac"]} - {row["TenDoiTac"]}";
                    ddlDoiTac.Items.Add(new ListItem(displayText, row["MaDoiTac"].ToString()));
                }
            }
        }

        // ✅ Xử lý nút Submit
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlKho.SelectedValue) ||
                string.IsNullOrEmpty(ddlTenHang.SelectedValue) ||
                string.IsNullOrEmpty(TextBox5.Text) ||
                string.IsNullOrEmpty(ddlDoiTac.SelectedValue))
            {
                // Nếu thiếu thông tin, có thể hiển thị thông báo lỗi tại đây
                return;
            }

            string maKho = ddlKho.SelectedValue;
            string tenKho = ddlKho.SelectedItem.Text;
            string maDoiTac = ddlDoiTac.SelectedValue;
            string tenDoiTac = ddlDoiTac.SelectedItem.Text;
            string loaiPhieu = rdbNhap.Checked ? "Nhập" : "Xuất";
            string ngay = TextBox2.Text;
            string maHang = ddlTenHang.SelectedValue.Split('|')[0];
            string tenHang = ddlTenHang.SelectedItem.Text;
            string donVi = TextBox4.Text;
            string soLuong = TextBox5.Text;
            string donGia = TextBox6.Text;
            string thanhTien = TextBox7.Text;

            // Chuyển hướng đến trang xác nhận và truyền dữ liệu qua QueryString
            string url = $"Confirm.aspx?MaKho={maKho}&TenKho={tenKho}&MaDoiTac={maDoiTac}&TenDoiTac={tenDoiTac}&LoaiPhieu={loaiPhieu}&Ngay={ngay}&MaHang={maHang}&TenHang={tenHang}&DonVi={donVi}&SoLuong={soLuong}&DonGia={donGia}&ThanhTien={thanhTien}";

            Response.Redirect(url);
        }
    }
}
