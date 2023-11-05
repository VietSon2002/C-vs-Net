using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace _20t1020242
{
    public partial class FormChiTietSach : Form
    {
        private string fileName = "20t1020242.json"; // Specify the JSON file name here
        private List<DanhSach> danhSachCu;
        public FormChiTietSach()
        {
            InitializeComponent();
        }

        private void picHinhDaiDien_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Chọn ảnh địa diện",
                Filter = "Image Files|*.jpg;*.jpeg;*.png",
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedFileName = openFileDialog.FileName;
                picHinhDaiDien.ImageLocation = selectedFileName;
            }
        }



        private void btnThemSach_Click(object sender, EventArgs e)
        {
            string maSachMoi = textMaSach.Text.Trim();

            if (string.IsNullOrEmpty(maSachMoi))
            {
                MessageBox.Show("Vui lòng nhập mã sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra xem mã sách đã tồn tại hay chưa
            if (danhSachCu.Any(s => s.MaSach == maSachMoi))
            {
                MessageBox.Show("Mã sách đã tồn tại. Vui lòng chọn mã sách khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Đọc dữ liệu hiện có từ tệp JSON vào một danh sách
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                danhSachCu = JsonConvert.DeserializeObject<List<DanhSach>>(json);

                if (danhSachCu == null)
                {
                    danhSachCu = new List<DanhSach>();
                }
            }
            else
            {
                danhSachCu = new List<DanhSach>();
            }

            // Tạo một đối tượng DanhSach mới từ dữ liệu trên giao diện người dùng
            var sach = new DanhSach
            {
                MaSach = maSachMoi,
                TenSach = textTenSach.Text,
                NamXuatBan = dateNgayXuatBan.Value,
                NhaXuatBan = textNhaXuatBan.Text,
                GiaNhap = textGiaNhap.Text,
                GiaBan = textGiaBan.Text,
                SoLuong = texSoLuong.Text,
                HinhSach = picHinhDaiDien.ImageLocation
            };

            // Thêm đối tượng mới vào danh sách đã đọc từ tệp JSON
            danhSachCu.Add(sach);

            // Ghi đè tệp JSON với danh sách đã cập nhật
            File.WriteAllText(fileName, JsonConvert.SerializeObject(danhSachCu));

            MessageBox.Show("Đã thêm sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dateNgaySinh_ValueChanged(object sender, EventArgs e)
        {
            // You can add code here if needed
        }

        private void FormChiTietSach_Load(object sender, EventArgs e)
        {
            // Đọc dữ liệu từ tệp JSON và gán cho biến danhSachCu
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                danhSachCu = JsonConvert.DeserializeObject<List<DanhSach>>(json);

                if (danhSachCu == null)
                {
                    danhSachCu = new List<DanhSach>();
                }
            }
            else
            {
                danhSachCu = new List<DanhSach>();
            }

            // ...
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            string maSachCanXoa = textMaSach.Text.Trim(); // Lấy mã sách cần xóa từ TextBox hoặc điều kiện khác

            if (string.IsNullOrEmpty(maSachCanXoa))
            {
                MessageBox.Show("Vui lòng nhập mã sách cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Ngừng thực hiện xóa sách nếu mã sách trống
            }
            // Tạo danh sách để lưu danh sách đã cập nhật
            List<DanhSach> danhSachMoi = new List<DanhSach>();

            bool timThay = false;

            // Lặp qua danh sách sách hiện có
            foreach (var sach in danhSachCu)
            {
                if (sach.MaSach == maSachCanXoa)
                {
                    // Nếu tìm thấy cuốn sách cần xóa, bỏ qua nó
                    timThay = true;
                    continue;
                }

                // Nếu không tìm thấy cuốn sách, thêm nó vào danh sách đã cập nhật
                danhSachMoi.Add(sach);
            }

            if (timThay)
            {
                // Nếu tìm thấy sách và xóa thành công
                // Cập nhật danh sách và ghi vào tệp JSON
                File.WriteAllText(fileName, JsonConvert.SerializeObject(danhSachMoi));

                // Hiển thị thông báo khi xóa thành công
                MessageBox.Show("Đã xóa sách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Nếu không tìm thấy sách
                MessageBox.Show("Không tìm thấy sách cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDSChiTiet_Click(object sender, EventArgs e)
        {
            FormDanhSach formDanhSach = new FormDanhSach();
            formDanhSach.Show();
        }

        private void textMaSach_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string maSachCanCapNhat = textMaSach.Text.Trim();

            if (string.IsNullOrEmpty(maSachCanCapNhat))
            {
                MessageBox.Show("Vui lòng nhập mã sách cần cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DanhSach sachCanCapNhat = danhSachCu.FirstOrDefault(s => s.MaSach == maSachCanCapNhat);

            if (sachCanCapNhat != null)
            {
                // Kiểm tra từng trường dữ liệu và cập nhật nếu thay đổi
                if (sachCanCapNhat.TenSach != textTenSach.Text)
                {
                    sachCanCapNhat.TenSach = textTenSach.Text;
                }
                if (sachCanCapNhat.GiaNhap != textGiaNhap.Text)
                {
                    sachCanCapNhat.GiaNhap = textGiaNhap.Text;
                }
                if (sachCanCapNhat.GiaBan != textGiaBan.Text)
                {
                    sachCanCapNhat.GiaBan = textGiaBan.Text;
                }
                if (sachCanCapNhat.SoLuong != texSoLuong.Text)
                {
                    sachCanCapNhat.SoLuong = texSoLuong.Text;
                }
                if (sachCanCapNhat.HinhSach != picHinhDaiDien.ImageLocation)
                {
                    sachCanCapNhat.HinhSach = picHinhDaiDien.ImageLocation;
                }

                // Ghi lại danh sách đã cập nhật vào tệp JSON
                File.WriteAllText(fileName, JsonConvert.SerializeObject(danhSachCu));

                MessageBox.Show("Đã cập nhật sách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không tìm thấy sách cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
