using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace BTL_web
{
    public partial class Confirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMaKho.Text = Request.QueryString["MaKho"];
                lblTenKho.Text = ExtractName(Request.QueryString["TenKho"]);
                lblMaDoiTac.Text = Request.QueryString["MaDoiTac"];
                lblTenDoiTac.Text = ExtractName(Request.QueryString["TenDoiTac"]);
                lblLoaiPhieu.Text = Request.QueryString["LoaiPhieu"];
                lblNgay.Text = Request.QueryString["Ngay"];
                lblMaHang.Text = Request.QueryString["MaHang"];
                lblTenHang.Text = ExtractName(Request.QueryString["TenHang"]);
                lblDonVi.Text = Request.QueryString["DonVi"];
                lblSoLuong.Text = Request.QueryString["SoLuong"];
                lblDonGia.Text = Request.QueryString["DonGia"];
                lblThanhTien.Text = Request.QueryString["ThanhTien"];
            }
        }

        private string ExtractName(string input)
        {
            if (!string.IsNullOrEmpty(input) && input.Contains("-"))
            {
                return input.Substring(input.IndexOf("-") + 1).Trim();
            }
            return input;
        }

        protected void btnXacNhan_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=LAPTOP-8VS68C7J;Database=QuanLyKho;Integrated Security=True;";

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(lblMaKho.Text) || string.IsNullOrEmpty(lblMaDoiTac.Text) || string.IsNullOrEmpty(lblLoaiPhieu.Text))
            {
                Response.Write("<script>alert('Lỗi: Dữ liệu không hợp lệ!');</script>");
                return;
            }

            DateTime ngayPhieu;
            if (!DateTime.TryParseExact(lblNgay.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngayPhieu))
            {
                Response.Write("<script>alert('Lỗi: Ngày không hợp lệ!');</script>");
                return;
            }

            int soLuong;
            decimal thanhTien;
            if (!int.TryParse(lblSoLuong.Text, out soLuong) || soLuong <= 0)
            {
                Response.Write("<script>alert('Lỗi: Số lượng không hợp lệ!');</script>");
                return;
            }
            if (!decimal.TryParse(lblThanhTien.Text, out thanhTien) || thanhTien <= 0)
            {
                Response.Write("<script>alert('Lỗi: Thành tiền không hợp lệ!');</script>");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Thêm vào bảng Phieu
                string queryPhieu = "INSERT INTO Phieu (MaKho, MaDoiTac, LoaiPhieu, Ngay) OUTPUT INSERTED.MaPhieu " +
                    "VALUES (@MaKho, @MaDoiTac, @LoaiPhieu, @Ngay)";

                SqlCommand cmdPhieu = new SqlCommand(queryPhieu, conn);
                cmdPhieu.Parameters.Add("@MaKho", SqlDbType.VarChar, 50).Value = lblMaKho.Text;
                cmdPhieu.Parameters.Add("@MaDoiTac", SqlDbType.VarChar, 50).Value = lblMaDoiTac.Text;
                cmdPhieu.Parameters.Add("@LoaiPhieu", SqlDbType.NVarChar, 50).Value = lblLoaiPhieu.Text;

                DateTime ngayValue;
                if (!DateTime.TryParse(lblNgay.Text, out ngayValue))
                {
                    Response.Write("<script>alert('Ngày không hợp lệ!');</script>");
                    return;
                }
                cmdPhieu.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngayValue;


                int maPhieu = (int)cmdPhieu.ExecuteScalar();

                // Thêm vào bảng ChiTietPhieu
                string queryChiTiet = "INSERT INTO ChiTietPhieu (MaPhieu, MaHang, SoLuong, ThanhTien) VALUES (@MaPhieu, @MaHang, @SoLuong, @ThanhTien)";
                SqlCommand cmdChiTiet = new SqlCommand(queryChiTiet, conn);
                cmdChiTiet.Parameters.Add("@MaPhieu", SqlDbType.Int).Value = maPhieu;
                cmdChiTiet.Parameters.Add("@MaHang", SqlDbType.VarChar, 50).Value = lblMaHang.Text;
                cmdChiTiet.Parameters.Add("@SoLuong", SqlDbType.Int).Value = soLuong;
                cmdChiTiet.Parameters.Add("@ThanhTien", SqlDbType.Decimal).Value = thanhTien;

                cmdChiTiet.ExecuteNonQuery();
            }

            // Thông báo thành công
            Response.Write("<script>alert('Nhập kho thành công!'); window.location='TaoPhieu.aspx';</script>");
        }
    }
}
