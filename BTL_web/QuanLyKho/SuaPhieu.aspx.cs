using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_web
{
    public partial class SuaPhieu : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;");


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                LoadKho();
                LoadLoaiPhieu();
                
                LoadHangHoa();
                string maPhieu = Request.QueryString["MaPhieu"];
                if (!string.IsNullOrEmpty(maPhieu))
                {
                    txtMaPhieu.Text = maPhieu;
                    LoadPhieu(maPhieu);
                    LoadChiTietPhieu(maPhieu);
                }
            }
        }

        private void LoadPhieu(string maPhieu)
        {
            using (SqlConnection conn = new SqlConnection("Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;"))
            {
                conn.Open();
                string query = "SELECT MaKho, LoaiPhieu, MaDoiTac, Ngay FROM Phieu WHERE MaPhieu = @MaPhieu";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ddlMaKho.SelectedValue = reader["MaKho"].ToString();
                        string loaiPhieu = reader["LoaiPhieu"].ToString();
                        ddlLoaiPhieu.SelectedValue = loaiPhieu;
                        txtNgay.Text = Convert.ToDateTime(reader["Ngay"]).ToString("yyyy-MM-dd");

                        string maDoiTac = reader["MaDoiTac"].ToString();
                        reader.Close(); // Đóng reader trước khi gọi hàm khác

                        // Gọi LoadDoiTac để chọn đúng đối tác
                        LoadDoiTac(loaiPhieu, maDoiTac);
                    }
                }
            }
        }


        private void LoadChiTietPhieu(string maPhieu)
        {
            using (SqlConnection conn = new SqlConnection("Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;"))
            {
                conn.Open();
                string query = @"
            SELECT ct.*, hh.DonVi, hh.DonGia, hh.MaKho 
            FROM ChiTietPhieu ct 
            JOIN HangHoa hh ON ct.MaHang = hh.MaHang
            WHERE ct.MaPhieu = @MaPhieu";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string maKho = reader["MaKho"].ToString();  // Lấy mã kho
                        string maHang = reader["MaHang"].ToString(); // Lấy mã hàng

                        ddlMaKho.SelectedValue = maKho; // Cập nhật ddlMaKho nếu có
                        LoadHangHoa(); // Gọi LoadHangHoa để cập nhật ddlTenHang theo kho

                        // Gán mã hàng vào ddlTenHang sau khi LoadHangHoa chạy xong
                        if (ddlTenHang.Items.FindByValue(maHang) != null)
                        {
                            ddlTenHang.SelectedValue = maHang;
                        }

                        txtSoLuong.Text = reader["SoLuong"].ToString();
                        txtThanhTien.Text = reader["ThanhTien"].ToString();
                        txtDonVi.Text = reader["DonVi"].ToString();
                        txtDonGia.Text = reader["DonGia"].ToString();
                    }
                }
            }
        }


        // 🏠 Load danh sách kho vào dropdown
        private void LoadKho()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT MaKho, TenKho FROM Kho", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            ddlMaKho.DataSource = dr;
            ddlMaKho.DataTextField = "TenKho";
            ddlMaKho.DataValueField = "MaKho";
            ddlMaKho.DataBind();
            conn.Close();
        }

        // 🏠 Load loại phiếu (Nhập - Xuất)
        private void LoadLoaiPhieu()
        {
            ddlLoaiPhieu.Items.Clear();
            ddlLoaiPhieu.Items.Add(new ListItem("Nhập", "Nhập"));
            ddlLoaiPhieu.Items.Add(new ListItem("Xuất", "Xuất"));
        }

        // 🏠 Load thông tin phiếu vào form khi sửa


        private void LoadDoiTac(string loaiPhieu, string selectedDoiTac)
        {
            using (SqlConnection conn = new SqlConnection("Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;"))
            {
                conn.Open();
                string loaiDoiTac = (loaiPhieu == "Nhập") ? "Nhà cung cấp" : "Đại lý";

                string query = "SELECT MaDoiTac, TenDoiTac FROM DoiTac WHERE LoaiDoiTac = @LoaiDoiTac";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LoaiDoiTac", loaiDoiTac);
                    SqlDataReader dr = cmd.ExecuteReader();

                    ddlDoiTac.DataSource = dr;
                    ddlDoiTac.DataTextField = "TenDoiTac";
                    ddlDoiTac.DataValueField = "MaDoiTac";
                    ddlDoiTac.DataBind();
                    dr.Close();
                }
                conn.Close();
            }

            // Đặt giá trị đã chọn
            if (!string.IsNullOrEmpty(selectedDoiTac))
            {
                ddlDoiTac.SelectedValue = selectedDoiTac;
            }
        }


        private void LoadHangHoa()
        {
            string selectedHang = ddlTenHang.SelectedValue; // Lưu hàng đã chọn
            string maKho = ddlMaKho.SelectedValue;

            // Kiểm tra nếu chưa chọn kho thì xóa danh sách hàng hóa và thoát
            if (string.IsNullOrEmpty(maKho))
            {
                ddlTenHang.Items.Clear();
                ddlTenHang.Items.Add(new ListItem("-- Chọn hàng hóa --", ""));
                return;
            }

            using (SqlConnection conn = new SqlConnection("Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
            SELECT MaHang, TenHang, DonVi, DonGia 
            FROM HangHoa 
            WHERE MaKho = @MaKho", conn))
                {
                    cmd.Parameters.AddWithValue("@MaKho", maKho);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Xóa danh sách cũ và thêm lựa chọn mặc định
                        ddlTenHang.Items.Clear();
                        ddlTenHang.Items.Add(new ListItem("-- Chọn hàng hóa --", ""));

                        ddlTenHang.DataSource = dt;
                        ddlTenHang.DataTextField = "TenHang";
                        ddlTenHang.DataValueField = "MaHang";
                        ddlTenHang.DataBind();
                    }
                }
            }

            // Giữ lại giá trị đã chọn nếu nó có trong danh sách
            if (!string.IsNullOrEmpty(selectedHang) && ddlTenHang.Items.FindByValue(selectedHang) != null)
            {
                ddlTenHang.SelectedValue = selectedHang;
            }
        }


        // 🏠 Khi chọn kho -> Load danh sách hàng hóa trong kho
        protected void ddlMaKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT MaHang, TenHang FROM HangHoa WHERE MaKho = @MaKho", conn);
            cmd.Parameters.AddWithValue("@MaKho", ddlMaKho.SelectedValue);
            SqlDataReader dr = cmd.ExecuteReader();
            ddlTenHang.DataSource = dr;
            ddlTenHang.DataTextField = "TenHang";
            ddlTenHang.DataValueField = "MaHang";
            ddlTenHang.DataBind();
            conn.Close();
        }


        // 🏠 Khi chọn hàng -> Load đơn giá, đơn vị
        protected void ddlTenHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDonViDonGia();
        }
        private void UpdateDonViDonGia()
        {
            if (ddlTenHang.SelectedValue != null && ddlMaKho.SelectedValue != null)
            {
                string selectedMaHang = ddlTenHang.SelectedValue;
                string selectedMaKho = ddlMaKho.SelectedValue;

                using (SqlConnection conn = new SqlConnection("Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;"))
                {
                    conn.Open();
                    string query = "SELECT DonVi, DonGia FROM HangHoa WHERE MaHang = @MaHang AND MaKho = @MaKho";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaHang", selectedMaHang);
                    cmd.Parameters.AddWithValue("@MaKho", selectedMaKho);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read()) // Nếu tìm thấy hàng hóa trong kho đã chọn
                    {
                        txtDonVi.Text = dr["DonVi"].ToString();
                        txtDonGia.Text = dr["DonGia"].ToString();
                    }
                    else
                    {
                        txtDonVi.Text = "";
                        txtDonGia.Text = "";
                    }
                    conn.Close();
                }
            }
        }





        // 🏠 Khi nhập số lượng -> Tính thành tiền
        protected void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            UpdateThanhTien();
        }

        private void UpdateThanhTien()
        {
            decimal donGia = 0, soLuong = 0;

            decimal.TryParse(txtDonGia.Text, out donGia);
            decimal.TryParse(txtSoLuong.Text, out soLuong);

            txtThanhTien.Text = (donGia * soLuong).ToString("N0"); // Format số tiền
        }


        // 🏠 Khi chọn loại phiếu -> Load đối tác phù hợp
        protected void ddlLoaiPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loaiPhieu = ddlLoaiPhieu.SelectedValue;
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT MaDoiTac, TenDoiTac FROM DoiTac WHERE LoaiDoiTac = @LoaiDoiTac", conn);
            cmd.Parameters.AddWithValue("@LoaiDoiTac", loaiPhieu == "Nhập" ? "Nhà cung cấp" : "Đại lý");
            SqlDataReader dr = cmd.ExecuteReader();
            ddlDoiTac.DataSource = dr;
            ddlDoiTac.DataTextField = "TenDoiTac";
            ddlDoiTac.DataValueField = "MaDoiTac";
            ddlDoiTac.DataBind();
            conn.Close();
        }

        // 🏠 Cập nhật phiếu vào database
        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // Cập nhật bảng Phieu
                SqlCommand cmdPhieu = new SqlCommand("UPDATE Phieu SET MaKho=@MaKho, MaDoiTac=@MaDoiTac, LoaiPhieu=@LoaiPhieu, Ngay=@Ngay WHERE MaPhieu=@MaPhieu", conn);
                cmdPhieu.Parameters.AddWithValue("@MaKho", ddlMaKho.SelectedValue);
                cmdPhieu.Parameters.AddWithValue("@MaDoiTac", ddlDoiTac.SelectedValue);
                cmdPhieu.Parameters.AddWithValue("@LoaiPhieu", ddlLoaiPhieu.SelectedValue);
                cmdPhieu.Parameters.AddWithValue("@Ngay", txtNgay.Text);
                cmdPhieu.Parameters.AddWithValue("@MaPhieu", txtMaPhieu.Text);
                cmdPhieu.ExecuteNonQuery();

                // Cập nhật bảng ChiTietPhieu
                SqlCommand cmdChiTiet = new SqlCommand("UPDATE ChiTietPhieu SET MaHang=@MaHang, SoLuong=@SoLuong, ThanhTien=@ThanhTien WHERE MaPhieu=@MaPhieu", conn);
                cmdChiTiet.Parameters.AddWithValue("@MaHang", ddlTenHang.SelectedValue);
                cmdChiTiet.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));
                cmdChiTiet.Parameters.AddWithValue("@ThanhTien", int.Parse(txtThanhTien.Text.Replace(",", "")));
                cmdChiTiet.Parameters.AddWithValue("@MaPhieu", txtMaPhieu.Text);
                cmdChiTiet.ExecuteNonQuery();

                conn.Close();

                // Hiển thị thông báo và chuyển hướng
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cập nhật thành công!'); window.location='QLPhieu.aspx';", true);
            }
            catch (Exception ex)
            {
                conn.Close();
                ClientScript.RegisterStartupScript(this.GetType(), "error", "alert('Lỗi: " + ex.Message + "');", true);
            }
        }


    }
}